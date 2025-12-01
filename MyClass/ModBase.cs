using System.Security.Principal;

namespace MCLaunch.MyClass;

public class ModBase
{
    bool isAdmin()
    {
        var id = WindowsIdentity.GetCurrent();
        var principal = new WindowsPrincipal(id);
        return principal.IsInRole(WindowsBuiltInRole.Administrator);
    }
}