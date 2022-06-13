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

        UcitavanjePodataka podaci = new UcitavanjePodataka();
        DataTable tabela_reda_voznji = new DataTable();
        public PregledRedaVoznje()
        {
            InitializeComponent();
            SetProperties();
            tabela_reda_voznji.Columns.Add("dan u sedmici", typeof(string));
            tabela_reda_voznji.Columns.Add("Linija", typeof(string));
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
                    redVoznji = podaci.redoviVoznje;
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
                    MessageBox.Show("redvoznji po danu");
                    break;
                }
                default:
                {
                    redVoznji = redVoznjiPoLiniji(linija.Text);
                    MessageBox.Show("redvoznji po liniji");
                    break;
                }
            }
            
            Dictionary<string, string> linije = new Dictionary<string, string>();
            for (int i = 0; i < redVoznji.Count(); i++)
            {
                //.Columns.Add(redVoznji[i].danUNedelji.ToString());

                tabela_reda_voznji.Rows.Add(redVoznji[i].danUNedelji.ToString(), redVoznji[i].linije_string());
                
            }
            MessageBox.Show("Prikaz!");
            redovi_voznji.ItemsSource = tabela_reda_voznji.DefaultView;
            
        }

        private void OdjaviMe_Click(object sender, RoutedEventArgs e)
        {
            OdjavaMenadzer logout = new OdjavaMenadzer();
            this.Close();
            logout.Show();
        }

        private void DodajRedVoznje_Click(object sender, RoutedEventArgs e)
        {
            DodajNoviRedVoznje dodajRedVoznje = new DodajNoviRedVoznje();
            this.Close();
            dodajRedVoznje.Show();
        }

        private List<RedVoznje> redVoznjiPoDanu(string linija)
        {
            List<RedVoznje> rezultat = new List<RedVoznje>();
            for (int i = 0; i < podaci.redoviVoznje.Count; i++)
            {
                if (podaci.redoviVoznje[i].danUNedelji.ToString().ToLower().Equals(linija.ToLower()))
                    rezultat.Add(podaci.redoviVoznje[i]);
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
    }


}
