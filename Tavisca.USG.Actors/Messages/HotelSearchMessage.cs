using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tavisca.USG.Entities;

namespace Tavisca.USG.Actors
{
    public class HotelSearchMessage
    {
        public HotelSearchCriteria SearchCriteria { get; set; }
        public List<SupplierInfo> SupplierList { get; set; }
        public List<SupplierMetaData> SupplierMetadataList { get; set; }
        public Dictionary<int, List<HotelMapping>> SupplierHotelMappings { get; set; }
        public string SessionId { get; set; }
    }
}
