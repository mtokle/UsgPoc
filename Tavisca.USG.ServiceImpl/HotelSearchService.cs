using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tavisca.USG.Entities;
using Tavisca.USG.Interfaces;
using Tavisca.USG.Providers;
using Tavisca.USG.ServiceImpl.DataContracts;
using Tavisca.USG.ServiceImpl.Providers;
using Tavisca.USG.ServiceImpl.ServiceContracts;
using Tavisca.USG.ServiceImpl.Translators;

namespace Tavisca.USG.ServiceImpl
{
    public class HotelSearchService : IHotelSearchService
    {
        HotelSearchProvider _searchProvider = null;
        public HotelSearchService()
        {
            //TODO:Use DI to inject depedancies
            _searchProvider = new HotelSearchProvider(new MockSessionStateManager(), new MockResultStoreManager(), null);
        }

        public SearchInitRs InitSearch(SearchInitRq request)
        {
            var response = new SearchInitRs();
            try
            {
                response.SessionId = _searchProvider.SearchInit(GetHotelSearchCriteria(request));
            }
            catch(Exception ex)
            {
                //Handle exception
            }
            return response;
        }

        public SearchResultRs GetResults(string sessionId)
        {
            var response = new SearchResultRs();
            try
            {
                var hotels = _searchProvider.GetResults(sessionId);
                response.Hotels = hotels.ToDataContract();
            }
            catch (Exception ex)
            {
                //Handle exception
            }
            return response;
        }

        private HotelSearchCriteria GetHotelSearchCriteria(SearchInitRq request)
        {
            return new HotelSearchCriteria()
            {

            };
        }
    }
}
