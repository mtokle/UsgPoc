using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tavisca.USG.Interfaces;

namespace Tavisca.USG.Actors
{
    public class SearchBroadcastActor : ReceiveActor
    {
        IHotelConnectorFactory _connectorFactory = null;
        public SearchBroadcastActor(IHotelConnectorFactory connectorFactory)
        {
            _connectorFactory = connectorFactory;
            Receives();
            Console.WriteLine("actor init");
        }

        private void Receives()
        {
            Console.WriteLine("init methods");
            Receive<HotelSearchMessage>(message =>
            {
                Console.WriteLine("Message received");
                foreach (var supplier in message.SupplierList)
                {
                    var hotelConnector = _connectorFactory.GetHotelConnectorInstance(supplier.Id);
                    var connector = Context.ActorOf(Props.Create(() => new ConnectorSearchActor(hotelConnector)));
                    connector.Tell(new SupplierSearchMessage() { Supplier = supplier, HotelMappings = message.SupplierHotelMappings[supplier.Id], Metadata = message.SupplierMetadataList.Find(m => m.SupplierId == supplier.Id), SessionId = message.SessionId, SourceActor = Sender });
                }
            });

            Receive<SearchResultMessage>(message =>
            {
                message.SourceActor.Tell(message.Hotels);
            });
        }
    }
}
