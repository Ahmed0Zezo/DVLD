using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusienessLayer
{
    public class clsEmail
    {
        public static List<string> EmailForms = new List<string>
{
    "@gmail.com",
    "@yahoo.com",
    "@outlook.com",
    "@hotmail.com",
    "@live.com",
    "@icloud.com",
    "@aol.com",
    "@protonmail.com",
    "@mail.com",
    "@zoho.com",
    "@yandex.com"
};
        public static bool IsEmailInCorrectForm(string Email)
        {
            if (string.IsNullOrEmpty(Email)) return false;

            foreach (string emailForm in EmailForms)
            {
                if (Email.Contains(emailForm))
                    return true;
            }
            return false;
        }
    }
}
