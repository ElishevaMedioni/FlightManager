using BE;
using DAL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace BLL
{
    public class BLImp:IBL
    {
   
        HelperClass Helper = new HelperClass();

        Dictionary<string, IEnumerable<BE.FlightInfoPartial>> flightOutAndIn = new Dictionary<string, IEnumerable<BE.FlightInfoPartial>>(); //Belongs to BL
        public IDAL dal { get; set; }
        public BLImp()
        {
            dal = new DALImp();
        }


        #region Flight
        public Dictionary<string, IEnumerable<BE.FlightInfoPartial>>GetCurrentFlights()
        {
            IEnumerable<FlightInfoPartial> Incoming = dal.GetIncomingFlights();
            Incoming = Incoming.Where(o => dal.GetCurrentFlight(o.SourceId) != null).ToList();
            IEnumerable<FlightInfoPartial> Outgoing = dal.GetOutgoingFlights();
            Outgoing = Outgoing.Where(o => dal.GetCurrentFlight(o.SourceId) != null).ToList();

            flightOutAndIn.Add("Incoming", Incoming);
            flightOutAndIn.Add("Outgoing", Outgoing);

            return flightOutAndIn;
        }
       
        public FlightRoot GetFlightData(String Key)
        {

            return dal.GetCurrentFlight(Key);
        }
        
        public void SaveFlightToDB(BE.FlightInfoPartial Flight)
        {
            if ((dal.GetFlight(x => x.Id == Flight.Id)) == null)
            {

                dal.AddFlightToHistoryDb(Flight);

            }
        }

        public List<FlightInfoPartial> GetAllFlightInfoPartial(Func<FlightInfoPartial, bool> predicate = null)
            => dal.GetAllFlightsFromDB(predicate);

        public bool ClearAllListFlights()
        {
            flightOutAndIn.Clear();
            return dal.ClearAllFlights();
        }

        #endregion Flight

        #region Weather
        public WeatherRoot GetWeatherWithLatLong(string latitude, string longitude)
        {
            WeatherRoot Info = null;

            try
            {
                Info = dal.GetOneFlightWeather(latitude, longitude);

                Info.sys.sunsetDate = Helper.GetTimeFromEpoch(Convert.ToDouble(Info.sys.sunset));
                Info.sys.sunriseDate = Helper.GetTimeFromEpoch(Convert.ToDouble(Info.sys.sunrise));
                Info.main.temp = Helper.GetTemp(Convert.ToDouble(Info.main.temp));
                Info.main.feels_like = Helper.GetTemp(Convert.ToDouble(Info.main.feels_like));
                Info.main.temp_min = Helper.GetTemp(Convert.ToDouble(Info.main.temp_min));
                Info.main.temp_max = Helper.GetTemp(Convert.ToDouble(Info.main.temp_max));
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            return Info;

        }


        #endregion Weather 

        #region Date
        public bool GetHoliday()
        {
            DateTime start = DateTime.Now;
            bool flag = false;

            for (int i = 0; i < 6; i++)
            {
                flag = dal.GetHoliday(start.AddDays(i));
                if (flag == true)
                {
                   return true;
                }
            }
            return false;

        }

        public DateRoot GetDate(DateTime date)
        {
            return dal.GetDate(date);
        }

        #endregion Date

    }
}
