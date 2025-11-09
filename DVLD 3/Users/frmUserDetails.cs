using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_3.Users
{
    public partial class frmUserDetails : Form
    {
        int _userID;
        public frmUserDetails(int UserID)
        {
            InitializeComponent();
            _userID = UserID;
        }

        private void frmUserDetails_Load(object sender, EventArgs e)
        {
            user_Details1.LoadUserInfo(_userID);
        }
    }
}
