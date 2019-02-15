using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Harmony;
using UnityEngine;

namespace MedkitHotkey
{
	public static class Entry
	{
		public static KeyCode SelectedHotkey;

		public static void Initialize()
		{
			if (ConfigHandler.TryLoadConfig())
			{
#if DEBUG
				Debug.Log($"[MedkitHotkey] :: Current hotkey is '{SelectedHotkey.ToString()}'");
#endif

				InitializeHarmony();

#if DEBUG
				Debug.Log($"[MedkitHotkey] :: Initialized Harmony");
#endif
			}

			else
			{
				Debug.Log("[MedkitHotkey] :: Could not load hotkey from config file! Mod has not been enabled.");
			}
		}

		public static void InitializeHarmony()
		{
			HarmonyInstance harmony = HarmonyInstance.Create("dingo.medkithotkey");

#if DEBUG
			HarmonyInstance.DEBUG = true;
#endif

			/* uGUI_QuickSlots.Update checks game state and does all of the necessary null checks.
			 * This hotkey is only relevant when QuickSlots are relevant, so we check for input in the same method. */
			MethodInfo handleQuickSlotsInput = AccessTools.Method(typeof(uGUI_QuickSlots), "HandleInput");

			harmony.Patch(handleQuickSlotsInput, null, new HarmonyMethod(typeof(Entry), nameof(Patch_uGUI_QuickSlots_ListenForMedkit)), null);
		}

		public static void Patch_uGUI_QuickSlots_ListenForMedkit()
		{
			if (!IntroVignette.isIntroActive && Player.main.GetCanItemBeUsed() && Input.GetKeyDown(SelectedHotkey))
			{
#if DEBUG
				Debug.Log($"[MedkitHotkey] :: Pressed '{SelectedHotkey.ToString()}', looking for medkit.");
#endif

				Inventory playerInventory = Inventory.main;

				IList<InventoryItem> medkits = playerInventory.container.GetItems(TechType.FirstAidKit);

				if (medkits != null)
				{
					playerInventory.UseItem(medkits.First());
				}

				else
				{
					ErrorMessage.AddWarning("No first aid kit in inventory!");
				}
			}
		}
	}
}
