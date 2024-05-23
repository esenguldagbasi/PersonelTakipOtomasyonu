namespace PersonelTakipOtomasyonu
{
    partial class frmPersonelMesaileri
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
            this.dataGridViewPersoneller = new System.Windows.Forms.DataGridView();
            this.dataGridViewMesailer = new System.Windows.Forms.DataGridView();
            this.lblKayitSayisi = new System.Windows.Forms.Label();
            this.lblMesaiUcreti = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtMesaiIDAra = new System.Windows.Forms.TextBox();
            this.txtPersonelId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPersonelAdiSoyadi = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPersoneller)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMesailer)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewPersoneller
            // 
            this.dataGridViewPersoneller.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewPersoneller.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPersoneller.Dock = System.Windows.Forms.DockStyle.Left;
            this.dataGridViewPersoneller.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewPersoneller.Name = "dataGridViewPersoneller";
            this.dataGridViewPersoneller.RowHeadersWidth = 51;
            this.dataGridViewPersoneller.RowTemplate.Height = 24;
            this.dataGridViewPersoneller.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewPersoneller.Size = new System.Drawing.Size(328, 450);
            this.dataGridViewPersoneller.TabIndex = 0;
            this.dataGridViewPersoneller.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewPersoneller_CellClick);
            // 
            // dataGridViewMesailer
            // 
            this.dataGridViewMesailer.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewMesailer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewMesailer.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataGridViewMesailer.Location = new System.Drawing.Point(328, 0);
            this.dataGridViewMesailer.Name = "dataGridViewMesailer";
            this.dataGridViewMesailer.RowHeadersWidth = 51;
            this.dataGridViewMesailer.RowTemplate.Height = 24;
            this.dataGridViewMesailer.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewMesailer.Size = new System.Drawing.Size(823, 357);
            this.dataGridViewMesailer.TabIndex = 1;
            // 
            // lblKayitSayisi
            // 
            this.lblKayitSayisi.AutoSize = true;
            this.lblKayitSayisi.Location = new System.Drawing.Point(925, 410);
            this.lblKayitSayisi.Name = "lblKayitSayisi";
            this.lblKayitSayisi.Size = new System.Drawing.Size(161, 16);
            this.lblKayitSayisi.TabIndex = 2;
            this.lblKayitSayisi.Text = "Toplam 25 kayıt listelendi.";
            // 
            // lblMesaiUcreti
            // 
            this.lblMesaiUcreti.AutoSize = true;
            this.lblMesaiUcreti.Location = new System.Drawing.Point(909, 382);
            this.lblMesaiUcreti.Name = "lblMesaiUcreti";
            this.lblMesaiUcreti.Size = new System.Drawing.Size(198, 16);
            this.lblMesaiUcreti.TabIndex = 2;
            this.lblMesaiUcreti.Text = "Mesai ücreti tutarı= 14342455 TL.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(379, 382);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "Mesai ID Ara:";
            // 
            // txtMesaiIDAra
            // 
            this.txtMesaiIDAra.BackColor = System.Drawing.Color.Bisque;
            this.txtMesaiIDAra.Location = new System.Drawing.Point(354, 410);
            this.txtMesaiIDAra.Name = "txtMesaiIDAra";
            this.txtMesaiIDAra.Size = new System.Drawing.Size(157, 22);
            this.txtMesaiIDAra.TabIndex = 0;
            this.txtMesaiIDAra.TextChanged += new System.EventHandler(this.txtMesaiIDAra_TextChanged);
            // 
            // txtPersonelId
            // 
            this.txtPersonelId.BackColor = System.Drawing.Color.Bisque;
            this.txtPersonelId.Location = new System.Drawing.Point(536, 410);
            this.txtPersonelId.Name = "txtPersonelId";
            this.txtPersonelId.Size = new System.Drawing.Size(157, 22);
            this.txtPersonelId.TabIndex = 1;
            this.txtPersonelId.TextChanged += new System.EventHandler(this.txtPersonelId_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(564, 382);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "Personel ID:";
            // 
            // txtPersonelAdiSoyadi
            // 
            this.txtPersonelAdiSoyadi.BackColor = System.Drawing.Color.Bisque;
            this.txtPersonelAdiSoyadi.Location = new System.Drawing.Point(716, 410);
            this.txtPersonelAdiSoyadi.Name = "txtPersonelAdiSoyadi";
            this.txtPersonelAdiSoyadi.Size = new System.Drawing.Size(157, 22);
            this.txtPersonelAdiSoyadi.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(728, 382);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(133, 16);
            this.label2.TabIndex = 7;
            this.label2.Text = "Personel Adı Soyadı:";
            // 
            // frmPersonelMesaileri
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1151, 450);
            this.Controls.Add(this.txtPersonelAdiSoyadi);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtPersonelId);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtMesaiIDAra);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblMesaiUcreti);
            this.Controls.Add(this.lblKayitSayisi);
            this.Controls.Add(this.dataGridViewMesailer);
            this.Controls.Add(this.dataGridViewPersoneller);
            this.Name = "frmPersonelMesaileri";
            this.Text = "Personel Mesaileri Sayfası";
            this.Load += new System.EventHandler(this.frmPersonelMesaileri_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPersoneller)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMesailer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewPersoneller;
        private System.Windows.Forms.DataGridView dataGridViewMesailer;
        private System.Windows.Forms.Label lblKayitSayisi;
        private System.Windows.Forms.Label lblMesaiUcreti;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtMesaiIDAra;
        private System.Windows.Forms.TextBox txtPersonelId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPersonelAdiSoyadi;
        private System.Windows.Forms.Label label2;
    }
}