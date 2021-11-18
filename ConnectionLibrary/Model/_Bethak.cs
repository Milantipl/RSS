using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionLibrary.Model
{
   public class _Bethak
    {
       public int BethakID { get; set; }
       public int B_MonthID { get; set; }

       public int TW_VastiBethak { get; set; }
        public int B_NagarID { get; set; }
        public int B_BhagID { get; set; }
        public bool FW_VishayBethak1 { get; set; }
        public bool FW_VishayBethak2 { get; set; }
        public bool FW_VishayBethak3 { get; set; }
        public bool FW_VishayBethak4 { get; set; }
        public bool FW_VishayBethak5 { get; set; }
        public bool SW_KarykariniBethak { get; set; }
        public bool FW_VistrutBethak { get; set; }
        public string BVastiIDs { get; set; }
    }
}
