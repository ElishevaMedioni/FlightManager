
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
using BLL;
using PL.Historic;
using PL.Flights;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        private HistoricView historicView;
        private FlightsView flightsView;

        public MainWindow()
        {
            InitializeComponent();
            
        }
        


        private void historicButton_Click(object sender, RoutedEventArgs e)
        {

            if (!(MainUC.Content is HistoricView))
            {
                historicView = new HistoricView();

            }
            
            MainUC.Content = historicView;
            
        }

        private void FlightsButton_Click(object sender, RoutedEventArgs e)
        {
            if (!(MainUC.Content is FlightsView))
            {
                flightsView = new FlightsView();

            }
           
            MainUC.Content = flightsView;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }



        //private void Button_Click_1(object sender, RoutedEventArgs e)
        //{
        //    DispatcherTimer dispatcherTimer = new DispatcherTimer();
        //    dispatcherTimer.Tick += DispatcherTimer_Tick;
        //    dispatcherTimer.Interval = new TimeSpan(0, 0, 15);
        //    dispatcherTimer.Start();


        //}

        //private void DispatcherTimer_Tick(object sender, EventArgs e)
        //{
        //    UpdateFlight(SelectedFlight);
        //    Counter.Text = (Convert.ToInt32(Counter.Text) + 1).ToString();
        //}



    }
}