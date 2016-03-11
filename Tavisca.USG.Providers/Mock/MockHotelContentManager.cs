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
    public class MockHotelContentManager : IHotelContentManager
    {

        public List<HotelMapping> GetHotelContentMapping(int supplierId)
        {
            var mappings = new List<HotelMapping>();
            for (int i = 0; i < 100; i++)
            {
                mappings.Add(new HotelMapping()
                {
                    ClarifiId = i + 1,
                    SupplierHotelId = "S" + i,
                    SupplierId = supplierId,
                });
            }
            Thread.Sleep(500);
            return mappings;
        }
    }
}
