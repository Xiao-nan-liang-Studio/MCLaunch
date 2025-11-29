using MinecraftLaunch.Components.Provider;

namespace MCLaunch.MyClass;

public class Download
{
    public async Task CurseForge(string modName)
    { 
        CurseforgeProvider curseforgeProvider = new();
        foreach (var cfResource in await curseforgeProvider.SearchResourcesAsync(modName)) {
            Console.WriteLine("Id： " + cfResource.Id);
            Console.WriteLine("ClassId： " + cfResource.ClassId);
            Console.WriteLine("Name： " + cfResource.Name);
            Console.WriteLine("Summary： " + cfResource.Summary);
            Console.WriteLine("IconUrl： " + cfResource.IconUrl);
            Console.WriteLine("WebsiteUrl： " + cfResource.WebsiteUrl);
            Console.WriteLine("DownloadCount： " + cfResource.DownloadCount);
            Console.WriteLine("DateModified： " + cfResource.DateModified);
            Console.WriteLine("MinecraftVersions： " + string.Join('，', cfResource.MinecraftVersions));
            Console.WriteLine("Categories： " + string.Join('，', cfResource.Categories));
            Console.WriteLine("Authors： " + string.Join('，', cfResource.Authors));
            Console.WriteLine("Screenshots： " + string.Join('，', cfResource.Screenshots));
            Console.WriteLine("LatestFiles - FileName： " + string.Join('，', cfResource.LatestFiles.Select(x => x.FileName)));
            Console.WriteLine();
        }

        foreach (var cfResources in await curseforgeProvider.GetResourceFilesByFingerprintsAsync([568671043])) {
            var cfResource = cfResources.Key;

            Console.WriteLine("Id： " + cfResource.Id);
            Console.WriteLine("ModId： " + cfResource.ModId);
            Console.WriteLine("FileName： " + cfResource.FileName);
            Console.WriteLine("Published： " + cfResource.Published);
            Console.WriteLine("IsAvailable： " + cfResource.IsAvailable);
            Console.WriteLine("ReleaseType： " + cfResource.ReleaseType);
            Console.WriteLine("DownloadUrl： " + cfResource.DownloadUrl);
            Console.WriteLine("FileFingerprint： " + cfResource.FileFingerprint);
            Console.WriteLine("MinecraftVersions： " + string.Join('，', cfResource.MinecraftVersions));
            Console.WriteLine();
        }
    }
}