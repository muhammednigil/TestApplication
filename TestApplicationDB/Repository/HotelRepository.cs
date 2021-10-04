using System;
using System.Collections.Generic;
using System.Text;
using TestApplicationDomain.Entities;
using TestApplicationInterface.Repository;

namespace TestApplicationDB.Repository
{
    public class HotelReposiotry : Repository<Hotel>, IHotelRepository
    {
        public HotelReposiotry(DbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
