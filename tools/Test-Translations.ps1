<#
.SYNOPSIS
  Lints XUnity.AutoTranslator translation files the way XUnity itself reads them.

.DESCRIPTION
  Mirrors XUnity's TextHelper.ReadTranslationLineAndDecode and RegexTranslation, then reports:
    ERROR  regex that does not compile (XUnity drops it with a warning)
    ERROR  line XUnity silently ignores: no '=', a second unescaped '=', '//' inside the key
    ERROR  '//' inside the value (XUnity cuts the value there)
    ERROR  replacement that references a group the regex does not have
    ERROR  "$${N}" in a replacement (prints a literal "${N}"; a dollar plus group N is "$$${N}")
    WARN   regex without ^...$ anchors (XUnity replaces the WHOLE text with the replacement, so an
           unanchored regex turns any longer text that contains the match into this one line)
    WARN   the same key with different values in one language (says which one XUnity keeps)

  Usage: powershell -File tools\Test-Translations.ps1 [-Root BepInEx\config]
  Exit code 1 when there is at least one ERROR.
#>
param([string]$Root = (Join-Path $PSScriptRoot '..\BepInEx\config'))

$ErrorActionPreference = 'Stop'
$script:errors = 0
$script:warnings = 0

function Write-Finding($level, $file, $line, $msg) {
    if ($level -eq 'ERROR') { $script:errors++ } else { $script:warnings++ }
    Write-Output ("{0,-5} {1}:{2}: {3}" -f $level, $file, $line, $msg)
}

# Port of XUnity TextHelper.ReadTranslationLineAndDecode. Returns @{ Key; Value; Problem }.
function Read-TranslationLine([string]$s) {
    $parts = New-Object string[] 2
    $num = 0; $esc = $false
    $sb = New-Object System.Text.StringBuilder
    for ($i = 0; $i -lt $s.Length; $i++) {
        $c = $s[$i]
        if ($esc) {
            switch -CaseSensitive ($c) {
                '=' { [void]$sb.Append($c) }
                '\' { [void]$sb.Append($c) }
                'n' { [void]$sb.Append("`n") }
                'r' { [void]$sb.Append("`r") }
                'u' {
                    if ($i + 4 -lt $s.Length) {
                        [void]$sb.Append([char][Convert]::ToInt32($s.Substring($i + 1, 4), 16)); $i += 4
                    } else { return @{ Problem = 'invalid \u escape' } }
                }
                default { [void]$sb.Append('\').Append($c) }
            }
            $esc = $false; continue
        }
        if ($c -eq '\') { $esc = $true; continue }
        if ($c -eq '=') {
            if ($num -gt 1) { return @{ Problem = 'more than one unescaped "=" (escape it as \=); XUnity ignores the line' } }
            $parts[$num++] = $sb.ToString(); [void]$sb.Clear(); continue
        }
        if ($c -eq '%' -and $i + 2 -lt $s.Length -and $s[$i + 1] -eq '3' -and $s[$i + 2] -eq 'D') {
            [void]$sb.Append('='); $i += 2; continue
        }
        if ($c -eq '/' -and $i + 1 -lt $s.Length -and $s[$i + 1] -eq '/') {
            if ($num -gt 1) { return @{ Problem = 'more than one unescaped "=" (escape it as \=); XUnity ignores the line' } }
            $parts[$num++] = $sb.ToString()
            if ($num -eq 2) { return @{ Key = $parts[0]; Value = $parts[1]; Problem = 'TRUNCATED' } }
            return @{ Problem = 'COMMENT' }
        }
        [void]$sb.Append($c)
    }
    if ($num -eq 0) { return @{ Problem = 'NOEQ' } }
    if ($num -ne 1) { return @{ Problem = 'more than one unescaped "=" (escape it as \=); XUnity ignores the line' } }
    $parts[1] = $sb.ToString()
    return @{ Key = $parts[0]; Value = $parts[1] }
}

# Port of the quote stripping in XUnity RegexTranslation.
function Get-Unquoted([string]$s) {
    if ($s.StartsWith('r:') -or $s.StartsWith('sr:')) { $s = $s.Substring($s.IndexOf(':') + 1) }
    $a = $s.IndexOf('"')
    if ($a -ne -1) {
        $b = $s.LastIndexOf('"')
        if ($b -eq $a) { throw 'starts with " but does not end with "' }
        $s = $s.Substring($a + 1, $b - $a - 1)
    }
    return $s
}

# Checks a .NET Regex.Replace / Match.Result replacement pattern against the regex's groups.
function Test-Replacement([regex]$rx, [string]$rep) {
    $problems = @()
    $nums = $rx.GetGroupNumbers()
    $names = $rx.GetGroupNames()
    $i = 0
    while ($i -lt $rep.Length) {
        if ($rep[$i] -ne '$' -or $i + 1 -ge $rep.Length) { $i++; continue }
        $n = $rep[$i + 1]
        if ($n -eq '$') {
            if ($i + 2 -lt $rep.Length -and $rep[$i + 2] -eq '{' -and $rep.Substring($i + 2) -match '^\{(\w+)\}') {
                $problems += ('"$${0}" prints a literal "${0}"; use "$$${0}" for a dollar sign followed by the group' -f ('{' + $Matches[1] + '}'))
            }
            $i += 2; continue
        }
        if ($n -eq '{') {
            $close = $rep.IndexOf('}', $i)
            if ($close -gt 0) {
                $name = $rep.Substring($i + 2, $close - $i - 2)
                if ($names -notcontains $name) { $problems += ('"${{{0}}}" refers to a group the regex does not have' -f $name) }
                $i = $close + 1; continue
            }
            $i++; continue
        }
        if ([char]::IsDigit($n)) {
            $j = $i + 1; $digits = ''
            while ($j -lt $rep.Length -and [char]::IsDigit($rep[$j])) { $digits += $rep[$j]; $j++ }
            # .NET takes the longest prefix of the digits that is a valid group number
            $ok = $false
            for ($k = $digits.Length; $k -ge 1; $k--) { if ($nums -contains [int]$digits.Substring(0, $k)) { $ok = $true; break } }
            if (-not $ok) { $problems += ('"${0}" refers to a group the regex does not have (printed literally)' -f $digits) }
            $i = $j; continue
        }
        $i++
    }
    return $problems
}

function Get-DistinctCount($list) {
    $set = New-Object 'System.Collections.Generic.HashSet[string]' ([StringComparer]::Ordinal)
    foreach ($e in $list) { [void]$set.Add($e.Value) }
    return $set.Count
}

foreach ($lang in 'Translation', 'Translation_de') {
    $dir = Join-Path $Root $lang
    if (-not (Test-Path $dir)) { continue }
    $dir = (Resolve-Path $dir).Path
    $outputFile = if ($lang -eq 'Translation') { 'Lang.txt' } else { '_AutoGeneratedTranslations.txt' }
    $special = '_Substitutions.txt', '_Preprocessors.txt', '_Postprocessors.txt'

    # XUnity load order: output file first, then every other .txt by full path, descending.
    $files = Get-ChildItem $dir -Recurse -File -Filter *.txt |
        Where-Object { $special -notcontains $_.Name -and $_.Name -notlike '*resizer.txt' } |
        Sort-Object @{ Expression = { $_.Name -ne $outputFile -or $_.DirectoryName -ne $dir } }, @{ Expression = { $_.FullName.ToLowerInvariant() }; Descending = $true }

    # XUnity's dictionaries are case-sensitive, so these must be too.
    $plain = New-Object System.Collections.Hashtable ([StringComparer]::Ordinal)   # key -> list of @{Value; Where}
    $regexes = New-Object System.Collections.Hashtable ([StringComparer]::Ordinal) # pattern -> list of @{Value; Where}
    foreach ($f in $files) {
        $rel = $f.FullName.Substring($dir.Length + 1)
        $show = "$lang\$rel"
        $isOutput = ($f.Name -eq $outputFile -and $f.DirectoryName -eq $dir)
        $lines = [IO.File]::ReadAllText($f.FullName, [Text.Encoding]::UTF8) -split "`r`n|`n|`r"
        for ($ln = 0; $ln -lt $lines.Count; $ln++) {
            $line = $lines[$ln].TrimStart([char]0xFEFF)
            $no = $ln + 1
            $trimmed = $line.Trim()
            if ($trimmed.Length -eq 0) { continue }
            if (-not $isOutput -and $trimmed -match '^#(set|unset|enable)\s+\S') { continue }
            $r = Read-TranslationLine $line
            $problem = $r.Problem
            if ($problem -eq 'COMMENT') {
                if (-not $trimmed.StartsWith('//')) {
                    Write-Finding ERROR $show $no 'the key contains "//", so XUnity reads the line as a comment and ignores it (write each / as \u002F)'
                }
                continue
            }
            if ($problem -eq 'NOEQ') {
                # "# Header" and "// note" lines are comments by convention; XUnity skips them silently.
                if (-not ($trimmed.StartsWith('#') -or $trimmed.StartsWith('//'))) {
                    Write-Finding WARN $show $no 'no "=" in the line (a broken multi-line entry?); XUnity ignores it'
                }
                continue
            }
            if ($problem -eq 'TRUNCATED') {
                Write-Finding ERROR $show $no 'the value contains "//": XUnity cuts the value there (write each / as \u002F)'
            } elseif ($problem) {
                Write-Finding ERROR $show $no $problem
                continue
            }
            $key = $r.Key; $value = $r.Value
            if ([string]::IsNullOrEmpty($key) -or [string]::IsNullOrEmpty($value)) {
                Write-Finding ERROR $show $no 'empty key or value; XUnity ignores the line'
                continue
            }
            $where = "${show}:$no"
            if ($key.StartsWith('r:') -or $key.StartsWith('sr:')) {
                try {
                    $pattern = Get-Unquoted $key
                    $rep = Get-Unquoted $value
                    $rx = [regex]::new($pattern)
                } catch {
                    Write-Finding ERROR $show $no ("regex does not compile: " + $_.Exception.InnerException.Message + $_.Exception.Message)
                    continue
                }
                if ($key.StartsWith('r:')) {
                    foreach ($p in (Test-Replacement $rx $rep)) { Write-Finding ERROR $show $no $p }
                    $body = $pattern -replace '^\(\?[imnsx-]+\)', ''
                    if (-not ($body.StartsWith('^') -or $body.StartsWith('\A')) -or -not ($body.EndsWith('$') -or $body.EndsWith('\z'))) {
                        Write-Finding WARN $show $no 'regex is not anchored with ^...$: the whole text is replaced by the replacement, so any longer text containing this match collapses into it'
                    }
                }
                if (-not $regexes.ContainsKey($pattern)) { $regexes[$pattern] = New-Object System.Collections.ArrayList }
                [void]$regexes[$pattern].Add(@{ Value = $rep; Where = $where })
            } else {
                if (-not $plain.ContainsKey($key)) { $plain[$key] = New-Object System.Collections.ArrayList }
                [void]$plain[$key].Add(@{ Value = $value; Where = $where })
            }
        }
    }

    foreach ($k in $plain.Keys) {
        $list = $plain[$k]
        if ((Get-DistinctCount $list) -gt 1) {
            $kept = $list[$list.Count - 1]   # plain keys: the last one loaded wins
            $all = ($list | ForEach-Object { "$($_.Where) => '$($_.Value)'" }) -join '; '
            Write-Finding WARN $lang '-' ("key '{0}' has different values: {1}. XUnity keeps {2}" -f $k, $all, $kept.Where)
        }
    }
    foreach ($k in $regexes.Keys) {
        $list = $regexes[$k]
        if ((Get-DistinctCount $list) -gt 1) {
            $kept = $list[0]                 # regexes: the first one loaded wins
            $all = ($list | ForEach-Object { "$($_.Where) => '$($_.Value)'" }) -join '; '
            Write-Finding WARN $lang '-' ("regex '{0}' has different values: {1}. XUnity keeps {2}" -f $k, $all, $kept.Where)
        }
    }
}

Write-Output ("{0} error(s), {1} warning(s)" -f $script:errors, $script:warnings)
if ($script:errors -gt 0) { exit 1 }
