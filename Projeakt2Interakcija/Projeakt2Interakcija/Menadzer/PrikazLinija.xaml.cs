using Projeakt2Interakcija.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Projeakt2Interakcija
{
    /// <summary>
    /// Interaction logic for PrikazVozova.xaml
    /// </summary>
    public partial class PrikazLinija : Window
    {
        UcitavanjePodataka ucitavanje;

        public PrikazLinija()
        {
            InitializeComponent();
            SetProperties();
            ucitavanje = new UcitavanjePodataka();

            table.Columns.Add("Naziv", typeof(string));
            table.Columns.Add("Voz", typeof(string));
            table.Columns.Add("Stanice", typeof(string));
            table.Columns.Add("Cene po stanici", typeof(string));

            // pretraga i filter
            tableSearch.Columns.Add("Naziv", typeof(string));
            tableSearch.Columns.Add("Voz", typeof(string));
            tableSearch.Columns.Add("Stanice", typeof(string));
            tableSearch.Columns.Add("Cene po stanici", typeof(string));
        }


        void SetProperties()
        {
            this.Title = "Srbija Voz - Pregled voznih linija";

            this.MinHeight = 600;
            this.MinWidth = 800;
            Uri iconUri = new Uri("../../Slike/SrbijaVozLogo.jpg", UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);
        }

        DataTable table = new DataTable();
        DataTable tableSearch = new DataTable();
        public static DataRowView selectedRow;

        private void ucitaj(object sender, EventArgs e)
        {
            table.Clear();
            ucitavanje = new UcitavanjePodataka();
            // linije

            foreach (var item in ucitavanje.linije)
            {
                string s = "";
                string cene = "";
                foreach (var st in item.stanice)
                {
                    s += st.ToString() + ", ";
                }
                foreach (var c in item.cene)
                {
                    cene += c.ToString() + ", ";
                }
                s = s.Substring(0, s.Length - 2);
                cene = cene.Substring(0, cene.Length - 2);
                table.Rows.Add(item.naziv, item.Voz.getVozInfo(), s, cene);
            }




            // ItemsSource
            datagrid.ItemsSource = table.DefaultView;
            polaziste.ItemsSource = ucitavanje.stanice;
            odrediste.ItemsSource = ucitavanje.stanice;

            
        }

        private void txtChange(object sender, EventArgs e)
        {

            if (pretraga.Text != "")
            {
                List<Linija> filtered = ucitavanje.linije.FindAll(x => x.naziv.Contains(pretraga.Text));
                tableSearch.Clear();
                UcitajListu(filtered);
            }

        }

        private void OcistiTekst(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = "";
        }
        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            selectedRow = null;
            DodavanjeIzmenaLinije dodavanje = new DodavanjeIzmenaLinije();
            dodavanje.Show();
            dodavanje.Closed += this.ucitaj;
        }


        private void Izmeni_Click(object sender, RoutedEventArgs e)
        {
            selectedRow = datagrid.SelectedItem as DataRowView;

            if (selectedRow != null)
            {
                DodavanjeIzmenaLinije izmena = new DodavanjeIzmenaLinije();
                izmena.Show();
                izmena.Closed += this.ucitaj;
            }
            else MessageBox.Show("Izaberite liniju koju želite da izmenite!");
        }

        private void Obrisi_Click(object sender, RoutedEventArgs e)
        {
            DataRowView row = datagrid.SelectedItem as DataRowView;

            if (row != null)
            {
                int id = datagrid.SelectedIndex;
                Linija linija = ucitavanje.linije.Find(x => x.naziv.Equals(row[0].ToString()));
                table.Rows.RemoveAt(id);
                ucitavanje.linije.Remove(linija);
                ucitavanje.UpisLinija();

            }
        }

        private void datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedRow = datagrid.SelectedItem as DataRowView;
        }



        private void stanica_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ucitavanje = new UcitavanjePodataka();
            if (polaziste.SelectedItem != null && odrediste.SelectedItem != null)
            {
                Stanica pol_stanica = ucitavanje.stanice.Find(x => x.naziv.Equals(polaziste.SelectedItem.ToString()));
                Stanica odr_stanica = ucitavanje.stanice.Find(x => x.naziv.Equals(odrediste.SelectedItem.ToString()));

                List<Linija> lin = new List<Linija>();
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
                tableSearch.Clear();
                UcitajListu(lin);

            }
        }

        private void UcitajListu(List<Linija> list)
        {

            if (list.Count > 0)
            {

                foreach (var item in list)
                {
                    string s = "";
                    string cene = "";
                    foreach (var st in item.stanice)
                    {
                        s += st.ToString() + ", ";
                    }
                    foreach (var c in item.cene)
                    {
                        cene += c.ToString() + ", ";
                    }
                    s = s.Substring(0, s.Length - 2);
                    cene = cene.Substring(0, cene.Length - 2); tableSearch.Rows.Add(item.naziv, item.Voz.getVozInfo(), s, cene);
                }
                datagrid.ItemsSource = tableSearch.DefaultView;
            }

            else
            {
                datagrid.ItemsSource = table.DefaultView;
            }
        }

        private void ResetujFilter_Click(object sender, RoutedEventArgs e)
        {
            datagrid.ItemsSource = table.DefaultView;
            polaziste.SelectedIndex = -1;
            odrediste.SelectedIndex = -1;
            pretraga.Text = "";
            foreach (CheckBox checkBox in tipovi.Children)
            {
                checkBox.IsChecked = false;
            }
        }

        private void tip_Checked(object sender, RoutedEventArgs e)
        {
            ucitavanje = new UcitavanjePodataka();
            CheckBox checkBox = sender as CheckBox;
            Linija linija = new Linija();
            List<Linija> lin = new List<Linija>();
            foreach (DataRow row in tableSearch.Rows)
            {
                linija = ucitavanje.linije.Find(x => x.naziv.Equals(row[0].ToString()));
                lin.Add(linija);
            }
            if (checkBox.Name.Equals("soko"))
            {
                if (ucitavanje.linije.FindAll(x => x.getTipVoza().Equals("soko")).Count > 0)
                    lin.AddRange(ucitavanje.linije.FindAll(x => x.getTipVoza().Equals("soko")));
            }
            else if (checkBox.Name.Equals("regio"))
            {
                if (ucitavanje.linije.FindAll(x => x.getTipVoza().Equals("regio voz")).Count > 0)
                    lin.AddRange(ucitavanje.linije.FindAll(x => x.getTipVoza().Equals("regio voz")));
            }
            else if (checkBox.Name.Equals("brzi"))
            {
                if (ucitavanje.linije.FindAll(x => x.getTipVoza().Equals("brzi voz")).Count > 0)
                    lin.AddRange(ucitavanje.linije.FindAll(x => x.getTipVoza().Equals("brzi voz")));
            }
            else if (checkBox.Name.Equals("inter"))
            {
                if (ucitavanje.linije.FindAll(x => x.getTipVoza().Equals("inter city")).Count > 0)
                    lin.AddRange(ucitavanje.linije.FindAll(x => x.getTipVoza().Equals("inter city")));

            }
            else if (checkBox.Name.Equals("stari"))
            {
                if (ucitavanje.linije.FindAll(x => x.getTipVoza().Equals("stari voz")).Count > 0)
                    lin.AddRange(ucitavanje.linije.FindAll(x => x.getTipVoza().Equals("stari voz")));
            }
            tableSearch.Clear();
            if (lin.Count > 0) UcitajListu(lin);
            else datagrid.ItemsSource = null;
        }

        private void uncheck(object sender, RoutedEventArgs e)
        {
            ucitavanje = new UcitavanjePodataka();
            CheckBox checkBox = (CheckBox)sender;
            Linija linija = new Linija();
            List<Linija> lin = new List<Linija>();
            foreach (DataRow row in tableSearch.Rows)
            {
                linija = ucitavanje.linije.Find(x => x.naziv.Equals(row[0].ToString()));
                lin.Add(linija);
            }
            if (checkBox.Name.Equals("soko"))
            {
                lin.RemoveAll(x => x.getTipVoza().Equals("soko"));
            }
            else if (checkBox.Name.Equals("regio"))
            {
                lin.RemoveAll(x => x.getTipVoza().Equals("regio voz"));
            }
            else if (checkBox.Name.Equals("brzi"))
            {
                lin.RemoveAll(x => x.getTipVoza().Equals("brzi voz"));
            }
            else if (checkBox.Name.Equals("inter"))
            {
                lin.RemoveAll(x => x.getTipVoza().Equals("inter city"));
            }
            else if (checkBox.Name.Equals("stari"))
            {
                lin.RemoveAll(x => x.getTipVoza().Equals("stari voz"));
            }
            tableSearch.Clear();
            UcitajListu(lin);
        }


        private void NazadNaPocetnu_Click(object sender, RoutedEventArgs e)
        {
            MenadzerPocetna pocetna = new MenadzerPocetna();
            this.Close();
            pocetna.Show();
        }


        private void OdjaviMe_Click(object sender, RoutedEventArgs e)
        {
            OdjavaMenadzer logout = new OdjavaMenadzer();
            this.Close();
            logout.Show();
        }
    }
}
