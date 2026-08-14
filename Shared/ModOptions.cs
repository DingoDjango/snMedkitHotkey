using Nautilus.Extensions;
using Nautilus.Json;
using Nautilus.Options;
using Nautilus.Options.Attributes;
using UnityEngine;

namespace MedkitHotkey
{
    [Menu(ModPlugin.modName)]
    public class ModOptions : ConfigFile
    {
        [Keybind(null, LabelLanguageId = "FirstAidKey", TooltipLanguageId = "Tooltip_FirstAidKey")]
        public KeyCode FirstAidKey = KeyCode.H;
    }
}
