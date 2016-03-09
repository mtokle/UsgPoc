using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tavisca.USG.Interfaces
{
    public interface IHotelConnectorFactory
    {
        IHotelConnector GetHotelConnectorInstance(int supplierId);
    }
}
