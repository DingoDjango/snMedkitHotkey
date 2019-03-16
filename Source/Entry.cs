using System.IO;
using System.Reflection;
using UnityEngine;

namespace MedkitHotkey
{
    public static class Entry
    {
        internal static string GetAssemblyDirectory
        {
            get
            {
                string fullPath = Assembly.GetExecutingAssembly().Location;
                return Path.GetDirectoryName(fullPath);
            }
        }

        public static void Initialize()
        {
            ConfigHandler startupHandler = new ConfigHandler();

            if (startupHandler.TryLoadConfig())
            {
#if DEBUG
				Debug.Log($"[MedkitHotkey] :: Current hotkey is '{HarmonyPatches.FirstAidHotkey.ToString()}'");
#endif

                HarmonyPatches.InitializeHarmony();

#if DEBUG
				Debug.Log($"[MedkitHotkey] :: Initialized Harmony");
#endif
            }

            else
            {
                Debug.Log("[MedkitHotkey] :: Could not load hotkey from config file! Mod has not been enabled.");
            }
        }
    }
}
