using BE;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL.Historic
{
    public class HistoricViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public HistoricModel HistoricModel { get; set; }

        private ObservableCollection<BE.FlightInfoPartial> historicFlights { get; set; }
        public ObservableCollection<BE.FlightInfoPartial> HistoricFlights 
        {
            get { return historicFlights; }
            set
            {
                historicFlights = value;
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged("historicFlights");
            }
        } 


        public HistoricViewModel()
        {
            HistoricModel = new HistoricModel();
            historicFlights = new ObservableCollection<BE.FlightInfoPartial>();
            var Flights = HistoricModel.GetFlightsHistoricModel();

            HistoricFlights = new ObservableCollection<FlightInfoPartial>(Flights);

        }

       

        public List<BE.FlightInfoPartial> GetFlightsHistoricViewModel()
        {
            var Flights = HistoricModel.GetFlightsHistoricModel();

            return Flights;
        }
        public List<BE.FlightInfoPartial> GetFlightsHistoricByDateViewModel(DateTime start, DateTime end)
        {
            List<FlightInfoPartial> ListByDate = HistoricModel.GetFlightsHistoricByDateModel(start, end);
            return ListByDate;
        }

        public void DeleteFlightHistoricViewModel(int idFlight)
        {
            //delete from ObservableCollection
            var flightDelete = HistoricFlights.FirstOrDefault(x => x.Id == idFlight);

                HistoricFlights.Remove(flightDelete);
                //delete from Db
                HistoricModel.DeleteFlightHistoricModel(idFlight);

        }

        public void DeleteFlightsHistoricViewModel(List<FlightInfoPartial> DeletFlight)
        {
            HistoricModel.DeleteFlightsHistoricModel(DeletFlight);
            historicFlights.Clear();

        }
        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    }
}
