param([string]$Dll, [string]$Needle)
$Root = 'D:\Programs\ThunderStore\Data\LethalCompany\profiles\123\BepInEx'
Add-Type -Path "$Root\core\Mono.Cecil.dll"
$m = [Mono.Cecil.ModuleDefinition]::ReadModule($Dll)
foreach ($t in $m.GetTypes()) { foreach ($meth in $t.Methods) {
  if (-not $meth.HasBody) { continue }
  foreach ($i in $meth.Body.Instructions) {
    if ($i.OpCode.Name -eq 'ldstr' -and ([string]$i.Operand) -like "*$Needle*") { "{0}::{1}  [{2}]" -f $t.FullName, $meth.Name, $i.Operand }
  } } }
