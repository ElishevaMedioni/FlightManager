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

        Dictionary<string, IEnumerable<BE.FlightInfoPartial>> Result = new Dictionary<string, IEnumerable<BE.FlightInfoPartial>>(); //Belongs to BL
        public IDAL dal { get; set; }
        public BLImp()
        {
            dal = new DALImp();
        }


        #region Flight
        public Dictionary<string, IEnumerable<BE.FlightInfoPartial>>GetCurrentFlights()
        {
            IEnumerable<FlightInfoPartial> Incoming = dal.GetIncomingFlights();
            IEnumerable<FlightInfoPartial> Outgoing = dal.GetOutgoingFlights();

            Result.Add("Incoming", Incoming);
            Result.Add("Outgoing", Outgoing);

            return Result;
        }
       
        public FlightRoot GetFlightData(String Key)
        {

            
            return dal.GetOneflight(Key);
        }

        public bool ClearAllListFlights()
        {
            return dal.ClearAllFlights();
        }

        #endregion Flight

        #region Weather
        public WeatherRoot GetWeatherWithLatLong(string latitude, string longitude)
        {
            WeatherRoot Info = null;

            try
            {

                Info = dal.GetOneflightWeather(latitude, longitude);

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

            for (int i = 0; i < 7; i++)
            {
                flag = dal.GetHoliday(start.AddDays(i));
                if (flag == true)
                {
                   return true;
                }
            }
            return false;

        }

        #endregion Date
       
    }
}
