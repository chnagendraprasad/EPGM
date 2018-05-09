using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;
using EPGM.Business;
using EPGM.Framework;
using System.Text;
using EPGM.Models;

namespace EPGM.Controllers
{
    public class DistrictController : Controller
    {
        /// <summary>
        /// Business object to manipulate data
        /// </summary>
        private readonly DistrictBusiness _DistBuss = new DistrictBusiness();
        private readonly LoginBusiness _loginBuss = new LoginBusiness();

        /// <summary>
        /// The listing / default page
        /// </summary>
        /// <returns>default view</returns>
        public ActionResult Index(string dW5kZWZpbmVk = "", string BeneType = "", string StateCode = "", string Month = "", string Year = "",string CenterType = "")         //dW5kZWZpbmVk - Encrypteed Parameter Name 'whoType'
        {
            Session["State"] = StateCode == "" ? "" : StateCode;
            string userType = Convert.ToString(Session["UserTypeID"]);
            if (string.IsNullOrEmpty(userType))
                return RedirectToAction("MainIndex", "Login");

            if (!string.IsNullOrEmpty(dW5kZWZpbmVk))
            {
                string whoType = Encoding.UTF8.GetString(Convert.FromBase64String(dW5kZWZpbmVk));
                Session["WHOTYPE"] = whoType;
                Session["BeneType"] = BeneType;
                Session["DashMonth"] = Month;
                Session["DashYear"] = Year;


                if (Session["Username"].ToString() != "marsadmin")
                {
                    Session["DashCenterType"] = Session["UserCenterType"].ToString();
                }
                else
                {
                    Session["DashCenterType"] = CenterType;

                }
            }
            ViewBag.Type = Helper.GetGradeNames(Convert.ToString(Session["WHOTYPE"]));
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
                int RecCount = 0;
                string whoType = (Convert.ToString(Session["WHOTYPE"]) == "") ? Convert.ToString(Session["BeneWHOType"]) : Convert.ToString(Session["WHOTYPE"]);
                string stateCode = Session["State"].ToString() == "" ? Convert.ToString(Session["UserState"]) : Session["State"].ToString();
                string distCode = Convert.ToString(Session["UserDistrict"]);
                string BeneType = Session["BeneType"].ToString();
                var items = _DistBuss.Get(search, stateCode, distCode, whoType, BeneType, Session["DashMonth"].ToString(), Session["DashYear"].ToString(), Session["DashCenterType"].ToString());
                //if (items.Count == 0) return null;
                var total = (int)Math.Ceiling(RecCount / (float)search.rows);		//	get page count
                var jsonData = new
                {
                    total = total,
                    page = (total < search.page ? 1 : search.page),					//	set the current page
                    records = items.Count,
                    rows = items,
                };
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Content(ex.InnerException.Message);
            }
        }

        public ActionResult MasterIndex()
        {
            return View();
        }

        /// <summary>
        /// To get the records to show in the grid
        /// </summary>
        /// <param name="search">search criteria</param>
        /// <returns>matching data</returns>
        public ActionResult MasterGridData(SearchFilter search, string StateCode)
        {
            try
            {
                int RecCount = 0;
                var items = _DistBuss.GetMasterData(search, StateCode);
                //if (items.Count == 0) return null;
                var total = (int)Math.Ceiling(RecCount / (float)search.rows);       //	get page count
                var jsonData = new
                {
                    total = total,
                    page = (total < search.page ? 1 : search.page),					//	set the current page
                    records = items.Count,
                    rows = items,
                };
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Content(ex.InnerException.Message);
            }
        }

        public ActionResult DistrictMaster()
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
                return RedirectToAction("MainIndex", "Login");
            }
        }

        [HttpPost]
        public ActionResult DistrictMaster(TblDistrictMst _dist)
        {
            SPUpdateDistrict_Result Data = new SPUpdateDistrict_Result();
            try
            {
                string userType = Convert.ToString(Session["UserTypeID"]);
                if (string.IsNullOrEmpty(userType))
                    return RedirectToAction("MainIndex", "Login");

                if (ModelState.IsValid)
                {
                    _dist.UserCode = Session["UserCode"].ToString();
                    Data = _DistBuss.UpdateDistrict(_dist);
                    return Json(Data, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    Data = new SPUpdateDistrict_Result() { Code = "001", Message = "Please fill All the fields" };
                    return Json(Data, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                Data = new SPUpdateDistrict_Result() { Code = "001", Message = ex.Message.ToString() };
                return Json(Data, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
