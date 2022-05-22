using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeakt2Interakcija
{
    class Korisnik
    {
        public string ime { get; set; }
        public string prezime { get; set; }
        public string email { get; set; }
        public string datum_rodjenja { get; set; }
        public string korisnicko_ime { get; set; }
        public string lozinka { get; set; }


        public Korisnik()
        {

        }
        public Korisnik(string i,string b,string c,string d,string e,string f)
        {
            this.ime = i;
            this.prezime = b;
            this.email = c;
            this.datum_rodjenja=d;
            this.korisnicko_ime = e;
            this.lozinka = f;
        }
        

    }
}
