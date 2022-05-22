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
        List<Korisnik> lista;
        string text5;
        string text6;
        public MainWindow()
        {
            InitializeComponent();
            lista = new List<Korisnik>();
            using (StreamReader reader = File.OpenText("..\\..\\Podaci.txt"))
            {

                while (!reader.EndOfStream)
                {

                    string line = reader.ReadLine();
                    if (line == "") break;
                    string[] korisnik = line.Split('|');
                    foreach (var item in korisnik)
                    {
                        Console.WriteLine(item);
                    }

                    string ime = korisnik[0];
                    string prezime = korisnik[1];
                    string email = korisnik[2];
                    string datum = korisnik[3];
                    string korisnicko_ime = korisnik[4];
                    string lozinka = korisnik[5];
                    lista.Add(new Korisnik(ime, prezime, email, datum, korisnicko_ime, lozinka));
                }
            }
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

            

            foreach (Korisnik k in lista)
            {
                if (text5.Equals(k.korisnicko_ime) && text6.Equals(k.lozinka))
                {
                    MessageBox.Show("Uspesno ste se ulogovali");
                    e.Handled = true;
                }
                else
                {
                    MessageBox.Show("Nepostojeci korisnik");
                }

            }
        }
    }
}
