using System;
using System.IO;
using BepInEx.Logging;

namespace Logger;

public class Config {
    internal static ManualLogSource logger;
    private static string tempLogFile;
    internal static bool logOnFile;
    internal static bool traceLevel;


    // Load the logs start configs.
    public static void Load(ManualLogSource logger, string worldType, bool logOnFile, bool traceLevel) {
        Config.logger = logger;
        Config.logOnFile = logOnFile;
        Config.traceLevel = traceLevel;

        if (!Directory.Exists(Settings.Config.PluginFolderPath)) {
            Directory.CreateDirectory(Settings.Config.PluginFolderPath);
        }

        tempLogFile = $"{Settings.Config.PluginFolderPath}\\{Settings.Config.PluginGUID}-{worldType}.txt";

        Log.Start($"Using \"{tempLogFile}\" to save logs.");
    }

    internal static void logFile(object data, string level, string prefix = "") {
        if (logOnFile) {
            using (StreamWriter w = File.AppendText(tempLogFile)) {
                var msg = $"{prefix}{DateTime.Now.ToString("hh:mm:ss")} [{level} {Settings.Config.PluginGUID}]: {data}";
                w.WriteLine(msg);
            }
        }
    }
}
