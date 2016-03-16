using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tavisca.USG.Interfaces;

namespace Tavisca.USG.Actors
{
    public class SearchQueueActor : ReceiveActor
    {
        ITenantConfigManager _configManager = null;
        ISupplierMetadataManager _metadataManager = null;
        IHotelContentManager _contentManager = null;
        IHotelConnectorFactory _connectorFactory = null;
        IResultStoreManager _resultStoreManager = null;
        public SearchQueueActor(ITenantConfigManager configManager, ISupplierMetadataManager metadataManager, IHotelContentManager contentManager, IHotelConnectorFactory connectorFactory, IResultStoreManager resultStoreManager)
        {
            _metadataManager = metadataManager;
            _contentManager = contentManager;
            _connectorFactory = connectorFactory;
            _resultStoreManager = resultStoreManager;
            _configManager = configManager;
            Receives();
        }

        private void Receives()
        {
            Receive<HotelSearchMessage>(message =>
            {
                Console.WriteLine("search message received");
                var hotelSearchActor = Context.ActorOf(Props.Create(() => new HotelSearchActor(_configManager, _metadataManager, _contentManager, _connectorFactory, _resultStoreManager)));
                hotelSearchActor.Tell(new HotelSearchMessage() { SessionId = message.SessionId, SearchCriteria = message.SearchCriteria, RequestOriginator = Sender });
            });
        }
    }
}
