using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tavisca.USG.Interfaces;

namespace Tavisca.USG.Actors
{
    public class HotelSearchRequestQueueHandler : ReceiveActor
    {
        ITenantConfigManager _configManager = null;
        ISupplierMetadataManager _metadataManager = null;
        IHotelContentManager _contentManager = null;
        IHotelConnectorFactory _connectorFactory = null;
        IResultStoreManager _resultStoreManager = null;
        private HotelSearchMessage _hotelSearchMessage = null;
        public HotelSearchRequestQueueHandler(ITenantConfigManager configManager, ISupplierMetadataManager metadataManager, IHotelContentManager contentManager, IHotelConnectorFactory connectorFactory, IResultStoreManager resultStoreManager)
        {
            _metadataManager = metadataManager;
            _contentManager = contentManager;
            _connectorFactory = connectorFactory;
            _resultStoreManager = resultStoreManager;
            _configManager = configManager;

            Receive<HotelSearchMessage>(searchRequest =>
            {
                var hotelSearchActor = Context.ActorOf(Props.Create(() => new HotelSearchActor(_configManager, _metadataManager, 
                    _contentManager, _connectorFactory, _resultStoreManager)));
                hotelSearchActor.Tell(searchRequest);
            });
        }
    }
}
