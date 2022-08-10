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

namespace PL.FlightData
{
    /// <summary>
    /// Interaction logic for FlightDataView.xaml
    /// </summary>
    public partial class FlightDataView : UserControl
    {
        public FlightDataViewModel FlightDataViewModel;
        public FlightDataView()
        {
            InitializeComponent();
            FlightDataViewModel = new FlightDataViewModel();
            DataContext = FlightDataViewModel;
        }
    }
}
