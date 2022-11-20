using System;
using System.IO;

using BepInEx.Configuration;

using Utils.Settings;

namespace Utils.Settings;

public class Config {
    public static string PluginGUID;
    public static string PluginFolderPath = getPluginFolderPath();

    internal static ConfigFile cfg;

    public static void Load(string pluginGUID, ConfigFile config, params Action[] actions) {
        PluginGUID = pluginGUID;
        Config.cfg = config;

        Debug.load();

        if (actions == null || actions.Length == 0) return;
        foreach (var action in actions) {
            action();
        }
    }

    private static string getPluginFolderPath() {
        var pluginFolderPath = $"{BepInEx.Paths.ConfigPath}\\{PluginGUID}";
        if (!Directory.Exists(pluginFolderPath)) {
            Directory.CreateDirectory(pluginFolderPath);
        }
        return pluginFolderPath;
    }
}
