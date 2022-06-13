using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeakt2Interakcija.Model
{    
    public class Linija
    {
        public string naziv { get; set; }
        public Voz Voz { get; set; }
        public List<Stanica> stanice { get; set; }
        public List<double> cene { get; set; }
        public List<DateTime> polasci { get; set; }


        public Linija()
        {

        }

   
        public Linija(string naziv, Voz voz, List<Stanica> stanicel, List<double> cene, List<DateTime> polasci)
        {
            this.naziv = naziv;
            Voz = voz;
            stanice = stanicel;
            this.cene = cene;
            this.polasci = polasci;
        }
        public Linija(Linija lin)
        {
            naziv = lin.naziv;
            Voz voz = lin.Voz;
            stanice = lin.stanice;
            cene = lin.cene;
            polasci = lin.polasci;
        }

        public string getTipVoza()
        {
            return Voz.tip;
        }

        public override string ToString()
        {
            return naziv;
        }

        public string stanice_string()
        {
            string stanice = "";
            foreach (Stanica stanica in this.stanice)
            {
                stanice += stanica.mesto + ",";
            }
            stanice = stanice.Substring(0, stanice.Length - 1);
            return stanice;
        }

        public string polasci_string()
        {
            string polasci = "";
            foreach (DateTime polazak in this.polasci)
            {
                polasci += polazak.ToShortTimeString() + ",";
            }
            polasci = polasci.Substring(0, polasci.Length - 1);
            return polasci;
        }

        public string cene_string()
        {
            string cene = "";
            foreach (double cena in this.cene)
            {
                cene += cena.ToString() + ",";
            }
            cene = cene.Substring(0, cene.Length - 1);
            return cene;
        }
    }
}
