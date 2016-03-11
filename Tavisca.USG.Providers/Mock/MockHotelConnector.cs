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
    public class MockHotelConnector : IHotelConnector
    {
        public List<Hotel> Search(SupplierInfo supplier, SupplierMetaData metadata, List<HotelMapping> hotelMappings)
        {
            int sleepTime = StaticRandom.Instance.Next(1, 3) * 1000;
            //Console.WriteLine("{0}|at {1:hh.mm.ss.ffffff}", sleepTime, DateTime.Now);

            List<Hotel> hotels = new List<Hotel>();
            foreach (var mapping in hotelMappings)
            {
                hotels.Add(new Hotel()
                {
                    SupplierId = mapping.SupplierId,
                    SupplierHotelId = mapping.SupplierHotelId,
                    ClarifiId = mapping.ClarifiId
                });
            }
            
            Thread.Sleep(sleepTime);
            return hotels;
        }
    }

    public static class StaticRandom
    {
        private static int seed;

        private static ThreadLocal<Random> threadLocal = new ThreadLocal<Random>
            (() => new Random(Interlocked.Increment(ref seed)));

        static StaticRandom()
        {
            seed = Environment.TickCount;
        }

        public static Random Instance { get { return threadLocal.Value; } }
    }
}
