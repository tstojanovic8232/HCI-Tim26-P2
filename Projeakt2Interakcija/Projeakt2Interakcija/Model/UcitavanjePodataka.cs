using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeakt2Interakcija.Model
{
    public class UcitavanjePodataka
    {
        public List<Voz> vozovi = new List<Voz>();
        public List<Klijent> korisnici = new List<Klijent>();
        public List<Linija> linije = new List<Linija>();
        public List<Stanica> stanice = new List<Stanica>();

        public List <Karta> karte = new List<Karta>();
        public List <RedVoznje> redoviVoznje = new List<RedVoznje>();
        public List<Menadzer> menadzers=new List<Menadzer>();
        public static Klijent ulogovaniKorisnik=new Klijent();

        public UcitavanjePodataka()
        {
            this.UcitavanjeStanica();
            this.UcitavanjeVozova();
            this.UcitavanjeLinija();
            this.UcitavanjeRedaVoznje();
            this.UcitavanjeKarata();
            this.ucitavanjeKorisnika();
        }

        public void UcitavanjeLinija()
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
                    Voz voz = new Voz();
                    foreach (Voz item in this.vozovi)
                    {
                        if (item.id == id)
                        {
                            voz = new Voz(item);
                            break;
                        }
                    }
                    List<Stanica> stanicel = parsiranje(stanice, linije[2]);
                    List<double> cene = new List<double>();
                    string[] niz = linije[3].Split(',');
                    foreach (string item in niz)
                    {
                        cene.Add(Double.Parse(item));
                    }
                    List<DateTime> polasci = new List<DateTime>();
                    string[] niz2 = linije[4].Split(',');
                    foreach (string item in niz2)
                    {
                        DateTime vreme;
                        if (DateTime.TryParseExact(item, "H:m", null, System.Globalization.DateTimeStyles.None, out vreme))
                        {
                            polasci.Add(vreme);

                        }
                    }
                    this.linije.Add(new Linija(naziv, voz, stanicel, cene, polasci));
                }
            }
        }

        public List<Stanica> parsiranje(List<Stanica> stanice, string mesta)
        {
            string[] niz = mesta.Split(',');
            List<Stanica> lista = new List<Stanica>();
            foreach (string mesto in niz)
            {
                Stanica stanica = new Stanica();
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
            vozovi.Clear();
            using (StreamReader reader = File.OpenText("..\\..\\Podaci\\Vozovi.txt"))
            {

                while (!reader.EndOfStream)
                {

                    string line = reader.ReadLine();
                    if (line == "") break;
                    string[] linije = line.Split('|');

                    int id = Int32.Parse(linije[0]);
                    string tip = linije[1];
                    string brojSedista = linije[2];

                    vozovi.Add(new Voz(id, tip, brojSedista));
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
                    float x = float.Parse(linije[2]);
                    float y = float.Parse(linije[3]);

                    stanice.Add(new Stanica(naziv, mesto, x, y));
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
                    DateTime datumVreme = DateTime.ParseExact(lines[1], "d.M.yyyy HH:mm", null);

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

                    karte.Add(new Karta(id, datumVreme, lin, polaziste, odrediste, brojsed, cena, prodato, mail));

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

                    int dan = Int32.Parse(linije[0]);
                    DanUNedelji danUnedelji = (DanUNedelji)dan;
                    List<Linija> lin = new List<Linija>();
                    string[] li = linije[1].Split(',');
                    foreach (string linija in li)
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
                    string email = lines[2];
                    string datumRodjenja = lines[3];
                    string korisnickoIme = lines[4];
                    string lozinka = lines[5];

                    menadzers.Add(new Menadzer(ime, prezime, email, datumRodjenja, korisnickoIme, lozinka));

                }
            }
        }

        public void UpisLinija()
        {

            using (StreamWriter writer = new StreamWriter("..\\..\\Podaci\\Linija.txt"))
            { 
                foreach (Linija linija in this.linije)
                {
                    //Bujanovac-Beograd|2|Bujanovac,Beograd|0,600|12:50,14:20
                    writer.Write(linija.naziv + "|");
                    writer.Write(linija.Voz.id + "|");
                    for (int i = 0; i < linija.stanice.Count(); i++)
                    {
                        writer.Write(linija.stanice[i].mesto);
                        if (i < linija.stanice.Count - 1) writer.Write(",");
                        else writer.Write("|");
                    }
                    for (int i = 0; i < linija.cene.Count(); i++)
                    {
                        writer.Write(linija.cene[i]);
                        if (i < linija.cene.Count - 1) writer.Write(",");
                        else writer.Write("|");
                    }
                    for (int i = 0; i < linija.polasci.Count(); i++)
                    {
                        writer.Write(linija.polasci[i].ToShortTimeString());
                        if (i < linija.polasci.Count - 1) writer.Write(",");
                        
                    }
                    writer.WriteLine("");
                }
                writer.Close();
            }
        }

        public void UpisVozova()
        {

            using (StreamWriter writer = new StreamWriter("..\\..\\Podaci\\Vozovi.txt"))
            {
                foreach (Voz voz in this.vozovi)
                {
                    //1|soko|200
                    writer.Write(voz.id + "|");
                    writer.Write(voz.tip + "|");
                    writer.WriteLine(voz.brojSedista);
                }
                writer.Close();
            }
        }
        public void ucitavanjeKorisnika()
        {
            using (StreamReader reader = File.OpenText("..\\..\\Podaci\\Korisnici.txt"))
            {

                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (line == "") break;
                    string[] korisnik = line.Split('|');

                    string ime = korisnik[0];
                    string prezime = korisnik[1];
                    string email = korisnik[2];
                    string datum = korisnik[3];
                    string korisnicko_ime = korisnik[4];
                    string lozinka = korisnik[5];
                    korisnici.Add(new Klijent(ime, prezime, email, datum, korisnicko_ime, lozinka));
                }
            }
        }

        public void UpisStanica()
        {

            using (StreamWriter writer = new StreamWriter("..\\..\\Podaci\\Stanice.txt"))
            {
                foreach (Stanica stanica in this.stanice)
                {
                    //Zeleznicka stanica Indjija|Indjija|1.0|4.0
                    writer.Write(stanica.naziv + "|");
                    writer.Write(stanica.mesto + "|");
                    writer.Write(stanica.x + "|");
                    writer.WriteLine(stanica.y);
                }
                writer.Close();
            }
        }

        public void UpisRedovaVoznje()
        {
            
            using (StreamWriter writer = new StreamWriter("..\\..\\Podaci\\RedVoznje.txt"))
            {
                foreach (RedVoznje redVoznje in this.redoviVoznje)
                {
                    //1|NoviSad-Beograd,...
                    writer.Write(((int)redVoznje.danUNedelji).ToString() + "|");
                    Console.WriteLine("ispispoceo");
                    for (int i = 0; i < redVoznje.linije.Count(); i++)
                    {
                        writer.Write(redVoznje.linije[i].naziv);
                        if (i < redVoznje.linije.Count - 1) writer.Write(",");
                        else writer.Write("");
                    }
                    writer.WriteLine("");
                }
                writer.Close();
            }

            
            
        }

        public void UpisKarata()
        {

            using (StreamWriter writer = new StreamWriter("..\\..\\Podaci\\Karte.txt"))
            {
                foreach (Karta karta in this.karte)
                {
                    //0 | 1.7.2022 19:40 | NoviSad - Beograd | Novi Sad | Beograd | 200 | 500.0 | false | mail@
                    writer.Write(karta.id + " | ");
                    writer.Write(karta.datumVreme.ToString() + " | ");
                    writer.Write(karta.linija.naziv + " | ");
                    writer.Write(karta.polaziste + " | ");
                    writer.Write(karta.odrediste + " | ");
                    writer.Write(karta.brojSedista + " | ");
                    writer.Write(karta.cena + " | ");
                    writer.WriteLine(karta.mail);
                }
                writer.Close();
            }
        }

        
        public void UpisMenadzera()//TODO: dodati za korisnike?
        {

            StreamWriter writer = new StreamWriter("..\\..\\Podaci\\Korisnici.txt");
            foreach (Menadzer menadzer in this.menadzers)
            {
                //Teodora|Stojanovic|t.stojanovic8232 @gmail.com|15/05/2000|tea|tea
                writer.Write(menadzer.ime + "|");
                writer.Write(menadzer.prezime + "|");
                writer.Write(menadzer.email + "|");
                writer.Write(menadzer.datum_rodjenja + "|");
                writer.Write(menadzer.korisnicko_ime + "|");
                writer.WriteLine(menadzer.lozinka);
            }
            writer.Close();
        }

    }
}