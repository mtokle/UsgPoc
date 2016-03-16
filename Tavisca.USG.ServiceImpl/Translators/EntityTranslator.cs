using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tavisca.USG.ServiceImpl.Translators
{
    public static class EntityTranslator
    {
        public static List<DataContracts.Hotel> ToDataContract(this List<Entities.Hotel> hotels)
        {
            if (hotels == null) return null;
            return hotels.ConvertAll(h => ToDataContract(h));
        }

        public static DataContracts.Hotel ToDataContract(this Entities.Hotel hotel)
        {
            if (hotel == null) return null;
            return new DataContracts.Hotel()
            {
                ClarifiId = hotel.ClarifiId,
                SupplierHotelId = hotel.SupplierHotelId,
                SupplierId = hotel.SupplierId,
            };
        }
    }
}
