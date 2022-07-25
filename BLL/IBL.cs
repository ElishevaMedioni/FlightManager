using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public interface IBL
    {
        #region FlightInfoPartial

        Dictionary<string, IEnumerable<BE.FlightInfoPartial>> GetCurrentFlights();

        FlightRoot GetFlightData(String Key);


        #endregion FlightInfoPartial
    }
}
