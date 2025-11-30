using MinecraftLaunch.Base.Models.JsonConverter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;

namespace MinecraftLaunch.Utilities;

public static class JsonSerializerUtil {
    public static JsonSerializerOptions GetDefaultOptions() {
        var options = new JsonSerializerOptions {
            MaxDepth = 100,
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            Converters = {
                new DateTimeJsonConverter()
            },
        };

        return options;
    }
}
