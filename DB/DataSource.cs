using BE;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;

namespace DB
{
    public static class DataSource
    {

        private const string AllURL = @"https://data-cloud.flightradar24.com/zones/fcgi/feed.js?faa=1&bounds=38.64%2C21.377%2C24.676%2C40.605&satellite=1&mlat=1&flarm=1&adsb=1&gnd=1&air=1&vehicles=1&estimated=1&maxage=14400&gliders=1&stats=1";

        private const string FlightURL = @"https://data-live.flightradar24.com/clickhandler/?version=1.5&flight=";
        private const string WeatherURL = @"https://api.openweathermap.org/data/2.5/weather?lat={0}&lon={1}&appid={2}";
        private const string dateURL = @"https://www.hebcal.com/converter?cfg=json&date={0}-{1}-{2}&g2h=1&strict=1";

        private const string APIKey = "16e42275ad992efba2e0fd36ff522dc4";

        public static List<BE.FlightInfoPartial> Incoming = new List<BE.FlightInfoPartial>();
        public static List<BE.FlightInfoPartial> Outgoing = new List<BE.FlightInfoPartial>();

        

        //פונקצית אתחול לכל רשימה
        static DataSource()
        {
            OutgoingFlightsInit();
            IncomingFlightsInit();
        }



        #region Flight
        public static void OutgoingFlightsInit()
        {
            JObject AllFlightData = null;
            using (var webClient = new System.Net.WebClient())
            {
                //worked with synchronous -->need to change to asynchronous
                var json = webClient.DownloadString(AllURL);

                HelperClass Helper = new HelperClass();
                AllFlightData = JObject.Parse(json);

                try
                {
                    foreach (var item in AllFlightData)
                    {
                        var key = item.Key;
                        if (key == "full_count") continue;
                        if (key == "version") continue;
                        if (item.Value[11].ToString() == "TLV")

                            Outgoing.Add(new FlightInfoPartial
                            {
                                Id = -1,
                                SourceFilter = item.Value[11].ToString(),
                                Destination = item.Value[12].ToString(),
                                SourceId = key,
                                Long = Convert.ToDouble(item.Value[1]),
                                Lat = Convert.ToDouble(item.Value[2]),
                                DateAndTime = Helper.GetTimeFromEpoch(Convert.ToDouble(item.Value[10])),
                                FlightCode = item.Value[13].ToString()
                            });

                    }

                }
                catch (Exception e)
                {
                    Debug.Print(e.Message);
                }


            }
        }

        public static void IncomingFlightsInit()
        {
            JObject AllFlightData = null;
            using (var webClient = new System.Net.WebClient())
            {
                //worked with synchronous -->need to change to asynchronous
                var json = webClient.DownloadString(AllURL);

                HelperClass Helper = new HelperClass();
                AllFlightData = JObject.Parse(json);

                try
                {
                    foreach (var item in AllFlightData)
                    {
                        var key = item.Key;
                        if (key == "full_count") continue;
                        if (key == "version") continue;

                        if (item.Value[12].ToString() == "TLV")
                            Incoming.Add(new FlightInfoPartial
                            {
                                Id = -1,
                                SourceFilter = item.Value[11].ToString(),
                                Destination = item.Value[12].ToString(),
                                SourceId = key,
                                Long = Convert.ToDouble(item.Value[1]),
                                Lat = Convert.ToDouble(item.Value[2]),
                                DateAndTime = Helper.GetTimeFromEpoch(Convert.ToDouble(item.Value[10])),
                                FlightCode = item.Value[13].ToString()

                            });

                    }

                }
                catch (Exception e)
                {
                    Debug.Print(e.Message);
                }
            }
        }
        public static FlightRoot GetFlightDataInit(String Key)
        {

            var CurrentUrl = FlightURL + Key;
            FlightRoot CurrentFlight = null;

            //MUST use try- catch
            using (var webClient = new System.Net.WebClient())
            {
                
                try
                {
                    var json = webClient.DownloadString(CurrentUrl);
                    CurrentFlight = (FlightRoot)Newtonsoft.Json.JsonConvert.DeserializeObject(json, typeof(FlightRoot));
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
            }
            return CurrentFlight;
        }

        #endregion

        #region Weather
        public static WeatherRoot GetWeatherData(string latitude, string longitude)
        {
            using (WebClient web = new WebClient())
            {

                string urlweater = string.Format(WeatherURL, latitude, longitude, APIKey);
                WeatherRoot Info = null;

                try
                {
                    var json = web.DownloadString(urlweater);
                    Info = JsonConvert.DeserializeObject<BE.WeatherRoot>(json);

                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }

                return Info;
            }
        }

        #endregion

        #region Date
        public static bool GetHolidayInit(DateTime date)
        {
            using (var webClient = new System.Net.WebClient())
            {
                var yyyy = date.ToString("yyyy");
                var mm = date.ToString("MM");
                var dd = date.ToString("dd");
                string urldate = string.Format(dateURL, yyyy, mm, dd);


                var json = webClient.DownloadString(urldate);
                Root Data = JsonConvert.DeserializeObject<Root>(json);
                Data.events.ForEach(Console.WriteLine);
                var matchingvalues = Data.events.FirstOrDefault(stringToCheck => stringToCheck.Contains("Erev"));

                if (matchingvalues != null)
                    return true;
                else return false;
            }
        }

        #endregion



    }
}
