using System;

namespace Projeakt2Interakcija.Model
{
    public class Karta
    {
        public int id { get; set; }
        public DateTime datumVreme { get; set; }
        public Linija linija { get; set; }
        public string polaziste { get; set; }
        public string odrediste { get; set; }
        public int brojSedista { get; set; }
        public double cena { get; set; }
        public bool prodata { get; set; }

        public string mail { get; set; }

        public Karta()
        {

        }

        public Karta(int id, DateTime datumVreme, Linija linija, string polaziste, string odrediste, int brojSedista, double cena, bool prodata, string mail)
        {
            this.id = id;
            this.datumVreme = datumVreme;
            this.linija = linija;
            this.polaziste = polaziste;
            this.odrediste = odrediste;
            this.brojSedista = brojSedista;
            this.cena = cena;
            this.prodata = prodata;
            this.mail = mail;
        }
        public override string ToString()
        {
            return id + " " + datumVreme + " " + polaziste + " " + odrediste+ " " + brojSedista + " " + cena + " " + prodata;
        }
    }

}
