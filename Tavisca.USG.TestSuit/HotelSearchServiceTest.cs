using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using Tavisca.USG.TestSuit.Proxy;
using Tavisca.USG.ServiceImpl.DataContracts;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;

namespace Tavisca.USG.TestSuit
{
    [TestClass]
    public class HotelSearchServiceTest
    {
        private string _baseServiceUrl = string.Empty;
        public HotelSearchServiceTest()
        {
            _baseServiceUrl = ConfigurationManager.AppSettings["ServiceBaseUrl"];
        }

        [TestMethod]
        public void HotelSearchTest()
        {
            string url = string.Format("{0}/HotelSearchService.svc/InitSearch", _baseServiceUrl);
            HttpClient client = new HttpClient(url, ContentType.Json);
            SearchInitRq request = new SearchInitRq()
            {
                SupplierIds = new List<int> { 1, 2, 3},
            };
            SearchInitRs response = client.Post<SearchInitRq, SearchInitRs>(request);
            url = string.Format("{0}/HotelSearchService.svc/GetResults?sessionId={1}", _baseServiceUrl, response.SessionId);
            client = new HttpClient(url, ContentType.Json);
            int pollCount = 0;
            List<Hotel> hotels = new List<Hotel>();

            while (hotels.Count < (request.SupplierIds.Count * 100))
            {
                SearchResultRs searchResponse = client.Get<SearchResultRs>();
                hotels.AddRange(searchResponse.Hotels);
                pollCount++;
                if (pollCount > 100) break;
                Thread.Sleep(200); //result polling interval
            }
        }
    }
}
