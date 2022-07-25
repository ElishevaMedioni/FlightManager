using BE;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace DB
{
    public static class DataSource
    {
        private const string AllURL = @"https://data-cloud.flightradar24.com/zones/fcgi/feed.js?faa=1&bounds=38.64%2C21.377%2C24.676%2C40.605&satellite=1&mlat=1&flarm=1&adsb=1&gnd=1&air=1&vehicles=1&estimated=1&maxage=14400&gliders=1&stats=1";

        private const string FlightURL = @"https://data-live.flightradar24.com/clickhandler/?version=1.5&flight=";

        public static List<BE.FlightInfoPartial> Incoming = new List<BE.FlightInfoPartial>();
        public static List<BE.FlightInfoPartial> Outgoing = new List<BE.FlightInfoPartial>();

        //פונקצית אתחול לכל רשימה
        static DataSource()
        {
            OutgoingFlightsInit();
            IncomingFlightsInit();
        }

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
    }
}
