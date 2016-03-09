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
        public void StartBackgroundTask(string sessionId, HotelSearchCriteria criteria)
        {
            SystemActors.HotelSearchActor.Tell(new HotelSearchMessage() { SessionId = sessionId, SearchCriteria = criteria });
        }
    }
}
