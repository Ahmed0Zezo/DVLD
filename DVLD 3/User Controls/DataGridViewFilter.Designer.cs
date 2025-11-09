namespace DVLD_3.UserControls
{
    partial class DataGridViewFilter
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
            this.lblfilter = new System.Windows.Forms.Label();
            this.cbFilterItems = new System.Windows.Forms.ComboBox();
            this.txtFilteredValue = new System.Windows.Forms.TextBox();
            this.cbFilterValue = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // lblfilter
            // 
            this.lblfilter.AutoSize = true;
            this.lblfilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblfilter.Location = new System.Drawing.Point(3, 11);
            this.lblfilter.Name = "lblfilter";
            this.lblfilter.Size = new System.Drawing.Size(63, 15);
            this.lblfilter.TabIndex = 0;
            this.lblfilter.Text = "Filter By:";
            // 
            // cbFilterItems
            // 
            this.cbFilterItems.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFilterItems.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFilterItems.FormattingEnabled = true;
            this.cbFilterItems.Location = new System.Drawing.Point(63, 9);
            this.cbFilterItems.Name = "cbFilterItems";
            this.cbFilterItems.Size = new System.Drawing.Size(177, 24);
            this.cbFilterItems.TabIndex = 1;
            this.cbFilterItems.SelectedIndexChanged += new System.EventHandler(this.cbFilterItems_SelectedIndexChanged);
            // 
            // txtFilteredValue
            // 
            this.txtFilteredValue.Location = new System.Drawing.Point(246, 11);
            this.txtFilteredValue.Name = "txtFilteredValue";
            this.txtFilteredValue.Size = new System.Drawing.Size(177, 20);
            this.txtFilteredValue.TabIndex = 2;
            this.txtFilteredValue.TextChanged += new System.EventHandler(this.txtFilteredValue_TextChanged);
            // 
            // cbFilterValue
            // 
            this.cbFilterValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFilterValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFilterValue.FormattingEnabled = true;
            this.cbFilterValue.Items.AddRange(new object[] {
            "All",
            "Yes",
            "No"});
            this.cbFilterValue.Location = new System.Drawing.Point(246, 9);
            this.cbFilterValue.Name = "cbFilterValue";
            this.cbFilterValue.Size = new System.Drawing.Size(117, 24);
            this.cbFilterValue.TabIndex = 1;
            this.cbFilterValue.SelectedIndexChanged += new System.EventHandler(this.cbFilterValue_SelectedIndexChanged);
            // 
            // DataGridViewFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbFilterValue);
            this.Controls.Add(this.txtFilteredValue);
            this.Controls.Add(this.cbFilterItems);
            this.Controls.Add(this.lblfilter);
            this.Name = "DataGridViewFilter";
            this.Size = new System.Drawing.Size(429, 42);
            this.Load += new System.EventHandler(this.DataGridViewFilter_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblfilter;
        private System.Windows.Forms.ComboBox cbFilterItems;
        private System.Windows.Forms.TextBox txtFilteredValue;
        private System.Windows.Forms.ComboBox cbFilterValue;
    }
}
