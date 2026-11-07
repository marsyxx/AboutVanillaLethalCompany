$Root = 'D:\Programs\ThunderStore\Data\LethalCompany\profiles\123\BepInEx'
$Out  = 'C:\Users\ayrae\AppData\Local\Temp\claude\C--Users-ayrae-AppData-Roaming-Claude-scratch-workspaces-664e686c-aad7-4b5c-9361-07f5b5adc266-4765498e-5d89-4ddd-a201-52e581c9499c-scratch-2026-11-03-20927f\9a1aeef8-d419-411e-978a-970c129e6447\scratchpad'
Add-Type -Path "$Root\core\Mono.Cecil.dll"
$g = 'D:\Programs\steamapps\common\Lethal Company\Lethal Company_Data\Managed\Assembly-CSharp.dll'
$gm = [Mono.Cecil.AssemblyDefinition]::ReadAssembly($g).MainModule

# index the game assembly
$types = @{}
foreach ($t in $gm.GetTypes()) {
  $ms = New-Object 'System.Collections.Generic.HashSet[string]'
  foreach ($m in $t.Methods) {
    $ps = ($m.Parameters | ForEach-Object { $_.ParameterType.FullName }) -join ','
    [void]$ms.Add($m.Name + '(' + $ps + ')')
  }
  $fs = New-Object 'System.Collections.Generic.HashSet[string]'
  foreach ($f in $t.Fields) { [void]$fs.Add($f.Name) }
  $types[$t.FullName] = @{ M=$ms; F=$fs; Base=$(if ($t.BaseType) { $t.BaseType.FullName } else { $null }); Scope=$(if ($t.BaseType) { $t.BaseType.Scope.Name } else { $null }) }
}
function Has-Member($typeName, $kind, $key) {
  $cur = $typeName; $guard = 0
  while ($cur -and $guard -lt 20) {
    $guard++
    if (-not $types.ContainsKey($cur)) { return ($guard -eq 1) }   # left Assembly-CSharp while walking bases: not found there
    $e = $types[$cur]
    if ($kind -eq 'M' -and $e.M.Contains($key)) { return $true }
    if ($kind -eq 'F' -and $e.F.Contains($key)) { return $true }
    $cur = $e.Base
  }
  return $false
}

$skip = '^(MMHOOK|0Harmony|Mono\.|System\.|Unity|Newtonsoft|Microsoft|BepInEx\.|HarmonyXInterop|Assembly-CSharp|Facepunch|Sirenix|OdinSerializer|com\.unity|Dissonance|Netcode)'
$dlls = Get-ChildItem "$Root\plugins","$Root\patchers" -Recurse -Filter *.dll | Where-Object { $_.Name -notmatch $skip }
$res = New-Object System.Collections.Generic.List[object]
foreach ($f in $dlls) {
  try { $mod = [Mono.Cecil.AssemblyDefinition]::ReadAssembly($f.FullName).MainModule } catch { continue }
  $owner = ($f.FullName.Substring($Root.Length+1) -split '\\')[1]
  $seen = New-Object 'System.Collections.Generic.HashSet[string]'
  $refsGame = $false
  foreach ($r in $mod.AssemblyReferences) { if ($r.Name -eq 'Assembly-CSharp') { $refsGame = $true } }
  if (-not $refsGame) { continue }
  foreach ($t in $mod.GetTypes()) {
    foreach ($m in $t.Methods) {
      if (-not $m.HasBody) { continue }
      foreach ($ins in $m.Body.Instructions) {
        $op = $ins.Operand
        if ($op -is [Mono.Cecil.GenericInstanceMethod]) { $op = $op.ElementMethod }
        if ($op -is [Mono.Cecil.MethodReference]) {
          $dt = $op.DeclaringType
          if ($dt.IsGenericInstance -or $dt.Scope.Name -ne 'Assembly-CSharp') { continue }
          $key = $op.Name + '(' + (($op.Parameters | ForEach-Object { $_.ParameterType.FullName }) -join ',') + ')'
          if (-not $types.ContainsKey($dt.FullName)) { $tag = "TYPE|" + $dt.FullName }
          elseif (-not (Has-Member $dt.FullName 'M' $key)) { $tag = "METHOD|" + $dt.FullName + "::" + $key } else { continue }
          if ($seen.Add($tag)) { $res.Add([pscustomobject]@{Owner=$owner;Dll=$f.Name;Kind=($tag.Split('|')[0]);Missing=($tag.Split('|',2)[1]);In=("{0}::{1}" -f $t.Name,$m.Name)}) }
        }
        elseif ($op -is [Mono.Cecil.FieldReference]) {
          $dt = $op.DeclaringType
          if ($dt.IsGenericInstance -or $dt.Scope.Name -ne 'Assembly-CSharp') { continue }
          if (-not $types.ContainsKey($dt.FullName)) { $tag = "TYPE|" + $dt.FullName }
          elseif (-not (Has-Member $dt.FullName 'F' $op.Name)) { $tag = "FIELD|" + $dt.FullName + "::" + $op.Name } else { continue }
          if ($seen.Add($tag)) { $res.Add([pscustomobject]@{Owner=$owner;Dll=$f.Name;Kind=($tag.Split('|')[0]);Missing=($tag.Split('|',2)[1]);In=("{0}::{1}" -f $t.Name,$m.Name)}) }
        }
      }
    }
  }
}
$res | Export-Csv "$Out\compat.csv" -NoTypeInformation -Encoding UTF8
"missing refs: " + $res.Count + " across " + (($res | Select-Object -ExpandProperty Owner -Unique) | Measure-Object).Count + " mods"
