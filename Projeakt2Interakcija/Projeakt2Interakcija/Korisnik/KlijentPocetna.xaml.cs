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
using Projeakt2Interakcija.Model;

namespace Projeakt2Interakcija
{
    /// <summary>
    /// Interaction logic for KlijentPocetna.xaml
    /// </summary>
    public partial class KlijentPocetna : Window
    {
        public KlijentPocetna()
        {
            InitializeComponent();
            SetProperties();
            frame.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Hidden;
            MrezniPrikazKlijent mp = new MrezniPrikazKlijent();
            frame.Content = mp;
        }

        void SetProperties()
        {
            this.Title = "Srbija Voz - Klijent";

            this.MinHeight = 600;
            this.MinWidth = 800;
            Uri iconUri = new Uri("../../Slike/SrbijaVozLogo.jpg", UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);
        }

        private void PrikaziKarte_Click(object sender, RoutedEventArgs e)
        {
              PregledKarataKlijent pk = new PregledKarataKlijent();
       
            frame.Content = pk;
        }

        private void PrikaziLinije_Click(object sender, RoutedEventArgs e)
        {
            PregledLinijaKlijent pl= new PregledLinijaKlijent();
            frame.Content = pl;
        }

        private void RezervisiKartu_Click(object sender, RoutedEventArgs e)
        {
            RezervacijaKarata rk = new RezervacijaKarata();
            frame.Content = rk;
        }

        private void Pocetna_Click(object sender, RoutedEventArgs e)
        {
            MrezniPrikazKlijent mp = new MrezniPrikazKlijent();
            frame.Content = mp;
        }

        private void Odjava_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            this.username = "";
            main.Show();
            this.Close();
        }

        private void PrikaziRedVoznje_Click(object sender, RoutedEventArgs e)
        {
            PregledRedaVoznjeKlijent prv = new PregledRedaVoznjeKlijent();
            frame.Content = prv;
        }
    }

}

