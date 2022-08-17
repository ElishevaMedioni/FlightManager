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
         
        }
        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime selectedDate = _datePicker.SelectedDate.Value;
            DateTime now= DateTime.Now;
            FlightsHistoricList.ItemsSource = historicViewModel.GetFlightsHistoricByDateViewModel(selectedDate, now);
        }

        private void DisplayHistoric_Click(object sender, RoutedEventArgs e)
        {
            FlightsHistoricList.ItemsSource = historicViewModel.GetFlightsHistoricViewModel();

        }

        private void DeleteSelectedFlight_Click(object sender, RoutedEventArgs e)
        {

           // historicViewModel.DeletFlightHistoricViewModel();
        }

        private void DeleteAllHistoric_Click(object sender, RoutedEventArgs e)
        {
            var flight =historicViewModel.GetFlightsHistoricViewModel();
            historicViewModel.DeletFlightsHistoricViewModel(flight); ;
        }

        

        //private void _datePicker_SelectionModeChanged(object sender, EventArgs e)
        //{
        //    DateTime selectedStartDate = _datePicker..Value;
        //    DateTime selectedEndDate = _datePicker.DisplayDateEnd.Value;
        //    FlightsHistoricList.ItemsSource = historicViewModel.GetFlightsHistoricByDateViewModel(selectedStartDate, selectedEndDate);

        //}
    }
}
