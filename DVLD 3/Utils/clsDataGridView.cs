using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_3.Utils
{
    internal static class clsDataGridView
    {
        public static int GetID_FromDataGridView(DataGridView dataGridView,int IdCoulmnIndex)
        {
            return (int)dataGridView.SelectedCells[IdCoulmnIndex].Value;
        }

    }
}
