using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.Historic
{
    public  class HistoricViewModel
    {
        public HistoricModel HistoricModel { get; set; }
        public ObservableCollection<BE.FlightInfoPartial> HistoricFlights { get; set; }


        public HistoricViewModel()
        {
            HistoricModel = new HistoricModel();
            HistoricFlights = new ObservableCollection<BE.FlightInfoPartial>();
        }

        public List<BE.FlightInfoPartial> GetFlightsHistoricViewModel()
        {
            return HistoricModel.GetFlightsHistoricModel();
        }

    }
}
