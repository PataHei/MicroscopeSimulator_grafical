
namespace MicroscopeSimulator_grafical
{
    partial class HlavniOkno
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HlavniOkno));
            this.labelKomora = new System.Windows.Forms.Label();
            this.LabelDektektory = new System.Windows.Forms.Label();
            this.labelVakuum = new System.Windows.Forms.Label();
            this.labelZdrojEl = new System.Windows.Forms.Label();
            this.buttonOtevritKomoru = new System.Windows.Forms.Button();
            this.buttonZavritKomoru = new System.Windows.Forms.Button();
            this.buttonVycerpavat = new System.Windows.Forms.Button();
            this.buttonZavzdusnit = new System.Windows.Forms.Button();
            this.labelTlakKomoryPopisek = new System.Windows.Forms.Label();
            this.labelTlakVtubusuPopis = new System.Windows.Forms.Label();
            this.labelTlakKomory = new System.Windows.Forms.Label();
            this.labelTlakTubusu = new System.Windows.Forms.Label();
            this.checkBoxNapajeni = new System.Windows.Forms.CheckBox();
            this.trackBarUrychlovaciNapeti = new System.Windows.Forms.TrackBar();
            this.trackBarPracovniVzdalenost = new System.Windows.Forms.TrackBar();
            this.buttonSED = new System.Windows.Forms.Button();
            this.buttonBSED = new System.Windows.Forms.Button();
            this.labelUrychlovaciNapeti = new System.Windows.Forms.Label();
            this.labelJednotkykV = new System.Windows.Forms.Label();
            this.labelPracovniVzdalenost = new System.Windows.Forms.Label();
            this.labelJednotkymm = new System.Windows.Forms.Label();
            this.pictureBoxMicroscope = new System.Windows.Forms.PictureBox();
            this.textBoxStavInfo = new System.Windows.Forms.TextBox();
            this.labelStavSystemu = new System.Windows.Forms.Label();
            this.pictureBoxScan = new System.Windows.Forms.PictureBox();
            this.labelScanSample = new System.Windows.Forms.Label();
            this.timerZmenaTlaku = new System.Windows.Forms.Timer(this.components);
            this.numericUpDownPracovniVzdalenost = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownNapeti = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarUrychlovaciNapeti)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarPracovniVzdalenost)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMicroscope)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxScan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPracovniVzdalenost)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNapeti)).BeginInit();
            this.SuspendLayout();
            // 
            // labelKomora
            // 
            this.labelKomora.AutoSize = true;
            this.labelKomora.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelKomora.Location = new System.Drawing.Point(38, 36);
            this.labelKomora.Name = "labelKomora";
            this.labelKomora.Size = new System.Drawing.Size(174, 25);
            this.labelKomora.TabIndex = 0;
            this.labelKomora.Text = "Ovladani komory";
            // 
            // LabelDektektory
            // 
            this.LabelDektektory.AutoSize = true;
            this.LabelDektektory.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelDektektory.Location = new System.Drawing.Point(38, 531);
            this.LabelDektektory.Name = "LabelDektektory";
            this.LabelDektektory.Size = new System.Drawing.Size(104, 25);
            this.LabelDektektory.TabIndex = 1;
            this.LabelDektektory.Text = "Detektory";
            // 
            // labelVakuum
            // 
            this.labelVakuum.AutoSize = true;
            this.labelVakuum.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelVakuum.Location = new System.Drawing.Point(38, 136);
            this.labelVakuum.Name = "labelVakuum";
            this.labelVakuum.Size = new System.Drawing.Size(162, 25);
            this.labelVakuum.TabIndex = 2;
            this.labelVakuum.Text = "Ovladani vakua";
            // 
            // labelZdrojEl
            // 
            this.labelZdrojEl.AutoSize = true;
            this.labelZdrojEl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelZdrojEl.Location = new System.Drawing.Point(38, 278);
            this.labelZdrojEl.Name = "labelZdrojEl";
            this.labelZdrojEl.Size = new System.Drawing.Size(267, 25);
            this.labelZdrojEl.TabIndex = 3;
            this.labelZdrojEl.Text = "Ovladani svazku elektronů";
            // 
            // buttonOtevritKomoru
            // 
            this.buttonOtevritKomoru.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonOtevritKomoru.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonOtevritKomoru.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonOtevritKomoru.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonOtevritKomoru.Location = new System.Drawing.Point(43, 74);
            this.buttonOtevritKomoru.Name = "buttonOtevritKomoru";
            this.buttonOtevritKomoru.Size = new System.Drawing.Size(168, 43);
            this.buttonOtevritKomoru.TabIndex = 4;
            this.buttonOtevritKomoru.Text = "Otevřít komoru";
            this.buttonOtevritKomoru.UseVisualStyleBackColor = false;
            this.buttonOtevritKomoru.Click += new System.EventHandler(this.buttonOtevritKomoru_Click);
            // 
            // buttonZavritKomoru
            // 
            this.buttonZavritKomoru.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonZavritKomoru.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonZavritKomoru.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonZavritKomoru.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonZavritKomoru.Location = new System.Drawing.Point(238, 74);
            this.buttonZavritKomoru.Name = "buttonZavritKomoru";
            this.buttonZavritKomoru.Size = new System.Drawing.Size(168, 43);
            this.buttonZavritKomoru.TabIndex = 5;
            this.buttonZavritKomoru.Text = "Zavřit komoru";
            this.buttonZavritKomoru.UseVisualStyleBackColor = false;
            this.buttonZavritKomoru.Click += new System.EventHandler(this.buttonZavritKomoru_Click);
            // 
            // buttonVycerpavat
            // 
            this.buttonVycerpavat.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonVycerpavat.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonVycerpavat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonVycerpavat.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonVycerpavat.Location = new System.Drawing.Point(43, 219);
            this.buttonVycerpavat.Name = "buttonVycerpavat";
            this.buttonVycerpavat.Size = new System.Drawing.Size(168, 43);
            this.buttonVycerpavat.TabIndex = 6;
            this.buttonVycerpavat.Text = "Vyčerpavat";
            this.buttonVycerpavat.UseVisualStyleBackColor = false;
            this.buttonVycerpavat.Click += new System.EventHandler(this.buttonVycerpavat_Click);
            // 
            // buttonZavzdusnit
            // 
            this.buttonZavzdusnit.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonZavzdusnit.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonZavzdusnit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonZavzdusnit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonZavzdusnit.Location = new System.Drawing.Point(238, 219);
            this.buttonZavzdusnit.Name = "buttonZavzdusnit";
            this.buttonZavzdusnit.Size = new System.Drawing.Size(168, 43);
            this.buttonZavzdusnit.TabIndex = 7;
            this.buttonZavzdusnit.Text = "Zavzdušnit";
            this.buttonZavzdusnit.UseVisualStyleBackColor = false;
            this.buttonZavzdusnit.Click += new System.EventHandler(this.buttonZavzdusnit_Click);
            // 
            // labelTlakKomoryPopisek
            // 
            this.labelTlakKomoryPopisek.AutoSize = true;
            this.labelTlakKomoryPopisek.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTlakKomoryPopisek.Location = new System.Drawing.Point(44, 165);
            this.labelTlakKomoryPopisek.Name = "labelTlakKomoryPopisek";
            this.labelTlakKomoryPopisek.Size = new System.Drawing.Size(114, 20);
            this.labelTlakKomoryPopisek.TabIndex = 8;
            this.labelTlakKomoryPopisek.Text = "Tlak v komoře";
            // 
            // labelTlakVtubusuPopis
            // 
            this.labelTlakVtubusuPopis.AutoSize = true;
            this.labelTlakVtubusuPopis.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTlakVtubusuPopis.Location = new System.Drawing.Point(44, 190);
            this.labelTlakVtubusuPopis.Name = "labelTlakVtubusuPopis";
            this.labelTlakVtubusuPopis.Size = new System.Drawing.Size(108, 20);
            this.labelTlakVtubusuPopis.TabIndex = 9;
            this.labelTlakVtubusuPopis.Text = "Tlak v tubusu";
            // 
            // labelTlakKomory
            // 
            this.labelTlakKomory.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTlakKomory.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelTlakKomory.Location = new System.Drawing.Point(261, 165);
            this.labelTlakKomory.Name = "labelTlakKomory";
            this.labelTlakKomory.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labelTlakKomory.Size = new System.Drawing.Size(145, 20);
            this.labelTlakKomory.TabIndex = 10;
            this.labelTlakKomory.Text = "101325 Pa";
            this.labelTlakKomory.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelTlakKomory.UseMnemonic = false;
            // 
            // labelTlakTubusu
            // 
            this.labelTlakTubusu.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTlakTubusu.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelTlakTubusu.Location = new System.Drawing.Point(257, 190);
            this.labelTlakTubusu.Name = "labelTlakTubusu";
            this.labelTlakTubusu.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labelTlakTubusu.Size = new System.Drawing.Size(149, 20);
            this.labelTlakTubusu.TabIndex = 11;
            this.labelTlakTubusu.Text = "101325 Pa";
            this.labelTlakTubusu.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelTlakTubusu.UseMnemonic = false;
            // 
            // checkBoxNapajeni
            // 
            this.checkBoxNapajeni.AutoSize = true;
            this.checkBoxNapajeni.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxNapajeni.Location = new System.Drawing.Point(44, 319);
            this.checkBoxNapajeni.Name = "checkBoxNapajeni";
            this.checkBoxNapajeni.Size = new System.Drawing.Size(96, 24);
            this.checkBoxNapajeni.TabIndex = 12;
            this.checkBoxNapajeni.Text = "Napajeni";
            this.checkBoxNapajeni.UseVisualStyleBackColor = true;
            this.checkBoxNapajeni.CheckedChanged += new System.EventHandler(this.checkBoxNapajeni_CheckedChanged);
            // 
            // trackBarUrychlovaciNapeti
            // 
            this.trackBarUrychlovaciNapeti.Location = new System.Drawing.Point(43, 393);
            this.trackBarUrychlovaciNapeti.Maximum = 25;
            this.trackBarUrychlovaciNapeti.Name = "trackBarUrychlovaciNapeti";
            this.trackBarUrychlovaciNapeti.Size = new System.Drawing.Size(363, 56);
            this.trackBarUrychlovaciNapeti.TabIndex = 13;
            this.trackBarUrychlovaciNapeti.Tag = "0";
            this.trackBarUrychlovaciNapeti.TickFrequency = 5;
            this.trackBarUrychlovaciNapeti.Scroll += new System.EventHandler(this.trackBarUrychlovaciNapeti_Scroll);
            this.trackBarUrychlovaciNapeti.MouseCaptureChanged += new System.EventHandler(this.trackBarUrychlovaciNapeti_MouseCaptureChanged);
            // 
            // trackBarPracovniVzdalenost
            // 
            this.trackBarPracovniVzdalenost.LargeChange = 1;
            this.trackBarPracovniVzdalenost.Location = new System.Drawing.Point(43, 474);
            this.trackBarPracovniVzdalenost.Name = "trackBarPracovniVzdalenost";
            this.trackBarPracovniVzdalenost.Size = new System.Drawing.Size(363, 56);
            this.trackBarPracovniVzdalenost.TabIndex = 14;
            this.trackBarPracovniVzdalenost.Tag = "0";
            this.trackBarPracovniVzdalenost.Scroll += new System.EventHandler(this.trackBarPracovniVzdalenost_Scroll);
            this.trackBarPracovniVzdalenost.MouseCaptureChanged += new System.EventHandler(this.trackBarPracovniVzdalenost_MouseCaptureChanged);
            // 
            // buttonSED
            // 
            this.buttonSED.BackColor = System.Drawing.SystemColors.Control;
            this.buttonSED.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonSED.FlatAppearance.BorderSize = 0;
            this.buttonSED.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSED.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSED.Location = new System.Drawing.Point(43, 578);
            this.buttonSED.Name = "buttonSED";
            this.buttonSED.Size = new System.Drawing.Size(168, 43);
            this.buttonSED.TabIndex = 15;
            this.buttonSED.Tag = "SED";
            this.buttonSED.Text = "SED";
            this.buttonSED.UseVisualStyleBackColor = false;
            this.buttonSED.Click += new System.EventHandler(this.buttonDetektor_Click);
            // 
            // buttonBSED
            // 
            this.buttonBSED.BackColor = System.Drawing.SystemColors.Control;
            this.buttonBSED.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonBSED.FlatAppearance.BorderSize = 0;
            this.buttonBSED.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonBSED.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonBSED.Location = new System.Drawing.Point(238, 578);
            this.buttonBSED.Name = "buttonBSED";
            this.buttonBSED.Size = new System.Drawing.Size(168, 43);
            this.buttonBSED.TabIndex = 16;
            this.buttonBSED.Tag = "BSED";
            this.buttonBSED.Text = "BSED";
            this.buttonBSED.UseVisualStyleBackColor = false;
            this.buttonBSED.Click += new System.EventHandler(this.buttonDetektor_Click);
            // 
            // labelUrychlovaciNapeti
            // 
            this.labelUrychlovaciNapeti.AutoSize = true;
            this.labelUrychlovaciNapeti.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelUrychlovaciNapeti.Location = new System.Drawing.Point(44, 369);
            this.labelUrychlovaciNapeti.Name = "labelUrychlovaciNapeti";
            this.labelUrychlovaciNapeti.Size = new System.Drawing.Size(146, 20);
            this.labelUrychlovaciNapeti.TabIndex = 17;
            this.labelUrychlovaciNapeti.Text = "Urychlovací napětí";
            // 
            // labelJednotkykV
            // 
            this.labelJednotkykV.AutoSize = true;
            this.labelJednotkykV.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelJednotkykV.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelJednotkykV.Location = new System.Drawing.Point(371, 369);
            this.labelJednotkykV.Name = "labelJednotkykV";
            this.labelJednotkykV.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labelJednotkykV.Size = new System.Drawing.Size(28, 20);
            this.labelJednotkykV.TabIndex = 19;
            this.labelJednotkykV.Text = "kV";
            this.labelJednotkykV.UseMnemonic = false;
            // 
            // labelPracovniVzdalenost
            // 
            this.labelPracovniVzdalenost.AutoSize = true;
            this.labelPracovniVzdalenost.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPracovniVzdalenost.Location = new System.Drawing.Point(44, 449);
            this.labelPracovniVzdalenost.Name = "labelPracovniVzdalenost";
            this.labelPracovniVzdalenost.Size = new System.Drawing.Size(159, 20);
            this.labelPracovniVzdalenost.TabIndex = 20;
            this.labelPracovniVzdalenost.Text = "Pracovní vzdálenost";
            // 
            // labelJednotkymm
            // 
            this.labelJednotkymm.AutoSize = true;
            this.labelJednotkymm.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelJednotkymm.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelJednotkymm.Location = new System.Drawing.Point(371, 449);
            this.labelJednotkymm.Name = "labelJednotkymm";
            this.labelJednotkymm.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labelJednotkymm.Size = new System.Drawing.Size(37, 20);
            this.labelJednotkymm.TabIndex = 22;
            this.labelJednotkymm.Text = "mm";
            this.labelJednotkymm.UseMnemonic = false;
            // 
            // pictureBoxMicroscope
            // 
            this.pictureBoxMicroscope.BackColor = System.Drawing.SystemColors.HighlightText;
            this.pictureBoxMicroscope.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBoxMicroscope.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxMicroscope.Image")));
            this.pictureBoxMicroscope.Location = new System.Drawing.Point(437, 74);
            this.pictureBoxMicroscope.Name = "pictureBoxMicroscope";
            this.pictureBoxMicroscope.Size = new System.Drawing.Size(291, 434);
            this.pictureBoxMicroscope.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxMicroscope.TabIndex = 23;
            this.pictureBoxMicroscope.TabStop = false;
            // 
            // textBoxStavInfo
            // 
            this.textBoxStavInfo.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.textBoxStavInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxStavInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxStavInfo.Location = new System.Drawing.Point(437, 528);
            this.textBoxStavInfo.Multiline = true;
            this.textBoxStavInfo.Name = "textBoxStavInfo";
            this.textBoxStavInfo.ReadOnly = true;
            this.textBoxStavInfo.Size = new System.Drawing.Size(755, 93);
            this.textBoxStavInfo.TabIndex = 24;
            this.textBoxStavInfo.Text = "hlaska 4\r\nhlaska 3\r\nhlaska 2\r\nhlaska 1";
            // 
            // labelStavSystemu
            // 
            this.labelStavSystemu.AutoSize = true;
            this.labelStavSystemu.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStavSystemu.Location = new System.Drawing.Point(432, 36);
            this.labelStavSystemu.Name = "labelStavSystemu";
            this.labelStavSystemu.Size = new System.Drawing.Size(179, 25);
            this.labelStavSystemu.TabIndex = 25;
            this.labelStavSystemu.Text = "Kontrola systemu";
            // 
            // pictureBoxScan
            // 
            this.pictureBoxScan.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.pictureBoxScan.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxScan.Image")));
            this.pictureBoxScan.Location = new System.Drawing.Point(758, 74);
            this.pictureBoxScan.Name = "pictureBoxScan";
            this.pictureBoxScan.Size = new System.Drawing.Size(434, 434);
            this.pictureBoxScan.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxScan.TabIndex = 26;
            this.pictureBoxScan.TabStop = false;
            // 
            // labelScanSample
            // 
            this.labelScanSample.AutoSize = true;
            this.labelScanSample.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelScanSample.Location = new System.Drawing.Point(751, 36);
            this.labelScanSample.Name = "labelScanSample";
            this.labelScanSample.Size = new System.Drawing.Size(132, 25);
            this.labelScanSample.TabIndex = 27;
            this.labelScanSample.Text = "Sken vzorku";
            // 
            // timerZmenaTlaku
            // 
            this.timerZmenaTlaku.Interval = 500;
            this.timerZmenaTlaku.Tick += new System.EventHandler(this.ZmenaTlaku_Tick);
            // 
            // numericUpDownPracovniVzdalenost
            // 
            this.numericUpDownPracovniVzdalenost.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownPracovniVzdalenost.Location = new System.Drawing.Point(312, 447);
            this.numericUpDownPracovniVzdalenost.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numericUpDownPracovniVzdalenost.Name = "numericUpDownPracovniVzdalenost";
            this.numericUpDownPracovniVzdalenost.Size = new System.Drawing.Size(53, 26);
            this.numericUpDownPracovniVzdalenost.TabIndex = 29;
            this.numericUpDownPracovniVzdalenost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownPracovniVzdalenost.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numericUpDownPracovniVzdalenost.ValueChanged += new System.EventHandler(this.numericUpDownPracovniVzdalenost_ValueChanged);
            // 
            // numericUpDownNapeti
            // 
            this.numericUpDownNapeti.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownNapeti.Location = new System.Drawing.Point(312, 367);
            this.numericUpDownNapeti.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numericUpDownNapeti.Name = "numericUpDownNapeti";
            this.numericUpDownNapeti.Size = new System.Drawing.Size(53, 26);
            this.numericUpDownNapeti.TabIndex = 30;
            this.numericUpDownNapeti.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownNapeti.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numericUpDownNapeti.ValueChanged += new System.EventHandler(this.numericUpDownNapeti_ValueChanged);
            // 
            // HlavniOkno
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1240, 661);
            this.Controls.Add(this.numericUpDownNapeti);
            this.Controls.Add(this.numericUpDownPracovniVzdalenost);
            this.Controls.Add(this.labelScanSample);
            this.Controls.Add(this.pictureBoxScan);
            this.Controls.Add(this.labelStavSystemu);
            this.Controls.Add(this.textBoxStavInfo);
            this.Controls.Add(this.pictureBoxMicroscope);
            this.Controls.Add(this.labelJednotkymm);
            this.Controls.Add(this.labelPracovniVzdalenost);
            this.Controls.Add(this.labelJednotkykV);
            this.Controls.Add(this.labelUrychlovaciNapeti);
            this.Controls.Add(this.buttonBSED);
            this.Controls.Add(this.buttonSED);
            this.Controls.Add(this.trackBarPracovniVzdalenost);
            this.Controls.Add(this.trackBarUrychlovaciNapeti);
            this.Controls.Add(this.checkBoxNapajeni);
            this.Controls.Add(this.labelTlakTubusu);
            this.Controls.Add(this.labelTlakKomory);
            this.Controls.Add(this.labelTlakVtubusuPopis);
            this.Controls.Add(this.labelTlakKomoryPopisek);
            this.Controls.Add(this.buttonZavzdusnit);
            this.Controls.Add(this.buttonVycerpavat);
            this.Controls.Add(this.buttonZavritKomoru);
            this.Controls.Add(this.buttonOtevritKomoru);
            this.Controls.Add(this.labelZdrojEl);
            this.Controls.Add(this.labelVakuum);
            this.Controls.Add(this.LabelDektektory);
            this.Controls.Add(this.labelKomora);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.Name = "HlavniOkno";
            this.Text = "Mikroskop";
            ((System.ComponentModel.ISupportInitialize)(this.trackBarUrychlovaciNapeti)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarPracovniVzdalenost)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMicroscope)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxScan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPracovniVzdalenost)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNapeti)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelKomora;
        private System.Windows.Forms.Label LabelDektektory;
        private System.Windows.Forms.Label labelVakuum;
        private System.Windows.Forms.Label labelZdrojEl;
        private System.Windows.Forms.Button buttonOtevritKomoru;
        private System.Windows.Forms.Button buttonZavritKomoru;
        private System.Windows.Forms.Button buttonVycerpavat;
        private System.Windows.Forms.Button buttonZavzdusnit;
        private System.Windows.Forms.Label labelTlakKomoryPopisek;
        private System.Windows.Forms.Label labelTlakVtubusuPopis;
        private System.Windows.Forms.Label labelTlakKomory;
        private System.Windows.Forms.Label labelTlakTubusu;
        private System.Windows.Forms.CheckBox checkBoxNapajeni;
        private System.Windows.Forms.TrackBar trackBarUrychlovaciNapeti;
        private System.Windows.Forms.TrackBar trackBarPracovniVzdalenost;
        private System.Windows.Forms.Button buttonSED;
        private System.Windows.Forms.Button buttonBSED;
        private System.Windows.Forms.Label labelUrychlovaciNapeti;
        private System.Windows.Forms.Label labelJednotkykV;
        private System.Windows.Forms.Label labelPracovniVzdalenost;
        private System.Windows.Forms.Label labelJednotkymm;
        private System.Windows.Forms.PictureBox pictureBoxMicroscope;
        private System.Windows.Forms.TextBox textBoxStavInfo;
        private System.Windows.Forms.Label labelStavSystemu;
        private System.Windows.Forms.PictureBox pictureBoxScan;
        private System.Windows.Forms.Label labelScanSample;
        private System.Windows.Forms.Timer timerZmenaTlaku;
        private System.Windows.Forms.NumericUpDown numericUpDownPracovniVzdalenost;
        private System.Windows.Forms.NumericUpDown numericUpDownNapeti;
    }
}

