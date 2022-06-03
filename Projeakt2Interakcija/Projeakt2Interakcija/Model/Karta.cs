using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeakt2Interakcija.Model
{
    internal class Karta
    {
        public Linija linija { get; set; }
        public string polaziste { get; set; }
        public string odrediste { get; set; }
        public int brojSedista { get; set; }
        public double cena { get; set; }
        public bool prodata { get; set; }

        public Karta()
        {

        }
    }
}
