using BE;
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

        public Dictionary<string, IEnumerable<BE.FlightInfoPartial>> getAllFlightsViewModel()
        {
            return FlightsModel.getAllflightModel();
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
    }
}
