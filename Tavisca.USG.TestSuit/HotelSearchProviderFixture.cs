using Akka.Actor;
using Akka.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tavisca.USG.Actors;
using Tavisca.USG.Entities;
using Tavisca.USG.Interfaces;
using Tavisca.USG.Providers;
using Tavisca.USG.ServiceImpl.Providers;

namespace Tavisca.USG.TestSuit
{
    [TestClass]
    public class HotelSearchProviderFixture
    {
        protected static ActorSystem ActorSystem;
        private HotelSearchProvider _searchProvider = null;
        public HotelSearchProviderFixture()
        {
            ActorSystem = ActorSystem.Create("usg");
            ITenantConfigManager _configManager = new MockTenantConfigManager();
            ISupplierMetadataManager _metadataManager = new MockMetadataManager();
            IHotelContentManager _contentManager = new MockHotelContentManager();
            IHotelConnectorFactory _connectorFactory = new MockHotelConnectorFactory();
            IResultStoreManager _resultStoreManager = new MockResultStoreManager();
            _searchProvider = new HotelSearchProvider(new MockSessionStateManager(), _resultStoreManager, new AkkaTaskManager());
            SystemActors.HotelSearchActor = ActorSystem.ActorOf(Props.Create(() => new HotelSearchActor(_configManager, _metadataManager, _contentManager, _connectorFactory, _resultStoreManager)));
            //SystemActors.SearchBroadcastActor = ActorSystem.ActorOf(Props.Create(() => new SearchBroadcastActor(new MockHotelConnectorFactory())), "broadcaster"); //local connector actor
            SystemActors.SearchBroadcastActor = ActorSystem.ActorOf(Props.Empty.WithRouter(FromConfig.Instance), "supplier");
        }

        [TestMethod]
        public void SearchInitTest()
        {
            var criteria = new HotelSearchCriteria()
            {
                SupplierIds = new List<int>() { 1, 2, 3 },
            };
            string sessionId = _searchProvider.SearchInit(criteria);
            var hotels = new List<Hotel>();
            for (int i = 1; i <= criteria.SupplierIds.Count; i++)
            {
                Thread.Sleep(5000); //result polling interval
                hotels.AddRange(_searchProvider.GetResults(sessionId));
                //Assert.IsTrue(hotels.Count == (i * 100)); // should get incremental results as each mock supplier takes 4000ms, 8000ms, 12000ms,... to return result
            }
        }

        [TestMethod]
        public void TestActor()
        {
            var ref1 = ActorSystem.ActorOf<TestActor>("act1"); // create new actor
            var ref2 = ActorSystem.ActorOf<TestActor>("act2"); // create new actor
            var ref3 = ActorSystem.ActorSelection("/user/act1"); // get the existing actor ref
            ref1.Tell("print");
            ref2.Tell("print");
            ref3.Tell("print");
            Thread.Sleep(30000);
        }

    }
}
