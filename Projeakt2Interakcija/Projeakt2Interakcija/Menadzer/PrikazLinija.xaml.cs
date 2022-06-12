using Projeakt2Interakcija.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Projeakt2Interakcija
{
    /// <summary>
    /// Interaction logic for PrikazVozova.xaml
    /// </summary>
    public partial class PrikazLinija : Window
    {
        static UcitavanjePodataka ucitavanje = new UcitavanjePodataka();

        public PrikazLinija()
        {
            InitializeComponent();
        }
        DataTable noveStanice = new DataTable();
        DataTable table = new DataTable();
        DataTable tableSearch = new DataTable();

        private void ucitaj(object sender, EventArgs e)
        {
            table.Clear();

            // vozovi
            List<string> vozs = new List<string>();
            foreach (var voz in ucitavanje.vozovi)
            {
                vozs.Add(voz.ToString());
            }

            // linije
            table.Columns.Add("Naziv", typeof(string));
            table.Columns.Add("Voz", typeof(string));
            table.Columns.Add("Stanice", typeof(string));
            table.Columns.Add("Cene po stanici", typeof(string));
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



            // unos stanice
            noveStanice.Columns.Add("Mesto", typeof(string));
            noveStanice.Columns.Add("Polazak", typeof(string));
            noveStanice.Columns.Add("Cena", typeof(double));

            // ItemsSource
            datagrid.ItemsSource = table.DefaultView;
            unosStanica.ItemsSource = noveStanice.DefaultView;
            vozovi.ItemsSource = vozs.ToArray();
            polaziste.ItemsSource = ucitavanje.stanice;
            odrediste.ItemsSource = ucitavanje.stanice;

            // pretraga i filter
            tableSearch.Columns.Add("Naziv", typeof(string));
            tableSearch.Columns.Add("Voz", typeof(string));
            tableSearch.Columns.Add("Stanice", typeof(string));
            tableSearch.Columns.Add("Cene po stanici", typeof(string));
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
            if (naziv.Text != null && vozovi.SelectedItem != null)
            {
                int voz = ucitavanje.vozovi.Find(x => x.ToString().Equals(vozovi.SelectedItem.ToString())).id;
                string stanice = "";
                string cene = "";
                string polasci = "";
                foreach (DataRow row in noveStanice.Rows)
                {
                    var stanica = row[0].ToString();
                    if (!stanica.Equals(""))
                        stanice += stanica + ",";

                    var polazak = row[1].ToString();
                    if (!polazak.Equals("")) polasci += polazak + ",";

                    var cena = row[2].ToString();
                    if (!cena.Equals("")) cene += cena + ",";

                }
                bool sve = false;
                if (stanice.Length - 1 > 0 && stanice.Split(',').Length > 2)
                {
                    stanice = stanice.Substring(0, stanice.Length - 1);
                    sve = true;
                }
                else
                {
                    sve = false;
                    MessageBox.Show("Unesite bar dve stanice!");
                }
                if (polasci.Length - 1 > 0 && polasci.Split(',').Length > 2)
                {
                    polasci = polasci.Substring(0, polasci.Length - 1);
                    sve = true;
                }
                else
                {
                    sve = false;
                    MessageBox.Show("Unesite vreme za svaku stanicu!");
                }
                if (cene.Length - 1 > 0 && cene.Split(',').Length > 2)
                {
                    sve = false;
                    cene = cene.Substring(0, cene.Length - 1);
                }
                else
                {
                    sve = false;
                    MessageBox.Show("Unesite cenu za svaku stanicu!");
                }

                string ime = naziv.Text;
                if (sve) Console.WriteLine(ime + "|" + voz + "|" + stanice + "|" + cene + "|" + polasci);
            }
            else if (vozovi.SelectedItem == null) MessageBox.Show("Izaberite voz!");
        }


        private void Izmeni_Click(object sender, RoutedEventArgs e)
        {
            DataRowView row = datagrid.SelectedItem as DataRowView;

            if (row != null)
            {

                int id = datagrid.SelectedIndex;

                Console.WriteLine(table.Rows.Count);
                row.Delete();
                List<string> quotelist = File.ReadAllLines("..\\..\\Podaci\\Vozovi.txt").ToList();

                quotelist.RemoveAt(id);

                File.WriteAllLines("..\\..\\Podaci\\Vozovi.txt", quotelist.ToArray());
                noveStanice.Clear();
            }
        }

        private void Obrisi_Click(object sender, RoutedEventArgs e)
        {
            DataRowView row = datagrid.SelectedItem as DataRowView;

            if (row != null)
            {

                int ind = datagrid.SelectedIndex;

                List<string> quotelist = File.ReadAllLines("..\\..\\Podaci\\Vozovi.txt").ToList();
                row.Delete();

                //foreach (Voz v in vozs)
                //{
                //    if (v.id == ind)
                //    {
                //        v.id = Int32.Parse(id.Text);
                //        v.tip = tip.Text;
                //        v.brojSedista = br.Text;
                //        Console.WriteLine(v.ToString());

                //    }
                //}

                System.IO.File.WriteAllLines("..\\..\\Podaci\\Vozovi.txt", quotelist.ToArray());

            }
        }

        private void datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView row = datagrid.SelectedItem as DataRowView;
            if (row != null)
            {
                naziv.Text = row[0].ToString();
                vozovi.SelectedItem = ucitavanje.vozovi.Find(x => x.getVozInfo().Equals(row[1])).ToString();

                Linija linija = ucitavanje.linije.Find(x => x.naziv.Equals(row[0].ToString()));
                List<Stanica> stanice = linija.stanice;
                List<double> cene = linija.cene;
                List<DateTime> polasci = linija.polasci;
                noveStanice.Clear();
                for (int i = 0; i < stanice.Count; i++)
                {
                    noveStanice.Rows.Add(stanice[i].getMesto(), polasci[i].ToShortTimeString(), cene[i].ToString());
                }
            }
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

    }
}
