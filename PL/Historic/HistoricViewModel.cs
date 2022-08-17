using BE;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL.Historic
{
    public class HistoricViewModel
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
            var Flight = HistoricModel.GetFlightsHistoricModel();

            return Flight;
        }
        public List<BE.FlightInfoPartial> GetFlightsHistoricByDateViewModel(DateTime start, DateTime end)
        {
            List<FlightInfoPartial> ListByDate = HistoricModel.GetFlightsHistoricByDateModel(start, end);
            return ListByDate;
        }

        public void DeletFlightHistoricViewModel(int idFlight)
        {
            //delete from ObservableCollection
            var flightDelete = HistoricFlights.FirstOrDefault(x => x.Id == idFlight);

                HistoricFlights.Remove(flightDelete);
                //delete from Db
                HistoricModel.DeletFlightHistoricModel(idFlight);

        }

        public void DeletFlightsHistoricViewModel(List<FlightInfoPartial> DeletFlight)
        {
            HistoricModel.DeletFlightsHistoricModel(DeletFlight);
            HistoricFlights.Clear();

        }
    }
}
