using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using BLL;

namespace PL.FlightData
{
    public class FlightDataModel
    {
        IBL BL;

        public FlightDataModel()
        {
            BL = new BLImp();
        }


    }
}
