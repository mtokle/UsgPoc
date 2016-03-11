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
    public class MockMetadataManager : ISupplierMetadataManager
    {
        public List<SupplierMetaData> GetSupplierMetadata(List<int> supplierIds)
        {
            Thread.Sleep(500);
            return new List<SupplierMetaData>();
        }
    }
}
