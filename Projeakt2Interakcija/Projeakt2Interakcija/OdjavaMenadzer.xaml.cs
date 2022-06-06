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

namespace Projeakt2Interakcija
{
    /// <summary>
    /// Interaction logic for OdjavaMenadzer.xaml
    /// </summary>
    public partial class OdjavaMenadzer : Window
    {
        public OdjavaMenadzer()
        {
            InitializeComponent();
        }

        private void PrijaviMe_Click(object sender, RoutedEventArgs e)
        {
            MainWindow pocetna = new MainWindow();
            pocetna.Show();
        }
    }
}
