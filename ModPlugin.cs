using BepInEx;
using BepInEx.Configuration;
using UnityEngine;

namespace MedkitHotkey
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class ModPlugin : BaseUnityPlugin
    {
        private const string modGUID = "Dingo.SN.MedkitHotkey";
        internal const string modName = "Medkit Hotkey";
        private const string modVersion = "2.0.0";

        public static ConfigEntry<KeyboardShortcut> ConfigFirstAidKey;

        private void InitializeConfig()
        {
            ConfigFirstAidKey = Config.Bind(
                section: "General",
                key: "First Aid Kit Hotkey",
                defaultValue: new KeyboardShortcut(KeyCode.H),
                description: "Keybinding used to activate a First Aid Kit from inventory, if one is available.");
        }

        public void Start()
        {
            InitializeConfig();

            HarmonyPatches.InitializeHarmony();
        }
    }
}
