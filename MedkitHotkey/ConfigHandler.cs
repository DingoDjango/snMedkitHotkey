using System;
using System.IO;
using UnityEngine;

namespace MedkitHotkey
{
    /* Json serialization from:
     * https://docs.unity3d.com/Manual/JSONSerialization.html
     *
     * Directory fetching snippet from:
     * https://stackoverflow.com/questions/52797/how-do-i-get-the-path-of-the-assembly-the-code-is-in */

    internal class ConfigHandler
    {
        private const string SettingsFileName = "settings.json";

        private bool ValidateValues(Config cfg, out KeyCode selectedKey)
        {
            selectedKey = KeyCode.None;

            foreach (KeyCode key in Enum.GetValues(typeof(KeyCode)))
            {
                if (String.Equals(key.ToString(), cfg.FirstAidHotkey, StringComparison.OrdinalIgnoreCase))
                {
                    selectedKey = key;

                    break;
                }
            }

            if (selectedKey != KeyCode.None)
            {
                return true;
            }

            return false;
        }

        internal bool TryLoadConfig()
        {
            try
            {
                string settingsFilePath = Path.Combine(Entry.GetAssemblyDirectory, SettingsFileName);
                string settingsAsJson = File.ReadAllText(settingsFilePath);
                Config configFromJson = JsonUtility.FromJson<Config>(settingsAsJson);

                if (!this.ValidateValues(configFromJson, out KeyCode validatedKeyCode))
                {
                    return false;
                }

                HarmonyPatches.FirstAidHotkey = validatedKeyCode;
            }

            catch (Exception ex)
            {
                Debug.Log($"[MedkitHotkey] :: {ex.ToString()}");

                return false;
            }

            return true;
        }
    }
}
