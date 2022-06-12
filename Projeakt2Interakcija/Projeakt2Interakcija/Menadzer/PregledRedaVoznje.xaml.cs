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

namespace Projeakt2Interakcija
{
    /// <summary>
    /// Interaction logic for PregledRedaVoznje.xaml
    /// </summary>
    public partial class PregledRedaVoznje : Window
    {

        UcitavanjePodataka podaci = new UcitavanjePodataka();
        public PregledRedaVoznje()
        {
            InitializeComponent();
            SetProperties();
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
            this.Close();
        }

        private void Prikazi_Click(object sender, RoutedEventArgs e)
        {
            List<RedVoznje> redVoznji = podaci.redoviVoznje;
            switch(linija.ToString().ToLower())
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
                    redVoznji = redVoznjiPoDanu(linija.ToString());
                    break;
                }
                default:
                {
                    redVoznji = redVoznjiPoLiniji(linija.ToString());
                    break;
                }
            }
            redovi_voznji
            redovi_voznji.ItemsSource = redVoznji;
            

            
        }

        private void OdjaviMe_Click(object sender, RoutedEventArgs e)
        {
            OdjavaMenadzer logout = new OdjavaMenadzer();
            logout.Show();
        }

        private void DodajRedVoznje_Click(object sender, RoutedEventArgs e)
        {
            DodajNoviRedVoznje dodajRedVoznje = new DodajNoviRedVoznje();
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
    }


}
