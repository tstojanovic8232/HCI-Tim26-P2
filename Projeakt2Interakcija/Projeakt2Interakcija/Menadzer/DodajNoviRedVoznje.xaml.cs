using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for DodajNoviRedVoznje.xaml
    /// </summary>
    public partial class DodajNoviRedVoznje : Window
    {
        UcitavanjePodataka podaci = PregledRedaVoznje.podaci;

        public DodajNoviRedVoznje()
        {
            InitializeComponent();
            
            vozneLinije.ItemsSource = podaci.linije;
            List <DanUNedelji> dani = Enum.GetValues(typeof(DanUNedelji)).Cast<DanUNedelji>().ToList();
            //danUsedmici.ItemsSource = dani;

        }


        private void DodajRedVoznje_Click(object sender, RoutedEventArgs e)
        {
            Linija novaLinija = (Linija)vozneLinije.SelectedItem;
            
            List<DanUNedelji> dani = new List<DanUNedelji>();
            if ((bool)Ponedeljak.IsChecked) dani.Add(DanUNedelji.Ponedeljak);
            if ((bool)Utorak.IsChecked) dani.Add(DanUNedelji.Utorak);
            if ((bool)Sreda.IsChecked) dani.Add(DanUNedelji.Sreda);
            if ((bool)Cetvrtak.IsChecked) dani.Add(DanUNedelji.Cetvrtak);
            if ((bool)Petak.IsChecked) dani.Add(DanUNedelji.Petak);
            if ((bool)Subota.IsChecked) dani.Add(DanUNedelji.Subota);
            if ((bool)Nedelja.IsChecked) dani.Add(DanUNedelji.Nedelja);

            MessageBox.Show((dani[0]).ToString());
            List<RedVoznje> noviRedovi = podaci.redoviVoznje;
            for (int i = 0; i<podaci.redoviVoznje.Count; i++)
            {
                foreach (var dan in dani)
                {
                    if (podaci.redoviVoznje[i].danUNedelji.Equals(dan) && 
                        !podaci.redoviVoznje[i].linije.Contains(novaLinija))
                    {
                        podaci.redoviVoznje[i].linije.Add(novaLinija);
                    }
                }
               
            }
            podaci.redoviVoznje = noviRedovi;
            podaci.UpisRedovaVoznje();
            MessageBox.Show("Unos uspjesan");
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
