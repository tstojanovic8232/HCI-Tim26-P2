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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Projeakt2Interakcija.Model;

namespace Projeakt2Interakcija
{
    /// <summary>
    /// Interaction logic for RezervacijaKarata.xaml
    /// </summary>
    public partial class RezervacijaKarata : Page
    {
        UcitavanjePodataka ucitavanje = new UcitavanjePodataka();
        public RezervacijaKarata()
        {
            InitializeComponent();
            polaziste.ItemsSource = ucitavanje.stanice;
            odrediste.ItemsSource = ucitavanje.stanice;
        }

    }
}
