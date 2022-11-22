using System.Reflection;
using BepInEx.Configuration;

namespace Utils.Settings;

public static class ENV {
    public static bool LogOnTempFile = false;
    public static bool EnableTraceLogs = false;

    public class Debug {
        private static string debugSection = "🪲Debug";
        private static ConfigEntry<bool> DebugLogOnTempFile;
        private static ConfigEntry<bool> DebugEnableTraceLogs;

        public static void Setup() {
            Config.AddConfigActions(
                () => load()
            );
        }

        // Load the plugin debug variables.
        private static void load() {
            if (enableDebugConfigs()) {
                DebugLogOnTempFile = Config.cfg.Bind(
                    debugSection,
                    "LogOnTempFile",
                    false,
                    "Enabled, will log every plugin log on a temp file"
                );

                DebugEnableTraceLogs = Config.cfg.Bind(
                    debugSection,
                    "EnableTraceLogs",
                    false,
                    "Enabled, will print Trace logs (Debug output in BepInEx)"
                );
            }

            validateValues();
        }

        private static void validateValues() {
            if (DebugLogOnTempFile != null) {
                LogOnTempFile = DebugLogOnTempFile.Value;
            }

            if (DebugEnableTraceLogs != null) {
                EnableTraceLogs = DebugEnableTraceLogs.Value;
            }

            Config.cfg.Save();
        }

        private static bool enableDebugConfigs() {
            var assemblyConfigurationAttribute = typeof(Debug).Assembly.GetCustomAttribute<AssemblyConfigurationAttribute>();
            var buildConfigurationName = assemblyConfigurationAttribute?.Configuration;
            return buildConfigurationName != "Release";
        }
    }
}