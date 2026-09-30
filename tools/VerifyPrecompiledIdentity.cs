using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
internal static class VerifyPrecompiledIdentity {
 static int methods, fields, types, bodies, attributes;
 static void Require(bool condition,string message) { if(!condition)throw new InvalidDataException(message); }
 static string Operand(object value,IList<Instruction> instructions,int skip) {
  if(value==null)return "";
  if(value is Instruction)return "@"+(instructions.IndexOf((Instruction)value)-skip);
  if(value is Instruction[])return string.Join(",",((Instruction[])value).Select(i=>Operand(i,instructions,skip)).ToArray());
  if(value is Local)return "local:"+((Local)value).Index;
  if(value is Parameter)return "arg:"+((Parameter)value).Index;
  if(value is IMemberRef)return ((IMemberRef)value).FullName;
  if(value is TypeSig)return ((TypeSig)value).FullName;
  if(value is IFormattable)return ((IFormattable)value).ToString(null,CultureInfo.InvariantCulture);
  return value.ToString();
 }
 static string Body(MethodDef m,int skip) {
  if(!m.HasBody)return "none";
  var il=m.Body.Instructions;
  List<string> parts=new List<string>();
  parts.Add(m.Body.InitLocals+":"+m.Body.MaxStack);
  parts.Add(string.Join(",",m.Body.Variables.Select(v=>v.Type.FullName).ToArray()));
  parts.AddRange(il.Skip(skip).Select(i=>i.OpCode+":"+Operand(i.Operand,il,skip)));
  foreach(var eh in m.Body.ExceptionHandlers)parts.Add(eh.HandlerType+":"+Operand(eh.TryStart,il,skip)+":"+Operand(eh.TryEnd,il,skip)+":"+Operand(eh.HandlerStart,il,skip)+":"+Operand(eh.HandlerEnd,il,skip)+":"+Operand(eh.FilterStart,il,skip)+":"+Operand(eh.CatchType,il,skip));
  return string.Join("\n",parts.ToArray());
 }
 static void Compare(string before,string after,bool payload) {
  using(var a=ModuleDefMD.Load(before)) using(var b=ModuleDefMD.Load(after)) {
   Require(a.Mvid==b.Mvid && a.Assembly.FullName==b.Assembly.FullName,"Identity changed "+a.Name);
   Require(b.MetaData.TablesStream.MethodPtrTable.Rows==0 && b.MetaData.TablesStream.FieldPtrTable.Rows==0,"Pointer table in "+b.Name);
   foreach(var t in a.GetTypes()) {
    var u=b.ResolveToken(t.MDToken) as TypeDef;
    Require(u!=null && u.FullName==t.FullName,"Type token changed "+t.FullName);types++;
    uint mask=payload?~(uint)TypeAttributes.VisibilityMask:uint.MaxValue;
    Require(((uint)t.Attributes&mask)==((uint)u.Attributes&mask),"Type flags changed "+t.FullName);
    if(payload)Require(u.Visibility==t.Visibility || u.IsPublic || u.IsNestedPublic,"Type visibility narrowed "+t.FullName);
    foreach(var f in t.Fields) {
     var g=b.ResolveToken(f.MDToken) as FieldDef;
     Require(g!=null && g.FullName==f.FullName && g.Attributes==f.Attributes,"Field changed "+f.FullName);fields++;
    }
    foreach(var m in t.Methods) {
     var n=b.ResolveToken(m.MDToken) as MethodDef;
     Require(n!=null && n.FullName==m.FullName && n.Attributes==m.Attributes && n.ImplAttributes==m.ImplAttributes,"Method changed "+m.FullName);methods++;
     bool gate=payload && t.FullName=="MPatcherFork.CustomPatches.MagnificationDownscale" && m.Name=="MenuUpdatePostfix";
     if(gate)Require(n.Body.Instructions[0].OpCode==OpCodes.Call && n.Body.Instructions[1].OpCode==OpCodes.Brtrue && n.Body.Instructions[2].OpCode==OpCodes.Ret,"Missing readiness gate");
     Require(Body(m,0)==Body(n,gate?3:0),"Original body changed "+m.FullName);if(m.HasBody)bodies++;
    }
   }
   var at=a.MetaData.TablesStream.CustomAttributeTable;var bt=b.MetaData.TablesStream.CustomAttributeTable;
   Require(at.Rows==bt.Rows,"Attribute count changed");
   for(uint i=1;i<=at.Rows;i++) {
    var ar=a.MetaData.TablesStream.ReadCustomAttributeRow(i);var br=b.MetaData.TablesStream.ReadCustomAttributeRow(i);
    Require(ar.Parent==br.Parent && ar.Type==br.Type && a.ReadCustomAttribute(i).GetBlob().SequenceEqual(b.ReadCustomAttribute(i).GetBlob()),"Attribute row changed "+i);attributes++;
   }
   foreach(var resource in a.Resources.OfType<EmbeddedResource>()) {
    var other=b.Resources.OfType<EmbeddedResource>().Single(x=>x.Name==resource.Name);
    Require(resource.Attributes==other.Attributes && resource.GetResourceData().SequenceEqual(other.GetResourceData()),"Original resource changed "+resource.Name);
   }
  }
 }
 static int Main(string[] args) {
  if(args.Length!=3){Console.Error.WriteLine("usage: VerifyPrecompiledIdentity originalPayload originalGame preparedDirectory");return 2;}
  try {Compare(args[0],Path.Combine(args[2],"MPatchrMain.dll"),true);Compare(args[1],Path.Combine(args[2],"Assembly-CSharp.dll"),false);Console.WriteLine("PASS types="+types+" fields="+fields+" methods="+methods+" bodies="+bodies+" attributeRows="+attributes+" allowedBodyChange=Alpha-readiness-gate");return 0;}
  catch(Exception e){Console.Error.WriteLine(e);return 1;}
 }
}