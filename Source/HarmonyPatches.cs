using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Harmony;
using UnityEngine;

namespace MedkitHotkey
{
    public static class HarmonyPatches
    {
        public static KeyCode FirstAidHotkey;

        public static bool TextInputOpen;

        private static void Patch_uGUI_InputGroup_Deselect_Postfix()
        {
            TextInputOpen = false;
        }

        private static void Patch_uGUI_InputGroup_OnDeselect_Postfix()
        {
            TextInputOpen = false;
        }

        private static void Patch_uGUI_InputGroup_OnSelect_Postfix()
        {
            TextInputOpen = true;
        }

        private static void Patch_uGUI_QuickSlots_HandleInput_Postfix()
        {
            if (!TextInputOpen && Input.GetKeyDown(FirstAidHotkey) && !IntroVignette.isIntroActive && Player.main.GetCanItemBeUsed())
            {
#if DEBUG
				Debug.Log($"[MedkitHotkey] :: Pressed '{FirstAidHotkey.ToString()}', looking for medkit.");
#endif

                Inventory playerInventory = Inventory.main;

                IList<InventoryItem> medkits = playerInventory?.container.GetItems(TechType.FirstAidKit);

                if (medkits != null)
                {
                    playerInventory.UseItem(medkits.First());
                }

                else
                {
                    ErrorMessage.AddWarning("MissingMedkit".Translate());
                }
            }
        }

        private static void Patch_SetCurrentLanguage_Postfix()
        {
            Translation.ReloadLanguage();
        }

        internal static void InitializeHarmony()
        {
            HarmonyInstance harmony = HarmonyInstance.Create("dingo.medkithotkey");

#if DEBUG
			HarmonyInstance.DEBUG = true;
#endif

            MethodInfo inputGroupDeselect = AccessTools.Method(typeof(uGUI_InputGroup), nameof(uGUI_InputGroup.Deselect));
            MethodInfo inputGroupOnDeselect = AccessTools.Method(typeof(uGUI_InputGroup), nameof(uGUI_InputGroup.OnDeselect));
            MethodInfo inputGroupOnSelect = AccessTools.Method(typeof(uGUI_InputGroup), nameof(uGUI_InputGroup.OnSelect));
            MethodInfo handleQuickSlotsInput = AccessTools.Method(typeof(uGUI_QuickSlots), "HandleInput");
            MethodInfo setLanguage = AccessTools.Method(typeof(Language), nameof(Language.SetCurrentLanguage));

            // Update player text input mode (for signs, lockers, console, etc.)
            harmony.Patch(inputGroupDeselect, null, new HarmonyMethod(typeof(HarmonyPatches), nameof(HarmonyPatches.Patch_uGUI_InputGroup_Deselect_Postfix)), null);
            harmony.Patch(inputGroupOnDeselect, null, new HarmonyMethod(typeof(HarmonyPatches), nameof(HarmonyPatches.Patch_uGUI_InputGroup_OnDeselect_Postfix)), null);
            harmony.Patch(inputGroupOnSelect, null, new HarmonyMethod(typeof(HarmonyPatches), nameof(HarmonyPatches.Patch_uGUI_InputGroup_OnSelect_Postfix)), null);

            // Listen for First Aid Kit hotkey. Injected where QuickSlots are handled.
            harmony.Patch(handleQuickSlotsInput, null, new HarmonyMethod(typeof(HarmonyPatches), nameof(HarmonyPatches.Patch_uGUI_QuickSlots_HandleInput_Postfix)), null);

            // Reset language cache upon language change
            harmony.Patch(setLanguage, null, new HarmonyMethod(typeof(HarmonyPatches), nameof(HarmonyPatches.Patch_SetCurrentLanguage_Postfix)), null);
        }
    }
}
