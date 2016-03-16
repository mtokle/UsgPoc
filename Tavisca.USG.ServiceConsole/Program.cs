using Akka.Actor;
using Akka.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tavisca.USG.Actors;
using Tavisca.USG.Entities;
using Tavisca.USG.Interfaces;
using Tavisca.USG.Providers;
using Tavisca.USG.ServiceImpl.Providers;
using Topshelf;

namespace Tavisca.USG.ServiceConsole
{
    class Program
    {
        static int Main(string[] args)
        {
            return (int)HostFactory.Run(x =>
            {
                x.SetServiceName("USGWindowsService");
                x.SetDisplayName("Akka.NET USG");
                x.SetDescription("Akka.NET USG Demo.");

                x.UseAssemblyInfoForServiceInfo();
                x.RunAsLocalSystem();
                x.StartAutomatically();
                //x.UseNLog();
                x.Service<UsgService>();
                x.EnableServiceRecovery(r => r.RestartService(1));
            });
        }
    }
}
