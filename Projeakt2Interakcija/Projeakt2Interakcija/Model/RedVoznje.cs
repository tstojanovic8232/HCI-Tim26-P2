using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeakt2Interakcija.Model
{
    internal class RedVoznje
    {
        public DanUNedelji danUNedelji { get; set; }
        public List<Linija> linije;

        public RedVoznje()
        {
            
        }

        public RedVoznje(DanUNedelji danUNedelji, List<Linija> linije)
        {
            this.danUNedelji = danUNedelji;
            this.linije = linije;
        }

    }

    enum DanUNedelji
    {
        Ponedeljak = 1,
        Utorak,
        Sreda,
        Cetvrtak,
        Petak,
        Subota,
        Nedelja
    }
}
