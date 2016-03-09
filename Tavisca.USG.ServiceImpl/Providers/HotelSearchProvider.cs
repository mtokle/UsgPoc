using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tavisca.USG.Actors;
using Tavisca.USG.Entities;
using Tavisca.USG.Interfaces;

namespace Tavisca.USG.ServiceImpl.Providers
{
    public class HotelSearchProvider
    {
        ISessionStateManager _stateManager = null;
        IResultStoreManager _resultStoreManager = null;
        IBackgroundTaskManager _taskManager = null;
        public HotelSearchProvider(ISessionStateManager sessionStateManager, IResultStoreManager resultStoreManager, IBackgroundTaskManager backgroundTaskManager)
        {
            _stateManager = sessionStateManager;
            _resultStoreManager = resultStoreManager;
            _taskManager = backgroundTaskManager;
        }

        public string SearchInit(HotelSearchCriteria criteria)
        {
            string sessionId = string.Empty;
            try
            {
                if (IsValidCriteria())
                {
                    // Store search criteria in session
                    var session = _stateManager.SaveHotelSearchRequest(criteria);
                    sessionId = session.SessionId;

                    // start background hotel search task
                    _taskManager.StartBackgroundTask(sessionId, criteria);
                }
            }
            catch (Exception ex)
            {
                //Handle exception
            }
            return sessionId;
        }

        public List<Hotel> GetResults(string sessionId)
        {
            List<Hotel> hotels = new List<Hotel>();
            try
            {
                if (IsValidSessionId(sessionId))
                {
                    hotels = _resultStoreManager.GetNextResults(sessionId);
                }
            }
            catch (Exception ex)
            {
                //Handle exception
            }
            return hotels;
        }

        private bool IsValidSessionId(string sessionId)
        {
            return true;
        }

        private bool IsValidCriteria()
        {
            return true;
        }
    }
}
