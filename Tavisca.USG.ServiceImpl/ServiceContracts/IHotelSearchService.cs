using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using Tavisca.USG.ServiceImpl.DataContracts;

namespace Tavisca.USG.ServiceImpl.ServiceContracts
{
    [ServiceContract(Name = "IHotelSearchService")]
    public interface IHotelSearchService
    {
        [OperationContract]
        [WebInvoke(UriTemplate = "/InitSearch", ResponseFormat = WebMessageFormat.Json)]
        SearchInitRs InitSearch(SearchInitRq request);

        [OperationContract]
        [WebInvoke(UriTemplate = "/GetResults", ResponseFormat = WebMessageFormat.Json)]
        SearchResultRs GetResults(string sessionId);
    }
}
