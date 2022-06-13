using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeakt2Interakcija.Model
{
    public class RedVoznje
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

        public string linije_string()
        {
            string linije="";
            foreach(Linija linija in this.linije)
            {
                linije += linija + ",";
            }
            linije = linije.Substring(0, linije.Length - 1);
            return linije;

        }
        public override string ToString()
        {
            return this.danUNedelji.ToString() + "|" + linije_string();
        }

    }

    public enum DanUNedelji
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
