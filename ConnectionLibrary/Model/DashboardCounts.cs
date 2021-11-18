using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionLibrary.Model
{
     public class DashboardCounts
    {
        public int SrNo { get; set; }
        public int BhagID { get; set; }
        public int VibhagID { get; set; }
        public string Bhag { get; set; }
        public int TotalVasti { get; set; }
        public int Karyayukt { get; set; }
        public int Samparkit { get; set; }
        public int SamparkitVasti { get; set; }
        public int TotalShakha { get; set; }
        public int TotalMilan { get; set; }
    }
}
