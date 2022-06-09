﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Projeakt2Interakcija.Model;

namespace Projeakt2Interakcija
{
    /// <summary>
    /// Interaction logic for RezervacijaKarata.xaml
    /// </summary>
    public partial class PregledRedaVoznjeKlijent : Page
    {
        UcitavanjePodataka ucitavanje = new UcitavanjePodataka();
        DataTable table = new DataTable();
        RedVoznje red;
        public PregledRedaVoznjeKlijent()
        {
            InitializeComponent();
            DanUNedelji dan = danasDanUNedelji();
            danUNedelji.SelectedIndex = (int)dan;

            red = ucitavanje.redoviVoznje.Find(x => x.danUNedelji.Equals(dan));
            if(red == null)
            {
                red = ucitavanje.redoviVoznje.Find(x => x.danUNedelji.Equals(DanUNedelji.Ponedeljak));
            }
            linija.ItemsSource = red.linije;
            foreach (var item in Enum.GetValues(typeof(DanUNedelji)))
            {
                danUNedelji.Items.Add(item);
            }
        }

        private void linija_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(linija.SelectedItem != null)
            {
                Linija lin = red.linije.Find(x => x.naziv.Equals(linija.SelectedItem.ToString()));
                red = ucitavanje.redoviVoznje.Find(x => x.danUNedelji.Equals((DanUNedelji)danUNedelji.SelectedItem));
                if (red != null)
                {
                    table.Clear();
                    table.Columns.Add("stanica", typeof(string));
                    table.Columns.Add("polazak", typeof(string));
                    for (int i = 0; i < lin.stanice.Count; i++)
                    {
                        table.Rows.Add(lin.stanice[i].getMesto(), lin.polasci[i]);
                    }
                }

                redVoznje.ItemsSource = table.DefaultView;
            }
            
        }

        private DanUNedelji danasDanUNedelji()
        {
            DayOfWeek day = DateTime.Today.DayOfWeek;

            if (day.Equals(DayOfWeek.Monday)) return DanUNedelji.Ponedeljak;
            else if (day.Equals(DayOfWeek.Tuesday)) return DanUNedelji.Utorak;
            else if (day.Equals(DayOfWeek.Wednesday)) return DanUNedelji.Sreda;
            else if (day.Equals(DayOfWeek.Thursday)) return DanUNedelji.Cetvrtak;
            else if (day.Equals(DayOfWeek.Friday)) return DanUNedelji.Petak;
            else if (day.Equals(DayOfWeek.Saturday)) return DanUNedelji.Subota;
            else if (day.Equals(DayOfWeek.Sunday)) return DanUNedelji.Nedelja;

            return DanUNedelji.Ponedeljak;
        }
    }
}
