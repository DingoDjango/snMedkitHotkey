using System;
using BepInEx.Configuration;
using Nautilus.Handlers;
using Nautilus.Options;
using UnityEngine;

namespace MedkitHotkey
{
	public class ModSettings : ModOptions
	{
		private ModKeybindOption firstAidKeyOption;

		public ModSettings() : base(ModPlugin.modName)
		{
			OptionsPanelHandler.RegisterModOptions(this);

			// ModKeybindOption firstAidKeyOption = ModPlugin.ConfigFirstAidKey.ToModKeybindOption();
			this.firstAidKeyOption = this.ToModKeybindOptionTemp(ModPlugin.ConfigFirstAidKey);

			this.AddItem(firstAidKeyOption);

			ModPlugin.LogMessage($"Tooltip set to {firstAidKeyOption.Tooltip}");
		}

		private ModKeybindOption ToModKeybindOptionTemp(ConfigEntry<KeyCode> configEntry)
		{
			ModKeybindOption optionItem = ModKeybindOption.Create(id: $"{configEntry.Definition.Section}_{configEntry.Definition.Key}",
			label: configEntry.Definition.Key, device: ModPlugin.PrimaryDevice, key: configEntry.Value, tooltip: configEntry.Description.Description);

			GameInput.OnPrimaryDeviceChanged += () =>
			{
				optionItem.Device = ModPlugin.PrimaryDevice;
				ModPlugin.LogMessage($"Device changed to {optionItem.Device}");
			};

			optionItem.OnChanged += (_, e) =>
			{
				configEntry.Value = e.Value;

				ModPlugin.LogMessage($"event value is {e.Value}");
				ModPlugin.LogMessage($"current firstAidKeyOption value is {firstAidKeyOption.Value}");
				ModPlugin.LogMessage($"current config entry value is {ModPlugin.ConfigFirstAidKey.Value}");
			};

			return optionItem;
		}
	}
}
