param([string]$Dll, [string]$TypeFilter, [string]$MemberFilter = '.')
$Root = 'D:\Programs\ThunderStore\Data\LethalCompany\profiles\123\BepInEx'
Add-Type -Path "$Root\core\Mono.Cecil.dll"
$m = [Mono.Cecil.ModuleDefinition]::ReadModule($Dll)
foreach ($t in $m.GetTypes()) {
  if (-not ($t.FullName -match $TypeFilter)) { continue }
  "type {0} : {1}" -f $t.FullName, $(if ($t.BaseType) { $t.BaseType.Name } else { '' })
  foreach ($f in $t.Fields) { if ($f.Name -match $MemberFilter) { "    {0}field {1} {2}" -f $(if ($f.IsStatic) { 'static ' } else { '' }), $f.FieldType.Name, $f.Name } }
  foreach ($c in $t.Methods) { if ($c.Name -match $MemberFilter) { "    {0}{1}({2}) -> {3}" -f $(if ($c.IsStatic) { 'static ' } else { '' }), $c.Name, (($c.Parameters | ForEach-Object { $_.ParameterType.Name + ' ' + $_.Name }) -join ', '), $c.ReturnType.Name } }
}
