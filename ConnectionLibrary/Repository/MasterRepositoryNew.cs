using ConnectionLibrary.Model;
using ConnectionLibrary.Model.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionLibrary.Repository
{
    public class MasterRepositoryNew
    {
        public static string LogError(string Query)
        {
            var cn = new ConnectionClass();

            try
            {
                var objGetData = cn.Insert(Query);
                return objGetData.ToString();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static List<_Yadi> GetListYadi()
        {
            var result = new List<_Yadi>();
            try
            {
                var cn = new ConnectionClass();
                string query = "Select YadiID,Name,FatherName,Surname,Bhag.Bhag as BhagID,Nagar.Nagar as NagarID,Mobile,Mail,Dob, Blood, Vasti.Vasti as NvastiID, MilanType.MilanType, Shakha.ShakhaName, JobType, Business, Study, " +
"SanghSikshan, PresentD, DSelect, Gatividhi, Padadhikari, Uniform, " +
"Vadhya, SanghPravesh, Abhiruchi, SelectOrganization" +
" from Yadi inner join Bhag on Yadi.BHagID = Bhag.BHagID" +
" inner join Nagar on Yadi.NagarID = Nagar.NagarID" +
" inner join Vasti on Yadi.NvastiID = Vasti.vastiid" +
" inner join MilanType on Yadi.MilanType = MilanType.MTID" +
" inner join Shakha on Yadi.ShakhaName = Shakha.ShakhaID" +
" order by YadiID";
                var Result = cn.Select(query);
                if (Result.Rows.Count > 0)
                {
                 //   result = Result.ToListof<_Yadi>();
                    result = Result.ToListof<_Yadi>();
                    return result;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception Ex)
            {
                return null;
            }
        }

        public static string GetBhagIdByName(string bhagName)
        {
            var result = "0";
            try
            {
                var cn = new ConnectionClass();
                DataTable dtbhag = cn.Select(@"Select BhagID from Bhag where Bhag= '" + bhagName + "'");
                if (dtbhag.Rows.Count > 0)

                {
                    result = dtbhag.Rows[0]["BhagID"].ToString();
                    return result;
                }
                else
                {
                    return result.ToString();
                }

            }
            catch (Exception Ex)
            {
                return result.ToString();
            }
        }

        public static string GetNagarByName(string NagarName)
        {
            var result = "0";
            try
            {
                var cn = new ConnectionClass();
                DataTable dtbhag = cn.Select(@"Select NagarID from Nagar where Nagar= '" + NagarName + "'");
                if (dtbhag.Rows.Count > 0)
                {
                    result = dtbhag.Rows[0]["NagarID"].ToString();
                    return result;
                }
                else
                {
                    return result.ToString();
                }
            }
            catch (Exception Ex)
            {
                return result.ToString();
            }
        }

        public static string GetNivasivastiByName(string vastiName)
        {
            var result = "0";
            try
            {
                var cn = new ConnectionClass();
                // DataTable dtbhag = cn.Select(@"Select VastiID from Vasti where Vasti= '" + vastiName + "' and NagarID="+ NagarID+"");
                DataTable dtbhag = cn.Select(@"Select VastiID from Vasti where Vasti= '" + vastiName + "'");
                if (dtbhag.Rows.Count > 0)

                {
                    result = dtbhag.Rows[0]["VastiID"].ToString();
                    return result;
                }
                else
                {
                    return result.ToString();
                }

            }
            catch (Exception Ex)
            {
                return result.ToString();
            }
        }

        public static string GetShakhaByName(string ShakhaName)
        {
            var result = "0";
            try
            {
                var cn = new ConnectionClass();
                DataTable dtbhag = cn.Select(@"Select ShakhaID from Shakha where ShakhaName= '" + ShakhaName + "'");
                if (dtbhag.Rows.Count > 0)

                {
                    result = dtbhag.Rows[0]["ShakhaID"].ToString();
                    return result;
                }
                else
                {
                    return result.ToString();
                }

            }
            catch (Exception Ex)
            {
                return result.ToString();
            }
        }

        public static string GetMTypeByName(string MTypeName)
        {
            var result = "0";
            try
            {
                var cn = new ConnectionClass();
                DataTable dtbhag = cn.Select(@"Select MTID from MilanType where MilanType= '" + MTypeName + "'");
                if (dtbhag.Rows.Count > 0)

                {
                    result = dtbhag.Rows[0]["MTID"].ToString();
                    return result;
                }
                else
                {
                    return result.ToString();
                }
            }
            catch (Exception Ex)
            {
                return result.ToString();
            }

        }
    }
}
