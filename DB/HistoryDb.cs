using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DB
{
    public class HistoryDb:DbContext
    {
        public DbSet<FlightInfoPartial> HistoryFlights { get; set; }

        public HistoryDb() : base("HistoryDb")
        {
            
        }

        


    }
}
