using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeakt2Interakcija.Model
{
    internal class Stanica
    {
        
        public string naziv { get; set; }
        public string mesto { get; set; }
        public float x { get; set; }
        public float y { get; set; }

        public Stanica()
        {

        }

        public Stanica(string naziv, string mesto, float x, float y)
        {
            this.naziv = naziv;
            this.mesto = mesto;
            this.x = x;
            this.y = y;
        }

        public Stanica(Stanica stanica)
        {
            naziv=stanica.naziv;
            mesto=stanica.naziv;
            x=stanica.x;
            y = stanica.y;
        }

        public override string ToString()
        {
            return naziv;
        }

        public string getMesto()
        {
            return mesto;
        }
    }
}
