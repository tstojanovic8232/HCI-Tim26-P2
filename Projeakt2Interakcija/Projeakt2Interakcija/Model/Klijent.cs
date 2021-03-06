using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeakt2Interakcija.Model
{
    public class Klijent
    {
        public string ime { get; set; }
        public string prezime { get; set; }
        public string email { get; set; }
        public string datum_rodjenja { get; set; }
        public string korisnicko_ime { get; set; }
        public string lozinka { get; set; }
        public List<Karta> kupljeneKarte { get; set; }
        public List<Karta> rezervisaneKarte { get; set; }

        public Klijent()
        {

        }
        public Klijent(string i,string b,string c,string d,string e,string f)
        {
            this.ime = i;
            this.prezime = b;
            this.email = c;
            this.datum_rodjenja=d;
            this.korisnicko_ime = e;
            this.lozinka = f;
        }

        public Klijent(Klijent k)
        {
            this.ime = k.ime;
            this.prezime = k.prezime;
            this.email = k.email;
            this.datum_rodjenja = k.datum_rodjenja;
            this.korisnicko_ime = k.korisnicko_ime;
            this.lozinka = k.lozinka;
        }
    }
}
