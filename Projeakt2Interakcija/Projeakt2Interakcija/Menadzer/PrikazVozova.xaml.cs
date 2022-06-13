using System;
using System.Collections.Generic;
using System.Data;
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
using System.IO;

namespace Projeakt2Interakcija
{
    /// <summary>
    /// Interaction logic for PrikazVozova.xaml
    /// </summary>
    public partial class PrikazVozova : Window
    {
        static UcitavanjePodataka ucitavanje = new UcitavanjePodataka();
        List<Voz> vozs = ucitavanje.vozovi;

        public PrikazVozova()
        {
            InitializeComponent();
        }
        DataTable table = new DataTable();


        private void ucitaj(object sender, EventArgs e)
        {
            table.Clear();
            table.Columns.Add("Id", typeof(int));
            table.Columns.Add("Tip", typeof(string));
            table.Columns.Add("broj sedista", typeof(string));
            foreach (var item in ucitavanje.vozovi)
            {
                table.Rows.Add(item.id, item.tip, item.brojSedista);
            }
            datagrid.ItemsSource = table.DefaultView;


        }

        private void txtChange(object sender, EventArgs e)
        {

            var tbx = sender as TextBox;
            if (tbx.Text != "")
            {
                var filtered = ucitavanje.vozovi.Where(x => x.tip.Contains(tbx.Text) || x.brojSedista.Contains(tbx.Text));
                datagrid.ItemsSource = null;
                datagrid.ItemsSource = filtered;
            }

            else
            {
                datagrid.ItemsSource = table.DefaultView;
            }

        }

        private void OcistiTekst(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = "";
        }
        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(id.Text + " ," + tip.Text + ", " + br.Text);
            table.Rows.Add(Int32.Parse(id.Text), tip.Text, br.Text);
            File.AppendAllText("..\\..\\Podaci\\Vozovi.txt", id.Text + "|" + tip.Text + "|" + br.Text + "\n");



        }

        private void Obrisi_Click(object sender, RoutedEventArgs e)
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

            }
        }

        private void Izmeni_Click(object sender, RoutedEventArgs e)
        {
            DataRowView row = datagrid.SelectedItem as DataRowView;

            if (row != null)
            {

                int ind = datagrid.SelectedIndex;

                List<string> quotelist = File.ReadAllLines("..\\..\\Podaci\\Vozovi.txt").ToList();
                row.Delete();

                Voz v = new Voz(Int32.Parse(id.Text), tip.Text, br.Text);
                quotelist.RemoveAt(ind);
                quotelist.Add(v.ToString());

                table.Rows.Add(id.Text, tip.Text, br.Text);
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
                id.Text = row[0].ToString();
                tip.Text = row[1].ToString();
                br.Text = row[2].ToString();
            }
        }

        private void soko_checkd(object sender, RoutedEventArgs e)
        {
            stari.IsChecked = false;
            regia.IsChecked = false;
            sed1.IsChecked = false;

            var filtered = ucitavanje.vozovi.Where(x => x.tip.Contains(soko.Name));
                datagrid.ItemsSource = null;
                datagrid.ItemsSource = filtered;


            if (soko.IsChecked == false)
            {
                datagrid.ItemsSource = table.DefaultView;
            }

            
            
        }

        private void stari_Checked(object sender, RoutedEventArgs e)
        {
            soko.IsChecked = false;
            regia.IsChecked = false;
            sed1.IsChecked = false;
            var filtered = ucitavanje.vozovi.Where(x => x.tip.Contains("stari voz"));
                datagrid.ItemsSource = null;
                datagrid.ItemsSource = filtered;
            
            if (stari.IsChecked == false)
            {
                datagrid.ItemsSource = table.DefaultView;
            }
        }

        private void regio_Checked(object sender, RoutedEventArgs e)
        {
            stari.IsChecked = false;
            soko.IsChecked = false;
            sed1.IsChecked = false;
            var filtered = ucitavanje.vozovi.Where(x => x.tip.Contains("regio voz"));
                datagrid.ItemsSource = null;
                datagrid.ItemsSource = filtered;

            
           
        }

        private void br1_checkd(object sender, RoutedEventArgs e)
        {
            regia.IsChecked = false;
            stari.IsChecked = false;
            soko.IsChecked = false;

            var filtered = ucitavanje.vozovi.Where(x => Int32.Parse(x.brojSedista)>100 && Int32.Parse(x.brojSedista) < 200);
            
            
            datagrid.ItemsSource = null;
            datagrid.ItemsSource = filtered;



        }
        private void uncheck(object sender, RoutedEventArgs e)
        {
            
            datagrid.ItemsSource = table.DefaultView;

        }

        private void NazadNaPocetnu_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            MenadzerPocetna pocetna = new MenadzerPocetna();
            pocetna.Show();
        }

        private void OdjaviMe_Click(object sender, RoutedEventArgs e)
        {
            OdjavaMenadzer logout = new OdjavaMenadzer();
            this.Close();
            logout.Show();
        }
    }
}
