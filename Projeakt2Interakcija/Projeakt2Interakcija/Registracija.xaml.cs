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
using System.Windows.Shapes;

namespace Projeakt2Interakcija
{
    /// <summary>
    /// Interaction logic for Registracija.xaml
    /// </summary>
    public partial class Registracija : Window
    {
        String text1 = "";
        String text2 = "";
        String text3 = "";
        String text4 = "";
        String text5 = "";
        String text6 = "";
        List<Klijent> korisnici;

        public Registracija()
        {
            
            InitializeComponent();
            korisnici = new List<Klijent>();
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            text1 = ime.Text;
            text2 = prezime.Text;
            text3 = email.Text;
            text4 = datum_rodjenja.Text;
            text5 = korisnicko_ime.Text;
            text6 = lozinka.Text;

            Klijent k1 = new Klijent(text1, text2, text3, text4, text5, text6);

            bool exists = false;
            foreach (Klijent k in korisnici)
            {
                if (k1.email.Equals(k.email))
                {
                    MessageBox.Show("Korisnik sa ovim mejlom vec postoji");
                    exists = true;
                    break;
                }

            }
            if (!exists) {
                korisnici.Add(k1);
                using (StreamWriter sr = new StreamWriter("..\\..\\Korisnici.txt"))
                {
                    foreach (var item in korisnici)
                    {
                        sr.WriteLine(item.ime+"|"+ item.prezime+"|"+ item.email+"|"+ item.datum_rodjenja+"|"+ item.korisnicko_ime+"|"+ item.lozinka);

                    }
                sr.Close();
                }
            

            }
        }
    }
}
