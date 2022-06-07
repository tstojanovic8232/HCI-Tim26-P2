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
namespace Projeakt2Interakcija
{
    /// <summary>
    /// Interaction logic for PrikazVozova.xaml
    /// </summary>
    public partial class PrikazVozova : Window
    {
        UcitavanjePodataka ucitavanje=new UcitavanjePodataka();
        
        public PrikazVozova()
        {
            InitializeComponent();
            ucitavanje.UcitavanjeVozova();
            var prozor = Application.Current.Windows;
            Console.WriteLine(prozor.ToString());

        }
        DataTable table = new DataTable();

        
        private void ucitaj(object sender,EventArgs e)
        {
           
            table.Columns.Add("Id", typeof(int));
            table.Columns.Add("Tip", typeof(string));
            table.Columns.Add("broj sedista", typeof(string));
            foreach (var item in ucitavanje.vozovi)
            {
                table.Rows.Add(item.id,item.tip,item.brojSedista);
            }
            datagrid.ItemsSource = table.DefaultView;
            

        }

        private void txtChange(object sender, EventArgs e)
        {

            var tbx = sender as TextBox;
            if (tbx.Text != "")
            {
                var filtered = ucitavanje.vozovi.Where(x => x.tip.Contains(tbx.Text)  || x.brojSedista.Contains(tbx.Text));
                datagrid.ItemsSource = null;
                datagrid.ItemsSource = filtered;
            }
           
            else
            {
                datagrid.ItemsSource = table.DefaultView;
            }

        }
    }
}
