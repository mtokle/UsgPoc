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
    public class SearchRequestHandler : ReceiveActor
    {
        IResultStoreManager _resultStoreManager;
        HotelSearchMessage _hotelSearchMessage;
        public SearchRequestHandler(IResultStoreManager resultStoreManager)
        {
            _resultStoreManager = resultStoreManager;
            Receives();
        }

        private void Receives()
        {
            Receive<HotelSearchMessage>(message =>
            {
                _hotelSearchMessage = message;
                SystemActors.SearchQueueActor.Tell(message);
            });

            Receive<List<Hotel>>(hotels =>
            {
                _resultStoreManager.Save(_hotelSearchMessage.SessionId, hotels);
            });
        }
    }
}
