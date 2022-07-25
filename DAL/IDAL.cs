using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IDAL
    {
        #region FlightInfoPartial
        IEnumerable<BE.FlightInfoPartial> GetIncomingFlights();
        IEnumerable<BE.FlightInfoPartial> GetOutgoingFlights();


        #endregion FlightInfoPartial

    }
}
