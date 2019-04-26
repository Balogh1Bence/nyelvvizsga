using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace ConsoleApp9
{
    class nyelvvizsga
    {
        static void Main(string[] args)
        {
            new feladatok();
            Console.ReadKey();
        }
        #region feladatok
        public class feladatok
        {
            List<sikeres> sikeresek = new List<sikeres>();
            List<sikeres> vizsgak = new List<sikeres>();
            List<sikertelen> sikertelenek = new List<sikertelen>();
            List<nyelvsorrend> nyv = new List<nyelvsorrend>();
            List<vizsga> vizsgas = new List<vizsga>();
            
            public void olvaso()
            {
                StreamReader str = new StreamReader("sikertelen.csv", Encoding.Default);
                StreamReader sr = new StreamReader("sikeres.csv", Encoding.Default);
                sr.ReadLine();
                str.ReadLine();
                int i = 1;
                while (!sr.EndOfStream)
                {
                    while (i < 9)
                    {
                        string sor = sr.ReadLine() + ';' + str.ReadLine();
                        Console.WriteLine("sor: "+sor);
                        string[] adat = sor.Split(';');
                        try
                        {
                            sor = adat[0] + ';' + (2009 + i) + ';' + adat[i] + ';' + adat[10 + i];
                            Console.WriteLine(sor);
                            if (sor == ";")
                            { return; }
                            else
                            {
                                vizsgas.Add(new vizsga(sor));
                            }
                        }
                        catch { break; }

                        i++;
                    }
                    i = 0;

                }
            }
            public feladatok()
            {
                olvaso();
                olvasoSikeres();
                olvasoSikertelen();
                elso3Legnepszerubb();
                Console.WriteLine("év megadása: ");
                int ev = Convert.ToInt32(Console.ReadLine());
                while (!(ev <= 2017 && ev >= 2009))
                {
                    Console.WriteLine("év megadása: ");
                    ev = Convert.ToInt32(Console.ReadLine());
                }
                //összes/sikertelen
                foreach (vizsga v in vizsgas)
                {
                    if (v.ev == ev)
                    {
                        vizsgas.Find(x=>(dobule)x.sikeres ((double)(v.sikertelen) / (v.sikeres + v.sikertelen)));
                    }
                }
             

                

            }

            public void elso3Legnepszerubb()
            {
              
                foreach (sikeres s in sikeresek)
                {
                    vizsgak.Add(s);
                }
                foreach (sikertelen s in sikertelenek)
                {
                    foreach (sikeres ss in vizsgak)
                    {
                        if (s.nyelv == ss.nyelv)
                        {
                            ss.v2009 = ss.v2009 + s.v2009;
                            ss.v2010 = ss.v2010 + s.v2010;
                            ss.v2011 = ss.v2011 + s.v2011;
                            ss.v2012 = ss.v2012 + s.v2012;
                            ss.v2013 = ss.v2013 + s.v2013;
                            ss.v2014 = ss.v2014 + s.v2014;
                            ss.v2015 = s.v2015 + ss.v2015;
                            ss.v2016 = ss.v2016 + s.v2016;
                            ss.v2017 = ss.v2017 + s.v2017;
                        }
                            }
       
                }
               
                foreach (sikeres s in vizsgak)
                {
                    nyv.Add(new nyelvsorrend(s.nyelv, (s.v2009 + s.v2010 + s.v2011 + s.v2012 + s.v2013 + s.v2014 + s.v2015 + s.v2016 + s.v2017)));

                }
                nyv = nyv.OrderByDescending(x => x.szam).ToList();
                


                Console.WriteLine( vizsgak.Find(x => x.v2010 + x.v2009 + x.v2011 + x.v2012 + x.v2013 + x.v2014 + x.v2015 + x.v2016 + x.v2017 == vizsgak.Max(y => y.v2010 + y.v2009 + y.v2011 + y.v2012 + y.v2013 + y.v2014 + y.v2015 + y.v2016 + y.v2017)).nyelv);
                //Console.WriteLine(sikertelenek.Find(x => x.v20010 + x.v2009 + x.v2011 + x.v2012 + x.v2013 + x.v2014 + x.v2015 + x.v2016 + x.v2017 == sikertelenek.Max(y => y.v20010 + y.v2009 + y.v2011 + y.v2012 + y.v2013 + y.v2014 + y.v2015 + y.v2016 + y.v2017)).nyelv);
                //Console.WriteLine(sikeresek.Max(y => y.v20010 + y.v2009 + y.v2011 + y.v2012 + y.v2013 + y.v2014 + y.v2015 + y.v2016 + y.v2017) + sikertelenek.Max(y => y.v20010 + y.v2009 + y.v2011 + y.v2012 + y.v2013 + y.v2014 + y.v2015 + y.v2016 + y.v2017));
                Console.WriteLine(nyv[1].nyelv);
                Console.WriteLine(nyv[2].nyelv);
                
            }
            public class nyelvsorrend
            {
                public string nyelv {get; set;}
                public int szam {get; set;}

                public nyelvsorrend(string nyelv, int szam)
                {
                    this.nyelv = nyelv;
                    this.szam = szam;
                }
            }
            public void olvasoSikeres()
            {
                StreamReader sr = new StreamReader("sikeres.csv", Encoding.Default);
                sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    sikeresek.Add(new sikeres(sr.ReadLine()));
                }
            }
            public void olvasoSikertelen()
            {
                StreamReader sr = new StreamReader("sikertelen.csv", Encoding.Default);
                sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    sikertelenek.Add(new sikertelen(sr.ReadLine()));
                }
            }
         

        }
        #endregion
        public class arany
        {
            public string nyelv { get; set; }
            public int szam { get; set; }

            public arany(string nyelv, int szam)
            {
                this.nyelv = nyelv;
                this.szam = szam;
            }
        }
        #region sikeres
        class sikeres
        {
            public string nyelv { get; set; }
            public int v2009 { get; set; }
            public int v2010 { get; set; }
            public int v2011 { get; set; }
            public int v2012 { get; set; }
            public int v2013 { get; set; }
            public int v2014 { get; set; }
            public int v2015 { get; set; }
            public int v2016 { get; set; }
            public int v2017 { get; set; }
            public sikeres(string line)
            {
                string[] a = line.Split(';');
                setSikeres(a[0], Convert.ToInt32(a[1]), Convert.ToInt32(a[2]), Convert.ToInt32(a[3]), Convert.ToInt32(a[4]), Convert.ToInt32(a[5]), Convert.ToInt32(a[6]), Convert.ToInt32(a[7]), Convert.ToInt32(a[8]), Convert.ToInt32(a[9]));
            }

            public void  setSikeres(string nyelv, int v2009, int v2010, int v2011, int v2012, int v2013, int v2014, int v2015, int v2016, int v2017)
            {
                this.nyelv = nyelv;
                this.v2009 = v2009;
                this.v2010 = v2010;
                this.v2011 = v2011;
                this.v2012 = v2012;
                this.v2013 = v2013;
                this.v2014 = v2014;
                this.v2015 = v2015;
                this.v2016 = v2016;
                this.v2017 = v2017;
            }

           
           
        }
        #endregion
        #region sikertelen
        class sikertelen
        {
            public string nyelv { get; set; }
            public int v2009 { get; set; }
            public int v2010 { get; set; }
            public int v2011 { get; set; }
            public int v2012 { get; set; }
            public int v2013 { get; set; }
            public int v2014 { get; set; }
            public int v2015 { get; set; }
            public int v2016 { get; set; }
            public int v2017 { get; set; }
            public sikertelen(string line)
            {
                string[] a = line.Split(';');
                setSikertelen(a[0], Convert.ToInt32(a[1]), Convert.ToInt32(a[2]), Convert.ToInt32(a[3]), Convert.ToInt32(a[4]), Convert.ToInt32(a[5]), Convert.ToInt32(a[6]), Convert.ToInt32(a[7]), Convert.ToInt32(a[8]), Convert.ToInt32(a[9]));
            }

            public void setSikertelen(string nyelv, int v2009, int v2010, int v2011, int v2012, int v2013, int v2014, int v2015, int v2016, int v2017)
            {
                this.nyelv = nyelv;
                this.v2009 = v2009;
                this.v2010 = v2010;
                this.v2011 = v2011;
                this.v2012 = v2012;
                this.v2013 = v2013;
                this.v2014 = v2014;
                this.v2015 = v2015;
                this.v2016 = v2016;
                this.v2017 = v2017;
            }

            public override string ToString()
            {
               return nyelv + ';' + v2009 + ';' + v2010 + ';' + v2011 + ';' + v2012 + ';' + v2013 + ';' + v2014 + ';' + v2015 + ';' + v2016 + ';' + v2017;
            }
        }

        #endregion
      

    }
    #region sikeres

    public class vizsga
    {
        public string nyelv { get; set; }

        public int ev { get; set; }
        public int sikeres { get; set; }
        public int sikertelen { get; set; }
        public vizsga(string line)
        {
            string[] a = line.Split(';');
            setSikeres(a[0], Convert.ToInt32(a[1]), Convert.ToInt32(a[2]), Convert.ToInt32(a[3]));
        }

        public void setSikeres(string nyelv, int ev, int sikeres, int sikertelen)
        {
            this.nyelv = nyelv;
            this.ev = ev;
            this.sikeres = sikeres;
            this.sikertelen = sikertelen;
            Console.WriteLine(nyelv + ", " + ev + ", " + sikeres + ", " + sikertelen);
        }

        public override string ToString()
        {
            return nyelv + ',' + ev + ',' + sikeres + ',' + sikertelen;
        }
    }
    #endregion
}

