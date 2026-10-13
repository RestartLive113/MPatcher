param(
    [Parameter(Mandatory=$true)][string]$InputAssemblyPath,
    [Parameter(Mandatory=$true)][string]$OutputAssemblyPath,
    [Parameter(Mandatory=$true)][string]$ProofPath,
    [string]$DnlibPath=(Join-Path $PSScriptRoot 'bin\dnlib.dll')
)
$ErrorActionPreference='Stop'
Set-StrictMode -Version Latest
$inputPath=(Resolve-Path -LiteralPath $InputAssemblyPath).Path
$outputPath=[IO.Path]::GetFullPath($OutputAssemblyPath)
$repositoryRoot=[IO.Path]::GetFullPath((Join-Path $PSScriptRoot '..'))
$artifactRoots=@((Join-Path $repositoryRoot 'build'),(Join-Path $repositoryRoot 'analysis')) | ForEach-Object {[IO.Path]::GetFullPath($_)+[IO.Path]::DirectorySeparatorChar}
if(@($artifactRoots | Where-Object {$outputPath.StartsWith($_,[StringComparison]::OrdinalIgnoreCase)}).Count -eq 0){throw 'Output must remain inside repository build/analysis artifacts.'}
if($inputPath -eq $outputPath -or (Test-Path -LiteralPath $outputPath)){throw 'Output must be a new analysis/build candidate, never the input.'}
$null=[Reflection.Assembly]::LoadFrom((Resolve-Path -LiteralPath $DnlibPath).Path)
function Hash-Bytes([byte[]]$data) { $sha=[Security.Cryptography.SHA256]::Create(); try {([BitConverter]::ToString($sha.ComputeHash($data))).Replace('-','')} finally {$sha.Dispose()} }
function Preserve-Attributes($item) {
    for($i=0;$i -lt $item.CustomAttributes.Count;$i++){$attribute=$item.CustomAttributes[$i];$item.CustomAttributes[$i]=[dnlib.DotNet.CustomAttribute]::new($attribute.Constructor,[byte[]]$attribute.GetBlob())}
}
function Preserve-AllAttributes($module) {
    Preserve-Attributes $module; Preserve-Attributes $module.Assembly
    foreach($type in $module.GetTypes()) {
        Preserve-Attributes $type; foreach($p in $type.GenericParameters){Preserve-Attributes $p}
        foreach($m in $type.Methods){Preserve-Attributes $m; foreach($p in $m.ParamDefs){Preserve-Attributes $p}; foreach($p in $m.GenericParameters){Preserve-Attributes $p}}
        foreach($f in $type.Fields){Preserve-Attributes $f}; foreach($p in $type.Properties){Preserve-Attributes $p}; foreach($e in $type.Events){Preserve-Attributes $e}
    }
}
function Attribute-Key($module,[uint32]$rid) {
    $row=$module.MetaData.TablesStream.ReadCustomAttributeRow($rid)
    return $row.Parent.ToString()+':'+$row.Type.ToString()+':'+[Convert]::ToBase64String($module.ReadCustomAttribute($rid).GetBlob())
}
function Restore-AttributeOrder($before,[string]$path) {
    [byte[]]$bytes=[IO.File]::ReadAllBytes($path); $after=[dnlib.DotNet.ModuleDefMD]::Load($bytes)
    try {
        $table=$after.MetaData.TablesStream.CustomAttributeTable
        if($table.Rows -ne $before.MetaData.TablesStream.CustomAttributeTable.Rows){throw 'Attribute count changed'}
        $rows=[Collections.Generic.Dictionary[string,Collections.Generic.Queue[byte[]]]]::new([StringComparer]::Ordinal)
        for([uint32]$rid=1;$rid -le $table.Rows;$rid++) {
            $key=Attribute-Key $after $rid
            if(-not $rows.ContainsKey($key)){$rows[$key]=[Collections.Generic.Queue[byte[]]]::new()}
            $rowBytes=[byte[]]::new($table.RowSize)
            [Buffer]::BlockCopy($bytes,[int]([uint32]$table.StartOffset+($rid-1)*$table.RowSize),$rowBytes,0,$rowBytes.Length)
            $rows[$key].Enqueue($rowBytes)
        }
        for([uint32]$rid=1;$rid -le $table.Rows;$rid++) {
            $key=Attribute-Key $before $rid
            if(-not $rows.ContainsKey($key) -or $rows[$key].Count -eq 0){throw ('Attribute missing at RID '+$rid)}
            $rowBytes=$rows[$key].Dequeue()
            [Buffer]::BlockCopy($rowBytes,0,$bytes,[int]([uint32]$table.StartOffset+($rid-1)*$table.RowSize),$rowBytes.Length)
        }
    } finally {$after.Dispose()}
    [IO.File]::WriteAllBytes($path,$bytes)
}
function Body-Fingerprint($method) {
    if(-not $method.HasBody){return '<none>'}
    $body=$method.Body; $indexes=@{}; for($i=0;$i -lt $body.Instructions.Count;$i++){$indexes[$body.Instructions[$i]]=$i}
    function Operand-Text($operand) {
        if($null -eq $operand){return ''}
        if($operand -is [dnlib.DotNet.Emit.Instruction]){return 'target:'+$indexes[$operand]}
        if($operand -is [dnlib.DotNet.Emit.Instruction[]]){return 'switch:'+(@($operand | ForEach-Object {$indexes[$_]}) -join ',')}
        if($operand -is [dnlib.DotNet.Emit.Local]){return 'local:'+$operand.Index}
        if($operand -is [dnlib.DotNet.Parameter]){return 'parameter:'+$operand.Index}
        if($operand -is [string]){return 'string:'+([Convert]::ToBase64String([Text.Encoding]::UTF8.GetBytes($operand)))}
        if($operand -is [dnlib.DotNet.IMethod] -or $operand -is [dnlib.DotNet.IField] -or $operand -is [dnlib.DotNet.ITypeDefOrRef]){return $operand.FullName}
        if($operand -is [IFormattable]){return $operand.ToString($null,[Globalization.CultureInfo]::InvariantCulture)}
        return $operand.ToString()
    }
    $text=[Text.StringBuilder]::new()
    $null=$text.AppendLine('init='+$body.InitLocals+';max='+$body.MaxStack)
    foreach($local in $body.Variables){$null=$text.AppendLine('local='+$local.Type.FullName)}
    foreach($instruction in $body.Instructions){$null=$text.AppendLine($instruction.OpCode.Code.ToString()+'|'+(Operand-Text $instruction.Operand))}
    foreach($handler in $body.ExceptionHandlers){$null=$text.AppendLine('EH='+$handler.HandlerType+'|'+(Operand-Text $handler.TryStart)+'|'+(Operand-Text $handler.TryEnd)+'|'+(Operand-Text $handler.HandlerStart)+'|'+(Operand-Text $handler.HandlerEnd)+'|'+(Operand-Text $handler.FilterStart)+'|'+(Operand-Text $handler.CatchType))}
    return Hash-Bytes ([Text.Encoding]::UTF8.GetBytes($text.ToString()))
}
$module=[dnlib.DotNet.ModuleDefMD]::Load($inputPath)
try {
    $allMethods=@($module.GetTypes() | ForEach-Object Methods)
    $decoder=@($allMethods | Where-Object { $_.Name.String -eq 'smethod_28' -and $_.GenericParameters.Count -eq 1 })
    if($decoder.Count -ne 1){throw 'Expected exactly one module smethod_28<T> decoder'}
    $decoder=$decoder[0]; $constantType=$decoder.DeclaringType
    $ctor=@($constantType.Methods | Where-Object Name -eq '.cctor')[0]
    $init=$ctor.Body.Instructions
    if($init.Count -ne 5 -or $init[0].OpCode.Code -ne [dnlib.DotNet.Emit.Code]::Ldstr -or $init[1].Operand.FullName -notlike '*Convert::FromBase64String*' -or $init[2].OpCode.Code -ne [dnlib.DotNet.Emit.Code]::Stsfld -or $init[2].Operand.Name.String -ne 'byte_0'){throw 'Unrecognized constant initializer'}
    $blob=[Convert]::FromBase64String($init[0].Operand); $blobHash=Hash-Bytes $blob
    if($blob.Length -ne 39644 -or $blobHash -ne 'E650314F8E41D753F4D1BA3531BFA17DB15754EE11447E1EECA2816DCE06A036'){throw 'Constants provenance mismatch'}
    $decoderHash=Body-Fingerprint $decoder
    if($decoderHash -ne '6D7D7F0D731F43F412673387D1C55411EF2F8E5D0EF1FDA462BD00BAB76A9209'){throw 'Exact decoder instruction graph changed; re-audit required'}
    $constantWriters=@($allMethods | Where-Object HasBody | ForEach-Object { $writer=$_; foreach($instruction in $writer.Body.Instructions){if($instruction.OpCode.Code -eq [dnlib.DotNet.Emit.Code]::Stsfld -and $instruction.Operand -eq $init[2].Operand){$writer}} })
    if($constantWriters.Count -ne 1 -or $constantWriters[0] -ne $ctor){throw 'Constant blob has an unexpected direct writer'}
    $literalInts=@($decoder.Body.Instructions | Where-Object {$_.IsLdcI4()} | ForEach-Object {$_.GetLdcI4Value()})
    if($literalInts -notcontains 1716630677 -or $literalInts -notcontains 228143951){throw 'Decoder arithmetic changed'}
    $seed=[uint64]2987648538; $encoded=[uint32]((($seed*1716630677) -band [uint64]4294967295) -bxor [uint64]228143951)
    if(($encoded -shr 30) -ne 3){throw 'Literal is not a string constant'}
    $offset=[int](($encoded -band [uint32]1073741823) -shl 2); $length=[BitConverter]::ToInt32($blob,$offset)
    if($length -ne 5 -or $offset+4+$length -gt $blob.Length){throw 'Unexpected scene literal bounds'}
    $value=[Text.Encoding]::UTF8.GetString($blob,$offset+4,$length)
    if($value -ne 'Build'){throw 'Unexpected scene literal value'}
    $method=@($allMethods | Where-Object { $_.DeclaringType.FullName -eq 'Class58' -and $_.Name.String -eq 'smethod_0' })
    if($method.Count -ne 1){throw 'Expected exactly one tracker prefix'}
    $method=$method[0]; $codes=$method.Body.Instructions; $sites=@()
    for($i=1;$i -lt $codes.Count;$i++){
        $op=$codes[$i].Operand
        if($codes[$i].OpCode.Code -eq [dnlib.DotNet.Emit.Code]::Call -and $op -is [dnlib.DotNet.MethodSpec] -and $op.Method -eq $decoder -and $op.GenericInstMethodSig.GenericArguments.Count -eq 1 -and $op.GenericInstMethodSig.GenericArguments[0].FullName -eq 'System.String' -and $codes[$i-1].IsLdcI4() -and [BitConverter]::ToUInt32([BitConverter]::GetBytes([int]$codes[$i-1].GetLdcI4Value()),0) -eq $seed){$sites+=$i}
    }
    if($sites.Count -ne 1 -or $sites[0] -ne 5){throw 'Expected one first scene-name decoder call at instruction 5'}
    $at=$sites[0]
    if($codes[$at-2].Operand.Name.String -ne 'get_name' -or $codes[$at-2].Operand.DeclaringType.FullName -ne 'UnityEngine.SceneManagement.Scene'){throw 'Scene read order changed'}
    foreach($ins in $codes){if($ins.Operand -eq $codes[$at] -or ($ins.Operand -is [dnlib.DotNet.Emit.Instruction[]] -and $ins.Operand -contains $codes[$at])){throw 'Unexpected branch entering decoder with independently prepared argument'}}
    $tokens=@($module.GetTypes() | ForEach-Object { $_.MDToken.ToString()+'|'+$_.FullName; $_.Fields | ForEach-Object {$_.MDToken.ToString()+'|'+$_.FullName}; $_.Methods | ForEach-Object {$_.MDToken.ToString()+'|'+$_.FullName} })
    $before=@{}; foreach($m in $allMethods){$before[$m.MDToken.ToString()]=Body-Fingerprint $m}
    $resources=@($module.Resources | ForEach-Object { if($_ -is [dnlib.DotNet.EmbeddedResource]){$_.Name.String+'|'+(Hash-Bytes $_.GetResourceData())} else {$_.ToString()} })
    $originalCtor=Body-Fingerprint $ctor; $mvid=$module.Mvid.ToString(); $inputHash=(Get-FileHash -LiteralPath $inputPath).Hash
    $assemblyRefs=@($module.GetAssemblyRefs() | ForEach-Object {$_.MDToken.ToString()+'|'+$_.FullName})
    $helpers=[dnlib.DotNet.TypeRefUser]::new($module,'System.Runtime.CompilerServices','RuntimeHelpers',$module.CorLibTypes.AssemblyRef)
    $existingHandle=@($module.GetTypeRefs() | Where-Object FullName -eq 'System.RuntimeTypeHandle')
    if($existingHandle.Count -ne 1){throw 'Expected one existing RuntimeTypeHandle reference'}
    $handleType=[dnlib.DotNet.ValueTypeSig]::new($existingHandle[0])
    $signature=[dnlib.DotNet.MethodSig]::CreateStatic($module.CorLibTypes.Void,$handleType)
    $runCtor=[dnlib.DotNet.MemberRefUser]::new($module,'RunClassConstructor',$signature,$helpers)
    # The old two instruction objects retain all incoming branch/EH references.
    # Preserve lazy type initialization and constructor failure at the old call point.
    $codes[$at-1].OpCode=[dnlib.DotNet.Emit.OpCodes]::Ldtoken; $codes[$at-1].Operand=$constantType
    $codes[$at].Operand=$runCtor
    $codes.Insert($at+1,[dnlib.DotNet.Emit.Instruction]::Create([dnlib.DotNet.Emit.OpCodes]::Ldstr,$value))
    $method.Body.KeepOldMaxStack=$true
    foreach($m in $allMethods){if($m.HasBody){$m.Body.KeepOldMaxStack=$true}}
    $expected=Body-Fingerprint $method
    $options=[dnlib.DotNet.Writer.ModuleWriterOptions]::new($module)
    $options.MetadataOptions.Flags=[dnlib.DotNet.Writer.MetadataFlags]::PreserveAll
    Preserve-AllAttributes $module
    [IO.Directory]::CreateDirectory([IO.Path]::GetDirectoryName($outputPath)) | Out-Null
    $module.Write($outputPath,$options)
    Restore-AttributeOrder $module $outputPath
    $written=[dnlib.DotNet.ModuleDefMD]::Load($outputPath)
    try {
        $afterTokens=@($written.GetTypes() | ForEach-Object { $_.MDToken.ToString()+'|'+$_.FullName; $_.Fields | ForEach-Object {$_.MDToken.ToString()+'|'+$_.FullName}; $_.Methods | ForEach-Object {$_.MDToken.ToString()+'|'+$_.FullName} })
        if(($tokens -join "`n") -ne ($afterTokens -join "`n") -or $written.Mvid.ToString() -ne $mvid){throw 'Definitions/tokens/MVID changed'}
        $afterAssemblyRefs=@($written.GetAssemblyRefs() | ForEach-Object {$_.MDToken.ToString()+'|'+$_.FullName})
        if(($assemblyRefs -join "`n") -ne ($afterAssemblyRefs -join "`n")){throw 'Assembly references changed'}
        $attributeCount=$module.MetaData.TablesStream.CustomAttributeTable.Rows
        if($written.MetaData.TablesStream.CustomAttributeTable.Rows -ne $attributeCount){throw 'Custom attribute count changed'}
        for([uint32]$rid=1;$rid -le $attributeCount;$rid++){if((Attribute-Key $module $rid) -ne (Attribute-Key $written $rid)){throw ('Custom attribute row/blob changed at RID '+$rid)}}
        $afterResources=@($written.Resources | ForEach-Object { if($_ -is [dnlib.DotNet.EmbeddedResource]){$_.Name.String+'|'+(Hash-Bytes $_.GetResourceData())} else {$_.ToString()} })
        if(($resources -join "`n") -ne ($afterResources -join "`n")){throw 'Embedded resources changed'}
        $changed=0; $unchanged=0
        foreach($m in $written.GetTypes() | ForEach-Object Methods){$key=$m.MDToken.ToString();$fingerprint=Body-Fingerprint $m;if($key -eq $method.MDToken.ToString()){if($fingerprint -ne $expected){throw 'Written scene fold differs from exact expected instruction graph'};$changed++}elseif($fingerprint -ne $before[$key]){throw ('Other method body changed '+$m.FullName)}else{$unchanged++}}
        if($changed -ne 1 -or (Body-Fingerprint $written.ResolveToken($ctor.MDToken.Raw)) -ne $originalCtor){throw 'Constructor/changed-method count proof failed'}
        $proof=[pscustomobject]@{Passed=$true;Input=$inputPath;InputSHA256=$inputHash;Output=$outputPath;OutputSHA256=(Get-FileHash -LiteralPath $outputPath).Hash;Mvid=$mvid;MethodToken=$method.MDToken.ToString();Method=$method.FullName;ConstantsSHA256=$blobHash;ConstantsBytes=$blob.Length;Seed=$seed;Value=$value;BlobOffset=$offset;ConstructorPreserved=$true;Initialization='ldtoken same constant type -> RuntimeHelpers.RunClassConstructor -> ldstr Build at original decoder call point';ChangedBodies=$changed;UnchangedBodies=$unchanged;AllDefinitionTokensExact=$true;AssemblyReferencesExact=$true;CustomAttributeRowsAndBlobsExact=$true;CustomAttributeCount=$attributeCount;CorLibReference=$module.CorLibTypes.AssemblyRef.FullName;ResourcesExact=$true;ExpectedInstructionGraphSHA256=$expected;GameProcessActions=$false;GameFilesChanged=$false}
        [IO.Directory]::CreateDirectory([IO.Path]::GetDirectoryName([IO.Path]::GetFullPath($ProofPath))) | Out-Null
        $proof | ConvertTo-Json -Depth 6 | Set-Content -LiteralPath $ProofPath -Encoding utf8
        $proof | ConvertTo-Json -Depth 6
    } finally {$written.Dispose()}
} finally {$module.Dispose()}
