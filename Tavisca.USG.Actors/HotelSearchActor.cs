using Akka.Actor;
using Akka.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tavisca.USG.Entities;
using Tavisca.USG.Interfaces;

namespace Tavisca.USG.Actors
{
    public class HotelSearchActor : ReceiveActor
    {
        ITenantConfigManager _configManager = null;
        ISupplierMetadataManager _metadataManager = null;
        IHotelContentManager _contentManager = null;
        IHotelConnectorFactory _connectorFactory = null;
        IResultStoreManager _resultStoreManager = null;
        private HotelSearchMessage _hotelSearchMessage = null;
        IActorRef _requester = ActorRefs.Nobody;
        public HotelSearchActor(ITenantConfigManager configManager, ISupplierMetadataManager metadataManager, IHotelContentManager contentManager, IHotelConnectorFactory connectorFactory, IResultStoreManager resultStoreManager)
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
                _hotelSearchMessage = message;
                //Work flow :
                //Get supplier configuration detailed information 
                var configActor = Context.ActorOf(Props.Create(() => new ConfigActor(_configManager)));
                configActor.Tell(message.SearchCriteria.SupplierIds);

                //Get metadata for each supplier
                var metadataActor = Context.ActorOf(Props.Create(() => new MetadataActor(_metadataManager)));
                metadataActor.Tell(message.SearchCriteria.SupplierIds);

                //Get supplier hotels ids for each supplier from clarifi
                var contentActor = Context.ActorOf(Props.Create(() => new HotelContentActor(_contentManager)));
                contentActor.Tell(message.SearchCriteria.SupplierIds);

                //Make search on each supplier
                //save the results into result store
            });

            Receive<List<SupplierInfo>>(supplierInfoList =>
            {
                _hotelSearchMessage.SupplierList = supplierInfoList;
                BroadCastSearchMessage();
            });

            Receive<List<SupplierMetaData>>(supplierMetadataList =>
            {
                _hotelSearchMessage.SupplierMetadataList = supplierMetadataList;
                BroadCastSearchMessage();
            });

            Receive<Dictionary<int, List<HotelMapping>>>(hotelMappings =>
            {
                _hotelSearchMessage.SupplierHotelMappings = hotelMappings;
                BroadCastSearchMessage();
            });
        }

        private void BroadCastSearchMessage()
        {
            // send search request to connectors if we have all required information to create supplier specific search request
            if(_hotelSearchMessage.SupplierHotelMappings!=null && _hotelSearchMessage.SupplierList != null && _hotelSearchMessage.SupplierMetadataList != null)
            {
                SystemActors.SearchBroadcastActor.Tell(_hotelSearchMessage);
            }
        }
    }
}
