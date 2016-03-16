using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tavisca.USG.Actors;
using Tavisca.USG.Entities;
using Tavisca.USG.Interfaces;

namespace Tavisca.USG.ServiceImpl.Providers
{
    public class AkkaTaskManager : IBackgroundTaskManager
    {
        ITenantConfigManager _configManager = null;
        ISupplierMetadataManager _metadataManager = null;
        IHotelContentManager _contentManager = null;
        IHotelConnectorFactory _connectorFactory = null;
        IResultStoreManager _resultStoreManager = null;
        public AkkaTaskManager(ITenantConfigManager configManager, ISupplierMetadataManager metadataManager, IHotelContentManager contentManager, IHotelConnectorFactory connectorFactory, IResultStoreManager resultStoreManager)
        {
            _metadataManager = metadataManager;
            _contentManager = contentManager;
            _connectorFactory = connectorFactory;
            _resultStoreManager = resultStoreManager;
            _configManager = configManager;
        }

        public void StartBackgroundTask(string sessionId, HotelSearchCriteria criteria)
        {
            var hotelSearchActor = SystemActors.UsgActorSystem.ActorOf(Props.Create(() => new HotelSearchActor(_configManager, _metadataManager, _contentManager, _connectorFactory, _resultStoreManager)));
            hotelSearchActor.Tell(new HotelSearchMessage() { SessionId = sessionId, SearchCriteria = criteria });
        }
    }
}
