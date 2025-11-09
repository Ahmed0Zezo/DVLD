using DVLD_3.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_3.MangePeople
{
    public partial class ShowPersonDetailsForm : Form
    {
        public bool IsPersonDataUpdated;

        int _personID;
        public ShowPersonDetailsForm(int PersonID)
        {
            InitializeComponent();
            IsPersonDataUpdated = false;
            _personID = PersonID;
            showPersonDetails1.LoadPersonInfo(_personID);

            showPersonDetails1.PersonUpdated += PersonDataUpdated;
        }

        public void PersonDataUpdated()
        {
            IsPersonDataUpdated = true;
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
