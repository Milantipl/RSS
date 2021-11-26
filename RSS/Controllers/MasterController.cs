using ConnectionLibrary;
using ConnectionLibrary.Model;
using ConnectionLibrary.Model;
using ConnectionLibrary.Repository;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace RSS.Controllers
{
    public class MasterController : Controller
    {
        //
        // GET: /Master/

        public ActionResult Dashboard()
        {
            var model = new Result();
            if (Session["UID"] != null)
            {
                model.UserDetail = AccountRepository.GetuserDetail(Convert.ToInt32(Session["UID"]));
                model.ListMonth = MasterRepository.GetListMonth();
                var firstitemmonth = new _Month();
                firstitemmonth.MonthID = 0;
                firstitemmonth.Month = "--Select Month---";
                model.ListMonth.Insert(0, firstitemmonth);


                var parameters = new List<Tuple<string, string, SqlDbType, int?>>();
                parameters.Clear();

                var MonthId = model.ListMonth.Where(x => x.Month.Trim() == (DateTime.Now.ToString("MMM") + "-" + DateTime.Now.ToString("yy")).Trim()).FirstOrDefault().MonthID;
                if (MonthId != null)
                {
                    model.SearchMonthID = MonthId;
                    parameters.Add(new Tuple<string, string, SqlDbType, int?>("@S_MonthID", MonthId.ToString(), SqlDbType.NVarChar, 500));
                }
                model.DashboardCountList = MasterRepository.GetDashbordCount(parameters);
                model.DashboardBandShakhaList = MasterRepository.GetDashbordBandhShkha(parameters);
                // model.DashboardNivasiKaryakartaList = MasterRepository.GetDashbordNivasiKaryakarta(parameters);
                if (model.UserDetail.Roleid.ToString().Trim() == "2")
                {
                    model.DashboardCountList = model.DashboardCountList.Where(x => x.VibhagID.ToString() == model.UserDetail.RoleWiseDept.ToString().Trim()).ToList();
                    model.DashboardBandShakhaList = model.DashboardBandShakhaList.Where(x => x.VibhagID.ToString() == model.UserDetail.RoleWiseDept.ToString().Trim()).ToList();
                    // model.DashboardNivasiKaryakartaList = model.DashboardNivasiKaryakartaList.Where(x => x.VibhagID.ToString() == model.UserDetail.RoleWiseDept.ToString().Trim()).ToList();


                }
                else if (model.UserDetail.Roleid.ToString().Trim() == "3")
                {
                    model.DashboardCountList = model.DashboardCountList.Where(x => x.BhagID.ToString() == model.UserDetail.RoleWiseDept.ToString().Trim()).ToList();
                    model.DashboardBandShakhaList = model.DashboardBandShakhaList.Where(x => x.BhagID.ToString() == model.UserDetail.RoleWiseDept.ToString().Trim()).ToList();
                    //model.DashboardNivasiKaryakartaList = model.DashboardNivasiKaryakartaList.Where(x => x.VibhagID.ToString() == model.UserDetail.RoleWiseDept.ToString().Trim()).ToList();

                }
            }
            else
            {
                return RedirectToAction("LogOff", "Account");
            }
            return View(model);
        }
        public ActionResult DashboardLazyLoad(int MonthID)
        {
            var model = new Result();
            if (Session["UID"] != null)
            {
                model.UserDetail = AccountRepository.GetuserDetail(Convert.ToInt32(Session["UID"]));

                var parameters = new List<Tuple<string, string, SqlDbType, int?>>();
                parameters.Clear();

                if (MonthID != null)
                {
                    model.SearchMonthID = MonthID;
                    parameters.Add(new Tuple<string, string, SqlDbType, int?>("@S_MonthID", MonthID.ToString(), SqlDbType.NVarChar, 500));
                }
                model.DashboardCountList = MasterRepository.GetDashbordCount(parameters);
                model.DashboardBandShakhaList = MasterRepository.GetDashbordBandhShkha(parameters);
                // model.DashboardNivasiKaryakartaList = MasterRepository.GetDashbordNivasiKaryakarta(parameters);
                if (model.UserDetail.Roleid.ToString().Trim() == "2")
                {
                    model.DashboardCountList = model.DashboardCountList.Where(x => x.VibhagID.ToString() == model.UserDetail.RoleWiseDept.ToString().Trim()).ToList();
                    model.DashboardBandShakhaList = model.DashboardBandShakhaList.Where(x => x.VibhagID.ToString() == model.UserDetail.RoleWiseDept.ToString().Trim()).ToList();
                    model.DashboardNivasiKaryakartaList = model.DashboardNivasiKaryakartaList.Where(x => x.VibhagID.ToString() == model.UserDetail.RoleWiseDept.ToString().Trim()).ToList();


                }
                else if (model.UserDetail.Roleid.ToString().Trim() == "3")
                {
                    model.DashboardCountList = model.DashboardCountList.Where(x => x.BhagID.ToString() == model.UserDetail.RoleWiseDept.ToString().Trim()).ToList();
                    model.DashboardBandShakhaList = model.DashboardBandShakhaList.Where(x => x.BhagID.ToString() == model.UserDetail.RoleWiseDept.ToString().Trim()).ToList();
                    //   model.DashboardNivasiKaryakartaList = model.DashboardNivasiKaryakartaList.Where(x => x.VibhagID.ToString() == model.UserDetail.RoleWiseDept.ToString().Trim()).ToList();

                }
                //return PartialView("~/Views/Shared/Partial/Reports/_DashboardCount.cshtml", model);
                model.DashboardCountString = ConvertViewToString("~/Views/Shared/Partial/_DashboardCount.cshtml", model);
                model.DashboardBandhShakhaString = ConvertViewToString("~/Views/Shared/Partial/_DashboardBandhshakha.cshtml", model);
                //  model.DashboardNivasiKaryakartaString = ConvertViewToString("~/Views/Shared/Partial/_DashboardKaryaVihinVasti.cshtml", model);
                return Json(new { DashboardCountString = model.DashboardCountString, DashboardBandhShakhaString = model.DashboardBandhShakhaString, DashboardNivasiKaryakartaString = model.DashboardNivasiKaryakartaString }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return RedirectToAction("LogOff", "Account");
            }
        }
        public string ConvertViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (StringWriter writer = new StringWriter())
            {
                ViewEngineResult vResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                ViewContext vContext = new ViewContext(this.ControllerContext, vResult.View, ViewData, new TempDataDictionary(), writer);
                vResult.View.Render(vContext, writer);
                return writer.ToString();
            }
        }

        #region Vibhag
        public ActionResult Vibhag()
        {
            var model = new Result();
            if (Session["UID"] != null)
            {
                model.UserDetail = AccountRepository.GetuserDetail(Convert.ToInt32(Session["UID"]));
                model.ListVibhag = MasterRepository.GetListVibhag();
                if (model.UserDetail.Roleid.ToString().Trim() == "2")
                {
                    model.ListVibhag = model.ListVibhag.Where(x => x.VibhagID.ToString() == model.UserDetail.RoleWiseDept.ToString().Trim()).ToList();

                }
                var firstitemVibhag = new _Vibhag();
                firstitemVibhag.VibhagID = 0;
                firstitemVibhag.Vibhag = "--Select Vibhag---";
                model.ListVibhag.Insert(0, firstitemVibhag);

                model.ViewVibhag = MasterRepository.ViewVibhag();
                if (model.UserDetail.Roleid.ToString().Trim() == "2")
                {
                    model.ViewVibhag = model.ViewVibhag.Where(x => x.VibhagID.ToString() == model.UserDetail.RoleWiseDept.ToString().Trim()).ToList();

                }

            }
            else
            {
                return RedirectToAction("LogOff", "Account");
            }
            return View(model);
        }
        public ActionResult VibhagByID(_Vibhag Vibhag)
        {
            var model = new Result();
            if (Session["UID"] != null)
            {

                model.ViewVibhag = MasterRepository.ViewVibhagByID(Vibhag.VibhagID);
                if (model.ViewVibhag.Count > 0)
                {
                    return PartialView("~/Views/Shared/Partial/_ViewVibhag.cshtml", model);
                }
                else
                {
                    return Json("0", JsonRequestBehavior.AllowGet);
                }


            }
            else
            {
                return RedirectToAction("LogOff", "Account");
            }

        }
        #endregion
        #region Bhag
        public ActionResult Bhag()
        {
            var model = new Result();
            if (Session["UID"] != null)
            {
                model.UserDetail = AccountRepository.GetuserDetail(Convert.ToInt32(Session["UID"]));
                model.ListBhag = MasterRepository.GetListBhag();

                if (model.UserDetail.Roleid.ToString().Trim() == "2")
                {
                    model.ListBhag = model.ListBhag.Where(x => x.VibhagID.ToString() == model.UserDetail.RoleWiseDept.ToString().Trim()).ToList();

                }
                else if (model.UserDetail.Roleid.ToString().Trim() == "3")
                {
                    model.ListBhag = model.ListBhag.Where(x => x.BhagID.ToString() == model.UserDetail.RoleWiseDept.ToString().Trim()).ToList();

                }

                var firstitembhag = new _Bhag();
                firstitembhag.BhagID = 0;
                firstitembhag.Bhag = "--Select Bhag---";
                model.ListBhag.Insert(0, firstitembhag);

                model.ViewBhag = MasterRepository.ViewBhag();

                if (model.UserDetail.Roleid.ToString().Trim() == "2")
                {
                    model.ViewBhag = model.ViewBhag.Where(x => x.VibhagID.ToString() == model.UserDetail.RoleWiseDept.ToString().Trim()).ToList();

                }
                else if (model.UserDetail.Roleid.ToString().Trim() == "3")
                {
                    model.ViewBhag = model.ViewBhag.Where(x => x.BhagID.ToString() == model.UserDetail.RoleWiseDept.ToString().Trim()).ToList();

                }


            }
            else
            {
                return RedirectToAction("LogOff", "Account");
            }
            return View(model);
        }
        public ActionResult BhagByID(_Bhag bhag)
        {
            var model = new Result();
            if (Session["UID"] != null)
            {

                model.ViewBhag = MasterRepository.ViewBhagByID(bhag.BhagID);
                if (model.ViewBhag.Count > 0)
                {
                    return PartialView("~/Views/Shared/Partial/_ViewBhag.cshtml", model);
                }
                else
                {
                    return Json("0", JsonRequestBehavior.AllowGet);
                }


            }
            else
            {
                return RedirectToAction("LogOff", "Account");
            }

        }
        #endregion
        #region Nagar
        public ActionResult Nagar()
        {
            var model = new Result();
            if (Session["UID"] != null)
            {
                model.UserDetail = AccountRepository.GetuserDetail(Convert.ToInt32(Session["UID"]));
                model.ListBhag = MasterRepository.GetListBhag();
                if (model.UserDetail.Roleid.ToString().Trim() == "2")
                {
                    model.ListBhag = model.ListBhag.Where(x => x.VibhagID.ToString() == model.UserDetail.RoleWiseDept.ToString().Trim()).ToList();

                }
                else if (model.UserDetail.Roleid.ToString().Trim() == "3")
                {
                    model.ListBhag = model.ListBhag.Where(x => x.BhagID.ToString() == model.UserDetail.RoleWiseDept.ToString().Trim()).ToList();

                }
                var firstitembhag = new _Bhag();
                firstitembhag.BhagID = 0;
                firstitembhag.Bhag = "--Select Bhag---";
                model.ListBhag.Insert(0, firstitembhag);

                model.ViewNagar = MasterRepository.ViewNagar();
                if (model.UserDetail.Roleid.ToString().Trim() == "2")
                {
                    model.ViewNagar = model.ViewNagar.Where(x => x.VibhagID.ToString() == model.UserDetail.RoleWiseDept.ToString().Trim()).ToList();

                }
                else if (model.UserDetail.Roleid.ToString().Trim() == "3")
                {
                    model.ViewNagar = model.ViewNagar.Where(x => x.BhagID.ToString() == model.UserDetail.RoleWiseDept.ToString().Trim()).ToList();

                }


            }
            else
            {
                return RedirectToAction("LogOff", "Account");
            }
            return View(model);
        }
        public ActionResult SearchNagar(_Nagar Nagar)
        {
            var model = new Result();
            if (Session["UID"] != null)
            {
                var WhereClause = "where 1=1 ";
                if (Nagar.NagarID.ToString() != "0")
                {
                    WhereClause += "and Nagar.NagarID=" + Nagar.NagarID;
                }
                if (Nagar.BhagID.ToString() != "0")
                {
                    WhereClause += "and Bhag.BhagID=" + Nagar.BhagID;
                }
                model.ViewNagar = MasterRepository.ViewNagarByID(WhereClause);
                if (model.ViewNagar.Count > 0)
                {
                    return PartialView("~/Views/Shared/Partial/_ViewNagar.cshtml", model);
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
        public ActionResult NagarByBhagID(string BhagID)
        {
            var model = new Result();
            if (Session["UID"] != null)
            {

                model.ListNagar = MasterRepository.GetListNagar(Convert.ToInt32(BhagID));
                var firstitemnagar = new _Nagar();
                firstitemnagar.NagarID = 0;
                firstitemnagar.Nagar = "--Select Nagar---";
                model.ListNagar.Insert(0, firstitemnagar);
                if (model.ListNagar.Count > 0)
                {
                    return PartialView("~/Views/Shared/Partial/_DDLNagar.cshtml", model);
                }

            }
            else
            {
                return Json("-1", JsonRequestBehavior.AllowGet);
            }
            return View(model);
        }
        #endregion

        #region vasti
        public ActionResult Vasti()
        {
            var model = new Result();
            if (Session["UID"] != null)
            {
                model.UserDetail = AccountRepository.GetuserDetail(Convert.ToInt32(Session["UID"]));
                model.ListBhag = MasterRepository.GetListBhag();
                if (model.UserDetail.Roleid.ToString().Trim() == "2")
                {
                    model.ListBhag = model.ListBhag.Where(x => x.VibhagID.ToString() == model.UserDetail.RoleWiseDept.ToString().Trim()).ToList();

                }
                else if (model.UserDetail.Roleid.ToString().Trim() == "3")
                {
                    model.ListBhag = model.ListBhag.Where(x => x.BhagID.ToString() == model.UserDetail.RoleWiseDept.ToString().Trim()).ToList();

                }
                var firstitembhag = new _Bhag();
                firstitembhag.BhagID = 0;
                firstitembhag.Bhag = "--Select Bhag---";
                model.ListBhag.Insert(0, firstitembhag);

                model.ViewVasti = MasterRepository.ViewVasti();
                if (model.UserDetail.Roleid.ToString().Trim() == "2")
                {
                    model.ViewVasti = model.ViewVasti.Where(x => x.VibhagID.ToString() == model.UserDetail.RoleWiseDept.ToString().Trim()).ToList();

                }
                else if (model.UserDetail.Roleid.ToString().Trim() == "3")
                {
                    model.ViewVasti = model.ViewVasti.Where(x => x.BhagID.ToString() == model.UserDetail.RoleWiseDept.ToString().Trim()).ToList();

                }
            }
            else
            {
                return RedirectToAction("LogOff", "Account");
            }
            return View(model);
        }
        public ActionResult SearchVasti(_Nagar Nagar)
        {
            var model = new Result();
            if (Session["UID"] != null)
            {
                var WhereClause = "where 1=1 ";
                if (Nagar.NagarID.ToString() != "0")
                {
                    WhereClause += "and Nagar.NagarID=" + Nagar.NagarID;
                }
                if (Nagar.BhagID.ToString() != "0")
                {
                    WhereClause += "and Bhag.BhagID=" + Nagar.BhagID;
                }
                model.ViewVasti = MasterRepository.ViewVastiByID(WhereClause);
                if (model.ViewVasti.Count > 0)
                {
                    return PartialView("~/Views/Shared/Partial/_ViewVasti.cshtml", model);
                }
                else
                {
                    return Json("0", JsonRequestBehavior.AllowGet);
                }


            }
            else
            {
                return RedirectToAction("LogOff", "Account");
            }
            return View(model);
        }
        #endregion

        #region ShakhaType
        public ActionResult ShakhaType()
        {
            var model = new Result();
            if (Session["UID"] != null)
            {
                model.UserDetail = AccountRepository.GetuserDetail(Convert.ToInt32(Session["UID"]));
                model.ListShakhaType = MasterRepository.GetListShakhaType();

            }
            else
            {
                return RedirectToAction("LogOff", "Account");
            }
            return View(model);

        }
        #endregion


        #region MilanType
        public ActionResult MilanType()
        {
            var model = new Result();
            if (Session["UID"] != null)
            {
                model.UserDetail = AccountRepository.GetuserDetail(Convert.ToInt32(Session["UID"]));
                model.ListMilanType = MasterRepository.GetListMilanType();

            }
            else
            {
                return RedirectToAction("LogOff", "Account");
            }
            return View(model);
        }
        #endregion


        #region SevaKary
        public ActionResult SevaKary()
        {

            var model = new Result();
            if (Session["UID"] != null)
            {
                model.UserDetail = AccountRepository.GetuserDetail(Convert.ToInt32(Session["UID"]));
                model.ListSevaKary = MasterRepository.GetListSevakary();

            }
            else
            {
                return RedirectToAction("LogOff", "Account");
            }
            return View(model);
        }
        #endregion

        #region Shakha
        public ActionResult Shakha()
        {

            var model = new Result();
            if (Session["UID"] != null)
            {
                model.UserDetail = AccountRepository.GetuserDetail(Convert.ToInt32(Session["UID"]));
                model.ListBhag = MasterRepository.GetListBhag();
                if (model.UserDetail.Roleid.ToString().Trim() == "2")
                {
                    model.ListBhag = model.ListBhag.Where(x => x.VibhagID.ToString() == model.UserDetail.RoleWiseDept.ToString().Trim()).ToList();

                }
                else if (model.UserDetail.Roleid.ToString().Trim() == "3")
                {
                    model.ListBhag = model.ListBhag.Where(x => x.BhagID.ToString() == model.UserDetail.RoleWiseDept.ToString().Trim()).ToList();

                }

                var firstitembhag = new _Bhag();
                firstitembhag.BhagID = 0;
                firstitembhag.Bhag = "--Select Bhag---";
                model.ListBhag.Insert(0, firstitembhag);

                model.ListNagar = new List<_Nagar>();
                var firstitemnagar = new _Nagar();
                firstitemnagar.NagarID = 0;
                firstitemnagar.Nagar = "--Select Nagar---";
                model.ListNagar.Insert(0, firstitemnagar);

                model.ListSearchVasti = new List<_Vasti>();
                var firstitemSearchvasti = new _Vasti();
                firstitemSearchvasti.VastiID = 0;
                firstitemSearchvasti.Vasti = "--Select Vasti---";
                model.ListSearchVasti.Insert(0, firstitemSearchvasti);

                model.ListShakha = new List<_Shakha>();
                var firstitemShakha = new _Shakha();
                firstitemShakha.ShakhaID = 0;
                firstitemShakha.ShakhaName = "--Select Shakha---";
                model.ListShakha.Insert(0, firstitemShakha);

                model.ListSevavasti = new List<_Sevavasti>();
                var firstitemSevavasti = new _Sevavasti();
                firstitemSevavasti.SVID = 0;
                firstitemSevavasti.SevaVasti = "--Select Sevavasti---";
                model.ListSevavasti.Insert(0, firstitemSevavasti);

                model.ListShakhaType = MasterRepository.GetListShakhaType();
                var firstitemShkhatype = new _ShakhaType();
                firstitemShkhatype.STID = 0;
                firstitemShkhatype.ShakhaType = "--Select ShakhaType---";
                model.ListShakhaType.Insert(0, firstitemShkhatype);


                model.ListShakhaTime = MasterRepository.GetListShakhaTime();
                var firstitemShkhaTime = new _Shakhatime_Mast();
                firstitemShkhaTime.ID = 0;
                firstitemShkhaTime.Shakhatime = "--Select Shakhatime---";
                model.ListShakhaTime.Insert(0, firstitemShkhaTime);

                var WhereClause = " where 1=1 ";
                if (model.UserDetail.Roleid.ToString().Trim() == "2")
                {
                    WhereClause += " and Bhag.VibhagID=" + model.UserDetail.RoleWiseDept;

                }
                else if (model.UserDetail.Roleid.ToString().Trim() == "3")
                {
                    WhereClause += " and Bhag.BhagID=" + model.UserDetail.RoleWiseDept;

                }
                model.p = model.p == 0 ? 1 : model.p;
                var Total = 0;
                model.ViewShakha = MasterRepository.ViewShakha(WhereClause, model.p, model.size, out Total);

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
        public ActionResult Fill_AddShakhaDetail(string VastiID)
        {
            if (Session["UID"] != null)
            {
                if (VastiID != null)
                {
                    var nagarid = MasterRepository.GetListVasti().Where(m => m.VastiID == Convert.ToInt32(VastiID)).ToList().FirstOrDefault().NagarID;
                    var nagarList = MasterRepository.GetListNagar();
                    var nagar = nagarList.Where(m => m.NagarID == Convert.ToInt32(nagarid)).ToList().FirstOrDefault().Nagar;
                    var BhagID = nagarList.Where(m => m.NagarID == Convert.ToInt32(nagarid)).ToList().FirstOrDefault().BhagID;
                    var bhag = MasterRepository.GetListBhag().Where(m => m.BhagID == Convert.ToInt32(BhagID)).ToList().FirstOrDefault().Bhag;

                    var SevavastiList = MasterRepository.GetListSevavastiByNagar(nagarid);
                    if (SevavastiList != null)
                    {

                        return Json(new { Nagar = nagar, Bhag = bhag, SevavastiList = SevavastiList }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { Nagar = nagar, Bhag = bhag, SevavastiList = "" }, JsonRequestBehavior.AllowGet);
                    }
                }


            }
            else
            {
                return RedirectToAction("LogOff", "Account");
            }
            return View();
        }
        public ActionResult VastByID(string VastiID)
        {
            if (Session["UID"] != null)
            {
                var result = new Result();

                if (VastiID != null)
                {
                    result.ListVasti = MasterRepository.GetListVasti_Nagar().Where(m => m.VastiID == Convert.ToInt32(VastiID)).ToList();
                    result.VastiID = Convert.ToInt32(VastiID);
                    if (result.ListVasti.Count > 0)
                    {
                        return PartialView("~/Views/Shared/Partial/_DDLvasti.cshtml", result);
                    }
                }


            }
            else
            {
                return RedirectToAction("LogOff", "Account");
            }
            return View();
        }
        public ActionResult ClearVastiVast()
        {
            if (Session["UID"] != null)
            {
                var result = new Result();
                result.ListVasti = new List<_Vasti>();
                return PartialView("~/Views/Shared/Partial/_DDLvasti.cshtml", result);

            }
            else
            {
                return RedirectToAction("LogOff", "Account");
            }
            return View();
        }
        [OutputCache(Duration = 300)]
        public JsonResult GetVastiDrpList(string term)
        {
            var model = new Result();
            var VastiList = MasterRepository.GetListVasti_Nagar();
            model.UserDetail = AccountRepository.GetuserDetail(Convert.ToInt32(Session["UID"]));
            if (model.UserDetail.Roleid.ToString().Trim() == "2")
            {
                VastiList = VastiList.Where(x => x.VibhagID.ToString() == model.UserDetail.RoleWiseDept.ToString().Trim()).ToList();

            }
            else if (model.UserDetail.Roleid.ToString().Trim() == "3")
            {
                VastiList = VastiList.Where(x => x.BhagID.ToString() == model.UserDetail.RoleWiseDept.ToString().Trim()).ToList();

            }
            if (string.IsNullOrWhiteSpace(term))
            {
                return base.Json((from vasti in VastiList
                                  select new
                                  {
                                      id = vasti.VastiID,
                                      text = vasti.Vasti
                                  }).ToList(), JsonRequestBehavior.AllowGet);
            }
            return base.Json((from vasti in VastiList
                              where vasti.Vasti.ToLower().Contains(term.ToLower())
                              select new
                              {
                                  id = vasti.VastiID,
                                  text = vasti.Vasti
                              }).ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Fill_SearchShakhaControl(string BhagID)
        {
            if (Session["UID"] != null)
            {
                var nagarids = String.Empty;
                var vastiids = string.Empty;
                var Vasti = new List<_Vasti>();
                var nagar = new List<_Nagar>();
                var shakha = new List<_Shakha>();
                var nagarList = MasterRepository.GetListNagar();

                //var ShakhaList = MasterRepository.GetListShakhaByNagar("1,2,3,4");
                nagar = nagarList.Where(m => m.BhagID == Convert.ToInt32(BhagID)).ToList();


                if (nagar.Count > 0)
                {
                    foreach (var nid in nagar)
                    {
                        if (nagarids == "")
                        {
                            nagarids = nid.NagarID.ToString();
                        }
                        else
                        {
                            nagarids += "," + nid.NagarID.ToString();
                        }


                    }
                    Vasti = MasterRepository.GetListvastiByNagar(nagarids);

                    if (Vasti.Count > 0)
                    {
                        foreach (var vid in Vasti)
                        {
                            if (vastiids == "")
                            {
                                vastiids = vid.VastiID.ToString();
                            }
                            else
                            {
                                vastiids += "," + vid.VastiID.ToString();
                            }

                        }
                        shakha = MasterRepository.GetListShakhaByVasti(vastiids);
                    }
                    return Json(new { Nagar = nagar, Vasti = Vasti, shakha = shakha }, JsonRequestBehavior.AllowGet);
                }

                else
                {
                    return Json("0", JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return RedirectToAction("LogOff", "Account");
            }

        }
        public ActionResult InsertShakha(_Shakha Shakha)
        {
            var model = new Result();
            if (Session["UID"] != null)
            {
                model.UserDetail = AccountRepository.GetuserDetail(Convert.ToInt32(Session["UID"]));
                var result = false;
                if (Shakha.ShakhaID.ToString() != "0")
                {
                    result = MasterRepository.UpdateShakha(Shakha);

                }
                else
                {
                    result = MasterRepository.InsertShakha(Shakha);
                }


                model.p = model.p == 0 ? 1 : model.p;
                var Total = 0;
                var WhereClause = " where 1=1 ";
                if (model.UserDetail.Roleid.ToString().Trim() == "2")
                {
                    WhereClause += " and Bhag.VibhagID=" + model.UserDetail.RoleWiseDept;


                }
                else if (model.UserDetail.Roleid.ToString().Trim() == "3")
                {
                    WhereClause += " and Bhag.BhagID=" + model.UserDetail.RoleWiseDept;

                }

                model.ViewShakha = MasterRepository.ViewShakha(WhereClause, model.p, model.size, out Total);

                model.Total = Total;
                var pager = new Pager(model.Total, model.p);
                model.pager = pager;
                if (model.ViewShakha.Count > 0)
                {
                    return PartialView("~/Views/Shared/Partial/_ViewShakha.cshtml", model);
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
        public ActionResult DeleteShakha(string ShakhaID)
        {
            var model = new Result();
            if (Session["UID"] != null)
            {
                model.UserDetail = AccountRepository.GetuserDetail(Convert.ToInt32(Session["UID"]));
                var result = false;
                if (ShakhaID.ToString() != "0")
                {
                    result = MasterRepository.DeleteShakha(Convert.ToInt32(ShakhaID));

                }
                model.p = model.p == 0 ? 1 : model.p;
                var Total = 0;
                var WhereClause = " where 1=1 ";
                if (model.UserDetail.Roleid.ToString().Trim() == "2")
                {
                    WhereClause += " and Bhag.VibhagID=" + model.UserDetail.RoleWiseDept;


                }
                else if (model.UserDetail.Roleid.ToString().Trim() == "3")
                {
                    WhereClause += " and Bhag.BhagID=" + model.UserDetail.RoleWiseDept;

                }

                model.ViewShakha = MasterRepository.ViewShakha(WhereClause, model.p, model.size, out Total);
                model.Total = Total;
                var pager = new Pager(model.Total, model.p);
                model.pager = pager;
                if (model.ViewShakha.Count > 0)
                {
                    return PartialView("~/Views/Shared/Partial/_ViewShakha.cshtml", model);
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

        public ActionResult SearchShakha(Result model)
        {

            if (Session["UID"] != null)
            {
                var WhereClause = " where 1=1 ";
                model.UserDetail = AccountRepository.GetuserDetail(Convert.ToInt32(Session["UID"]));
                if (model.UserDetail.Roleid.ToString().Trim() == "2")
                {

                    WhereClause += " and Bhag.VibhagID=" + model.UserDetail.RoleWiseDept;


                }
                else if (model.UserDetail.Roleid.ToString().Trim() == "3")
                {
                    WhereClause += " and Bhag.BhagID=" + model.UserDetail.RoleWiseDept;

                }
                if (model.SearchNagarID.ToString() != "0")
                {
                    WhereClause += " and Nagar.NagarID=" + model.SearchNagarID;
                }
                if (model.SearchBhagID.ToString() != "0")
                {
                    WhereClause += " and Bhag.BhagID=" + model.SearchBhagID;
                }
                if (model.SearchVastiID.ToString() != "0")
                {
                    WhereClause += " and Vasti.VastiID=" + model.SearchVastiID;
                }
                if (model.SearchShakhaID.ToString() != "0")
                {
                    WhereClause += " and Shakha.ShakhaID=" + model.SearchShakhaID;
                }
                if (model.SearchSTID.ToString() != "0")
                {
                    WhereClause += " and Shakha.STID=" + model.SearchSTID;
                }
                if (model.SearchPalak.ToString() != "0")
                {
                    WhereClause += " and Shakha.Palak='" + model.SearchPalak + "'";
                }
                if (model.SearchToli.ToString() != "0")
                {
                    WhereClause += " and Shakha.Toli='" + model.SearchToli + "'";
                }
                if (model.SearchSevavasti.ToString() == "Yes")
                {
                    WhereClause += " and Shakha.AssignSVID!=0 ";
                }

                model.p = model.p == 0 ? 1 : model.p;
                var Total = 0;
                model.ViewShakha = MasterRepository.SearchShakha(WhereClause, model.p, model.size, out Total);
                model.Total = Total;
                var pager = new Pager(model.Total, model.p);
                model.pager = pager;
                //if (model.ViewShakha != null)
                //{
                //    if (model.ViewShakha.Count > 0)
                //    {
                return PartialView("~/Views/Shared/Partial/_ViewShakha.cshtml", model);
                //}
                //else
                //{
                //    return Json("0", JsonRequestBehavior.AllowGet);
                //}
                //}
                //else
                //{
                //    return Json("-1", JsonRequestBehavior.AllowGet);
                //}


            }
            else
            {
                return Json("-1", JsonRequestBehavior.AllowGet);
            }

        }

        #endregion

        #region Milan
        public ActionResult Milan()
        {
            var model = new Result();
            if (Session["UID"] != null)
            {
                model.UserDetail = AccountRepository.GetuserDetail(Convert.ToInt32(Session["UID"]));
                model.ListBhag = MasterRepository.GetListBhag();
                if (model.UserDetail.Roleid.ToString().Trim() == "2")
                {
                    model.ListBhag = model.ListBhag.Where(x => x.VibhagID.ToString() == model.UserDetail.RoleWiseDept.ToString().Trim()).ToList();

                }
                else if (model.UserDetail.Roleid.ToString().Trim() == "3")
                {
                    model.ListBhag = model.ListBhag.Where(x => x.BhagID.ToString() == model.UserDetail.RoleWiseDept.ToString().Trim()).ToList();

                }
                var firstitembhag = new _Bhag();
                firstitembhag.BhagID = 0;
                firstitembhag.Bhag = "--Select Bhag---";
                model.ListBhag.Insert(0, firstitembhag);

                model.ListNagar = new List<_Nagar>();
                var firstitemnagar = new _Nagar();
                firstitemnagar.NagarID = 0;
                firstitemnagar.Nagar = "--Select Nagar---";
                model.ListNagar.Insert(0, firstitemnagar);

                model.ListSearchVasti = new List<_Vasti>();
                var firstitemSearchvasti = new _Vasti();
                firstitemSearchvasti.VastiID = 0;
                firstitemSearchvasti.Vasti = "--Select Vasti---";
                model.ListSearchVasti.Insert(0, firstitemSearchvasti);


                model.ListShakhaType = MasterRepository.GetListShakhaType();
                var firstitemShkhatype = new _ShakhaType();
                firstitemShkhatype.STID = 0;
                firstitemShkhatype.ShakhaType = "--Select ShakhaType---";
                model.ListShakhaType.Insert(0, firstitemShkhatype);

                model.ListMilanType = MasterRepository.GetListMilanType();
                var firstitemmilantype = new _MilanType();
                firstitemmilantype.MTID = 0;
                firstitemmilantype.MilanType = "--Select MilanType---";
                model.ListMilanType.Insert(0, firstitemmilantype);

                model.ListMilan = new List<_Milan>();
                var firstitemMilan = new _Milan();
                firstitemMilan.MTID = 0;
                firstitemMilan.MilanName = "--Select Milan---";
                model.ListMilan.Insert(0, firstitemMilan);
                var WhereClause = " where 1=1 ";
                if (model.UserDetail.Roleid.ToString().Trim() == "2")
                {

                    WhereClause += " and Bhag.VibhagID=" + model.UserDetail.RoleWiseDept;


                }
                else if (model.UserDetail.Roleid.ToString().Trim() == "3")
                {
                    WhereClause += " and Bhag.BhagID=" + model.UserDetail.RoleWiseDept;

                }
                //model.ListVasti = MasterRepository.GetListVasti_Nagar();

                model.p = model.p == 0 ? 1 : model.p;
                var Total = 0;
                model.ViewMilan = MasterRepository.ViewMilan(WhereClause, model.p, model.size, out Total);
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
        public ActionResult Fill_AddMilanDetail(string VastiID)
        {
            if (Session["UID"] != null)
            {
                var vastiids = string.Empty;
                if (VastiID != null)
                {
                    var Vasti = new List<_Vasti>();

                    var nagarid = MasterRepository.GetListVasti().Where(m => m.VastiID == Convert.ToInt32(VastiID)).ToList().FirstOrDefault().NagarID;
                    var nagarList = MasterRepository.GetListNagar();
                    var nagar = nagarList.Where(m => m.NagarID == Convert.ToInt32(nagarid)).ToList().FirstOrDefault().Nagar;
                    var BhagID = nagarList.Where(m => m.NagarID == Convert.ToInt32(nagarid)).ToList().FirstOrDefault().BhagID;
                    var bhag = MasterRepository.GetListBhag().Where(m => m.BhagID == Convert.ToInt32(BhagID)).ToList().FirstOrDefault().Bhag;
                    Vasti = MasterRepository.GetListvastiByNagar(nagarid.ToString());

                    if (Vasti.Count > 0)
                    {
                        foreach (var vid in Vasti)
                        {
                            if (vastiids == "")
                            {
                                vastiids = vid.VastiID.ToString();
                            }
                            else
                            {
                                vastiids += "," + vid.VastiID.ToString();
                            }

                        }


                    }



                    return Json(new { Nagar = nagar, Bhag = bhag, NagarID = nagarid }, JsonRequestBehavior.AllowGet);

                }


            }
            else
            {
                return RedirectToAction("LogOff", "Account");
            }
            return View();
        }
        public ActionResult Fill_SearchMilanControl(string BhagID)
        {
            if (Session["UID"] != null)
            {
                var nagarids = String.Empty;
                var vastiids = string.Empty;
                var Vasti = new List<_Vasti>();
                var nagar = new List<_Nagar>();
                var Milan = new List<_Milan>();
                var nagarList = MasterRepository.GetListNagar();

                //var ShakhaList = MasterRepository.GetListShakhaByNagar("1,2,3,4");
                nagar = nagarList.Where(m => m.BhagID == Convert.ToInt32(BhagID)).ToList();


                if (nagar.Count > 0)
                {
                    foreach (var nid in nagar)
                    {
                        if (nagarids == "")
                        {
                            nagarids = nid.NagarID.ToString();
                        }
                        else
                        {
                            nagarids += "," + nid.NagarID.ToString();
                        }


                    }
                    Vasti = MasterRepository.GetListvastiByNagar(nagarids);

                    if (Vasti.Count > 0)
                    {
                        foreach (var vid in Vasti)
                        {
                            if (vastiids == "")
                            {
                                vastiids = vid.VastiID.ToString();
                            }
                            else
                            {
                                vastiids += "," + vid.VastiID.ToString();
                            }

                        }
                        Milan = MasterRepository.GetListMilanByVasti(vastiids);
                    }
                    return Json(new { Nagar = nagar, Vasti = Vasti, Milan = Milan }, JsonRequestBehavior.AllowGet);
                }

                else
                {
                    return Json("0", JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return RedirectToAction("LogOff", "Account");
            }

        }

        public ActionResult InsertMilan(_Milan milan)
        {
            var model = new Result();
            if (Session["UID"] != null)
            {
                model.UserDetail = AccountRepository.GetuserDetail(Convert.ToInt32(Session["UID"]));
                var result = false;
                if (milan.MilanID.ToString() != "0")
                {
                    result = MasterRepository.UpdateMilan(milan);

                }
                else
                {
                    result = MasterRepository.InsertMilan(milan);
                }


                model.p = model.p == 0 ? 1 : model.p;
                var Total = 0;
                var WhereClause = " where 1=1 ";
                if (model.UserDetail.Roleid.ToString().Trim() == "2")
                {

                    WhereClause += " and Bhag.VibhagID=" + model.UserDetail.RoleWiseDept;


                }
                else if (model.UserDetail.Roleid.ToString().Trim() == "3")
                {
                    WhereClause += " and Bhag.BhagID=" + model.UserDetail.RoleWiseDept;

                }
                model.ViewMilan = MasterRepository.ViewMilan(WhereClause, model.p, model.size, out Total);
                model.Total = Total;
                var pager = new Pager(model.Total, model.p);
                model.pager = pager;
                if (model.ViewMilan.Count > 0)
                {
                    return PartialView("~/Views/Shared/Partial/_ViewMilan.cshtml", model);
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

        public ActionResult DeleteMilan(string MilanID)
        {
            var model = new Result();
            if (Session["UID"] != null)
            {
                model.UserDetail = AccountRepository.GetuserDetail(Convert.ToInt32(Session["UID"]));
                var result = false;
                if (MilanID.ToString() != "0")
                {
                    result = MasterRepository.DeleteMilan(Convert.ToInt32(MilanID));

                }
                model.p = model.p == 0 ? 1 : model.p;
                var Total = 0;
                var WhereClause = " where 1=1 ";
                if (model.UserDetail.Roleid.ToString().Trim() == "2")
                {

                    WhereClause += " and Bhag.VibhagID=" + model.UserDetail.RoleWiseDept;


                }
                else if (model.UserDetail.Roleid.ToString().Trim() == "3")
                {
                    WhereClause += " and Bhag.BhagID=" + model.UserDetail.RoleWiseDept;

                }
                model.ViewMilan = MasterRepository.ViewMilan(WhereClause, model.p, model.size, out Total);
                model.Total = Total;
                var pager = new Pager(model.Total, model.p);
                model.pager = pager;
                if (model.ViewMilan.Count > 0)
                {
                    return PartialView("~/Views/Shared/Partial/_ViewMilan.cshtml", model);
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

        public ActionResult SearchMilan(Result model)
        {

            if (Session["UID"] != null)
            {
                var WhereClause = " where 1=1 ";

                model.UserDetail = AccountRepository.GetuserDetail(Convert.ToInt32(Session["UID"]));
                if (model.UserDetail.Roleid.ToString().Trim() == "2")
                {

                    WhereClause += " and Bhag.VibhagID=" + model.UserDetail.RoleWiseDept;


                }
                else if (model.UserDetail.Roleid.ToString().Trim() == "3")
                {
                    WhereClause += " and Bhag.BhagID=" + model.UserDetail.RoleWiseDept;

                }
                if (model.SearchNagarID.ToString() != "0")
                {
                    WhereClause += " and Nagar.NagarID=" + model.SearchNagarID;
                }
                if (model.SearchBhagID.ToString() != "0")
                {
                    WhereClause += " and Bhag.BhagID=" + model.SearchBhagID;
                }
                if (model.SearchVastiID.ToString() != "0")
                {
                    WhereClause += " and Vasti.VastiID=" + model.SearchVastiID;
                }
                if (model.SearchMilanID.ToString() != "0")
                {
                    WhereClause += " and Milan.MilanID=" + model.SearchMilanID;
                }
                if (model.SearchSTID.ToString() != "0")
                {
                    WhereClause += " and Milan.STID=" + model.SearchSTID;
                }
                if (model.SearchMTID.ToString() != "0")
                {
                    WhereClause += " and Milan.MTID=" + model.SearchMTID;
                }


                model.p = model.p == 0 ? 1 : model.p;
                var Total = 0;
                model.ViewMilan = MasterRepository.SearchMilan(WhereClause, model.p, model.size, out Total);
                model.Total = Total;
                var pager = new Pager(model.Total, model.p);
                model.pager = pager;
                //if (model.ViewShakha != null)
                //{
                //    if (model.ViewShakha.Count > 0)
                //    {
                return PartialView("~/Views/Shared/Partial/_ViewMilan.cshtml", model);
                //}
                //else
                //{
                //    return Json("0", JsonRequestBehavior.AllowGet);
                //}
                //}
                //else
                //{
                //    return Json("-1", JsonRequestBehavior.AllowGet);
                //}


            }
            else
            {
                return Json("-1", JsonRequestBehavior.AllowGet);
            }

        }
        #endregion

        #region Sevavasti
        public ActionResult Sevavasti()
        {
            var model = new Result();
            if (Session["UID"] != null)
            {
                model.UserDetail = AccountRepository.GetuserDetail(Convert.ToInt32(Session["UID"]));
                model.ListBhag = MasterRepository.GetListBhag();
                if (model.UserDetail.Roleid.ToString().Trim() == "2")
                {
                    model.ListBhag = model.ListBhag.Where(x => x.VibhagID.ToString() == model.UserDetail.RoleWiseDept.ToString().Trim()).ToList();

                }
                else if (model.UserDetail.Roleid.ToString().Trim() == "3")
                {
                    model.ListBhag = model.ListBhag.Where(x => x.BhagID.ToString() == model.UserDetail.RoleWiseDept.ToString().Trim()).ToList();

                }
                var firstitembhag = new _Bhag();
                firstitembhag.BhagID = 0;
                firstitembhag.Bhag = "--Select Bhag---";
                model.ListBhag.Insert(0, firstitembhag);

                model.ListNagar = new List<_Nagar>();
                var firstitemnagar = new _Nagar();
                firstitemnagar.NagarID = 0;
                firstitemnagar.Nagar = "--Select Nagar---";
                model.ListNagar.Insert(0, firstitemnagar);

                model.ListSearchVasti = new List<_Vasti>();
                var firstitemSearchvasti = new _Vasti();
                firstitemSearchvasti.VastiID = 0;
                firstitemSearchvasti.Vasti = "--Select Vasti---";
                model.ListSearchVasti.Insert(0, firstitemSearchvasti);

                model.ListShakha = new List<_Shakha>();
                var firstitemShakha = new _Shakha();
                firstitemShakha.ShakhaID = 0;
                firstitemShakha.ShakhaName = "--Select Shakha---";
                model.ListShakha.Insert(0, firstitemShakha);

                model.ListSevavasti = new List<_Sevavasti>();
                var firstitemSevavasti = new _Sevavasti();
                firstitemSevavasti.SVID = 0;
                firstitemSevavasti.SevaVasti = "--Select Sevavasti---";
                model.ListSevavasti.Insert(0, firstitemSevavasti);

                model.ListSevaKary = MasterRepository.GetListSevakary();
                var firstitemSevaKary = new _Sevakary();
                firstitemSevaKary.SKID = 0;
                firstitemSevaKary.SevaKary = "--Select SevaKary---";
                model.ListSevaKary.Insert(0, firstitemSevaKary);


                model.ListVasti = MasterRepository.GetListVasti_Nagar();

                model.p = model.p == 0 ? 1 : model.p;
                var Total = 0;
                var WhereClause = " where 1=1 ";
                if (model.UserDetail.Roleid.ToString().Trim() == "2")
                {

                    WhereClause += " and Bhag.VibhagID=" + model.UserDetail.RoleWiseDept;


                }
                else if (model.UserDetail.Roleid.ToString().Trim() == "3")
                {
                    WhereClause += " and Bhag.BhagID=" + model.UserDetail.RoleWiseDept;

                }
                model.ViewSevaVasti = MasterRepository.ViewSevaVasti(WhereClause, model.p, model.size, out Total);
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

        public ActionResult Fill_AddSevavastiDetail(string VastiID)
        {
            if (Session["UID"] != null)
            {
                var vastiids = string.Empty;
                if (VastiID != null)
                {
                    var Vasti = new List<_Vasti>();
                    var ShakhaList = new List<_Shakha>();
                    var nagarid = MasterRepository.GetListVasti().Where(m => m.VastiID == Convert.ToInt32(VastiID)).ToList().FirstOrDefault().NagarID;
                    var nagarList = MasterRepository.GetListNagar();
                    var nagar = nagarList.Where(m => m.NagarID == Convert.ToInt32(nagarid)).ToList().FirstOrDefault().Nagar;
                    var BhagID = nagarList.Where(m => m.NagarID == Convert.ToInt32(nagarid)).ToList().FirstOrDefault().BhagID;
                    var bhag = MasterRepository.GetListBhag().Where(m => m.BhagID == Convert.ToInt32(BhagID)).ToList().FirstOrDefault().Bhag;
                    Vasti = MasterRepository.GetListvastiByNagar(nagarid.ToString());

                    if (Vasti.Count > 0)
                    {
                        foreach (var vid in Vasti)
                        {
                            if (vastiids == "")
                            {
                                vastiids = vid.VastiID.ToString();
                            }
                            else
                            {
                                vastiids += "," + vid.VastiID.ToString();
                            }

                        }

                        ShakhaList = MasterRepository.GetListShakhaByVasti(vastiids);
                    }

                    if (ShakhaList != null)
                    {

                        return Json(new { Nagar = nagar, Bhag = bhag, NagarID = nagarid, ShakhaList = ShakhaList }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { Nagar = nagar, Bhag = bhag, NagarID = nagarid, ShakhaList = "" }, JsonRequestBehavior.AllowGet);
                    }
                }


            }
            else
            {
                return RedirectToAction("LogOff", "Account");
            }
            return View();
        }
        public ActionResult Fill_SearchSevaVastiControl(string BhagID)
        {
            if (Session["UID"] != null)
            {
                var nagarids = String.Empty;
                var vastiids = string.Empty;
                var Vasti = new List<_Vasti>();
                var nagar = new List<_Nagar>();
                var SevaVasti = new List<_Sevavasti>();
                var nagarList = MasterRepository.GetListNagar();

                //var ShakhaList = MasterRepository.GetListShakhaByNagar("1,2,3,4");
                nagar = nagarList.Where(m => m.BhagID == Convert.ToInt32(BhagID)).ToList();


                if (nagar.Count > 0)
                {
                    foreach (var nid in nagar)
                    {
                        if (nagarids == "")
                        {
                            nagarids = nid.NagarID.ToString();
                        }
                        else
                        {
                            nagarids += "," + nid.NagarID.ToString();
                        }


                    }
                    Vasti = MasterRepository.GetListvastiByNagar(nagarids);

                    if (Vasti.Count > 0)
                    {
                        foreach (var vid in Vasti)
                        {
                            if (vastiids == "")
                            {
                                vastiids = vid.VastiID.ToString();
                            }
                            else
                            {
                                vastiids += "," + vid.VastiID.ToString();
                            }

                        }
                        SevaVasti = MasterRepository.GetListSevavastiByVasti(vastiids);
                    }
                    return Json(new { Nagar = nagar, Vasti = Vasti, SevaVasti = SevaVasti }, JsonRequestBehavior.AllowGet);
                }

                else
                {
                    return Json("0", JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return RedirectToAction("LogOff", "Account");
            }

        }
        public ActionResult InsertSevaVasti(Result model)
        {
            // var model = new Result();
            if (Session["UID"] != null)
            {
                model.UserDetail = AccountRepository.GetuserDetail(Convert.ToInt32(Session["UID"]));
                var result = false;
                if (model.SVID.ToString() != "0")
                {
                    result = MasterRepository.UpdateSevavasti(model);

                }
                else
                {
                    result = MasterRepository.InsertSevaVasti(model);
                }


                model.p = model.p == 0 ? 1 : model.p;
                var Total = 0;
                var WhereClause = " where 1=1 ";
                if (model.UserDetail.Roleid.ToString().Trim() == "2")
                {

                    WhereClause += " and Bhag.VibhagID=" + model.UserDetail.RoleWiseDept;


                }
                else if (model.UserDetail.Roleid.ToString().Trim() == "3")
                {
                    WhereClause += " and Bhag.BhagID=" + model.UserDetail.RoleWiseDept;

                }
                model.ViewSevaVasti = MasterRepository.ViewSevaVasti(WhereClause, model.p, model.size, out Total);
                model.Total = Total;
                var pager = new Pager(model.Total, model.p);
                model.pager = pager;
                if (model.ViewSevaVasti.Count > 0)
                {
                    return PartialView("~/Views/Shared/Partial/_ViewSevaVasti.cshtml", model);
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
        public ActionResult DeleteSevavasti(string SVID)
        {
            var model = new Result();
            if (Session["UID"] != null)
            {
                model.UserDetail = AccountRepository.GetuserDetail(Convert.ToInt32(Session["UID"]));
                var result = false;
                if (SVID.ToString() != "0")
                {
                    result = MasterRepository.DeleteSevaVasti(Convert.ToInt32(SVID));

                }
                model.p = model.p == 0 ? 1 : model.p;
                var Total = 0;
                var WhereClause = " where 1=1 ";
                if (model.UserDetail.Roleid.ToString().Trim() == "2")
                {

                    WhereClause += " and Bhag.VibhagID=" + model.UserDetail.RoleWiseDept;


                }
                else if (model.UserDetail.Roleid.ToString().Trim() == "3")
                {
                    WhereClause += " and Bhag.BhagID=" + model.UserDetail.RoleWiseDept;

                }
                model.ViewSevaVasti = MasterRepository.ViewSevaVasti(WhereClause, model.p, model.size, out Total);
                model.Total = Total;
                var pager = new Pager(model.Total, model.p);
                model.pager = pager;
                if (model.ViewSevaVasti.Count > 0)
                {
                    return PartialView("~/Views/Shared/Partial/_ViewSevaVasti.cshtml", model);
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

        public ActionResult SearchSevavasti(Result model)
        {

            if (Session["UID"] != null)
            {
                var WhereClause = " where 1=1 ";
                model.UserDetail = AccountRepository.GetuserDetail(Convert.ToInt32(Session["UID"]));
                if (model.UserDetail.Roleid.ToString().Trim() == "2")
                {

                    WhereClause += " and Bhag.VibhagID=" + model.UserDetail.RoleWiseDept;


                }
                else if (model.UserDetail.Roleid.ToString().Trim() == "3")
                {
                    WhereClause += " and Bhag.BhagID=" + model.UserDetail.RoleWiseDept;

                }
                if (model.SearchNagarID.ToString() != "0")
                {
                    WhereClause += " and Nagar.NagarID=" + model.SearchNagarID;
                }
                if (model.SearchBhagID.ToString() != "0")
                {
                    WhereClause += " and Bhag.BhagID=" + model.SearchBhagID;
                }
                if (model.SearchVastiID.ToString() != "0")
                {
                    WhereClause += " and Vasti.VastiID=" + model.SearchVastiID;
                }
                if (model.SearchSVID.ToString() != "0")
                {
                    WhereClause += " and Sevavasti.SVID=" + model.SearchSVID;
                }

                if (model.SearchSevakary.ToString() == "Yes")
                {
                    WhereClause += " and Sevavasti.SKID!=0 ";
                }
                if (model.SearchShakha.ToString() == "Yes")
                {
                    WhereClause += " and Sevavasti.ShakhaID!=0 ";
                }
                model.p = model.p == 0 ? 1 : model.p;
                var Total = 0;
                model.ViewSevaVasti = MasterRepository.SearchSevaVasti(WhereClause, model.p, model.size, out Total);
                model.Total = Total;
                var pager = new Pager(model.Total, model.p);
                model.pager = pager;
                //if (model.ViewShakha != null)
                //{
                //    if (model.ViewShakha.Count > 0)
                //    {
                return PartialView("~/Views/Shared/Partial/_ViewSevaVasti.cshtml", model);
                //}
                //else
                //{
                //    return Json("0", JsonRequestBehavior.AllowGet);
                //}
                //}
                //else
                //{
                //    return Json("-1", JsonRequestBehavior.AllowGet);
                //}


            }
            else
            {
                return Json("-1", JsonRequestBehavior.AllowGet);
            }

        }


        #endregion

        #region MotoBhag

        SqlConnection DB = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        OleDbConnection Econ;
        public ActionResult MSevavasti(HttpPostedFileBase file, string btn)
        {

            var model = new Result();
            if (Session["UID"] != null)
            {
                model.UserDetail = AccountRepository.GetuserDetail(Convert.ToInt32(Session["UID"]));
                model.ListBhag = MasterRepository.GetListBhag();
                model.ListYadis = MasterRepositoryNew.GetListYadi();
                model.WrongYadis = MasterRepositoryNew.GetListWrongYadi();
                // ExportExcel();

                if (file != null)
                {
                    string filename = Guid.NewGuid() + Path.GetExtension(file.FileName);
                    string filepath = "/ExcelData/" + filename;
                    file.SaveAs(Path.Combine(Server.MapPath("/ExcelData"), filename));
                    InsertExceldata(filepath, filename);
                }
                else
                {
                    ViewBag.Message = "File not Upload";
                }

                if (btn != null)
                {

                }

              

            }
            else
            {
                return RedirectToAction("LogOff", "Account");
            }
            return View(model);
        }

        private void ExcelConn(string filepath)
        {
            string constr = string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 12.0 Xml;HDR=YES;""", filepath);
            Econ = new OleDbConnection(constr);
        }

        private void InsertExceldata(string fileepath, string filename)
        {


            int insertedRecordCount = 0, NotInsertedCount = 0;

            string fullpath = Server.MapPath("/ExcelData/") + filename;
            ExcelConn(fullpath);
            string query = string.Format("Select * from [{0}]", "Sheet1$");
            OleDbCommand Ecom = new OleDbCommand(query, Econ);
            Econ.Open();

            DataSet ds = new DataSet();
            OleDbDataAdapter oda = new OleDbDataAdapter(query, Econ);
            Econ.Close();
            oda.Fill(ds);

            DataTable dt = ds.Tables[0];
            SqlBulkCopy objbulk = new SqlBulkCopy(DB);
            objbulk.DestinationTableName = "Yadi";
            objbulk.ColumnMappings.Add("Name", "Name");
            objbulk.ColumnMappings.Add("FatherName", "FatherName");
            objbulk.ColumnMappings.Add("Surname", "Surname");
            objbulk.ColumnMappings.Add("BhagID", "BhagID");
            objbulk.ColumnMappings.Add("NagarID", "NagarID");
            objbulk.ColumnMappings.Add("Mobile", "Mobile");
            objbulk.ColumnMappings.Add("Mail", "Mail");
            objbulk.ColumnMappings.Add("Dob", "Dob");
            objbulk.ColumnMappings.Add("Blood", "Blood");
            objbulk.ColumnMappings.Add("NvastiID", "NvastiID");
            objbulk.ColumnMappings.Add("MilanType", "MilanType");
            objbulk.ColumnMappings.Add("ShakhaName", "ShakhaName");
            objbulk.ColumnMappings.Add("JobType", "JobType");
            objbulk.ColumnMappings.Add("Business", "Business");
            objbulk.ColumnMappings.Add("Study", "Study");
            objbulk.ColumnMappings.Add("SanghSikshan", "SanghSikshan");
            objbulk.ColumnMappings.Add("PresentD", "PresentD");
            objbulk.ColumnMappings.Add("DSelect", "DSelect");
            objbulk.ColumnMappings.Add("Gatividhi", "Gatividhi");
            objbulk.ColumnMappings.Add("Padadhikari", "Padadhikari");
            objbulk.ColumnMappings.Add("Uniform", "Uniform");
            objbulk.ColumnMappings.Add("Vadhya", "Vadhya");
            objbulk.ColumnMappings.Add("SanghPravesh", "SanghPravesh");
            objbulk.ColumnMappings.Add("Abhiruchi", "Abhiruchi");
            objbulk.ColumnMappings.Add("SelectOrganization", "SelectOrganization");

            DataTable dtNotMatchRecord = new DataTable();
            DataRow rr = dtNotMatchRecord.NewRow();

            StringBuilder sb = new StringBuilder();
            var context = new _WrongYadi();

            _Yadi model = new _Yadi();

            _WrongYadi _Wrong = new _WrongYadi();

            //    dtNotMatchRecord = dt;
            String _bhagID, _WrongbhagID, _bhagIDWrong, _NagarID, _VastiID, _ShakhaID, _mobile,  _MilanType = "0";
            int i = 1;
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows) // search whole table
                {
                    _bhagID = MasterRepositoryNew.GetBhagIdByName(dr["BhagID"].ToString()).ToString();
                    if (_bhagID == "0")

                    {

                        var datar = dr.ItemArray;
                        DB.Open();
                        string wquery = "INSERT INTO YadiWrong ([Name],[FatherName],[Surname],[BhagID],[NagarID],[Mobile],[Mail],[Dob],[Blood],[NvastiID],[MilanType],[ShakhaName],[JobType],[Business],[Study],[SanghSikshan],[PresentD],[DSelect],[Gatividhi],[Padadhikari],[Uniform],[Vadhya],[SanghPravesh],[Abhiruchi],[SelectOrganization]) VALUES (@Name,@FatherName,@Surname,@BhagID,@NagarID,@Mobile,@Mail,@Dob,@Blood,@NvastiID,@MilanType,@ShakhaName,@JobType,@Business,@Study,@SanghSikshan,@PresentD,@DSelect,@Gatividhi,@Padadhikari,@Uniform,@Vadhya,@SanghPravesh,@Abhiruchi,@SelectOrganization)";
                        SqlCommand sqcmd = new SqlCommand(wquery, DB);
                        sqcmd.Parameters.AddWithValue("@Name", datar[0]);
                        sqcmd.Parameters.AddWithValue("@FatherName", datar[1]);
                        sqcmd.Parameters.AddWithValue("@Surname", datar[2]);
                        sqcmd.Parameters.AddWithValue("@BhagID", datar[3]);
                        sqcmd.Parameters.AddWithValue("@NagarID", datar[4]);
                        sqcmd.Parameters.AddWithValue("@Mobile", datar[5]);
                        sqcmd.Parameters.AddWithValue("@Mail", datar[6]);
                        sqcmd.Parameters.AddWithValue("@Dob", datar[7]);
                        sqcmd.Parameters.AddWithValue("@Blood", datar[8]);
                        sqcmd.Parameters.AddWithValue("@NvastiID", datar[9]);
                        sqcmd.Parameters.AddWithValue("@MilanType", datar[10]);
                        sqcmd.Parameters.AddWithValue("@ShakhaName", datar[11]);
                        sqcmd.Parameters.AddWithValue("@JobType", datar[12]);
                        sqcmd.Parameters.AddWithValue("@Business", datar[13]);
                        sqcmd.Parameters.AddWithValue("@Study", datar[14]);
                        sqcmd.Parameters.AddWithValue("@SanghSikshan", datar[15]);
                        sqcmd.Parameters.AddWithValue("@PresentD", datar[16]);
                        sqcmd.Parameters.AddWithValue("@DSelect", datar[17]);
                        sqcmd.Parameters.AddWithValue("@Gatividhi", datar[18]);
                        sqcmd.Parameters.AddWithValue("@Padadhikari", datar[19]);
                        sqcmd.Parameters.AddWithValue("@Uniform", datar[20]);
                        sqcmd.Parameters.AddWithValue("@Vadhya", datar[21]);
                        sqcmd.Parameters.AddWithValue("@SanghPravesh", datar[22]);
                        sqcmd.Parameters.AddWithValue("@Abhiruchi", datar[23]);
                        sqcmd.Parameters.AddWithValue("@SelectOrganization", datar[24]);
                        sqcmd.ExecuteNonQuery().ToString();
                        DB.Close();

                        dr.Delete();
                        goto start;
                    }

                    else
                    {
                        dr["BhagID"] = _bhagID;
                    }

                    _NagarID = MasterRepositoryNew.GetNagarByName(dr["NagarID"].ToString()).ToString();
                    if (_NagarID == "0")
                    {

                        var datar = dr.ItemArray;
                        DB.Open();
                        string wquery = "INSERT INTO YadiWrong ([Name],[FatherName],[Surname],[BhagID],[NagarID],[Mobile],[Mail],[Dob],[Blood],[NvastiID],[MilanType],[ShakhaName],[JobType],[Business],[Study],[SanghSikshan],[PresentD],[DSelect],[Gatividhi],[Padadhikari],[Uniform],[Vadhya],[SanghPravesh],[Abhiruchi],[SelectOrganization]) VALUES (@Name,@FatherName,@Surname,@BhagID,@NagarID,@Mobile,@Mail,@Dob,@Blood,@NvastiID,@MilanType,@ShakhaName,@JobType,@Business,@Study,@SanghSikshan,@PresentD,@DSelect,@Gatividhi,@Padadhikari,@Uniform,@Vadhya,@SanghPravesh,@Abhiruchi,@SelectOrganization)";
                        SqlCommand sqcmd = new SqlCommand(wquery, DB);
                        sqcmd.Parameters.AddWithValue("@Name", datar[0]);
                        sqcmd.Parameters.AddWithValue("@FatherName", datar[1]);
                        sqcmd.Parameters.AddWithValue("@Surname", datar[2]);
                        sqcmd.Parameters.AddWithValue("@BhagID", datar[3]);
                        sqcmd.Parameters.AddWithValue("@NagarID", datar[4]);
                        sqcmd.Parameters.AddWithValue("@Mobile", datar[5]);
                        sqcmd.Parameters.AddWithValue("@Mail", datar[6]);
                        sqcmd.Parameters.AddWithValue("@Dob", datar[7]);
                        sqcmd.Parameters.AddWithValue("@Blood", datar[8]);
                        sqcmd.Parameters.AddWithValue("@NvastiID", datar[9]);
                        sqcmd.Parameters.AddWithValue("@MilanType", datar[10]);
                        sqcmd.Parameters.AddWithValue("@ShakhaName", datar[11]);
                        sqcmd.Parameters.AddWithValue("@JobType", datar[12]);
                        sqcmd.Parameters.AddWithValue("@Business", datar[13]);
                        sqcmd.Parameters.AddWithValue("@Study", datar[14]);
                        sqcmd.Parameters.AddWithValue("@SanghSikshan", datar[15]);
                        sqcmd.Parameters.AddWithValue("@PresentD", datar[16]);
                        sqcmd.Parameters.AddWithValue("@DSelect", datar[17]);
                        sqcmd.Parameters.AddWithValue("@Gatividhi", datar[18]);
                        sqcmd.Parameters.AddWithValue("@Padadhikari", datar[19]);
                        sqcmd.Parameters.AddWithValue("@Uniform", datar[20]);
                        sqcmd.Parameters.AddWithValue("@Vadhya", datar[21]);
                        sqcmd.Parameters.AddWithValue("@SanghPravesh", datar[22]);
                        sqcmd.Parameters.AddWithValue("@Abhiruchi", datar[23]);
                        sqcmd.Parameters.AddWithValue("@SelectOrganization", datar[24]);
                        sqcmd.ExecuteNonQuery().ToString();
                        DB.Close();


                        dr.Delete();

                        goto start;
                    }
                    else
                    {
                        dr["NagarID"] = _NagarID;

                    }

                    _VastiID = MasterRepositoryNew.GetNivasivastiByName(dr["NvastiID"].ToString()).ToString();
                    if (_VastiID == "0")
                    {

                        var datar = dr.ItemArray;
                        DB.Open();
                        string wquery = "INSERT INTO YadiWrong ([Name],[FatherName],[Surname],[BhagID],[NagarID],[Mobile],[Mail],[Dob],[Blood],[NvastiID],[MilanType],[ShakhaName],[JobType],[Business],[Study],[SanghSikshan],[PresentD],[DSelect],[Gatividhi],[Padadhikari],[Uniform],[Vadhya],[SanghPravesh],[Abhiruchi],[SelectOrganization]) VALUES (@Name,@FatherName,@Surname,@BhagID,@NagarID,@Mobile,@Mail,@Dob,@Blood,@NvastiID,@MilanType,@ShakhaName,@JobType,@Business,@Study,@SanghSikshan,@PresentD,@DSelect,@Gatividhi,@Padadhikari,@Uniform,@Vadhya,@SanghPravesh,@Abhiruchi,@SelectOrganization)";
                        SqlCommand sqcmd = new SqlCommand(wquery, DB);
                        sqcmd.Parameters.AddWithValue("@Name", datar[0]);
                        sqcmd.Parameters.AddWithValue("@FatherName", datar[1]);
                        sqcmd.Parameters.AddWithValue("@Surname", datar[2]);
                        sqcmd.Parameters.AddWithValue("@BhagID", datar[3]);
                        sqcmd.Parameters.AddWithValue("@NagarID", datar[4]);
                        sqcmd.Parameters.AddWithValue("@Mobile", datar[5]);
                        sqcmd.Parameters.AddWithValue("@Mail", datar[6]);
                        sqcmd.Parameters.AddWithValue("@Dob", datar[7]);
                        sqcmd.Parameters.AddWithValue("@Blood", datar[8]);
                        sqcmd.Parameters.AddWithValue("@NvastiID", datar[9]);
                        sqcmd.Parameters.AddWithValue("@MilanType", datar[10]);
                        sqcmd.Parameters.AddWithValue("@ShakhaName", datar[11]);
                        sqcmd.Parameters.AddWithValue("@JobType", datar[12]);
                        sqcmd.Parameters.AddWithValue("@Business", datar[13]);
                        sqcmd.Parameters.AddWithValue("@Study", datar[14]);
                        sqcmd.Parameters.AddWithValue("@SanghSikshan", datar[15]);
                        sqcmd.Parameters.AddWithValue("@PresentD", datar[16]);
                        sqcmd.Parameters.AddWithValue("@DSelect", datar[17]);
                        sqcmd.Parameters.AddWithValue("@Gatividhi", datar[18]);
                        sqcmd.Parameters.AddWithValue("@Padadhikari", datar[19]);
                        sqcmd.Parameters.AddWithValue("@Uniform", datar[20]);
                        sqcmd.Parameters.AddWithValue("@Vadhya", datar[21]);
                        sqcmd.Parameters.AddWithValue("@SanghPravesh", datar[22]);
                        sqcmd.Parameters.AddWithValue("@Abhiruchi", datar[23]);
                        sqcmd.Parameters.AddWithValue("@SelectOrganization", datar[24]);
                        sqcmd.ExecuteNonQuery().ToString();
                        DB.Close();

                        dr.Delete();
                        goto start;
                    }
                    else
                    {

                        dr["NvastiID"] = _VastiID;

                    }


                    _ShakhaID = MasterRepositoryNew.GetShakhaByName(dr["ShakhaName"].ToString()).ToString();
                    if (_ShakhaID == "0")
                    {

                        var datar = dr.ItemArray;
                        DB.Open();
                        string wquery = "INSERT INTO YadiWrong ([Name],[FatherName],[Surname],[BhagID],[NagarID],[Mobile],[Mail],[Dob],[Blood],[NvastiID],[MilanType],[ShakhaName],[JobType],[Business],[Study],[SanghSikshan],[PresentD],[DSelect],[Gatividhi],[Padadhikari],[Uniform],[Vadhya],[SanghPravesh],[Abhiruchi],[SelectOrganization]) VALUES (@Name,@FatherName,@Surname,@BhagID,@NagarID,@Mobile,@Mail,@Dob,@Blood,@NvastiID,@MilanType,@ShakhaName,@JobType,@Business,@Study,@SanghSikshan,@PresentD,@DSelect,@Gatividhi,@Padadhikari,@Uniform,@Vadhya,@SanghPravesh,@Abhiruchi,@SelectOrganization)";
                        SqlCommand sqcmd = new SqlCommand(wquery, DB);
                        sqcmd.Parameters.AddWithValue("@Name", datar[0]);
                        sqcmd.Parameters.AddWithValue("@FatherName", datar[1]);
                        sqcmd.Parameters.AddWithValue("@Surname", datar[2]);
                        sqcmd.Parameters.AddWithValue("@BhagID", datar[3]);
                        sqcmd.Parameters.AddWithValue("@NagarID", datar[4]);
                        sqcmd.Parameters.AddWithValue("@Mobile", datar[5]);
                        sqcmd.Parameters.AddWithValue("@Mail", datar[6]);
                        sqcmd.Parameters.AddWithValue("@Dob", datar[7]);
                        sqcmd.Parameters.AddWithValue("@Blood", datar[8]);
                        sqcmd.Parameters.AddWithValue("@NvastiID", datar[9]);
                        sqcmd.Parameters.AddWithValue("@MilanType", datar[10]);
                        sqcmd.Parameters.AddWithValue("@ShakhaName", datar[11]);
                        sqcmd.Parameters.AddWithValue("@JobType", datar[12]);
                        sqcmd.Parameters.AddWithValue("@Business", datar[13]);
                        sqcmd.Parameters.AddWithValue("@Study", datar[14]);
                        sqcmd.Parameters.AddWithValue("@SanghSikshan", datar[15]);
                        sqcmd.Parameters.AddWithValue("@PresentD", datar[16]);
                        sqcmd.Parameters.AddWithValue("@DSelect", datar[17]);
                        sqcmd.Parameters.AddWithValue("@Gatividhi", datar[18]);
                        sqcmd.Parameters.AddWithValue("@Padadhikari", datar[19]);
                        sqcmd.Parameters.AddWithValue("@Uniform", datar[20]);
                        sqcmd.Parameters.AddWithValue("@Vadhya", datar[21]);
                        sqcmd.Parameters.AddWithValue("@SanghPravesh", datar[22]);
                        sqcmd.Parameters.AddWithValue("@Abhiruchi", datar[23]);
                        sqcmd.Parameters.AddWithValue("@SelectOrganization", datar[24]);
                        sqcmd.ExecuteNonQuery().ToString();
                        DB.Close();
                        
                        dr.Delete();
                        goto start;
                    }
                    else
                    {
                        dr["ShakhaName"] = _ShakhaID;

                    }

                    _mobile = MasterRepositoryNew.GetMobile(dr["Mobile"].ToString()).ToString();
                    if (_mobile == "0")
                    {
                        dr.Delete();
                        goto start;
                    }
                    else
                    {
                        dr["Mobile"] = _mobile;

                    }


                    _MilanType = MasterRepositoryNew.GetMTypeByName(dr["MilanType"].ToString()).ToString();
                    if (_MilanType == "0" && _MilanType == null)
                    {

                        var datar = dr.ItemArray;
                        DB.Open();
                        string wquery = "INSERT INTO YadiWrong ([Name],[FatherName],[Surname],[BhagID],[NagarID],[Mobile],[Mail],[Dob],[Blood],[NvastiID],[MilanType],[ShakhaName],[JobType],[Business],[Study],[SanghSikshan],[PresentD],[DSelect],[Gatividhi],[Padadhikari],[Uniform],[Vadhya],[SanghPravesh],[Abhiruchi],[SelectOrganization]) VALUES (@Name,@FatherName,@Surname,@BhagID,@NagarID,@Mobile,@Mail,@Dob,@Blood,@NvastiID,@MilanType,@ShakhaName,@JobType,@Business,@Study,@SanghSikshan,@PresentD,@DSelect,@Gatividhi,@Padadhikari,@Uniform,@Vadhya,@SanghPravesh,@Abhiruchi,@SelectOrganization)";
                        SqlCommand sqcmd = new SqlCommand(wquery, DB);
                        sqcmd.Parameters.AddWithValue("@Name", datar[0]);
                        sqcmd.Parameters.AddWithValue("@FatherName", datar[1]);
                        sqcmd.Parameters.AddWithValue("@Surname", datar[2]);
                        sqcmd.Parameters.AddWithValue("@BhagID", datar[3]);
                        sqcmd.Parameters.AddWithValue("@NagarID", datar[4]);
                        sqcmd.Parameters.AddWithValue("@Mobile", datar[5]);
                        sqcmd.Parameters.AddWithValue("@Mail", datar[6]);
                        sqcmd.Parameters.AddWithValue("@Dob", datar[7]);
                        sqcmd.Parameters.AddWithValue("@Blood", datar[8]);
                        sqcmd.Parameters.AddWithValue("@NvastiID", datar[9]);
                        sqcmd.Parameters.AddWithValue("@MilanType", datar[10]);
                        sqcmd.Parameters.AddWithValue("@ShakhaName", datar[11]);
                        sqcmd.Parameters.AddWithValue("@JobType", datar[12]);
                        sqcmd.Parameters.AddWithValue("@Business", datar[13]);
                        sqcmd.Parameters.AddWithValue("@Study", datar[14]);
                        sqcmd.Parameters.AddWithValue("@SanghSikshan", datar[15]);
                        sqcmd.Parameters.AddWithValue("@PresentD", datar[16]);
                        sqcmd.Parameters.AddWithValue("@DSelect", datar[17]);
                        sqcmd.Parameters.AddWithValue("@Gatividhi", datar[18]);
                        sqcmd.Parameters.AddWithValue("@Padadhikari", datar[19]);
                        sqcmd.Parameters.AddWithValue("@Uniform", datar[20]);
                        sqcmd.Parameters.AddWithValue("@Vadhya", datar[21]);
                        sqcmd.Parameters.AddWithValue("@SanghPravesh", datar[22]);
                        sqcmd.Parameters.AddWithValue("@Abhiruchi", datar[23]);
                        sqcmd.Parameters.AddWithValue("@SelectOrganization", datar[24]);
                        sqcmd.ExecuteNonQuery().ToString();
                        DB.Close();

                        dr.Delete();
                        goto start;
                    }
                    else
                    {
                        dr["MilanType"] = _MilanType;

                    }

                start:
                    string s = "";
                    i++;
                }
                dt.AcceptChanges();
                dtNotMatchRecord.AcceptChanges();
            }


            DB.Open();
            objbulk.WriteToServer(dt);
            DB.Close();

            //  DB.Open();
            //_objbulk.WriteToServer(dtNotMatchRecord);
            //  DB.Close();
        }

        public ActionResult ExportExcel()
        {

            var result = new List<_WrongYadiExcel>();
            var cn = new ConnectionClass();
            string query = "SELECT Name,FatherName,Surname,BhagID,NagarID,Mobile,Mail,Dob,Blood,NvastiID,MilanType,ShakhaName,JobType,Business,Study,SanghSikshan,PresentD,DSelect,Gatividhi,Padadhikari,Uniform,Vadhya,SanghPravesh,Abhiruchi,SelectOrganization FROM YadiWrong";
            var Result = cn.Select(query);

            result = Result.ToListof<_WrongYadiExcel>();

            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Sheet1");
            workSheet.Cells[1, 1].LoadFromCollection(result, true);
            using (var memoryStream = new MemoryStream())
            {
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;  filename=WrongYadi.xlsx");
                excel.SaveAs(memoryStream);
                memoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }

            return RedirectToAction("MSevavasti");
        }
        


        #endregion

        #region VrutNagar
        [OutputCache(Duration = 300)]
        public JsonResult GetNagarDrpList(string term)
        {
            var model = new Result();
            var NagarList = MasterRepository.GetListNagarWithBhag();
            model.UserDetail = AccountRepository.GetuserDetail(Convert.ToInt32(Session["UID"]));
            if (model.UserDetail.Roleid.ToString().Trim() == "2")
            {
                NagarList = NagarList.Where(x => x.VibhagID.ToString() == model.UserDetail.RoleWiseDept.ToString().Trim()).ToList();

            }
            else if (model.UserDetail.Roleid.ToString().Trim() == "3")
            {
                NagarList = NagarList.Where(x => x.BhagID.ToString() == model.UserDetail.RoleWiseDept.ToString().Trim()).ToList();

            }
            if (string.IsNullOrWhiteSpace(term))
            {
                return base.Json((from Nagar in NagarList
                                  select new
                                  {
                                      id = Nagar.NagarID,
                                      text = Nagar.Nagar
                                  }).ToList(), JsonRequestBehavior.AllowGet);
            }
            return base.Json((from Nagar in NagarList
                              where Nagar.Nagar.ToLower().Contains(term.ToLower())
                              select new
                              {
                                  id = Nagar.NagarID,
                                  text = Nagar.Nagar
                              }).ToList(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult VrutNagar()
        {
            Result model = new Result();
            if (Session["UID"] != null)
            {
                model.UserDetail = AccountRepository.GetuserDetail(Convert.ToInt32(Session["UID"]));
                model.ListMonth = MasterRepository.GetListMonth();
                var firstitemmonth = new _Month();
                firstitemmonth.MonthID = 0;
                firstitemmonth.Month = "--Select Month---";
                model.ListMonth.Insert(0, firstitemmonth);
            }
            else
            {
                return RedirectToAction("LogOff", "Account");
            }
            //model.ViewMilanUPVrut = MasterRepository.GetViewMilan_NagarVrut();
            // model.ViewShakhaUPVrut = MasterRepository.GetViewShakha_NagarVrut();
            // model.ViewNagarVrut = MasterRepository.GetViewNagarVrut();
            return View(model);
        }
        public ActionResult SearchNagarVrut(Result model)
        {
            if (Session["UID"] != null)
            {
                model.ViewNagarVrut = MasterRepository.GetViewNagarVrut(model.SearchNagarID, model.SearchMonthID);
                if (model.ViewNagarVrut != null)
                {
                    model.Total = model.ViewNagarVrut.Count;
                }
                else
                {
                    model.Total = 0;
                }
                return PartialView("~/Views/Shared/Partial/_ViewNagarVrut.cshtml", model);
            }
            else
            {
                return Json("-1", JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult SearchShakhaVrut(string NagarID)
        {
            if (Session["UID"] != null)
            {
                Result model = new Result();
                if (NagarID != null)
                {
                    var Vasti = new List<_Vasti>();
                    string vastiids = string.Empty;
                    Vasti = MasterRepository.GetListvastiByNagar(NagarID);
                    if (Vasti != null)
                    {
                        if (Vasti.Count > 0)
                        {
                            foreach (var vid in Vasti)
                            {
                                if (vastiids == "")
                                {
                                    vastiids = vid.VastiID.ToString();
                                }
                                else
                                {
                                    vastiids += "," + vid.VastiID.ToString();
                                }

                            }
                        }
                        model.ViewShakhaUPVrut = MasterRepository.GetViewShakha_NagarVrut(vastiids);
                    }

                }
                if (model.ViewShakhaUPVrut != null)
                {
                    model.Total = model.ViewShakhaUPVrut.Count;
                }
                else
                {
                    model.Total = 0;
                }
                return PartialView("~/Views/Shared/Partial/_ViewShakhaVrut.cshtml", model);

            }
            else
            {
                return Json("-1", JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult SearchShakhaVrutByMonth(string NagarID, int MonthID)
        {
            if (Session["UID"] != null)
            {
                Result model = new Result();
                if (NagarID != null)
                {
                    var Vasti = new List<_Vasti>();
                    string vastiids = string.Empty;
                    Vasti = MasterRepository.GetListvastiByNagar(NagarID);
                    if (Vasti != null)
                    {
                        if (Vasti.Count > 0)
                        {
                            foreach (var vid in Vasti)
                            {
                                if (vastiids == "")
                                {
                                    vastiids = vid.VastiID.ToString();
                                }
                                else
                                {
                                    vastiids += "," + vid.VastiID.ToString();
                                }

                            }
                        }
                        model.ViewShakhaUPVrut = MasterRepository.GetViewShakha_NagarVrutByMonth(vastiids, MonthID);
                    }

                }
                if (model.ViewShakhaUPVrut != null)
                {
                    model.Total = model.ViewShakhaUPVrut.Count;
                }
                else
                {
                    model.Total = 0;
                }
                return PartialView("~/Views/Shared/Partial/_ViewShakhaVrut.cshtml", model);

            }
            else
            {
                return RedirectToAction("LogOff", "Account");
            }
            return View();
        }

        public ActionResult SearchMilanVrut(string NagarID)
        {
            if (Session["UID"] != null)
            {
                Result model = new Result();
                if (NagarID != null)
                {
                    var Vasti = new List<_Vasti>();
                    string vastiids = string.Empty;
                    Vasti = MasterRepository.GetListvastiByNagar(NagarID);
                    if (Vasti != null)
                    {
                        if (Vasti.Count > 0)
                        {
                            foreach (var vid in Vasti)
                            {
                                if (vastiids == "")
                                {
                                    vastiids = vid.VastiID.ToString();
                                }
                                else
                                {
                                    vastiids += "," + vid.VastiID.ToString();
                                }

                            }
                        }
                        model.ViewMilanUPVrut = MasterRepository.GetViewMilan_NagarVrut(vastiids);
                    }

                }
                if (model.ViewMilanUPVrut != null)
                {
                    model.Total = model.ViewMilanUPVrut.Count;
                }
                else
                {
                    model.Total = 0;
                }
                return PartialView("~/Views/Shared/Partial/_ViewMilanVrut.cshtml", model);

            }
            else
            {
                return Json("-1", JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult SearchBethak(string NagarID)
        {
            if (Session["UID"] != null)
            {
                Result model = new Result();
                if (NagarID != null)
                {
                    var Vasti = new List<_Vasti>();
                    string vastiids = string.Empty;
                    Vasti = MasterRepository.GetListvastiByNagar(NagarID);
                    if (Vasti != null)
                    {
                        if (Vasti.Count > 0)
                        {
                            foreach (var vid in Vasti)
                            {
                                if (vastiids == "")
                                {
                                    vastiids = vid.VastiID.ToString();
                                }
                                else
                                {
                                    vastiids += "," + vid.VastiID.ToString();
                                }

                            }
                        }
                        model.ViewBethakvasti = MasterRepository.GetViewBathakVasti(vastiids);
                    }


                }
                if (model.ViewBethakvasti != null)
                {
                    model.BTotal = model.ViewBethakvasti.Count;
                }
                else
                {
                    model.BTotal = 0;
                }
                return PartialView("~/Views/Shared/Partial/_ViewBethak.cshtml", model);

            }
            else
            {
                return Json("-1", JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult SearchMilanVrutByMonth(string NagarID, int MonthID)
        {
            if (Session["UID"] != null)
            {
                Result model = new Result();
                if (NagarID != null)
                {
                    var Vasti = new List<_Vasti>();
                    string vastiids = string.Empty;
                    Vasti = MasterRepository.GetListvastiByNagar(NagarID);

                    if (Vasti.Count > 0)
                    {
                        foreach (var vid in Vasti)
                        {
                            if (vastiids == "")
                            {
                                vastiids = vid.VastiID.ToString();
                            }
                            else
                            {
                                vastiids += "," + vid.VastiID.ToString();
                            }

                        }
                    }
                    model.ViewMilanUPVrut = MasterRepository.GetViewMilan_NagarVrutByMonth(vastiids, MonthID);

                }
                if (model.ViewMilanUPVrut != null)
                {
                    model.Total = model.ViewMilanUPVrut.Count;
                }
                else
                {
                    model.Total = 0;
                }
                return PartialView("~/Views/Shared/Partial/_ViewMilanVrut.cshtml", model);

            }
            else
            {
                return RedirectToAction("LogOff", "Account");
            }

        }
        public ActionResult SearchBethakByMonth(string NagarID, int MonthID)
        {
            if (Session["UID"] != null)
            {
                Result model = new Result();
                if (NagarID != null)
                {
                    var Vasti = new List<_Vasti>();
                    string vastiids = string.Empty;
                    Vasti = MasterRepository.GetListvastiByNagar(NagarID);

                    if (Vasti.Count > 0)
                    {
                        foreach (var vid in Vasti)
                        {
                            if (vastiids == "")
                            {
                                vastiids = vid.VastiID.ToString();
                            }
                            else
                            {
                                vastiids += "," + vid.VastiID.ToString();
                            }

                        }
                    }
                    model.ViewBethakvasti = MasterRepository.GetViewBethakVastiByMonth(vastiids, MonthID);

                }
                if (model.ViewBethakvasti != null)
                {
                    model.BTotal = model.ViewBethakvasti.Count;
                }
                else
                {
                    model.BTotal = 0;
                }
                return PartialView("~/Views/Shared/Partial/_ViewBethak.cshtml", model);

            }
            else
            {
                return RedirectToAction("LogOff", "Account");
            }

        }
        public ActionResult Fill_AddNagarVrutDetail(string NagarID)
        {
            if (Session["UID"] != null)
            {

                var nagarList = MasterRepository.GetListNagar();

                var BhagID = nagarList.Where(m => m.NagarID == Convert.ToInt32(NagarID)).ToList().FirstOrDefault().BhagID;
                var bhag = MasterRepository.GetListBhag().Where(m => m.BhagID == Convert.ToInt32(BhagID)).ToList().FirstOrDefault().Bhag;

                return Json(new { Bhag = bhag }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return RedirectToAction("LogOff", "Account");
            }

        }

        public ActionResult Fill_AddBethakDetail(string NagarID)
        {
            if (Session["UID"] != null)
            {

                var nagarList = MasterRepository.GetListNagar();

                var BhagID = nagarList.Where(m => m.NagarID == Convert.ToInt32(NagarID)).ToList().FirstOrDefault().BhagID;
                var bhag = MasterRepository.GetListBhag().Where(m => m.BhagID == Convert.ToInt32(BhagID)).ToList().FirstOrDefault().Bhag;

                return Json(new { Bhag = bhag }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return RedirectToAction("LogOff", "Account");
            }

        }
        public ActionResult Fill_AddBethakDetailonUpdate(string NagarID, int MonthID)
        {
            if (Session["UID"] != null)
            {

                var Bethak = MasterRepository.GetViewBethakByMonth(Convert.ToInt32(NagarID), MonthID);
                return Json(new { BethakID = Bethak.FirstOrDefault().BethakID, FW_VishayBethak1 = Bethak.FirstOrDefault().FW_VishayBethak1, FW_VishayBethak2 = Bethak.FirstOrDefault().FW_VishayBethak2, FW_VishayBethak3 = Bethak.FirstOrDefault().FW_VishayBethak3, FW_VishayBethak4 = Bethak.FirstOrDefault().FW_VishayBethak4, FW_VishayBethak5 = Bethak.FirstOrDefault().FW_VishayBethak5, SW_KarykariniBethak = Bethak.FirstOrDefault().SW_KarykariniBethak, TW_VastiBethak = Bethak.FirstOrDefault().TW_VastiBethak, FW_VistrutBethak = Bethak.FirstOrDefault().FW_VistrutBethak }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return RedirectToAction("LogOff", "Account");
            }

        }
        public ActionResult ClearNagarVrutList()
        {
            if (Session["UID"] != null)
            {
                var result = new Result();


                result.ListNagar = new List<_Nagar>();


                return PartialView("~/Views/Shared/Partial/_DDLSelect2N_Nagar.cshtml", result);

            }
            else
            {
                return Json("-1", JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult ClearShakhaVrutList()
        {
            if (Session["UID"] != null)
            {
                var result = new Result();


                result.ListNagar = new List<_Nagar>();


                return PartialView("~/Views/Shared/Partial/_DDLSelect2S_Nagar.cshtml", result);

            }
            else
            {
                return Json("-1", JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult ClearMilanVrutList()
        {
            if (Session["UID"] != null)
            {
                var result = new Result();


                result.ListNagar = new List<_Nagar>();
                return PartialView("~/Views/Shared/Partial/_DDLSelect2M_Nagar.cshtml", result);

            }
            else
            {
                return Json("-1", JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult ClearBethakList()
        {
            if (Session["UID"] != null)
            {
                var result = new Result();


                result.ListNagar = new List<_Nagar>();
                return PartialView("~/Views/Shared/Partial/_DDLSelect2B_Nagar.cshtml", result);

            }
            else
            {
                return Json("-1", JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult InsertNagarVrut(_NagarVrut Nagarvrut)
        {
            var model = new Result();
            if (Session["UID"] != null)
            {
                var result = false;
                if (Nagarvrut.NVID.ToString() != "0")
                {
                    result = MasterRepository.UpdateNagarVrut(Nagarvrut);

                }
                else
                {
                    result = MasterRepository.InsertNagarVrut(Nagarvrut);
                }



                if (result == true)
                {
                    return Json("1", JsonRequestBehavior.AllowGet);
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
        public ActionResult InsertShakhaVrut(_ShakhaUPVrut Shakhavrut)
        {
            var model = new Result();
            if (Session["UID"] != null)
            {
                var result = false;
                if (Shakhavrut.SUPVID.ToString() != "0")
                {
                    result = MasterRepository.UpdateShakhaVrut(Shakhavrut);

                }
                else
                {
                    result = MasterRepository.InsertShakhaVrut(Shakhavrut);
                }



                if (result == true)
                {
                    return Json("1", JsonRequestBehavior.AllowGet);
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

        public ActionResult InsertMilanVrut(_MilanUPVrut Milanvrut)
        {
            var model = new Result();
            if (Session["UID"] != null)
            {
                var result = false;
                if (Milanvrut.MVID.ToString() != "0")
                {
                    result = MasterRepository.UpdateMilanVrut(Milanvrut);

                }
                else
                {
                    result = MasterRepository.InsertMilanVrut(Milanvrut);
                }



                if (result == true)
                {
                    return Json("1", JsonRequestBehavior.AllowGet);
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

        public ActionResult InsertBethak(_Bethak Bethak)
        {
            var model = new Result();
            if (Session["UID"] != null)
            {
                var result = false;
                int r = 0;
                if (Bethak.BethakID.ToString() != "0")
                {
                    result = MasterRepository.UpdateBethak(Bethak);
                    var p = MasterRepository.DeleteBethakVasti(Bethak.BethakID, Bethak.BVastiIDs);
                    if (!string.IsNullOrEmpty(Bethak.BVastiIDs))
                    {
                        if (Bethak.BVastiIDs.Contains(","))
                        {
                            string[] strarrvastis = Bethak.BVastiIDs.Split(',');
                            foreach (string strvastiID in strarrvastis)
                            {
                                result = MasterRepository.InsertBethak_vasti(Bethak.BethakID, Convert.ToInt32(strvastiID));
                            }
                        }
                        else
                        {
                            result = MasterRepository.InsertBethak_vasti(Bethak.BethakID, Convert.ToInt32(Bethak.BVastiIDs));
                        }
                    }
                }
                else
                {
                    r = MasterRepository.InsertBethak(Bethak);
                    if (r != 0)
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                    if (!string.IsNullOrEmpty(Bethak.BVastiIDs) && r != 0)
                    {
                        if (Bethak.BVastiIDs.Contains(","))
                        {
                            string[] strarrvastis = Bethak.BVastiIDs.Split(',');
                            foreach (string strvastiID in strarrvastis)
                            {
                                result = MasterRepository.InsertBethak_vasti(r, Convert.ToInt32(strvastiID));
                            }
                        }
                        else
                        {
                            result = MasterRepository.InsertBethak_vasti(r, Convert.ToInt32(Bethak.BVastiIDs));
                        }
                    }
                }



                if (result == true)
                {
                    return Json("1", JsonRequestBehavior.AllowGet);
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
        public ActionResult N_NagarByID(string NagarID)
        {
            if (Session["UID"] != null)
            {
                var result = new Result();

                if (NagarID != null)
                {
                    result.ListNagar = MasterRepository.GetListNagarWithBhag().Where(m => m.NagarID == Convert.ToInt32(NagarID)).ToList();
                    result.NagarID = Convert.ToInt32(NagarID);
                    if (result.ListNagar.Count > 0)
                    {
                        return PartialView("~/Views/Shared/Partial/_DDLSelect2N_Nagar.cshtml", result);
                    }
                }


            }
            else
            {
                return RedirectToAction("LogOff", "Account");
            }
            return View();
        }
        public ActionResult S_NagarByID(string NagarID)
        {
            if (Session["UID"] != null)
            {
                var result = new Result();

                if (NagarID != null)
                {
                    result.ListNagar = MasterRepository.GetListNagarWithBhag().Where(m => m.NagarID == Convert.ToInt32(NagarID)).ToList();
                    result.NagarID = Convert.ToInt32(NagarID);
                    if (result.ListNagar.Count > 0)
                    {
                        return PartialView("~/Views/Shared/Partial/_DDLSelect2S_Nagar.cshtml", result);
                    }
                }


            }
            else
            {
                return RedirectToAction("LogOff", "Account");
            }
            return View();
        }
        public ActionResult M_NagarByID(string NagarID)
        {
            if (Session["UID"] != null)
            {
                var result = new Result();

                if (NagarID != null)
                {
                    result.ListNagar = MasterRepository.GetListNagarWithBhag().Where(m => m.NagarID == Convert.ToInt32(NagarID)).ToList();
                    result.NagarID = Convert.ToInt32(NagarID);
                    if (result.ListNagar.Count > 0)
                    {
                        return PartialView("~/Views/Shared/Partial/_DDLSelect2M_Nagar.cshtml", result);
                    }
                }


            }
            else
            {
                return RedirectToAction("LogOff", "Account");
            }
            return View();
        }

        public ActionResult B_NagarByID(string NagarID)
        {
            if (Session["UID"] != null)
            {
                var result = new Result();

                if (NagarID != null)
                {
                    result.ListNagar = MasterRepository.GetListNagarWithBhag().Where(m => m.NagarID == Convert.ToInt32(NagarID)).ToList();
                    result.NagarID = Convert.ToInt32(NagarID);
                    if (result.ListNagar.Count > 0)
                    {
                        return PartialView("~/Views/Shared/Partial/_DDLSelect2B_Nagar.cshtml", result);
                    }
                }


            }
            else
            {
                return RedirectToAction("LogOff", "Account");
            }
            return View();
        }
        #endregion

        #region Bethak

        #endregion
        #region Pravasi Karyakarta
        public ActionResult PravasiKaryakarta(string Nagar, string Bhag, string NagarID)
        {
            var model = new Result();
            if (Session["UID"] != null)
            {
                model.UserDetail = AccountRepository.GetuserDetail(Convert.ToInt32(Session["UID"]));
                model.Nagar = Nagar;
                model.Bhag = Bhag;
                model.NagarID = Convert.ToInt32(NagarID);
                model.ListVasti = MasterRepository.GetListvastiByNagar(NagarID);
                var firstitevasti = new _Vasti();
                firstitevasti.VastiID = 0;
                firstitevasti.Vasti = "--Select Vasti---";
                model.ListVasti.Insert(0, firstitevasti);


                model.ListDayitva = MasterRepository.GetListDayitva();
                var firstiteDatitva = new _Dayitva_Mast();
                firstiteDatitva.Id = 0;
                firstiteDatitva.Dayitva = "--Select Dayitva---";
                model.ListDayitva.Insert(0, firstiteDatitva);


                model.ListStar = MasterRepository.GetListStar();
                var firstiteStar = new _Star_Mast();
                firstiteStar.ID = 0;
                firstiteStar.Star = "--Select Star---";
                model.ListStar.Insert(0, firstiteStar);

                model.ViewPravasikaryakarta = MasterRepository.GetViewPravasiKaryakarta(Convert.ToInt32(NagarID));
            }
            else
            {
                return RedirectToAction("LogOff", "Account");
            }
            return View(model);
        }
        public ActionResult InsertPravasiKaryaKarta(_Pravasi_Karyakarta pk)
        {
            var model = new Result();
            if (Session["UID"] != null)
            {
                var result = false;
                if (pk.Pravasi_karyakartaID.ToString() != "0")
                {
                    result = MasterRepository.UpdatePravasiKaryaKarta(pk);

                }
                else
                {
                    result = MasterRepository.InsertPravasiKaryaKarta(pk);
                }



                model.ViewPravasikaryakarta = MasterRepository.GetViewPravasiKaryakarta(Convert.ToInt32(pk.NagarID));


                if (model.ViewPravasikaryakarta.Count > 0)
                {
                    return PartialView("~/Views/Shared/Partial/_ViewPravasiKaryakarta.cshtml", model);
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
        public ActionResult DeletePravasiKaryaKarta(string Pravasi_karyakartaID, string NagarID)
        {
            var model = new Result();
            if (Session["UID"] != null)
            {
                var result = false;
                if (Pravasi_karyakartaID.ToString() != "0")
                {
                    result = MasterRepository.DeletepravaSikaryakarta(Convert.ToInt32(Pravasi_karyakartaID));

                }

                model.ViewPravasikaryakarta = MasterRepository.GetViewPravasiKaryakarta(Convert.ToInt32(NagarID));


                if (model.ViewPravasikaryakarta.Count > 0)
                {
                    return PartialView("~/Views/Shared/Partial/_ViewPravasiKaryakarta.cshtml", model);
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
        #endregion
    }
}
