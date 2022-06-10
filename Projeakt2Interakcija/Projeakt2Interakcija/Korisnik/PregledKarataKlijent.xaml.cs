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
using System.Windows.Forms;
using Projeakt2Interakcija.Model;
using System.IO;

namespace Projeakt2Interakcija
{
    /// <summary>
    /// Interaction logic for PregledKarataKlijent.xaml
    /// </summary>
    public partial class PregledKarataKlijent : Page
    {
        static UcitavanjePodataka ucitavanje = new UcitavanjePodataka();
        public PregledKarataKlijent()
        {
            InitializeComponent();
            ucitavanje.UcitavanjeKarata();
        }

        

        DataTable table = new DataTable();

        private void ucitaj(object sender, EventArgs e)
        {
            table.Clear();
            table.Columns.Add("Id", typeof(int));
            table.Columns.Add("Datum i vreme", typeof(DateTime));
            //table.Columns.Add("Linija", typeof(string));
            table.Columns.Add("polaziste", typeof(string));
            table.Columns.Add("odrediste", typeof(string));
            table.Columns.Add("cena", typeof(double));
            foreach (var item in ucitavanje.karte)
            {
                table.Rows.Add(item.id, item.datumVreme, item.polaziste, item.odrediste, item.cena);
            }
            datagrid.ItemsSource = table.DefaultView;


        }
        private void datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView row = datagrid.SelectedItem as DataRowView;
            
        }
    }
}
