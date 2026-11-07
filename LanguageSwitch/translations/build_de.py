#!/usr/bin/env python
"""Builds the German XUnity dictionary shipped with LanguageSwitch.

Inputs
  de_source.txt        hand-written translations, one `<exact English game text>=<German>` per line
  rtlc_keys_en.json    the list of English strings the game really shows (file name -> [keys]); regenerate it from the
                       RTLC dictionary with extract_keys.py. Only the English keys are stored, no one else's translations.
Output
  ../package/config/Translation_de/Game/<File>.txt   (installed to BepInEx/config/Translation_de/Game/)

Every translated key is checked against the key list, so a typo in an English key is reported instead of silently
never matching. Item names and prompts are generated from a noun table plus a few sentence patterns.
"""
import io
import json
import os
import re
import sys

HERE = os.path.dirname(os.path.abspath(__file__))
OUT_DIR = os.path.join(HERE, '..', 'package', 'config', 'Translation_de', 'Game')

NOUNS = {
    'body': 'Leiche', 'spray paint': 'Sprühdose', 'jetpack': 'Jetpack', 'shovel': 'Schaufel', 'boombox': 'Boombox',
    'lockpicker': 'Dietrich', 'walkie-talkie': 'Walkie-Talkie', 'tzp-inhalant': 'TZP-Inhalator',
    'pro-flashlight': 'Profi-Taschenlampe', 'radar-booster': 'Radar-Booster', 'stun grenade': 'Betäubungsgranate',
    'extension ladder': 'Ausziehleiter', 'zap gun': 'Zap-Pistole', 'flashlight': 'Taschenlampe', 'clipboard': 'Klemmbrett',
    'sticky note': 'Haftnotiz', 'big bolt': 'Große Schraube', 'key': 'Schlüssel', 'jar of pickles': 'Gurkenglas',
    'laser pointer': 'Laserpointer', 'magnifying glass': 'Lupe', 'candy': 'Süßigkeit', 'brass bell': 'Messingglocke',
    'bell': 'Glocke', 'fancy lamp': 'Schicke Lampe', 'large axle': 'Große Achse', 'v-type engine': 'V-Motor',
    'clown horn': 'Clownshupe', 'toothpaste': 'Zahnpasta', 'red soda': 'Rote Limo', 'robot toy': 'Spielzeugroboter',
    'toy robot': 'Spielzeugroboter', 'plastic fish': 'Plastikfisch', 'metal sheet': 'Blech',
    'tattered metal sheet': 'Zerfetztes Blech', 'tea kettle': 'Teekessel', 'teeth': 'Gebiss', 'airhorn': 'Signalhorn',
    'stop sign': 'Stoppschild', 'apparatice': 'Apparat', 'apparatus': 'Apparat', 'bee hive': 'Bienenstock',
    'hive': 'Bienenstock', 'mug': 'Becher', 'bottles': 'Flaschen', 'gold bar': 'Goldbarren', 'painting': 'Gemälde',
    'data chip': 'Datenchip', 'rubber ducky': 'Quietscheente', 'cash register': 'Registrierkasse',
    'chemical jug': 'Chemiekanister', 'wedding ring': 'Ehering', 'ring': 'Ring', 'toy cube': 'Spielwürfel',
    'yield sign': 'Vorfahrt-Schild', 'steering wheel': 'Lenkrad', 'remote': 'Fernbedienung', 'hair brush': 'Haarbürste',
    'brush': 'Bürste', 'coffee mug': 'Kaffeetasse', 'cookie mold pan': 'Keksform', 'dust pan': 'Kehrschaufel',
    'egg beater': 'Schneebesen', 'golden cup': 'Goldener Pokal', 'old phone': 'Altes Telefon',
    'perfume bottle': 'Parfümflasche', 'pill bottle': 'Pillendose', 'gift box': 'Geschenkbox', 'gift': 'Geschenk',
    'hairdryer': 'Föhn', 'comedy': 'Komödie', 'tragedy': 'Tragödie', 'shotgun': 'Schrotflinte',
    'double-barrel': 'Doppelflinte', 'binoculars': 'Fernglas', 'mapper': 'Kartograf',
    'homemade flashbang': 'Selbstgebaute Blendgranate', 'whoopie cushion': 'Furzkissen', 'shotgun shell': 'Schrotpatrone',
    'ammo': 'Munition', 'flask': 'Flachmann', 'magic 7 ball': 'Magische 7-Kugel', 'bubblegum': 'Kaugummi',
    'sneakers': 'Turnschuhe', 'item': 'Gegenstand', 'easter egg': 'Osterei', 'kitchen knife': 'Küchenmesser',
    'weed killer': 'Unkrautvernichter', 'unnamed': 'Unbenannt',
}

VERBS = {
    'Spray : [LMB]': 'Sprühen : [LMB]', 'Shake can : [Q]': 'Dose schütteln : [Q]',
    'Use jetpack : [LMB]': 'Jetpack benutzen : [LMB]', 'Swing shovel : [LMB]': 'Schaufel schwingen : [LMB]',
    'Toggle music : [LMB]': 'Musik umschalten : [LMB]', 'Place on door : [LMB]': 'An Tür anbringen : [LMB]',
    'Power button : [Q]': 'Ein/Aus : [Q]', 'Send voice : [LMB]': 'Sprechen : [LMB]', 'Take TZP : [LMB]': 'TZP einnehmen : [LMB]',
    'Toggle light : [LMB]': 'Licht umschalten : [LMB]', 'Turn on booster : [LMB]': 'Booster einschalten : [LMB]',
    'Use grenade : [LMB]': 'Granate benutzen : [LMB]', 'Throw grenade: [LMB]': 'Granate werfen: [LMB]',
    'Pop up ladder : [LMB]': 'Leiter ausfahren : [LMB]', 'Scan for threat: [LMB]': 'Nach Bedrohung scannen: [LMB]',
    'Turn page : [Q/E]': 'Umblättern : [Q/E]', 'Inspect: [Z]': 'Untersuchen: [Z]', 'Use key : [LMB]': 'Schlüssel benutzen : [LMB]',
    'Toggle laser : [LMB]': 'Laser umschalten : [LMB]', 'Use horn : [LMB]': 'Hupe benutzen : [LMB]',
    'Use airhorn : [LMB]': 'Signalhorn benutzen : [LMB]', 'Swing sign : [LMB]': 'Schild schwingen : [LMB]',
    'Use cash register : [LMB]': 'Kasse benutzen : [LMB]', 'Use remote : [LMB]': 'Fernbedienung benutzen : [LMB]',
    'Open gift : [LMB]': 'Geschenk öffnen : [LMB]', 'Use hairdryer : [LMB]': 'Föhn benutzen : [LMB]',
    'Wear mask : [LMB]': 'Maske aufsetzen : [LMB]', 'Reload : [E]': 'Nachladen : [E]',
    'Turn safety on: [Q]': 'Sicherung einschalten: [Q]', 'Turn safety off: [Q]': 'Sicherung ausschalten: [Q]',
    'Use binoculars : [LMB]': 'Fernglas benutzen : [LMB]', 'Update map : [LMB]': 'Karte aktualisieren : [LMB]',
    'Inspect map : [Z]': 'Karte ansehen : [Z]', 'Use DIY-Flashbang : [LMB]': 'Selbstbau-Blendgranate benutzen : [LMB]',
    'Toss egg: [LMB]': 'Ei werfen: [LMB]', 'Stab : [LMB]': 'Zustechen : [LMB]', '(Radar booster)': '(Radar-Booster)',
    'Tattered metal sheet!': 'Zerfetztes Blech eingesammelt!',
}


def noun(text):
    return NOUNS.get(text.strip().lower())


def item_translation(key):
    """German text for an item-related key, or None when no rule applies."""
    if key in VERBS:
        return VERBS[key]
    trailing = key[len(key.rstrip()):]
    core = key.strip()
    german = noun(core)
    if german:
        return german + trailing
    m = re.match(r'^Drop (.+?) ?: \[G\]$', core)
    if m and noun(m.group(1)):
        return '%s ablegen : [G]' % noun(m.group(1))
    m = re.match(r'^(.+?) collected!$', core)
    if m and noun(m.group(1)):
        return '%s eingesammelt!' % noun(m.group(1))
    return None


def main():
    keys = json.load(io.open(os.path.join(HERE, 'rtlc_keys_en.json'), encoding='utf-8'))
    where = {}                      # key -> file it belongs to (first file wins)
    for fn, ks in keys.items():
        for k in ks:
            where.setdefault(k, fn)

    result = {fn: {} for fn in keys}
    problems = []

    for line in io.open(os.path.join(HERE, 'de_source.txt'), encoding='utf-8').read().split('\n'):
        line = line.rstrip('\r')
        if not line.strip() or line.startswith('#'):
            continue
        key, value = line.split('=', 1)
        if key not in where:
            problems.append('key not in the game text list: ' + key)
            continue
        result[where[key]][key] = value

    untranslated_items = []
    for key in keys.get('Items.txt', []):
        if key in where and where[key] != 'Items.txt':
            continue
        german = item_translation(key)
        if german is None:
            if not (key.startswith('r:') or key.startswith('sr:')):
                untranslated_items.append(key)
            continue
        result['Items.txt'][key] = german

    os.makedirs(OUT_DIR, exist_ok=True)
    total = 0
    for fn, entries in result.items():
        if not entries:
            continue
        with io.open(os.path.join(OUT_DIR, fn), 'w', encoding='utf-8', newline='\n') as f:
            f.write('// German translation of Lethal Company text (LanguageSwitch). Format: original=translation\n')
            for key, value in entries.items():
                f.write('%s=%s\n' % (key, value))
        total += len(entries)
        print('%-16s %4d entries' % (fn, len(entries)))
    print('total', total)
    if untranslated_items:
        print('items without a rule (left in English): %d' % len(untranslated_items))
    for p in problems:
        print('PROBLEM:', p)
    return 1 if problems else 0


if __name__ == '__main__':
    sys.exit(main())
