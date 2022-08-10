using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Microsoft.Maps.MapControl.WPF;
using System.Collections.ObjectModel;

namespace PL.Flights
{
    /// <summary>
    /// Interaction logic for FlightsView.xaml
    /// </summary>
    public partial class FlightsView : UserControl
    {

        public FlightsViewModel flightsViewModel;
        public FlightsView()
        {
            InitializeComponent();
            flightsViewModel = new FlightsViewModel();
            DataContext = flightsViewModel;
        }

        private void getAllflightToMap()
        {
            //load current
            Dictionary<string, IEnumerable<BE.FlightInfoPartial>> FlightKeys= flightsViewModel.getAllFlightsViewModel();

            InFlightsListBox.DataContext = FlightKeys["Incoming"];
            OutFlightsListBox.DataContext = FlightKeys["Outgoing"];
            foreach (var Flight in FlightKeys["Incoming"])
                PinCurrentFlight(Flight);
            foreach (var Flight in FlightKeys["Outgoing"])
                PinCurrentFlight(Flight);
            dateHoliday();

        }

        private void PinCurrentFlight(BE.FlightInfoPartial selected)
        {

            var Flight = flightsViewModel.GetFlightDataViewModel(selected);


            //update map
            if (Flight != null)
            {
                List<BE.Trail> OrderedPlaces = (from f in Flight.trail
                                                orderby f.ts
                                                select f).ToList<BE.Trail>();

                BE.Trail CurrentPlace = null;

                Pushpin PinCurrent = new Pushpin { ToolTip = selected.FlightCode };

                PositionOrigin origin = new PositionOrigin { X = 0.4, Y = 0.4 };

                MapLayer.SetPositionOrigin(PinCurrent, origin);

                if (Flight.airport.destination.code.iata == "TLV")
                {
                    PinCurrent.Style = (Style)Resources["ToIsrael"];
                }
                else
                {
                    PinCurrent.Style = (Style)Resources["FromIsrael"];
                }

                CurrentPlace = OrderedPlaces.Last<BE.Trail>();
                var PlaneLocation = new Location { Latitude = CurrentPlace.lat, Longitude = CurrentPlace.lng };
                PinCurrent.Location = PlaneLocation;

                CurrentPlace = OrderedPlaces.First<BE.Trail>();
                PlaneLocation = new Location { Latitude = CurrentPlace.lat, Longitude = CurrentPlace.lng };
                myMap.Children.Add(PinCurrent);
            }
        }

        private void ViewAllFlights()
        {
            var FlightKeys = flightsViewModel.getAllFlightsViewModel();
            foreach (var Flight in FlightKeys["Incoming"])
                PinCurrentFlight(Flight);
            foreach (var Flight in FlightKeys["Outgoing"])
                PinCurrentFlight(Flight);
        }

        private void FlightsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            myMap.Children.Clear();
            BE.FlightInfoPartial SelectedFlight = null;
            SelectedFlight = e.AddedItems[0] as BE.FlightInfoPartial; //dangerous code - works but need to change it
            UpdateFlight(SelectedFlight);
            //UpdateWeather(SelectedFlight);

            flightsViewModel.SaveFlightToDBViewModel(SelectedFlight);
          
        }

        private void UpdateFlight(BE.FlightInfoPartial selected)
        {
            var Flight = flightsViewModel.GetFlightDataViewModel(selected);

            var Flight = flightsViewModel.GetFlightDataViewModel(selected);

            DetailsPanel.DataContext = Flight;


            //update map
            if (Flight != null)
            {
                List<BE.Trail> OrderedPlaces = (from f in Flight.trail
                                                orderby f.ts
                                                select f).ToList<BE.Trail>();

                addNewPolyLine(OrderedPlaces);

                BE.Trail CurrentPlace = null;

                Pushpin PinCurrent = new Pushpin { ToolTip = selected.FlightCode };
                Pushpin PinOrigin = new Pushpin { ToolTip = Flight.airport.origin.name };

                PositionOrigin origin = new PositionOrigin { X = 0.4, Y = 0.4 };

                MapLayer.SetPositionOrigin(PinCurrent, origin);

                if (Flight.airport.destination.code.iata == "TLV")
                {
                    PinCurrent.Style = (Style)Resources["ToIsrael"];
                }
                else
                {
                    PinCurrent.Style = (Style)Resources["FromIsrael"];
                }

                CurrentPlace = OrderedPlaces.Last<BE.Trail>();
                var PlaneLocation = new Location { Latitude = CurrentPlace.lat, Longitude = CurrentPlace.lng };
                PinCurrent.Location = PlaneLocation;

                CurrentPlace = OrderedPlaces.First<BE.Trail>();
                PlaneLocation = new Location { Latitude = CurrentPlace.lat, Longitude = CurrentPlace.lng };
                PinOrigin.Location = PlaneLocation;
                //PinCurrent.MouseDown += Pin MouseDown;

                myMap.Children.Add(PinOrigin);
                myMap.Children.Add(PinCurrent);

            }
        }
    }
}
