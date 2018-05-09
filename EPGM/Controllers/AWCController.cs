using EPGM.Business;
using EPGM.Framework;
using EPGM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace EPGM.Controllers
{
    public class AWCController : Controller
    {
        /// <summary>
        /// Business object to manipulate data
        /// </summary>
        private readonly AWCBusiness _awcBuss = new AWCBusiness();
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
                string StateCode = Convert.ToString(Session["UserState"]);
                string distCode = Convert.ToString(Session["UserDistrict"]);
                string SectorCode = Convert.ToString(Session["UserSector"]);
                string BeneType = Session["BeneType"].ToString();
                var items = _awcBuss.Get(search, StateCode, SectorCode, whoType, BeneType, Session["DashMonth"].ToString(), Session["DashYear"].ToString(), Session["DashCenterType"].ToString());
                if (items.Count == 0) return null;
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

        public ActionResult GetGridData(SearchFilter search, string StateCode, string DistrictCode, string ProjectCode, string SectorCode,string CenterType)
        {
            try
            {
                int RecCount = 0;
                var items = _awcBuss.GetGridData(search, StateCode, DistrictCode, ProjectCode, SectorCode, CenterType);
                if (items.Count == 0) return null;
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


        /// <summary>
        /// To get the data for listing 
        /// </summary>
        /// <returns></returns>
        public ActionResult GetListJSON(string TypeID = "")
        {
            try
            {
                return Json(_awcBuss.GetList(TypeID), JsonRequestBehavior.AllowGet);	//	Get the listing (combo) data to call from client side
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
                return _awcBuss.GetList();						//	Get the listing (combo) data to use in other controllers
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
                var data = _awcBuss.Export();						//	Get the data to export
                //Helper.Export(type, Response, data, "AWC");					//	Create export content and embed into response
                return View("Index");
            }
            catch (Exception ex)
            {
                return Content(ex.InnerException.Message);
            }
        }

        ///// <summary>
        ///// To get selected record details or empty form 
        ///// </summary>
        ///// <param name="ID">ID of record to fetch</param>
        ///// <returns>pre-filled view</returns>
        //public ActionResult Edit(int ID = 0)
        //{
        //    try
        //    {
        //        if (ID != 0)
        //        {													//	Edit mode
        //            var data = BusinessObject.Get(ID);				//	Get the record from DB
        //            return View(data);
        //        }
        //        else
        //        {													//	Create mode
        //            var data = new TblAWC();
        //            return View(data);								//	return view with appropriate data
        //        }
        //    }
        //    catch (CustomException ex)
        //    {
        //        LogManager.Instance.Error(ex);
        //        return Content(ex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        LogManager.Instance.Error(ex);
        //        return Content(Constants.Error);
        //    }
        //}

        ///// <summary>
        ///// To save or update the record
        ///// </summary>
        ///// <param name="AWC">AWC object to save or update</param>
        ///// <returns>status</returns>
        //[HttpPost]
        //public ActionResult Edit(TblAWC data)
        //{
        //    try
        //    {
        //        if (data == null || !ModelState.IsValid)				//	validate data
        //            throw new CustomException("Invalid Data", null);

        //        var status = BusinessObject.Save(data);					//	save data
        //        return Json(status.ToString(), JsonRequestBehavior.AllowGet);						//	return status of the operation 
        //    }
        //    catch (CustomException ex)
        //    {
        //        LogManager.Instance.Error(ex);
        //        return Content(ex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        LogManager.Instance.Error(ex);
        //        return Content(Constants.Error);
        //    }
        //}

        ///// <summary>
        ///// To toggle Active state of a record
        ///// </summary>
        ///// <param name="ID">ID of the record to toggle</param>
        ///// <returns>status</returns>
        //[HttpPost]
        //public ActionResult ToggleActive(int ID)
        //{
        //    try
        //    {
        //        var status = BusinessObject.ToggleActive(ID);			//	Toggle active / inactive for record
        //        return Content(status.ToString());						//	return status of the operation
        //    }
        //    catch (CustomException ex)
        //    {
        //        LogManager.Instance.Error(ex);
        //        return Content(ex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        LogManager.Instance.Error(ex);
        //        return Content(Constants.Error);
        //    }
        //}

        ///// <summary>
        ///// To delete a record
        ///// </summary>
        ///// <param name="ID">ID of the record to delete</param>
        ///// <returns>status</returns>
        //[HttpPost]
        //public ActionResult Delete(int ID)
        //{
        //    try
        //    {
        //        var status = BusinessObject.Delete(ID);					//	Delete the selected record
        //        return Content(status.ToString());						//	return status of the operation
        //    }
        //    catch (CustomException ex)
        //    {
        //        LogManager.Instance.Error(ex);
        //        return Content(ex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        LogManager.Instance.Error(ex);
        //        return Content(Constants.Error);
        //    }
        //}
        public ActionResult CreateAWC()
        {
            try
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
                else { return RedirectToAction("Index", "Login"); }
            }
            catch (Exception ex)
            {
                return Content(ex.Message.ToString());
            }
        }

        [HttpPost]
        public ActionResult CreateAWC(NewAWC _newawc)
        {
            SPCreateAWC_Result Data = new SPCreateAWC_Result();
            try
            {
                if (ModelState.IsValid)
                {
                    _newawc.UserCode = Session["UserCode"].ToString();
                    var _loginBuss = new LoginBusiness();
                    Data = _awcBuss.CreateAWC(_newawc);
                    return Json(Data, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    Data = new SPCreateAWC_Result() { Code = "001", Message = "Please fill All the fields", AWCCode = 0 };
                    return Json(Data, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                Data = new SPCreateAWC_Result() { Code = "001", Message = ex.Message.ToString(), AWCCode = 0 };
                return Json(Data, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult AWCMaster()
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

                List<SPGetStaffFF_Result> Staff = _loginBuss.GetStaff("0").ToList();
                Staff.Insert(0, new SPGetStaffFF_Result { StaffID = 0, StaffCode = "--Select Staff--" });
                ViewBag.Staff = Staff.Select(m => new SelectListItem() { Text = m.StaffCode.ToString(), Value = m.StaffID.ToString() });

                // Get Staff
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
        [HttpPost]
        public ActionResult AWCMaster(TblAWCMst _awc)
        {
            SPUpdateAWC_Result Data = new SPUpdateAWC_Result();
            try
            {
                string userType = Convert.ToString(Session["UserTypeID"]);
                if (string.IsNullOrEmpty(userType))
                    return RedirectToAction("MainIndex", "Login");

                if (ModelState.IsValid)
                {
                    _awc.UserCode = Session["UserCode"].ToString();
                    Data = _awcBuss.UpdateAWC(_awc);
                    return Json(Data, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    Data = new SPUpdateAWC_Result() { Code = "001", Message = "Please fill All the fields" };
                    return Json(Data, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                Data = new SPUpdateAWC_Result() { Code = "001", Message = ex.Message.ToString() };
                return Json(Data, JsonRequestBehavior.AllowGet);
            }
        }

        #region Face Api for Group(Hierarchy) Creation,Activation and Deletion
        public ActionResult CreateHierarchy(string AWCCode, string AWCName, string StateCode)
        {
            ResponseStatus Response = new ResponseStatus();
            Response = _awcBuss.CreateHierarchy(StateCode, AWCCode, AWCName);
            return Json(Response, JsonRequestBehavior.AllowGet); ;
        }

        public ActionResult ActivateHierarchy(string AWCCode, string StateCode)
        {
            ResponseStatus Response = new ResponseStatus();
            Response = _awcBuss.ActivateHierarchy(StateCode, AWCCode);
            return Json(Response, JsonRequestBehavior.AllowGet); ;
        }

        public ActionResult DeleteHierarchy(UserData _UData)
        {
            ResponseStatus Response = new ResponseStatus();
            _UData.UserCode = Session["UserCode"].ToString();
            Response = _awcBuss.DeleteHierarchy(_UData);
            return Json(Response, JsonRequestBehavior.AllowGet); ;
        }

        public ActionResult AssignStaff(string StateID, string DistrictID, string ProjectID, string SectorID, string AwcCode, string StaffId)
        {
            //ResponseStatus Response = new ResponseStatus();

            try
            {
                string UpdatedBy = Session["UserCode"].ToString();


                var Response = _awcBuss.AssignStaff(StateID, DistrictID, ProjectID, SectorID, AwcCode, Convert.ToInt32(StaffId), UpdatedBy);
                return Json(Response, JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                ResponseStatus Response = new ResponseStatus();

                Response.statusCode = "999";
                Response.message = ex.Message.ToString();
                return Json(Response, JsonRequestBehavior.AllowGet);
            }


        }

        public ActionResult GetStaffDetails(string StaffCode)
        {
            try
            {
                var Res = _awcBuss.GetStaffDetails(StaffCode);
                return Json(Res, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Content(ex.InnerException.Message);
            }

        }

        public ActionResult GetStaff(string State)
        {
            try
            {
                var Res = _loginBuss.GetStaff(State).ToList();
                return Json(Res, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Content(ex.InnerException.Message);
            }

        }


        #endregion
    }
}
