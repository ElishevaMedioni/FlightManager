using BE;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.Flights
{
    public class FlightsViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public FlightsModel FlightsModel { get; set; }

        private Dictionary<string, IEnumerable<BE.FlightInfoPartial>> flightKeys { get; set; }
        public Dictionary<string, IEnumerable<BE.FlightInfoPartial>> FlightKeys
        {
            get { return flightKeys; }
            set
            {
                flightKeys = value;
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged("flightKeys");
            }
        }

        
        public FlightsViewModel()
        {
            FlightsModel = new FlightsModel();
            flightKeys = new Dictionary<string, IEnumerable<BE.FlightInfoPartial>>();
            var Flights = FlightsModel.GetAllFlightsModel();

            FlightKeys = new Dictionary<string, IEnumerable<BE.FlightInfoPartial>>(Flights);
        }


        public FlightRoot GetFlightDataViewModel(FlightInfoPartial flightInfoPartial)
        {
            return FlightsModel.GetFlightDataModel(flightInfoPartial);
        }

        public void SaveFlightToDBViewModel(FlightInfoPartial flightInfoPartial)
        {
            FlightsModel.SaveFlightToDBModel(flightInfoPartial);
        }
        public WeatherRoot GetWeatherWithLatLongViewModel(string latitude, string longitude)
        {
            return FlightsModel.GetWeatherWithLatLongModel(latitude, longitude);
        }
        public bool GetHolidayViewModel()
        {
            return FlightsModel.GetHolidayModel();
        }
        public DateRoot GetdateViewModel(DateTime date)
        {
            return FlightsModel.Getdate(date);
        }
        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    }
}
