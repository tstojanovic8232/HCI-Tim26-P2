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
    /// Interaction logic for VideoPregledRedaVoznje.xaml
    /// </summary>
    public partial class VideoPregledRedaVoznje : Window
    {
        public VideoPregledRedaVoznje()
        {
            InitializeComponent();
            //mojVideo.Stop();
            //mojVideo.AutoPlay = true;
            //var mySourceUri = new Uri("../../Video/PregledRedaVoznje.avi");
            //mojVideo.Source = mySourceUri;
            mojVideo.Play();
        }
    }
}
