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

        #region Weather
        WeatherRoot GetWeatherWithLatLong(string latitude, string longitude);

        #endregion Weather

        #region date
        bool GetHoliday(DateTime today);

        #endregion date
    }
}
