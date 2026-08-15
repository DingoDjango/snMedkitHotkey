using Nautilus.Handlers;

namespace MedkitHotkey
{
    internal class Keybinds
    {
        private static string MedkitHotkeyDefault => GameInputHandler.Paths.Keyboard.H;

        internal GameInput.Button FirstAidKey = EnumHandler.AddEntry<GameInput.Button>("MedkitHotkeyFirstAidKit")
                .CreateInput("FirstAidKey".Translate(), "Tooltip_FirstAidKey".Translate())
                .WithKeyboardBinding(MedkitHotkeyDefault)
                .WithControllerBinding("None")
                .AvoidConflicts(GameInput.Device.Keyboard)
                .WithCategory(ModPlugin.modName);
    }
}
