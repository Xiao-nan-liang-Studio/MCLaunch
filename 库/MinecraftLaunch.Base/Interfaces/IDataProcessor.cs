namespace MinecraftLaunch.Base.Interfaces;

public interface IDataProcessor {
    Dictionary<string, object> Datas { get; set; }

    void Handle(object data);
    Task SaveAsync(CancellationToken cancellationToken = default);
}