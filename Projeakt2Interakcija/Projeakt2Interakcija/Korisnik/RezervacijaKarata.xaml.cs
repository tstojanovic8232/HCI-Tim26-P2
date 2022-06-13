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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Projeakt2Interakcija.Model;

namespace Projeakt2Interakcija
{
    /// <summary>
    /// Interaction logic for RezervacijaKarata.xaml
    /// </summary>
    public partial class RezervacijaKarata : Page
    {
        static UcitavanjePodataka ucitavanje = new UcitavanjePodataka();
        public string klijentmail = UcitavanjePodataka.ulogovaniKorisnik.email;
        public RezervacijaKarata()
        {
            InitializeComponent();
            var ii = DateTime.Today.AddHours(10); // 14:00:00
            while (ii <= DateTime.Today.AddHours(24)) // 16:00:00
            {
                vreme.Items.Add(ii.TimeOfDay.ToString(@"hh\:mm"));
                ii = ii.AddMinutes(30);
            }

            foreach (var item in ucitavanje.stanice)
            {
                polaziste.Items.Add(item.mesto);
                odrediste.Items.Add(item.mesto);
                
            }
               
                //polaziste.ItemsSource = ucitavanje.stanice;
                //odrediste.ItemsSource = ucitavanje.stanice;
                linija.ItemsSource = ucitavanje.linije;
            
           
        }

        private void polaziste_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (polaziste.SelectedItem != null && odrediste.SelectedItem != null)
            {
                Stanica pol_stanica = ucitavanje.stanice.Find(x => x.mesto.Equals(polaziste.SelectedItem.ToString()));
                Stanica odr_stanica = ucitavanje.stanice.Find(x => x.mesto.Equals(odrediste.SelectedItem.ToString()));

                List<Linija> lin = new List<Linija>();
                bool prva = false;
                bool druga = false;
                int cena = 50;
                foreach (Linija linija in ucitavanje.linije)
                {
                    foreach (Stanica stanica in linija.stanice)
                    {
                        if (stanica.mesto.Equals(pol_stanica.mesto))
                        {
                            prva = true;

                        }
                        else if (prva && stanica.mesto.Equals(odr_stanica.mesto))
                        {
                            druga = true;
                        }
                        
                    }
                    if (prva && druga)
                    {
                        lin.Add(linija);
                        price.Text=cena+"";

                        
                    }
                    prva = false;
                    druga = false;
                }
                linija.ItemsSource = lin.ToArray();
                price.Text = cena + "";
            }
        }

        private void linija_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Console.WriteLine(linija.SelectedItem.ToString());
            string lines = linija.SelectedItem.ToString();
            
            foreach (var item in ucitavanje.linije)
            {
               
                if (item.naziv.Equals(lines))
                {
                    price.Text = item.cene[1]+"";
                }

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int id = 0;
            int brsed = 0;

            string s = datumVreme.Text.TrimEnd('.');
            foreach (var item in ucitavanje.karte)
            {
                id = Math.Max(item.id, id);
                id++;
                brsed = Math.Max(item.brojSedista, brsed);
                brsed += 10;
            }

            File.AppendAllText("..\\..\\Podaci\\Karte.txt", id + "|" + s + " " + vreme.SelectedItem.ToString() + "|" + linija.SelectedItem.ToString() + "|" + polaziste.SelectedItem.ToString() + "|" + odrediste.SelectedItem.ToString() + "|" + brsed + "|" + price.Text + "|" + "false" + "|" + klijentmail + "\n");
            MessageBox.Show("Uspesno ste rezervisali kartu");
        }
    }
}
