using Akka.Actor;
using Akka.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tavisca.USG.Actors;
using Tavisca.USG.Interfaces;
using Tavisca.USG.Providers;
using Topshelf;

namespace Tavisca.USG.ServiceConsole
{
    class UsgService : ServiceControl
    {
        public bool Start(HostControl hostControl)
        {
            ActorSystem ActorSystem = ActorSystem.Create("usg");
            ITenantConfigManager _configManager = new MockTenantConfigManager();
            ISupplierMetadataManager _metadataManager = new MockMetadataManager();
            IHotelContentManager _contentManager = new MockHotelContentManager();
            IHotelConnectorFactory _connectorFactory = new MockHotelConnectorFactory();
            IResultStoreManager _resultStoreManager = new MockResultStoreManager();
            SystemActors.SearchQueueActor = ActorSystem.ActorOf(Props.Create(() => new SearchQueueActor(_configManager, _metadataManager, _contentManager, _connectorFactory, _resultStoreManager)), "searchqueueactor");
            //SystemActors.SearchBroadcastActor = ActorSystem.ActorOf(Props.Create(() => new SearchBroadcastActor(new MockHotelConnectorFactory())), "broadcaster"); //local connector actor
            SystemActors.SearchBroadcastActor = ActorSystem.ActorOf(Props.Empty.WithRouter(FromConfig.Instance), "supplier"); //remote actor
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
