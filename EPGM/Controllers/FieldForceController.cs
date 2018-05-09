using EPGM.Business;
using EPGM.Framework;
using EPGM.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace EPGM.Controllers
{
    public class FieldForceController : Controller
    {
        private readonly BeneBusiness _bussBene = new BeneBusiness();
        private readonly DistrictBusiness _distBuss = new DistrictBusiness();
        private readonly LoginBusiness _loginBuss = new LoginBusiness();
       
        public ActionResult Index(string V0hPfERpc3RyaWN0SUQ = "", string Month = "", string Year = "")      //V0hPfERpc3RyaWN0SUQ - Encrypteed Parameter Name 'WHO|DistrictID'
        {
            string userType = Convert.ToString(Session["UserTypeID"]);
            if (string.IsNullOrEmpty(userType))
                return RedirectToAction("MainIndex", "Login");

            if (!string.IsNullOrEmpty(V0hPfERpc3RyaWN0SUQ))
            {
                //Session["State"] = "";
                V0hPfERpc3RyaWN0SUQ = Encoding.UTF8.GetString(Convert.FromBase64String(V0hPfERpc3RyaWN0SUQ));
                string[] str = V0hPfERpc3RyaWN0SUQ.Split('|');
                if (Convert.ToString(str[1]) == "0")
                    return RedirectToAction("MainIndex", "Home");

                Session["BeneWHOType"] = Convert.ToString(str[0]);
                Session["BeneDistCode"] = Convert.ToString(str[1]);
                Session["BeneProjCode"] = Convert.ToString(str[2]);
                Session["BeneSecCode"] = Convert.ToString(str[3]);
                Session["BeneAWCCode"] = Convert.ToString(str[4]);
                Session["BeneType"] = Convert.ToString(str[5]);
                if (Month != "" && Year != "")
                {
                    Session["DashMonth"] = Month;
                    Session["DashYear"] = Year;
                }

            }
            string stateCode = Session["State"].ToString() == "" ? Convert.ToString(Session["UserState"]) : Session["State"].ToString();
            ViewBag.BeneDistName = _distBuss.GetDistrictName(stateCode, Convert.ToString(Session["BeneDistCode"]));
            ViewBag.BeneWHOType = Helper.GetGradeNames(Convert.ToString(Session["BeneWHOType"]));
            return View();
        }

        public ActionResult AddStaff()
        {
            string userType = Convert.ToString(Session["UserTypeID"]);
            if (string.IsNullOrEmpty(userType))
                return RedirectToAction("MainIndex", "Login");

            var ResultData = Common.GetUserRights(userType, "/" + Request.RequestContext.RouteData.Values["Controller"].ToString() + "/" + Request.RequestContext.RouteData.Values["Action"].ToString());
            if (ResultData.Count != 0)
            {

                List<SpGetStates_Result> States = _loginBuss.GetStates().ToList();
                States.Insert(0, new SpGetStates_Result { StateCode = "0", StateName = "--Select State--" });
                ViewBag.States = States.Select(m => new SelectListItem() { Text = m.StateName.ToString(), Value = m.StateCode.ToString() });
              
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        private readonly FieldForceBusiness _bussFieldForce = new FieldForceBusiness();

        [HttpPost]
        public ActionResult InsertFieldForceStaff(IEnumerable<HttpPostedFileBase> files)
        {

            try
            {
                NewFieldForceStaff newFFStaff = new NewFieldForceStaff();
                newFFStaff.CreatedBy = Session["UserCode"].ToString();
                newFFStaff.StaffCode = Request.Form["StaffCode"].ToString() == null ? "" : Request.Form["StaffCode"].ToString();
                newFFStaff.Password = "Admin";
                newFFStaff.Name = Request.Form["Name"].ToString() == null ? "" : Request.Form["Name"].ToString();
                newFFStaff.SurName = Request.Form["SurName"].ToString() == null ? "" : Request.Form["SurName"].ToString();
                newFFStaff.MiddleName = Request.Form["MiddleName"].ToString() == null ? "" : Request.Form["MiddleName"].ToString();
                newFFStaff.DOB = Request.Form["DOB"].ToString() == null ? "" : Request.Form["DOB"].ToString();
                newFFStaff.DOJ = Request.Form["DOJ"].ToString() == null ? "" : Request.Form["DOJ"].ToString();
                newFFStaff.Gender = Request.Form["Gender"].ToString() == null ? "" : Request.Form["Gender"].ToString();
                newFFStaff.MobileNo = Request.Form["MobileNo"].ToString() == null ? "" : Request.Form["MobileNo"].ToString();
                newFFStaff.Email = Request.Form["Email"].ToString() == null ? "" : Request.Form["Email"].ToString();
                newFFStaff.Address = Request.Form["Address"].ToString() == null ? "" : Request.Form["Address"].ToString();
                newFFStaff.StateId = Request.Form["StateId"].ToString() == null ? "" : Request.Form["StateId"].ToString();
                newFFStaff.Qualification = Request.Form["Qualification"].ToString() == null ? "" : Request.Form["Qualification"].ToString();

                string fileName, fileNameFullpath;
                fileNameFullpath = "";

                var date = DateTime.Now.ToString("dd-MM-yyyy").Split('-');
                var time = DateTime.Now.ToString("HH:mm:ss").Split(':');
                var dateandtime = date[0] + date[1] + date[2] + time[0] + time[1] + time[2];

                List<string> FilePath = new List<string>();
                List<string> NamesSave = new List<string>();

                if (Request.Files.Count > 0)
                {
                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        var file = Request.Files[i];
                        string SavePath = "";
                        fileName = Path.GetFileName(Request.Files[i].FileName);
                        if (file != null && file.ContentLength > 0)
                        {
                            if (file.ContentLength < 1000000)
                            {
                                var name = fileName;
                                if (i == 0)
                                {
                                    SavePath = Path.Combine(Server.MapPath("~/FieldForceStaff/"), fileName + "_" + dateandtime);

                                     
                                    fileNameFullpath = ConfigurationManager.AppSettings["FieldForceStaffURL"] + dateandtime + "_" + fileName;
                                }

                                file.SaveAs(SavePath);
                                FilePath.Add(SavePath);
                                NamesSave.Add(name);
                            }
                            else
                            {
                                ResponseStatus FileData = new ResponseStatus();
                                FileData.code = "998";
                                FileData.message = "Inavalid File Length";
                                return Json(FileData, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                }

                newFFStaff.PhotoImagePath = fileNameFullpath;

                ResponseStatus Data = _bussFieldForce.InsertFieldForceStaffDetails(newFFStaff);
                return Json(Data, JsonRequestBehavior.AllowGet);
            }

            catch(Exception ex)
            {
                ResponseStatus Data = new ResponseStatus();
                Data.statusCode = "999";
                Data.message = ex.Message.ToString();
                return Json(Data, JsonRequestBehavior.AllowGet);

            }
           
            //return Content(Data.ToString());
        }

        public ActionResult ActiveInActiveFFStaff()
        {

            string userType = Convert.ToString(Session["UserTypeID"]);
            if (string.IsNullOrEmpty(userType))
                return RedirectToAction("MainIndex", "Login");


            var ResultData = Common.GetUserRights(userType, "/" + Request.RequestContext.RouteData.Values["Controller"].ToString() + "/" + Request.RequestContext.RouteData.Values["Action"].ToString());
            if (ResultData.Count != 0)
            {
                List<SpGetStates_Result> States = _loginBuss.GetStates().ToList();
                States.Insert(0, new SpGetStates_Result { StateCode = "0", StateName = "--Select State--" });
                ViewBag.States = States.Select(m => new SelectListItem() { Text = m.StateName.ToString(), Value = m.StateCode.ToString() });

                

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        #region Bind Filed Force Data To Grid
        public ActionResult GetFieldForceStaff(SearchFilter search, string StateCode)
        {
            try
            {
                int RecCount = 0;

                string stateCode = StateCode == null ? Convert.ToString(Session["UserState"]) : StateCode;
                
                var items = _bussFieldForce.GetFieldForceStaff(search, out RecCount, stateCode);
                //if (items.Count == 0) return null;
                var total = (int)Math.Ceiling(RecCount / (float)search.rows);		//	get page count
                var jsonData = new
                {
                    total = total,
                    page = (total < search.page ? 1 : search.page),					//	set the current page
                    records = items.Count,
                    rows = items
                };
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Content(ex.InnerException.Message);
            }
        }

        #endregion
        #region Field Force Staff Status Change
        public ActionResult FielForceStatusChange(string StaffId, string Status, string StateCode)
        {
            string enabled = null;
            if (Status == "True")
            {
                enabled = "0";
            }
            if (Status == "False")
            {
                enabled = "1";
            }
            //var result = _bussBene.UpdateStatusOfBene(Session["UserState"].ToString(), Session["BDistrict"].ToString(), Session["BProject"].ToString(), Session["BSector"].ToString(), Session["BAWC"].ToString(), BeneCode, enabled);
            var result = _bussFieldForce.FielForceStatusChange(StaffId, StateCode, enabled);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion


    }
}