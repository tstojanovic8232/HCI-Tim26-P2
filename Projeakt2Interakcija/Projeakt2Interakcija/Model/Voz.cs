using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeakt2Interakcija.Model
{
    internal class Voz
    {
        public int  id { get; set; }
        public string tip { get; set; }
        public string brojSedista { get; set; }

        public Voz(int id,string tip,string br)
        {
            this.id = id;
            this.tip = tip;
            this.brojSedista = br;

        }
        public Voz(Voz voz)
        {
            id = voz.id;
            tip = voz.tip;
            brojSedista = voz.brojSedista;
        }

        public Voz()
        {
        }
        public override string ToString()
        {
            return  id + "|" + tip+"|"+brojSedista;
        }

        public string getVozInfo()
        {
            return tip + ", broj sedista: " + brojSedista;
        }
    }
}
