param([string]$Dll, [string]$TypeFilter, [switch]$All)
$Root = 'D:\Programs\ThunderStore\Data\LethalCompany\profiles\123\BepInEx'
Add-Type -Path "$Root\core\Mono.Cecil.dll"
$m = [Mono.Cecil.ModuleDefinition]::ReadModule($Dll)
foreach ($t in $m.GetTypes()) {
  if (-not ($t.FullName -match $TypeFilter)) { continue }
  if (-not $All -and -not ($t.IsPublic -or $t.IsNestedPublic)) { continue }
  $kind = if ($t.IsEnum) { 'enum' } elseif ($t.IsInterface) { 'interface' } elseif ($t.IsValueType) { 'struct' } else { 'class' }
  "{0} {1} : {2}" -f $kind, $t.FullName, $(if ($t.BaseType) { $t.BaseType.Name } else { '' })
  if ($t.IsEnum) { "    " + (($t.Fields | Where-Object { $_.Name -ne 'value__' } | ForEach-Object { $_.Name + '=' + $_.Constant }) -join ', '); continue }
  foreach ($c in $t.Methods) { if ($c.IsPublic -and ($c.IsConstructor -or -not $c.IsSpecialName)) { "    {0}{1}({2}) -> {3}" -f $(if ($c.IsStatic) { 'static ' } else { '' }), $c.Name, (($c.Parameters | ForEach-Object { $_.ParameterType.Name + ' ' + $_.Name }) -join ', '), $c.ReturnType.Name } }
  foreach ($p in $t.Properties) { if ($p.GetMethod -and $p.GetMethod.IsPublic -or ($p.SetMethod -and $p.SetMethod.IsPublic)) { "    prop {0} {1}{2}" -f $p.PropertyType.Name, $p.Name, $(if ($p.SetMethod -and $p.SetMethod.IsPublic) { ' {get;set;}' } else { ' {get;}' }) } }
  foreach ($f in $t.Fields) { if ($f.IsPublic) { "    field {0} {1}" -f $f.FieldType.Name, $f.Name } }
}
