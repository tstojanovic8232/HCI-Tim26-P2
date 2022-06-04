using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeakt2Interakcija.Model
{    
    internal class Linija
    {
        private object ime;
        private object prezime;
        private object email;
        private object datum;
        private object korisnicko_ime;
        private object lozinka;
        private List<Stanica> stanicel;

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
    }
}
