using System;
using System.Collections.Generic;
using System.IO;
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

namespace Projeakt2Interakcija
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Klijent> korisnici;
        string text5;
        string text6;
        public MainWindow()
        {
            InitializeComponent();
            korisnici = new List<Klijent>();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Registracija r = new Registracija();
            r.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            text5 = korisnciko_ime.Text;
            text6 = lozinka.Text;


            bool found = false;
            foreach (Klijent k in korisnici)
            {
                if (text5.Equals(k.korisnicko_ime) && text6.Equals(k.lozinka))
                {
                    found = true;
                    break;
                }

            }
            if (found) MessageBox.Show("Uspesno ste se ulogovali");
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
        }
    }
}
