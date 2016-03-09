using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tavisca.USG.Entities;
using Tavisca.USG.Interfaces;

namespace Tavisca.USG.Actors
{
    public class ConnectorSearchActor : ReceiveActor
    {
        IHotelConnector _hotelConnector = null;
        public ConnectorSearchActor(IHotelConnector hotelConnector)
        {
            _hotelConnector = hotelConnector;
            Receives();
        }

        private void Receives()
        {
            Receive<SupplierSearchMessage>(message =>
            {
                List<Hotel> hotels = _hotelConnector.Search(message.Supplier, message.Metadata, message.HotelMappings);
                Sender.Tell(new SearchResultMessage() { SourceActor = message.SourceActor, Hotels = hotels });
            });
        }
    }
}
