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
    public class MasterRepository
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
        public static DateTime GetDate()
        {
            var cn = new ConnectionClass();
            var objGetDate = cn.Select("SELECT  Convert(date,GETDATE(),103)");
            System.Globalization.CultureInfo en = new System.Globalization.CultureInfo("en-GB");
            return Convert.ToDateTime(objGetDate.Rows[0][0].ToString(), en);
        }
        public static List<_Vibhag> GetListVibhag()
        {
            var result = new List<_Vibhag>();
            try
            {
                var cn = new ConnectionClass();
                var Result = cn.Select("Select VibhagID,Vibhag from Vibhag order by VibhagID");
                if (Result.Rows.Count > 0)
                {
                    result = Result.ToListof<_Vibhag>();
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
        public static List<_Bhag> GetListBhag()
        {
            var result = new List<_Bhag>();
            try
            {
                var cn = new ConnectionClass();
                var Result = cn.Select("Select BhagID,Bhag,VibhagID from Bhag order by BhagID ");
                if (Result.Rows.Count > 0)
                {
                    result = Result.ToListof<_Bhag>();
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

        
        public static List<_Nagar> GetListNagar()
        {
            var result = new List<_Nagar>();
            try
            {
                var cn = new ConnectionClass();
                var Result = cn.Select("Select NagarID,Nagar,BhagID from Nagar order by NagarID  ");
                if (Result.Rows.Count > 0)
                {
                    result = Result.ToListof<_Nagar>();
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
        public static List<_Nagar> GetListNagarWithBhag()
        {
            var result = new List<_Nagar>();
            try
            {
                var cn = new ConnectionClass();
                var Result = cn.Select(@"Select NagarID,Nagar+' - '+Bhag as Nagar,Bhag.BhagID,Bhag.VibhagID from Nagar 
left join Bhag on Bhag.BhagID=Nagar.BhagID
order by NagarID  ");
                if (Result.Rows.Count > 0)
                {
                    result = Result.ToListof<_Nagar>();
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
        public static List<_Month> GetListMonth()
        {
            var result = new List<_Month>();
            try
            {
                var cn = new ConnectionClass();
                var Result = cn.Select("Select MonthID,Month from Month order by MonthID  ");
                if (Result.Rows.Count > 0)
                {
                    result = Result.ToListof<_Month>();
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
        public static List<_Sevavasti> GetListSevavasti()
        {
            var result = new List<_Sevavasti>();
            try
            {
                var cn = new ConnectionClass();
                var Result = cn.Select("Select * from SevaVasti order by SevaVasti order by SVID");
                if (Result.Rows.Count > 0)
                {
                    result = Result.ToListof<_Sevavasti>();
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
        public static List<_Sevavasti> GetListSevavastiByNagar(int NagarID)
        {
            var result = new List<_Sevavasti>();
            try
            {
                var cn = new ConnectionClass();
                var Result = cn.Select("Select * from SevaVasti where NagarID =" + NagarID + " order by SVID");
                if (Result.Rows.Count > 0)
                {
                    result = Result.ToListof<_Sevavasti>();
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

        public static List<_Shakha> GetListShakhaByNagar(int NagarID)
        {
            var result = new List<_Shakha>();
            try
            {
                var cn = new ConnectionClass();
                var Result = cn.Select("Select * from Shakha where NagarID =" + NagarID + " order by ShakhaID ");
                if (Result.Rows.Count > 0)
                {
                    result = Result.ToListof<_Shakha>();
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
        public static List<_Vasti> GetListVasti()
        {
            var result = new List<_Vasti>();
            try
            {
                var cn = new ConnectionClass();
                var Result = cn.Select("Select VastiID,Vasti,NagarID from Vasti order by VastiID  ");
                if (Result.Rows.Count > 0)
                {
                    result = Result.ToListof<_Vasti>();
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
        public static List<_Vasti> GetListVasti_Nagar()
        {
            var result = new List<_Vasti>();
            try
            {
                var cn = new ConnectionClass();
                var Result = cn.Select(@"Select Vasti +'-' + Nagar as Vasti,VastiID,Bhag.VibhagID,Bhag.BhagID from Vasti
 left join Nagar on Nagar.NagarID=Vasti.NagarID
 left join Bhag on Bhag.BhagID=nagar.BhagID 
order by VastiID  ");
                if (Result.Rows.Count > 0)
                {
                    result = Result.ToListof<_Vasti>();
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

        public static List<_ShakhaType> GetListShakhaType()
        {
            var result = new List<_ShakhaType>();
            try
            {
                var cn = new ConnectionClass();
                var Result = cn.Select("Select STID,ShakhaType from ShakhaType order by STID");
                if (Result.Rows.Count > 0)
                {
                    result = Result.ToListof<_ShakhaType>();
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
        public static List<_Shakhatime_Mast> GetListShakhaTime()
        {
            var result = new List<_Shakhatime_Mast>();
            try
            {
                var cn = new ConnectionClass();
                var Result = cn.Select("Select ID,Shakhatime from Shakhatime_Mast order by ID");
                if (Result.Rows.Count > 0)
                {
                    result = Result.ToListof<_Shakhatime_Mast>();
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

        public static List<_MilanType> GetListMilanType()
        {
            var result = new List<_MilanType>();
            try
            {
                var cn = new ConnectionClass();
                var Result = cn.Select("Select MTID,MilanType from MilanType order by MTID");
                if (Result.Rows.Count > 0)
                {
                    result = Result.ToListof<_MilanType>();
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

        public static List<_Sevakary> GetListSevakary()
        {
            var result = new List<_Sevakary>();
            try
            {
                var cn = new ConnectionClass();
                var Result = cn.Select("Select SKID,SevaKary from SevaKary order by SKID ");
                if (Result.Rows.Count > 0)
                {
                    result = Result.ToListof<_Sevakary>();
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

        public static List<_Shakha> GetListShakhaByVasti(string VastiID)
        {
            var result = new List<_Shakha>();
            try
            {
                var cn = new ConnectionClass();
                var Result = cn.Select("select ShakhaName,ShakhaID from Shakha where VastiID in (" + VastiID + ") order by ShakhaID ");
                if (Result.Rows.Count > 0)
                {
                    result = Result.ToListof<_Shakha>();
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
        public static List<_Dayitva_Mast> GetListDayitva()
        {
            var result = new List<_Dayitva_Mast>();
            try
            {
                var cn = new ConnectionClass();
                var Result = cn.Select("select * from Dayitva_Mast  ");
                if (Result.Rows.Count > 0)
                {
                    result = Result.ToListof<_Dayitva_Mast>();
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
        public static List<_Star_Mast> GetListStar()
        {
            var result = new List<_Star_Mast>();
            try
            {
                var cn = new ConnectionClass();
                var Result = cn.Select("select * from Star_Mast  ");
                if (Result.Rows.Count > 0)
                {
                    result = Result.ToListof<_Star_Mast>();
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
        public static List<_Sevavasti> GetListSevavastiByVasti(string VastiID)
        {
            var result = new List<_Sevavasti>();
            try
            {
                var cn = new ConnectionClass();
                var Result = cn.Select("select SVID,SevaVasti from Sevavasti where VastiID in (" + VastiID + ") order by SVID ");
                if (Result.Rows.Count > 0)
                {
                    result = Result.ToListof<_Sevavasti>();
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
        public static List<_Milan> GetListMilanByVasti(string VastiID)
        {
            var result = new List<_Milan>();
            try
            {
                var cn = new ConnectionClass();
                var Result = cn.Select("select MilanID,MilanName from Milan where VastiID in (" + VastiID + ") order by MilanID ");
                if (Result.Rows.Count > 0)
                {
                    result = Result.ToListof<_Milan>();
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
        public static List<_Vasti> GetListvastiByNagar(string NagarID)
        {
            var result = new List<_Vasti>();
            try
            {
                var cn = new ConnectionClass();
                var Result = cn.Select("select Vasti,VastiID from Vasti where NagarID in (" + NagarID + ") order by VastiID ");
                if (Result.Rows.Count > 0)
                {
                    result = Result.ToListof<_Vasti>();
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
        public static List<_Nagar> GetListNagar(int BhagID)
        {
            var result = new List<_Nagar>();
            try
            {
                var cn = new ConnectionClass();
                var Result = cn.Select("Select NagarID,Nagar from Nagar where BhagID=" + BhagID + "order by NagarID");
                if (Result.Rows.Count > 0)
                {
                    result = Result.ToListof<_Nagar>();
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
        public static List<_Vibhag> ViewVibhag()
        {
            var result = new List<_Vibhag>();
            try
            {
                var cn = new ConnectionClass();
                //var Result = cn.Select("select Vibhag,(select Count(BhagID) from Bhag) as BhagCount,(select Count(NagarID) from Nagar) as NagarCount,(select Count(VastiID) from Vasti) as VastiCount from Vibhag order by VibhagID");
                var Result = cn.Select(@"SELECT  Vibhag.VibhagID,Vibhag,BhagCount ,Sum(NagarCount) as NagarCount,SUM(tbl2.TotalVasti) as VastiCount FROM Vibhag
											LEFT JOIN
                                            (
	                                         SELECT VibhagID,COUNT(BhagID) as BhagCount FROM Bhag GROUP BY VibhagID
                                            )tbl
                                            On
                                            Vibhag.VibhagID=tbl.VibhagID
											LEFT JOIN
                                            Bhag 
                                            ON
                                            Bhag.VibhagID=Vibhag.VibhagID
                                            LEFT JOIN
                                            (
	                                         SELECT BhagID,NagarID,COUNT(NagarID) as NagarCount FROM Nagar GROUP BY BhagID,nagarID
                                            )tbl1
                                            On
                                            Bhag.BhagID=tbl1.BhagID
                                            LEFT JOIN(
                                            SELECT NagarID,COUNT(VastiID) as TotalVasti FROM Vasti GROUP BY NagarID
                                            )tbl2
                                            ON
                                            tbl1.NagarId=tbl2.NagarId
                                           GROUP BY Vibhag.VibhagID,Vibhag,BhagCount
");
                if (Result.Rows.Count > 0)
                {
                    result = Result.ToListof<_Vibhag>();
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


        public static List<_Vibhag> ViewVibhagByID(int VibhagID)
        {
            var result = new List<_Vibhag>();
            try
            {
                var cn = new ConnectionClass();
                var Result = cn.Select("select Vibhag,(select Count(BhagID) from Bhag) as BhagCount,(select Count(NagarID) from Nagar) as NagarCount,(select Count(VastiID) from Vasti) as VastiCount from Vibhag where VibhagID=" + VibhagID);
                if (Result.Rows.Count > 0)
                {
                    result = Result.ToListof<_Vibhag>();
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


        public static List<_Bhag> ViewBhag()
        {
            var result = new List<_Bhag>();
            try
            {
                var cn = new ConnectionClass();
                var Result = cn.Select(@"SELECT  Bhag.VibhagID, Bhag.BhagID,Bhag,NagarCount,SUM(tbl2.TotalVasti) as VastiCount FROM Bhag
                                            LEFT JOIN
                                            (
	                                         SELECT BhagID,COUNT(NagarID) as NagarCount FROM Nagar GROUP BY BhagID
                                            )tbl1
                                            On
                                            Bhag.BhagID=tbl1.BhagID
                                            LEFT JOIN
                                            Nagar 
                                            ON
                                            Bhag.BhagID=Nagar.BhagId
                                            LEFT JOIN(
                                            SELECT NagarID,COUNT(VastiID) as TotalVasti FROM Vasti GROUP BY NagarID
                                            )tbl2
                                            ON
                                            Nagar.NagarId=tbl2.NagarId
                                           GROUP BY Bhag.VibhagID,Bhag.BhagID,Bhag,NagarCount

                                            ");
                if (Result.Rows.Count > 0)
                {
                    result = Result.ToListof<_Bhag>();
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


        public static List<_Bhag> ViewBhagByID(int BhagID)
        {
            var result = new List<_Bhag>();
            try
            {
                var cn = new ConnectionClass();
                var Result = cn.Select(@"SELECT Bhag,NagarCount,SUM(tbl2.TotalVasti) as VastiCount FROM Bhag
                                            LEFT JOIN
                                            (
	                                         SELECT BhagID,COUNT(NagarID) as NagarCount FROM Nagar GROUP BY BhagID
                                            )tbl1
                                            On
                                            Bhag.BhagID=tbl1.BhagID
                                            LEFT JOIN
                                            Nagar 
                                            ON
                                            Bhag.BhagID=Nagar.BhagId
                                            LEFT JOIN(
                                            SELECT NagarID,COUNT(VastiID) as TotalVasti FROM Vasti GROUP BY NagarID
                                            )tbl2
                                            ON
                                            Nagar.NagarId=tbl2.NagarId
                                            where Bhag.BhagID=" + BhagID + "GROUP BY Bhag,NagarCount");
                if (Result.Rows.Count > 0)
                {
                    result = Result.ToListof<_Bhag>();
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


        public static List<_Nagar> ViewNagar()
        {
            var result = new List<_Nagar>();
            try
            {
                var cn = new ConnectionClass();
                var Result = cn.Select(@"Select VibhagID,Bhag.BhagID,Nagar,Bhag,NagarID ,
                    (Select count(vastiID) from vasti where NagarID=Nagar.nagarID )  as VastiCount
                    from Nagar                        
                    left join Bhag on Bhag.BhagID=Nagar.BhagID
                    order by Nagar.NagarID");
                if (Result.Rows.Count > 0)
                {
                    result = Result.ToListof<_Nagar>();
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
        public static List<_Nagar> ViewNagarByID(string whereClause)
        {
            var result = new List<_Nagar>();
            try
            {
                var cn = new ConnectionClass();
                var Result = cn.Select(@"Select Nagar,Bhag, 
                (Select count(vastiID) from vasti where NagarID=Nagar.nagarID )  as VastiCount
                from Nagar                        
                 left join Bhag on Bhag.BhagID=Nagar.BhagID
                  " + whereClause + "order by Nagar.NagarID");
                if (Result.Rows.Count > 0)
                {
                    result = Result.ToListof<_Nagar>();
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

        public static List<_Vasti> ViewVasti()
        {
            var result = new List<_Vasti>();
            try
            {
                var cn = new ConnectionClass();
                var Result = cn.Select(@"Select Bhag.BhagID,VibhagID,Vasti,Nagar,Bhag from Vasti
                                        left join Nagar on Nagar.NagarID=Vasti.NagarID
                                        left join Bhag on Bhag.BhagID=Nagar.BhagID
order by VastiID");
                if (Result.Rows.Count > 0)
                {
                    result = Result.ToListof<_Vasti>();
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
        public static List<_Vasti> ViewVastiByID(string whereClause)
        {
            var result = new List<_Vasti>();
            try
            {
                var cn = new ConnectionClass();
                var Result = cn.Select(@"Select Vasti ,Nagar,Bhag from Vasti
                                        left join Nagar on Nagar.NagarID=Vasti.NagarID
                                        left join Bhag on Bhag.BhagID=Nagar.BhagID  " + whereClause + "order by VastiID");
                if (Result.Rows.Count > 0)
                {
                    result = Result.ToListof<_Vasti>();
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
        //Shakha
        public static bool InsertShakha(_Shakha shakha)
        {
            var cn = new ConnectionClass();
            try
            {
                var objInsertshakha = cn.Insert("INSERT INTO Shakha ([ShakhaName] ,[VastiID] ,[STID],[Toli],[Palak],[AssignSVID],[SevaUP],[Time],[ShakhaTime]) VALUES ('" + shakha.ShakhaName + "'," + shakha.VastiID + "," + shakha.STID + ",'" + shakha.Toli + "','" + shakha.Palak + "'," + shakha.AssignSVID + ",'" + shakha.SevaUP + "','" + shakha.Time + "'," + shakha.ShakhaTime + ")");
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static List<_Shakha> ViewShakha(string whereClause, int p, int Size, out int Total)
        {

            var cn = new ConnectionClass();
            Total = Convert.ToInt32(cn.Select(@"Select Count(ShakhaID)from Shakha 
                                    left join Vasti on Vasti.VastiID=Shakha.VastiID 
                                    left join nagar on nagar.nagarID=Vasti.NagarID
                                    left join Bhag on Bhag.BhagID=nagar.BhagID " + whereClause).Rows[0][0].ToString());
            return cn.Select(@"SELECT * FROM (select ROW_NUMBER() OVER (ORDER BY ShakhaID DESC) AS RowNumber, ShakhaName,ShakhaID,Vasti,Nagar,Bhag,Vasti.VastiID,STID,AssignSVID,Toli,Palak,SevaUP,Shakhatime_Mast.ID as ShakhaTime,Time from Shakha
                                    left join Vasti on Vasti.VastiID=Shakha.VastiID
                                    left join nagar on nagar.nagarID=Vasti.NagarID
                                    left join Bhag on Bhag.BhagID=nagar.BhagID
                                    left join Shakhatime_Mast on Shakhatime_Mast.ID=Shakha.ShakhaTime 
                                 " + whereClause + " )tbl1 WHERE RowNumber BETWEEN " + (((p - 1) * Size) + 1) + " AND " + ((((p - 1) * Size) + 1) + Size - 1) + "").ToListof<_Shakha>();


        }
        public static bool UpdateShakha(_Shakha shakha)
        {
            var cn = new ConnectionClass();
            try
            {
                var objUpdateshakha = cn.Update("UPDATE [RSSDB].[dbo].[Shakha] SET [ShakhaName] ='" + shakha.ShakhaName + "',[VastiID] =" + shakha.VastiID + "  ,[STID] = " + shakha.STID + " ,[Toli] ='" + shakha.Toli + "'  ,[Palak] = '" + shakha.Palak + "',[AssignSVID]=" + shakha.AssignSVID + ",[SevaUP] ='" + shakha.SevaUP + "',[Time]='" + shakha.Time + "',[ShakhaTime]=" + shakha.ShakhaTime + " WHERE ShakhaID=" + shakha.ShakhaID);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static bool DeleteShakha(int ShakhaID)
        {
            var cn = new ConnectionClass();
            try
            {
                var objDeleteshakha = cn.Delete("Delete from Shakha WHERE ShakhaID=" + ShakhaID);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static List<_Shakha> SearchShakha(string whereClause, int p, int Size, out int Total)
        {

            var cn = new ConnectionClass();
            Total = Convert.ToInt32(cn.Select(@"Select Count(ShakhaID) from Shakha   
                                    left join Vasti on Vasti.VastiID=Shakha.VastiID
                                    left join nagar on nagar.nagarID=Vasti.NagarID
                                    left join Bhag on Bhag.BhagID=nagar.BhagID " + whereClause).Rows[0][0].ToString());
            return cn.Select(@"SELECT * FROM (select ROW_NUMBER() OVER (ORDER BY ShakhaID DESC) AS RowNumber, ShakhaName,ShakhaID,Vasti,Nagar,Bhag,Vasti.VastiID,STID,AssignSVID,Toli,Palak,SevaUP,Shakhatime_Mast.ID as ShakhaTime,Time from Shakha
                                    left join Vasti on Vasti.VastiID=Shakha.VastiID
                                    left join nagar on nagar.nagarID=Vasti.NagarID
                                    left join Bhag on Bhag.BhagID=nagar.BhagID 
                                    left join Shakhatime_Mast on Shakhatime_Mast.ID=Shakha.ShakhaTime " + whereClause + ")tbl1 WHERE RowNumber BETWEEN " + (((p - 1) * Size) + 1) + " AND " + ((((p - 1) * Size) + 1) + Size - 1) + "").ToListof<_Shakha>();

        }


        //Milan
        public static bool InsertMilan(_Milan Milan)
        {
            var cn = new ConnectionClass();
            try
            {
                var objInsertMilan = cn.Insert("INSERT INTO Milan ([MilanName],[VastiID],[STID],[MTID]) VALUES ('" + Milan.MilanName + "'," + Milan.VastiID + "," + Milan.STID + "," + Milan.MTID + ")");
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static List<_Milan> ViewMilan(string whereClause, int p, int Size, out int Total)
        {

            var cn = new ConnectionClass();
            Total = Convert.ToInt32(cn.Select(@"Select Count(MilanID) from Milan
                                    left join Vasti on Vasti.VastiID=Milan.VastiID
                                    left join nagar on nagar.nagarID=Vasti.NagarID
                                    left join Bhag on Bhag.BhagID=nagar.BhagID
                                    " + whereClause).Rows[0][0].ToString()); ;
            return cn.Select(@"SELECT * FROM (select ROW_NUMBER() OVER (ORDER BY MilanID DESC) AS RowNumber, MilanName,Vasti,Nagar,Bhag,Vasti.VastiID,Milan.STID,Milan.MTID,MilanID from Milan
                                    left join Vasti on Vasti.VastiID=Milan.VastiID
                                    left join nagar on nagar.nagarID=Vasti.NagarID
                                    left join Bhag on Bhag.BhagID=nagar.BhagID
                                     " + whereClause + ")tbl1 WHERE RowNumber BETWEEN " + (((p - 1) * Size) + 1) + " AND " + ((((p - 1) * Size) + 1) + Size - 1) + "").ToListof<_Milan>();


        }
        public static bool UpdateMilan(_Milan Milan)
        {
            var cn = new ConnectionClass();
            try
            {
                var objUpdateshakha = cn.Update("UPDATE [RSSDB].[dbo].[Milan] SET [MilanName] ='" + Milan.MilanName + "',[VastiID] =" + Milan.VastiID + "  ,[STID] = " + Milan.STID + " ,[MTID]=" + Milan.MTID + " WHERE MilanID=" + Milan.MilanID);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static bool DeleteMilan(int MilanID)
        {
            var cn = new ConnectionClass();
            try
            {
                var objDeleteshakha = cn.Delete("Delete from Milan WHERE MilanID=" + MilanID);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static List<_Milan> SearchMilan(string whereClause, int p, int Size, out int Total)
        {

            var cn = new ConnectionClass();
            Total = Convert.ToInt32(cn.Select(@"Select Count(MilanID) from Milan   
                                    left join Vasti on Vasti.VastiID=Milan.VastiID
                                    left join nagar on nagar.nagarID=Vasti.NagarID
                                    left join Bhag on Bhag.BhagID=nagar.BhagID
                                     " + whereClause).Rows[0][0].ToString()); ;
            return cn.Select(@"SELECT * FROM (select ROW_NUMBER() OVER (ORDER BY MilanID DESC) AS RowNumber, MilanName,Vasti,Nagar,Bhag,Vasti.VastiID,Milan.STID,Milan.MTID,MilanID from Milan
                                    left join Vasti on Vasti.VastiID=Milan.VastiID
                                    left join nagar on nagar.nagarID=Vasti.NagarID
                                    left join Bhag on Bhag.BhagID=nagar.BhagID
                                      " + whereClause + ")tbl1 WHERE RowNumber BETWEEN " + (((p - 1) * Size) + 1) + " AND " + ((((p - 1) * Size) + 1) + Size - 1) + "").ToListof<_Milan>();

        }




        //SevaVasti
        public static bool InsertSevaVasti(Result SevaVasti)
        {
            var cn = new ConnectionClass();
            try
            {
                var objInsertMilan = cn.Insert("INSERT INTO SevaVasti ([SevaVasti],[VastiID],[NagarID],[ShakhaID],[SKID]) VALUES ('" + SevaVasti.SevaVasti + "'," + SevaVasti.VastiID + "," + SevaVasti.NagarID + "," + SevaVasti.ShakhaID + "," + SevaVasti.SKID + ")");
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static List<_Sevavasti> ViewSevaVasti(string whereClause, int p, int Size, out int Total)
        {

            var cn = new ConnectionClass();
            Total = Convert.ToInt32(cn.Select(@"Select Count(SVID) from SevaVasti left join Vasti on Vasti.VastiID=SevaVasti.VastiID
                                    left join nagar on nagar.nagarID=Vasti.NagarID
                                    left join Bhag on Bhag.BhagID=nagar.BhagID
                                    " + whereClause).Rows[0][0].ToString());
            return cn.Select(@"SELECT * FROM (select ROW_NUMBER() OVER (ORDER BY SVID DESC) AS RowNumber,SVID, SevaVasti,SevaVasti.ShakhaID,Vasti,Nagar,Bhag,Vasti.VastiID,SevaVasti.SKID from SevaVasti
                                    left join Vasti on Vasti.VastiID=SevaVasti.VastiID
                                    left join nagar on nagar.nagarID=Vasti.NagarID
                                    left join Bhag on Bhag.BhagID=nagar.BhagID
                                   " + whereClause + ")tbl1 WHERE RowNumber BETWEEN " + (((p - 1) * Size) + 1) + " AND " + ((((p - 1) * Size) + 1) + Size - 1) + "").ToListof<_Sevavasti>();


        }
        public static bool UpdateSevavasti(Result Sevavasti)
        {
            var cn = new ConnectionClass();
            try
            {
                var objUpdateshakha = cn.Update("UPDATE [RSSDB].[dbo].[SevaVasti] SET [SevaVasti] ='" + Sevavasti.SevaVasti + "',[VastiID] =" + Sevavasti.VastiID + "  ,[NagarID] = " + Sevavasti.NagarID + " ,[ShakhaID]=" + Sevavasti.ShakhaID + " ,[SKID]=" + Sevavasti.SKID + " WHERE SVID=" + Sevavasti.SVID);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static bool DeleteSevaVasti(int SVID)
        {
            var cn = new ConnectionClass();
            try
            {
                var objDeleteshakha = cn.Delete("Delete from SevaVasti WHERE SVID=" + SVID);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static List<_Sevavasti> SearchSevaVasti(string whereClause, int p, int Size, out int Total)
        {

            var cn = new ConnectionClass();
            Total = Convert.ToInt32(cn.Select(@"Select Count(SVID) from SevaVasti   
                                   left join Vasti on Vasti.VastiID=SevaVasti.VastiID
                                    left join nagar on nagar.nagarID=Vasti.NagarID
                                    left join Bhag on Bhag.BhagID=nagar.BhagID
                                    " + whereClause).Rows[0][0].ToString()); ;
            return cn.Select(@"SELECT * FROM (select ROW_NUMBER() OVER (ORDER BY SVID DESC) AS RowNumber,SVID, SevaVasti,SevaVasti.ShakhaID,Vasti,Nagar,Bhag,Vasti.VastiID,SevaVasti.SKID from SevaVasti
                                    left join Vasti on Vasti.VastiID=SevaVasti.VastiID
                                    left join nagar on nagar.nagarID=Vasti.NagarID
                                    left join Bhag on Bhag.BhagID=nagar.BhagID
                                     " + whereClause + ")tbl1 WHERE RowNumber BETWEEN " + (((p - 1) * Size) + 1) + " AND " + ((((p - 1) * Size) + 1) + Size - 1) + "").ToListof<_Sevavasti>();

        }


        //NagarVrut
        public static List<_ShakhaUPVrut> GetViewShakha_NagarVrut(string vastiID)
        {
            var result = new List<_ShakhaUPVrut>();
            try
            {
                var cn = new ConnectionClass();
                var Result = cn.Select(@"Select 0 as SUPVID,Shakha.ShakhaID,ShakhaName,0 as S_Tarun,0 as S_Bal,0 as S_Yog,0 as S_Shishu,0 as S_NagarID from Shakha 

left join ShakhaUPVrut on ShakhaUPVrut.ShakhaID=Shakha.ShakhaID
where VastiID in (" + vastiID + ") order by ShakhaID");
                if (Result.Rows.Count > 0)
                {
                    result = Result.ToListof<_ShakhaUPVrut>();
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

        public static List<_ShakhaUPVrut> GetViewShakha_NagarVrutByMonth(string vastiID, int MonthID)
        {
            var result = new List<_ShakhaUPVrut>();
            try
            {
                var cn = new ConnectionClass();
                var Result = cn.Select(@"Select ShakhaUPVrut.SUPVID,Shakha.ShakhaID,ShakhaName,S_Tarun,S_Bal,S_Yog,S_Shishu,Isnull(S_NagarID,0) as S_NagarID from Shakha 
inner join ShakhaUPVrut on ShakhaUPVrut.ShakhaID=Shakha.ShakhaID
where VastiID in (" + vastiID + ")  and S_monthID=" + MonthID + " order by ShakhaID");
                if (Result.Rows.Count > 0)
                {
                    result = Result.ToListof<_ShakhaUPVrut>();
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
        public static List<_MilanUPVrut> GetViewMilan_NagarVrut(string vastiIDs)
        {
            var result = new List<_MilanUPVrut>();
            try
            {
                var cn = new ConnectionClass();
                var Result = cn.Select(@"Select 0 as MVID, Milan.MilanID,MilanName,0 as M_Tarun,0 as M_Bal,0 as M_Yog,0 as M_Shishu,0 as M_NagarID from Milan 
left join  MilanUPVrut on MilanUPVrut.MilanID=Milan.MilanID
where VastiID in (" + vastiIDs + ") order by Milan.MilanID ");
                if (Result.Rows.Count > 0)
                {
                    result = Result.ToListof<_MilanUPVrut>();
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

        public static List<_MilanUPVrut> GetViewMilan_NagarVrutByMonth(string vastiIDs, int MonthID)
        {
            var result = new List<_MilanUPVrut>();
            try
            {
                var cn = new ConnectionClass();
                var Result = cn.Select(@"Select MilanUPVrut.MVID,Milan.MilanID,MilanName,M_Tarun,M_Bal,M_Yog,M_Shishu,Isnull(M_NagarID,0) as M_NagarID from Milan 
left join  MilanUPVrut on MilanUPVrut.MilanID=Milan.MilanID
where VastiID in (" + vastiIDs + ") and M_monthID=" + MonthID + " order by Milan.MilanID ");
                if (Result.Rows.Count > 0)
                {
                    result = Result.ToListof<_MilanUPVrut>();
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

        public static List<_BethakVasti> GetViewBethakVastiByMonth(string vastiIDs, int MonthID)
        {
            var result = new List<_BethakVasti>();
            try
            {
                var cn = new ConnectionClass();
                var Result = cn.Select(@"Select Vasti.VastiID,Vasti,1 ,FW_VishayBethak1,FW_VishayBethak2,FW_VishayBethak3,FW_VishayBethak4,FW_VishayBethak5,SW_KarykariniBethak,TW_VastiBethak,FW_VistrutBethak,1 as Active from Vasti
inner join Bethak_Vasti on Vasti.VastiID=Bethak_Vasti.VastiID
inner join Bethak on Bethak.BethakID=Bethak_Vasti.BethakID
where Vasti.VastiID in (" + vastiIDs + ") union All Select Vasti.VastiID,Vasti,0,0 as FW_VishayBethak1,0 as FW_VishayBethak2 ,0 as FW_VishayBethak3, 0 as FW_VishayBethak4, 0 as FW_VishayBethak15,0 as SW_KarykariniBethak,0 as TW_VastiBethak,0 as FW_VistrutBethak, 0 as Active from Vasti left join Bethak_Vasti on Vasti.VastiID=Bethak_Vasti.VastiID where Vasti.VastiID in (" + vastiIDs + ") and  Vasti.VastiID  not in (select vastiID from Bethak_Vasti )");
                if (Result.Rows.Count > 0)
                {
                    result = Result.ToListof<_BethakVasti>();
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
        public static List<_Bethak> GetViewBethakByMonth(int NagarID, int MonthID)
        {
            var result = new List<_Bethak>();
            try
            {
                var cn = new ConnectionClass();
                var Result = cn.Select(@"Select * from Bethak where B_MonthID=" + MonthID + "and B_nagarID=" + NagarID + "");
                if (Result.Rows.Count > 0)
                {
                    result = Result.ToListof<_Bethak>();
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
        public static List<_BethakVasti> GetViewBathakVasti(string vastiIDs)
        {
            var result = new List<_BethakVasti>();
            try
            {
                var cn = new ConnectionClass();
                var Result = cn.Select(@"Select Vasti.VastiID,Vasti,0 as Active from Vasti
where VastiID in (" + vastiIDs + ") order by VastiID ");
                if (Result.Rows.Count > 0)
                {
                    result = Result.ToListof<_BethakVasti>();
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

        public static List<_NagarVrut> GetViewNagarVrut(int NagarID, int MonthID)
        {
            var result = new List<_NagarVrut>();
            try
            {
                var cn = new ConnectionClass();
                var Result = cn.Select(@"select Nagar,Bhag,Isnull(NVID,0) as NVID,Nagar.NagarID,Bethak,Bvarg,BvargUP,AbyasVarg,Apekshit,Upsthit,Ektrikaran,N_Tarun,N_Bal,N_Yog,N_Shishu,isnull(Month.Month,0) as Month,N_MonthID,
(Select Count (SUPVID) from ShakhaUPVrut where ShakhaUPVrut.S_nagarID=Nagar.NagarID and ShakhaUPVrut.S_MonthID=" + MonthID + ") as ShakhaCount,(Select Count (MVID) from MilanUPVrut where MilanUPVrut.M_nagarID=Nagar.NagarID and MilanUPVrut.M_MonthID= " + MonthID + " ) as MilanCount,(Select Count (BethakVasti_ID) from Bethak_Vasti left join Bethak on Bethak.BethakID=Bethak_Vasti.BethakID where Bethak.B_nagarID=Nagar.NagarID and Bethak.B_MonthID= " + MonthID + " ) as BethakCount,(Select Count (BethakID) from Bethak where Bethak.B_nagarID=Nagar.NagarID and Bethak.B_MonthID= " + MonthID + " ) as BCount  from Nagar left join Bhag on Bhag.BhagID=Nagar.BhagID left join NagarVrut on NagarVrut.NagarID=Nagar.nagarID left join Month on Month.MonthID=NagarVrut.N_MonthID where Nagar.NagarID=" + NagarID + " and NagarVrut.N_MonthID=" + MonthID + " order by NagarID  ");
                if (Result.Rows.Count > 0)
                {
                    result = Result.ToListof<_NagarVrut>();
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

        public static bool InsertNagarVrut(_NagarVrut NagarVrut)
        {
            var cn = new ConnectionClass();
            try
            {
                var Result = cn.Select(@"select * from NagarVrut where NagarID=" + NagarVrut.NagarID + " and N_MonthID=" + NagarVrut.N_MonthID + " ");
                if (Result.Rows.Count == 0)
                {

                    var objInsert = cn.Insert(@"INSERT INTO [RSSDB].[dbo].[NagarVrut]
           ([NagarID],[Bethak],[Bvarg],[BvargUP],[AbyasVarg],[Apekshit],[Upsthit],[Ektrikaran],[N_Tarun],[N_Bal],[N_Yog],[N_Shishu],[N_MonthID])
VALUES (" + NagarVrut.NagarID + "," + NagarVrut.Bethak + ",'" + NagarVrut.Bvarg + "'," + NagarVrut.BvargUP + ",'" + NagarVrut.AbyasVarg + "'," + NagarVrut.Apekshit + "," + NagarVrut.Upsthit + ",'" + NagarVrut.Ektrikaran + "'," + NagarVrut.N_Tarun + "," + NagarVrut.N_Bal + "," + NagarVrut.N_Yog + "," + NagarVrut.N_Shishu + "," + NagarVrut.N_MonthID + ")");
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool UpdateNagarVrut(_NagarVrut NagarVrut)
        {
            var cn = new ConnectionClass();
            try
            {
                var objInsert = cn.Update(@"UPDATE [RSSDB].[dbo].[NagarVrut]
SET [NagarID] =" + NagarVrut.NagarID + " ,[Bethak] =" + NagarVrut.Bethak + " ,[Bvarg] ='" + NagarVrut.Bvarg + "' ,[BvargUP] =" + NagarVrut.BvargUP + "  ,[AbyasVarg] =' " + NagarVrut.AbyasVarg + "' ,[Apekshit] = " + NagarVrut.Apekshit + " ,[Upsthit] = " + NagarVrut.Upsthit + ",[Ektrikaran] ='" + NagarVrut.Ektrikaran + "' ,[N_Tarun] = " + NagarVrut.N_Tarun + ",[N_Bal] =" + NagarVrut.N_Bal + " ,[N_Yog] = " + NagarVrut.N_Yog + " ,[N_Shishu] =" + NagarVrut.N_Shishu + "  ,[N_MonthID] = " + NagarVrut.N_MonthID + " Where NVID=" + NagarVrut.NVID);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool InsertShakhaVrut(_ShakhaUPVrut ShakhaVrut)
        {
            var cn = new ConnectionClass();
            try
            {
                var objInsert = cn.Insert(@"INSERT INTO [RSSDB].[dbo].[ShakhaUPVrut]
           ([S_MonthID],[ShakhaID],[S_Tarun],[S_Bal],[S_Yog],[S_Shishu],[S_NagarID])
VALUES (" + ShakhaVrut.S_MonthID + "," + ShakhaVrut.ShakhaID + "," + ShakhaVrut.S_Tarun + "," + ShakhaVrut.S_Bal + "," + ShakhaVrut.S_Yog + "," + ShakhaVrut.S_Shishu + "," + ShakhaVrut.S_NagarID + ")");
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool InsertMilanVrut(_MilanUPVrut MilanVrut)
        {
            var cn = new ConnectionClass();
            try
            {
                var objInsert = cn.Insert(@"INSERT INTO [RSSDB].[dbo].[MilanUPVrut]
           ([M_MonthID],[MilanID] ,[M_Tarun] ,[M_Bal],[M_Yog],[M_Shishu],[M_NagarID])
VALUES (" + MilanVrut.M_MonthID + "," + MilanVrut.MilanID + "," + MilanVrut.M_Tarun + "," + MilanVrut.M_Bal + "," + MilanVrut.M_Yog + "," + MilanVrut.M_Shishu + "," + MilanVrut.M_NagarID + ")");
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool UpdateShakhaVrut(_ShakhaUPVrut ShakhaVrut)
        {
            var cn = new ConnectionClass();
            try
            {
                var objInsert = cn.Update(@"UPDATE [RSSDB].[dbo].[ShakhaUPVrut]
   SET [S_MonthID] =" + ShakhaVrut.S_MonthID + " ,[ShakhaID] =" + ShakhaVrut.ShakhaID + "  ,[S_Tarun] =" + ShakhaVrut.S_Tarun + "  ,[S_Bal] =" + ShakhaVrut.S_Bal + " ,[S_Yog] = " + ShakhaVrut.S_Yog + ",[S_Shishu] =" + ShakhaVrut.S_Shishu + "  ,[S_NagarID]=" + ShakhaVrut.S_NagarID + "Where SUPVID=" + ShakhaVrut.SUPVID);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool UpdateMilanVrut(_MilanUPVrut MilanVrut)
        {
            var cn = new ConnectionClass();
            try
            {
                var objInsert = cn.Update(@"UPDATE [RSSDB].[dbo].[MilanUPVrut]
   SET [M_MonthID] =" + MilanVrut.M_MonthID + " ,[MilanID] =" + MilanVrut.MilanID + "  ,[M_Tarun] =" + MilanVrut.M_Tarun + "  ,[M_Bal] =" + MilanVrut.M_Bal + " ,[M_Yog] = " + MilanVrut.M_Yog + ",[M_Shishu] =" + MilanVrut.M_Shishu + "  ,[M_NagarID]=" + MilanVrut.M_NagarID + "Where MVID=" + MilanVrut.MVID);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public static int InsertBethak(_Bethak Bethak)
        {
            var cn = new ConnectionClass();
            try
            {
                var Result = cn.Select(@"select * from Bethak where B_NagarID=" + Bethak.B_NagarID + " and B_MonthID=" + Bethak.B_MonthID + " ");
                if (Result.Rows.Count == 0)
                {
                    var objInsert = cn.InsertScope(@"INSERT INTO [dbo].[Bethak]([B_MonthID],[B_NagarID] ,[SW_KarykariniBethak] ,[TW_VastiBethak] ,[FW_VistrutBethak],[FW_VishayBethak1],[FW_VishayBethak2],[FW_VishayBethak3] ,[FW_VishayBethak4],[FW_VishayBethak5]     )
     VALUES (" + Bethak.B_MonthID + "," + Bethak.B_NagarID + ",'" + Bethak.SW_KarykariniBethak + "'," + Bethak.TW_VastiBethak + ",'" + Bethak.FW_VistrutBethak + "','" + Bethak.FW_VishayBethak1 + "','" + Bethak.FW_VishayBethak2 + "','" + Bethak.FW_VishayBethak3 + "','" + Bethak.FW_VishayBethak4 + "','" + Bethak.FW_VishayBethak5 + "');select SCOPE_IDENTITY();");
                    return objInsert;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public static bool InsertBethak_vasti(int BethakID, int vastiID)
        {
            var cn = new ConnectionClass();
            try
            {
                var Result = cn.Select(@"select * from Bethak_Vasti where BethakID=" + BethakID + " and VastiID=" + vastiID + " ");
                if (Result.Rows.Count == 0)
                {
                    var objInsert = cn.InsertScope(@"INSERT INTO [dbo].[Bethak_Vasti]
           (
           [BethakID]
           ,[VastiID])
     VALUES(" + BethakID + "," + vastiID + ")");
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool UpdateBethak(_Bethak Bethak)
        {
            var cn = new ConnectionClass();
            try
            {
                var objInsert = cn.Update(@"UPDATE [RSSDB].[dbo].[Bethak]
   SET [FW_VishayBethak1] ='" + Bethak.FW_VishayBethak1 + "',[FW_VishayBethak2] ='" + Bethak.FW_VishayBethak2 + "',[FW_VishayBethak3] ='" + Bethak.FW_VishayBethak3 + "',[FW_VishayBethak4] ='" + Bethak.FW_VishayBethak4 + "',[FW_VishayBethak5] ='" + Bethak.FW_VishayBethak5 + "' ,[SW_KarykariniBethak] ='" + Bethak.SW_KarykariniBethak + "' ,[TW_VastiBethak] =" + Bethak.TW_VastiBethak + ",[FW_VistrutBethak] = '" + Bethak.FW_VistrutBethak + "' Where BethakID=" + Bethak.BethakID);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static List<_Pravasi_Karyakarta> GetViewPravasiKaryakarta(int NagarID)
        {
            var result = new List<_Pravasi_Karyakarta>();
            try
            {
                var cn = new ConnectionClass();
                var Result = cn.Select(@"select PK.NagarID,PK.Pravasi_karyakartaID,PK.Name,PK.MobileNo,D.Dayitva,PK.DayitvaID,PK.StarID,PK.VastiID,S.Star,v.vasti from Pravasi_Karyakarta PK
left join Dayitva_Mast D on D.Id=PK.DayitvaID
left join Star_Mast S on S.ID=PK.StarID
left join Vasti v on v.vastiID=PK.vastiID
Where PK.NagarID=" + NagarID);
                if (Result.Rows.Count > 0)
                {
                    result = Result.ToListof<_Pravasi_Karyakarta>();
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

        public static bool InsertPravasiKaryaKarta(_Pravasi_Karyakarta Pk)
        {
            var cn = new ConnectionClass();
            try
            {
                var objInsert = cn.Insert(@"INSERT INTO [RSSDB].[dbo].[Pravasi_Karyakarta]
           ([Name]
           ,[MobileNo]
           ,[DayitvaID]
           ,[StarID]
           ,[VastiID]
,NagarID)
     VALUES('" + Pk.Name + "','" + Pk.MobileNo + "'," + Pk.DayitvaID + "," + Pk.StarID + "," + Pk.VastiID + "," + Pk.NagarID + ")");
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool UpdatePravasiKaryaKarta(_Pravasi_Karyakarta Pk)
        {
            var cn = new ConnectionClass();
            try
            {
                var objInsert = cn.Update(@"UPDATE [RSSDB].[dbo].[Pravasi_Karyakarta]
   SET [Name] ='" + Pk.Name + "' ,[MobileNo] ='" + Pk.MobileNo + "' ,[DayitvaID] =" + Pk.DayitvaID + " ,[StarID] =" + Pk.StarID + " ,[VastiID] =" + Pk.VastiID + "  Where Pravasi_karyakartaID=" + Pk.Pravasi_karyakartaID);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool DeletepravaSikaryakarta(int PKID)
        {
            var cn = new ConnectionClass();
            try
            {
                var objDelete = cn.Delete("Delete from Pravasi_Karyakarta WHERE Pravasi_karyakartaID=" + PKID);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool DeleteBethakVasti(int BethakID, string vastiID)
        {
            var cn = new ConnectionClass();
            try
            {
                var objDelete = cn.Delete("Delete from Bethak_Vasti WHERE BethakID=" + BethakID + " and VastiID not in (" + vastiID + ") ");
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static List<Report1> GetReport1(List<Tuple<string, string, SqlDbType, int?>> list)
        {
            var result = new List<Report1>();
            try
            {
                var cn = new ConnectionClass();
                DataTable dt = cn.ExecuteProcedure<DataTable>("Report1", list);
                if (dt.Rows.Count > 0)
                {
                    result = dt.ToListof<Report1>();
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

        public static List<Report2> GetReport2(List<Tuple<string, string, SqlDbType, int?>> list)
        {
            var result = new List<Report2>();
            try
            {
                var cn = new ConnectionClass();
                DataTable dt = cn.ExecuteProcedure<DataTable>("Report2", list);
                if (dt.Rows.Count > 0)
                {
                    result = dt.ToListof<Report2>();
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

        public static List<Report3> GetReport3()
        {
            var result = new List<Report3>();
            try
            {
                var cn = new ConnectionClass();
                DataTable dt = cn.ExecuteProcedure<DataTable>("Report3");
                if (dt.Rows.Count > 0)
                {
                    result = dt.ToListof<Report3>();
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
        public static List<Report4> GetReport4(List<Tuple<string, string, SqlDbType, int?>> list)
        {
            var result = new List<Report4>();
            try
            {
                var cn = new ConnectionClass();
                DataTable dt = cn.ExecuteProcedure<DataTable>("Report4", list);
                if (dt.Rows.Count > 0)
                {
                    result = dt.ToListof<Report4>();
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

        public static List<Report5> GetReport5(List<Tuple<string, string, SqlDbType, int?>> list)
        {
            var result = new List<Report5>();
            try
            {
                var cn = new ConnectionClass();
                DataTable dt = cn.ExecuteProcedure<DataTable>("Report5", list);
                if (dt.Rows.Count > 0)
                {
                    result = dt.ToListof<Report5>();
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

        public static List<Report6> GetReport6(List<Tuple<string, string, SqlDbType, int?>> list)
        {
            var result = new List<Report6>();
            try
            {
                var cn = new ConnectionClass();
                DataTable dt = cn.ExecuteProcedure<DataTable>("Report6", list);
                if (dt.Rows.Count > 0)
                {
                    result = dt.ToListof<Report6>();
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

        public static List<Report7> GetReport7(List<Tuple<string, string, SqlDbType, int?>> list)
        {
            var result = new List<Report7>();
            try
            {
                var cn = new ConnectionClass();
                DataTable dt = cn.ExecuteProcedure<DataTable>("Report7", list);
                if (dt.Rows.Count > 0)
                {
                    result = dt.ToListof<Report7>();
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

        public static List<Report8> GetReport8(List<Tuple<string, string, SqlDbType, int?>> list)
        {
            var result = new List<Report8>();
            try
            {
                var cn = new ConnectionClass();
                DataTable dt = cn.ExecuteProcedure<DataTable>("Report8", list);
                if (dt.Rows.Count > 0)
                {
                    result = dt.ToListof<Report8>();
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

        public static List<Report9> GetReport9(List<Tuple<string, string, SqlDbType, int?>> list)
        {
            var result = new List<Report9>();
            try
            {
                var cn = new ConnectionClass();
                DataTable dt = cn.ExecuteProcedure<DataTable>("Report9", list);
                if (dt.Rows.Count > 0)
                {
                    result = dt.ToListof<Report9>();
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

        public static List<Report10> GetReport10(List<Tuple<string, string, SqlDbType, int?>> list)
        {
            var result = new List<Report10>();
            try
            {
                var cn = new ConnectionClass();
                DataTable dt = cn.ExecuteProcedure<DataTable>("Report10", list);
                if (dt.Rows.Count > 0)
                {
                    result = dt.ToListof<Report10>();
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

        public static List<Report11> GetReport11(List<Tuple<string, string, SqlDbType, int?>> list)
        {
            var result = new List<Report11>();
            try
            {
                var cn = new ConnectionClass();
                DataTable dt = cn.ExecuteProcedure<DataTable>("Report11", list);
                if (dt.Rows.Count > 0)
                {
                    result = dt.ToListof<Report11>();
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

        public static List<Report12> GetReport12(List<Tuple<string, string, SqlDbType, int?>> list)
        {
            var result = new List<Report12>();
            try
            {
                var cn = new ConnectionClass();
                DataTable dt = cn.ExecuteProcedure<DataTable>("Report12", list);
                if (dt.Rows.Count > 0)
                {
                    result = dt.ToListof<Report12>();
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

        public static List<ShakhaReport> GetBandhShakhaList(int NagarID, int MonthID)
        {
            var result = new List<ShakhaReport>();
            try
            {
                var cn = new ConnectionClass();
                //                DataTable dt = cn.Select(@"SELECT ShakhaUPVrut.S_NagarID,Shakha.ShakhaID,ShakhaName FROM Shakha,ShakhaUPVrut WHERE Shakha.ShakhaID=ShakhaUPVrut.ShakhaID 
                //                                           AND S_NagarID in (" + NagarID + @") AND  S_Yog=0 AND S_MonthID=" + MonthID + "");


                DataTable dt1 = cn.Select(@"SELECT ShakhaName FROM Shakha WHERE ShakhaID in (
                                            SELECT 
                                            ShakhaID 
                                            FROM 
                                            ShakhaUPVrut WHERE S_NagarID=" + NagarID + @" AND S_Yog=0 AND S_MonthID=" + MonthID + @")");

                if (dt1.Rows.Count > 0)
                {
                    result = dt1.ToListof<ShakhaReport>();
                    return result;
                }
                else
                {
                    DataTable dt = cn.Select(@"SELECT ShakhaName FROM Shakha,(SELECT 
                                            Vasti.NagarID,
                                            Shakha.ShakhaID
                                            FROM Shakha,Vasti
                                            WHERE
                                            Shakha.VastiID=Vasti.VastiID
                                            AND Vasti.NagarID in (" + NagarID + @")) as tbl1
                                            WHERE Shakha.ShakhaID=tbl1.ShakhaID");

                    if (dt.Rows.Count > 0)
                    {
                        result = dt.ToListof<ShakhaReport>();
                        return result;
                    }
                    else
                    {
                        return null;
                    }

                }


            }
            catch (Exception Ex)
            {
                return null;
            }
        }

        public static List<MilanReport> GetBandhMilanList(int NagarID, int MonthID)
        {
            var result = new List<MilanReport>();
            try
            {
                var cn = new ConnectionClass();
                //                DataTable dt = cn.Select(@"SELECT MilanID,MilanName FROM Milan WHERE MilanID in (
                //                                            SELECT MilanID FROM MilanUPVrut WHERE M_Yog=0 AND M_MonthID=" + MonthID + " AND M_NagarID=" + NagarID + ")");

                //                 DataTable dt = cn.Select(@"SELECT MilanName FROM Milan,(SELECT 
                //                                            Vasti.NagarID,
                //                                            Milan.MilanID
                //                                            FROM Milan,Vasti
                //                                            WHERE
                //                                            Milan.VastiID=Vasti.VastiID
                //                                            AND Vasti.NagarID in (" + NagarID + @")
                //                                            UNION
                //                                            SELECT 
                //                                            Nagar.NagarID,
                //                                            MilanUPVrut.MilanID
                //                                            FROM MilanUPVrut,Nagar,Bhag WHERE M_Yog=0 AND M_MonthID=" + MonthID + @" AND M_NagarID=Nagar.NagarID
                //                                            AND
                //                                            Nagar.BhagID=Bhag.bhagId
                //                                            AND
                //                                            Nagar.NagarID in (" + NagarID + @"))tbl1
                //                                            WHERE
                //                                            Milan.MilanID=tbl1.MilanID");
                //                 if (dt.Rows.Count > 0)
                //                 {
                //                     result = dt.ToListof<MilanReport>();
                //                     return result;
                //                 }
                //                 else
                //                 {
                //                     return null;
                //                 }


                DataTable dt1 = cn.Select(@"SELECT MilanName FROM Milan,(SELECT 
                                            Nagar.NagarID,
                                            MilanUPVrut.MilanID
                                            FROM MilanUPVrut,Nagar,Bhag WHERE M_Yog=0 AND M_MonthID=" + MonthID + @" AND M_NagarID=Nagar.NagarID
                                            AND
                                            Nagar.BhagID=Bhag.bhagId
                                            AND
                                            Nagar.NagarID in (" + NagarID + @"))tbl1
                                            WHERE
                                            Milan.MilanID=tbl1.MilanID");
                if (dt1.Rows.Count > 0)
                {
                    result = dt1.ToListof<MilanReport>();
                    return result;
                }
                else
                {
                    DataTable dt = cn.Select(@"SELECT MilanName FROM Milan,(
                                            SELECT 
                                            Nagar.NagarID,
                                            MilanUPVrut.MilanID
                                            FROM MilanUPVrut,Nagar,Bhag WHERE M_Yog=0 AND M_MonthID=" + MonthID + @" AND M_NagarID=Nagar.NagarID
                                            AND
                                            Nagar.BhagID=Bhag.bhagId
                                            AND
                                            Nagar.NagarID in (" + NagarID + @"))tbl1
                                            WHERE
                                            Milan.MilanID=tbl1.MilanID");
                    if (dt.Rows.Count > 0)
                    {
                        result = dt.ToListof<MilanReport>();
                        return result;
                    }
                    else
                    {
                        return null;
                    }
                }

            }
            catch (Exception Ex)
            {
                return null;
            }
        }
        public static List<DashboardCounts> GetDashbordCount(List<Tuple<string, string, SqlDbType, int?>> list)
        {
            var result = new List<DashboardCounts>();
            try
            {
                var cn = new ConnectionClass();
                DataTable dt = cn.ExecuteProcedure<DataTable>("DashboardCount", list);
                if (dt.Rows.Count > 0)
                {
                    result = dt.ToListof<DashboardCounts>();
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

        public static List<DashboardBandhShakha> GetDashbordBandhShkha(List<Tuple<string, string, SqlDbType, int?>> list)
        {
            var result = new List<DashboardBandhShakha>();
            try
            {
                var cn = new ConnectionClass();
                DataTable dt = cn.ExecuteProcedure<DataTable>("DashboardBandhShakha", list);
                if (dt.Rows.Count > 0)
                {
                    result = dt.ToListof<DashboardBandhShakha>();
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
        public static List<DashboardNivasikaryakarta> GetDashbordNivasiKaryakarta(List<Tuple<string, string, SqlDbType, int?>> list)
        {
            var result = new List<DashboardNivasikaryakarta>();
            try
            {
                var cn = new ConnectionClass();
                DataTable dt = cn.ExecuteProcedure<DataTable>("DashboardKaryaVihinVasti", list);
                if (dt.Rows.Count > 0)
                {
                    result = dt.ToListof<DashboardNivasikaryakarta>();
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
        public static List<NoBethakVastiReport> GetNoBethakVastiList(int NagarID, int MonthID)
        {
            var result = new List<NoBethakVastiReport>();
            try
            {
                var cn = new ConnectionClass();
                //                DataTable dt = cn.Select(@"SELECT MilanID,MilanName FROM Milan WHERE MilanID in (
                //                                            SELECT MilanID FROM MilanUPVrut WHERE M_Yog=0 AND M_MonthID=" + MonthID + " AND M_NagarID=" + NagarID + ")");

                DataTable dt = cn.Select(@"SELECT 
                                            Vasti
                                            FROM 
                                            Vasti 
                                            WHERE 
                                            VastiID not in (SELECT VastiID FROM Bethak_Vasti WHERE BethakID in (SELECT BethakID FROM Bethak WHERE B_MonthID=" + MonthID + @" AND B_NagarID=" + NagarID + @"))
                                            AND
                                            Vasti.NagarID=" + NagarID);
                if (dt.Rows.Count > 0)
                {
                    result = dt.ToListof<NoBethakVastiReport>();
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

        public static List<NoBethakVastiReport> GetKaryVihinVastiList(int NagarID, int MonthID)
        {
            var result = new List<NoBethakVastiReport>();
            try
            {
                var cn = new ConnectionClass();


                DataTable dt = cn.Select(@"SELECT Vasti 
                                            FROM 
                                            Vasti 
                                            WHERE NagarID=" + NagarID + @" AND VastiID not in (SELECT VastiID FROM Shakha)");
                if (dt.Rows.Count > 0)
                {
                    result = dt.ToListof<NoBethakVastiReport>();
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

        public static List<NoBethakVastiReport> GetAllKaryVihinVastiList(int NagarID)
        {
            var result = new List<NoBethakVastiReport>();
            try
            {
                var cn = new ConnectionClass();


                DataTable dt = cn.Select(@"SELECT Vasti 
                                            FROM 
                                            Vasti 
                                            WHERE NagarID=" + NagarID + @" AND VastiID not in (SELECT VastiID FROM Shakha) AND VastiID not in (SELECT VastiID FROM Milan)");
                if (dt.Rows.Count > 0)
                {
                    result = dt.ToListof<NoBethakVastiReport>();
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
        public static List<NoBethakVastiReport> GetAllSamparkitVastiList(int NagarID)
        {
            var result = new List<NoBethakVastiReport>();
            try
            {
                var cn = new ConnectionClass();


                DataTable dt = cn.Select(@"SELECT 
Vasti.Vasti
FROM 
Milan
  left join Vasti on Vasti.VastiID=Milan.VastiID
  left join nagar on nagar.nagarID=Vasti.NagarID  
  Where nagar.nagarID=" + NagarID + @" order by Nagar.NagarID ");
                if (dt.Rows.Count > 0)
                {
                    result = dt.ToListof<NoBethakVastiReport>();
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
        public static List<NoBethakVastiReport> GetAllkaryayuktVastiList(int NagarID)
        {
            var result = new List<NoBethakVastiReport>();
            try
            {
                var cn = new ConnectionClass();


                DataTable dt = cn.Select(@"SELECT 
Vasti.Vasti
FROM 
Shakha
  left join Vasti on Vasti.VastiID=Shakha.VastiID
  left join nagar on nagar.nagarID=Vasti.NagarID  
  Where nagar.nagarID=" + NagarID + @" order by Nagar.NagarID ");
                if (dt.Rows.Count > 0)
                {
                    result = dt.ToListof<NoBethakVastiReport>();
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
    }
}
