using System.Collections;
using System.Collections.Generic;

namespace WildArms.Helper
{
    internal static class DataInterpreter
    {
        public static List<string> ConvertElements(byte elements)
        {
            var list = new List<string>();
            var bits = new BitArray(new byte[] { elements });

            if (elements == 254)
            {
                list.Add("All");
                return list;
            }
            if (elements == 0)
            {
                list.Add("None");
                return list;
            }
            if (bits[0] == true)
            {
                list.Add("Mind");
            }
            if (bits[1] == true)
            {
                list.Add("Evil");
            }
            if (bits[2] == true)
            {
                list.Add("Holy");
            }
            if (bits[3] == true)
            {
                list.Add("Thunder");
            }
            if (bits[4] == true)
            {
                list.Add("Wind");
            }
            if (bits[5] == true)
            {
                list.Add("Fire");
            }
            if (bits[6] == true)
            {
                list.Add("Water");
            }
            if (bits[7] == true)
            {
                list.Add("Earth");
            }

            return list;
        }
    }
}
