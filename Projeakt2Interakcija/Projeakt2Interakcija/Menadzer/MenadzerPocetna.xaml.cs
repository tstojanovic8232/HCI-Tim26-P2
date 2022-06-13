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
    /// Interaction logic for MenadzerPocetna.xaml
    /// </summary>
    public partial class MenadzerPocetna : Window
    {
        public MenadzerPocetna()
        {
            InitializeComponent();
            SetProperties();
        }

        void SetProperties()
        {
            this.Title = "Srbija Voz";

            this.MinHeight = 600;
            this.MinWidth = 800;
            Uri iconUri = new Uri("../../Slike/SrbijaVozLogo.jpg", UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);
        }

        private void ProdateKarte_Click(object sender, RoutedEventArgs e)
        {
            PregledKarataMenadzer pregledKarata = new PregledKarataMenadzer();
            this.Close();
            pregledKarata.ShowDialog();
        }

        private void OdjaviMe_Click(object sender, RoutedEventArgs e)
        {
            OdjavaMenadzer logout = new OdjavaMenadzer();
            this.Close();
            logout.Show();
        }

        private void VozneLinije_Click(object sender, RoutedEventArgs e)
        {
            PregledVoznihLinijaMenadzer vozneLinije = new PregledVoznihLinijaMenadzer();
            this.Close();
            vozneLinije.Show();
        }

        private void NasiVozovi_Click(object sender, RoutedEventArgs e)
        {
            NasiVozoviMenadzer nasiVozovi = new NasiVozoviMenadzer();
            this.Close();
            nasiVozovi.ShowDialog();
        }

        private void RedVoznje_Click(object sender, RoutedEventArgs e)
        {
            PregledRedaVoznje redVoznje = new PregledRedaVoznje();
            this.Close();
            redVoznje.ShowDialog();
        }
    }
}
