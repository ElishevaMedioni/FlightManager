using BE;
using DAL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLImp:IBL
    {
        private const string FlightURL = @"https://data-live.flightradar24.com/clickhandler/?version=1.5&flight=";

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
    }
}
