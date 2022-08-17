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

namespace PL.Historic
{
    /// <summary>
    /// Interaction logic for HistoricView.xaml
    /// </summary>
    public partial class HistoricView : UserControl
    {
        public HistoricViewModel historicViewModel;
        public HistoricView()
        {
            InitializeComponent();
            historicViewModel = new HistoricViewModel();
            DataContext = historicViewModel;

            FlightsHistoricList.ItemsSource = historicViewModel.HistoricFlights;

        }
        //private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    DateTime selectedDate = _datePicker.SelectedDate.Value;
        //    DateTime now= DateTime.Now;
        //    FlightsHistoricList.ItemsSource = historicViewModel.GetFlightsHistoricByDateViewModel(selectedDate, now);
        //}

        private void DisplayHistoric_Click(object sender, RoutedEventArgs e)
        {
             
            if (_Calendar.SelectedDate.HasValue)
            {
                _Calendar.SelectedDates.Clear();
            }

            FlightsHistoricList.ItemsSource = historicViewModel.HistoricFlights;
        }

        private void DeleteSelectedFlight_Click(object sender, RoutedEventArgs e)
        {
            BE.FlightInfoPartial flightToDelete = FlightsHistoricList.SelectedItem as BE.FlightInfoPartial;
           if (flightToDelete==null)
            {
                MessageBox.Show("You didn't select a flight to delete.");
            }
           else
                historicViewModel.DeleteFlightHistoricViewModel(flightToDelete.Id);
            FlightsHistoricList.ItemsSource = historicViewModel.HistoricFlights;
        }

        private void DeleteAllHistoric_Click(object sender, RoutedEventArgs e)
        {
            var flight =historicViewModel.GetFlightsHistoricViewModel();
            historicViewModel.DeleteFlightsHistoricViewModel(flight); ;
            FlightsHistoricList.ItemsSource = historicViewModel.HistoricFlights;
        }

        //private void _Calendar_SelectionModeChanged(object sender, EventArgs e)
        //{
        //    var calendar = sender as Calendar;

        //    if (calendar.SelectedDate.HasValue)
        //    {
        //        DateTime selectedStartDate = calendar.SelectedDates.First();
        //        DateTime selectedEndDate = calendar.SelectedDates.Last();
        //        FlightsHistoricList.ItemsSource = historicViewModel.GetFlightsHistoricByDateViewModel(selectedStartDate, selectedEndDate);
        //    }
      
        //}

        private void _Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            var calendar = sender as Calendar;

            if (calendar.SelectedDate.HasValue)
            {
                DateTime selectedStartDate = calendar.SelectedDates.First();
                DateTime selectedEndDate = calendar.SelectedDates.Last();
                FlightsHistoricList.ItemsSource = historicViewModel.GetFlightsHistoricByDateViewModel(selectedStartDate, selectedEndDate);
            }
        }

        private void FlightsHistoricList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FlightsHistoricList.Items.Refresh();
        }
    }
}
