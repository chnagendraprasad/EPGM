using EPGM.Business;
using EPGM.Framework;
using EPGM.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace EPGM.Controllers
{
    public class BeneController : Controller
    {
        /// <summary>
        /// Business object to manipulate data
        /// </summary>
        private readonly BeneBusiness _bussBene = new BeneBusiness();
        private readonly DistrictBusiness _distBuss = new DistrictBusiness();
        private readonly LoginBusiness _loginBuss = new LoginBusiness();

        /// <summary>
        /// The listing / default page
        /// </summary>
        /// <returns>default view</returns>
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
               // Session["DashCenterType"] = Session["UserCenterType"].ToString();

                if (Session["Username"].ToString() != "marsadmin")
                {
                    Session["DashCenterType"] = Session["UserCenterType"].ToString();
                }
               

            }
            string stateCode = Session["State"].ToString() == "" ? Convert.ToString(Session["UserState"]) : Session["State"].ToString();
            ViewBag.BeneDistName = _distBuss.GetDistrictName(stateCode, Convert.ToString(Session["BeneDistCode"]));
            ViewBag.BeneWHOType = Helper.GetGradeNames(Convert.ToString(Session["BeneWHOType"]));
            return View();
        }

        /// <summary>
        /// To get the records to show in the grid
        /// </summary>
        /// <param name="search">search criteria</param>
        /// <returns>matching data</returns>
        public ActionResult GridData(SearchFilter search)
        {
            try
            {
                string whoType = "";
                int RecCount = 0;
                if (Session["UserTypeID"].ToString() != "7")
                {
                    whoType = Session["WHOTYPE"] == "" ? "All" : Session["WHOTYPE"].ToString();
                }
                else
                {
                    whoType = Session["BeneWHOType"].ToString();
                }

                string stateCode = Session["State"].ToString() == "" ? Convert.ToString(Session["UserState"]) : Session["State"].ToString();
                string distCode = Session["BeneDistCode"] == null ? "" : Session["BeneDistCode"].ToString();
                string projCode = Session["BeneProjCode"] == null ? "" : Session["BeneProjCode"].ToString();
                string secCode = Session["BeneSecCode"] == null ? "" : Session["BeneSecCode"].ToString();
                string awcCode = Session["BeneAWCCode"] == null ? "" : Session["BeneAWCCode"].ToString();
                string Typebene = Session["BeneType"] == null ? "" : Session["BeneType"].ToString();
                string CenterType = Session["DashCenterType"] == null ? "" : Session["DashCenterType"].ToString();
               
                var items = _bussBene.Get(search, out RecCount, stateCode, distCode, projCode, secCode, awcCode, whoType, Session["DashMonth"].ToString(), Session["DashYear"].ToString(), Typebene, CenterType);
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
        public ActionResult BeneList(string beneListParams)
        {
            string data = beneListParams;
            string distCode = "";
            string whoType = "";
            string projCode = "";
            string secCode = "";
            string awcCode = "";
            return View();
        }
        public ActionResult MasterIndex(string Dist, string Proje, string Sector, string Center)
        {
            string userType = Convert.ToString(Session["UserTypeID"]);
            string UserCenterType = Convert.ToString(Session["UserCenterType"]);
            if (string.IsNullOrEmpty(userType))
                return RedirectToAction("MainIndex", "Login");

            var ResultData = Common.GetUserRights(userType, "/" + Request.RequestContext.RouteData.Values["Controller"].ToString() + "/" + Request.RequestContext.RouteData.Values["Action"].ToString());
            if (ResultData.Count != 0)
            {
                List<SpGetStates_Result> States = _loginBuss.GetStates().ToList();
                States.Insert(0, new SpGetStates_Result { StateCode = "0", StateName = "--Select State--" });
                ViewBag.States = States.Select(m => new SelectListItem() { Text = m.StateName.ToString(), Value = m.StateCode.ToString() });

                ViewBag.statecode = Session["UserState"].ToString();
                //Months Binding
                Dictionary<int, string> dDur = new Dictionary<int, string>();

                dDur.Add(0, "--Select Month--");
                for (int i = 1; i <= 12; i++)
                {
                    dDur.Add(i, CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i));
                }

                ViewBag.Months = dDur.Select(item => new SelectListItem { Value = item.Key.ToString(), Text = item.Value });

                var Data = _loginBuss.GetBeneType();
                ViewBag.BeneType = Data.AsEnumerable().Select(m => new SelectListItem() { Text = m.BeneType, Value = m.BeneTypeID.ToString() });
                //Get Center Types
                List<SPGetCentersType_Result> CenterTypes = _loginBuss.GetCenterTypes(UserCenterType).ToList();
                ViewBag.CenterType = CenterTypes.Select(m => new SelectListItem() { Text = m.CenterType.ToString(), Value = m.CenterTypeID.ToString() });
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
        /// <summary>
        /// To get the records to show in the grid
        /// </summary>
        /// <param name="search">search criteria</param>
        /// <returns>matching data</returns>
        public ActionResult MasterGridData(SearchFilter search, string StateCode, string distCode = "", string projCode = "", string secCode = "", string awcCode = "", string month = "", string year = "", string BeneType = "",string CenterType = "")
        {
            try
            {
                int RecCount = 0;
                string stateCode = Convert.ToString(Session["UserState"]);
                Session["SelectedState"] = StateCode;
                Session["SelectedDistrict"] = distCode;
                Session["SelectedProject"] = projCode;
                Session["SelectedSector"] = secCode;
                Session["SelectedAWC"] = awcCode;
                Session["SelectedMonth"] = month;
                Session["SelectedYear"] = year;
                Session["SelectedBeneType"] = BeneType;
                Session["CenterType"] = CenterType;
                var items = _bussBene.GetMasterData(search, out RecCount, StateCode, distCode, projCode, secCode, awcCode, "All", month, year, BeneType, CenterType);
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
        public ActionResult BeneDetails(string beneCode, string StateCode, string distcode, string Projectcode, string Sectorcode, string awccode, string StateName, string distname, string projectname, string sectorname, string awcname, string Month, string Year, string BeneType,string CenterType)
        {
            try
            {
                string userType = Convert.ToString(Session["UserTypeID"]);
                if (string.IsNullOrEmpty(userType))
                    return RedirectToAction("MainIndex", "Login");

                ViewBag.BeneCode = beneCode;
                ViewBag.StateCode = StateCode;
                ViewBag.distcode = distcode;
                ViewBag.Projectcode = Projectcode;
                ViewBag.Sectorcode = Sectorcode;
                ViewBag.awccode = awccode;
                ViewBag.Month = Month;
                ViewBag.Year = Year;
                ViewBag.distname = distname;
                ViewBag.projectname = projectname;
                ViewBag.sectorname = sectorname;
                ViewBag.awcname = awcname;
                ViewBag.BeneType = BeneType;
                ViewBag.CenterType = CenterType;
                return View();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public ActionResult AddBeneficiary()
        {
            string userType = Convert.ToString(Session["UserTypeID"]);
            string UserCenterType = Convert.ToString(Session["UserCenterType"]);
            if (string.IsNullOrEmpty(userType))
                return RedirectToAction("MainIndex", "Login");

            var ResultData = Common.GetUserRights(userType, "/" + Request.RequestContext.RouteData.Values["Controller"].ToString() + "/" + Request.RequestContext.RouteData.Values["Action"].ToString());
            if (ResultData.Count != 0)
            {

                List<SpGetStates_Result> States = _loginBuss.GetStates().ToList();
                States.Insert(0, new SpGetStates_Result { StateCode = "0", StateName = "--Select State--" });
                ViewBag.States = States.Select(m => new SelectListItem() { Text = m.StateName.ToString(), Value = m.StateCode.ToString() });

                var Category = _loginBuss.GetCategorys().AsEnumerable().Select(m => new SelectListItem() { Text = m.CategoryName, Value = m.CategoryCode.ToString() });
                ViewBag.Categorys = Category;

                var Data = _loginBuss.GetBeneType();
                Data.Insert(0, new SPGetBeneType_Result { BeneTypeID = 0, BeneType = "--Select BeneType--" });
                ViewBag.BeneType = Data.AsEnumerable().Select(m => new SelectListItem() { Text = m.BeneType, Value = m.BeneTypeID.ToString() });
                //Get Center Types
                List<SPGetCentersType_Result> CenterTypes = _loginBuss.GetCenterTypes(UserCenterType).ToList();
                ViewBag.CenterType = CenterTypes.Select(m => new SelectListItem() { Text = m.CenterType.ToString(), Value = m.CenterTypeID.ToString() });
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
        public ActionResult InsertBeneficiary(NewBene newbene)
        {
            newbene.CreatedBy = Session["UserCode"].ToString();
            newbene.StateID = newbene.StateID == null ? Session["UserState"].ToString() : newbene.StateID;
            newbene.DistrictID = (newbene.DistrictID) == null ? Session["UserDistrict"].ToString() : newbene.DistrictID;
            newbene.ProjectID = (newbene.ProjectID) == null ? Session["UserProject"].ToString() : newbene.ProjectID;
            newbene.SectorID = (newbene.SectorID) == null ? Session["UserSector"].ToString() : newbene.SectorID;
            newbene.CenterID = (newbene.CenterID) == null ? Session["UserCenter"].ToString() : newbene.CenterID;
            newbene.FatherName = (newbene.FatherName) == null ? "" : newbene.FatherName.ToString().ToUpper();
            ResponseStatus Data = new BeneBusiness().InsertBeneDetails(newbene);
            return Json(Data, JsonRequestBehavior.AllowGet);
            //return Content(Data.ToString());
        }
        public ActionResult BeneDetailsExport(string filetype)
        {
            SearchFilter filter = (SearchFilter)Session["Spaesearch"];
            try
            {
                int RecCount = 0;
                string stateCode = Convert.ToString(Session["SelectedState"]);
                string distCode = Convert.ToString(Session["SelectedDistrict"]);
                string projCode = Convert.ToString(Session["SelectedProject"]);
                string secCode = Convert.ToString(Session["SelectedSector"]);
                string awcCode = Convert.ToString(Session["SelectedAWC"]);
                string month = Convert.ToString(Session["SelectedMonth"]);
                string year = Convert.ToString(Session["SelectedYear"]);
                string BeneType = Convert.ToString(Session["SelectedBeneType"]);
                string CenterType = Convert.ToString(Session["CenterType"]);


                var data = _bussBene.GetMasterDataExport(filter, out RecCount, stateCode, distCode, projCode, secCode, awcCode, "All", month, year, BeneType, CenterType);
                Helper.Export(filetype, Response,data, "List of Beneficiaries");
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public ActionResult ActiveInactiveBene()
        {
            string userType = Convert.ToString(Session["UserTypeID"]);

            string UserCenterType = Convert.ToString(Session["UserCenterType"]);
            if (string.IsNullOrEmpty(userType))
                return RedirectToAction("MainIndex", "Login");

            var ResultData = Common.GetUserRights(userType, "/" + Request.RequestContext.RouteData.Values["Controller"].ToString() + "/" + Request.RequestContext.RouteData.Values["Action"].ToString());
            if (ResultData.Count != 0)
            {
                List<SpGetStates_Result> States = _loginBuss.GetStates().ToList();
                States.Insert(0, new SpGetStates_Result { StateCode = "0", StateName = "--Select State--" });
                ViewBag.States = States.Select(m => new SelectListItem() { Text = m.StateName.ToString(), Value = m.StateCode.ToString() });

                ViewBag.statecode = Session["UserState"].ToString();
                var Data = _loginBuss.GetBeneType();
                ViewBag.BeneType = Data.AsEnumerable().Select(m => new SelectListItem() { Text = m.BeneType, Value = m.BeneTypeID.ToString() });

                //Get Center Types
                List<SPGetCentersType_Result> CenterTypes = _loginBuss.GetCenterTypes(UserCenterType).ToList();
                ViewBag.CenterType = CenterTypes.Select(m => new SelectListItem() { Text = m.CenterType.ToString(), Value = m.CenterTypeID.ToString() });
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
        public ActionResult GetBenesofCenter(SearchFilter search, string StateCode, string distCode = "", string projCode = "", string secCode = "", string awcCode = "", string BeneType = "",string CenterType = "")
        {
            try
            {
                int RecCount = 0;
                string stateCode = StateCode == null ? Convert.ToString(Session["UserState"]) : StateCode;
                Session["BDistrict"] = distCode;
                Session["BProject"] = projCode;
                Session["BSector"] = secCode;
                Session["BAWC"] = awcCode;
                Session["BBeneType"] = BeneType;
                var items = _bussBene.GetBenesofCenter(search, out RecCount, stateCode, distCode, projCode, secCode, awcCode, BeneType, CenterType);
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
        public ActionResult BenestatusChange(string BeneCode, string Status, string StateCode, string distCode = "", string projCode = "", string secCode = "", string awcCode = "",string centertype = "")
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
            var result = _bussBene.UpdateStatusOfBene(StateCode, distCode, projCode, secCode, awcCode, BeneCode, enabled, centertype);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult BeneEdit()
        {
            try
            {
                string userType = Convert.ToString(Session["UserTypeID"]);
                string UserCenterType = Convert.ToString(Session["UserCenterType"]);
                if (string.IsNullOrEmpty(userType))
                    return RedirectToAction("MainIndex", "Login");

                var ResultData = Common.GetUserRights(userType, "/" + Request.RequestContext.RouteData.Values["Controller"].ToString() + "/" + Request.RequestContext.RouteData.Values["Action"].ToString());
                if (ResultData.Count != 0)
                {

                    List<SpGetStates_Result> States = _loginBuss.GetStates().ToList();
                    States.Insert(0, new SpGetStates_Result { StateCode = "0", StateName = "--Select State--" });
                    ViewBag.States = States.Select(m => new SelectListItem() { Text = m.StateName.ToString(), Value = m.StateCode.ToString() });

                    List<USP_S_CATEGORY_Result> BeneCat = _loginBuss.GetCategorys().ToList();
                    BeneCat.Insert(0, new USP_S_CATEGORY_Result { CategoryCode = "0", CategoryName = "--Select Category--" });
                    ViewBag.Categorys = BeneCat.Select(m => new SelectListItem() { Text = m.CategoryName.ToString(), Value = m.CategoryCode.ToString() });
                    var Data = _loginBuss.GetBeneType();
                    ViewBag.BeneType = Data.AsEnumerable().Select(m => new SelectListItem() { Text = m.BeneType, Value = m.BeneTypeID.ToString() });

                    //Get Center Types
                    List<SPGetCentersType_Result> CenterTypes = _loginBuss.GetCenterTypes(UserCenterType).ToList();
                    ViewBag.CenterType = CenterTypes.Select(m => new SelectListItem() { Text = m.CenterType.ToString(), Value = m.CenterTypeID.ToString() });
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }
            }
            catch (Exception ex)
            {
                return Content(ex.Message.ToString());
            }
        }
        public ActionResult GetBeneDataforEdit(SearchFilter search, string StateCode, string distCode = "", string projCode = "", string secCode = "", string awcCode = "", string BeneType = "",string CenterType = "")
        {
            try
            {
                int RecCount = 0;
                var items = _bussBene.GetBeneficiaryDetails(search, out RecCount, StateCode, distCode, projCode, secCode, awcCode, BeneType, CenterType);
                // if (items.Count == 0) return null;
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
        [HttpPost]
        public ActionResult BeneEdit(UpdateBene _Update)
        {
            USP_Update_BENEFICIARY_Result Data = new USP_Update_BENEFICIARY_Result();
            try
            {
                string userType = Convert.ToString(Session["UserTypeID"]);
                if (string.IsNullOrEmpty(userType))
                    return RedirectToAction("MainIndex", "Login");


                _Update.UserCode = Session["UserCode"].ToString();
                _Update.StateID = (_Update.StateID) == null ? Session["UserState"].ToString() : _Update.StateID;
                _Update.DistrictID = (_Update.DistrictID) == null ? Session["UserDistrict"].ToString() : _Update.DistrictID;
                _Update.ProjectID = (_Update.ProjectID) == null ? Session["UserProject"].ToString() : _Update.ProjectID;
                _Update.SectorID = (_Update.SectorID) == null ? Session["UserSector"].ToString() : _Update.SectorID;
                _Update.AWCID = (_Update.AWCID) == null ? Session["UserCenter"].ToString() : _Update.AWCID;
                _Update.FatherName = (_Update.FatherName) == null ? "" : _Update.FatherName.ToString().ToUpper();

                Data = new BeneBusiness().UpdateBeneDetails(_Update);
                return Json(Data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Data = new USP_Update_BENEFICIARY_Result() { Code = "001", Message = ex.Message.ToString() };
                return Json(Data, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult ChangePWorLWType()
        {
            try
            {
                string userType = Convert.ToString(Session["UserTypeID"]);
                string UserCenterType = Convert.ToString(Session["UserCenterType"]);
                if (string.IsNullOrEmpty(userType))
                    return RedirectToAction("MainIndex", "Login");

                var ResultData = Common.GetUserRights(userType, "/" + Request.RequestContext.RouteData.Values["Controller"].ToString() + "/" + Request.RequestContext.RouteData.Values["Action"].ToString());
                if (ResultData.Count != 0)
                {

                    List<SpGetStates_Result> States = _loginBuss.GetStates().ToList();
                    States.Insert(0, new SpGetStates_Result { StateCode = "0", StateName = "--Select State--" });
                    ViewBag.States = States.Select(m => new SelectListItem() { Text = m.StateName.ToString(), Value = m.StateCode.ToString() });

                    var Data = _loginBuss.GetBeneType();
                    ViewBag.BeneType = Data.AsEnumerable().Select(m => new SelectListItem() { Text = m.BeneType, Value = m.BeneTypeID.ToString() });

                    //Get Center Types
                    List<SPGetCentersType_Result> CenterTypes = _loginBuss.GetCenterTypes(UserCenterType).ToList();
                    ViewBag.CenterType = CenterTypes.Select(m => new SelectListItem() { Text = m.CenterType.ToString(), Value = m.CenterTypeID.ToString() });
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }
            }
            catch (Exception ex)
            {
                return Content(ex.Message.ToString());
            }
        }

        public ActionResult GetPWWomenData(SearchFilter search, string StateCode, string distCode = "", string projCode = "", string secCode = "", string awcCode = "", string BeneType = "",string CenterType = "")
        {

            try
            {
                int RecCount = 0;
                var items = _bussBene.GetPWWomenData(search, out RecCount, StateCode, distCode, projCode, secCode, awcCode, BeneType, CenterType);
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

        public ActionResult UpdatePWType(string BeneID = "", string BeneCode = "", string BeneType = "")
        {
            try
            {
                var Response = _bussBene.UpdatePWType(BeneID, BeneCode, BeneType);
                return Json(Response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.InnerException.Message, JsonRequestBehavior.AllowGet);
            }
        }


        #region Face Api for Beneficiary Creation,Activation and Deletion

        public ActionResult RegisterPerson(FaceAPIReg _FaceAPIReg)
        {
            ResponseStatus Response = new ResponseStatus();
            Response = _bussBene.RegisterPerson(_FaceAPIReg);
            return Json(Response, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ErollPerson(FaceAPIReg _FaceAPIReg)
        {
            ResponseStatus Response = new ResponseStatus();
            Response = _bussBene.EnrollPerson(_FaceAPIReg);
            return Json(Response, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeletePersonFace(FaceAPIReg _FaceAPIReg)
        {
            ResponseStatus Response = new ResponseStatus();
            _FaceAPIReg.UserCode = Session["UserCode"].ToString();
            Response = _bussBene.DeletePersonFace(_FaceAPIReg);
            return Json(Response, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeletePerson(FaceAPIReg _FaceAPIReg)
        {
            ResponseStatus Response = new ResponseStatus();
            _FaceAPIReg.UserCode = Session["UserCode"].ToString();
            Response = _bussBene.DeletePerson(_FaceAPIReg);
            return Json(Response, JsonRequestBehavior.AllowGet);
        }
        #endregion


        public ActionResult GetWeightHeightYears()
        {
            return View();
        }
    }
}
