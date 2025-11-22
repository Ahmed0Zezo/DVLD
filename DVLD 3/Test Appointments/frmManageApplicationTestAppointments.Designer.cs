namespace DVLD_3.Test_Appointments
{
    partial class frmManageApplicationTestAppointments
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
            this.lblHeader = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.picBoxControlImage = new System.Windows.Forms.PictureBox();
            this.publicFormsPanel1 = new DVLD_3.UserControls.PublicFormsPanel();
            this.ctrlLDApplicationInfo1 = new DVLD_3.Applications.Controls.ctrlLDApplicationInfo();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.takeTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxControlImage)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.Font = new System.Drawing.Font("Cairo", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.Red;
            this.lblHeader.Location = new System.Drawing.Point(178, 96);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(295, 40);
            this.lblHeader.TabIndex = 8;
            this.lblHeader.Text = "Test Appointment";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(11, 467);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 16);
            this.label1.TabIndex = 7;
            this.label1.Text = "Appointments";
            // 
            // picBoxControlImage
            // 
            this.picBoxControlImage.Image = global::DVLD_3.Properties.Resources.TestType_512;
            this.picBoxControlImage.Location = new System.Drawing.Point(254, -2);
            this.picBoxControlImage.Name = "picBoxControlImage";
            this.picBoxControlImage.Size = new System.Drawing.Size(143, 95);
            this.picBoxControlImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picBoxControlImage.TabIndex = 9;
            this.picBoxControlImage.TabStop = false;
            // 
            // publicFormsPanel1
            // 
            this.publicFormsPanel1.Location = new System.Drawing.Point(4, 455);
            this.publicFormsPanel1.Name = "publicFormsPanel1";
            this.publicFormsPanel1.Size = new System.Drawing.Size(645, 292);
            this.publicFormsPanel1.TabIndex = 6;
            // 
            // ctrlLDApplicationInfo1
            // 
            this.ctrlLDApplicationInfo1.Location = new System.Drawing.Point(3, 124);
            this.ctrlLDApplicationInfo1.Name = "ctrlLDApplicationInfo1";
            this.ctrlLDApplicationInfo1.Size = new System.Drawing.Size(645, 329);
            this.ctrlLDApplicationInfo1.TabIndex = 5;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Font = new System.Drawing.Font("Cairo", 9.75F, System.Drawing.FontStyle.Bold);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem,
            this.takeTestToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(197, 102);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Image = global::DVLD_3.Properties.Resources.edit_32;
            this.editToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(196, 38);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // takeTestToolStripMenuItem
            // 
            this.takeTestToolStripMenuItem.Name = "takeTestToolStripMenuItem";
            this.takeTestToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.takeTestToolStripMenuItem.Text = "Take Test";
            // 
            // frmManageApplicationTestAppointments
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(661, 500);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.ctrlLDApplicationInfo1);
            this.Controls.Add(this.picBoxControlImage);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.publicFormsPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmManageApplicationTestAppointments";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmTestAppointments";
            this.Load += new System.EventHandler(this.frmTestAppointments_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picBoxControlImage)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picBoxControlImage;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label label1;
        private UserControls.PublicFormsPanel publicFormsPanel1;
        private Applications.Controls.ctrlLDApplicationInfo ctrlLDApplicationInfo1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem takeTestToolStripMenuItem;
    }
}