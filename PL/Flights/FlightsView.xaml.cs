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
using PL.FlightData;

namespace PL.Flights
{
    /// <summary>
    /// Interaction logic for FlightsView.xaml
    /// </summary>
    public partial class FlightsView : UserControl
    {

        public FlightsViewModel flightsViewModel;
        private FlightDataView flightDataView;
        public FlightsView()
        {
            InitializeComponent();
            flightsViewModel = new FlightsViewModel();
            DataContext = flightsViewModel;
            getAllflightToMap();
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
            getdateheb();


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
            UpdateWeather(SelectedFlight);

            flightsViewModel.SaveFlightToDBViewModel(SelectedFlight);

            var Flight = flightsViewModel.GetFlightDataViewModel(SelectedFlight);
            if (Flight != null)
            {
                BE.WeatherRoot weatherRoot = flightsViewModel.GetWeatherWithLatLongViewModel(Flight.airport.destination.position.latitude.ToString(), Flight.airport.destination.position.longitude.ToString());
                flightDataView = new FlightDataView(Flight,weatherRoot);
                FlightData.Content = flightDataView;
            }


        }

        private void UpdateFlight(BE.FlightInfoPartial selected)
        {
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

        void addNewPolyLine(List<BE.Trail> Route)
        {
            //new version of MapPolyline -- different fields -- need to adapt


            MapPolyline polyline = new MapPolyline();
            //polyline.Fill = new System.Windows.Media.SolidColorBrush(Colors.Red);
            polyline.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Red);
            polyline.StrokeThickness = 1;
            polyline.Opacity = 0.7;
            polyline.Locations = new LocationCollection();

            foreach (var item in Route)
            {
                polyline.Locations.Add(new Location(item.lat, item.lng));
            }

            myMap.Children.Clear();
            myMap.Children.Add(polyline);


        }

        private void UpdateWeather(BE.FlightInfoPartial selected)
        {

            var Flight =flightsViewModel.GetFlightDataViewModel(selected);

            //some of the flight null
            if (Flight != null)
            {
                BE.WeatherRoot weatherRoot = flightsViewModel.GetWeatherWithLatLongViewModel(Flight.airport.destination.position.latitude.ToString(), Flight.airport.destination.position.longitude.ToString());
                WeatherPanel.DataContext = weatherRoot;
            }
        }

        private void dateHoliday()
        {

            DateTime start = DateTime.Now;
            bool flag = flightsViewModel.GetHolidayViewModel();
            if (flag == true)
            {
                date.Text = "There is a holiday this week ";
            }
            else
            {
                date.Text = "There is no holiday this week ";

            }
        }
        private void getdateheb()
        {
            DateTime today = DateTime.Now;
            dateheb.Text = flightsViewModel.GetdateViewModel(today).ToString()+" date:"+ today.ToString();
        }


            private void myMap_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ViewAllFlights();

        }
    }
}
