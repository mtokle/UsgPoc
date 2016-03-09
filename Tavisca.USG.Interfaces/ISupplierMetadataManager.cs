using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tavisca.USG.Entities;

namespace Tavisca.USG.Interfaces
{
    public interface ISupplierMetadataManager
    {
        List<SupplierMetaData> GetSupplierMetadata(List<int> supplierIds);
    }
}
