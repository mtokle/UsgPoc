using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace Tavisca.USG.ConnectorConsole
{
    public class Program
    {
        static int Main(string[] args)
        {
            return (int)HostFactory.Run(x =>
            {
                x.SetServiceName("Connector");
                x.SetDisplayName("Akka.NET Connector");
                x.SetDescription("Akka.NET Cluster Demo - connector.");

                x.UseAssemblyInfoForServiceInfo();
                x.RunAsLocalSystem();
                x.StartAutomatically();
                //x.UseNLog();
                x.Service<ConnectorService>();
                x.EnableServiceRecovery(r => r.RestartService(1));
            });
        }
    }
}
