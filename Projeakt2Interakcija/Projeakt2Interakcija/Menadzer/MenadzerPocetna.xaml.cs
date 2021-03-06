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
            pregledKarata.Show();
            this.Close();
        }

        private void OdjaviMe_Click(object sender, RoutedEventArgs e)
        {
            OdjavaMenadzer logout = new OdjavaMenadzer();
            logout.Show();
            this.Close();
        }

        private void VozneLinije_Click(object sender, RoutedEventArgs e)
        {
            PrikazLinija prikazLinija = new PrikazLinija();
            prikazLinija.Show();
            this.Close();
        }

        private void NasiVozovi_Click(object sender, RoutedEventArgs e)
        {
            PrikazVozova prikaz = new PrikazVozova();
            prikaz.Show();
            this.Close();
        }

        private void RedVoznje_Click(object sender, RoutedEventArgs e)
        {
            PregledRedaVoznje redVoznje = new PregledRedaVoznje();
            redVoznje.Show();
            this.Close();
        }
    }
}
