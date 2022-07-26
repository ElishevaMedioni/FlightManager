﻿using System;
using System.Collections.Generic;
using System.Globalization;
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
using BE;

namespace PL.FlightData
{
    /// <summary>
    /// Interaction logic for FlightDataView.xaml
    /// </summary>
    public partial class FlightDataView : UserControl
    {
        public FlightDataViewModel FlightDataViewModel;
        public FlightDataView(FlightRoot flightRoot, WeatherRoot weatherRoot)
        {
            InitializeComponent();
            FlightDataViewModel = new FlightDataViewModel();
            DataContext = FlightDataViewModel;
            DetailsPanel.DataContext = flightRoot;
            WeatherPanel.DataContext = weatherRoot;
            weatherIcon.Source = new BitmapImage(new Uri("https://openweathermap.org/img/w/" + weatherRoot.weather[0].icon + ".png"));

        }

        
        
        
    }
}
