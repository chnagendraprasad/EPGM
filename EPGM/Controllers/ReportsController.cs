using EPGM.Business;
using EPGM.Framework;
using EPGM.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EPGM.Controllers
{
    public class ReportsController : Controller
    {

        /// <summary>
        /// Business object to manipulate data
        /// </summary>
        private readonly BeneBusiness _bussBene = new BeneBusiness();
        private readonly DistrictBusiness _distBuss = new DistrictBusiness();
        private readonly LoginBusiness _loginBuss = new LoginBusiness();
        private readonly ReportsBusiness _repBuss = new ReportsBusiness();

        public ActionResult MonthlyProgressReport()
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

                    Dictionary<int, string> dDur = new Dictionary<int, string>();
                    dDur.Add(0, "--Select Month--");
                    for (int i = 1; i <= 12; i++)
                    {
                        dDur.Add(i, CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i));
                    }
                    ViewBag.Months = dDur.Select(item => new SelectListItem { Value = item.Key.ToString(), Text = item.Value });
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
                throw new Exception(ex.Message);
            }

        }

        public ActionResult ProjectWiseWeightAgeStataticsRpt()
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

                    Dictionary<int, string> dDur = new Dictionary<int, string>();
                    dDur.Add(0, "--Select Month--");
                    for (int i = 1; i <= 12; i++)
                    {
                        dDur.Add(i, CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i));
                    }
                    ViewBag.Months = dDur.Select(item => new SelectListItem { Value = item.Key.ToString(), Text = item.Value });
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
                throw new Exception(ex.Message);
            }
        }
        public ActionResult Top5PerformanceProjects()
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

                    Dictionary<int, string> dDur = new Dictionary<int, string>();
                    dDur.Add(0, "--Select Month--");
                    for (int i = 1; i <= 12; i++)
                    {
                        dDur.Add(i, CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i));
                    }
                    ViewBag.statecode = Session["UserState"].ToString();
                    ViewBag.Months = dDur.Select(item => new SelectListItem { Value = item.Key.ToString(), Text = item.Value });

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
                throw new Exception(ex.Message);
            }
        }

        public ActionResult Bottom5PerformanceProjects()
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

                    Dictionary<int, string> dDur = new Dictionary<int, string>();
                    dDur.Add(0, "--Select Month--");
                    for (int i = 1; i <= 12; i++)
                    {
                        dDur.Add(i, CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i));
                    }
                    ViewBag.statecode = Session["UserState"].ToString();
                    ViewBag.Months = dDur.Select(item => new SelectListItem { Value = item.Key.ToString(), Text = item.Value });

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
                throw new Exception(ex.Message);
            }
        }

        public ActionResult ComparisonAuditReport()
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

                    Dictionary<int, string> dDur = new Dictionary<int, string>();
                    dDur.Add(0, "--Select Month--");
                    for (int i = 1; i <= 12; i++)
                    {
                        dDur.Add(i, CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i));
                    }

                    ViewBag.statecode = Session["UserState"].ToString();
                    ViewBag.Months = dDur.Select(item => new SelectListItem { Value = item.Key.ToString(), Text = item.Value });
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
                throw new Exception(ex.Message);
            }
        }

        public ActionResult WeightStatisticsReport()
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

                    Dictionary<int, string> dDur = new Dictionary<int, string>();
                    dDur.Add(0, "--Select Month--");
                    for (int i = 1; i <= 12; i++)
                    {
                        dDur.Add(i, CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i));
                    }

                    ViewBag.statecode = Session["UserState"].ToString();
                    ViewBag.Months = dDur.Select(item => new SelectListItem { Value = item.Key.ToString(), Text = item.Value });
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
                throw new Exception(ex.Message);
            }
        }

        public ActionResult HeightStatisticsReport()
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

                    Dictionary<int, string> dDur = new Dictionary<int, string>();
                    dDur.Add(0, "--Select Month--");
                    for (int i = 1; i <= 12; i++)
                    {
                        dDur.Add(i, CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i));
                    }

                    ViewBag.statecode = Session["UserState"].ToString();
                    ViewBag.Months = dDur.Select(item => new SelectListItem { Value = item.Key.ToString(), Text = item.Value });
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
                throw new Exception(ex.Message);
            }
        }
        public ActionResult MonthlyAttendaceReport()
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

                    Dictionary<int, string> dDur = new Dictionary<int, string>();
                    dDur.Add(0, "--Select Month--");
                    for (int i = 1; i <= 12; i++)
                    {
                        dDur.Add(i, CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i));
                    }

                    ViewBag.statecode = Session["UserState"].ToString();
                    ViewBag.Months = dDur.Select(item => new SelectListItem { Value = item.Key.ToString(), Text = item.Value });
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

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ActionResult ChildGrowthCalculator()
        {
            try
            {
                string userType = Convert.ToString(Session["UserTypeID"]);
                if (string.IsNullOrEmpty(userType))
                    return RedirectToAction("MainIndex", "Login");

                return View();
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        public ActionResult GetChildGrowthGradeNames(ChildGrade _grade)
        {
            var Result = _repBuss.ChildGrowthGradeNames(_grade);
            return Json(Result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DetailedStatisticsView()
        {
            try
            {
                string userType = Convert.ToString(Session["UserTypeID"]);
                if (string.IsNullOrEmpty(userType))
                    return RedirectToAction("MainIndex", "Login");

                return View();
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        public ActionResult GetWeightAgeData(SearchFilter search, string Gender)
        {
            try
            {
                var items = _repBuss.GetMasterDataWeightAge(search, Gender);
                //if (items.Count == 0) return null;
                var total = (int)Math.Ceiling(items.Count / (float)search.rows);		//	get page count
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

        public ActionResult GetHeightWeightData(SearchFilter search, string Gender)
        {
            try
            {
                var items = _repBuss.MasterDataHeightWeight(search, Gender);
                //if (items.Count == 0) return null;
                var total = (int)Math.Ceiling(items.Count / (float)search.rows);		//	get page count
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

        public ActionResult GetHeightAgeData(SearchFilter search, string Gender)
        {
            try
            {
                var items = _repBuss.MasterDataHeightAge(search, Gender);
                //if (items.Count == 0) return null;
                var total = (int)Math.Ceiling(items.Count / (float)search.rows);		//	get page count
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

        public ActionResult BeneRegistrationsReport()
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

                    Dictionary<int, string> dDur = new Dictionary<int, string>();
                    dDur.Add(0, "--Select Month--");
                    for (int i = 1; i <= 12; i++)
                    {
                        dDur.Add(i, CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i));
                    }
                    ViewBag.Months = dDur.Select(item => new SelectListItem { Value = item.Key.ToString(), Text = item.Value });
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
                throw new Exception(ex.Message);
            }

        }

        public ActionResult HeightReduceReport()
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

                    Dictionary<int, string> dDur = new Dictionary<int, string>();
                    dDur.Add(0, "--Select Month--");
                    for (int i = 1; i <= 12; i++)
                    {
                        dDur.Add(i, CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i));
                    }
                    ViewBag.Months = dDur.Select(item => new SelectListItem { Value = item.Key.ToString(), Text = item.Value });

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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

    }
}