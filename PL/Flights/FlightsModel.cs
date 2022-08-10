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
        private Dictionary<string, IEnumerable<BE.FlightInfoPartial>> getAllflight()
        {
            //load current
           return BL.GetCurrentFlights();

         
        }
    }
}
