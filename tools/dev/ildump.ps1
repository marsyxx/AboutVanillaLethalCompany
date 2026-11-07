param([string]$Type, [string]$Methods, [string]$Asm = 'Assembly-CSharp.dll')
$Root = 'D:\Programs\ThunderStore\Data\LethalCompany\profiles\123\BepInEx'
Add-Type -Path "$Root\core\Mono.Cecil.dll"
$man = 'D:\Programs\steamapps\common\Lethal Company\Lethal Company_Data\Managed'
$p = if (Test-Path $Asm) { $Asm } else { "$man\$Asm" }
$m = [Mono.Cecil.AssemblyDefinition]::ReadAssembly($p).MainModule
$t = $m.GetTypes() | Where-Object { $_.Name -eq $Type -or $_.FullName -eq $Type } | Select-Object -First 1
foreach ($name in $Methods.Split(',')) {
  foreach ($meth in ($t.Methods | Where-Object { $_.Name -eq $name })) {
    "---- $Type::$name($(($meth.Parameters | ForEach-Object { $_.ParameterType.Name }) -join ','))"
    foreach ($i in $meth.Body.Instructions) {
      $op = $i.Operand
      $s = if ($op -is [Mono.Cecil.MethodReference]) { $op.DeclaringType.Name + '::' + $op.Name } elseif ($op -is [Mono.Cecil.FieldReference]) { $op.DeclaringType.Name + '.' + $op.Name } elseif ($op -is [Mono.Cecil.Cil.Instruction]) { 'IL_' + $op.Offset.ToString('x4') } elseif ($null -ne $op) { "$op" } else { '' }
      "{0:x4} {1} {2}" -f $i.Offset, $i.OpCode.Name, $s
    }
  }
}
