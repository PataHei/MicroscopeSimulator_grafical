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
		private bool KomoraJeZavrena;
		//vlastnosti tykajici se tlaku vzduchu v jednotlivych castech mikroskopu
		private double AktualniTlakVKomore;
		private double AktualniTlakVTubusu;
		public double MinTlak;  //tlak nutny ke spusteni mereni mikroskopu
		public double TlakVLaboratori; //je maximalni tlak
		public int StupenVakua;// 0- zadne vakuum, 1 - nejake vakuum, 2 - pracovni vakuum 

		//Vakuova pumpa
		
		private int AktualniStavCerpani; // stav 0 - nic nedela, 1 - vycerpava vzuduch, 2 - napousti
		//Zdroj elektronu
		private bool JeZapnutyZdrojElektronu;
		private int AktualniUrychlovaciNapeti;
		public int MaxUrychlovaciNapeti; //min. napeti se ocekava O.
		public int MinUrychlovaciNapeti;
		//pracovni vzdalenost
		public int MinPracovniVzdalenost; //= 0, ale vzasade muze nabyvat asi i jine hodnoty
		public int MaxPracovniVzdalenost;
		public int PracovniVzdalenostMikroskopu; //parametr daneho mikroskopu
		private int AktualniPracovniVzdalenost;
		//detektory
		private string NazevVybranehoDetekroru;
		private string[] SeznamDetekroru;
		public bool JeVybranyDetektor;


		/// <summary>
		/// Konstruktor mikroskopu. Vychozi nastaveni mikroskopu
		/// </summary>
		public Microscope()
		{
			InformaceProUzivatele = "";
			//Komora
			KomoraJeZavrena = false;
			//vlastnosti tykajici se tlaku vzduchu v jednotlivych castech mikroskopu
			TlakVLaboratori = 101325;//jednotky Pa
			AktualniTlakVKomore = TlakVLaboratori; //jednotky: Pa
			AktualniTlakVTubusu = TlakVLaboratori; //jednotky: Pa
			MinTlak = 10; //jednotky: Pa
			StupenVakua = 0;
			AktualniStavCerpani = 0; // stav 0 - nic nedela, 1 - vycerpava vzuduch, 2 - napousti
			
			//Zdroj elektronu
			JeZapnutyZdrojElektronu = false;
			AktualniUrychlovaciNapeti = 0; //jednotky kV
			MaxUrychlovaciNapeti = 25; //jednotky kV
			MinUrychlovaciNapeti = 0; //jednotky kV

			//pracovni vzdalenost
			MinPracovniVzdalenost = 0; //jednotky mm
			MaxPracovniVzdalenost = 10; //jednotky mm
			PracovniVzdalenostMikroskopu = 5; //jednotky mm
			AktualniPracovniVzdalenost = 0; //jednotky mm

			//detektory
			NazevVybranehoDetekroru = null;
			SeznamDetekroru = new string[] { "BSED", "SED" };
			JeVybranyDetektor = false;

		}

		/// <summary>
		/// Konstruktor, kde lze nastavit mikroskop do stavu v provozu. Lze tak testovat zda ovladaci software si spravne zjisti nastaveni mikroskopu a graf. rozhrani se podle toho nastavi.
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
		/// <param name="jeVybranyDetektor"></param>
		public Microscope(bool komoraJeZavrena, double aktualniTlakVkomore, double aktualniTlakVtubusu, int aktualniStavCerpani, bool jeZapnutyZdrojElektronu, int aktualniUrychlovaciNapeti, int aktualniPracovniVzdalenost, string nazevVybranehoDetekroru)
		{
			InformaceProUzivatele = "";
			//Komora
			KomoraJeZavrena = komoraJeZavrena;
			//vlastnosti tykajici se tlaku vzduchu v jednotlivych castech mikroskopu
			TlakVLaboratori = 101325;//jednotky Pa
			AktualniTlakVKomore = aktualniTlakVkomore; //jednotky: Pa
			AktualniTlakVTubusu = aktualniTlakVtubusu; //jednotky: Pa
			StupenVakua = UrciStupenVakua();
			MinTlak = 10; //jednotky: Pa
			
			AktualniStavCerpani = aktualniStavCerpani;
			//Zdroj elektronu
			JeZapnutyZdrojElektronu = jeZapnutyZdrojElektronu;
			AktualniUrychlovaciNapeti = aktualniUrychlovaciNapeti; //jednotky kV
			MaxUrychlovaciNapeti = 25; //jednotky kV
			MinUrychlovaciNapeti = 0; //jednotky kV
									  //pracovni vzdalenost
			MinPracovniVzdalenost = 0; //jednotky mm
			MaxPracovniVzdalenost = 10; //jednotky mm
			PracovniVzdalenostMikroskopu = 5; //jednotky mm
			AktualniPracovniVzdalenost = aktualniPracovniVzdalenost; //jednotky mm
																	 //detektory
			NazevVybranehoDetekroru = nazevVybranehoDetekroru;
			SeznamDetekroru = new string[] { "BSED", "SED" };
			JeVybranyDetektor = NazevVybranehoDetekroru != null ?  true : false;

		}


		//OVLADANI KOMORY
		public void ZavritKomoru()
		{
			InformaceProUzivatele = "Komora je zavrená.";
			KomoraJeZavrena = true;
		}

		public bool OtevritKomoru()
		{
			AktualniTlakVKomore = ZmerTlakKomory();
			if (UrciStupenVakua() == 0) //ocekava se zbytkovy podlak
			{
				AktualniTlakVKomore = TlakVLaboratori; //vyrovnaji se zbyle rozdili v tlacich po zavzdusneni
				AktualniTlakVTubusu = TlakVLaboratori; //vyrovnaji se zbyle rozdili v tlacich po zavzdusneni
				InformaceProUzivatele = "Komora je otevrená.";
				return KomoraJeZavrena = false;
			}
			else
			{
				InformaceProUzivatele = "Komoru nelze kvuli podtlaku otevrit. Nejprve ji zavzdusnete.";
				return KomoraJeZavrena = true;
			}
		}
		//overi stav uzavreni komory
		public bool JeKomoraZavrena()
		{
			return KomoraJeZavrena;
		}
		/// <summary>
		/// Fce zjisti zda komora je dostatecne zavzdusna a tim zda je mozne otevrit komoru. Stejne tak nelze otevrit jiz otevrenou komoru.
		/// </summary>
		/// <returns></returns>
		public bool JeMozneOtevritKomoru()
        {
			if(UrciStupenVakua() == 0 && JeKomoraZavrena()) //&& !JeZapnutaVakuovaVyveva() 
			{ return true; }
			else
			{ return false; }
        }

		//OVLADADNI VAKUOVE TECHNIKY
		/// <summary>
		/// Overi zda jsou splnene podminky pro spusteni vakuove pumpy, tj: 
		/// 1)zavrena komora a 
		/// 2) nedochazi zrovna ke zavzdusnovani - StavAktualnihoCerpani() != 2.
		/// </summary>
		/// <returns>true - pokud je mozne pustit vyvevu</returns>
		public bool JeMozneZapnoutVakuum()
        {
			if(KomoraJeZavrena && StavAktualnihoCerpani() != 2) 
			{ 
				return true;
			}
			else
			{ 
				return false;
			}
        }
		/// <summary>
		/// Skontroluje podminky pro zavzdusneni: 
		/// 1) je vypnuty zdroj elektronu, 
		/// 2) je v mikroskopu vakuum, 
		/// 3) vakuoa pumpa nepracuje - StavAktualnihoCerpani != 1.
		/// </summary>
		/// <returns>true pokud je mozne zavzdusnovat aparaturu</returns>
		public bool JeMozneZavzdusnit()
		{
			//2) je v mikroskopu vakuum -tedy je co zavzdusnovat, technicky neni podminka nutna, ale pro ovladani podle me vhodna
			if (!JeZapnutyNapetovyZdrojElektronu() && UrciStupenVakua() > 0 && StavAktualnihoCerpani() != 1)
			{ return true; }
			else
			{ return false; }
		}

		public bool JeOtevrenyZavzdusnovaciVeltil()
        {
			if (StavAktualnihoCerpani() == 2)
			{ return true; }
			else
			{ return false; }

		}
		/// <summary>
		/// Pokud je Komora uzavrena, tak dojde k vyvakuovani mikroskopu - pusti se vakuova pumpa. AktualniStavCerpani se nastavi na hodnotu 1. Pokud nelze pumpu pustit AktualniStavCerpani = 0.
		/// </summary>
		/// <returns>JeVakuum = true</returns>
		public void ZapniVakuum()
		{
			if (KomoraJeZavrena)
			{

				InformaceProUzivatele = "Byla zapnuta vyveva.";
				AktualniStavCerpani = 1;

			}
			else
			{
				InformaceProUzivatele = "Nelze zapnout vakuum. Skontrolujte komoru.";
				AktualniStavCerpani = 0;
			}

		}

		/// <summary>
		/// Zjisti zda vakuova pumpa vycerpava vzduch z mirkoskopu. 
		/// </summary>
		/// <returns>true pokud StavAktualnihoCerpani() == 1</returns>
		public bool JeZapnutaVakuovaVyveva()
        {
			if(StavAktualnihoCerpani() == 1)
			{ return true; }
            else 
			{ return false; }
			
		}
		/// <summary>
		/// Vypne vyvevu. Stav vyvevy se ulozi do AktualniStavCerpani = 0. 
		/// </summary>
		/// <returns></returns>
		public void VypniVakuum()
		{
			AktualniStavCerpani = 0;
			InformaceProUzivatele = "Vyveva byla zastavena.";
		}

		/// <summary>
		/// Fce simulace vycerpani tlaku AktualniTlakVTubusu a AktualniTlakVKomore za jednotku casu. AktualniStavCerpani nastavi na hodnotu = 1.
		/// Rychlost napousteni lze menit zmenou delky jednoto tiku Timeru, ktery fci vola.
		/// </summary>
		public void SnizTlakKomoryATubusu() 
		{
			InformaceProUzivatele = "Vyčerpává se vzduch z mikroskopu. Čekejte prosím.";
			//Rychlost vakuovani lze menit zmenou delky jednoto tiku Timeru, ktery fci vola nebo zmenou parametru 1.3 v teto fci.
			AktualniTlakVKomore /= 1.3; 
			AktualniTlakVTubusu /= 1.3;

		}

		/// <summary>
		/// Fce zvysi tlak AktualniTlakVTubusu a AktualniTlakVKomore za jednotku casu. AktualniStavCerpani nastavi na hodnotu = 2.
		/// simulace napousteni s limitem laboratorniho tlaku (nejde to proste prefouknout :-D).
		/// Rychlost napousteni lze menit zmenou delky jednoto tiku Timeru, ktery fci vola
		/// </summary>
		public void ZvysTlakKomoryATubusu() 
		{
			InformaceProUzivatele = "Mikroskop se zavzdušňuje.";
			AktualniTlakVKomore = 2 * AktualniTlakVKomore *  ((2 * TlakVLaboratori - AktualniTlakVKomore) / (2 * TlakVLaboratori)); 
			AktualniTlakVTubusu = 2 * AktualniTlakVTubusu * ((2*TlakVLaboratori- AktualniTlakVTubusu)/(2*TlakVLaboratori));

		}
		public void OtevriZavzdusnovaciVentil()
        {
			AktualniStavCerpani = 2;
			InformaceProUzivatele = "Byl otevren zavzdusnovaci ventil.";
		}
		public void ZavriZavzdusnovaciVentil()
        {
			AktualniStavCerpani = 0;
			InformaceProUzivatele = "Byl zavren zavzdusnovaci ventil.";
		}

		public int StavAktualnihoCerpani()
		{
			return AktualniStavCerpani;

		}
		/// <summary>
		/// funkce vrati stupen vakua v komore a tubusu. 
		/// </summary>
		/// <returns>hodnota 0 - tlak je vetsi jak 95 % tlaku vzduchu v laboratori, 1 - tlak je mensi nez 95 % tlaku vzduchu v laboratori a vetsi jak minTlak pro praci mikroskopu, 2- tlak je rovny nebo mensi nez minTlak odovidajici maximalnimu pripustnymu tlaku pro praci mikroskopu</returns>
		public int UrciStupenVakua()
        {
			if (ZmerTlakKomory() > 0.95 * TlakVLaboratori && ZmerTlakTubusu() > 0.95 * TlakVLaboratori)
			{ 
				return StupenVakua = 0; //lehky podtlak neni vakuum. Silou komoru clovek otevre
			}
			else if (ZmerTlakKomory() <= MinTlak && ZmerTlakTubusu() <= MinTlak)
			{
				InformaceProUzivatele = "V mikroskopu je dostatecne vakuum pro praci.";
				return StupenVakua = 2; //vakuum pri kterem je mozne pracovat
			}
			else 
			{ 
				return StupenVakua = 1; //neco mezi predchozimi stavy. Bud je treba dovakuovat nebo zavzdusnit
			}
		}

		/// <summary>
		/// Vraci hodnotu aktualniho tlaku uvnitr tubusu
		/// </summary>
		/// <returns>tlak </returns>
		public double ZmerTlakTubusu()
		{
			return AktualniTlakVTubusu;
		}
		/// <summary>
		/// Vraci hodnotu aktualniho tlaku uvnitr komory
		/// </summary>
		/// <returns>tlak </returns>
		public double ZmerTlakKomory()
		{
			return AktualniTlakVKomore;
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
				JeZapnutyZdrojElektronu = true;
			}

		}
		/// <summary>
		/// Zjisti zda je mozne zapnout zdroj napeti. Podminkou zapnuti je vakuum v komore
		/// </summary>
		/// <returns></returns>
		public bool JeMozneZapnoutNapetoviZdroj()
        {
			if(UrciStupenVakua() == 2)
			{ 
				return true; 
			}
			else
			{
				return false;
			}
			
        }

		public bool JeZapnutyNapetovyZdrojElektronu()
        {
			return JeZapnutyZdrojElektronu;
        }
		/// <summary>
		/// Vypne zdroj elektronu. Napeti na zdroji spadne na 0 kV.
		/// </summary>
		/// <returns>JeZapnutyZdrojElektronu = false</returns>
		public bool VypniNapajeniZdrojeElektronu()
		{
			NastavUrychlovaciNapeti(0);
			InformaceProUzivatele = "Zdroj napajeni svazku elektronu byl vypnut.";
			return JeZapnutyZdrojElektronu = false;
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
			if (JeZapnutyZdrojElektronu)
			{
				if (napeti <= MaxUrychlovaciNapeti && napeti >= MinUrychlovaciNapeti)
				{
					InformaceProUzivatele = "Bylo nastaveno zadané napěti na zdroji elektronů.";
					AktualniUrychlovaciNapeti = napeti;
				}
				else if (napeti > MaxUrychlovaciNapeti)
				{
					InformaceProUzivatele = "Bylo nastaveno maximalni povolene napeti " + MaxUrychlovaciNapeti.ToString() + " kV.";
					AktualniUrychlovaciNapeti = MaxUrychlovaciNapeti;
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
				AktualniUrychlovaciNapeti = 0;
			}


		}
		/// <summary>
		/// Zmeri aktualni nastavene napeti na zdroji Elektronu
		/// </summary>
		/// <returns>AktualniUrychlovaciNapeti - hodnotu napeti </returns>
		public int ZmerUrychlovaciNapeti()
		{
			return AktualniUrychlovaciNapeti;
		}
		//PRACOVNI VZDALENOST
		/// <summary>
		/// Nastavi pracovni vzdalenost v rozdahu hodnot MinPracovniVzdalenost a MaxPracovniVzdalenost
		/// </summary>
		/// <param name="vzdalenost">int, ocekava hodnotu v mm</param>
		/// <returns></returns>
		public void NastavPracovniVzdalenosti(int vzdalenost)
		{
			if (JeZapnutyZdrojElektronu)
			{
				if (vzdalenost >= MinPracovniVzdalenost && vzdalenost <= MaxPracovniVzdalenost)
				{
					InformaceProUzivatele = "Byla nastavena zadaná pracovní vzdálenost.";
					AktualniPracovniVzdalenost = vzdalenost;
				}
				else if (vzdalenost > MaxPracovniVzdalenost)
				{
					InformaceProUzivatele = "Pozadovana pracovni vzdalenost je vyssi nez maximalni pracovni vzdalenost. Byla nastavena maximalni pracovni vzdalenost.";
					AktualniPracovniVzdalenost = MaxPracovniVzdalenost;
				}
				else if (vzdalenost < MinPracovniVzdalenost)
				{
					InformaceProUzivatele = "Pozadovana pracovni vzdalenost je nizsi nez minimalni pracovni vzdalenost. Pracovni vzdalenost nebyla nastavena.";
					//zustane posledni nastavena hodnota
				}
			}
			else
			{
				InformaceProUzivatele = "Neni zapnuty zdroj elektronu. Pred nastavenim pracovni vzdalenosti zapnete zdroj elektronu. Je nastavena minimalni pracovni vzdalenost.";
				AktualniPracovniVzdalenost = MinPracovniVzdalenost;
			}

		}
		/// <summary>
		/// Zjisti nastavenou pracovni vzdalenost mezi vzorkem a tubusem.
		/// </summary>
		/// <returns>AktualniPracovniVzdalenost</returns>
		public int ZmerPracovniVzdalenost()
		{
			return AktualniPracovniVzdalenost;
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
				JeVybranyDetektor = false;
				NazevVybranehoDetekroru = null;
				InformaceProUzivatele = "Neni aktivovan zadny z dostupnych detektoru";
			}
			else
			{
				JeVybranyDetektor = true;
				NazevVybranehoDetekroru = nazevDetektoru;
				InformaceProUzivatele = "Nyní je aktivni detektor " + NazevVybranehoDetekroru;
			}

		}
		/// <summary>
		/// Vrati nazev nastaveneho dektoru.
		/// </summary>
		/// <returns>NazevVybranehoDetekroru</returns>
		public string VybranyDetektor()
		{
			return NazevVybranehoDetekroru;
		}

	}
}

