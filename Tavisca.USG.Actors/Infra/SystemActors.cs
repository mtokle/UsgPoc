using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tavisca.USG.Actors
{
    public static class SystemActors
    {
        public static IActorRef HotelSearchActor = ActorRefs.Nobody;
        public static IActorRef SearchBroadcastActor = ActorRefs.Nobody;
    }
}
