using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for PregledKarataMenadzer.xaml
    /// </summary>
    public partial class PregledKarataMenadzer : Window
    {
        UcitavanjePodataka podaci = new UcitavanjePodataka();

        DataTable tabelaKarata = new DataTable();


        public PregledKarataMenadzer()
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

        private void NazadNaPocetnu_Click(object sender, RoutedEventArgs e)
        {
            
            MenadzerPocetna pocetna = new MenadzerPocetna();
            pocetna.Show();
            this.Close();
        }

        private void Prikazi_Click(object sender, RoutedEventArgs e)
        {
            tabelaKarata.Clear();
            tabelaKarata.Columns.Add("Id", typeof(int));
            tabelaKarata.Columns.Add("Datum i vreme", typeof(DateTime));
            tabelaKarata.Columns.Add("Linija", typeof(string));
            tabelaKarata.Columns.Add("polaziste", typeof(string));
            tabelaKarata.Columns.Add("odrediste", typeof(string));
            tabelaKarata.Columns.Add("cena", typeof(double));
            foreach (var item in podaci.karte)
            {
                tabelaKarata.Rows.Add(item.id, item.datumVreme, "",item.polaziste, item.odrediste, item.cena);
            }
            tabelaPodataka.ItemsSource = tabelaKarata.DefaultView;
        }

        private void OdjaviMe_Click(object sender, RoutedEventArgs e)
        {
            OdjavaMenadzer logout = new OdjavaMenadzer();
            logout.Show();
            this.Close();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
