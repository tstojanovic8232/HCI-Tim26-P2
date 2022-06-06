﻿using System;
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
    /// Interaction logic for PregledVoznihLinijaMenadzer.xaml
    /// </summary>
    public partial class PregledVoznihLinijaMenadzer : Window
    {
        public PregledVoznihLinijaMenadzer()
        {
            InitializeComponent();
        }

        void SetProperties()
        {
            this.Title = "Srbija Voz";

            this.MinHeight = 600;
            this.MinWidth = 800;
            Uri iconUri = new Uri("./Slike/SrbijaVozLogo.jpg", UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);
        }

        private void NazadNaPocetnu_Click(object sender, RoutedEventArgs e)
        {
            MenadzerPocetna menadzerPocetna = new MenadzerPocetna();
            menadzerPocetna.Show();
        }

        private void Prikazi_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OdjaviMe_Click(object sender, RoutedEventArgs e)
        {
            OdjavaMenadzer logout = new OdjavaMenadzer();
            logout.Show();
        }


    }
}
