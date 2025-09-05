using BepInEx;
using BepInEx.Configuration;
using Nautilus.Handlers;
using Nautilus.Options;
using UnityEngine;

namespace MedkitHotkey
{
    [BepInPlugin(modGUID, modName, modVersion)]
	[BepInDependency("com.snmodding.nautilus")]
	public class ModPlugin : BaseUnityPlugin
    {
        private const string modGUID = "Dingo.SN.MedkitHotkey";
        internal const string modName = "Medkit Hotkey";
        private const string modVersion = "2.2.0";

     //   private ModOptions modSettings;

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

     //       this.modSettings = new ModSettings();

			HarmonyPatches.InitializeHarmony();
        }
    }
}
