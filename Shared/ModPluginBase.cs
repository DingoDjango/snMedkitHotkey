using BepInEx;

namespace MedkitHotkey
{
    public abstract class ModPluginBase : BaseUnityPlugin
    {
        public static ModPlugin Instance;

#if BELOWZERO
        public static ModOptions options;
#endif

        public abstract void LogMessage(string message);

        public abstract void LogWarning(string warning);

        public abstract void LogError(string error);
    }
}
