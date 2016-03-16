using Akka.Actor;
using Akka.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Tavisca.USG.Actors;
using Tavisca.USG.Interfaces;
using Tavisca.USG.Providers;
using Tavisca.USG.ServiceImpl;

namespace Tavisca.USG.Services
{
    public class Global : System.Web.HttpApplication
    {
        protected static ActorSystem ActorSystem;
        protected void Application_Start(object sender, EventArgs e)
        {
            ActorSystem = ActorSystem.Create("usg");
            SystemActors.UsgActorSystem = ActorSystem;
            //SystemActors.SearchBroadcastActor = ActorSystem.ActorOf(Props.Create(() => new SearchBroadcastActor(new MockHotelConnectorFactory())), "broadcaster"); //local connector actor
            SystemActors.SearchQueueActor = ActorSystem.ActorOf(Props.Empty.WithRouter(FromConfig.Instance), "hotelsearchhandler");
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}