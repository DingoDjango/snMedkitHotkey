## **Medkit Hotkey by Dingo**
#### **Description:**
I was a bit miffed when I found out you can't bind the First Aid Kit to a hotkey. This simple mod aims to fix that inconvenience.

The hotkey (default: "H") is configured through a json file, since Subnautica's key bindings system is rather modder unfriendly.

#### **Installation:**
1) Install [QMods](https://www.nexusmods.com/subnautica/mods/201)﻿ if you haven't already.  
2) Download the zip file from the [Files tab](https://www.nexusmods.com/subnautica/mods/190?tab=files).  
3) Unzip the contents of the zip to the game's main directory (where Subnautica.exe can be found).

#### **(Optional) Configuration:**
1) Navigate to the mod's directory (*Subnautica\QMods\MedkitHotkey*).  
2) Edit settings.json with Notepad or your favourite text editor.  
3) Replace the default "H" with your preferred key. **Use only KeyCode names found on [this page](https://docs.unity3d.com/ScriptReference/KeyCode.html).**

#### **(Optional) Translation:**
If you want to contribute a translation for this mod, please follow these steps:  
1) Look at the file *"English.json"* in *QMods\MedkitHotkey\Languages*  
2) Copy that file and change the file name to your language. It needs to match the file name in *Subnautica\SNUnmanagedData\LanguageFiles*  
3) Translate the file. Do not touch the keys ("MissingMedkit"), only the translated values ("No first aid...")  
4) Share the file with me, preferrably over GitHub, a Nexus private message or (if you must) a comment

#### **FAQ:**

* **Q. Is this mod safe to add or remove from an existing save file?**
* A. Perfectly safe.
* **Q. Does this mod have any known conflicts?**
* A. It should not conflict with other mods. Obviously, the hotkey you define should be unique to avoid trouble.

[Source code can be found here.](https://github.com/DingoDjango/snMedkitHotkey)﻿

#### **Credits:**
- Powered by [Harmony](https://github.com/pardeike/Harmony)  
- Made for the [QMods Subnautica Mod System](https://www.nexusmods.com/subnautica/mods/201)﻿  
- Russian translation by [mstislavovich](https://forums.nexusmods.com/index.php?/user/23416669-mstislavovich/)  


2019-03-17 - v1.2.2  
- Added Russian translation by mstislavovich  

2019-03-16 - v1.2.1  
- Small refactor to language loading  

2019-03-16 - v1.2.0  
- Added a framework for translations  
- Refactored code  

2019-02-15 - v1.1.2  
- Added a check for text input (console, signs, lockers etc.) based on [player feedback](https://www.nexusmods.com/subnautica/mods/190?tab=bugs)﻿  

2019-02-15 - v1.1.0  
- Updated for QMods v2.0.0  
- Switched directly to Unity KeyCode  

2019-02-03 - v1.0.0  
- Initial mod release
