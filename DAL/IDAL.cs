using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IDAL
    {
        #region Flight
        IEnumerable<BE.FlightInfoPartial> GetIncomingFlights();
        IEnumerable<BE.FlightInfoPartial> GetOutgoingFlights();
       FlightRoot GetOneflight(string key);

        bool ClearAllFlights ();


        #endregion Flight

        #region Weather
        BE.WeatherRoot GetOneflightWeather(string latitude, string longitude);
        #endregion

        #region Date
        bool GetHoliday(DateTime dateTime);
        #endregion



    }
}
