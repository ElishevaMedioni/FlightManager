using BE;
using DB;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALImp : IDAL
    {
        public DALImp() { }
        IEnumerable<FlightInfoPartial> IDAL.GetIncomingFlights()
        {
            {
                return from FlightInfoPartial in DB.DataSource.Incoming
                       select FlightInfoPartial.Clone();
            }
        }

        IEnumerable<FlightInfoPartial> IDAL.GetOutgoingFlights()
        {
            {
                return from FlightInfoPartial in DB.DataSource.Outgoing
                       select FlightInfoPartial.Clone();
            }
        }
    }
}
