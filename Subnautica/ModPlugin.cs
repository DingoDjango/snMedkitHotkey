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
        private const string modVersion = "2.1.0";

        public static ConfigEntry<KeyCode> ConfigFirstAidKey;

        private void InitializeConfig()
        {
            ConfigFirstAidKey = this.Config.Bind(
                section: "General",
                key: "First Aid Kit Hotkey",
                defaultValue: KeyCode.H,
                description: "Keybinding used to activate a First Aid Kit from inventory, if one is available.");
        }

        internal static void LogMessage(string message)
        {
            Debug.Log($"{modName} :: " + message);
        }

        public void Start()
        {
            this.InitializeConfig();

            HarmonyPatches.InitializeHarmony();
        }
    }
}
