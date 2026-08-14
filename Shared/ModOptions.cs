using Nautilus.Extensions;
using Nautilus.Json;
using Nautilus.Options;
using Nautilus.Options.Attributes;
using System.Collections;
using UnityEngine;

namespace MedkitHotkey
{
    [Menu(ModPlugin.modName)]
    public class ModOptions : ConfigFile
    {
        private static GameObject firstAidKeyObject;
        private static KeyCode pendingFirstAidKey;
        private static bool hasPendingFirstAidKey;

        [Keybind(null, LabelLanguageId = "FirstAidKey", TooltipLanguageId = "Tooltip_FirstAidKey")]
        [OnGameObjectCreated(nameof(OnFirstAidKeyOptionCreated))]
        [OnChange(nameof(OnFirstAidKeyChanged))]
        public KeyCode FirstAidKey = KeyCode.H;

        private void OnFirstAidKeyOptionCreated(GameObjectCreatedEventArgs e)
        {
            firstAidKeyObject = e.Value;
        }

        private void OnFirstAidKeyChanged(object sender, KeybindChangedEventArgs e)
        {
            pendingFirstAidKey = e.Value;
            hasPendingFirstAidKey = true;
            ModPlugin.Instance.StartCoroutine(UpdateFirstAidKeyDisplay());
        }

        // Temporary fix for Nautilus rebind display bug in BZ
        private IEnumerator UpdateFirstAidKeyDisplay()
        {
            yield return null;
            if (!hasPendingFirstAidKey) yield break;
            if (firstAidKeyObject == null) yield break;
            uGUI_Binding binding = firstAidKeyObject.GetComponentInChildren<uGUI_Binding>();
            if (binding == null) yield break;
            string inputName = pendingFirstAidKey.KeyCodeToString();
            string buttonName = GameInput.GetInputName(inputName);
            string displayText = uGUI.GetDisplayTextForBinding(buttonName);
            binding.currentText.text = displayText;
            hasPendingFirstAidKey = false;
        }
    }
}
