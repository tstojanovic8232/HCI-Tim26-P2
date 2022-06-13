using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeakt2Interakcija.Model
{
    public class Menadzer
    {
        
       

        public string ime { get; set; }
        public string prezime { get; set; }
        public string email { get; set; }
        public string datum_rodjenja { get; set; }
        public string korisnicko_ime { get; set; }
        public string lozinka { get; set; }

        public Menadzer()
        {

        }

        public Menadzer(string ime, string prezime, string email, string datumRodjenja, string korisnickoIme, string lozinka)
        {
            this.ime = ime;
            this.prezime = prezime;
            this.email = email;
            this.datum_rodjenja = datumRodjenja;
            this.korisnicko_ime = korisnickoIme;
            this.lozinka = lozinka;
        }
    }
}
