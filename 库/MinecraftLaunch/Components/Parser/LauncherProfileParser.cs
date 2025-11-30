using MinecraftLaunch.Base.Interfaces;
using MinecraftLaunch.Base.Models.Game;
using MinecraftLaunch.Extensions;
using MinecraftLaunch.Utilities;
using System.Text;
using System.Text.Json;

namespace MinecraftLaunch.Components.Parser;

/// <summary>
/// 官方游戏配置解析器
/// </summary>
/// <remarks>
/// 取自 launcher_profile.json
/// </remarks>
public sealed class DefaultLauncherProfileParser : IDataProcessor {
    private readonly Guid _clientToken;
    private string _filePath;

    private LauncherProfileEntry _launcherProfile;
    public Dictionary<string, object> Datas { get; set; } = [];

    public DefaultLauncherProfileParser(Guid clientToken = default) {
        _clientToken = clientToken;
    }

    public void Handle(object data) {
        Datas.Clear();
        if (data is IEnumerable<MinecraftEntry> minecrafts && minecrafts.Any()) {
            _filePath = Path.Combine(minecrafts.First()?.MinecraftFolderPath, "launcher_profiles.json");
            if (File.Exists(_filePath)) {
                var launcherProfileJson = File.ReadAllText(_filePath, Encoding.UTF8);
                _launcherProfile = launcherProfileJson.Deserialize(new LauncherProfileEntryContext(
                    JsonSerializerUtil.GetDefaultOptions()).LauncherProfileEntry);
            }

            foreach (var minecraft in minecrafts) {
                _launcherProfile ??= new() {
                    Profiles = [],
                    ClientToken = _clientToken.ToString("N"),
                    LauncherVersion = new LauncherVersionEntry {
                        Format = 6,
                        Name = "MinecraftLaunch"
                    }
                };

                if (!_launcherProfile.Profiles.ContainsKey(minecraft.Id))
                    _launcherProfile.Profiles.Add(minecraft.Id, new() {
                        Type = "custom",
                        Name = minecraft.Id,
                        Created = DateTime.Now,
                        LastVersionId = minecraft.Id,
                        GameFolder = minecraft.ToWorkingPath(true),
                        Resolution = new()
                    });
            }

            Datas = _launcherProfile.Profiles
                .ToDictionary(x => x.Key, x1 => x1.Value as object);
        }
    }

    public Task SaveAsync(CancellationToken cancellationToken = default) {
        _launcherProfile.Profiles = Datas.ToDictionary(x => x.Key, x1 => x1.Value as GameProfileEntry);
        var json = _launcherProfile?.Serialize(new LauncherProfileEntryContext(
            JsonSerializerUtil.GetDefaultOptions()).LauncherProfileEntry);

        return File.WriteAllTextAsync(_filePath, json, cancellationToken);
    }
}