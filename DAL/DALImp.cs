using BE;
using DB;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.History;
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

        public FlightRoot GetCurrentFlight(string key)
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
        public BE.WeatherRoot GetOneFlightWeather(string latitude, string longitude)
        {
            return DataSource.GetWeatherData(latitude, longitude);
        }
        #endregion

        #region Date
        public bool GetHoliday(DateTime dateTime)
        {
            return DataSource.GetHolidayInit(dateTime);
        }
        public DateRoot GetDate(DateTime dateTime)
        {
            return DataSource.GetdateInit(dateTime);
        }

        #endregion

        #region History
        void IDAL.AddFlightToHistoryDb(BE.FlightInfoPartial flight)
        {
            using (var db = new HistoryDb())
            {
                db.HistoryFlights.Add(flight);
                db.SaveChanges();
            }
        }

        public BE.FlightInfoPartial GetFlight(Func<BE.FlightInfoPartial, bool> predicate = null)
        {
            using (var ctx = new HistoryDb())
            {
                return ctx.HistoryFlights.Where(predicate).FirstOrDefault();

            }

        }

        public List<FlightInfoPartial> GetAllFlightsFromDB(Func<FlightInfoPartial, bool> predicate = null)
        {
            using (var ctx = new HistoryDb())
            {
                if (predicate == null)
                {
                    return ctx.HistoryFlights.ToList();
                }
                else
                {

                    return ctx.HistoryFlights.Where(predicate).ToList();
                }
            }
        }
        #endregion

    }
}
