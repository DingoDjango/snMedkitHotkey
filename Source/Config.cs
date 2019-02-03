using System;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace MedkitHotkey
{
	/* Json serialization from:
	 * https://docs.unity3d.com/Manual/JSONSerialization.html
	 * 
	 * Directory fetching snippet from:
	 * https://stackoverflow.com/questions/52797/how-do-i-get-the-path-of-the-assembly-the-code-is-in */

	[Serializable]
	public class Config
	{
		public string FirstAidHotkey = "h";
	}

	public static class ConfigHandler
	{
		private static readonly string SettingsFileName = "settings.json";

		private static string GetAssemblyDirectory
		{
			get
			{
				string codeBase = Assembly.GetExecutingAssembly().CodeBase;
				UriBuilder uri = new UriBuilder(codeBase);
				string path = Uri.UnescapeDataString(uri.Path);
				return Path.GetDirectoryName(path);
			}
		}

		public static bool TryLoadConfig()
		{
			try
			{
				string settingsFilePath = Path.Combine(GetAssemblyDirectory, SettingsFileName);
				string settingsAsJson = File.ReadAllText(settingsFilePath);
				Config configFromJson = JsonUtility.FromJson<Config>(settingsAsJson);

				// Missing some validation here...

				Entry.ModConfig = configFromJson;
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
