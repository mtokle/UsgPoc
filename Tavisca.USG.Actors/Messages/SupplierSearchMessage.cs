using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tavisca.USG.Entities;

namespace Tavisca.USG.Actors
{
    public class SupplierSearchMessage
    {
        public string SessionId { get; set; }
        public SupplierInfo Supplier { get; set; }
        public SupplierMetaData Metadata { get; set; }
        public List<HotelMapping> HotelMappings { get; set; }
    }
}
