using BepInEx;
using Nautilus.Handlers;
using UnityEngine;

namespace MedkitHotkey
{
    [BepInPlugin(modGUID, modName, modVersion)]
    [BepInDependency("com.snmodding.nautilus")]
    public class ModPlugin : ModPluginBase
    {
        public const string modGUID = "Dingo.SN.MedkitHotkey";
        public const string modName = "Medkit Hotkey";
        public const string modVersion = "3.0.8.3031";

        internal Keybinds keyBinds;

        private void Awake()
        {
            Instance = this;

            LanguageHandler.RegisterLocalizationFolder();

            keyBinds = new Keybinds();

            // SN version uses Keybinds with new system
            // options = OptionsPanelHandler.RegisterModOptions<ModOptions>();

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
