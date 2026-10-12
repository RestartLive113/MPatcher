using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using dnlib.DotNet.Writer;

// Offline only: preserve original bodies/tokens; append ready Harmony variants.
internal static class BuildPrecompiledPatches
{
    static Dictionary<string,ModuleDefMD> modules = new Dictionary<string,ModuleDefMD>();
    static string referenceDirectory;
    static Dictionary<string,ModuleDefMD> dependencies = new Dictionary<string,ModuleDefMD>();
    static Dictionary<string,MethodDef> bridges = new Dictionary<string,MethodDef>();
    static List<string> audit = new List<string>();
    static string Decode(string s) { return Encoding.UTF8.GetString(Convert.FromBase64String(s)); }
    static string Encode(string s) { return Convert.ToBase64String(Encoding.UTF8.GetBytes(s)); }
    static TypeDef NestedContainer(TypeDef parent)
    {
        TypeDef existing = parent.NestedTypes.FirstOrDefault(t => t.Name == "__MPatcher_Precompiled");
        if(existing != null) return existing;
        TypeDef created = new TypeDefUser("", "__MPatcher_Precompiled", parent.Module.CorLibTypes.Object.TypeDefOrRef) { Attributes=TypeAttributes.NestedPublic|TypeAttributes.Abstract|TypeAttributes.Sealed };
        parent.NestedTypes.Add(created);
        return created;
    }
    static TypeDef ResolveType(ITypeDefOrRef type)
    {
        if(type == null || type.DefinitionAssembly == null) return null;
        ModuleDefMD module;
        if(!modules.TryGetValue(type.DefinitionAssembly.Name, out module) && !dependencies.TryGetValue(type.DefinitionAssembly.Name, out module))
        {
            string path=Path.Combine(referenceDirectory,type.DefinitionAssembly.Name+".dll");
            if(!File.Exists(path)) return null;
            module=ModuleDefMD.Load(path);dependencies.Add(type.DefinitionAssembly.Name,module);
        }
        return module.Find(type.FullName,false);
    }
    static MethodDef ResolveMethod(IMethod method)
    {
        MethodDef def = method as MethodDef;
        if(def != null) return def;
        TypeDef type = ResolveType(method.DeclaringType);
        if(type == null) return null;
        return type.Methods.FirstOrDefault(m => m.Name == method.Name && new SigComparer().Equals(m.MethodSig, method.MethodSig));
    }
    static FieldDef ResolveField(IField field)
    {
        FieldDef def = field as FieldDef;
        if(def != null) return def;
        TypeDef type = ResolveType(field.DeclaringType);
        return type == null ? null : type.Fields.FirstOrDefault(f => f.Name == field.Name);
    }
    static void ExposeType(TypeDef type)
    {
        if(type == null || type.Module.Assembly.Name != "MPatchrMain") return;
        if(type.DeclaringType != null) { ExposeType(type.DeclaringType); type.Visibility = TypeAttributes.NestedPublic; }
        else type.Visibility = TypeAttributes.Public;
    }
    static void ExposeSig(TypeSig sig)
    {
        if(sig == null) return;
        ExposeType(ResolveType(sig.ToTypeDefOrRef()));
        ExposeSig(sig.Next);
        GenericInstSig generic = sig as GenericInstSig;
        if(generic != null) foreach(TypeSig arg in generic.GenericArguments) ExposeSig(arg);
    }
    static MethodDef MethodBridge(MethodDef target, OpCode opcode)
    {
        if(target.HasGenericParameters || target.DeclaringType.HasGenericParameters) throw new NotSupportedException("Generic private bridge " + target.FullName);
        string key = "M:" + target.MDToken + ":" + opcode;
        MethodDef found;
        if(bridges.TryGetValue(key, out found)) return found;
        List<TypeSig> parameters = target.MethodSig.Params.ToList();
        if(!target.IsStatic) parameters.Insert(0, target.DeclaringType.IsValueType ? (TypeSig)new ByRefSig(target.DeclaringType.ToTypeSig()) : target.DeclaringType.ToTypeSig());
        MethodDef method = new MethodDefUser("__MPatcher_Access_" + bridges.Count, MethodSig.CreateStatic(target.MethodSig.RetType, parameters.ToArray()), MethodImplAttributes.IL, MethodAttributes.Public | MethodAttributes.Static | MethodAttributes.HideBySig);
        NestedContainer(target.DeclaringType).Methods.Add(method);
        method.Body = new CilBody();
        for(int i=0;i<parameters.Count;i++) method.Body.Instructions.Add(Instruction.Create(OpCodes.Ldarg, method.Parameters[i]));
        method.Body.Instructions.Add(Instruction.Create(opcode, target));
        method.Body.Instructions.Add(Instruction.Create(OpCodes.Ret));
        ExposeType(target.DeclaringType); ExposeSig(target.MethodSig.RetType);
        foreach(TypeSig p in parameters) ExposeSig(p);
        bridges.Add(key, method); audit.Add("METHOD_BRIDGE\t" + target.FullName);
        return method;
    }
    static MethodDef FieldBridge(FieldDef target, OpCode opcode)
    {
        string key = "F:" + target.MDToken + ":" + opcode;
        MethodDef found;
        if(bridges.TryGetValue(key, out found)) return found;
        bool store = opcode == OpCodes.Stfld || opcode == OpCodes.Stsfld;
        bool address = opcode == OpCodes.Ldflda || opcode == OpCodes.Ldsflda;
        List<TypeSig> args = new List<TypeSig>();
        if(!target.IsStatic) args.Add(target.DeclaringType.IsValueType ? (TypeSig)new ByRefSig(target.DeclaringType.ToTypeSig()) : target.DeclaringType.ToTypeSig());
        if(store) args.Add(target.FieldType);
        TypeSig result = store ? target.Module.CorLibTypes.Void : address ? (TypeSig)new ByRefSig(target.FieldType) : target.FieldType;
        MethodDef method = new MethodDefUser("__MPatcher_Access_" + bridges.Count, MethodSig.CreateStatic(result, args.ToArray()), MethodImplAttributes.IL, MethodAttributes.Public | MethodAttributes.Static | MethodAttributes.HideBySig);
        NestedContainer(target.DeclaringType).Methods.Add(method); method.Body = new CilBody();
        for(int i=0;i<args.Count;i++) method.Body.Instructions.Add(Instruction.Create(OpCodes.Ldarg,method.Parameters[i]));
        method.Body.Instructions.Add(Instruction.Create(opcode,target)); method.Body.Instructions.Add(Instruction.Create(OpCodes.Ret));
        ExposeType(target.DeclaringType); ExposeSig(target.FieldType);
        bridges.Add(key,method); audit.Add("FIELD_BRIDGE\t"+target.FullName); return method;
    }
    static object ImportOperand(object value, Importer importer)
    {
        if(value is IMethod) return importer.Import((IMethod)value);
        if(value is IField) return importer.Import((IField)value);
        if(value is ITypeDefOrRef) return importer.Import((ITypeDefOrRef)value);
        if(value is TypeSig) return importer.Import((TypeSig)value);
        if(value is MethodSig) return importer.Import((MethodSig)value);
        return value;
    }
    static void PreserveAttributes(IHasCustomAttribute item)
    {
        for(int i=0;i<item.CustomAttributes.Count;i++)
        {
            CustomAttribute attribute=item.CustomAttributes[i];
            item.CustomAttributes[i]=new CustomAttribute(attribute.Constructor,attribute.GetBlob());
        }
    }
    static void PreserveAllAttributes(ModuleDefMD module)
    {
        PreserveAttributes(module);PreserveAttributes(module.Assembly);
        foreach(TypeDef type in module.GetTypes())
        {
            PreserveAttributes(type);
            foreach(GenericParam gp in type.GenericParameters)PreserveAttributes(gp);
            foreach(MethodDef method in type.Methods){PreserveAttributes(method);foreach(ParamDef p in method.ParamDefs)PreserveAttributes(p);foreach(GenericParam gp in method.GenericParameters)PreserveAttributes(gp);}
            foreach(FieldDef field in type.Fields)PreserveAttributes(field);
            foreach(PropertyDef property in type.Properties)PreserveAttributes(property);
            foreach(EventDef ev in type.Events)PreserveAttributes(ev);
        }
    }
    static string TypeShape(TypeSig sig, GenericInstSig owner, bool substitute)
    {
        GenericVar var=sig as GenericVar;if(var!=null)return substitute && var.Number<owner.GenericArguments.Count?TypeShape(owner.GenericArguments[(int)var.Number],owner,false):"!"+var.Number;
        GenericMVar methodVar=sig as GenericMVar;if(methodVar!=null)return "!!"+methodVar.Number;
        GenericInstSig generic=sig as GenericInstSig;if(generic!=null)return generic.GenericType.FullName+"<"+string.Join(",",generic.GenericArguments.Select(t=>TypeShape(t,owner,substitute)).ToArray())+">";
        if(sig is SZArraySig)return TypeShape(sig.Next,owner,substitute)+"[]";
        if(sig is ByRefSig)return TypeShape(sig.Next,owner,substitute)+"&";
        if(sig is PtrSig)return TypeShape(sig.Next,owner,substitute)+"*";
        return sig.FullName;
    }
    static string CloseSignature(MethodSig signature, GenericInstSig owner)
    {
        return signature.HasThis+":"+signature.GenParamCount+":"+"("+string.Join(",",signature.Params.Select(p=>TypeShape(p,owner,true)).ToArray())+")";
    }    static void RepairGenericMember(IMethod method, ModuleDef module)
    {
        MemberRef member=method as MemberRef;if(member==null)return;
        TypeSpec spec=member.DeclaringType as TypeSpec;if(spec==null)return;
        GenericInstSig generic=spec.TypeSig as GenericInstSig;if(generic==null)return;
        TypeDef definition=ResolveType(generic.GenericType.TypeDefOrRef);if(definition==null)throw new InvalidDataException("Missing generic owner "+member.FullName);
        string wanted=CloseSignature(member.MethodSig,generic);
        MethodDef[] matches=definition.Methods.Where(m=>m.Name==member.Name && CloseSignature(m.MethodSig,generic)==wanted).ToArray();
        if(matches.Length!=1)throw new InvalidDataException("Generic member match "+matches.Length+" "+member.FullName);
        if(!new SigComparer().Equals(member.MethodSig,matches[0].MethodSig))
        {
            member.Signature=new Importer(module).Import(matches[0].MethodSig);
            audit.Add("GENERIC_SIGNATURE\t"+member.FullName);
        }
    }
    static void NormalizeType(ITypeDefOrRef type, ModuleDef module)
    {
        TypeSpec spec=type as TypeSpec; if(spec!=null){NormalizeSig(spec.TypeSig,module);return;}
        TypeRef tr=type as TypeRef; if(tr==null)return;
        if(tr.DeclaringType!=null)NormalizeType(tr.DeclaringType,module);
        else if(tr.ResolutionScope is ModuleRef)
        {
            if(module.Find(tr.FullName,false)==null)throw new InvalidDataException("Invalid imported module scope "+tr.FullName+" in "+module.Name);
            tr.ResolutionScope=module;
        }
    }
    static void NormalizeSig(TypeSig sig, ModuleDef module)
    {
        if(sig==null)return;
        TypeDefOrRefSig tr=sig as TypeDefOrRefSig;if(tr!=null)NormalizeType(tr.TypeDefOrRef,module);
        GenericInstSig generic=sig as GenericInstSig;if(generic!=null){NormalizeSig(generic.GenericType,module);foreach(TypeSig arg in generic.GenericArguments)NormalizeSig(arg,module);}
        NormalizeSig(sig.Next,module);
    }
    static void NormalizeMethod(IMethod method, ModuleDef module)
    {
        RepairGenericMember(method,module);
        NormalizeType(method.DeclaringType,module);
        if(method.MethodSig!=null){NormalizeSig(method.MethodSig.RetType,module);foreach(TypeSig p in method.MethodSig.Params)NormalizeSig(p,module);}
        MethodSpec spec=method as MethodSpec;if(spec!=null){NormalizeMethod(spec.Method,module);foreach(TypeSig p in spec.GenericInstMethodSig.GenericArguments)NormalizeSig(p,module);}
    }
    static void NormalizeBody(MethodDef method)
    {
        ModuleDef module=method.Module;NormalizeMethod(method,module);
        foreach(Local local in method.Body.Variables)NormalizeSig(local.Type,module);
        foreach(ExceptionHandler eh in method.Body.ExceptionHandlers)NormalizeType(eh.CatchType,module);
        foreach(Instruction i in method.Body.Instructions)
        {
            if(i.Operand is IMethod)NormalizeMethod((IMethod)i.Operand,module);
            else if(i.Operand is IField){IField field=(IField)i.Operand;NormalizeType(field.DeclaringType,module);NormalizeSig(field.FieldSig.Type,module);}
            else if(i.Operand is ITypeDefOrRef)NormalizeType((ITypeDefOrRef)i.Operand,module);
        }
    }
    static MethodDef Clone(MethodDef source, TypeDef owner, string name, bool dynamic)
    {
        Importer importer = new Importer(owner.Module, ImporterOptions.TryToUseTypeDefs);
        MethodDef dest = new MethodDefUser(name, importer.Import(source.MethodSig), MethodImplAttributes.IL | MethodImplAttributes.NoInlining, MethodAttributes.Private | MethodAttributes.Static | MethodAttributes.HideBySig);
        owner.Methods.Add(dest);
        CilBody body = new CilBody { InitLocals=source.Body.InitLocals, MaxStack=source.Body.MaxStack, KeepOldMaxStack=true };
        dest.Body=body;
        Dictionary<Local,Local> locals=new Dictionary<Local,Local>();
        foreach(Local local in source.Body.Variables) { Local item=new Local(importer.Import(local.Type)); locals.Add(local,item);body.Variables.Add(item); if(!dynamic) ExposeSig(local.Type); }
        Dictionary<Instruction,Instruction> instructions=new Dictionary<Instruction,Instruction>();
        foreach(Instruction src in source.Body.Instructions) { Instruction item=new Instruction(src.OpCode,null); instructions.Add(src,item);body.Instructions.Add(item); }
        foreach(Instruction src in source.Body.Instructions)
        {
            Instruction item=instructions[src]; object operand=src.Operand;
            if(operand is Instruction) operand=instructions[(Instruction)operand];
            else if(operand is Instruction[]) operand=((Instruction[])operand).Select(i=>instructions[i]).ToArray();
            else if(operand is Local) operand=locals[(Local)operand];
            else if(operand is Parameter) operand=dest.Parameters[((Parameter)operand).Index];
            else
            {
                if(!dynamic && operand is IMethod)
                {
                    MethodDef target=ResolveMethod((IMethod)operand);
                    if(target!=null && target.Module.Assembly.Name=="MPatchrMain" && target.DeclaringType != owner)
                    {
                        ExposeType(target.DeclaringType);
                        if(!target.IsPublic && (src.OpCode==OpCodes.Call || src.OpCode==OpCodes.Callvirt)) { operand=MethodBridge(target,src.OpCode); item.OpCode=OpCodes.Call; }
                        ExposeSig(target.MethodSig.RetType); foreach(TypeSig p in target.MethodSig.Params) ExposeSig(p);
                    }
                }
                else if(!dynamic && operand is IField)
                {
                    FieldDef target=ResolveField((IField)operand);
                    if(target!=null && target.Module.Assembly.Name=="MPatchrMain" && target.DeclaringType != owner)
                    {
                        ExposeType(target.DeclaringType); ExposeSig(target.FieldType);
                        if(!target.IsPublic && src.OpCode!=OpCodes.Ldtoken) { operand=FieldBridge(target,src.OpCode); item.OpCode=OpCodes.Call; }
                    }
                }
                else if(!dynamic && operand is ITypeDefOrRef) ExposeType(ResolveType((ITypeDefOrRef)operand));
                operand=ImportOperand(operand,importer);
            }
            item.Operand=operand;
        }
        foreach(ExceptionHandler eh in source.Body.ExceptionHandlers)
            body.ExceptionHandlers.Add(new ExceptionHandler(eh.HandlerType) { TryStart=eh.TryStart==null?null:instructions[eh.TryStart], TryEnd=eh.TryEnd==null?null:instructions[eh.TryEnd], HandlerStart=eh.HandlerStart==null?null:instructions[eh.HandlerStart], HandlerEnd=eh.HandlerEnd==null?null:instructions[eh.HandlerEnd], FilterStart=eh.FilterStart==null?null:instructions[eh.FilterStart], CatchType=eh.CatchType==null?null:(ITypeDefOrRef)importer.Import(eh.CatchType) });
        NormalizeBody(dest);
        body.SimplifyBranches(); body.OptimizeBranches();
        return dest;
    }
    static void GateAlphaStartup()
    {
        ModuleDefMD payload=modules["MPatchrMain"];
        TypeDef gate=payload.Find("MPatcherFork.CustomPatches.MPatcherStartupGate",false);
        FieldDef ready=gate.Fields.Single(f=>f.Name=="runtimeReady");
        MethodDef read=FieldBridge(ready,OpCodes.Ldsfld);
        MethodDef update=payload.Find("MPatcherFork.CustomPatches.MagnificationDownscale",false).Methods.Single(m=>m.Name=="MenuUpdatePostfix");
        Instruction first=update.Body.Instructions[0];
        update.Body.Instructions.Insert(0,Instruction.Create(OpCodes.Call,read));
        update.Body.Instructions.Insert(1,Instruction.Create(OpCodes.Brtrue,first));
        update.Body.Instructions.Insert(2,Instruction.Create(OpCodes.Ret));
        audit.Add("STARTUP_ORDER\tAlpha waits for MPatcherStartupGate.runtimeReady");
    }
    static void RestoreAttributeOrder(ModuleDefMD original, string path)
    {
        byte[] image=File.ReadAllBytes(path);
        using(ModuleDefMD written=ModuleDefMD.Load(image))
        {
            var table=written.MetaData.TablesStream.CustomAttributeTable;
            if(table.Rows!=original.MetaData.TablesStream.CustomAttributeTable.Rows)throw new InvalidDataException("Unexpected attribute count");
            Dictionary<string,Queue<byte[]>> rows=new Dictionary<string,Queue<byte[]>>();
            for(uint rid=1;rid<=table.Rows;rid++)
            {
                var row=written.MetaData.TablesStream.ReadCustomAttributeRow(rid);
                string key=row.Parent+":"+row.Type+":"+Convert.ToBase64String(written.ReadCustomAttribute(rid).GetBlob());
                Queue<byte[]> queue;if(!rows.TryGetValue(key,out queue)){queue=new Queue<byte[]>();rows.Add(key,queue);}
                byte[] bytes=new byte[table.RowSize];Buffer.BlockCopy(image,(int)((uint)table.StartOffset+(rid-1)*table.RowSize),bytes,0,bytes.Length);queue.Enqueue(bytes);
            }
            for(uint rid=1;rid<=table.Rows;rid++)
            {
                var row=original.MetaData.TablesStream.ReadCustomAttributeRow(rid);
                string key=row.Parent+":"+row.Type+":"+Convert.ToBase64String(original.ReadCustomAttribute(rid).GetBlob());
                Queue<byte[]> queue;if(!rows.TryGetValue(key,out queue)||queue.Count==0)throw new InvalidDataException("Attribute changed "+key);
                byte[] bytes=queue.Dequeue();Buffer.BlockCopy(bytes,0,image,(int)((uint)table.StartOffset+(rid-1)*table.RowSize),bytes.Length);
            }
        }
        File.WriteAllBytes(path,image);
    }
    static int Main(string[] args)
    {
        if(args.Length!=4) { Console.Error.WriteLine("usage: BuildPrecompiledPatches capture payload refs output");return 2; }
        string capture=Path.GetFullPath(args[0]), output=Path.GetFullPath(args[3]);
        Directory.CreateDirectory(output);referenceDirectory=args[2];
        foreach(string name in new[]{"Assembly-CSharp","MPatchrMain"}) modules.Add(name,ModuleDefMD.Load(name=="MPatchrMain"?args[1]:Path.Combine(args[2],name+".dll")));
        foreach(ModuleDefMD input in modules.Values)PreserveAllAttributes(input);
        foreach(ModuleDefMD input in modules.Values) foreach(TypeDef inputType in input.GetTypes()) foreach(MethodDef inputMethod in inputType.Methods) if(inputMethod.HasBody) inputMethod.Body.KeepOldMaxStack=true;
        GateAlphaStartup();
        TypeDef carrier=new TypeDefUser("MPatcherFork.Generated","CachedRuntimeIL",modules["MPatchrMain"].CorLibTypes.Object.TypeDefOrRef) { Attributes=TypeAttributes.NotPublic|TypeAttributes.Abstract|TypeAttributes.Sealed };
        modules["MPatchrMain"].Types.Add(carrier);
        List<string> manifest=new List<string>();HashSet<string> keys=new HashSet<string>();
        foreach(string line in File.ReadAllLines(Path.Combine(capture,"plans.tsv")))
        {
            string[] fields=line.Split('\t'); if(!keys.Add(fields[0])) continue;
            string[] identity=Decode(fields[1]).Split('|'); string assembly=identity[0].Split(',')[0];
            ModuleDefMD module; TypeDef owner; bool dynamic=!modules.TryGetValue(assembly,out module);
            if(dynamic) owner=carrier;
            else
            {
                if(module.Mvid.ToString()!=identity[1]) throw new InvalidDataException("MVID mismatch "+assembly);
                MethodDef original=module.ResolveToken(Convert.ToUInt32(identity[2],16)) as MethodDef;
                if(original==null) throw new InvalidDataException("Target missing "+identity[3]);
                owner=original.DeclaringType;
                if(owner.HasGenericParameters || original.HasGenericParameters) { dynamic=true;owner=carrier; } else owner=NestedContainer(owner);
            }
            using(ModuleDefMD exported=ModuleDefMD.Load(Path.Combine(capture,Decode(fields[8])+".dll")))
            {
                exported.Name=owner.Module.Name; // Legacy dnlib uses source module name for self-assembly TypeRefs.
                MethodDef src=exported.GetTypes().SelectMany(t=>t.Methods).Single(m=>m.Name==Decode(fields[10]));
                MethodDef added=Clone(src,owner,"__MPatcher_Precompiled_"+fields[0],dynamic);
                manifest.Add(fields[0]+"\t"+Encode(owner.Module.Assembly.Name)+"\t"+Encode((dynamic?"~":"")+owner.ReflectionFullName)+"\t"+Encode(added.Name));
                audit.Add("PLAN\t"+fields[0]+"\t"+(dynamic?"cached-dynamic":"precompiled")+"\t"+identity[3]);
            }
        }
        modules["MPatchrMain"].Resources.Add(new EmbeddedResource("MPatcher.Prepatch.Plans.tsv",Encoding.UTF8.GetBytes(string.Join("\n",manifest.ToArray())),ManifestResourceAttributes.Private));
        foreach(var pair in modules)
        {
            ModuleWriterOptions options=new ModuleWriterOptions(pair.Value);options.MetaDataOptions.Flags|=MetaDataFlags.PreserveAll;
            pair.Value.Write(Path.Combine(output,pair.Key+".dll"),options);
            RestoreAttributeOrder(pair.Value,Path.Combine(output,pair.Key+".dll"));
            using(ModuleDefMD check=ModuleDefMD.Load(Path.Combine(output,pair.Key+".dll")))
                foreach(TypeDef type in pair.Value.GetTypes()) foreach(MethodDef method in type.Methods)
                    if(method.GetType().Name == "MethodDefMD" && ((MethodDef)check.ResolveToken(method.MDToken)).FullName!=method.FullName) throw new InvalidDataException("Original method token changed "+method.FullName);
        }
        File.WriteAllLines(Path.Combine(output,"audit.tsv"),audit.ToArray());
        File.WriteAllLines(Path.Combine(output,"plans.tsv"),manifest.ToArray());
        Console.WriteLine("Plans="+manifest.Count+" Bridges="+bridges.Count);return 0;
    }
}
