using System;
using Nautilus.Handlers;
using Nautilus.Options;

namespace MedkitHotkey
{
	public class ModSettings : ModOptions
	{
		public ModSettings() : base(ModPlugin.modName)
		{
			OptionsPanelHandler.RegisterModOptions(this);

			ModKeybindOption firstAidKeyOption = ModPlugin.ConfigFirstAidKey.ToModKeybindOption();
			firstAidKeyOption.OnChanged += this.OnChangedFirstAidKey;
			this.AddItem(firstAidKeyOption);
		}

		private void OnChangedFirstAidKey(object sender, KeybindChangedEventArgs e)
		{
			ModPlugin.ConfigFirstAidKey.Value = e.Value;
		}
	}
}
