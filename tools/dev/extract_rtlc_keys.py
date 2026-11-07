#!/usr/bin/env python
"""Writes LanguageSwitch/translations/rtlc_keys_en.json: the English strings the game shows, taken from an RTLC
dictionary folder (BepInEx/config/Translation/Game). Only the English keys are copied, not RTLC's translations.

Usage: python tools/dev/extract_rtlc_keys.py "<profile>/BepInEx/config/Translation/Game"
"""
import io
import json
import os
import sys

src = sys.argv[1]
out = {}
for fn in sorted(os.listdir(src)):
    if not fn.endswith('.txt'):
        continue
    keys = []
    for line in io.open(os.path.join(src, fn), encoding='utf-8-sig').read().splitlines():
        s = line.rstrip('\r')
        if not s.strip() or s.lstrip().startswith(('//', '#')) or '=' not in s:
            continue
        keys.append(s.split('=', 1)[0])
    out[fn] = keys
target = os.path.join(os.path.dirname(os.path.abspath(__file__)), '..', '..', 'LanguageSwitch', 'translations', 'rtlc_keys_en.json')
with io.open(target, 'w', encoding='utf-8', newline='\n') as f:
    json.dump(out, f, ensure_ascii=False, indent=0)
print('wrote', os.path.normpath(target), sum(len(v) for v in out.values()), 'keys')
