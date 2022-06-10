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
using Microsoft.Maps.MapControl.WPF;
using Microsoft.Toolkit.Wpf.UI.XamlHost;
using Microsoft.Toolkit.Wpf.UI.Controls;
using Windows.Devices.Geolocation;
using Windows.Storage.Streams;

namespace Projeakt2Interakcija
{
    /// <summary>
    /// Interaction logic for MrezniPrikazKlijent.xaml
    /// </summary>
    public partial class MrezniPrikazKlijent : Page
    {
        public MrezniPrikazKlijent()
        {
            InitializeComponent();
            var mapControl = new MapControl();
            mapControl.Loaded += MapControl_Loaded;

            mapControl.MapServiceToken = App.token;
            mapGrid.Children.Add(mapControl);
        }
        private async void MapControl_Loaded(object sender, RoutedEventArgs e)
        {
            BasicGeoposition geoposition = new BasicGeoposition() { Latitude = 44.8125, Longitude = 20.4612 };
            var center = new Geopoint(geoposition);

            await ((MapControl)sender).TrySetViewAsync(center, 12);
        }
    }
}
