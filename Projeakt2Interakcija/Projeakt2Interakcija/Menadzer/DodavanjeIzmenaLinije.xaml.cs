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
    public partial class DodavanjeIzmenaLinije : Window
    {
        static UcitavanjePodataka ucitavanje = new UcitavanjePodataka();
        DataRowView rowView;
        string nazivBox;
        public DodavanjeIzmenaLinije()
        {
            InitializeComponent();
            SetProperties();
            Uri iconUri = new Uri("../../Slike/SrbijaVozLogo.jpg", UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);

            rowView = PrikazLinija.selectedRow;
        }

        void SetProperties()
        {
            if (rowView == null) this.Title += "Dodaj novu liniju";
            else this.Title += "Izmeni liniju " + rowView[0].ToString();

            this.MinHeight = 600;
            this.MinWidth = 800;
            Uri iconUri = new Uri("../../Slike/SrbijaVozLogo.jpg", UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);
        }
        DataTable noveStanice = new DataTable();

        private void ucitaj(object sender, EventArgs e)
        {
            

            // vozovi
            List<string> vozs = new List<string>();
            foreach (var voz in ucitavanje.vozovi)
            {
                vozs.Add(voz.ToString());
            }

            


            // unos stanice
            noveStanice.Columns.Add("Mesto", typeof(string));
            noveStanice.Columns.Add("Polazak", typeof(string));
            noveStanice.Columns.Add("Cena", typeof(double));
            ok.Content = "DODAJ";
            if (rowView != null)
            {
                ok.Content = "IZMENI";
                naziv.Text = rowView[0].ToString();
                vozovi.SelectedItem = ucitavanje.vozovi.Find(x => x.getVozInfo().Equals(rowView[1])).ToString();

                Linija linija = ucitavanje.linije.Find(x => x.naziv.Equals(rowView[0].ToString()));
                List<Stanica> stanice = linija.stanice;
                List<double> cene = linija.cene;
                List<DateTime> polasci = linija.polasci;
                noveStanice.Clear();
                for (int i = 0; i < stanice.Count; i++)
                {
                    noveStanice.Rows.Add(stanice[i].getMesto(), polasci[i].ToShortTimeString(), cene[i].ToString());
                }
            }
            else { noveStanice.Clear(); }
            // ItemsSource
            unosStanica.ItemsSource = noveStanice.DefaultView;
            vozovi.ItemsSource = vozs.ToArray();
            nazivBox = naziv.Text;

        }


        private void OcistiTekst(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = "";
        }
        private void Ok_Click(object sender, RoutedEventArgs e)
        {   
            if(rowView == null) 
            { 
                if (naziv.Text != null && vozovi.SelectedItem != null)
                {
                    Linija linija = new Linija();
                    int voz = ucitavanje.vozovi.Find(x => x.ToString().Equals(vozovi.SelectedItem.ToString())).id;
                    List<Stanica> stanice = new List<Stanica>();
                    List<double> cene = new List<double>();
                    List<DateTime> polasci = new List<DateTime>();
                    foreach (DataRow row in noveStanice.Rows)
                    {
                        if(row != null)
                        {
                            stanice.Add(ucitavanje.stanice.Find(x => x.mesto.Equals(row[0].ToString())));
                            polasci.Add(DateTime.ParseExact(row[1].ToString(), "H:m", null));
                            cene.Add(double.Parse(row[2].ToString()));
                        }

                    }
                    bool sve = false;
                    if (stanice.Count > 1) sve = true; 
                    else
                    {
                        sve = false;
                        MessageBox.Show("Unesite bar dve stanice!");
                    }
                    if (polasci.Count > 1) sve = true; 
                    else
                    {
                        sve = false;
                        MessageBox.Show("Unesite vreme za svaku stanicu!");
                    }
                    if (cene.Count > 1) sve = true; 
                    else
                    {
                        sve = false;
                        MessageBox.Show("Unesite cenu za svaku stanicu!");
                    }
                    string ime = naziv.Text;
                    linija.naziv = ime;
                    linija.Voz = ucitavanje.vozovi.Find(x => x.id.Equals(voz));
                    linija.stanice = stanice;
                    linija.polasci = polasci;
                    linija.cene = cene;
                    bool exists = ucitavanje.linije.Exists(x => x.naziv.Equals(linija.naziv));
                    if (sve && !exists)
                    {
                        Console.WriteLine(ime + "|" + voz + "|" + linija.stanice_string() + "|" + linija.cene_string() + "|" + linija.polasci_string());
                        ucitavanje.linije.Add(linija);
                        ucitavanje.UpisLinija();
                        this.Close();
                    }
                    else if(exists)
                    {
                        MessageBox.Show("Ova linija već postoji!");
                    }
                }
                else if (vozovi.SelectedItem == null) MessageBox.Show("Izaberite voz!");
            }
            else
            {
                /* izmena naziva -> update red voznje, karte
                 * izmena ostalog -> zabrana ako postoje karte!
                 */
                if (naziv.Text != null && vozovi.SelectedItem != null)
                {
                    Linija linija = ucitavanje.linije.Find(x => x.naziv.Equals(rowView[0].ToString()));
                    string ime = naziv.Text;
                    
                    int id = ucitavanje.linije.IndexOf(linija);
                    int voz = ucitavanje.vozovi.Find(x => x.ToString().Equals(vozovi.SelectedItem.ToString())).id;
                    linija.Voz = ucitavanje.vozovi.Find(x => x.id.Equals(voz));
                    List<Stanica> stanice = new List<Stanica>();
                    List<double> cene = new List<double>();
                    List<DateTime> polasci = new List<DateTime>();

                    foreach (DataRow row in noveStanice.Rows)
                    {
                        if (row != null && row[0] != null && row[1] != null && row[2] != null)
                        {
                            stanice.Add(ucitavanje.stanice.Find(x => x.mesto.Equals(row[0].ToString())));
                            polasci.Add(DateTime.ParseExact(row[1].ToString(), "H:m", null));
                            cene.Add(double.Parse(row[2].ToString()));
                        }

                    }
                    bool sve = false;
                    if (stanice.Count > 1) sve = true;
                    else
                    {
                        sve = false;
                        MessageBox.Show("Unesite bar dve stanice!");
                    }
                    if (polasci.Count > 1) sve = true;
                    else
                    {
                        sve = false;
                        MessageBox.Show("Unesite vreme za svaku stanicu!");
                    }
                    if (cene.Count > 1) sve = true;
                    else
                    {
                        sve = false;
                        MessageBox.Show("Unesite cenu za svaku stanicu!");
                    }

                    bool allowed = false;
                    string staniceLinija = linija.stanice_string();
                    string staniceTabela = "";
                    foreach (Stanica stanica in stanice)
                    {
                        staniceTabela += stanica.mesto + ",";
                    }
                    staniceTabela = staniceTabela.Substring(0, staniceTabela.Length - 1);

                    string polasciLinija = linija.polasci_string();
                    string polasciTabela = "";
                    foreach (DateTime polazak in polasci)
                    {
                        polasciTabela += polazak.ToShortTimeString() + ",";
                    }
                    polasciTabela = polasciTabela.Substring(0, polasciTabela.Length - 1);
                    if (staniceLinija.Equals(staniceTabela) && polasciLinija.Equals(polasciTabela))
                    {
                        allowed = true;
                    }
                    else
                    {
                        List<Karta> karte = ucitavanje.karte.FindAll(x => x.linija.naziv.Equals(linija.naziv));
                        if (karte.Count == 0 || karte == null) allowed = true;
                        else allowed = false;
                    }
                    linija.naziv = ime;
                    if (!ime.Equals(nazivBox))
                    {
                        foreach (var item in ucitavanje.redoviVoznje)
                        {
                            if (item.linije.Contains(linija))
                            {
                                int index = item.linije.FindIndex(x => x.naziv.Equals(nazivBox));
                                item.linije[index] = linija;
                            }
                        }
                        ucitavanje.UpisRedovaVoznje();
                        foreach (var item in ucitavanje.karte)
                        {
                            if (item.linija.Equals(linija))
                            {
                                item.linija = linija;
                            }
                        }
                        ucitavanje.UpisKarata();
                    }
                    if (allowed)
                    {
                        ucitavanje.linije.Remove(linija);

                        linija.stanice = stanice;
                        linija.polasci = polasci;
                        linija.cene = cene;
                        if (sve)
                        {
                            ucitavanje.linije.Insert(id, linija);
                            ucitavanje.UpisLinija();
                            this.Close();
                        }
                    }
                    else MessageBox.Show("Mozete promeniti samo naziv jer postoje karte!");
                }
                else if (vozovi.SelectedItem == null) MessageBox.Show("Izaberite voz!");
            }
        }
        string ime = "";
    }
}
