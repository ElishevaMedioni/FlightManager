﻿using BE;
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
        private const string FlightURL = @"https://data-live.flightradar24.com/clickhandler/?version=1.5&flight=";
        private const string APIKey = "16e42275ad992efba2e0fd36ff522dc4";
        HelperClass Helper = new HelperClass();

        Dictionary<string, IEnumerable<BE.FlightInfoPartial>> Result = new Dictionary<string, IEnumerable<BE.FlightInfoPartial>>(); //Belongs to BL
        public IDAL dal { get; set; }
        public BLImp()
        {
            dal = new DALImp();
        }


        #region FlightInfoPartial
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

            var CurrentUrl = FlightURL + Key;
            FlightRoot CurrentFlight = null;

            //MUST use try- catch
            using (var webClient = new System.Net.WebClient())
            {
                var json = webClient.DownloadString(CurrentUrl);
                try
                {
                    CurrentFlight = (FlightRoot)Newtonsoft.Json.JsonConvert.DeserializeObject(json, typeof(FlightRoot));
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
            }
            return CurrentFlight;
        }


        #endregion FlightInfoPartial

        #region Weather
        public WeatherRoot GetWeatherWithLatLong(string latitude, string longitude)
        {
            using (WebClient web = new WebClient())
            {

                string urlweater = string.Format("https://api.openweathermap.org/data/2.5/weather?lat={0}&lon={1}&appid={2}", latitude, longitude, APIKey);
                WeatherRoot Info = null;
                
                try
                {
                    var json = web.DownloadString(urlweater);
                    Info = JsonConvert.DeserializeObject<BE.WeatherRoot>(json);
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
        }


        #endregion Weather 

        #region date
        public bool GetHoliday(DateTime date)
        {
            using (var webClient = new System.Net.WebClient())
            {
                var yyyy = date.ToString("yyyy");
                var mm = date.ToString("MM");
                var dd = date.ToString("dd");
                string urldate = string.Format("https://www.hebcal.com/converter?cfg=json&date={0}-{1}-{2}&g2h=1&strict=1", yyyy, mm, dd);


                var json = webClient.DownloadString(urldate);
                Root Data = JsonConvert.DeserializeObject<Root>(json);
                Data.events.ForEach(Console.WriteLine);
                var matchingvalues = Data.events.FirstOrDefault(stringToCheck => stringToCheck.Contains("Erev"));

                if (matchingvalues != null)
                    return true;
                else return false;
            }
        }

        #endregion date
       
    }
}
