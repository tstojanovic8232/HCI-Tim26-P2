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
using System.ComponentModel;

namespace Projeakt2Interakcija
{
    /// <summary>
    /// Interaction logic for PregledKarataKlijent.xaml
    /// </summary>
    public partial class PregledKarataKlijent : Page
    {
        
        static UcitavanjePodataka ucitavanje ;

        public string klijentmail =UcitavanjePodataka.ulogovaniKorisnik.email;

        public PregledKarataKlijent()
        {
            InitializeComponent();
            ucitavanje = new UcitavanjePodataka();


          //  Console.WriteLine(klijentmail);


        }

       
        
        private void ucitaj(object sender, EventArgs e) {
          
            foreach (var item in ucitavanje.karte)
            {
                //Klijent k = ucitavanje.korisnici.Find(x => x.email.Equals(item.mail));
                
                    if (item.mail.Equals(klijentmail))
                    {
                        ListBoxItem a = new ListBoxItem();
                        LB1.Items.Add(a);
                        a.Content = item.ToString();
                    }
                
                }
            
        }


        Point LB1StartMousePos;
        Point LB2StartMousePos;

        private void LB1_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            LB1StartMousePos = e.GetPosition(null);
        }

        private void LB2_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            LB2StartMousePos = e.GetPosition(null);
        }

        private void LB1_Drop(object sender, DragEventArgs e)
        {
           
            if (e.Data.GetData(DataFormats.FileDrop) is ListBoxItem listItem)
            {
                LB1.Items.Add(listItem);
            }
        }
        private void LB2_Drop(object sender, DragEventArgs e)
        {
           
            if (e.Data.GetData(DataFormats.FileDrop) is ListBoxItem listItem)
            {
                Console.WriteLine(listItem.Content);
              
                if (listItem.Content != null)
                {


                    int ind = Int32.Parse(listItem.Content.ToString().Split(' ')[0]);

                    

                    List<string> quotelist = File.ReadAllLines("..\\..\\Podaci\\Karte.txt").ToList();
                    int i=quotelist.FindIndex(x => x.Split('|')[0].Equals(ind.ToString()));
                    foreach (var item in quotelist)
                    {
                        Console.WriteLine(item);

                    }

                    quotelist.RemoveAt(i);
                    
                    System.IO.File.WriteAllLines("..\\..\\Podaci\\Karte.txt", quotelist.ToArray());

                }


                listItem.Content = null;

                MessageBox.Show("Uspesno ste obrisali kartu!");
            }
        }


        private void LB1_MouseMove(object sender, MouseEventArgs e)
        {
            Point mPos = e.GetPosition(null);

            if (e.LeftButton == MouseButtonState.Pressed &&
                Math.Abs(mPos.X) > SystemParameters.MinimumHorizontalDragDistance &&
                Math.Abs(mPos.Y) > SystemParameters.MinimumVerticalDragDistance)
            {
                try
                {
                   
                    ListBoxItem selectedItem = (ListBoxItem)LB1.SelectedItem;
                    
                    LB1.Items.Remove(selectedItem);

                    
                    DragDrop.DoDragDrop(this, new DataObject(DataFormats.FileDrop, selectedItem), DragDropEffects.Move);

                  

                    if (selectedItem.Parent == null)
                    {
                        LB1.Items.Add(selectedItem);
                        
                    }
                }
                catch { }
            }
        }



    }

}













    



