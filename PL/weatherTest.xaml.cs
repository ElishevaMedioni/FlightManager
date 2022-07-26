﻿using BE;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for weatherTest.xaml
    /// </summary>
    public partial class weatherTest : Window
    {
        public weatherTest()
        {
            InitializeComponent();
        }
        string APIKey = "16e42275ad992efba2e0fd36ff522dc4";
        HelperClass Helper = new HelperClass();

        private void getWdata(object sender, RoutedEventArgs e)
        {
            getWeather();

        }

        void getWeather()
        {
            using(WebClient web = new WebClient() )
            {

                try
                {
                    string urlweater = string.Format("https://api.openweathermap.org/data/2.5/weather?lat={0}&lon={1}&appid={2}", latitude.Text.ToString(), longitude.Text.ToString(), APIKey.ToString());
                    var json = web.DownloadString(urlweater);
                    BE.WeatherRoot Info = JsonConvert.DeserializeObject<BE.WeatherRoot>(json);
                    Info.sys.sunsetDate = Helper.GetTimeFromEpoch(Convert.ToDouble(Info.sys.sunset));
                    Info.sys.sunriseDate = Helper.GetTimeFromEpoch(Convert.ToDouble(Info.sys.sunrise));

                    DetailsPanel.DataContext = Info;
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }

                
                
                

            }

        }
    }
}
