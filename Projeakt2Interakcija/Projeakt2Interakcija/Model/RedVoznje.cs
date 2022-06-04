using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeakt2Interakcija.Model
{
    internal class RedVoznje
    {
        public string danUNedelji { get; set; }
        public List<Linija> linije;

        public RedVoznje()
        {
            
        }

        public RedVoznje(string danUNedelji, List<Linija> linije)
        {
            this.danUNedelji = danUNedelji;
            this.linije = linije;
        }

    }
}
