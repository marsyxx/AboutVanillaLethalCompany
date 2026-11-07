param([string]$Dll, [string]$Needle)
$Root = 'D:\Programs\ThunderStore\Data\LethalCompany\profiles\123\BepInEx'
Add-Type -Path "$Root\core\Mono.Cecil.dll"
$m = [Mono.Cecil.ModuleDefinition]::ReadModule($Dll)
$res = New-Object System.Collections.Generic.HashSet[string]
foreach ($t in $m.GetTypes()) { foreach ($meth in $t.Methods) {
  if (-not $meth.HasBody) { continue }
  foreach ($i in $meth.Body.Instructions) {
    $mr = $i.Operand -as [Mono.Cecil.MemberReference]
    if ($mr -and ($mr.Name -match $Needle)) { [void]$res.Add(("{0}::{1} -> {2}::{3}" -f $t.Name, $meth.Name, $mr.DeclaringType.Name, $mr.Name)) }
  } } }
$res | Sort-Object
