using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConnectionLibrary.Model;

namespace ConnectionLibrary.Repository
{
   public class AccountRepository
    {
       public static MemberOTPDetail ValidateMobileNo(string MobileNo, out string Error)
       {
           var result = new MemberOTPDetail();
           var error1 = String.Empty;
           try
           {
               var cn = new ConnectionClass();
               var Result = cn.Select("Select * From Login_Mast where  MobileNo='" + MobileNo.Replace("'", "''").Trim() + "'", out error1);
               if (Result.Rows.Count > 0)
               {
                   result = Result.ToListof<MemberOTPDetail>().FirstOrDefault();
                   Error = null;
                   return result;

               }
               else
               {
                   Error = null;
                   return null;
               }

           }
           catch (Exception ex)
           {
               Error = error1;
               return null;
           }
       }
       public static MemberOTPDetail CheckOPTAlreadyExist(string MobileNo)
       {
           var result = new MemberOTPDetail();
           try
           {
               var cn = new ConnectionClass();
               //select ID,MobileNo,OTP,CONVERT(varchar(10),OTPGDateTime,105) as OTPGDateTime  FROM dbo.MemberOTPDetail WHERE  IsSend=1 AND CONVERT(date,OTPGDateTime,103)=CONVERT(date,GETDATE(),103) and   MobileNo='8866357628' and DATEPART(hh,OTPGDateTime) =DATEPART(hh,getdate()) and DATEPART(n,OTPGDateTime)<=DATEPART(n,getdate())
              // var Result = cn.Select("select ID,MobileNo,OTP,CONVERT(varchar(10),OTPGDateTime,105) as OTPGDateTime  FROM dbo.MemberOTPDetail WHERE  IsSend=1 AND CONVERT(date,OTPGDateTime,103)=CONVERT(date,GETDATE(),103) and  MobileNo='" + MobileNo.Replace("'", "''").Trim() + "'");
               var Result = cn.Select("select ID,MobileNo,OTP,CONVERT(varchar(10),OTPGDateTime,105) as OTPGDateTime  FROM dbo.MemberOTPDetail WHERE  IsSend=1  and  MobileNo='" + MobileNo.Replace("'", "''").Trim() + "'");
               if (Result.Rows.Count > 0)
               {
                   result = Result.ToListof<MemberOTPDetail>().FirstOrDefault();
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
       public static int InsertOTPDetail(string MobileNo, string IPAddress, int OTP, bool issend)
       {
           try
           {
               var cn = new ConnectionClass();
               //var objGetOTPDetail = cn.InsertScope("INSERT INTO MemberOTPDetail(MobileNo, IPAddress, OTP, OTPGDateTime, IsSend, IsVerified) VALUES('" + MobileNo.Replace("'", "''") + "','" + IPAddress + "'," + OTP + ",GETDATE(),'" + issend + "',0);select SCOPE_IDENTITY();");
               var objGetOTPDetail = cn.InsertScope("INSERT INTO MemberOTPDetail(MobileNo, IPAddress, OTP, OTPGDateTime, IsSend, IsVerified) VALUES('" + MobileNo.Replace("'", "''") + "','" + IPAddress + "',123456,GETDATE(),'" + issend + "',0);select SCOPE_IDENTITY();");
               return objGetOTPDetail;
           }
           catch (Exception ex)
           {
               return 0;
           }
       }
       public static MemberOTPDetail ValidateOTP(string MobileNo, int OTP)
       {
           var result = new MemberOTPDetail();
           try
           {
               var cn = new ConnectionClass();
               //and DATEPART(hh,OTPGDateTime) =DATEPART(hh,getdate()) and DATEPART(n,OTPGDateTime)<=DATEPART(n,getdate())
              // var Result = cn.Select("select ID,m.MobileNo,OTP,CONVERT(varchar(10),OTPGDateTime,105) as OTPGDateTime ,l.LoginId,l.Username FROM dbo.MemberOTPDetail  m left join Login_Mast l on l.mobileno=m.mobileno WHERE  IsSend=1 AND CONVERT(date,OTPGDateTime,103)=CONVERT(date,GETDATE(),103) and  m.MobileNo='" + MobileNo.Replace("'", "''").Trim() + "' and OTP='" + OTP + "'");
               var Result = cn.Select("select ID,m.MobileNo,OTP,CONVERT(varchar(10),OTPGDateTime,105) as OTPGDateTime ,l.LoginId,l.Username FROM dbo.MemberOTPDetail  m left join Login_Mast l on l.mobileno=m.mobileno WHERE  IsSend=1  and  m.MobileNo='" + MobileNo.Replace("'", "''").Trim() + "' and OTP='" + OTP + "'");
               if (Result.Rows.Count > 0)
               {
                   result = Result.ToListof<MemberOTPDetail>().FirstOrDefault();
                   return result;
               }
               else
               {
                   return null;
               }

           }
           catch (Exception)
           {
               return null;
           }
       }
       public static bool UpdateOTPDetail(int ID)
       {
           try
           {
               var cn = new ConnectionClass();
               var objGetOTPDetail = cn.Update("Update MemberOTPDetail Set IsVerified = 1 where ID = " + ID);
               if (objGetOTPDetail > 0)
               {
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
  
       public static _Login_Mast GetuserDetail(int UID)
       {
           var result = new _Login_Mast();
           try
           {
               var cn = new ConnectionClass();
               var Result = cn.Select("Select * From Login_Mast where IsActive=1 and LoginId=" + UID + "");
               if (Result.Rows.Count > 0)
               {
                   result = Result.ToListof<_Login_Mast>().FirstOrDefault();
                   return result;
               }
               else
               {
                   return null;
               }

           }
           catch (Exception)
           {
               return null;
           }
       }
       public static bool InsertUser(_Login_Mast user)
       {
           var cn = new ConnectionClass();
           try
           {
               var objInsert = cn.Insert("INSERT INTO Login_Mast ([Username],[Roleid],[IsActive],[MobileNo],[RoleWiseDept]) VALUES ('" + user.Username + "'," + user.Roleid + ",1,'" + user.MobileNo + "'," + user.RoleWiseDept + ")");
               return true;
           }
           catch (Exception ex)
           {
               return false;
           }
       }
       public static List<_Login_Mast> ViewUser(int p, int Size, out int Total)
       {

           var cn = new ConnectionClass();
           Total = Convert.ToInt32(cn.Select(@"Select Count(LoginId) from Login_Mast").Rows[0][0].ToString()); ;
           return cn.Select(@"SELECT * FROM (select ROW_NUMBER() OVER (ORDER BY LoginId DESC) AS RowNumber, * from Login_Mast
                                    left join Role_Mast on Role_Mast.ID=Login_Mast.Roleid)tbl1 WHERE RowNumber BETWEEN " + (((p - 1) * Size) + 1) + " AND " + ((((p - 1) * Size) + 1) + Size - 1) + "").ToListof<_Login_Mast>();


       }
       public static bool UpdateUser(_Login_Mast user)
       {
           var cn = new ConnectionClass();
           try
           {
               var objUpdate = cn.Update("UPDATE [RSSDB].[dbo].[Login_Mast] SET [Username] ='" + user.Username + "',[MobileNo] ='" + user.MobileNo + "'  ,[RoleWiseDept] = " + user.RoleWiseDept + "' WHERE LoginId=" + user.LoginId);
               return true;
           }
           catch (Exception ex)
           {
               return false;
           }
       }
       public static bool DeleteUser(int LoginID)
       {
           var cn = new ConnectionClass();
           try
           {
               var objDelete = cn.Delete("Delete from Login_Mast WHERE LoginId=" + LoginID);
               return true;
           }
           catch (Exception ex)
           {
               return false;
           }
       }

    }
}
