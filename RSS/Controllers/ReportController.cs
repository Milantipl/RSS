using ConnectionLibrary.Model;
using ConnectionLibrary.Model.Reports;
using ConnectionLibrary.Repository;
using RSS.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace RSS.Controllers
{
    public class ReportController : Controller
    {
        //
        // GET: /Report/

        public ActionResult Index()
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
                model.Report1List = MasterRepository.GetReport1(parameters);
                if (model.UserDetail.Roleid.ToString().Trim() == "2")
                {
                    model.Report1List = model.Report1List.Where(x => x.VibhagID.ToString() == model.UserDetail.RoleWiseDept.ToString().Trim()).ToList();

                }
                else if (model.UserDetail.Roleid.ToString().Trim() == "3")
                {
                    model.Report1List = model.Report1List.Where(x => x.BhagID.ToString() == model.UserDetail.RoleWiseDept.ToString().Trim()).ToList();

                }
                return View(model);
            }
            else
            {
                return RedirectToAction("LogOff", "Account");
            }
        }


        public ActionResult LazyLoad(int MonthID)
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

                if (MonthID != null)
                {
                    model.SearchMonthID = MonthID;
                    parameters.Add(new Tuple<string, string, SqlDbType, int?>("@S_MonthID", MonthID.ToString(), SqlDbType.NVarChar, 500));
                }
                model.Report1List = MasterRepository.GetReport1(parameters);
                if (model.UserDetail.Roleid.ToString().Trim() == "2")
                {
                    model.Report1List = model.Report1List.Where(x => x.VibhagID.ToString() == model.UserDetail.RoleWiseDept.ToString().Trim()).ToList();

                }
                else if (model.UserDetail.Roleid.ToString().Trim() == "3")
                {
                    model.Report1List = model.Report1List.Where(x => x.BhagID.ToString() == model.UserDetail.RoleWiseDept.ToString().Trim()).ToList();

                }
                return PartialView("~/Views/Shared/Partial/Reports/_Report1List.cshtml", model);
            }
            else
            {
                return RedirectToAction("LogOff", "Account");
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult DownloadExcel1(string GridHtml)
        {
            //return File(Encoding.ASCII.GetBytes(GridHtml), "application/vnd.ms-excel", "Grid.xls");
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            var objStringWriter = new StringWriter();
            var objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
            Response.Buffer = true;
            objHtmlTextWriter.Write(GridHtml);
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("content-disposition", "attachment;filename=UserReport.xls");
            Response.Write(GridHtml);
            Response.Flush();
            Response.Close();
            return View();

        }


        public ActionResult Report2()
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
                    parameters.Add(new Tuple<string, string, SqlDbType, int?>("@N_MonthID", MonthId.ToString(), SqlDbType.NVarChar, 500));
                }
                model.Report2List = MasterRepository.GetReport2(parameters);
                if (model.UserDetail.Roleid.ToString().Trim() == "2")
                {
                    model.Report2List = model.Report2List.Where(x => x.VibhagID.ToString() == model.UserDetail.RoleWiseDept.ToString().Trim()).ToList();

                }
                else if (model.UserDetail.Roleid.ToString().Trim() == "3")
                {
                    model.Report2List = model.Report2List.Where(x => x.BhagID.ToString() == model.UserDetail.RoleWiseDept.ToString().Trim()).ToList();

                }
                return View(model);
            }
            else
            {
                return RedirectToAction("LogOff", "Account");
            }
        }

        public ActionResult Report2LazyLoad(int MonthID)
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

                if (MonthID != null)
                {
                    model.SearchMonthID = MonthID;
                    parameters.Add(new Tuple<string, string, SqlDbType, int?>("@N_MonthID", MonthID.ToString(), SqlDbType.NVarChar, 500));
                }
                model.Report2List = MasterRepository.GetReport2(parameters);
                if (model.UserDetail.Roleid.ToString().Trim() == "2")
                {
                    model.Report2List = model.Report2List.Where(x => x.VibhagID.ToString() == model.UserDetail.RoleWiseDept.ToString().Trim()).ToList();

                }
                else if (model.UserDetail.Roleid.ToString().Trim() == "3")
                {
                    model.Report2List = model.Report2List.Where(x => x.BhagID.ToString() == model.UserDetail.RoleWiseDept.ToString().Trim()).ToList();

                }
                return PartialView("~/Views/Shared/Partial/Reports/_Report2List.cshtml", model);
            }
            else
            {
                return RedirectToAction("LogOff", "Account");
            }
        }


        public ActionResult Report3()
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
                model.Report3List = MasterRepository.GetReport3();
                if (model.UserDetail.Roleid.ToString().Trim() == "2")
                {
                    model.Report3List = model.Report3List.Where(x => x.VibhagID.ToString() == model.UserDetail.RoleWiseDept.ToString().Trim()).ToList();

                }
                else if (model.UserDetail.Roleid.ToString().Trim() == "3")
                {
                    model.Report3List = model.Report3List.Where(x => x.BhagID.ToString() == model.UserDetail.RoleWiseDept.ToString().Trim()).ToList();

                }
                return View(model);
            }
            else
            {
                return RedirectToAction("LogOff", "Account");
            }
        }
        public ActionResult Report4()
        {
            if (Session["UID"] != null)
            {
                var model = new Result();
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
                var parameters = new List<Tuple<string, string, SqlDbType, int?>>();
                parameters.Clear();
                model.ListMonth = MasterRepository.GetListMonth();
                var firstitemmonth = new _Month();
                firstitemmonth.MonthID = 0;
                firstitemmonth.Month = "--Select Month---";
                model.ListMonth.Insert(0, firstitemmonth);
                var MonthId = model.ListMonth.Where(x => x.Month.Trim() == (DateTime.Now.ToString("MMM") + "-" + DateTime.Now.ToString("yy")).Trim()).FirstOrDefault().MonthID;
                if (MonthId != null)
                {
                    model.SearchMonthID = MonthId;
                }
                return View(model);
            }
            else
            {
                return RedirectToAction("LogOff", "Account");
            }
        }


        public ActionResult Report4LazyLoad(int MonthID, int BhagID)
        {
            if (Session["UID"] != null)
            {
                var model = new Result();
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
                var parameters = new List<Tuple<string, string, SqlDbType, int?>>();
                parameters.Clear();
                model.ListMonth = MasterRepository.GetListMonth();
                var firstitemmonth = new _Month();
                firstitemmonth.MonthID = 0;
                firstitemmonth.Month = "--Select Month---";
                model.ListMonth.Insert(0, firstitemmonth);
                if (MonthID != null)
                {
                    model.SearchMonthID = MonthID;
                    parameters.Add(new Tuple<string, string, SqlDbType, int?>("@S_MonthID", MonthID.ToString(), SqlDbType.NVarChar, 500));
                    parameters.Add(new Tuple<string, string, SqlDbType, int?>("@BhagId", BhagID.ToString(), SqlDbType.NVarChar, 500));
                }
                #region R1
                model.Report4List = MasterRepository.GetReport4(parameters);
                if (model.Report4List != null)
                {
                    foreach (var item in model.Report4List)
                    {
                        if (item.bandhShakha != 0)
                        {
                            var list = MasterRepository.GetBandhShakhaList(Convert.ToInt32(item.NagarID), MonthID);
                            if (list != null)
                            {
                                string strShakha = String.Empty;
                                foreach (var shakha in list)
                                {
                                    if (strShakha == "")
                                    {
                                        strShakha = shakha.ShakhaName;
                                    }
                                    else
                                    {
                                        strShakha += ", " + shakha.ShakhaName;
                                    }
                                }
                                item.bandhShakhaInString = strShakha;
                            }
                        }

                    }
                }
                model.R1String = ConvertViewToString("~/Views/Shared/Partial/Reports/_Report4List.cshtml", model);


                #endregion


                #region R2
                model.Report5List = MasterRepository.GetReport5(parameters);
                if (model.Report5List != null)
                {
                    foreach (var item in model.Report5List)
                    {
                        if (item.bandhMilanShakha != 0)
                        {
                            var list = MasterRepository.GetBandhMilanList(Convert.ToInt32(item.NagarID), MonthID);
                            if (list != null)
                            {
                                string strShakha = String.Empty;
                                foreach (var milan in list)
                                {
                                    if (strShakha == "")
                                    {
                                        strShakha = milan.MilanName;
                                    }
                                    else
                                    {
                                        strShakha += ", " + milan.MilanName;
                                    }
                                }
                                item.bandhMilanShakhaInString = strShakha;
                            }
                        }

                    }
                }

                model.R2String = ConvertViewToString("~/Views/Shared/Partial/Reports/_Report5List.cshtml", model);
                #endregion


                #region R3
                model.Report6List = MasterRepository.GetReport6(parameters);
                model.R3String = ConvertViewToString("~/Views/Shared/Partial/Reports/_Report6List.cshtml", model);

                #endregion

                #region R4
                model.Report7List = MasterRepository.GetReport7(parameters);
                model.R4String = ConvertViewToString("~/Views/Shared/Partial/Reports/_Report7List.cshtml", model);

                #endregion

                #region R5
                model.Report8List = MasterRepository.GetReport8(parameters);
                model.R5String = ConvertViewToString("~/Views/Shared/Partial/Reports/_Report8List.cshtml", model);

                #endregion

                #region R6
                model.Report9List = MasterRepository.GetReport9(parameters);


                if (model.Report9List != null)
                {
                    foreach (var item in model.Report9List)
                    {
                        if (item.NotBethak != 0)
                        {
                            var list = MasterRepository.GetNoBethakVastiList(Convert.ToInt32(item.NagarID), MonthID);
                            if (list != null)
                            {
                                string strShakha = String.Empty;
                                foreach (var shakha in list)
                                {
                                    if (strShakha == "")
                                    {
                                        strShakha = shakha.Vasti;
                                    }
                                    else
                                    {
                                        strShakha += ", " + shakha.Vasti;
                                    }
                                }
                                item.NotBethakinString = strShakha;
                            }
                        }

                    }
                }

                model.R6String = ConvertViewToString("~/Views/Shared/Partial/Reports/_Report9List.cshtml", model);

                #endregion

                #region R7

                model.Report10List = MasterRepository.GetReport10(parameters);
                model.R7String = ConvertViewToString("~/Views/Shared/Partial/Reports/_Report10List.cshtml", model);

                #endregion

                #region R8

                model.Report11List = MasterRepository.GetReport11(parameters);
                model.R8String = ConvertViewToString("~/Views/Shared/Partial/Reports/_Report11List.cshtml", model);

                #endregion

                #region R9

                model.Report12List = MasterRepository.GetReport12(parameters);
                if (model.Report12List != null)
                {
                    foreach (var item in model.Report12List)
                    {
                        if (item.KaryVihinVasti != 0)
                        {
                            var list = MasterRepository.GetKaryVihinVastiList(Convert.ToInt32(item.NagarID), MonthID);
                            if (list != null)
                            {
                                string strShakha = String.Empty;
                                foreach (var itm in list)
                                {
                                    if (strShakha == "")
                                    {
                                        strShakha = itm.Vasti;
                                    }
                                    else
                                    {
                                        strShakha += ", " + itm.Vasti;
                                    }
                                }
                                item.KaryVihinVastiInString = strShakha;
                            }
                        }

                    }
                }
                model.R9String = ConvertViewToString("~/Views/Shared/Partial/Reports/_Report12List.cshtml", model);

                #endregion

                return Json(new { R1 = model.R1String, R2 = model.R2String, R3 = model.R3String, R4 = model.R4String, R5 = model.R5String, R6 = model.R6String, R7 = model.R7String, R8 = model.R8String, R9 = model.R9String }, JsonRequestBehavior.AllowGet);
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
        [ValidateInput(false)]
        // [HttpPost]
        public ActionResult Export(string GridHtml, string Month, string Bhag)
        {
            var filename = Bhag.ToString().Trim() + "_" + Month.Replace('-', '_').Trim();
            GridHtml = "<!DOCTYPE html><html> <head><meta http-equiv=content-type content=text/html; charset=utf-8 /><link href=https://fonts.googleapis.com/css2?family=Mukta+Vaani:wght@200&display=swap rel=stylesheet> </head>" + GridHtml + " </html>";
            // var G = GridHtml.Replace("<div id=divreport4 class=card card-info>", " <div id=divreport4 class=card card-info style=margin-bottom:25px !important;>");

            string htmlpath = "~/Report4" + "/" + filename + ".html";
            string pdfpath = "~/Report4" + "/" + filename + ".pdf";

            if (System.IO.File.Exists(Server.MapPath(htmlpath)))
            {
                System.IO.File.Delete(Server.MapPath(htmlpath));

            }
            if (System.IO.File.Exists(Server.MapPath(pdfpath)))
            {
                System.IO.File.Delete(Server.MapPath(pdfpath));
            }
            if (!System.IO.File.Exists(Server.MapPath(htmlpath)))
            {
                using (FileStream fs = System.IO.File.Create(Server.MapPath(htmlpath), 1024))
                {
                    Byte[] info = new UTF8Encoding(true).GetBytes(GridHtml);
                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);
                }
            }


            string[] arr1 = new string[] { Server.MapPath(htmlpath) };

            PDFGenerator.HtmlToPdf("~/Report4/", filename, arr1);

            System.IO.File.Delete(Server.MapPath(htmlpath));
            string filepath = Server.MapPath(pdfpath);
            byte[] pdfByte = GetBytesFromFile(filepath);
            return File(pdfByte, "application/pdf", filename + ".pdf");


        }

        [ValidateInput(false)]
        // [HttpPost]
        public ActionResult Exportreport5(string GridHtml)
        {
            var filename = "Report5";
            GridHtml = "<!DOCTYPE html><html> <head><meta http-equiv=content-type content=text/html; charset=utf-8 /><link href=https://fonts.googleapis.com/css2?family=Mukta+Vaani:wght@200&display=swap rel=stylesheet> </head>" + GridHtml + " </html>";
            // var G = GridHtml.Replace("<div id=divreport4 class=card card-info>", " <div id=divreport4 class=card card-info style=margin-bottom:25px !important;>");

            string htmlpath = "~/Report4" + "/" + filename + ".html";
            string pdfpath = "~/Report4" + "/" + filename + ".pdf";

            if (System.IO.File.Exists(Server.MapPath(htmlpath)))
            {
                System.IO.File.Delete(Server.MapPath(htmlpath));

            }
            if (System.IO.File.Exists(Server.MapPath(pdfpath)))
            {
                System.IO.File.Delete(Server.MapPath(pdfpath));
            }
            if (!System.IO.File.Exists(Server.MapPath(htmlpath)))
            {
                using (FileStream fs = System.IO.File.Create(Server.MapPath(htmlpath), 1024))
                {
                    Byte[] info = new UTF8Encoding(true).GetBytes(GridHtml);
                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);
                }
            }


            string[] arr1 = new string[] { Server.MapPath(htmlpath) };

            PDFGenerator.HtmlToPdf("~/Report4/", filename, arr1);

            System.IO.File.Delete(Server.MapPath(htmlpath));
            string filepath = Server.MapPath(pdfpath);
            byte[] pdfByte = GetBytesFromFile(filepath);
            return File(pdfByte, "application/pdf", filename + ".pdf");


        }
        public byte[] GetBytesFromFile(string fullFilePath)
        {
            // this method is limited to 2^32 byte files (4.2 GB)
            FileStream fs = null;
            try
            {
                fs = System.IO.File.OpenRead(fullFilePath);
                byte[] bytes = new byte[fs.Length];
                fs.Read(bytes, 0, Convert.ToInt32(fs.Length));
                return bytes;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }
            }
        }

        public ActionResult Report5()
        {
            var model = new Result();
            if (Session["UID"] != null)
            {

                model.UserDetail = AccountRepository.GetuserDetail(Convert.ToInt32(Session["UID"]));
                model.ListBhag = MasterRepository.GetListBhag();
                model.ViewNagar = MasterRepository.ViewNagar();
                if (model.UserDetail.Roleid.ToString().Trim() == "2")
                {
                    model.ListBhag = model.ListBhag.Where(x => x.VibhagID.ToString() == model.UserDetail.RoleWiseDept.ToString().Trim()).ToList();

                }
                else if (model.UserDetail.Roleid.ToString().Trim() == "3")
                {
                    model.ListBhag = model.ListBhag.Where(x => x.BhagID.ToString() == model.UserDetail.RoleWiseDept.ToString().Trim()).ToList();

                }
             
                if (model.ListBhag != null)
                {
                    model.Report13List = new List<ListofReport13>();
                    foreach (var item in model.ListBhag)
                    {
                        ListofReport13 Report13List = new ListofReport13();
                        Report13List.ListReport13 = new List<Report13>();
                        Report13List.Bhag = item.Bhag;
             
                        var Nagar = model.ViewNagar.Where(x => x.BhagID.ToString() == item.BhagID.ToString()).ToList();
                        foreach (var ListNagar in Nagar)
                        {
                            var report13 = new Report13();
                            report13.Nagar = ListNagar.Nagar;
                            report13.Bhag = item.Bhag;
                        //Karyavihinvasti
                            var KaryaVihinVastilist = MasterRepository.GetAllKaryVihinVastiList(Convert.ToInt32(ListNagar.NagarID));
                            if (KaryaVihinVastilist != null)
                            {
                                string strvasti = String.Empty;
                                foreach (var itm in KaryaVihinVastilist)
                                {
                                    if (strvasti == "")
                                    {
                                        strvasti = itm.Vasti;
                                    }
                                    else
                                    {
                                        strvasti += ", " + itm.Vasti;
                                    }
                                }
                                report13.KaryVihinVasti = strvasti;
                            }

                            //Samparkit vasti
                            var SamparkitVastiList = MasterRepository.GetAllSamparkitVastiList(Convert.ToInt32(ListNagar.NagarID));
                            if (SamparkitVastiList != null)
                            {
                                string strvasti = String.Empty;
                                foreach (var itm in SamparkitVastiList)
                                {
                                    if (strvasti == "")
                                    {
                                        strvasti = itm.Vasti;
                                    }
                                    else
                                    {
                                        strvasti += ", " + itm.Vasti;
                                    }
                                }
                                report13.SamparkitVasti = strvasti;
                            }


                            //karyayuktVastiList
                            var karyayuktVastiList = MasterRepository.GetAllkaryayuktVastiList(Convert.ToInt32(ListNagar.NagarID));
                            if (karyayuktVastiList != null)
                            {
                                string strvasti = String.Empty;
                                foreach (var itm in karyayuktVastiList)
                                {
                                    if (strvasti == "")
                                    {
                                        strvasti = itm.Vasti;
                                    }
                                    else
                                    {
                                        strvasti += ", " + itm.Vasti;
                                    }
                                }
                                report13.KaryYuktVasti = strvasti;
                            }
                            Report13List.ListReport13.Add(report13);
                        }


                        model.Report13List.Add(Report13List);
                    }
                    
                 
                }
               
            }
            else
            {
                return RedirectToAction("LogOff", "Account");
            }
            return View(model);
        }

    }
}
