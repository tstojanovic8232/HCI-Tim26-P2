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
using System.Windows.Shapes;

namespace Projeakt2Interakcija
{
    /// <summary>
    /// Interaction logic for OdjavaMenadzer.xaml
    /// </summary>
    public partial class OdjavaMenadzer : Window
    {

        public OdjavaMenadzer()
        {
            InitializeComponent();
            SetProperties();

            foreach (Window item in App.Current.Windows)
            {
                if (item != this)
                    item.Close();
            }
        }

        void SetProperties()
        {
            Uri iconUri = new Uri("../../Slike/SrbijaVozLogo.jpg", UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);
        }





        private void PrijaviMe_Click(object sender, RoutedEventArgs e)
        {
            MainWindow pocetna = new MainWindow();
            pocetna.Show();
            this.Close();
        }
    }
}
