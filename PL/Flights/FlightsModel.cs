using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using BLL;

namespace PL.Flights
{
    public class FlightsModel
    {
        IBL BL;
        

        public FlightsModel()
        {
            BL = new BLImp();
        }
        public  Dictionary<string, IEnumerable<BE.FlightInfoPartial>> getAllflightModel()
        {
            //load current
           return BL.GetCurrentFlights();

        }
        public FlightRoot GetFlightDataModel(FlightInfoPartial flightInfoPartial)
        {
            return BL.GetFlightData(flightInfoPartial.SourceId);
        }

        public void SaveFlightToDBModel(FlightInfoPartial flightInfoPartial)
        {
            BL.SaveFlightToDB(flightInfoPartial);
        }

    }
}
