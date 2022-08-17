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
        public List<FlightInfoPartial> GetFlightsHistoricByDateModel(DateTime start, DateTime end)
        {
            var Flight = GetFlightsHistoricModel();
            List<FlightInfoPartial> ListByDate = Flight.Where(x => x.DateAndTime > start && x.DateAndTime < end).ToList();
            return ListByDate;
        }

        

    }
}
