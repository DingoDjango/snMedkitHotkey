using Nautilus.Handlers;

namespace MedkitHotkey
{
    internal class Keybinds
    {
        internal GameInput.Button FirstAidKey = EnumHandler.AddEntry<GameInput.Button>("MedkitHotkeyFirstAidKit")
                .CreateInput("FirstAidKey".Translate(), "Tooltip_FirstAidKey".Translate())
                .WithKeyboardBinding("<Keyboard>/h")
                .AvoidConflicts(GameInput.Device.Keyboard)
                .WithCategory(ModPlugin.modName);
    }
}
