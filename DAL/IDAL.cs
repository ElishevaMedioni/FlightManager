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
        FlightRoot GetCurrentFlight(string key);
        bool ClearAllFlights();

        #endregion Flight

        #region Weather
        BE.WeatherRoot GetOneFlightWeather(string latitude, string longitude);
        #endregion

        #region Date
        bool GetHoliday(DateTime dateTime);
        DateRoot GetDate(DateTime dateTime);
        #endregion

        #region History
        void AddFlightToHistoryDb(BE.FlightInfoPartial flight);
        BE.FlightInfoPartial GetFlight(Func<BE.FlightInfoPartial, bool> predicate = null);

        List<FlightInfoPartial> GetAllFlightsFromDB(Func<FlightInfoPartial, bool> predicate = null);
        void DeleteFlightsFromDB(int IdKey);


        #endregion

    }
}
