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
    public class MockResultStoreManager : IResultStoreManager
    {
        public class ResultRow
        {
            public string SessionId { get; set; }
            public List<Hotel> Hotels { get; set; }
            public bool IsRead { get; set; }
        }

        private static List<ResultRow> _resultStore = new List<ResultRow>();
        public List<Hotel> GetNextResults(string sessionId)
        {
            var results = _resultStore.FindAll(r => string.Equals(sessionId, r.SessionId) && !r.IsRead);
            var hotels = new List<Hotel>();
            foreach(var row in results)
            {
                hotels.AddRange(row.Hotels);
                row.IsRead = true;
            }
            return hotels;
        }

        public bool Save(string sessionId, List<Hotel> hotels)
        {
            _resultStore.Add(new ResultRow() { Hotels = hotels, SessionId = sessionId });
            return true;
        }
    }
}
