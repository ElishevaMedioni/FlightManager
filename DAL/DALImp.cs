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

        private const string AllURL = @"https://data-cloud.flightradar24.com/zones/fcgi/feed.js?faa=1&bounds=38.64%2C21.377%2C24.676%2C40.605&satellite=1&mlat=1&flarm=1&adsb=1&gnd=1&air=1&vehicles=1&estimated=1&maxage=14400&gliders=1&stats=1";

        public Dictionary<string, List<BE.FlightInfoPartial>> GetCurrentFlights()
        {
           

            JObject AllFlightData = null;
            //IList<string> Keys = null;
            //IList<Object> Values = null;

            List<BE.FlightInfoPartial> Incoming = new List<BE.FlightInfoPartial>();
            List<BE.FlightInfoPartial> Outgoing = new List<BE.FlightInfoPartial>();

            using (var webClient = new System.Net.WebClient())
            {
                //worked with synchronous -->need to change to asynchronous
                var json = webClient.DownloadString(AllURL);

                BE.HelperClass Helper = new BE.HelperClass();
                AllFlightData = JObject.Parse(json);

                try
                {
                    foreach (var item in AllFlightData)
                    {
                        var key = item.Key;
                        if (key == "full_count") continue;
                        if (key == "version") continue;
                        if (item.Value[11].ToString() == "TLV")
                            Outgoing.Add(new BE.FlightInfoPartial
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
                        if (item.Value[12].ToString() == "TLV")
                            Incoming.Add(new BE.FlightInfoPartial
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
            Result.Add("Incoming", Incoming);
            Result.Add("Outgoing", Outgoing);

            return Result;

        }

        public Root GetFlightData(String Key)
        {
            var CurrentUrl = FlightURL + Key;
            Root CurrentFlight = null;

            //MUST use try- catch
            using (var webClient = new System.Net.WebClient())
            {
                var json = webClient.DownloadString(CurrentUrl);
                try
                {
                    CurrentFlight = (Root)Newtonsoft.Json.JsonConvert.DeserializeObject(json, typeof(Root));
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
            }
            return CurrentFlight;
        }
    }
}
