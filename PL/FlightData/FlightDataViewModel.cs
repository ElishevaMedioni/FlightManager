using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.FlightData
{
    public class FlightDataViewModel
    {
        FlightDataModel FlightDataModel { get; set; }

        public FlightDataViewModel()
        {
            FlightDataModel = new FlightDataModel();
        }


    }
}
