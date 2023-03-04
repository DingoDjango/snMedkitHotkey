using SMLHelper.V2.Json;
using SMLHelper.V2.Options.Attributes;
using UnityEngine;

namespace MedkitHotkey_BZ
{
    [Menu("Medkit Hotkey BZ")]
    public class SMLConfig : ConfigFile
    {
        [Keybind(Id = "FirstAidKey", Label = "First Aid Kit Hotkey", Tooltip = "Keybinding used to activate a First Aid Kit from inventory, if one is available.")]
        public KeyCode ConfigFirstAidKey = KeyCode.H;
    }
}
