using BepInEx;
using Nautilus.Handlers;
using UnityEngine;

namespace MedkitHotkey
{
    [BepInPlugin(modGUID, modName, modVersion)]
	[BepInDependency("com.snmodding.nautilus")]
	public class ModPlugin : ModPluginBase
    {
        private const string modGUID = "Dingo.SNBZ.MedkitHotkey";
        internal const string modName = "Medkit Hotkey BZ";
        private const string modVersion = "3.0.8.3031";

        private void Awake()
        {
            Instance = this;

            LanguageHandler.RegisterLocalizationFolder();

            options = OptionsPanelHandler.RegisterModOptions<ModOptions>();

            Keybinds.Initialize();

            HarmonyPatches.InitializeHarmony();
        }

        public override void LogMessage(string message)
        {
            Debug.Log($"{modName} :: {message}");
        }

        public override void LogWarning(string warning)
        {
            Debug.LogWarning($"{modName} :: {warning}");
        }

        public override void LogError(string error)
        {
            Debug.LogError($"{modName} :: {error}");
        }
    }
}
