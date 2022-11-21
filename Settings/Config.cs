using System;
using System.IO;
using BepInEx.Configuration;

namespace Utils.Settings;

public class Config {
    public static string PluginGUID;
    public static string PluginFolderPath;

    internal static ConfigFile cfg;

    public static void Load(string pluginGUID, ConfigFile config, params Action[] actions) {
        PluginGUID = pluginGUID;
        PluginFolderPath = $"{BepInEx.Paths.ConfigPath}\\{PluginGUID}";
        if (!Directory.Exists(PluginFolderPath)) {
            Directory.CreateDirectory(PluginFolderPath);
        }

        Config.cfg = config;

        Debug.load();

        if (actions == null || actions.Length == 0) return;
        foreach (var action in actions) {
            action();
        }
    }
}
