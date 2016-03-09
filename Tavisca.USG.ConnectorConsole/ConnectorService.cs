using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tavisca.USG.Actors;
using Tavisca.USG.Providers;
using Topshelf;

namespace Tavisca.USG.ConnectorConsole
{
    class ConnectorService : ServiceControl
    {
        public bool Start(HostControl hostControl)
        {
            ClusterSystem = ActorSystem.Create("usg");

            SystemActors.SearchBroadcastActor = ClusterSystem.ActorOf(Props.Create(() => new SearchBroadcastActor(new MockHotelConnectorFactory())), "broadcaster");
            return true;
        }

        protected ActorSystem ClusterSystem { get; set; }

        public bool Stop(HostControl hostControl)
        {
            ClusterSystem.Shutdown();
            return true;
        }
    }
}
