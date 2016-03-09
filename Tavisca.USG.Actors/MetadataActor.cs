using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tavisca.USG.Interfaces;

namespace Tavisca.USG.Actors
{
    public class MetadataActor : ReceiveActor
    {
        ISupplierMetadataManager _metadataManager = null;

        public MetadataActor(ISupplierMetadataManager metadataManager)
        {
            _metadataManager = metadataManager;
            Receives();
        }

        private void Receives()
        {
            Receive<List<int>>(supplierIds =>
            {
                var supplierMetadataList = _metadataManager.GetSupplierMetadata(supplierIds);
                Sender.Tell(supplierMetadataList);
            });
        }
    }
}
