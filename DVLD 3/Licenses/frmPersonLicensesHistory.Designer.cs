namespace DVLD_3.Licenses
{
    partial class frmPersonLicensesHistory
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.personDetailsWithFilter1 = new DVLD_3.MangePeople.Controls.PersonDetailsWithFilter();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageLocalLicenses = new System.Windows.Forms.TabPage();
            this.tabPageInternationalLicenses = new System.Windows.Forms.TabPage();
            this.publicFormsPanelLocal = new DVLD_3.UserControls.PublicFormsPanel();
            this.publicFormsPanelInternational = new DVLD_3.UserControls.PublicFormsPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageLocalLicenses.SuspendLayout();
            this.tabPageInternationalLicenses.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DVLD_3.Properties.Resources.PersonLicenseHistory_512;
            this.pictureBox1.Location = new System.Drawing.Point(3, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(198, 320);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // personDetailsWithFilter1
            // 
            this.personDetailsWithFilter1.FilterEnabled = true;
            this.personDetailsWithFilter1.Location = new System.Drawing.Point(202, 12);
            this.personDetailsWithFilter1.Name = "personDetailsWithFilter1";
            this.personDetailsWithFilter1.ShowAddPerson = true;
            this.personDetailsWithFilter1.Size = new System.Drawing.Size(648, 320);
            this.personDetailsWithFilter1.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tabControl1);
            this.groupBox1.Font = new System.Drawing.Font("Cairo", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(13, 338);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(837, 358);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Driver Licenses";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageLocalLicenses);
            this.tabControl1.Controls.Add(this.tabPageInternationalLicenses);
            this.tabControl1.Location = new System.Drawing.Point(6, 19);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(825, 333);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPageLocalLicenses
            // 
            this.tabPageLocalLicenses.Controls.Add(this.label1);
            this.tabPageLocalLicenses.Controls.Add(this.publicFormsPanelLocal);
            this.tabPageLocalLicenses.Location = new System.Drawing.Point(4, 33);
            this.tabPageLocalLicenses.Name = "tabPageLocalLicenses";
            this.tabPageLocalLicenses.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageLocalLicenses.Size = new System.Drawing.Size(817, 296);
            this.tabPageLocalLicenses.TabIndex = 0;
            this.tabPageLocalLicenses.Text = "Local";
            this.tabPageLocalLicenses.UseVisualStyleBackColor = true;
            // 
            // tabPageInternationalLicenses
            // 
            this.tabPageInternationalLicenses.Controls.Add(this.label2);
            this.tabPageInternationalLicenses.Controls.Add(this.publicFormsPanelInternational);
            this.tabPageInternationalLicenses.Location = new System.Drawing.Point(4, 33);
            this.tabPageInternationalLicenses.Name = "tabPageInternationalLicenses";
            this.tabPageInternationalLicenses.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageInternationalLicenses.Size = new System.Drawing.Size(817, 296);
            this.tabPageInternationalLicenses.TabIndex = 1;
            this.tabPageInternationalLicenses.Text = "International";
            this.tabPageInternationalLicenses.UseVisualStyleBackColor = true;
            // 
            // publicFormsPanelLocal
            // 
            this.publicFormsPanelLocal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.publicFormsPanelLocal.Location = new System.Drawing.Point(7, 9);
            this.publicFormsPanelLocal.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.publicFormsPanelLocal.Name = "publicFormsPanelLocal";
            this.publicFormsPanelLocal.Size = new System.Drawing.Size(803, 278);
            this.publicFormsPanelLocal.TabIndex = 0;
            // 
            // publicFormsPanelInternational
            // 
            this.publicFormsPanelInternational.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.publicFormsPanelInternational.Location = new System.Drawing.Point(7, 9);
            this.publicFormsPanelInternational.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.publicFormsPanelInternational.Name = "publicFormsPanelInternational";
            this.publicFormsPanelInternational.Size = new System.Drawing.Size(803, 278);
            this.publicFormsPanelInternational.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "Local Licenses History";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(179, 24);
            this.label2.TabIndex = 3;
            this.label2.Text = "International Licenses History";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Font = new System.Drawing.Font("Cairo", 9.75F, System.Drawing.FontStyle.Bold);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(215, 42);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Image = global::DVLD_3.Properties.Resources.License_View_32;
            this.toolStripMenuItem1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(214, 38);
            this.toolStripMenuItem1.Text = "Show License Details";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // frmPersonLicensesHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(870, 536);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.personDetailsWithFilter1);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmPersonLicensesHistory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmPersonLicensesHistory";
            this.Load += new System.EventHandler(this.frmPersonLicensesHistory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPageLocalLicenses.ResumeLayout(false);
            this.tabPageLocalLicenses.PerformLayout();
            this.tabPageInternationalLicenses.ResumeLayout(false);
            this.tabPageInternationalLicenses.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private MangePeople.Controls.PersonDetailsWithFilter personDetailsWithFilter1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageLocalLicenses;
        private System.Windows.Forms.TabPage tabPageInternationalLicenses;
        private System.Windows.Forms.Label label1;
        private UserControls.PublicFormsPanel publicFormsPanelLocal;
        private System.Windows.Forms.Label label2;
        private UserControls.PublicFormsPanel publicFormsPanelInternational;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
    }
}