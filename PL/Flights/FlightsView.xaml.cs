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
using System.Drawing;
using System.IO;

namespace PL.Flights
{
    /// <summary>
    /// Interaction logic for FlightsView.xaml
    /// </summary>
    public partial class FlightsView : UserControl
    {

        public FlightsViewModel flightsViewModel;
        private FlightDataView flightDataView;
        
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        public FlightsView()
        {
            InitializeComponent();
            flightsViewModel = new FlightsViewModel();
            DataContext = flightsViewModel;
            GetAllFlightsToMap();
            
        }

        private void GetAllFlightsToMap()
        {
            //load current
            Dictionary<string, IEnumerable<BE.FlightInfoPartial>> FlightKeys= flightsViewModel.FlightKeys;

            InFlightsListBox.DataContext = FlightKeys["Incoming"];
            OutFlightsListBox.DataContext = FlightKeys["Outgoing"];
            foreach (var Flight in FlightKeys["Incoming"])
                PinCurrentFlight(Flight);
            foreach (var Flight in FlightKeys["Outgoing"])
                PinCurrentFlight(Flight);
            DateHoliday();
            GetDateHeb();


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
            allflight.Visibility = Visibility.Hidden;
            track.Visibility = Visibility.Hidden;

            var FlightKeys = flightsViewModel.FlightKeys;
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
                
                FlightData.Visibility = Visibility.Visible;
                allflight.Visibility = Visibility.Visible;
                track.Visibility = Visibility.Visible;
            }


        }

        
        //private double angleFromCoordinate(double lat1, double long1, double lat2,
        //double long2)
        //{

        //    double dLon = (long2 - long1);

        //    double y = Math.Sin(dLon) * Math.Cos(lat2);
        //    double x = Math.Cos(lat1) * Math.Sin(lat2) - Math.Sin(lat1)
        //            * Math.Cos(lat2) * Math.Cos(dLon);

        //    double brng = Math.Atan2(y, x);

        //    brng = (180 / Math.PI) * brng;
        //    brng = (brng + 360) % 360;
        //    brng = 360 - brng; // count degrees counter-clockwise - remove to make clockwise

        //    return brng;
        //}

        //private Bitmap RotateImage(Bitmap bmp, float angle)
        //{
        //    float height = bmp.Height;
        //    float width = bmp.Width;
        //    int hypotenuse = System.Convert.ToInt32(System.Math.Floor(Math.Sqrt(height * height + width * width)));
        //    Bitmap rotatedImage = new Bitmap(hypotenuse, hypotenuse);
        //    using (Graphics g = Graphics.FromImage(rotatedImage))
        //    {
        //        g.TranslateTransform((float)rotatedImage.Width / 2, (float)rotatedImage.Height / 2); //set the rotation point as the center into the matrix
        //        g.RotateTransform(angle); //rotate
        //        g.TranslateTransform(-(float)rotatedImage.Width / 2, -(float)rotatedImage.Height / 2); //restore rotation point into the matrix
        //        g.DrawImage(bmp, (hypotenuse - width) / 2, (hypotenuse - height) / 2, width, height);
        //    }
        //    return rotatedImage;
        //}

        //BitmapImage BitmapToImageSource(Bitmap bitmap)
        //{
        //    using (MemoryStream memory = new MemoryStream())
        //    {
        //        bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
        //        memory.Position = 0;
        //        BitmapImage bitmapimage = new BitmapImage();
        //        bitmapimage.BeginInit();
        //        bitmapimage.StreamSource = memory;
        //        bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
        //        bitmapimage.EndInit();

        //        return bitmapimage;
        //    }
        //}


        private void UpdateFlight(BE.FlightInfoPartial selected)
        {
            var Flight = flightsViewModel.GetFlightDataViewModel(selected);

            //DetailsPanel.DataContext = Flight;


            //update map
            if (Flight != null)
            {
                List<BE.Trail> OrderedPlaces = (from f in Flight.trail
                                                orderby f.ts
                                                select f).ToList<BE.Trail>();

                

                addNewPolyLine(OrderedPlaces);

                //code to rotate the plane
                //double angleForPlanePushPin = angleFromCoordinate(
                //    Flight.airport.destination.position.latitude,
                //    Flight.airport.destination.position.longitude,
                //    Flight.airport.origin.position.latitude,
                //    Flight.airport.origin.position.longitude);

                //float angle = (float)angleForPlanePushPin;

                //Bitmap iconRotated = RotateImage(new Bitmap("C:\\Users\\zeevm\\source\\repos\\FlightManager\\PL\\Images\\airplaneUpLeft.png"), angle);
                //imgTest.Source = BitmapToImageSource(iconRotated);
                //

                BE.Trail CurrentPlace = null;
                
                
                Pushpin PinCurrent = new Pushpin { ToolTip = selected.FlightCode };
                Pushpin PinOrigin = new Pushpin { ToolTip = Flight.airport.origin.name };
                
                PinOrigin.Background = System.Windows.Media.Brushes.HotPink;

                Pushpin PinDestination = new Pushpin { ToolTip = Flight.airport.destination.name };

                PinCurrent.MouseDoubleClick += Pushpin_MouseDoubleClick;
                //PinCurrent.MouseEnter += Pushpin_MouseEnter;

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
                //WeatherPanel.DataContext = weatherRoot;
            }
        }

        private void DateHoliday()
        {

            DateTime start = DateTime.Now;
            bool flag = flightsViewModel.GetHolidayViewModel();
            if (flag == true)
            {
                date.Text = "✔️ There is a holiday this week ";
            }
            else
            {
                date.Text = "❌ There is no holiday this week ";

            }
        }
        private void GetDateHeb()
        {
            DateTime today = DateTime.Now;
            dateheb.Text = "📆 " + today.ToString()+ "\n🗓 " + flightsViewModel.GetDateViewModel(today).ToString();
        }



        private void Pushpin_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            myMap.Children.Clear();
            BE.FlightInfoPartial SelectedFlight = null;
            SelectedFlight = e.Source as BE.FlightInfoPartial;
            //SelectedFlight = e. as BE.FlightInfoPartial; //dangerous code - works but need to change it
            //UpdateFlight(SelectedFlight);
            //UpdateWeather(SelectedFlight);


            var Flight = flightsViewModel.GetFlightDataViewModel(SelectedFlight);
            if (Flight != null)
            {
                BE.WeatherRoot weatherRoot = flightsViewModel.GetWeatherWithLatLongViewModel(Flight.airport.destination.position.latitude.ToString(), Flight.airport.destination.position.longitude.ToString());
                flightDataView = new FlightDataView(Flight, weatherRoot);
                FlightData.Content = flightDataView;
            }
        }

        private void allflight_Click(object sender, RoutedEventArgs e)
        {
            myMap.Children.Clear();
            FlightData.Visibility = Visibility.Hidden;
            if (dispatcherTimer.IsEnabled)
            {
                
                dispatcherTimer.Stop();
                Counter.Text = "1";
            }
            ViewAllFlights();
        }

        private void track_Click(object sender, RoutedEventArgs e)
        {
            if (dispatcherTimer.IsEnabled)
            {
                
                dispatcherTimer.Stop();
                Counter.Text = "1";
                
                track.Content = "▶️ Start Track";
            }
            else
            {
                
                dispatcherTimer.Tick += DispatcherTimer_Tick;
                dispatcherTimer.Interval = new TimeSpan(0, 0, 15);
                dispatcherTimer.Start();
                track.Content = "⏸️ Stop Track";
            }
            
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            BE.FlightInfoPartial selectedFlight1 = InFlightsListBox.SelectedItem as BE.FlightInfoPartial;
            BE.FlightInfoPartial selectedFlight2 = OutFlightsListBox.SelectedItem as BE.FlightInfoPartial;

            if (selectedFlight1 != null)
            {
                UpdateFlight(selectedFlight1);
                Counter.Text = (Convert.ToInt32(Counter.Text) + 1).ToString();
            }
            else
                if (selectedFlight2 != null)
            {
                UpdateFlight(selectedFlight2);
                Counter.Text = (Convert.ToInt32(Counter.Text) + 1).ToString();
            }
            
            
        }
    }
}
