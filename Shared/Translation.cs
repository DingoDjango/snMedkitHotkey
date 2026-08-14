using System;
using System.Collections.Generic;

namespace MedkitHotkey
{
    internal static class Translation
    {
        private static readonly HashSet<string> LoggedMissingKeys = new HashSet<string>();

        private static void LogMissingKey(string source)
        {
            if (LoggedMissingKeys.Add(source))
            {
                ModPlugin.Instance.LogWarning($"Could not find translated string for `{source}`");
            }
        }

        internal static string Translate(this string source)
        {
            if (Language.main.TryGet(source, out string translated))
            {
                return translated;
            }

            LogMissingKey(source);
            return source;
        }

        internal static string FormatTranslate(this string source, params object[] args)
        {
            string basic = source.Translate();

            if (args != null && args.Length > 0)
            {
                try
                {
                    return string.Format(basic, args);
                }
                catch (Exception ex)
                {
                    ModPlugin.Instance.LogError($"Failed to format '{source}': {ex}");
                }
            }

            return basic;
        }

        internal static string TryFormatTranslate(this string source, params object[] args)
        {
            if (!Language.main.TryGet(source, out string basic))
            {
                return null;
            }

            if (args == null || args.Length == 0)
            {
                return basic;
            }

            try
            {
                return string.Format(basic, args);
            }
            catch (Exception ex)
            {
                ModPlugin.Instance.LogMessage($"Failed to format '{source}': {ex}");
                return null;
            }
        }
    }
}
