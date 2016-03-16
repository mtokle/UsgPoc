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
    public class MockHotelConnector : IHotelConnector
    {
        public List<Hotel> Search(SupplierInfo supplier, SupplierMetaData metadata, List<HotelMapping> hotelMappings)
        {
            List<Hotel> hotels = new List<Hotel>();
            foreach (var mapping in hotelMappings)
            {
                hotels.Add(new Hotel()
                {
                    SupplierId = mapping.SupplierId,
                    SupplierHotelId = mapping.SupplierHotelId,
                    ClarifiId = mapping.ClarifiId
                });
            }
            Thread.Sleep(1000);
            return hotels;
        }
    }
}
