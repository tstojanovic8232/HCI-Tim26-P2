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
using Projeakt2Interakcija.Model;
using System.Data;


namespace Projeakt2Interakcija
{
    /// <summary>
    /// Interaction logic for PregledRedaVoznje.xaml
    /// </summary>
    public partial class PregledRedaVoznje : Window
    {

        public static UcitavanjePodataka podaci = new UcitavanjePodataka();
        DataTable tabela_reda_voznji = new DataTable();
        public static DataRowView odabraniRed;
        public PregledRedaVoznje()
        {
            InitializeComponent();
            SetProperties();

            
            tabela_reda_voznji.Columns.Add("dan u sedmici", typeof(string));
            tabela_reda_voznji.Columns.Add("Linija", typeof(string));
            tabela_reda_voznji.Columns.Add("Vrijeme polaska:", typeof(string));
            
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
            this.Close();
            pocetna.Show();
        }

        private void Prikazi_Click(object sender, RoutedEventArgs e)
        {
            tabela_reda_voznji.Clear();
            
            //redovi_voznji.Items.Clear();
            List<RedVoznje> redVoznji = podaci.redoviVoznje;
            switch(linija.Text.ToLower())
            {
                case "":
                {
                    redVoznji = new List<RedVoznje>();
                    for(int i = 0; i< podaci.redoviVoznje.Count;i++)
                    {
                            for(int j = 0; j < podaci.redoviVoznje[i].linije.Count; j++)
                            {
                                List<Linija> voznaLinija = new List<Linija>();
                                voznaLinija.Add(podaci.redoviVoznje[i].linije[j]);
                                redVoznji.Add(new RedVoznje(podaci.redoviVoznje[i].danUNedelji, voznaLinija));
                            }
                    }
                    //redVoznji = podaci.redoviVoznje;

                    break;
                }
                case "ponedeljak":
                case "utorak":
                case "sreda":
                case "cetvrtak":
                case "petak":
                case "subota":
                case "nedelja":
                {
                    redVoznji = redVoznjiPoDanu(linija.Text);
                    //MessageBox.Show("redvoznji po danu");
                    break;
                }
                default:
                {
                    redVoznji = redVoznjiPoLiniji(linija.Text);
                    //MessageBox.Show("redvoznji po liniji");
                    break;
                }
            }
            
            Dictionary<string, string> linije = new Dictionary<string, string>();
            for (int i = 0; i < redVoznji.Count(); i++)
            {
                //.Columns.Add(redVoznji[i].danUNedelji.ToString());

                tabela_reda_voznji.Rows.Add(redVoznji[i].danUNedelji.ToString(), redVoznji[i].linije_string(), redVoznji[i].linije[0].polasci[0].ToShortTimeString());
                
            }
            //MessageBox.Show("Prikaz!");
            redovi_voznji.ItemsSource = tabela_reda_voznji.DefaultView;
            
        }

        private void OdjaviMe_Click(object sender, RoutedEventArgs e)
        {
            OdjavaMenadzer logout = new OdjavaMenadzer();
            logout.Show();
            this.Close();
        }

        private void DodajRedVoznje_Click(object sender, RoutedEventArgs e)
        {
            DodajNoviRedVoznje dodajRedVoznje = new DodajNoviRedVoznje();
            dodajRedVoznje.Show();
            this.Close();
        }

        private List<RedVoznje> redVoznjiPoDanu(string linija)
        {
            List<RedVoznje> rezultat = new List<RedVoznje>();
            for (int i = 0; i < podaci.redoviVoznje.Count; i++)
            {
                for(int j =0; j< podaci.redoviVoznje[i].linije.Count; j++)
                    if (podaci.redoviVoznje[i].danUNedelji.ToString().ToLower().Equals(linija.ToLower()))
                    {
                        List<Linija> linije = new List<Linija>();
                        linije.Add(podaci.redoviVoznje[i].linije[j]);
                        rezultat.Add(new RedVoznje(podaci.redoviVoznje[i].danUNedelji, linije));
                    }
            }
            return rezultat;
        }

        private List<RedVoznje> redVoznjiPoLiniji(string linija)
        {
            List<RedVoznje> rezultat = new List<RedVoznje>();
            for (int i = 0; i < podaci.redoviVoznje.Count; i++)
            {
                for (int j = 0; j < podaci.redoviVoznje[i].linije.Count; j++)
                    if (podaci.redoviVoznje[i].linije[j].naziv.ToString().ToLower().Equals(linija.ToLower()))
                    {
                        List<Linija> linije = new List<Linija>();
                        linije.Add(podaci.redoviVoznje[i].linije[j]);
                        rezultat.Add(new RedVoznje(podaci.redoviVoznje[i].danUNedelji, linije));
                    }
                    
            }
            return rezultat;
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void IzmijeniRedVoznje_Click(object sender, RoutedEventArgs e)
        {

            if (redovi_voznji.SelectedIndex == -1)
            {
                MessageBox.Show("Morate odabrati red Voznje koji zelite da mijenjate!");
            }
            else
            {
                odabraniRed = redovi_voznji.SelectedItem as DataRowView;
                IzmijeniRedVoznje izmijeniRed = new IzmijeniRedVoznje();
                izmijeniRed.Show();

                /*
                DataRowView redVoznjeTabela = redovi_voznji.SelectedItem as DataRowView;
                
                if (MessageBox.Show("Sigurni ste da želite da brišete?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                {
                    //vrati nazad na pregled
                }
                else
                {

                }
                {
                    for (int i = 0; i < podaci.redoviVoznje.Count; i++)
                    {
                        
                        if (redVoznjeTabela!=null &&
                        podaci.redoviVoznje[i].danUNedelji.ToString().Equals(redVoznjeTabela[0] ) )
                        for (int j = 0; j < podaci.redoviVoznje[i].linije.Count; j++)
                        {
                            if (podaci.redoviVoznje[i].linije[j].naziv.Equals(redVoznjeTabela[1]) )
                            {
                                    podaci.redoviVoznje[i].linije.RemoveAt(j);
                                    podaci.UpisRedovaVoznje();
                                    Prikazi_Click(this, e);
                                    obrisan = true;
                                    break;
                            }
                                
                        }
                        if (obrisan) break;
                        else continue;

                    } 
                }
                */
            }
        }

        private void ObrisiRedVoznje_Click(object sender, RoutedEventArgs e)
        {
            bool obrisan = false;
            if (redovi_voznji.SelectedIndex == -1)
            {
                MessageBox.Show("Morate odabrati red Voznje koji zelite da brišete!");
            }
            else
            {

                DataRowView redVoznjeTabela = redovi_voznji.SelectedItem as DataRowView;
                
                if (MessageBox.Show("Sigurni ste da želite da brišete?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                {
                    //vrati nazad na pregled
                }
                else
                {
                    for (int i = 0; i < podaci.redoviVoznje.Count; i++)
                    {

                        if (redVoznjeTabela != null &&
                        podaci.redoviVoznje[i].danUNedelji.ToString().Equals(redVoznjeTabela[0]))
                            for (int j = 0; j < podaci.redoviVoznje[i].linije.Count; j++)
                            {
                                if (podaci.redoviVoznje[i].linije[j].naziv.Equals(redVoznjeTabela[1]))
                                {
                                    podaci.redoviVoznje[i].linije.RemoveAt(j);
                                    podaci.UpisRedovaVoznje();
                                    Prikazi_Click(this, e);
                                    obrisan = true;
                                    break;
                                }

                            }
                        if (obrisan) break;
                        else continue;

                    }
                }
            }
        }
    }


}
