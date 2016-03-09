using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tavisca.USG.Interfaces;

namespace Tavisca.USG.Providers
{
    public class MockHotelConnectorFactory : IHotelConnectorFactory
    {
        public IHotelConnector GetHotelConnectorInstance(int supplierId)
        {
            return new MockHotelConnector();
        }
    }
}
