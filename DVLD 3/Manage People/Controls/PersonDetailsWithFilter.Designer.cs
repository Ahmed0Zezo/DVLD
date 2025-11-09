namespace DVLD_3.MangePeople.Controls
{
    partial class PersonDetailsWithFilter
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
            this.showPersonDetails1 = new DVLD_3.MangePeople.ShowPersonDetails();
            this.gbFilter = new System.Windows.Forms.GroupBox();
            this.dataGridViewFilter1 = new DVLD_3.UserControls.DataGridViewFilter();
            this.btnAddPerson = new System.Windows.Forms.Button();
            this.btnFindPerson = new System.Windows.Forms.Button();
            this.gbFilter.SuspendLayout();
            this.SuspendLayout();
            // 
            // showPersonDetails1
            // 
            this.showPersonDetails1.Location = new System.Drawing.Point(14, 73);
            this.showPersonDetails1.Name = "showPersonDetails1";
            this.showPersonDetails1.Size = new System.Drawing.Size(623, 237);
            this.showPersonDetails1.TabIndex = 0;
            // 
            // gbFilter
            // 
            this.gbFilter.Controls.Add(this.dataGridViewFilter1);
            this.gbFilter.Controls.Add(this.btnAddPerson);
            this.gbFilter.Controls.Add(this.btnFindPerson);
            this.gbFilter.Location = new System.Drawing.Point(14, 2);
            this.gbFilter.Name = "gbFilter";
            this.gbFilter.Size = new System.Drawing.Size(623, 67);
            this.gbFilter.TabIndex = 1;
            this.gbFilter.TabStop = false;
            this.gbFilter.Text = "Person Filter";
            // 
            // dataGridViewFilter1
            // 
            this.dataGridViewFilter1.Location = new System.Drawing.Point(6, 19);
            this.dataGridViewFilter1.Name = "dataGridViewFilter1";
            this.dataGridViewFilter1.Size = new System.Drawing.Size(429, 42);
            this.dataGridViewFilter1.TabIndex = 6;
            // 
            // btnAddPerson
            // 
            this.btnAddPerson.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddPerson.Image = global::DVLD_3.Properties.Resources.AddPerson_32;
            this.btnAddPerson.Location = new System.Drawing.Point(527, 19);
            this.btnAddPerson.Name = "btnAddPerson";
            this.btnAddPerson.Size = new System.Drawing.Size(42, 42);
            this.btnAddPerson.TabIndex = 4;
            this.btnAddPerson.UseVisualStyleBackColor = true;
            this.btnAddPerson.Click += new System.EventHandler(this.btnAddPerson_Click);
            // 
            // btnFindPerson
            // 
            this.btnFindPerson.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFindPerson.Image = global::DVLD_3.Properties.Resources.SearchPerson;
            this.btnFindPerson.Location = new System.Drawing.Point(479, 19);
            this.btnFindPerson.Name = "btnFindPerson";
            this.btnFindPerson.Size = new System.Drawing.Size(42, 42);
            this.btnFindPerson.TabIndex = 5;
            this.btnFindPerson.UseVisualStyleBackColor = true;
            this.btnFindPerson.Click += new System.EventHandler(this.btnFindPerson_Click);
            // 
            // PersonDetailsWithFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbFilter);
            this.Controls.Add(this.showPersonDetails1);
            this.Name = "PersonDetailsWithFilter";
            this.Size = new System.Drawing.Size(648, 320);
            this.Load += new System.EventHandler(this.PersonDetailsWithFilter_Load);
            this.gbFilter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ShowPersonDetails showPersonDetails1 = new ShowPersonDetails();
        private System.Windows.Forms.GroupBox gbFilter;
        private UserControls.DataGridViewFilter dataGridViewFilter1;
        private System.Windows.Forms.Button btnAddPerson;
        private System.Windows.Forms.Button btnFindPerson;
    }
}
