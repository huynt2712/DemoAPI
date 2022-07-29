using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringBuilder1
{
    internal class StringDataStore
    {
        private string[] strArr = new string[10]; // internal data storage

        public string this[int index] //this = strArr index 
        {
            get
            {
                if (index == 3)
                    return strArr[index] + "3";
                return strArr[index];
            }

            set
            {
                if (index == 2)
                    strArr[index] = "Item at index 2";

                strArr[index] = value;
            }
        }
    }
}
