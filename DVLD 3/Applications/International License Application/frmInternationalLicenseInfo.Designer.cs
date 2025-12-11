namespace DVLD_3.Applications.International_License_Application
{
    partial class frmInternationalLicenseInfo
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
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.ctrlInternationalLicenseInfo1 = new DVLD_3.Applications.International_License_Application.Controls.ctrlInternationalLicenseInfo();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(182, 123);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(382, 29);
            this.label1.TabIndex = 8;
            this.label1.Text = "Driver International License Info";
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::DVLD_3.Properties.Resources.LicenseView_400;
            this.pictureBox3.Location = new System.Drawing.Point(314, 5);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(119, 115);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 10;
            this.pictureBox3.TabStop = false;
            // 
            // ctrlInternationalLicenseInfo1
            // 
            this.ctrlInternationalLicenseInfo1.Location = new System.Drawing.Point(-4, 155);
            this.ctrlInternationalLicenseInfo1.Name = "ctrlInternationalLicenseInfo1";
            this.ctrlInternationalLicenseInfo1.Size = new System.Drawing.Size(754, 235);
            this.ctrlInternationalLicenseInfo1.TabIndex = 11;
            // 
            // frmInternationalLicenseInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(752, 395);
            this.Controls.Add(this.ctrlInternationalLicenseInfo1);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmInternationalLicenseInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmInternationalLicenseInfo";
            this.Load += new System.EventHandler(this.frmInternationalLicenseInfo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox3;
        private Controls.ctrlInternationalLicenseInfo ctrlInternationalLicenseInfo1;
    }
}