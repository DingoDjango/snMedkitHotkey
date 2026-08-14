using System;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using HarmonyLib.Tools;
using UnityEngine;

namespace MedkitHotkey
{
    public static class HarmonyPatches
    {
        private static bool GetCanMedkitBeUsed()
        {
            if (Player.main.GetPDA().isInUse ||
                PlayerCinematicController.cinematicModeCount > 0 ||
                !Player.main.liveMixin.IsAlive() || // Respawn screen outlives vanilla 5s
                (FPSInputModule.current != null && FPSInputModule.current.lastGroup != null)) // Any UI input group active: sign/subname/console/crafting/builder menu etc.
            {
                return false;
            }

            return true;
        }

        private static void Patch_HandleInput_Postfix()
        {
#if BELOWZERO
            if (Input.GetKeyDown(ModPlugin.options.FirstAidKey))
#else
            if (GameInput.IsInitialized && GameInput.GetButtonDown(Keybinds.FirstAidKey))
#endif
            {
                if (GetCanMedkitBeUsed()) // `Player.main.GetCanItemBeUsed` checked in HandleInput
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
        }

        internal static void InitializeHarmony()
        {
            Harmony harmony = new Harmony("Dingo.Harmony.MedkitHotkey");

            try
            {
                harmony.Patch(
                    original: AccessTools.Method(typeof(uGUI_QuickSlots), "HandleInput"),
                    postfix: new HarmonyMethod(typeof(HarmonyPatches), nameof(HarmonyPatches.Patch_HandleInput_Postfix)));
            }
            catch (Exception ex)
            {
                ModPlugin.Instance?.LogError($"{ex}");
            }
        }
    }
}
