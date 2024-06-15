using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using HarmonyLib.Tools;

namespace MedkitHotkey
{
    public static class HarmonyPatches
    {
        private static bool TextInputOpen;

        private static void Patch_GUI_OnDeselect_Postfix()
        {
            TextInputOpen = false;
        }

        private static void Patch_GUI_OnSelect_Postfix()
        {
            TextInputOpen = true;
        }

        private static void Patch_HandleInput_Postfix()
        {
            if (!TextInputOpen && GameInput.GetKeyDown(ModPlugin.ConfigFirstAidKey.Value) && Player.main.GetCanItemBeUsed())
            {
                Inventory playerInventory = Inventory.main;

                IList<InventoryItem> medkits = playerInventory?.container.GetItems(TechType.FirstAidKit);

                if (medkits != null)
                {
                    playerInventory.ExecuteItemAction(ItemAction.Use, medkits.First());
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
            Harmony harmony = new Harmony("Dingo.Harmony.MedkitHotkey");

#if DEBUG
            HarmonyFileLog.Enabled = true;
#endif

            /* Take note of player text input mode to account for signs, lockers, console, etc. */
            // Patch: uGUI_InputGroup.Deselect
            harmony.Patch(
                original: AccessTools.Method(typeof(uGUI_InputGroup), nameof(uGUI_InputGroup.Deselect)),
                postfix: new HarmonyMethod(typeof(HarmonyPatches), nameof(HarmonyPatches.Patch_GUI_OnDeselect_Postfix)));
            // Patch: uGUI_InputGroup.OnDeselect
            harmony.Patch(
                original: AccessTools.Method(typeof(uGUI_InputGroup), nameof(uGUI_InputGroup.OnDeselect)),
                postfix: new HarmonyMethod(typeof(HarmonyPatches), nameof(HarmonyPatches.Patch_GUI_OnDeselect_Postfix)));
            // Patch: uGUI_InputGroup.OnSelect
            harmony.Patch(
                original: AccessTools.Method(typeof(uGUI_InputGroup), nameof(uGUI_InputGroup.OnSelect)),
                postfix: new HarmonyMethod(typeof(HarmonyPatches), nameof(HarmonyPatches.Patch_GUI_OnSelect_Postfix)));

            /* Listen for First Aid Kit hotkey press */
            // Patch: uGUI_QuickSlots.HandleInput
            harmony.Patch(
                original: AccessTools.Method(typeof(uGUI_QuickSlots), "HandleInput"),
                postfix: new HarmonyMethod(typeof(HarmonyPatches), nameof(HarmonyPatches.Patch_HandleInput_Postfix)));

            /* Reset language cache on game language change */
            // Patch: Language.SetCurrentLanguage
            harmony.Patch(
                original: AccessTools.Method(typeof(Language), nameof(Language.SetCurrentLanguage)),
                postfix: new HarmonyMethod(typeof(HarmonyPatches), nameof(HarmonyPatches.Patch_SetCurrentLanguage_Postfix)));
        }
    }
}
