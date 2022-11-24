using System.Collections.Concurrent;
using System;

namespace Utils.Database;

public static class Cache {
    public static ConcurrentDictionary<string, long> LastUpdate = new ConcurrentDictionary<string, long>();

    public static bool IsBlocked(string key, long blockedDuration) {
        var now = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        if (LastUpdate.TryGetValue(key, out long timestamp) && (now - timestamp) < blockedDuration) {
            return true;
        }

        LastUpdate.AddOrUpdate(key, now, (_, _) => now);
        return false;
    }
}