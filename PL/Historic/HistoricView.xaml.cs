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
            DateTime selectedFromDate = _datePicker.SelectedDate.Value;
            FlightsHistoricList.ItemsSource = historicViewModel.GetFlightsHistoricViewModel(selectedFromDate);
        }

    }
}
