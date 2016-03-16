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
        IResultStoreManager _resultStoreManager = null;
        public AkkaTaskManager(IResultStoreManager resultStoreManager)
        {
            _resultStoreManager = resultStoreManager;
        }

        public void StartBackgroundTask(string sessionId, HotelSearchCriteria criteria)
        {
            var hotelSearchActor = SystemActors.UsgActorSystem.ActorOf(Props.Create(() => new SearchRequestHandler(_resultStoreManager)));
            hotelSearchActor.Tell(new HotelSearchMessage() { SessionId = sessionId, SearchCriteria = criteria });
        }
    }
}
