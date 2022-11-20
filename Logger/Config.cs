using System;
using System.IO;

using BepInEx.Logging;

namespace Utils.Logger;

public class Config {
    internal static ManualLogSource logger;
    private static string tempLogFile;


    // Load the logs start configs.
    public static void Load(ManualLogSource logger, string worldType) {
        Config.logger = logger;

        tempLogFile = $"{Utils.Settings.Config.PluginFolderPath}\\{Utils.Settings.Config.PluginGUID}-{worldType}.txt";

        Log.Start($"Using \"{tempLogFile}\" to save logs.");
    }

    internal static void logFile(object data, string level, string prefix = "") {
        if (Utils.Settings.Debug.LogOnTempFile) {
            using (StreamWriter w = File.AppendText(tempLogFile)) {
                var msg = $"{prefix}{DateTime.Now.ToString("hh:mm:ss")} [{level} {Utils.Settings.Config.PluginGUID}]: {data}";
                w.WriteLine(msg);
            }
        }
    }
}
