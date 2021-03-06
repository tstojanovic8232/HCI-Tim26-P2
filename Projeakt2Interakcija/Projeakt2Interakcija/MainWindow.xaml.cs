using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Projeakt2Interakcija.Model;
using System.Windows.Controls.Primitives;
using System.Windows.Automation;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace Projeakt2Interakcija
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Klijent> korisnici;
        Menadzer menadzer;
        string username;
        string password;
        UcitavanjePodataka u=new UcitavanjePodataka();

        public MainWindow()
        {
            InitializeComponent();
            SetProperties();
            korisnici = new List<Klijent>();
        }

        void SetProperties()
        {
            this.Title = "Srbija Voz";

            this.MinHeight = 600;
            this.MinWidth = 800;
            Uri iconUri = new Uri("../../Slike/SrbijaVozLogo.jpg", UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);
        }

        private void RegistrujteSe_Click(object sender, RoutedEventArgs e)
        {
            Registracija r = new Registracija();
            r.Show();
            this.Hide();
            r.Closed += Prozor_Closed;
        }

        private void Prozor_Closed(object sender, EventArgs e)
        {
            this.Show();
        }

        private void PrijaviMe_Click(object sender, RoutedEventArgs e)
        {
            username = korisnciko_ime.Text;
            password = lozinka.Text;

            bool k_found = false; 
            bool m_found = false;

            // provera klijenti
            foreach (Klijent k in korisnici)
            {
                if (username.Equals(k.korisnicko_ime) && password.Equals(k.lozinka))
                {
                    k_found = true;
                    UcitavanjePodataka.ulogovaniKorisnik=k;
                    break;
                }

            }

            // provera menadzer
            if (username.Equals(menadzer.korisnicko_ime) && password.Equals(menadzer.lozinka))
            {
                m_found = true;
            }

            // proveri ko je ulogovan na osnovu flagova
            if (m_found)
            {
                //MessageBox.Show("Uspesno ste se ulogovali");
                MenadzerPocetna menadzerPocetna = new MenadzerPocetna();
                menadzerPocetna.Show();
                this.Close();
            }
            else if (k_found)
            {
                KlijentPocetna klijentPocetna = new KlijentPocetna();
                klijentPocetna.Show();
                this.Close();
            }
            else MessageBox.Show("Nepostojeci korisnik");

        }

        private void Window_GotFocus(object sender, RoutedEventArgs e)
        {
            using (StreamReader reader = File.OpenText("..\\..\\Podaci\\Korisnici.txt"))
            {

                while (!reader.EndOfStream)
                {

                    string line = reader.ReadLine();
                    if (line == "") break;
                    string[] korisnik = line.Split('|');

                    string ime = korisnik[0];
                    string prezime = korisnik[1];
                    string email = korisnik[2];
                    string datum = korisnik[3];
                    string korisnicko_ime = korisnik[4];
                    string lozinka = korisnik[5];
                    korisnici.Add(new Klijent(ime, prezime, email, datum, korisnicko_ime, lozinka));
                }
            }
            using (StreamReader reader = File.OpenText("..\\..\\Podaci\\Manager.txt"))
            {

                while (!reader.EndOfStream)
                {

                    string line = reader.ReadLine();
                    if (line == "") break;
                    string[] korisnik = line.Split('|');

                    string ime = korisnik[0];
                    string prezime = korisnik[1];
                    string email = korisnik[2];
                    string datum = korisnik[3];
                    string korisnicko_ime = korisnik[4];
                    string lozinka = korisnik[5];
                    menadzer = new Menadzer(ime, prezime, email, datum, korisnicko_ime, lozinka);
                }
            }
        }

        private void OcistiTekst(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = "";
        }

    }


}
