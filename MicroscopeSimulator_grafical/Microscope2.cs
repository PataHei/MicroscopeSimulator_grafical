using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;



namespace MicroscopeSimulator_grafical
{
    /// <summary>
    /// logika cinnosti mikroscopu.
    /// Ovlada prvky tubusu a komory mikroskopu, ovlada vakuum, provadi mereni.
    /// </summary>
    public class Microscope
    {
        //predava chybove hlasky z mikroskopu
        public string InformaceProUzivatele; //text, ktery napovi uzivateli, co udelal asi blbe
                                             //Komora
        private bool komoraJeZavrena;
        //vlastnosti tykajici se tlaku vzduchu v jednotlivych castech mikroskopu
        private double aktualniTlakVKomore;
        private double aktualniTlakVTubusu;
        private double minTlak;  //tlak nutny ke spusteni mereni mikroskopu
        private double tlakVLaboratori; //je maximalni tlak
        private int stupenVakua;// 0- zadne vakuum, 1 - nejake vakuum, 2 - pracovni vakuum 
        enum StupenVakua
        {
            Zadne,
            Nizke,
            Pracovni
        }

        //Vakuova pumpa

        private int aktualniStavCerpani; // stav 0 - nic nedela, 1 - vycerpava vzuduch, 2 - napousti
        enum AktualniStavCerpani
        {
            BezCinnosti,
            CerpaniVakua,
            Zavzdusnovani,
        }
        //Zdroj elektronu
        private bool jeZapnutyZdrojElektronu;
        private double aktualniUrychlovaciNapeti;
        public int MaxUrychlovaciNapeti; //min. napeti se ocekava O.
        public int MinUrychlovaciNapeti;
        //pracovni vzdalenost
        public int MinPracovniVzdalenost; //= 0, ale vzasade muze nabyvat asi i jine hodnoty
        public int MaxPracovniVzdalenost;
        private int pracovniVzdalenostMikroskopu; //parametr daneho mikroskopu
        private int aktualniPracovniVzdalenost;
        //detektory
        private string nazevVybranehoDetekroru;
        public string[] SeznamDetekroru; //uklada nazvy nainstalovanych detektoru v mikroskopu
        private bool jeVybranyDetektor;


        /// <summary>
        /// Konstruktor mikroskopu. Vychozi nastaveni mikroskopu
        /// </summary>
        public Microscope()
        {
            InformaceProUzivatele = "";
            //Komora
            komoraJeZavrena = false;
            //vlastnosti tykajici se tlaku vzduchu v jednotlivych castech mikroskopu
            tlakVLaboratori = 101325;//jednotky Pa
            aktualniTlakVKomore = tlakVLaboratori; //jednotky: Pa
            aktualniTlakVTubusu = tlakVLaboratori; //jednotky: Pa
            minTlak = 10; //jednotky: Pa
            stupenVakua = 0;
            aktualniStavCerpani = 0; // stav 0 - nic nedela, 1 - vycerpava vzuduch, 2 - napousti

            //Zdroj elektronu
            jeZapnutyZdrojElektronu = false;
            aktualniUrychlovaciNapeti = 0; //jednotky kV
            MaxUrychlovaciNapeti = 25; //jednotky kV
            MinUrychlovaciNapeti = 0; //jednotky kV

            //pracovni vzdalenost
            MinPracovniVzdalenost = 0; //jednotky mm
            MaxPracovniVzdalenost = 10; //jednotky mm
            pracovniVzdalenostMikroskopu = 5; //jednotky mm
            aktualniPracovniVzdalenost = 0; //jednotky mm

            //detektory
            nazevVybranehoDetekroru = null;
            SeznamDetekroru = new string[] { "BSED", "SED" };
            jeVybranyDetektor = false;

        }

        /// <summary>
        /// Konstruktor, kde lze nastavit mikroskop do stavu v provozu.
        /// Lze tak testovat zda ovladaci software si spravne zjisti nastaveni mikroskopu a graf. rozhrani se podle toho nastavi.
        /// </summary>
        /// <param name="komoraJeZavrena"></param>
        /// <param name="aktualniTlakVkomore"> v Pa</param>
        /// <param name="aktualniTlakVtubusu"> v Pa</param>
        /// <param name="jeZapnutaVyveva"></param>
        /// <param name="aktualniStavCerpani">stav 0 - nic nedela, 1 - vycerpava vzuduch, 2 - napousti</param>
        /// <param name="jeZapnutyZdrojElektronu"></param>
        /// <param name="aktualniUrychlovaciNapeti"> v kV</param>
        /// <param name="aktualniPracovniVzdalenost"> v mm</param>
        /// <param name="nazevVybranehoDetekroru">SED, BSED nebo null</param>
        public Microscope(bool komoraJeZavrena, double aktualniTlakVkomore, double aktualniTlakVtubusu, int aktualniStavCerpani, bool jeZapnutyZdrojElektronu, int aktualniUrychlovaciNapeti, int aktualniPracovniVzdalenost, string nazevVybranehoDetekroru)
        {
            InformaceProUzivatele = "";
            //Komora
            this.komoraJeZavrena = komoraJeZavrena;
            //vlastnosti tykajici se tlaku vzduchu v jednotlivych castech mikroskopu
            tlakVLaboratori = 101325;//jednotky Pa
            aktualniTlakVKomore = aktualniTlakVkomore; //jednotky: Pa
            aktualniTlakVTubusu = aktualniTlakVtubusu; //jednotky: Pa
            stupenVakua = UrciStupenVakua();
            minTlak = 10; //jednotky: Pa

            this.aktualniStavCerpani = aktualniStavCerpani;
            //Zdroj elektronu
            this.jeZapnutyZdrojElektronu = jeZapnutyZdrojElektronu;
            this.aktualniUrychlovaciNapeti = aktualniUrychlovaciNapeti; //jednotky kV
            MaxUrychlovaciNapeti = 25; //jednotky kV
            MinUrychlovaciNapeti = 0; //jednotky kV
                                      //pracovni vzdalenost
            MinPracovniVzdalenost = 0; //jednotky mm
            MaxPracovniVzdalenost = 10; //jednotky mm
            pracovniVzdalenostMikroskopu = 5; //jednotky mm
            this.aktualniPracovniVzdalenost = aktualniPracovniVzdalenost; //jednotky mm
                                                                          //detektory
            this.nazevVybranehoDetekroru = nazevVybranehoDetekroru;
            SeznamDetekroru = new string[] { "BSED", "SED" };
            jeVybranyDetektor = this.nazevVybranehoDetekroru != null ? true : false;

        }


        //OVLADANI KOMORY
        /// <summary>
        /// fce zavre komoru.
        /// </summary>
        public void ZavritKomoru()
        {
            InformaceProUzivatele = "Komora je zavrená.";
            komoraJeZavrena = true;
        }
        /// <summary>
        /// fce otevita komoru.
        /// </summary>
        public void OtevritKomoru()
        {
            //ocekava se zbytkovy podlak
            aktualniTlakVKomore = tlakVLaboratori; //vyrovnaji se zbyle rozdili v tlacich po zavzdusneni
            aktualniTlakVTubusu = tlakVLaboratori; //vyrovnaji se zbyle rozdili v tlacich po zavzdusneni
            InformaceProUzivatele = "Komora je otevrená.";
            komoraJeZavrena = false;
        }
        /// <summary>
        /// overi stav uzavreni komory
        /// </summary>
        /// <returns>true pokud je komora zavrene</returns>
        public bool JeKomoraZavrena()
        {
            return komoraJeZavrena;
        }
        /// <summary>
        /// Fce zjisti zda komora je dostatecne zavzdusna a tim zda je mozne otevrit komoru. Stejne tak nelze otevrit jiz otevrenou komoru.
        /// </summary>
        /// <returns></returns>
        public bool JeMozneOtevritKomoru()
        {
            return (stupenVakua == 0 && komoraJeZavrena); //&& !JeZapnutaVakuovaVyveva() 
        }

        //OVLADADNI VAKUOVE TECHNIKY
        /// <summary>
        /// Overi zda jsou splnene podminky pro spusteni vakuove pumpy, tj: 
        /// 1) zavrena komora a 
        /// 2) nedochazi zrovna ke zavzdusnovani - StavAktualnihoCerpani() != 2.
        /// 3) nebezi mereni = neni zapli zdroj elektronu, ktery by vakuovani narusovalo - ?????Overit
        /// </summary>
        /// <returns>true - pokud je mozne pustit vyvevu</returns>
        public bool JeMozneZapnoutVakuum()
        {
            return komoraJeZavrena && aktualniStavCerpani != 2 && !jeZapnutyZdrojElektronu;
        }
        /// <summary>
        /// Skontroluje podminky pro zavzdusneni: 
        /// 1) je vypnuty zdroj elektronu, 
        /// 2) je v mikroskopu vakuum - UrciStupenVakua() > 0,
        /// 3) vakuoa pumpa nepracuje - StavAktualnihoCerpani != 1.
        /// </summary>
        /// <returns>true pokud je mozne zavzdusnovat aparaturu</returns>
        public bool JeMozneZavzdusnit()
        {
            //2) je v mikroskopu vakuum -tedy je co zavzdusnovat, technicky neni podminka nutna, ale pro ovladani podle me vhodna
            return !jeZapnutyZdrojElektronu && stupenVakua > 0 && aktualniStavCerpani != 1;
        }
        /// <summary>
        /// Pokud je StavAktualnihoCerpani() = 2, znamena to že dochází ke zavzdusnovani komory, tedy je ventil otevreny
        /// </summary>
        /// <returns>true - pokud je StavAktivnihoCerpani == 2, aparatura se zavzdusnuje </returns>
        public bool JeOtevrenyZavzdusnovaciVeltil()
        {
            return aktualniStavCerpani == 2;
        }
        /// <summary>
        /// Pokud je Komora uzavrena, tak dojde k vyvakuovani mikroskopu - pusti se vakuova pumpa. AktualniStavCerpani se nastavi na hodnotu 1. Pokud nelze pumpu pustit AktualniStavCerpani = 0.
        /// </summary>
        /// <returns>JeVakuum = true</returns>
        public void ZapniVakuum()
        {
            aktualniStavCerpani = 1;
            InformaceProUzivatele = "Byla zapnuta vyveva.";
        }

        /// <summary>
        /// Zjisti zda vakuova pumpa vycerpava vzduch z mirkoskopu. 
        /// </summary>
        /// <returns>true pokud StavAktualnihoCerpani() == 1</returns>
        public bool JeZapnutaVakuovaVyveva()
        {
            return aktualniStavCerpani == 1;
        }
        /// <summary>
        /// Vypne vyvevu. Stav vyvevy se ulozi do AktualniStavCerpani = 0. 
        /// </summary>
        /// <returns></returns>
        public void VypniVakuum()
        {
            aktualniStavCerpani = 0;
            InformaceProUzivatele = "Vyveva byla zastavena.";
        }

        /// <summary>
        /// Fce simulace vycerpani tlaku AktualniTlakVTubusu a AktualniTlakVKomore za jednotku casu. AktualniStavCerpani nastavi na hodnotu = 1.
        /// Rychlost napousteni lze menit zmenou delky jednoto tiku Timeru, ktery fci vola skrz mereni tlaku.
        /// </summary>
        void snizTlakKomoryATubusu()
        {
            //Rychlost vakuovani lze menit zmenou delky jednoto tiku Timeru, ktery fci vola nebo zmenou parametru 1.3 v teto fci.
            aktualniTlakVKomore /= 1.3;
            aktualniTlakVTubusu /= 1.3;
            //kolik procent pozadovaneho vakua bylo dosazeno
            double DosazeneVakuumProcenta = (tlakVLaboratori - aktualniTlakVKomore) / (tlakVLaboratori - minTlak) * 100;
            if (DosazeneVakuumProcenta < 100)
            {
                InformaceProUzivatele = "Vyčerpává se vzduch z mikroskopu. Již je vycerpano " + DosazeneVakuumProcenta.ToString("F2") + " %. Čekejte prosím.";
            }
            else
            {
                InformaceProUzivatele = "V mikroskopu je dostatecne vakuum pro praci.";
            }

        }

        /// <summary>
        /// Fce zvysi tlak AktualniTlakVTubusu a AktualniTlakVKomore za jednotku casu. AktualniStavCerpani nastavi na hodnotu = 2.
        /// simulace napousteni s limitem laboratorniho tlaku (nejde to proste prefouknout :-D).
        /// Rychlost napousteni lze menit zmenou delky jednoto tiku Timeru, ktery fci vola skrz mereni tlaku.
        /// </summary>
        void zvysTlakKomoryATubusu()
        {
            aktualniTlakVKomore = 2 * aktualniTlakVKomore * ((2 * tlakVLaboratori - aktualniTlakVKomore) / (2 * tlakVLaboratori));
            aktualniTlakVTubusu = 2 * aktualniTlakVTubusu * ((2 * tlakVLaboratori - aktualniTlakVTubusu) / (2 * tlakVLaboratori));
            //s kolika procent je mikroskop zavzdusnen
            double KolikJeProcentZavzdusneno = 100 - (tlakVLaboratori - aktualniTlakVKomore) / (tlakVLaboratori - minTlak) * 100;
            if (KolikJeProcentZavzdusneno < 100)
            {
                InformaceProUzivatele = "Mikroskop se zavzdušňuje. Tlak v komore je " + KolikJeProcentZavzdusneno.ToString("F2") + " % labo. tlaku. Čekejte prosím.";
            }
            else
            {
                InformaceProUzivatele = "Mikroskop je zavzdušněn.";
            }

        }
        public void OtevriZavzdusnovaciVentil()
        {
            aktualniStavCerpani = 2;
            InformaceProUzivatele = "Byl otevren zavzdusnovaci ventil.";
        }
        public void ZavriZavzdusnovaciVentil()
        {
            aktualniStavCerpani = 0;
            InformaceProUzivatele = "Byl zavren zavzdusnovaci ventil.";
        }

        public int StavAktualnihoCerpani()
        {
            return aktualniStavCerpani;
        }
        /// <summary>
        /// funkce vrati stupen vakua v komore a tubusu. 
        /// </summary>
        /// <returns>hodnota 0 - tlak je vetsi jak 95 % tlaku vzduchu v laboratori, 1 - tlak je mensi nez 95 % tlaku vzduchu v laboratori a vetsi jak minTlak pro praci mikroskopu, 2- tlak je rovny nebo mensi nez minTlak odovidajici maximalnimu pripustnymu tlaku pro praci mikroskopu</returns>
        public int UrciStupenVakua()
        {
            if (aktualniTlakVKomore > 0.95 * tlakVLaboratori && aktualniTlakVTubusu > 0.95 * tlakVLaboratori)
            {
                return stupenVakua = 0; //lehky podtlak neni vakuum. Silou komoru clovek otevre
            }
            else if (aktualniTlakVKomore <= minTlak && aktualniTlakVTubusu <= minTlak)
            {
                return stupenVakua = 2; //vakuum pri kterem je mozne pracovat
            }
            else
            {
                return stupenVakua = 1; //neco mezi predchozimi stavy. Bud je treba dovakuovat nebo zavzdusnit
            }
        }

        /// <summary>
        /// Vraci hodnotu aktualniho tlaku uvnitr tubusu
        /// </summary>
        /// <returns>tlak </returns>
        public double ZmerTlakTubusu()
        {
            //tlak v tubusu v zasade nemuze byt radově jiny nez v komore, pokud mikroskop neobsahuje nejake oddelovaci prvky.
            //tlak v tubusu se meni s tlakem komory
            return aktualniTlakVTubusu;
        }
        /// <summary>
        /// Vraci hodnotu aktualniho tlaku uvnitr komory. 
        /// Ten se muze menit v pripade, ze bezi vakuova pumpa nebo dochazi ke zavzdusnovani (AktualniStavCerpani)
        /// </summary>
        /// <returns>tlak </returns>
        public double ZmerTlakKomory()
        {
            if (aktualniStavCerpani == 1 && aktualniTlakVKomore > minTlak)
            {
                snizTlakKomoryATubusu(); //fce zaroven meni tlak v tubusu
            }
            else if (aktualniStavCerpani == 2 && aktualniTlakVKomore < tlakVLaboratori)
            {
                zvysTlakKomoryATubusu(); //fce zaroven meni tlak v tubusu
            }
            return aktualniTlakVKomore;
        }

        //ZDROJ ELEKTRONU 
        /// <summary>
        /// Zapne zdroj elektronu, pokud je v komore pracovni vakuum
        /// </summary>
        /// <returns></returns>
        public void ZapnoutNapajeniZdrojeElektronu()
        {
            if (JeMozneZapnoutNapetoviZdroj())
            {
                InformaceProUzivatele = "Napajeni zdroje elektronů bylo zapnuto. Nyní můžete nastavovat parametry skenovani.";
                jeZapnutyZdrojElektronu = true;
            }
        }
        /// <summary>
        /// Zjisti zda je mozne zapnout zdroj napeti. Podminkou zapnuti je vakuum v komore. Tlak musi byt aspon roven MinTlak nebo menší.
        /// </summary>
        /// <returns>true pokud smi byt zapnut zdroj napeti.</returns>
        public bool JeMozneZapnoutNapetoviZdroj()
        {
            return stupenVakua == 2;
        }
        /// <summary>
        /// Overi zda je zdroj napeti zapnuty
        /// </summary>
        /// <returns>true, pokud je zdroj zapnuty</returns>
        public bool JeZapnutyNapetovyZdrojElektronu()
        {
            return jeZapnutyZdrojElektronu;
        }
        /// <summary>
        /// Vypne zdroj elektronu. Napeti na zdroji spadne na 0 kV.
        /// </summary>
        /// <returns>JeZapnutyZdrojElektronu = false</returns>
        public void VypniNapajeniZdrojeElektronu()
        {
            NastavUrychlovaciNapeti(0);
            InformaceProUzivatele = "Zdroj napajeni svazku elektronu byl vypnut.";
            jeZapnutyZdrojElektronu = false;
        }

        /// <summary>
        /// podkud je zapnuty zdroj elektronu, je mozne nastavit napeti na urychlovaci elektronu
        /// </summary>
        /// <param name="napeti">ocekava napeti - cele cislo v kV</param>
        /// <returns>
        /// AktualniUrychlovaciNapeti. 
        /// Pokud neni zdroj spusten hodnota bude 0.
        /// Pokud bude vyzadovano nastaveni napeti vyssi nez max. povolene napeti, nastavi se max. hodnota napeti.
        /// </returns>
        public void NastavUrychlovaciNapeti(int napeti)
        {
            Random sumNapeti = new Random();
            if (jeZapnutyZdrojElektronu)
            {
                if (napeti <= MaxUrychlovaciNapeti && napeti >= MinUrychlovaciNapeti)
                {
                    //simuluje nastavene napeti, ktere byva s presnosti na desetiny
                    double sum = sumNapeti.Next(9);
                    aktualniUrychlovaciNapeti = napeti * 0.99 + sum * 0.05;
                    InformaceProUzivatele = $"Bylo nastaveno {aktualniUrychlovaciNapeti} kV na zdroji elektronů.";
                }
                else if (napeti > MaxUrychlovaciNapeti)
                {
                    InformaceProUzivatele = "Bylo nastaveno maximalni povolene napeti " + MaxUrychlovaciNapeti.ToString() + " kV.";
                    aktualniUrychlovaciNapeti = MaxUrychlovaciNapeti;
                }
                else
                {
                    InformaceProUzivatele = "Nelze nastavit zaporne napeti. Napeti nebylo zmeneno.";
                    //zustane posledni nastavena hodnota
                }

            }
            else
            {
                //InformaceProUzivatele = "Nebyl zapnut zdroj elektronu.";
                aktualniUrychlovaciNapeti = 0;
            }
        }
        /// <summary>
        /// Zmeri aktualni nastavene napeti na zdroji Elektronu
        /// </summary>
        /// <returns>AktualniUrychlovaciNapeti - hodnotu napeti </returns>
        public double ZmerUrychlovaciNapeti()
        {
            return aktualniUrychlovaciNapeti;
        }
        //PRACOVNI VZDALENOST
        /// <summary>
        /// Nastavi pracovni vzdalenost v rozdahu hodnot MinPracovniVzdalenost a MaxPracovniVzdalenost
        /// </summary>
        /// <param name="vzdalenost">int, ocekava hodnotu v mm</param>

        public void NastavPracovniVzdalenosti(int vzdalenost)
        {
            if (jeZapnutyZdrojElektronu)
            {
                if (vzdalenost >= MinPracovniVzdalenost && vzdalenost <= MaxPracovniVzdalenost)
                {
                    aktualniPracovniVzdalenost = vzdalenost;
                    InformaceProUzivatele = $"Byla nastavena zadaná pracovní vzdálenost {aktualniPracovniVzdalenost} mm.";
                }
                else if (vzdalenost > MaxPracovniVzdalenost)
                {
                    InformaceProUzivatele = "Pozadovana pracovni vzdalenost je vyssi nez maximalni pracovni vzdalenost. Byla nastavena maximalni pracovni vzdalenost.";
                    aktualniPracovniVzdalenost = MaxPracovniVzdalenost;
                }
                else if (vzdalenost < MinPracovniVzdalenost)
                {
                    InformaceProUzivatele = "Pozadovana pracovni vzdalenost je nizsi nez minimalni pracovni vzdalenost. Pracovni vzdalenost nebyla nastavena.";
                    //zustane posledni nastavena hodnota
                }
            }
            else
            {
                InformaceProUzivatele = "Neni zapnuty zdroj elektronu. Pred nastavenim pracovni vzdalenosti zapnete zdroj elektronu.";
                //pracovni vzdalenost muze zustat nastavena z napr. predchoziho mereni, ale bez zapnuti zdroje s ji nelze aktivne menit.
                //aktualniPracovniVzdalenost = MinPracovniVzdalenost;
            }

        }
        /// <summary>
        /// Zjisti nastavenou pracovni vzdalenost mezi vzorkem a tubusem.
        /// </summary>
        /// <returns>AktualniPracovniVzdalenost</returns>
        public int ZmerPracovniVzdalenost()
        {
            return aktualniPracovniVzdalenost;
        }

        public bool JePracovniVzdalenosti()
        {
            return aktualniPracovniVzdalenost == pracovniVzdalenostMikroskopu;
        }

        //DETEKTORY
        /// <summary>
        /// Vybere detektor podle zadani uzivatele. Nazev se ulozi do NazevVybranehoDetekroru. 
        /// Pred ulozenim se zadany nazev detektoru se overi v SeznamuDetektoru. 
        /// Pokud zadany detektor v seznamu neni ulozi se do NazevVybranehoDetekroru null.
        /// </summary>
        /// <param name="nazevDetektoru">Nazev musi odpovidat nekteremu z nazvu ulozenym v SeznamDetektoru, jinak se nastavi null</param>
        public void VyberDetektor(string nazevDetektoru)
        {
            if (!SeznamDetekroru.Contains(nazevDetektoru))
            {
                jeVybranyDetektor = false;
                nazevVybranehoDetekroru = null;
                InformaceProUzivatele = "Neni aktivovan zadny z dostupnych detektoru";
            }
            else
            {
                jeVybranyDetektor = true;
                nazevVybranehoDetekroru = nazevDetektoru;
                InformaceProUzivatele = "Nyní je aktivni detektor " + nazevVybranehoDetekroru;
            }

        }
        /// <summary>
        /// Zjisti, ktery detektor elektronu byl vybran, tedy je aktivni.
        /// </summary>
        /// <returns>Nazev vybraneho detekroru elektronu</returns>
        public string VybranyDetektor()
        {
            return nazevVybranehoDetekroru;
        }

    }
}


