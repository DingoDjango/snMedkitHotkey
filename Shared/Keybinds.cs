using Nautilus.Handlers;
using UnityEngine;

namespace MedkitHotkey
{
    internal static class Keybinds
    {
        internal static GameInput.Button FirstAidKey { get; private set; }

        internal static void Initialize()
        {
            FirstAidKey = EnumHandler.AddEntry<GameInput.Button>("MedkitHotkeyFirstAidKit")
                .CreateInput("FirstAidKey".Translate(), "Tooltip_FirstAidKey".Translate())
                .WithKeyboardBinding("<Keyboard>/h")
                .WithBinding(GameInput.Device.Controller, GameInput.BindingSet.Primary, string.Empty)
                .AvoidConflicts(GameInput.Device.Keyboard)
                .WithCategory(ModPlugin.modName);
        }
    }
}
