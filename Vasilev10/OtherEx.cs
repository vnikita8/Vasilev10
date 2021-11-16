using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vasilev10
{
    internal static class OtherEx
    {
        public static string StringReverse(string inputString)
        {
            string reverse = "";
            for (int i = inputString.Length-1; i >= 0; i--)
            {
                reverse += inputString[i];
            }
            return reverse;
        }

        public static string TextUp(string fileText)
        {
            return fileText.ToUpper();
        }

        public static bool IsFormattableIS(object inObject)
        {
            return inObject is IFormattable;
        }

        public static bool IsFormatableAS(object inObject)
        {
            return inObject as IFormattable != null;
        }

        public static void SearchEmail(ref string s)
        {
            s = s.Split(Convert.ToChar("#"))[1];
        }
        public static string SearchUser(string s) //Своё
        {
            return s.Split(Convert.ToChar("#"))[0];
        }

    }
}
