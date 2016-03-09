using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tavisca.USG.Entities;
using Tavisca.USG.Interfaces;

namespace Tavisca.USG.Providers
{
    public class MockTenantConfigManager : ITenantConfigManager
    {
        public List<SupplierInfo> GetHotelSuppliers(List<int> supplierIds)
        {
            List<SupplierInfo> suppliers = new List<SupplierInfo>();
            foreach (var supplierId in supplierIds)
            {
                suppliers.Add(new SupplierInfo()
                {
                    Id = supplierId,
                    Family = "Supplier" + supplierId,
                });
            }
            return suppliers;
        }
    }
}
