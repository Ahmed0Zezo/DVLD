namespace DVLD_3.Applications.LocalDrivingLicenseApplication
{
    partial class frmShowLocalAppInfos
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
            this.ctrlLDApplicationInfo1 = new DVLD_3.Applications.Controls.ctrlLDApplicationInfo();
            this.SuspendLayout();
            // 
            // ctrlLDApplicationInfo1
            // 
            this.ctrlLDApplicationInfo1.Location = new System.Drawing.Point(5, 6);
            this.ctrlLDApplicationInfo1.Name = "ctrlLDApplicationInfo1";
            this.ctrlLDApplicationInfo1.Size = new System.Drawing.Size(645, 329);
            this.ctrlLDApplicationInfo1.TabIndex = 0;
            // 
            // frmShowLocalAppInfos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(653, 341);
            this.Controls.Add(this.ctrlLDApplicationInfo1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmShowLocalAppInfos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmShowLocalAppInfos";
            this.Load += new System.EventHandler(this.frmShowLocalAppInfos_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.ctrlLDApplicationInfo ctrlLDApplicationInfo1;
    }
}