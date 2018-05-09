using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Text;
using EPGM.Business;
using EPGM.Models;
using EPGM.Framework;

namespace EPGM.Controllers
{
    public class ProjectController : Controller
    {

        /// <summary>
        /// Business object to manipulate data
        /// </summary>
        private readonly ProjectBusiness _bussProj = new ProjectBusiness();
        private readonly LoginBusiness _loginBuss = new LoginBusiness();

        /// <summary>
        /// The listing / default page
        /// </summary>
        /// <returns>default view</returns>
        public ActionResult Index(string dW5kZWZpbmVk = "", string BeneType = "", string Month = "", string Year = "")
        {
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
            }
            ViewBag.Type = Helper.GetGradeNames(Convert.ToString(Session["WHOTYPE"]));
            return View();
        }

        public ActionResult Project(string distCode = "")
        {
            //if (Convert.ToString(Session["UserTypeID"]) != "1")
            //    return RedirectToAction("Index", "Login");

            var data = new ProjClass();
            data.DistrictCode = distCode;
            ViewBag.DistrictCode = distCode;
            if (!string.IsNullOrEmpty(distCode))
            {
                Session["DistrictCode"] = distCode;
                Session["DistrictName"] = _bussProj.GetDistrictName(Convert.ToString(Session["UserState"]), Convert.ToString(Session["DistrictCode"]));
                ViewBag.DistrictName = Convert.ToString(Session["DistrictName"]);
            }
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
                string stateCode = Convert.ToString(Session["UserState"]);
                string distCode = Convert.ToString(Session["UserDistrict"]);
                string BeneType = Session["BeneType"].ToString();
                var items = _bussProj.Get(search, stateCode, distCode, whoType,BeneType, Session["DashMonth"].ToString(), Session["DashYear"].ToString());
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

        public ActionResult BindDataGrid(SearchFilter search, string StateCode, string DistrictCode,string CenterType)
        {
            try
            {
                int RecCount = 0;
                var items = _bussProj.GetDataGrid(search, StateCode, DistrictCode, CenterType);
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


        /// <summary>
        /// To get selected record details or empty form 
        /// </summary>
        /// <param name="ID">ID of record to fetch</param>
        /// <returns>pre-filled view</returns>
        public ActionResult Edit(int ID = 0)
        {
            try
            {
                if (ID != 0)
                {													//	Edit mode
                    var data = _bussProj.Get(ID);				//	Get the record from DB
                    return View(data);
                }
                else
                {													//	Create mode
                    var data = new VWGetProject();
                    return View(data);								//	return view with appropriate data
                }
            }
            catch (Exception ex)
            {
                return Content(ex.InnerException.Message);
            }
        }

        /// <summary>
        /// To get the data for listing 
        /// </summary>
        /// <returns></returns>
        public ActionResult GetListJSON(string TypeID = "")
        {
            try
            {
                return Json(_bussProj.GetList(TypeID), JsonRequestBehavior.AllowGet);	//	Get the listing (combo) data to call from client side
            }
            catch (Exception ex)
            {
                return Content(ex.InnerException.Message);
            }
        }

        /// <summary>
        /// To get the data for listing 
        /// </summary>
        /// <returns>data</returns>
        [NonAction]
        public List<SelectListItem> GetList()
        {
            try
            {
                return _bussProj.GetList();						//	Get the listing (combo) data to use in other controllers
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// To export the data into various formats
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public ActionResult Export(string type)
        {
            try
            {
                var data = _bussProj.Export();						//	Get the data to export
                //Helper.Export(type, Response, data, "Project");					//	Create export content and embed into response
                return View("Index");
            }
            catch (Exception ex)
            {
                return Content(ex.InnerException.Message);
            }
        }


        public ActionResult ProjectMaster()
        {
            string UserCenterType = Convert.ToString(Session["UserCenterType"]);
            string userType = Convert.ToString(Session["UserTypeID"]);
            if (string.IsNullOrEmpty(userType))
                return RedirectToAction("MainIndex", "Login");

            List<SpGetStates_Result> States = _loginBuss.GetStates().ToList();
            States.Insert(0, new SpGetStates_Result { StateCode = "0", StateName = "--Select State--" });
            ViewBag.States = States.Select(m => new SelectListItem() { Text = m.StateName.ToString(), Value = m.StateCode.ToString() });

            //Get Center Types
            List<SPGetCentersType_Result> CenterTypes = _loginBuss.GetCenterTypes(UserCenterType).ToList();
            ViewBag.CenterType = CenterTypes.Select(m => new SelectListItem() { Text = m.CenterType.ToString(), Value = m.CenterTypeID.ToString() });

            return View();
        }

        [HttpPost]
        public ActionResult ProjectMaster(TblProjectMst _proj)
        {
            SPUpdateProject_Result Data = new SPUpdateProject_Result();
            try
            {
                string userType = Convert.ToString(Session["UserTypeID"]);
                if (string.IsNullOrEmpty(userType))
                    return RedirectToAction("MainIndex", "Login");

                var ResultData = Common.GetUserRights(userType, "/" + Request.RequestContext.RouteData.Values["Controller"].ToString() + "/" + Request.RequestContext.RouteData.Values["Action"].ToString());
                if (ResultData.Count != 0)
                {

                    if (ModelState.IsValid)
                    {
                        _proj.UserCode = Session["UserCode"].ToString();
                        Data = _bussProj.UpdateProject(_proj);
                        return Json(Data, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        Data = new SPUpdateProject_Result() { Code = "001", Message = "Please fill All the fields" };
                        return Json(Data, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }
            }
            catch (Exception ex)
            {
                Data = new SPUpdateProject_Result() { Code = "001", Message = ex.Message.ToString() };
                return Json(Data, JsonRequestBehavior.AllowGet);
            }
        }

    }
}
