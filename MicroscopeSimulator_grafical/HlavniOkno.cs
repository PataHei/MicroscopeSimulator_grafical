using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MicroscopeSimulator_grafical
{
    public partial class HlavniOkno : Form
    {
        //mikroskop, ktery je v idealnim vychozim stavu: otevreny (zavzdusneny) vse je vypnute.
        readonly Microscope microscope = new Microscope(false, 101325, 101325, 0, false, 0, 0, null);
        //mikroskop, ktery se je fyzicky pusteny, napr doslo pri praci k restartovani pocitace. Pri inicializici by se mel deaktivovat zdroj napeti a  vse na nem zavisle.
        //readonly Microscope microscope = new Microscope(true, 2, 2, 1, true, 7, 2, "SED");
        //mikroskop, ktery zustal pod castecnym vakuem a pumpa je vypnuta.
        //readonly Microscope microscope = new Microscope(true, 2000, 2000, 0, false, 0,0, null);


        readonly Bitmap[] mikroskopObrazkyAparatury = new Bitmap[]
        {
            Properties.DataSources.Resource1.mikroskop_se_vzduchem,
            Properties.DataSources.Resource1.mikroskop_se_vzduchem_anim,
            Properties.DataSources.Resource1.mikroskop_bez_vzduchu,
            Properties.DataSources.Resource1.mikroskop_bez_vzduchu_anim,
            Properties.DataSources.Resource1.mikroskop_se_svazkem,
            Properties.DataSources.Resource1.mikroskop_se_svazkem_BSED,
            Properties.DataSources.Resource1.mikroskop_se_svazkem_SED,
            Properties.DataSources.Resource1.Mic_Vakuum_cerpase2,

        };

        readonly Bitmap[] DemonstrativniScanyVzorku = new Bitmap[]
            {
            Properties.DataSources.Resource1.obrazek_BSED_ostry,
            Properties.DataSources.Resource1.obrazek_BSED_rozostreny,
            Properties.DataSources.Resource1.obrazek_SED_ostry,
            Properties.DataSources.Resource1.obrazek_SED_rozostreny,
            };

        bool probihaOknoInicializace; //identifikuje zda dochazi k inicializaci programu. To se pouziva dal k rizeni toho, ktere informace o systemu ma uzivatel dostat
                                      //pole uklada textove spravy s mikroskopu - microscope.InformaceInformaceProUzivatele. Nasledne se zobrazuje v textBoxu textBoxStavInfo

        //pomocne pole na ukladani informaci z mikroskopu. Dale se zobrazuje obsah v textBoxStavInfo
        string[] radkyInfoTextu = { null, null, null, null };
        int aktualniRadek = 0; //Uchovava informaci na ktery index textBoxStavInfo.Line se naposledy ukladala zprava z mikroskopu. Pri inicializaci se vzdy zacina na 0;


        //k prvkum ovladajicim hodnoty napeti na urychlovaci elektronu a pracovni vzdalenosti
        //pomocne promene ktere hlidaji zda byla udalost valueChanged volana primo prvkem numericUpDown (true) nebo neprimo zmenou trackbaru a numericUpDown se jen aktualizuje podle nastavenych hodnot (false).
        bool jeZmenenaHodnotaNumericUpDownNapeti;
        bool jeZmenenaHodnotaNumericUpDownPracovniVzdalenost;
        //pomocne promenne, ktera hlidaji zda doslo ke zmene napeti ci pracovni vzdalenosti prvkem trackBar.
        bool jeZmenenaHodnotaTrackBarPracovniVzdalenost;
        bool jeZmenenaHodnotaTrackBarNapeti;
        public HlavniOkno()
        {
            InitializeComponent();

            //software pri spusteni testuje stav mikroskopu - co je zaple, co je vyple...a podle toho aktivovat prvky v hlavnim Okne.
            probihaOknoInicializace = true;

            //microscope pri inicializaci se pokusi vypnout zdroj elektronu a vypne detektory - pro pripad ze ovladaci software spadl, ale pristroj bezi dal. Naopak vakuum necha bezet.
            microscope.VypniNapajeniZdrojeElektronu();
            microscope.VyberDetektor(null);

            //pokud je v mikroskopu vakuum, checkBox Napeti muze uzivatel zaskrtnout. Zda lze napetovi zdroj zapnout overuje fce microscope.JeMozneZapnoutNapetoviZdroj()
            checkBoxNapajeni.Enabled = microscope.JeMozneZapnoutNapetoviZdroj();

            //nastaveni rozsahu numericUpDown prvku urychlovaciho napeti podle rozsahu zdroje napeti
            numericUpDownNapeti.Minimum = microscope.MinUrychlovaciNapeti;
            numericUpDownNapeti.Maximum = microscope.MaxUrychlovaciNapeti;
            //nastaveni rozsahu numericUpDown prvku pro pracovni vzdalenost podle rozsahu, ktery umoznuje mikroskop
            numericUpDownPracovniVzdalenost.Minimum = microscope.MinPracovniVzdalenost;
            numericUpDownPracovniVzdalenost.Maximum = microscope.MaxPracovniVzdalenost;
            //vychozi hodntora jeZmenenaHodnota_numericUpDown... true umoznuje uzivateli pouzit k nastavovani hodnot jak trackBarem tak numericUpDown prvek.
            //False se nastavuje jen trackBarem pokud se ma numericUpDown jen aktualizovat podle nastavene hodnoty napeti ci pracovni vzdalenosti
            jeZmenenaHodnotaNumericUpDownNapeti = true;
            jeZmenenaHodnotaNumericUpDownPracovniVzdalenost = true;
            //jeZmenenaHodnota_trackBar... ma vliv na chovani prvku trackBar
            //hodnota false blokuje aktualizaci grafickych prvku pomoci metody trackBar..._MouseCaptureChanged volanou udalosti MouseCaptureChanged
            //pokud by nedoslo hlidani zmeny hodnoty trackBaru, tak by se metoda volala pri kazdem klikani kolem jezdce
            jeZmenenaHodnotaTrackBarPracovniVzdalenost = false;
            jeZmenenaHodnotaTrackBarNapeti = false;

            //Nastavi se prvky ovladani mikroskopu podle stavu mikroskopu
            ZjistiStavKomoryAnastavTlacitka();
            NastavPrvkyOvladaniSvazkuElektronu();
            ZjistiStavMikroskopuAZobrazVizualizaciMikroskopu();
            ZjistiStavDetektoruAnastavVzhledTlacitek();
            ZobrazSkenPodleDetektoru();

            //konec inicializace 
            probihaOknoInicializace = false;
            //informace pro stavovy radek o dokoncene inicializace mikroskopu
            microscope.InformaceProUzivatele = "Mikroskop byl inicializovan, nyni můžete pracovat.";
            ZjistiStavMikroskopuAInformuj();

        }

        //SERIE FUNKCI, KTERE PODLE STAVU MIKROSKOPU ZOBRAZUJI OBRAZKY, HODNOTY, AKTIVUJI PRVKY
        /// <summary>
        /// funkce zajistuje textovou informaci pro uzivatele o stavu nastaveni mikroskopu, kterou ziskava s tridy mikroskopu. 
        /// Tato informace se nezobrazuje pri inicializaci.
        /// Aktualizuje zobrazeni hodnot tlaku v komore a tubusu.
        /// </summary>
        /// <param name= infoUlozNaNovyRadekVTextBoxStavInfo> Nepovinny parametr. Prednastavena hodnota je true pro zapis zpravy z mikroskopu do noveho radku v textBoxStavInfo. Hodnota false je potreba zadat, pokud se ma prepsat posledni vytisteny radek novou zpravou </param>
        private void ZjistiStavMikroskopuAInformuj(bool infoUlozNaNovyRadekVTextBoxStavInfo = true)
        {
            //pri inicializaci se netisknou hlasky vyvolane textBox_changed. 
            if (!probihaOknoInicializace)
            {
                AktualizujtextBoxStavInfoLines(infoUlozNaNovyRadekVTextBoxStavInfo);
            }
            else
            {
                //pokud dochazi k inicializaci programu hlasky se nevypisuji
                textBoxStavInfo.Text = "";
            }

            labelTlakKomory.Text = microscope.ZmerTlakKomory().ToString("F0") + " Pa";
            labelTlakTubusu.Text = microscope.ZmerTlakTubusu().ToString("F0") + " Pa";

        }
        /// <summary>
        /// Zpravy z mikroskopu uklada do textBoxStavInfo.Lines, kde se ukladaji posledni 4 zpravy, ktere se zobrazuji v graf. rozhrani v textBoxStavInfo
        /// </summary>
        /// <param name= infoUlozNaNovyRadekVTextBoxStavInfo> Nepovinny parametr. Prednastavena hodnota je true pro zapis do noveho radku. Hodnota false je potreba zadat, pokud se ma prepsat vytisteny radek novou zpravou </param>

        private void AktualizujtextBoxStavInfoLines(bool infoUlozNaNovyRadekVTextBoxStavInfo)
        {

            //obsah textBoxStavInfo.Lines aktualizuje nasledujici kod, ktery v pomocnym poli stringu radkyInfoTextu posouva strare zpravy o radek vys[i - 1].
            // Pokud pole neni plne nova zprava jde na prvni volnou pozici. Pokud je pole plne se vklada nova zprava na pozici [Length-1] z mikroskopu.

            if (infoUlozNaNovyRadekVTextBoxStavInfo)
            {
                if (radkyInfoTextu.Contains(null))
                {
                    //pokud pole neni plne zapise se nova hlaska na prvni prazdny zadek 
                    radkyInfoTextu[aktualniRadek] = microscope.InformaceProUzivatele;
                    if (aktualniRadek < radkyInfoTextu.Length - 1)
                    {
                        aktualniRadek++;
                    }
                }
                else
                {
                    //pokud je pole plne, nejaktualnejsi tri posounou o pozici navrh a pak vlozit novou hlasku
                    for (int j = 1; j < radkyInfoTextu.Length; j++)
                    {
                        radkyInfoTextu[j - 1] = radkyInfoTextu[j];
                    }
                    radkyInfoTextu[aktualniRadek] = microscope.InformaceProUzivatele;
                }

            }
            //pokud se nema info napsat na novy radek, prepise se ten posledni
            else
            {
                radkyInfoTextu[aktualniRadek] = microscope.InformaceProUzivatele;
            }
            textBoxStavInfo.Lines = radkyInfoTextu;
            //na konci zavzdusneni mikroskopu se samovolne vybere (modre podbarv9) text v textBoxStavInfo. Nasledujici radku tento vyber rusi.
            textBoxStavInfo.SelectionStart = 0;
            textBoxStavInfo.SelectionLength = 0;

        }

        /// <summary>
        /// funkce skontroluje zda komora mikroskopu je otevrena ci zavrena a jestli bezi vyveva. 
        /// Podle toho nastavi tlacitka pro otvirani a zavirani komory a ovladani vakua a zavzdusneni.
        /// </summary>
        private void ZjistiStavKomoryAnastavTlacitka()
        {
            buttonOtevritKomoru.Enabled = microscope.JeMozneOtevritKomoru();
            buttonZavritKomoru.Enabled = !microscope.JeKomoraZavrena();
            buttonVycerpavat.Enabled = microscope.JeMozneZapnoutVakuum();
            if (microscope.JeZapnutaVakuovaVyveva())
            {
                buttonVycerpavat.BackColor = System.Drawing.Color.LawnGreen;
            }
            else
            {
                buttonVycerpavat.BackColor = System.Drawing.SystemColors.ButtonFace;
            }
            buttonZavzdusnit.Enabled = microscope.JeMozneZavzdusnit();

            if (microscope.JeOtevrenyZavzdusnovaciVeltil())
            {
                buttonZavzdusnit.BackColor = System.Drawing.Color.LawnGreen;
            }
            else
            {
                buttonZavzdusnit.BackColor = System.Drawing.SystemColors.ButtonFace;
            }
            checkBoxNapajeni.Enabled = microscope.JeMozneZapnoutNapetoviZdroj();
        }


        /// <summary>
        /// Nastavuje vzhled tlacitek k SED a BSED detektoru, podle toho jaky detektor je v mikroskopu aktivovan.
        /// </summary>
        private void ZjistiStavDetektoruAnastavVzhledTlacitek()
        {
            switch (microscope.VybranyDetektor())
            {
                case "SED":
                    buttonSED.BackColor = System.Drawing.Color.LawnGreen;
                    buttonBSED.BackColor = System.Drawing.SystemColors.ButtonFace;
                    break;
                case "BSED":
                    buttonSED.BackColor = System.Drawing.SystemColors.ButtonFace;
                    buttonBSED.BackColor = System.Drawing.Color.LawnGreen;
                    break;
                case null:
                    buttonSED.BackColor = System.Drawing.SystemColors.ButtonFace;
                    buttonBSED.BackColor = System.Drawing.SystemColors.ButtonFace;
                    break;
            }
        }

        /// <summary>
        /// Funkce nastavuje prvky sekce Ovladani Svazku Elektronu. Muze se volat napr. pri inicializaci okna nebo zmene zaskrtnuti CheckBoxu Napajeni 
        /// </summary>
        public void NastavPrvkyOvladaniSvazkuElektronu()
        {
            //zjisti zda je aktivni zdoj napeti zdroje elektronu a podle toho nastavi zbytek zavislych prvku
            bool aktivujPrvky = microscope.JeZapnutyNapetovyZdrojElektronu();
            //microscope zmeri hodnoty nastaveneho napeti a pracovni vzdalenosti, hodnoty se ulozi do pomocnych promennych, ktere dale se vyuziji k nastaveni ovladacich prvku
            double napeti = microscope.ZmerUrychlovaciNapeti();
            int vzdalenost = microscope.ZmerPracovniVzdalenost();

            //prvky urychlovace
            numericUpDownNapeti.Enabled = aktivujPrvky;
            numericUpDownNapeti.Value = Math.Round((decimal)napeti);
            trackBarUrychlovaciNapeti.Enabled = aktivujPrvky;
            trackBarUrychlovaciNapeti.Value = (int)Math.Round((decimal)napeti);

            //prvky pracovni vzdalenosti
            numericUpDownPracovniVzdalenost.Enabled = aktivujPrvky;
            numericUpDownPracovniVzdalenost.Value = vzdalenost;
            trackBarPracovniVzdalenost.Enabled = aktivujPrvky;
            trackBarPracovniVzdalenost.Value = vzdalenost;

            //prvky detektoru
            ZjistiStavDetektoruAnastavVzhledTlacitek();
            buttonSED.Enabled = aktivujPrvky;
            buttonBSED.Enabled = aktivujPrvky;
        }


        /// <summary>
        /// fce nastavuje vizualizace mikroskopu podle stavu vakua komory, stavu cerpani, zapnuti zdroje elektronu a vybraneho detektoru
        /// </summary>
        public void ZjistiStavMikroskopuAZobrazVizualizaciMikroskopu()
        {
            switch (microscope.StavAktualnihoCerpani())
            {
                case 0: //stav kdy se tlak v mikroskopu nemeni
                    if ((int)microscope.ZmerUrychlovaciNapeti() > 0) //pokud leti proud elektronu, což je pri napeti vetsim jak 0 kV
                    {
                        switch (microscope.VybranyDetektor())
                        {
                            case "SED":
                                pictureBoxMicroscope.Image = mikroskopObrazkyAparatury[6]; //obrazek aparatury se svazkem elektronu a detekci SED
                                break;
                            case "BSED":
                                pictureBoxMicroscope.Image = mikroskopObrazkyAparatury[5]; //obrazek aparatury se svazkem elektronu a detekci BSED
                                break;
                            default:
                                pictureBoxMicroscope.Image = mikroskopObrazkyAparatury[4]; //obrazek aparatury se svazkem elektronu
                                break;
                        }
                    }
                    else // urychlovac je vypnuty. Aparatura muze ale nemusi byt pod vakuem.
                    {
                        if (microscope.UrciStupenVakua() > 0)
                        {
                            pictureBoxMicroscope.Image = mikroskopObrazkyAparatury[2]; //obrazek aparatury ve vakuu bez svazku elektronu 
                        }
                        else // stupen Vakua = 0 je zavzdusneny mikroskop
                        {
                            pictureBoxMicroscope.Image = mikroskopObrazkyAparatury[0]; //obrazek aparatury naplnene vzduchem
                        }
                        pictureBoxScan.Image = null;
                    }
                    break;
                case 1:
                    if (microscope.UrciStupenVakua() < 1)
                    {
                        pictureBoxMicroscope.Image = mikroskopObrazkyAparatury[1]; //animace aparatury ve vzduchu
                    }
                    else
                    {
                        pictureBoxMicroscope.Image = mikroskopObrazkyAparatury[7]; //animace aparatury ve vakuu 
                    }
                    break;
                case 2: //zde by meli byt zdroj elektronu a detektory vzdy vypnute
                    pictureBoxMicroscope.Image = mikroskopObrazkyAparatury[3]; //obrazek aparatury ve vakuu, animace zavzdusneni
                    break;

            }
        }

        /// <summary>
        /// Zobrazi demonstrativni skeny podle vybraneho detektoru a pracovni vzdalenosti.
        /// </summary>
        public void ZobrazSkenPodleDetektoru()
        {
            //pomocna promena, ktera uklada vysledek fce, ktera v microscope overuje zda je nastavena spravna pracovni vzdalenost mikroskopu - pro ostry obraz
            bool jeNastavenaPracovniVzdalenosti = microscope.JePracovniVzdalenosti();
            switch (microscope.VybranyDetektor())
            {
                case "SED":
                    if (jeNastavenaPracovniVzdalenosti)
                    {
                        pictureBoxScan.Image = DemonstrativniScanyVzorku[2];
                    }
                    else
                    {
                        pictureBoxScan.Image = DemonstrativniScanyVzorku[3];
                    }

                    break;
                case "BSED":
                    if (jeNastavenaPracovniVzdalenosti)
                    {
                        pictureBoxScan.Image = DemonstrativniScanyVzorku[0];
                    }
                    else
                    {
                        pictureBoxScan.Image = DemonstrativniScanyVzorku[1];
                    }
                    break;
                default:
                    pictureBoxScan.Image = null;
                    break;
            }
        }

        //OVLADANI KOMORY
        //otevrit lze zavzdusnenou nebo temer zavzdusnenou komoru (s lehkym podlakem). Pumpa musi byt vypnuta. 
        private void buttonOtevritKomoru_Click(object sender, EventArgs e)
        {
            microscope.OtevritKomoru();
            ZjistiStavKomoryAnastavTlacitka();
            ZjistiStavMikroskopuAInformuj();
        }

        private void buttonZavritKomoru_Click(object sender, EventArgs e)
        {
            microscope.ZavritKomoru();
            ZjistiStavKomoryAnastavTlacitka();
            ZjistiStavMikroskopuAInformuj();
        }

        //OVLADANI VAKUA
        //pumpa muze cerpat vakuum, nebo byt zastavena. Zavzdusnovat se muze komore jen kdyz pumpa stoji - jinak by se strhala.

        /// <summary>
        /// Metoda zapne pokud nedochazi k cerpani vakua zapne vakuovou pumpu a soucasne spousti timerZmenaTlaku,
        /// ktery trigruje simulaci cerpani. V opacnem pripade dochazi k zastaveni cerpani a timeru
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonVycerpavat_Click(object sender, EventArgs e)
        {

            if (microscope.StavAktualnihoCerpani() != 1)
            {
                microscope.ZapniVakuum();
                ZjistiStavMikroskopuAInformuj();
                timerZmenaTlaku.Start();
            }
            else
            {

                microscope.VypniVakuum();
                ZjistiStavMikroskopuAInformuj();
                timerZmenaTlaku.Stop();

            }
            timerZmenaTlaku.Tag = 0;
            ZjistiStavMikroskopuAZobrazVizualizaciMikroskopu();
            ZjistiStavKomoryAnastavTlacitka();

        }
        /// <summary>
        /// Metoda pokud nedochazi k zavzdusnovani otevre zavzdusnovaci ventil a soucasne spousti timerZmenaTlaku, ktery trigruje simulaci napousteni vzduchu. V opacnem pripade dochazi k zastaveni zavzdusnovani a timeru
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonZavzdusnit_Click(object sender, EventArgs e)
        {

            if (microscope.StavAktualnihoCerpani() != 2)
            {
                microscope.OtevriZavzdusnovaciVentil();
                ZjistiStavMikroskopuAInformuj();
                timerZmenaTlaku.Start();
            }
            else
            {
                microscope.ZavriZavzdusnovaciVentil();
                ZjistiStavMikroskopuAInformuj();
                timerZmenaTlaku.Stop();

            }
            timerZmenaTlaku.Tag = 0;
            ZjistiStavMikroskopuAZobrazVizualizaciMikroskopu();
            ZjistiStavKomoryAnastavTlacitka();
        }

        /// <summary>
        /// Nacerpava vzduch do komory a tubusu o jeden krok za jeden tik podle stavu vakua v mikroskopu, ktery zjistuje fci UrciStupenVakua(). 
        /// Kdyz je dosazeno atmosferickeho tlaku zavre se zavzdusnovaci ventil
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ZmenaTlaku_Tick(object sender, EventArgs e)
        {
            //k zastaveni dojde kdyz:
            //pokud je dosazeno vakua pri vycerpavani

            if (microscope.UrciStupenVakua() == 2 && microscope.StavAktualnihoCerpani() == 1)
            {
                microscope.VypniVakuum();
                ZjistiStavMikroskopuAInformuj();
                timerZmenaTlaku.Stop(); //zastavi se timer ridici zmenu tlaku
                timerZmenaTlaku.Tag = 0;

            }
            //pokud je mikroskop zavzdusnen pri zavzdusnovani
            else if (microscope.UrciStupenVakua() == 0 && microscope.StavAktualnihoCerpani() == 2)
            {
                microscope.ZavriZavzdusnovaciVentil();
                ZjistiStavMikroskopuAInformuj();
                timerZmenaTlaku.Stop(); //zastavi se timer ridici zmenu tlaku
                timerZmenaTlaku.Tag = 0;

            }
            else
            {
                if ((int)timerZmenaTlaku.Tag == 0) //pri prvnim tik se napise zprava do noveho radku, pak se radek prepisuje (Tag != 0)
                {
                    ZjistiStavMikroskopuAInformuj(); //funkce vola microscope.ZmerTlakVKomore, ktera podminene vola fce menici tlaky
                    timerZmenaTlaku.Tag = 1;
                }
                else
                {
                    ZjistiStavMikroskopuAInformuj(false); //funkce vola microscope.ZmerTlakVKomore, ktera podminene vola fce menici tlaky
                }

            }
            // nastavi tlacitka ovladani komory a vakua podle stavu mikroskopu, aktualizuji se hodnoty tlaku, vypisou se hlasky pro uzivatele
            ZjistiStavKomoryAnastavTlacitka();
            ZjistiStavMikroskopuAZobrazVizualizaciMikroskopu(); //aktualizuje se vizualizace mikroskopu

        }

        //OVLADANI ZDROJE ELEKTRONU
        /// <summary>
        /// Po zaškrtnuti policka se zapina zdroj napeti pro elektronovy urychlovac. 
        /// Zaroven se povoli pouzivani prvku na ovladani velikosti napeti, velikosti pracovni vzdalenosti a vyber detektoru.
        /// Po odškrnutí se 1) vypne zdroj napeti a nastavi se O, 2) pracovni vzdalenost zustane nastavene, 3) deaktivuje se aktivni detektor, 4) všechny ovladaci prvky se zamknou
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxNapajeni_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBoxNapajeni.Checked)
            {
                //zapnne se zdroj napeti (predpoklada se ze sam od sebe nenastavuje skokove napeti, ale vychozi nastaveni je 0 kV)
                microscope.ZapnoutNapajeniZdrojeElektronu();
                ZjistiStavMikroskopuAInformuj();
                ZjistiStavDetektoruAnastavVzhledTlacitek();

            }
            else
            {
                bool byloNastaveneNapeti = (int)microscope.ZmerUrychlovaciNapeti() != 0 ? true : false; //hodnota napeti je double, bo se simuluji lehke fluktuace hodnoty v ramci presnosti mereni a stability zdroje, ale zde se testuje jako int
                //deaktivuje se detektor
                microscope.VyberDetektor(null);
                //deaktivuje se pouzivany detektor a ovladaci tlacitka se deaktivuji
                microscope.VypniNapajeniZdrojeElektronu(); //vypne zdroj napeti
                if (!byloNastaveneNapeti)
                {
                    ZjistiStavMikroskopuAInformuj();
                    ZjistiStavDetektoruAnastavVzhledTlacitek();
                }
                // else - tyto fce se volaji v ramci nulovani nastavene honoty napeti, ktere dela metoda trackBar_UrychlovaciNapeti_Scroll(...)

            }

            //AKTUALIZACE TLACITEK, SOUPATEK...
            ZobrazSkenPodleDetektoru();
            ZjistiStavKomoryAnastavTlacitka();
            NastavPrvkyOvladaniSvazkuElektronu();

        }

        /// <summary>
        /// Metoda reaguje na zmenu hodnty napeti nastavenou prvkem numericUpDown. 
        /// Pokud je tato zmena vyvolana primo manipulaci od uzivatele (jeZmenenaHodnotaNumericUpDownNapeti == true) provede se nastaveni napeti a na to navazujici aktualizace vzhledu hlavniho okna.
        /// Pokud je zmena hodnoty vyvolana prvkem TrackBar (jeZmenenaHodnotaNumericUpDownNapeti == false), nic se nedeje a jen se zmeni hodnotu promenne jeZmenenaHodnotaNumericUpDownNapeti = true.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void numericUpDownNapeti_ValueChanged(object sender, EventArgs e)
        {
            if (jeZmenenaHodnotaNumericUpDownNapeti == true) //pokud nastaveni napeti volalo prvni trackBar tak uz metodu nevola numericUpDown prvek, ktery reaguje na zmenu hodnoty napeti. Nenastavuje se napeti dvakrat za sebou na stejnou hodnotu.
            {
                microscope.NastavUrychlovaciNapeti((int)numericUpDownNapeti.Value); //nastavi napeti na zdroji
                trackBarUrychlovaciNapeti.Value = (int)Math.Round((decimal)microscope.ZmerUrychlovaciNapeti());
                ZjistiStavMikroskopuAInformuj();
                ZjistiStavMikroskopuAZobrazVizualizaciMikroskopu();
                ZobrazSkenPodleDetektoru();
            }
            jeZmenenaHodnotaNumericUpDownNapeti = true;

        }
        /// <summary>
        /// Nastavi napeti na elekronovy urychlovac s presnosti na 1 kV v rakci na posouvani jezdce trackBaru
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trackBarUrychlovaciNapeti_Scroll(object sender, EventArgs e)
        {
            jeZmenenaHodnotaNumericUpDownNapeti = false;
            microscope.NastavUrychlovaciNapeti(trackBarUrychlovaciNapeti.Value);//nastavi napeti na zdroji
            numericUpDownNapeti.Value = Math.Round((decimal)microscope.ZmerUrychlovaciNapeti());
            jeZmenenaHodnotaTrackBarNapeti = true;
        }

        /// <summary>
        /// Aktualizuje vzhled prvku Hlavniho okna po nastaveni napeti trackBarem
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trackBarUrychlovaciNapeti_MouseCaptureChanged(object sender, EventArgs e)
        {
            if (jeZmenenaHodnotaTrackBarNapeti) //Ochrana proti nasobnym klikanim v okoli jezdce, ktere nevedli na prenastaveni hodnoty napeti
            {
                ZjistiStavMikroskopuAInformuj();
                ZjistiStavMikroskopuAZobrazVizualizaciMikroskopu();
                ZobrazSkenPodleDetektoru();
                jeZmenenaHodnotaTrackBarNapeti = false;
            }

        }

        //NASTAVOVANI PRACOVNI VZDALENOSTI
        /// <summary>
        /// Metoda reaguje na zmenu hodnty pracovni vzdalenosti nastavenou prvkem numericUpDown. 
        /// Pokud je tato zmena vyvolana primo manipulaci od uzivatele (jeZmenenaHodnotaNumericUpDownPracovniVzdalenost == true) provede se nastaveni napeti a na to navazujici aktualizace vzhledu hlavniho okna.
        /// Pokud je zmena hodnoty vyvolana prvkem TrackBar (jeZmenenaHodnotaNumericUpDownPracovniVzdalenost == false), nic se nedeje a jen se zmeni hodnotu promenne jeZmenenaHodnotaNumericUpDownPracovniVzdalenost = true.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void numericUpDownPracovniVzdalenost_ValueChanged(object sender, EventArgs e)
        {
            if (jeZmenenaHodnotaNumericUpDownPracovniVzdalenost == true) //pokud nastaveni napeti volalo prvni trackBar tak uz metodu nevola numericUpDown prvek, ktery reaguje na zmenu hodnoty pracovni vzdalenosti. Nenastavuje se pracovni vzdalenost dvakrat za sebou na stejnou hodnotu.
            {
                microscope.NastavPracovniVzdalenosti((int)numericUpDownPracovniVzdalenost.Value); //nastavi pracovni vzdalenost
                trackBarPracovniVzdalenost.Value = microscope.ZmerPracovniVzdalenost();
                ZjistiStavMikroskopuAInformuj();
                ZjistiStavMikroskopuAZobrazVizualizaciMikroskopu();
                ZobrazSkenPodleDetektoru();
            }
            jeZmenenaHodnotaNumericUpDownPracovniVzdalenost = true;
        }
        /// <summary>
        /// Nastavi pracovni vzdalenost mikroskopu s presnosti na 1 mm. Zmena je vyvolana tazenim jezdce trackBaru.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trackBarPracovniVzdalenost_Scroll(object sender, EventArgs e)
        {
            microscope.NastavPracovniVzdalenosti(trackBarPracovniVzdalenost.Value);
            jeZmenenaHodnotaNumericUpDownPracovniVzdalenost = false;
            numericUpDownPracovniVzdalenost.Value = microscope.ZmerPracovniVzdalenost();
            jeZmenenaHodnotaTrackBarPracovniVzdalenost = true;
        }
        /// <summary>
        /// Aktualizuje vzhled hlavniho okna a zobrazi zpravy z mikroskopu po tom co uzivatel pusti jezdec, tedy po nastaveni konecne hodnoty pracovni vzdalenosti. Nereaguje na nahodne klikani v okoli jezdce
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trackBarPracovniVzdalenost_MouseCaptureChanged(object sender, EventArgs e)
        {
            if (jeZmenenaHodnotaTrackBarPracovniVzdalenost) //Ochrana proti nasobnym klikanim v okoli jezdce, ktere nevedli na prenastaveni hodnoty
            {
                ZjistiStavMikroskopuAInformuj();
                ZjistiStavMikroskopuAZobrazVizualizaciMikroskopu();
                ZobrazSkenPodleDetektoru();
                jeZmenenaHodnotaTrackBarPracovniVzdalenost = false;

            }

        }


        //OVLADANI DETEKTORU
        /// <summary>
        /// Stiskem tlacitka uzivatel vybere detektor, ktery bude provadet scan. Pokud uzivatel klikne na tlacitko s jiz vybranym dektektorerm, dojde k jeho deaktivaci.
        /// Nasledne se podle volby nastavi vzhled tlacitek, schemata mikroskopu a demonstrantivni skeny.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDetektor_Click(object sender, EventArgs e)
        {
            Button button_detektor = sender as Button;
            if (microscope.VybranyDetektor() == null || microscope.VybranyDetektor() != (string)button_detektor.Tag)
            {
                microscope.VyberDetektor((string)button_detektor.Tag);
            }
            else
            {
                microscope.VyberDetektor(null);
            }

            ZjistiStavDetektoruAnastavVzhledTlacitek();
            ZjistiStavMikroskopuAInformuj();
            ZjistiStavMikroskopuAZobrazVizualizaciMikroskopu();
            ZobrazSkenPodleDetektoru();
        }


    }
}
