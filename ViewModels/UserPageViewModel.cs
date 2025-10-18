using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCLaunch.ViewModels
{
    internal class UserPageViewModel
    {
        public string Message { get; set; } = "Hello from UserPageViewModel!";

        public override string ToString() => Message;
    }
}
