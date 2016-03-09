using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tavisca.USG.Actors
{
    public class TestActor : ReceiveActor
    {
        private int actorCounter = 0;
        public TestActor()
        {
            actorCounter++;
            Receives();
        }

        private void Receives()
        {
            Receive<string>(message =>
            {
                Console.WriteLine(actorCounter);
            });
        }
    }
}
