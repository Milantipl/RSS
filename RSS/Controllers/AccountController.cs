using ConnectionLibrary.Model;
using ConnectionLibrary.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;


namespace RSS.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/

        public ActionResult Index()
        {
            Session["UID"] = null;
            return View();
        }
        public ActionResult MobileOTPVerification(string MobileNo)
        {
            try
            {
                var model = new Result();
                var Status = false;
                var Error = "";
                model.loginDetail = AccountRepository.ValidateMobileNo(MobileNo, out Error);

                if (model.loginDetail != null)
                {
                    if (model.loginDetail.LoginId != null)
                    {
                        try
                        {
                            WebResponse myResponse = null;
                            string result = string.Empty;
                            string strOTP = string.Empty;

                            model.OTPDetail = AccountRepository.CheckOPTAlreadyExist(MobileNo);

                            if (model.OTPDetail != null)
                            {
                                if (model.OTPDetail.MobileNo != null)
                                {

                                    return Json(model.OTPDetail.ID.ToString(), JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    return Json("0", JsonRequestBehavior.AllowGet);

                                }
                            }
                            else
                            {
                                Random generator = new Random();
                                strOTP = generator.Next(0, 1000000).ToString("D6");
                                // string finalUrl = "http://ip.infisms.com/smsserver/SMS10N.aspx?Userid=" + ConfigurationManager.AppSettings["SMSUserName"].ToString() + "&UserPassword=" + ConfigurationManager.AppSettings["SMSPassword"].ToString() + "&PhoneNumber=91" + MobileNo + "&Text=" + ConfigurationManager.AppSettings["SMSText"].ToString() + strOTP;
                                //string finalUrl = ConfigurationManager.AppSettings["SMSAPIUrl"] + MobileNo.Replace("'", "''") + "?SMSText=" + ConfigurationManager.AppSettings["SMSText"] + " " + strOTP;
                                //HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(finalUrl);
                                //myResponse = myRequest.GetResponse();
                                //Stream st = myResponse.GetResponseStream();
                                //Encoding ec = System.Text.Encoding.GetEncoding("utf-8");
                                //using (StreamReader reader = new System.IO.StreamReader(st, ec))
                                //{
                                //    result = reader.ReadToEnd();
                                //}
                                //st.Close();


                                //model.OTPDetail = AccountRepository.GetEmailDetail(MobileNo);
                                var mailerror = "";
                                //var Strbody = ConfigurationManager.AppSettings["MailText"] + strOTP;
                                //var StrSub = strOTP + " for " + model.OTPDetail.Name + " on " + MasterRepository.GetDate().ToString() + " - www.biddetail.com";
                                // var issend=CommonFunction.sendMail(Strbody, StrSub, model.OTPDetail.Email, "mailSettings/smtp",out mailerror);
                                string strIPAddress = "";
                                strIPAddress = Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString();


                                var OTPID = AccountRepository.InsertOTPDetail(MobileNo, strIPAddress, Convert.ToInt32(strOTP), true);

                                Status = true;
                                ViewBag.Error = mailerror;
                                return Json(OTPID.ToString(), JsonRequestBehavior.AllowGet);
                            }
                        }
                        catch (Exception ex)
                        {

                            var strErrorLog = @"INSERT INTO ErrorLog(ErrorMessage,ErrorAction,ErrorType,UserID,ErrorDate) VALUES ('" + ex.Message + "','OTPVerify()','OTPVerify','','" + MasterRepository.GetDate() + "')";
                            Convert.ToInt32(MasterRepository.LogError(strErrorLog));
                            ViewBag.Error = ex.Message;
                        }
                    }
                }
                else
                {

                    ViewBag.MobileStatus = false;
                    ViewBag.Error = Error;
                    return Json("0", JsonRequestBehavior.AllowGet);


                }

            }
            catch (Exception ex)
            {

                ViewBag.Error = ex.ToString();
                return Json("0", JsonRequestBehavior.AllowGet);
            }
            return View();

        }
        public ActionResult VerifyOTP(string MobileNo,string OTP,string ID)
        {

            if (ModelState.IsValid)
            {
                var model = new Result();
                var Status = false;
                model.loginDetail = AccountRepository.ValidateOTP(MobileNo,Convert.ToInt32(OTP));
                if (model.loginDetail != null)
                {
                    if (model.loginDetail.MobileNo != null)
                    {

                        var status = AccountRepository.UpdateOTPDetail(Convert.ToInt32(ID));
                        FormsAuthentication.SetAuthCookie(model.loginDetail.Username, false);
                        Session["UID"] = model.loginDetail.LoginId;
                        Session["userName"] = model.loginDetail.Username;
                        //return RedirectToAction("Login", "Account",new { Mno= mno });

                        return Json("1", JsonRequestBehavior.AllowGet); //1 means Success

                    }
                    else
                    {
                        return Json("0", JsonRequestBehavior.AllowGet);

                    }
                }
                else
                {

                    return Json("0", JsonRequestBehavior.AllowGet);
                }
            }
            return View();
        }
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
           
            Session["userName"] = null;
            Session["UID"] = null;
            return Redirect("/Account/Index");
           
        }
        public ActionResult CreateUser()
        {
            var model = new Result();
            if (Session["UID"] != null)
            {
                model.UserDetail = AccountRepository.GetuserDetail(Convert.ToInt32(Session["UID"]));
                model.ListVibhag = MasterRepository.GetListVibhag();
                var firstitemVibhag = new _Vibhag();
                firstitemVibhag.VibhagID = 0;
                firstitemVibhag.Vibhag = "--Select Vibhag---";
                model.ListVibhag.Insert(0, firstitemVibhag);

                model.ListBhag = MasterRepository.GetListBhag();
                var firstitembhag = new _Bhag();
                firstitembhag.BhagID = 0;
                firstitembhag.Bhag = "--Select Bhag---";
                model.ListBhag.Insert(0, firstitembhag);

                model.p = model.p == 0 ? 1 : model.p;
                var Total = 0;
                model.ViewUser = AccountRepository.ViewUser(model.p, model.size, out Total);
                model.Total = Total;
                var pager = new Pager(model.Total, model.p);
                model.pager = pager;

            }
            else
            {
                return RedirectToAction("LogOff", "Account");
            }
            return View(model);

        }
        public ActionResult InsertUser(_Login_Mast user)
        {
            var model = new Result();
            if (Session["UID"] != null)
            {
                var result = false;
                if (user.LoginId.ToString() != "0")
                {
                    result = AccountRepository.UpdateUser(user);

                }
                else
                {
                    result = AccountRepository.InsertUser(user);
                }


                model.p = model.p == 0 ? 1 : model.p;
                var Total = 0;
                model.ViewUser = AccountRepository.ViewUser(model.p, model.size, out Total);
                model.Total = Total;
                var pager = new Pager(model.Total, model.p);
                model.pager = pager;
                if (model.ViewUser.Count > 0)
                {
                    return PartialView("~/Views/Shared/Partial/_ViewUser.cshtml", model);
                }
                else
                {
                    return Json("0", JsonRequestBehavior.AllowGet);
                }


            }
            else
            {
                return Json("-1", JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult DeleteUser(string LoginId)
        {
            var model = new Result();
            if (Session["UID"] != null)
            {
                var result = false;
                if (LoginId.ToString() != "0")
                {
                    result = AccountRepository.DeleteUser(Convert.ToInt32(LoginId));

                }
                model.p = model.p == 0 ? 1 : model.p;
                var Total = 0;
                model.ViewUser = AccountRepository.ViewUser(model.p, model.size, out Total);
                model.Total = Total;
                var pager = new Pager(model.Total, model.p);
                model.pager = pager;
                if (model.ViewUser.Count > 0)
                {
                    return PartialView("~/Views/Shared/Partial/_ViewUser.cshtml", model);
                }
                else
                {
                    return Json("0", JsonRequestBehavior.AllowGet);
                }



            }
            else
            {
                return Json("-1", JsonRequestBehavior.AllowGet);
            }

        }
    }
}
