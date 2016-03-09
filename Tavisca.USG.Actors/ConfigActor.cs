using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tavisca.USG.Interfaces;

namespace Tavisca.USG.Actors
{
    public class ConfigActor : ReceiveActor
    {
        ITenantConfigManager _configManager = null;
        public ConfigActor(ITenantConfigManager configManager)
        {
            _configManager = configManager;
            Receives();
        }
        private void Receives()
        {
            Receive<List<int>>(supplierIds =>
            {
                var supplierInfoList = _configManager.GetHotelSuppliers(supplierIds);
                Sender.Tell(supplierInfoList);
            });
        }
    }
}
