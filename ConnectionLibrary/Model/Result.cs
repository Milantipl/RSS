using ConnectionLibrary.Model.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionLibrary.Model
{
    public class Result
    {
        public MemberOTPDetail loginDetail { get; set; }
        public MemberOTPDetail OTPDetail { get; set; }
        public _Login_Mast UserDetail { get; set; }
        public List<_ShakhaType> ListShakhaType { get; set; }
        public List<_MilanType> ListMilanType { get; set; }
        public List<_Bhag> ListBhag { get; set; }
        public List<_Nagar> ListNagar { get; set; }
        public List<_Vasti> ListVasti { get; set; }
        public List<_Vasti> ListSearchVasti { get; set; }
        public List<_Nagar> ListSearchNagar { get; set; }
        public List<_Month> ListMonth { get; set; }
        public List<_Vibhag> ListVibhag { get; set; }
        public List<_Sevakary> ListSevaKary { get; set; }
        public List<_Shakha> ListShakha { get; set; }
        public List<_Sevavasti> ListSevavasti { get; set; }
        public List<_Milan> ListMilan { get; set; }
        public List<_Shakhatime_Mast> ListShakhaTime { get; set; }
        public List<_Star_Mast> ListStar { get; set; }
        public List<_Dayitva_Mast> ListDayitva { get; set; }  
        

        public int VibhagID { get; set; }
        public int BhagID { get; set; }
        public int NagarID { get; set; }
        public int VastiID { get; set; }
        public int STID { get; set; }
        public int SKID { get; set; }
        public int ShakhaID { get; set; }
        public int AssignSVID { get; set; }
        public int ShakhaTime { get; set; }
        public int StarID { get; set; }
        public int DayitvaID { get; set; }
        public int SVID { get; set; }
        public int MonthID { get; set; }

        public int M_MonthID { get; set; }
        public int N_MonthID { get; set; }
        public int S_MonthID { get; set; }
        public int B_MonthID { get; set; }

        public int MilanID { get; set; }
        public int MTID { get; set; }
        public string SevaVasti { get; set; }
        public string Nagar { get; set; }
        public string Bhag { get; set; }
        public string Month { get; set; }
        public string Vasti { get; set; }
      
        
        public int SearchBhagID { get; set; }
        public int SearchNagarID { get; set; }
        public int SearchVibhagID { get; set; }
        public int SearchMonthID { get; set; }
        public int SearchVastiID { get; set; }
        public string SearchMilanID { get; set; }
        public int SearchSTID { get; set; }
        public int SearchMTID { get; set; }
        public int SearchShakhaID { get; set; }
        public int SearchSVID { get; set; }

        

        public string SearchSevavasti { get; set; }
        public string SearchToli { get; set; }
        public string SearchPalak { get; set; }
        public string SearchSevaUP { get; set; }
        public string SearchSevakary { get; set; }
        public string SearchShakha { get; set; }
      
       
        public List<_Bhag> ViewBhag { get; set; }
        public List<_Nagar> ViewNagar { get; set; }
        public List<_Vasti> ViewVasti { get; set; }
        public List<_Vibhag> ViewVibhag { get; set; }
        public List<_Shakha> ViewShakha { get; set; }
        public List<_Sevavasti> ViewSevaVasti { get; set; }
        public List<_Milan> ViewMilan { get; set; }

        public List<_MilanUPVrut> ViewMilanUPVrut { get; set; }
        public List<_ShakhaUPVrut> ViewShakhaUPVrut { get; set; }
        public List<_NagarVrut> ViewNagarVrut { get; set; }
        public List<_Login_Mast> ViewUser { get; set; }
        public List<_BethakVasti> ViewBethakvasti { get; set; }
        public List<_Pravasi_Karyakarta> ViewPravasikaryakarta { get; set; }

        public List<_Yadi> ListYadis { get; set; }

        public List<_WrongYadi> WrongYadis { get; set; }

        public Pager pager { get; set; }
        public int p { get; set; }
        public int vid { get; set; }
        public int Total { get; set; }
        
        public int MTotal { get; set; }
        public int STotal { get; set; }
        public int BTotal { get; set; }
        public int size = 10;
        public List<Report1> Report1List { get; set; }
        public List<Report2> Report2List { get; set; }
        public List<Report3> Report3List { get; set; }
        public List<Report4> Report4List { get; set; }
        public List<Report5> Report5List { get; set; }
        public List<Report6> Report6List { get; set; }
        public List<Report7> Report7List { get; set; }
        public List<Report8> Report8List { get; set; }
        public List<Report9> Report9List { get; set; }
        public List<Report10> Report10List { get; set; }
        public List<Report11> Report11List { get; set; }
        public List<Report12> Report12List { get; set; }
        public List<DashboardCounts> DashboardCountList { get; set; }
        public List<DashboardBandhShakha> DashboardBandShakhaList { get; set; }
        public List<DashboardNivasikaryakarta> DashboardNivasiKaryakartaList { get; set; }
        public List<ListofReport13> Report13List { get; set; }
        public string DashboardCountString { get; set; }
        public string DashboardBandhShakhaString { get; set; }
        public string DashboardNivasiKaryakartaString { get; set; }
        public string R1String { get; set; }
        public string R2String { get; set; }
        public string R3String { get; set; }
        public string R4String { get; set; }
        public string R5String { get; set; }
        public string R6String { get; set; }
        public string R7String { get; set; }
        public string R8String { get; set; }
        public string R9String { get; set; }

        public string R13String { get; set; }

    }
}
