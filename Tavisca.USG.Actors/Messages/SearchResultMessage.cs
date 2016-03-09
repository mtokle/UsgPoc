using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tavisca.USG.Entities;

namespace Tavisca.USG.Actors
{
    public class SearchResultMessage
    {
        public IActorRef SourceActor { get; set; }
        public List<Hotel> Hotels { get; set; }
    }
}
