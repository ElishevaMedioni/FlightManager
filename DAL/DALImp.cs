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

        #region Flight
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

        public FlightRoot GetOneflight(string key)
        {

            return DataSource.GetFlightDataInit(key);
        }

        public bool ClearAllFlights()
        {
            DB.DataSource.Incoming.Clear();
            DB.DataSource.Outgoing.Clear(); 
            return true;
        }

        #endregion

        #region Weather
        public BE.WeatherRoot GetOneflightWeather(string latitude, string longitude)
        {
            return DataSource.GetWeatherData(latitude, longitude);
        }
        #endregion

        #region Date
        public bool GetHoliday(DateTime dateTime)
        {
            return DataSource.GetHolidayInit(dateTime);
        }
        #endregion



    }
}
