using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "d.M.yyyy";
            Thread.CurrentThread.CurrentCulture = ci;

            foreach (var item in ucitavanje.stanice)
            {
                polaziste.Items.Add(item.mesto);
                odrediste.Items.Add(item.mesto);
                
            }
               
                //polaziste.ItemsSource = ucitavanje.stanice;
                //odrediste.ItemsSource = ucitavanje.stanice;
            
           
        }

        private DanUNedelji danUNedelji(DayOfWeek day)
        {

            if (day.Equals(DayOfWeek.Monday)) return DanUNedelji.Ponedeljak;
            else if (day.Equals(DayOfWeek.Tuesday)) return DanUNedelji.Utorak;
            else if (day.Equals(DayOfWeek.Wednesday)) return DanUNedelji.Sreda;
            else if (day.Equals(DayOfWeek.Thursday)) return DanUNedelji.Cetvrtak;
            else if (day.Equals(DayOfWeek.Friday)) return DanUNedelji.Petak;
            else if (day.Equals(DayOfWeek.Saturday)) return DanUNedelji.Subota;
            else if (day.Equals(DayOfWeek.Sunday)) return DanUNedelji.Nedelja;

            return DanUNedelji.Ponedeljak;
        }

        private void polaziste_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (polaziste.SelectedItem != null && odrediste.SelectedItem != null && datumVreme.SelectedDate != null)
            {
                linija.ItemsSource = ucitavanje.linije;
                linija.SelectedIndex = -1;
                Stanica pol_stanica = ucitavanje.stanice.Find(x => x.mesto.Equals(polaziste.SelectedItem.ToString()));
                Stanica odr_stanica = ucitavanje.stanice.Find(x => x.mesto.Equals(odrediste.SelectedItem.ToString()));
                DateTime dateTime = datumVreme.SelectedDate.Value;
                DanUNedelji dan = danUNedelji(dateTime.DayOfWeek);
                RedVoznje redVoznje = ucitavanje.redoviVoznje.Find(x => x.danUNedelji.Equals(dan));
                List<Linija> lin = new List<Linija>();
                bool prva = false;
                bool druga = false;
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
                        if (redVoznje.linije.Contains(linija)) lin.Add(linija);
                        else continue;
                    }
                    prva = false;
                    druga = false;
                }
                linija.ItemsSource = lin.ToArray();
            }
        }

        DateTime vremePolaska = new DateTime();

        double cena = 0;

        private void linija_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(linija.SelectedItem != null)
            {
                Console.WriteLine(linija.SelectedItem.ToString());
                string lines = linija.SelectedItem.ToString();
                string voz = "";
                DateTime vremeDolaska = new DateTime();
                foreach (var item in ucitavanje.linije)
                {
                    if (item.naziv.Equals(lines))
                    {
                        int pol_id = item.stanice.FindIndex(x => x.mesto.Equals(polaziste.SelectedItem.ToString()));
                        int odr_id = item.stanice.FindIndex(x => x.mesto.Equals(odrediste.SelectedItem.ToString()));
                        vremePolaska = item.polasci[pol_id];
                        vremeDolaska = item.polasci[odr_id];
                        voz = item.getTipVoza();
                        for (int i = odr_id; i > pol_id; i--)
                        {
                            cena += item.cene[i];
                        }
                        break;
                    }
                }
                price.Text = "Ukupno: " + cena + " din";
                time.Text = "Polazi u " + vremePolaska.ToShortTimeString();
                tip.Text = "Tip voza: " + voz;
                dolazak.Text = "Dolazak na odredište: " + vremeDolaska.ToShortTimeString();
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

            File.AppendAllText("..\\..\\Podaci\\Karte.txt", id + "|" + s + " " + vremePolaska.ToShortTimeString() + "|" + linija.SelectedItem.ToString() + "|" + polaziste.SelectedItem.ToString() + "|" + odrediste.SelectedItem.ToString() + "|" + brsed + "|" + cena + "|" + "false" + "|" + klijentmail + "\n");
            MessageBox.Show("Uspesno ste rezervisali kartu");
        }

    }
}
