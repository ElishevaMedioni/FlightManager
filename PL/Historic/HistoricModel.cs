using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using BLL;

namespace PL.Historic
{
    public class HistoricModel
    {
        IBL BL;

        public HistoricModel()
        {
            BL = new BLImp();
        }

        public List<FlightInfoPartial> GetFlightsHistoricModel()
        {
            return BL.GetAllFlightInfoPartial(null);
        }

        
    }
}
