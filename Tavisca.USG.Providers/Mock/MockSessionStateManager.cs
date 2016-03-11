using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tavisca.USG.Entities;
using Tavisca.USG.Interfaces;

namespace Tavisca.USG.Providers
{
    public class MockSessionStateManager : ISessionStateManager
    {
        public SearchSession SaveHotelSearchRequest(HotelSearchCriteria criteria)
        {
            Thread.Sleep(500);
            return new SearchSession() { SessionId = Guid.NewGuid().ToString() };
        }
    }
}
