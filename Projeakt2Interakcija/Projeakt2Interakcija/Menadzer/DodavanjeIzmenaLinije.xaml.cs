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

        public DodavanjeIzmenaLinije()
        {
            InitializeComponent();
            SetProperties();
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

            if (rowView != null)
            {
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
                    if (sve)
                    {
                        Console.WriteLine(ime + "|" + voz + "|" + linija.stanice_string() + "|" + linija.cene_string() + "|" + linija.polasci_string());
                        ucitavanje.linije.Add(linija);
                        ucitavanje.UpisLinija();
                        this.Close();
                    }
                }
                else if (vozovi.SelectedItem == null) MessageBox.Show("Izaberite voz!");
            }
            else
            {
                if (naziv.Text != null && vozovi.SelectedItem != null)
                {
                    Linija linija = ucitavanje.linije.Find(x => x.naziv.Equals(rowView[0].ToString()));
                    int id = ucitavanje.linije.IndexOf(linija); 
                    ucitavanje.linije.Remove(linija);
                    int voz = ucitavanje.vozovi.Find(x => x.ToString().Equals(vozovi.SelectedItem.ToString())).id;
                    List<Stanica> stanice = new List<Stanica>();
                    List<double> cene = new List<double>();
                    List<DateTime> polasci = new List<DateTime>();
                    foreach (DataRow row in noveStanice.Rows)
                    {
                        if (row != null)
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
                    if (sve)
                    {
                        Console.WriteLine(ime + "|" + voz + "|" + linija.stanice_string() + "|" + linija.cene_string() + "|" + linija.polasci_string());
                        ucitavanje.linije.Insert(id, linija);
                        ucitavanje.UpisLinija();
                        this.Close();
                    }
                }
                else if (vozovi.SelectedItem == null) MessageBox.Show("Izaberite voz!");
            }
        }

    }
}
