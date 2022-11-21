using System;
using System.IO;
using System.Text.Json;
using Utils.Logger;

namespace Utils.Database;

public static class DB {
    internal static JsonSerializerOptions JSONOptions = new() {
        WriteIndented = false,
        IncludeFields = false
    };
    internal static JsonSerializerOptions Pretty_JSON_options = new() {
        WriteIndented = true,
        IncludeFields = true
    };

    private static Action[] loadActions;
    private static Action[] saveActions;
    private static Action[] cleanActions;

    public static void Config(Action[] load, Action[] save, Action[] clean = null) {
        loadActions = load;
        saveActions = save;
        cleanActions = clean;

        if (loadActions == null || saveActions == null) {
            Log.Warning($"Null nubmer of save/load actions");
            return;
        }

        if (loadActions.Length != saveActions.Length) {
            Log.Warning($"Different number of database loads({loadActions.Length}) and saves({saveActions.Length})");
        }
    }

    // Load all database actions.
    public static void Load() {
        if (loadActions == null) {
            Log.Error("Error loading database, null actions.");
            return;
        }
        foreach (var action in loadActions) {
            action();
        }

        Log.Info($"All({loadActions.Length}) database actions loaded.");
    }

    // Save all database actions.
    public static void Save() {
        if (saveActions == null) {
            Log.Error("Error saving database, null actions.");
            return;
        }
        foreach (var action in saveActions) {
            action();
        }

        Log.Info($"All({saveActions.Length}) database actions saved.");
    }

    // Clean all database actions.
    public static void Clean() {
        if (cleanActions == null) {
            Log.Error("Error garbage collect database, null actions.");
            return;
        }

        foreach (var action in cleanActions) {
            action();
        }

        Log.Info($"All({cleanActions.Length}) database actions cleaned.");
    }

    public static void save<T>(string fileName, T data, JsonSerializerOptions jsonOptions) {
        var filePath = $"{Utils.Settings.Config.PluginFolderPath}\\{fileName}.json";
        File.WriteAllText(filePath, JsonSerializer.Serialize(data, jsonOptions));
    }

    public static void load<T>(string fileName, ref T data) where T : new() {
        var filePath = $"{Utils.Settings.Config.PluginFolderPath}\\{fileName}.json";
        if (!File.Exists(filePath)) {
            FileStream stream = File.Create(filePath);
            stream.Dispose();
        }
        string json = File.ReadAllText(filePath);
        try {
            data = JsonSerializer.Deserialize<T>(json);
            Log.Trace($"{fileName} DB Populated");
        } catch {
            data = new T();
            Log.Trace($"{fileName} DB Created");
        }
    }
}
