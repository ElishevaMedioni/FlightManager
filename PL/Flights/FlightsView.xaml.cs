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
            Dictionary<string, IEnumerable<BE.FlightInfoPartial>> FlightKeys ;

            InFlightsListBox.DataContext = FlightKeys["Incoming"];
            OutFlightsListBox.DataContext = FlightKeys["Outgoing"];
            foreach (var Flight in FlightKeys["Incoming"])
                PinCurrentFlight(Flight);
            foreach (var Flight in FlightKeys["Outgoing"])
                PinCurrentFlight(Flight);
        }
    }
}
