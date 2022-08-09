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
        #region Flight

        Dictionary<string, IEnumerable<BE.FlightInfoPartial>> GetCurrentFlights();

        FlightRoot GetFlightData(String Key);
        bool ClearAllListFlights();


        #endregion Flight

        #region Weather
        WeatherRoot GetWeatherWithLatLong(string latitude, string longitude);

        #endregion Weather

        #region Date
        bool GetHoliday();

        #endregion date
    }
}
