using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeakt2Interakcija.Model
{    
    internal class Linija
    {
        public string naziv { get; set; }
        public Voz Voz { get; set; }
        public List<Stanica> stanice { get; set; }
        public List<double> cene { get; set; }
        public List<DateTime> polasci { get; set; }


        public Linija()
        {

        }

        public string getTipVoza()
        {
            return Voz.tip;
        }
    }
}
