using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeakt2Interakcija.Model
{
    class UcitavanjePodataka
    {
        List<Voz> vozovi=new List<Voz>();
        List<Linija> linije=new List<Linija>();
        List<Stanica> stanice = new List<Stanica>();
        List<Karta> karte = new List<Karta>();
        List<RedVoznje> redoviVoznje = new List<RedVoznje>();
        List<Menadzer> menadzers=new List<Menadzer>();
        public void UcitavanjeLinija(List<Voz>vozovi)
        {

            using (StreamReader reader = File.OpenText("..\\..\\Podaci\\Linija.txt"))
            {

                while (!reader.EndOfStream)
                {

                    string line = reader.ReadLine();
                    if (line == "") break;
                    string[] linije = line.Split('|');

                    string naziv = linije[0];
                   
                    int id = Int32.Parse(linije[1]);
                    Voz voz=new Voz();
                    foreach (Voz item in vozovi)
                    {
                        if (item.id == id)
                        {
                            voz = new Voz(item);
                            break;
                        }
                    }
                    List<Stanica> stanicel = parsiranje(stanice, linije[2]);
                    List<double> cene = new List<double>();
                    string[] niz=linije[3].Split(',');
                    foreach (string item in niz)
                    {
                        cene.Add(Double.Parse(item));
                    }
                    List<DateTime> polasci = new List<DateTime>();
                    string[] niz2 = linije[4].Split(',');
                    foreach (string item in niz2)
                    {
                        DateTime vreme;
                        if (DateTime.TryParse(item, out vreme))
                        {
                            polasci.Add(vreme);

                        }
                    }
                    this.linije.Add(new Linija(naziv,voz,stanicel,cene,polasci));
                }
            }
        }

        public List<Stanica> parsiranje(List<Stanica>stanice,string mesta)
        {
            string[] niz = mesta.Split(',');
            List<Stanica> lista = new List<Stanica>();
            foreach (string mesto in niz)
            {
                Stanica stanica=new Stanica();
                foreach (Stanica item in stanice)
                {
                    if (item.mesto == mesto)
                    {
                        stanica = new Stanica(item);
                        break;
                    }
                }
                lista.Add(stanica);
            }
           
            return lista;
        }
        public void UcitavanjeVozova()
        {

            using (StreamReader reader = File.OpenText("..\\..\\Podaci\\Vozovi.txt"))
            {

                while (!reader.EndOfStream)
                {

                    string line = reader.ReadLine();
                    if (line == "") break;
                    string[] linije = line.Split('|');

                    int id = Int32.Parse(linije[0]);
                    string tip  = linije[1];
                    string brojSedista = linije[2];
                    
                    vozovi.Add(new Voz(id,tip,brojSedista));
                }
            }
        }
        public void UcitavanjeStanica()
        {

            using (StreamReader reader = File.OpenText("..\\..\\Podaci\\Stanice.txt"))
            {

                while (!reader.EndOfStream)
                {

                    string line = reader.ReadLine();
                    if (line == "") break;
                    string[] linije = line.Split('|');

                    string naziv = linije[0];
                    string mesto = linije[1];
                    float x =float.Parse(linije[2]);
                    float y = float.Parse(linije[3]);
                   
                    stanice.Add(new Stanica(naziv,mesto,x,y));
                }
            }
        }

        public void UcitavanjeKarata()
        {

            using (StreamReader reader = File.OpenText("..\\..\\Podaci\\Karte.txt"))
            {

                while (!reader.EndOfStream)
                {

                    string line = reader.ReadLine();
                    if (line == "") break;
                    string[] lines = line.Split('|');

                    int id = Int32.Parse(lines[0]);
                    DateTime datumVreme = DateTime.Parse(lines[1]);
                    
                    Linija lin = new Linija();
                    foreach (Linija item in linije)
                    {
                        if (item.naziv.Equals(lines[2]))
                        {
                            lin = new Linija(item);
                            break;
                        }
                    }

                    string polaziste = lines[3];
                    string odrediste = lines[4];
                    int brojsed = Int32.Parse(lines[5]);
                    double cena = Double.Parse(lines[6]);
                    bool prodato = Boolean.Parse(lines[7]);
                    string mail = lines[8];

                    karte.Add(new Karta(id, datumVreme, lin, polaziste, odrediste, brojsed, cena, prodato,mail));

                }
            }
        }

        public void UcitavanjeRedaVoznje()
        {

            using (StreamReader reader = File.OpenText("..\\..\\Podaci\\RedVoznje.txt"))
            {

                while (!reader.EndOfStream)
                {

                    string line = reader.ReadLine();
                    if (line == "") break;
                    string[] linije = line.Split('|');

                    string danUnedelji = linije[0];
                    List<Linija> lin = new List<Linija>();
                    string[] li=linije[1].Split(',');
                    foreach(string linija in li)
                    {
                        foreach (Linija item in this.linije)
                        {
                            if (item.naziv.Equals(linija))
                            {
                                lin.Add(item);
                                break;
                            }
                        }
                    }

                    redoviVoznje.Add(new RedVoznje(danUnedelji, lin));
                }
            }
        }
        public void UcitavanjeMenadzera()
        {

            using (StreamReader reader = File.OpenText("..\\..\\Podaci\\Manager.txt"))
            {

                while (!reader.EndOfStream)
                {

                    string line = reader.ReadLine();
                    if (line == "") break;
                    string[] lines = line.Split('|');

                    

                  

                    string ime = lines[0];
                    string prezime = lines[1];
                    string email=lines[2];
                    string datumRodjenja = lines[3];
                    string korisnickoIme= lines[4];
                    string lozinka = lines[5];

                    menadzers.Add(new Menadzer(ime,prezime, email,datumRodjenja, korisnickoIme, lozinka));

                }
            }
        }


    }
}