using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.Flights
{
    public class FlightsViewModel
    {
        public FlightsModel FlightsModel { get; set; }
        public FlightsViewModel()
        {
            FlightsModel = new FlightsModel();
        }
    }
}
