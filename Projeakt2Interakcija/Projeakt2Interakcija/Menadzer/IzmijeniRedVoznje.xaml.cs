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
using System.Data;
using Projeakt2Interakcija.Model;

namespace Projeakt2Interakcija
{
    /// <summary>
    /// Interaction logic for IzmijeniRedVoznje.xaml
    /// </summary>
    public partial class IzmijeniRedVoznje : Window
    {
        DataRowView odabraniRed = PregledRedaVoznje.odabraniRed;
        UcitavanjePodataka podaci = PregledRedaVoznje.podaci;
        int danID = 0;
        int linijaID=0;

        public IzmijeniRedVoznje()
        {
            InitializeComponent();

            List<DanUNedelji> dani = Enum.GetValues(typeof(DanUNedelji)).Cast<DanUNedelji>().ToList();
            danUsedmici.ItemsSource = dani;
            for (int i = 0; i < dani.Count; i++)
            {
                if (odabraniRed[0].ToString().Equals(dani[i].ToString()))
                {
                    danUsedmici.SelectedIndex = i;
                    danID = i;
                }
            }

            vozneLinije.ItemsSource = podaci.linije;
            for (int i = 0; i<podaci.linije.Count; i++)
            {
                if (podaci.linije[i].naziv.Equals(odabraniRed[1].ToString()))
                {
                    vozneLinije.SelectedIndex = i;
                }  
                //vozneLinije.SelectedItem = odabraniRed[1].ToString();
            }

            
        }

        private void Izmijeni_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(danUsedmici.SelectedIndex.ToString());
            MessageBox.Show(danID.ToString());
            MessageBox.Show(podaci.redoviVoznje[danID].linije.Count.ToString());
            MessageBox.Show(linijaID.ToString());

            for (int i = 0; i < podaci.redoviVoznje[danID].linije.Count; i++)
            {
                if (podaci.linije[i].naziv.Equals(odabraniRed[1].ToString()))
                {
                    linijaID = i;
                }
                //vozneLinije.SelectedItem = odabraniRed[1].ToString();
            }

            Linija promijenjena = podaci.redoviVoznje[danID].linije[linijaID];
            MessageBox.Show(promijenjena.naziv);

            podaci.redoviVoznje[danID].linije.Remove(promijenjena);
            promijenjena = podaci.linije[vozneLinije.SelectedIndex];

            if (!podaci.redoviVoznje[danUsedmici.SelectedIndex].linije.Contains(promijenjena))
            {
                podaci.redoviVoznje[danUsedmici.SelectedIndex].linije.Add(promijenjena);

                podaci.UpisRedovaVoznje();
                this.Close();
            }
            else MessageBox.Show("Odabrana linija je već u redu vožnje za taj dan!");

        }

        private void NazadNaPocetnu_Click(object sender, RoutedEventArgs e)
        {

            //PregledRedaVoznje pregledRedaVoznje = new PregledRedaVoznje();
            //pregledRedaVoznje.Show();
            this.Close();
        }

        private void OdjaviMe_Click(object sender, RoutedEventArgs e)
        {
            OdjavaMenadzer logout = new OdjavaMenadzer();
            logout.Show();
            this.Close();
        }
    }
}
