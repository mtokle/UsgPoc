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
    public class HotelContentActor : ReceiveActor
    {
        IHotelContentManager _contentManager = null;
        public HotelContentActor(IHotelContentManager contentManager)
        {
            _contentManager = contentManager;
            Receives();
        }

        private void Receives()
        {
            Receive<List<int>>(supplierIds =>
            {
                Dictionary<int, List<HotelMapping>> hotelMappings = new Dictionary<int, List<HotelMapping>>();
                foreach(var supplierId in supplierIds)
                {
                    hotelMappings[supplierId] = _contentManager.GetHotelContentMapping(supplierId);
                }
                Sender.Tell(hotelMappings);
            });
        }
    }
}
