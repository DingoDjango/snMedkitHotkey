using BepInEx;
using BepInEx.Configuration;
using Nautilus.Handlers;
using UnityEngine;

namespace MedkitHotkey
{
    [BepInPlugin(modGUID, modName, modVersion)]
	[BepInDependency("com.snmodding.nautilus")]
	public class ModPlugin : BaseUnityPlugin
    {
        private const string modGUID = "Dingo.SNBZ.MedkitHotkey";
        internal const string modName = "Medkit Hotkey BZ";
        private const string modVersion = "2.2.1";

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

		private void Awake()
		{
			LanguageHandler.RegisterLocalizationFolder();

			this.InitializeConfig();

			// this.modSettings = new ModSettings();

			HarmonyPatches.InitializeHarmony();
		}
	}
}
