using DVLD_BusienessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_3
{
    internal class clsTextBoxUtil
    {
        public static void MakeTextBoxesHaveOnlyNumbers(TextBox txtBox)
        {

            StringBuilder NewText = new StringBuilder();

            for (int i = 0; i <= txtBox.Text.Length - 1; i++)
            {
                if (txtBox.Text[i] >= '0' && txtBox.Text[i] <= '9')
                {
                    NewText.Append(txtBox.Text[i]);
                }
            }

            txtBox.Text = NewText.ToString();
        }

        public static bool LinkTextBoxWithErrorProvider(TextBox txtBox, string ErrorMessage, bool ErrorCondition
            , ErrorProvider errorProvider, CancelEventArgs e)
        {
            

            if (ErrorCondition)
            {
                e.Cancel = true;

                errorProvider.SetError(txtBox, ErrorMessage);
                return false;
            }
            else
            {
                e.Cancel = false;
                errorProvider.SetError(txtBox, "");
                return true;
            }
        }
    }
}
