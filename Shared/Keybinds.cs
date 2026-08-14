using Nautilus.Handlers;

namespace MedkitHotkey
{
    internal static class Keybinds
    {
        internal static GameInput.Button FirstAidKey;

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
