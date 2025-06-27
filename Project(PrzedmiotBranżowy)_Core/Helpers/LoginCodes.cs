using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_PrzedmiotBranzowy_Core.Helpers
{
    public enum LoginCodes
    {
        OK,
        EMPTY_USERNAME,
        EMPTY_PASSWORD,
        EMPTY_PASSWORD_REPEAT,
        DIFFERENT_PASSWORDS,
        WRONG_USERNAME,
        WRONG_PASSWORD
    }
}
