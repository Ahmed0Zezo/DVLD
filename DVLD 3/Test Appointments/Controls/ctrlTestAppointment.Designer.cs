namespace DVLD_3.Tests.Controls
{
    partial class ctrlTestAppointment
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ctrlLDApplicationInfo1 = new DVLD_3.Applications.Controls.ctrlLDApplicationInfo();
            this.publicFormsPanel1 = new DVLD_3.UserControls.PublicFormsPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.picBoxControlImage = new System.Windows.Forms.PictureBox();
            this.lblHeader = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxControlImage)).BeginInit();
            this.SuspendLayout();
            // 
            // ctrlLDApplicationInfo1
            // 
            this.ctrlLDApplicationInfo1.Location = new System.Drawing.Point(3, 122);
            this.ctrlLDApplicationInfo1.Name = "ctrlLDApplicationInfo1";
            this.ctrlLDApplicationInfo1.Size = new System.Drawing.Size(645, 329);
            this.ctrlLDApplicationInfo1.TabIndex = 0;
            // 
            // publicFormsPanel1
            // 
            this.publicFormsPanel1.Location = new System.Drawing.Point(3, 446);
            this.publicFormsPanel1.Name = "publicFormsPanel1";
            this.publicFormsPanel1.Size = new System.Drawing.Size(645, 292);
            this.publicFormsPanel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 458);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Appointments";
            // 
            // picBoxControlImage
            // 
            this.picBoxControlImage.Image = global::DVLD_3.Properties.Resources.TestType_512;
            this.picBoxControlImage.Location = new System.Drawing.Point(263, 3);
            this.picBoxControlImage.Name = "picBoxControlImage";
            this.picBoxControlImage.Size = new System.Drawing.Size(143, 95);
            this.picBoxControlImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picBoxControlImage.TabIndex = 4;
            this.picBoxControlImage.TabStop = false;
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Cairo", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.Red;
            this.lblHeader.Location = new System.Drawing.Point(241, 91);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(181, 40);
            this.lblHeader.TabIndex = 3;
            this.lblHeader.Text = "Test Appointment";
            // 
            // ctrlTestAppointment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.picBoxControlImage);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.publicFormsPanel1);
            this.Controls.Add(this.ctrlLDApplicationInfo1);
            this.Name = "ctrlTestAppointment";
            this.Size = new System.Drawing.Size(651, 529);
            ((System.ComponentModel.ISupportInitialize)(this.picBoxControlImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Applications.Controls.ctrlLDApplicationInfo ctrlLDApplicationInfo1;
        private UserControls.PublicFormsPanel publicFormsPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox picBoxControlImage;
        private System.Windows.Forms.Label lblHeader;
    }
}
