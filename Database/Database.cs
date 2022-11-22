using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Utils.Logger;

namespace Utils.Database;

public static class DB {
    private static JsonSerializerOptions JSONOptions = new() {
        WriteIndented = false,
        IncludeFields = false
    };
    private static JsonSerializerOptions Pretty_JSON_options = new() {
        WriteIndented = true,
        IncludeFields = true
    };

    private static List<Action> loadActions = new List<Action>();
    private static List<Action> saveActions = new List<Action>();
    private static List<Action> cleanActions = new List<Action>();

    public static void Setup(List<Action> load, List<Action> save, List<Action> clean) {
        if (load != null) {
            loadActions.AddRange(load);
        }
        if (save != null) {
            saveActions.AddRange(save);
        }
        if (clean != null) {
            cleanActions.AddRange(clean);
        }
    }

    // Load all database actions.
    public static void Load() {
        if (loadActions == null) return;
        foreach (var action in loadActions) {
            action();
        }

        Log.Info($"All({loadActions.Count}) database actions loaded.");
    }

    // Save all database actions.
    public static void Save() {
        if (saveActions == null) return;
        foreach (var action in saveActions) {
            action();
        }

        Log.Info($"All({saveActions.Count}) database actions saved.");
    }

    // Clean all database actions.
    public static void Clean() {
        if (cleanActions == null) return;
        foreach (var action in cleanActions) {
            action();
        }

        Log.Info($"All({cleanActions.Count}) database actions cleaned.");
    }

    // AddLoadActions add new action to Load command.
    public static void AddLoadActions(params Action[] actions) {
        loadActions.AddRange(actions);
    }

    // AddSaveActions add new action to Save command.
    public static void AddSaveActions(params Action[] actions) {
        saveActions.AddRange(actions);
    }

    // AddLoadActions add new action to Clean command.
    public static void AddCleanActions(params Action[] actions) {
        cleanActions.AddRange(actions);
    }

    // Helpers

    public static void saveFile<T>(string fileName, T data, bool pretty = false, string extension = ".json") {
        var jsonOptions = JSONOptions;
        if (pretty) { jsonOptions = Pretty_JSON_options; }
        var filePath = $"{Settings.Config.PluginFolderPath}\\{fileName}{extension}";
        File.WriteAllText(filePath, JsonSerializer.Serialize(data, jsonOptions));
    }

    public static void loadFile<T>(string fileName, ref T data, string extension = ".json") where T : new() {
        var filePath = $"{Settings.Config.PluginFolderPath}\\{fileName}{extension}";
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
