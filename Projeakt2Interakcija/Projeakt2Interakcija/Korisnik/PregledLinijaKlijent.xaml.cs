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
using Projeakt2Interakcija.Model;

namespace Projeakt2Interakcija
{
    /// <summary>
    /// Interaction logic for RezervacijaKarata.xaml
    /// </summary>
    public partial class PregledLinijaKlijent : Page
    {
        UcitavanjePodataka ucitavanje = new UcitavanjePodataka();

        public PregledLinijaKlijent()
        {
            InitializeComponent();
            polaziste.ItemsSource = ucitavanje.stanice;
            odrediste.ItemsSource = ucitavanje.stanice;
            //kartaTest.ItemsSource = ucitavanje.karte;
            linija.ItemsSource = ucitavanje.linije;

        }



        private void stanica_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (polaziste.SelectedItem != null && odrediste.SelectedItem != null)
            {
                Stanica pol_stanica = ucitavanje.stanice.Find(x => x.naziv.Equals(polaziste.SelectedItem.ToString()));
                Stanica odr_stanica = ucitavanje.stanice.Find(x => x.naziv.Equals(odrediste.SelectedItem.ToString()));

                List<Linija> lin = new List<Linija>();
                bool prva = false;
                bool druga = false;
                foreach (Linija linija in ucitavanje.linije)
                {
                    foreach(Stanica stanica in linija.stanice)
                    {
                        if(stanica.naziv.Equals(pol_stanica.ToString()))
                        {
                            prva = true;
                        }
                        else if(prva && stanica.naziv.Equals(odr_stanica.ToString()))
                        {
                            druga = true;
                        }
                    }
                    if(prva && druga)
                    {
                        lin.Add(linija);
                    }
                    prva = false;
                    druga = false;
                }
                linija.ItemsSource = lin.ToArray();

            }

        }

        private void txtChange(object sender, EventArgs e)
        {
            List<Linija> lin = new List<Linija>();
            if (polaziste.SelectedItem != null && odrediste.SelectedItem != null)
            {
                Stanica pol_stanica = ucitavanje.stanice.Find(x => x.naziv.Equals(polaziste.SelectedItem.ToString()));
                Stanica odr_stanica = ucitavanje.stanice.Find(x => x.naziv.Equals(odrediste.SelectedItem.ToString()));

                bool prva = false;
                bool druga = false;
                foreach (Linija linija in ucitavanje.linije)
                {
                    foreach (Stanica stanica in linija.stanice)
                    {
                        if (stanica.naziv.Equals(pol_stanica.ToString()))
                        {
                            prva = true;
                        }
                        else if (prva && stanica.naziv.Equals(odr_stanica.ToString()))
                        {
                            druga = true;
                        }
                    }
                    if (prva && druga)
                    {
                        lin.Add(linija);
                    }
                    prva = false;
                    druga = false;
                }

            }
            else lin = ucitavanje.linije;
            if (pretraga.Text != "")
            {
                List<Linija> filtered = lin.FindAll(x => x.naziv.Contains(pretraga.Text));
                linija.ItemsSource = filtered;
            }
            else linija.ItemsSource = lin;

        }
        private void tip_Checked(object sender, RoutedEventArgs e)
        {
            ucitavanje = new UcitavanjePodataka();
            List<Linija> lin = new List<Linija>();
            if (polaziste.SelectedItem != null && odrediste.SelectedItem != null)
            {
                Stanica pol_stanica = ucitavanje.stanice.Find(x => x.naziv.Equals(polaziste.SelectedItem.ToString()));
                Stanica odr_stanica = ucitavanje.stanice.Find(x => x.naziv.Equals(odrediste.SelectedItem.ToString()));

                bool prva = false;
                bool druga = false;
                foreach (Linija linija in ucitavanje.linije)
                {
                    foreach (Stanica stanica in linija.stanice)
                    {
                        if (stanica.naziv.Equals(pol_stanica.ToString()))
                        {
                            prva = true;
                        }
                        else if (prva && stanica.naziv.Equals(odr_stanica.ToString()))
                        {
                            druga = true;
                        }
                    }
                    if (prva && druga)
                    {
                        lin.Add(linija);
                    }
                    prva = false;
                    druga = false;
                }

            }
            else lin = ucitavanje.linije;
            CheckBox checkBox = sender as CheckBox;
            List<Linija> linije = new List<Linija>();
            if (soko.IsChecked == true)
            {
                linije.AddRange(lin.FindAll(x => x.getTipVoza().Contains("soko")));
            }
            if (regio.IsChecked == true)
            {
                linije.AddRange(lin.FindAll(x => x.getTipVoza().Contains("regio")));
            }
            if (brzi.IsChecked == true)
            {
                linije.AddRange(lin.FindAll(x => x.getTipVoza().Contains("brzi")));
            }
            if (inter.IsChecked == true)
            {
                linije.AddRange(lin.FindAll(x => x.getTipVoza().Contains("inter")));
            }
            if (stari.IsChecked == true)
            {
                linije.AddRange(lin.FindAll(x => x.getTipVoza().Contains("stari")));
            }
            linija.ItemsSource = linije;
        }

        private void uncheck(object sender, RoutedEventArgs e)
        {
            ucitavanje = new UcitavanjePodataka();
            List<Linija> lin = new List<Linija>();
            if (polaziste.SelectedItem != null && odrediste.SelectedItem != null)
            {
                Stanica pol_stanica = ucitavanje.stanice.Find(x => x.naziv.Equals(polaziste.SelectedItem.ToString()));
                Stanica odr_stanica = ucitavanje.stanice.Find(x => x.naziv.Equals(odrediste.SelectedItem.ToString()));

                bool prva = false;
                bool druga = false;
                foreach (Linija linija in ucitavanje.linije)
                {
                    foreach (Stanica stanica in linija.stanice)
                    {
                        if (stanica.naziv.Equals(pol_stanica.ToString()))
                        {
                            prva = true;
                        }
                        else if (prva && stanica.naziv.Equals(odr_stanica.ToString()))
                        {
                            druga = true;
                        }
                    }
                    if (prva && druga)
                    {
                        lin.Add(linija);
                    }
                    prva = false;
                    druga = false;
                }

            }
            else lin = ucitavanje.linije;
            CheckBox checkBox = sender as CheckBox;
            List<Linija> linije = new List<Linija>();
            if (soko.IsChecked == true)
            {
                linije.AddRange(lin.FindAll(x => x.getTipVoza().Contains("soko")));
            }
            if (regio.IsChecked == true)
            {
                linije.AddRange(lin.FindAll(x => x.getTipVoza().Contains("regio")));
            }
            if (brzi.IsChecked == true)
            {
                linije.AddRange(lin.FindAll(x => x.getTipVoza().Contains("brzi")));
            }
            if (inter.IsChecked == true)
            {
                linije.AddRange(lin.FindAll(x => x.getTipVoza().Contains("inter")));
            }
            if (stari.IsChecked == true)
            {
                linije.AddRange(lin.FindAll(x => x.getTipVoza().Contains("stari")));
            }
            if (soko.IsChecked == false && regio.IsChecked == false && brzi.IsChecked == false && inter.IsChecked == false && stari.IsChecked == false)
                linija.ItemsSource = lin;
            else linija.ItemsSource = linije;

        }
        private void ResetujFilter_Click(object sender, RoutedEventArgs e)
        {
            linija.ItemsSource = ucitavanje.linije;
            polaziste.SelectedIndex = -1;
            odrediste.SelectedIndex = -1;
            foreach(CheckBox check in tipovi.Children)
            {
                check.IsChecked = false;
            }
        }
    }
}
