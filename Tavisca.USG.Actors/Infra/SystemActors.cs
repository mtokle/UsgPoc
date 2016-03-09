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
        public static IActorRef USGService = ActorRefs.Nobody;
        public static IActorRef ConfigActor = ActorRefs.Nobody;
        public static IActorRef MetadataActor = ActorRefs.Nobody;
        public static IActorRef HotelContentActor = ActorRefs.Nobody;
        public static IActorRef ConnectorHotelSearchActor = ActorRefs.Nobody;
    }
}
