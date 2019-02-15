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

		public static bool TextInputOpen;

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

			// Update player text input mode (for signs, lockers, console, etc.)
			MethodInfo inputGroupOnSelect = AccessTools.Method(typeof(uGUI_InputGroup), nameof(uGUI_InputGroup.OnSelect));
			MethodInfo inputGroupOnDeselect = AccessTools.Method(typeof(uGUI_InputGroup), nameof(uGUI_InputGroup.OnDeselect));
			MethodInfo inputGroupDeselect = AccessTools.Method(typeof(uGUI_InputGroup), nameof(uGUI_InputGroup.Deselect));

			harmony.Patch(inputGroupOnSelect, null, new HarmonyMethod(typeof(Entry), nameof(Patch_uGUI_InputGroup_OnSelect)), null);
			harmony.Patch(inputGroupOnDeselect, null, new HarmonyMethod(typeof(Entry), nameof(Patch_uGUI_InputGroup_OnDeselect)), null);
			harmony.Patch(inputGroupDeselect, null, new HarmonyMethod(typeof(Entry), nameof(Patch_uGUI_InputGroup_Deselect)), null);

			// Listen for First Aid Kit hotkey. Injected where QuickSlots are handled for convenience.
			MethodInfo handleQuickSlotsInput = AccessTools.Method(typeof(uGUI_QuickSlots), "HandleInput");

			harmony.Patch(handleQuickSlotsInput, null, new HarmonyMethod(typeof(Entry), nameof(Patch_uGUI_QuickSlots_ListenForMedkit)), null);
		}

		public static void Patch_uGUI_InputGroup_OnSelect()
		{
			TextInputOpen = true;
		}

		public static void Patch_uGUI_InputGroup_OnDeselect()
		{
			TextInputOpen = false;
		}

		public static void Patch_uGUI_InputGroup_Deselect()
		{
			TextInputOpen = false;
		}

		public static void Patch_uGUI_QuickSlots_ListenForMedkit()
		{
			if (!TextInputOpen && Input.GetKeyDown(SelectedHotkey) && !IntroVignette.isIntroActive && Player.main.GetCanItemBeUsed())
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
