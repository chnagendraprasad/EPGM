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
    public class HomeController : Controller
    {
        private readonly HomeBusiness _homeBuss = new HomeBusiness();
        private readonly LoginBusiness _loginBuss = new LoginBusiness();

        public ActionResult EPGMIndex()
        {
            string userType = Convert.ToString(Session["UserTypeID"]);
            string UserCenterType = Convert.ToString(Session["UserCenterType"]);
            if (string.IsNullOrEmpty(userType))
                return RedirectToAction("MainIndex", "Login");

            var ResultData = Common.GetUserRights(userType, "/" + Request.RequestContext.RouteData.Values["Controller"].ToString() + "/" + Request.RequestContext.RouteData.Values["Action"].ToString());
            if (ResultData.Count != 0)
            {
                List<SpGetStates_Result> States = _loginBuss.GetStates().ToList();
                //States.Insert(0, new SpGetStates_Result { StateCode = "0", StateName = "--Select State--" });
                ViewBag.States = States.Select(m => new SelectListItem() { Text = m.StateName.ToString(), Value = m.StateCode.ToString() });
                var Data = _loginBuss.GetBeneType();
                ViewBag.BeneType = Data.AsEnumerable().Select(m => new SelectListItem() { Text = m.BeneType, Value = m.BeneTypeID.ToString() });
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

        public ActionResult Index()
        {
            string userType = Convert.ToString(Session["UserTypeID"]);
            if (string.IsNullOrEmpty(userType))
                return RedirectToAction("MainIndex", "Login");

            var ResultData = Common.GetUserRights(userType, "/" + Request.RequestContext.RouteData.Values["Controller"].ToString() + "/" + Request.RequestContext.RouteData.Values["Action"].ToString());
            if (ResultData.Count != 0)
            {
                var Data = _loginBuss.GetBeneType();
                ViewBag.BeneType = Data.AsEnumerable().Select(m => new SelectListItem() { Text = m.BeneType, Value = m.BeneTypeID.ToString() });

                Dictionary<int, string> dDur = new Dictionary<int, string>();
                dDur.Add(0, "--Select Month--");
                for (int i = 1; i <= 12; i++)
                {
                    dDur.Add(i, CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i));
                }
                ViewBag.Months = dDur.Select(item => new SelectListItem { Value = item.Key.ToString(), Text = item.Value });
                //var data = GetStats();
                //ViewBag.Total = data.Count;
                //ViewBag.TotalCenters = data.CentersCount;
                //ViewBag.AttendanceCount = data.AttendanceCount;
                //ViewBag.BeneCount = data.BeneCount;
                //ViewBag.Absent = data.AbsentCount == "" ? 0 : Convert.ToInt32(data.AbsentCount);
                //double abs = data.AbsentCount == "" ? 0 : Convert.ToInt32(data.AbsentCount);
                ////double ben = data.BeneCount == "" ? 0 : Convert.ToInt32(data.BeneCount);
                //double ben = Convert.ToInt32(data.WANormalCount == "" ? 0 : Convert.ToInt32(data.WANormalCount)) +
                //             Convert.ToInt32(data.WAModerateCount == "" ? 0 : Convert.ToInt32(data.WAModerateCount)) +
                //             Convert.ToInt32(data.WASevereCount == "" ? 0 : Convert.ToInt32(data.WASevereCount));
                //double ase = (abs / ben) * 100;
                //ViewBag.AbsPer = ((abs / ben) * 100).ToString("f1");


                //ViewBag.Normal = data.WANormalCount == "" ? 0 : Convert.ToInt32(data.WANormalCount);
                //ViewBag.Moderate = data.WAModerateCount == "" ? 0 : Convert.ToInt32(data.WAModerateCount);
                //ViewBag.Severe = data.WASevereCount == "" ? 0 : Convert.ToInt32(data.WASevereCount);
                //double wanor = data.WANormalCount == "" ? 0 : Convert.ToInt32(data.WANormalCount);
                //double wamuw = data.WAModerateCount == "" ? 0 : Convert.ToInt32(data.WAModerateCount);
                //double wasuw = data.WASevereCount == "" ? 0 : Convert.ToInt32(data.WASevereCount);
                //ViewBag.WANorPer = Math.Round(((wanor / ben) * 100), 1).ToString() == "NaN" ? "0" : Math.Round(((wanor / ben) * 100), 1).ToString();//.ToString("f2");
                //ViewBag.WAModPer = Math.Round(((wamuw / ben) * 100), 1).ToString() == "NaN" ? "0" : Math.Round(((wamuw / ben) * 100), 1).ToString();//.ToString("f2");
                //ViewBag.WASevPer = Math.Round(((wasuw / ben) * 100), 1).ToString() == "NaN" ? "0" : Math.Round(((wasuw / ben) * 100), 1).ToString();//.ToString("f2");

                //ViewBag.HWNor = data.HWNormalCount;
                //ViewBag.HWMod = data.HWModerateCount;
                //ViewBag.HWSev = data.HWSevereCount;
                //double hwnor = data.HWNormalCount == "" ? 0 : Convert.ToInt32(data.HWNormalCount);
                //double hwmuw = data.HWModerateCount == "" ? 0 : Convert.ToInt32(data.HWModerateCount);
                //double hwsuw = data.HWSevereCount == "" ? 0 : Convert.ToInt32(data.HWSevereCount);
                //ViewBag.HWNorPer = Math.Round(((hwnor / ben) * 100), 1).ToString() == "NaN" ? "0" : Math.Round(((hwnor / ben) * 100), 1).ToString();//.ToString("f2");
                //ViewBag.HWModPer = Math.Round(((hwmuw / ben) * 100), 1).ToString() == "NaN" ? "0" : Math.Round(((hwmuw / ben) * 100), 1).ToString();//.ToString("f2");
                //ViewBag.HWSevPer = Math.Round(((hwsuw / ben) * 100), 1).ToString() == "NaN" ? "0" : Math.Round(((hwsuw / ben) * 100), 1).ToString();//.ToString("f2");

                //ViewBag.HANor = data.HANormalCount;
                //ViewBag.HAMod = data.HAModerateCount;
                //ViewBag.HASev = data.HASevereCount;
                //double hanor = data.HANormalCount == "" ? 0 : Convert.ToInt32(data.HANormalCount);
                //double hamuw = data.HAModerateCount == "" ? 0 : Convert.ToInt32(data.HAModerateCount);
                //double hasuw = data.HASevereCount == "" ? 0 : Convert.ToInt32(data.HASevereCount);
                //ViewBag.HANorPer = Math.Round(((hanor / ben) * 100), 1).ToString() == "NaN" ? "0" : Math.Round(((hanor / ben) * 100), 1).ToString();//.ToString("f2");
                //ViewBag.HAModPer = Math.Round(((hamuw / ben) * 100), 1).ToString() == "NaN" ? "0" : Math.Round(((hamuw / ben) * 100), 1).ToString();//.ToString("f2");
                //ViewBag.HASevPer = Math.Round(((hasuw / ben) * 100), 1).ToString() == "NaN" ? "0" : Math.Round(((hasuw / ben) * 100), 1).ToString();//.ToString("f2");
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult DistrictIndex()
        {
            string userType = Convert.ToString(Session["UserTypeID"]);
            if (string.IsNullOrEmpty(userType))
                return RedirectToAction("MainIndex", "Login");

            var ResultData = Common.GetUserRights(userType, "/" + Request.RequestContext.RouteData.Values["Controller"].ToString() + "/" + Request.RequestContext.RouteData.Values["Action"].ToString());
            if (ResultData.Count != 0)
            {
                var Data = _loginBuss.GetBeneType();
                ViewBag.BeneType = Data.AsEnumerable().Select(m => new SelectListItem() { Text = m.BeneType, Value = m.BeneTypeID.ToString() });

                Dictionary<int, string> dDur = new Dictionary<int, string>();
                dDur.Add(0, "--Select Month--");
                for (int i = 1; i <= 12; i++)
                {
                    dDur.Add(i, CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i));
                }
                ViewBag.Months = dDur.Select(item => new SelectListItem { Value = item.Key.ToString(), Text = item.Value });
                //var data = GetStats();
                //ViewBag.Total = data.Count;
                //ViewBag.TotalCenters = data.CentersCount;
                //ViewBag.AttendanceCount = data.AttendanceCount;
                //ViewBag.BeneCount = data.BeneCount;
                //ViewBag.Absent = data.AbsentCount == "" ? 0 : Convert.ToInt32(data.AbsentCount);
                //double abs = data.AbsentCount == "" ? 0 : Convert.ToInt32(data.AbsentCount);
                ////double ben = data.BeneCount == "" ? 0 : Convert.ToInt32(data.BeneCount);
                //double ben = Convert.ToInt32(data.WANormalCount == "" ? 0 : Convert.ToInt32(data.WANormalCount)) +
                //             Convert.ToInt32(data.WAModerateCount == "" ? 0 : Convert.ToInt32(data.WAModerateCount)) +
                //             Convert.ToInt32(data.WASevereCount == "" ? 0 : Convert.ToInt32(data.WASevereCount));
                //double ase = (abs / ben) * 100;
                //ViewBag.AbsPer = ((abs / ben) * 100).ToString("f1");

                //ViewBag.Normal = data.WANormalCount == "" ? 0 : Convert.ToInt32(data.WANormalCount);
                //ViewBag.Moderate = data.WAModerateCount == "" ? 0 : Convert.ToInt32(data.WAModerateCount);
                //ViewBag.Severe = data.WASevereCount == "" ? 0 : Convert.ToInt32(data.WASevereCount);
                //double wanor = data.WANormalCount == "" ? 0 : Convert.ToInt32(data.WANormalCount);
                //double wamuw = data.WAModerateCount == "" ? 0 : Convert.ToInt32(data.WAModerateCount);
                //double wasuw = data.WASevereCount == "" ? 0 : Convert.ToInt32(data.WASevereCount);
                //ViewBag.WANorPer = Math.Round(((wanor / ben) * 100), 1).ToString() == "NaN" ? "0" : Math.Round(((wanor / ben) * 100), 1).ToString();//.ToString("f2");
                //ViewBag.WAModPer = Math.Round(((wamuw / ben) * 100), 1).ToString() == "NaN" ? "0" : Math.Round(((wamuw / ben) * 100), 1).ToString();//.ToString("f2");
                //ViewBag.WASevPer = Math.Round(((wasuw / ben) * 100), 1).ToString() == "NaN" ? "0" : Math.Round(((wasuw / ben) * 100), 1).ToString();//.ToString("f2");

                //ViewBag.HWNor = data.HWNormalCount;
                //ViewBag.HWMod = data.HWModerateCount;
                //ViewBag.HWSev = data.HWSevereCount;
                //double hwnor = data.HWNormalCount == "" ? 0 : Convert.ToInt32(data.HWNormalCount);
                //double hwmuw = data.HWModerateCount == "" ? 0 : Convert.ToInt32(data.HWModerateCount);
                //double hwsuw = data.HWSevereCount == "" ? 0 : Convert.ToInt32(data.HWSevereCount);
                //ViewBag.HWNorPer = Math.Round(((hwnor / ben) * 100), 1).ToString() == "NaN" ? "0" : Math.Round(((hwnor / ben) * 100), 1).ToString();//.ToString("f2");
                //ViewBag.HWModPer = Math.Round(((hwmuw / ben) * 100), 1).ToString() == "NaN" ? "0" : Math.Round(((hwmuw / ben) * 100), 1).ToString();//.ToString("f2");
                //ViewBag.HWSevPer = Math.Round(((hwsuw / ben) * 100), 1).ToString() == "NaN" ? "0" : Math.Round(((hwsuw / ben) * 100), 1).ToString();//.ToString("f2");

                //ViewBag.HANor = data.HANormalCount;
                //ViewBag.HAMod = data.HAModerateCount;
                //ViewBag.HASev = data.HASevereCount;
                //double hanor = data.HANormalCount == "" ? 0 : Convert.ToInt32(data.HANormalCount);
                //double hamuw = data.HAModerateCount == "" ? 0 : Convert.ToInt32(data.HAModerateCount);
                //double hasuw = data.HASevereCount == "" ? 0 : Convert.ToInt32(data.HASevereCount);
                //ViewBag.HANorPer = Math.Round(((hanor / ben) * 100), 1).ToString() == "NaN" ? "0" : Math.Round(((hanor / ben) * 100), 1).ToString();//.ToString("f2");
                //ViewBag.HAModPer = Math.Round(((hamuw / ben) * 100), 1).ToString() == "NaN" ? "0" : Math.Round(((hamuw / ben) * 100), 1).ToString();//.ToString("f2");
                //ViewBag.HASevPer = Math.Round(((hasuw / ben) * 100), 1).ToString() == "NaN" ? "0" : Math.Round(((hasuw / ben) * 100), 1).ToString();//.ToString("f2");
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult ProjectIndex()
        {
            string userType = Convert.ToString(Session["UserTypeID"]);
            if (string.IsNullOrEmpty(userType) || userType != "5")
                return RedirectToAction("MainIndex", "Login");

            var ResultData = Common.GetUserRights(userType, "/" + Request.RequestContext.RouteData.Values["Controller"].ToString() + "/" + Request.RequestContext.RouteData.Values["Action"].ToString());
            if (ResultData.Count != 0)
            {
                var Data = _loginBuss.GetBeneType();
                ViewBag.BeneType = Data.AsEnumerable().Select(m => new SelectListItem() { Text = m.BeneType, Value = m.BeneTypeID.ToString() });

                Dictionary<int, string> dDur = new Dictionary<int, string>();
                dDur.Add(0, "--Select Month--");
                for (int i = 1; i <= 12; i++)
                {
                    dDur.Add(i, CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i));
                }
                ViewBag.Months = dDur.Select(item => new SelectListItem { Value = item.Key.ToString(), Text = item.Value });
                //var data = GetStats();
                //ViewBag.Total = data.Count;
                //ViewBag.TotalCenters = data.CentersCount;
                //ViewBag.AttendanceCount = data.AttendanceCount;
                //ViewBag.BeneCount = data.BeneCount;
                //ViewBag.Absent = data.AbsentCount == "" ? 0 : Convert.ToInt32(data.AbsentCount);
                //double abs = data.AbsentCount == "" ? 0 : Convert.ToInt32(data.AbsentCount);
                ////double ben = data.BeneCount == "" ? 0 : Convert.ToInt32(data.BeneCount);
                //double ben = Convert.ToInt32(data.WANormalCount == "" ? 0 : Convert.ToInt32(data.WANormalCount)) +
                //             Convert.ToInt32(data.WAModerateCount == "" ? 0 : Convert.ToInt32(data.WAModerateCount)) +
                //             Convert.ToInt32(data.WASevereCount == "" ? 0 : Convert.ToInt32(data.WASevereCount));
                //double ase = (abs / ben) * 100;
                //ViewBag.AbsPer = ((abs / ben) * 100).ToString("f1");

                //ViewBag.Normal = data.WANormalCount == "" ? 0 : Convert.ToInt32(data.WANormalCount);
                //ViewBag.Moderate = data.WAModerateCount == "" ? 0 : Convert.ToInt32(data.WAModerateCount);
                //ViewBag.Severe = data.WASevereCount == "" ? 0 : Convert.ToInt32(data.WASevereCount);
                //double wanor = data.WANormalCount == "" ? 0 : Convert.ToInt32(data.WANormalCount);
                //double wamuw = data.WAModerateCount == "" ? 0 : Convert.ToInt32(data.WAModerateCount);
                //double wasuw = data.WASevereCount == "" ? 0 : Convert.ToInt32(data.WASevereCount);
                //ViewBag.WANorPer = Math.Round(((wanor / ben) * 100), 1).ToString() == "NaN" ? "0" : Math.Round(((wanor / ben) * 100), 1).ToString();//.ToString("f2");
                //ViewBag.WAModPer = Math.Round(((wamuw / ben) * 100), 1).ToString() == "NaN" ? "0" : Math.Round(((wamuw / ben) * 100), 1).ToString();//.ToString("f2");
                //ViewBag.WASevPer = Math.Round(((wasuw / ben) * 100), 1).ToString() == "NaN" ? "0" : Math.Round(((wasuw / ben) * 100), 1).ToString();//.ToString("f2");

                //ViewBag.HWNor = data.HWNormalCount;
                //ViewBag.HWMod = data.HWModerateCount;
                //ViewBag.HWSev = data.HWSevereCount;
                //double hwnor = data.HWNormalCount == "" ? 0 : Convert.ToInt32(data.HWNormalCount);
                //double hwmuw = data.HWModerateCount == "" ? 0 : Convert.ToInt32(data.HWModerateCount);
                //double hwsuw = data.HWSevereCount == "" ? 0 : Convert.ToInt32(data.HWSevereCount);
                //ViewBag.HWNorPer = Math.Round(((hwnor / ben) * 100), 1).ToString() == "NaN" ? "0" : Math.Round(((hwnor / ben) * 100), 1).ToString();//.ToString("f2");
                //ViewBag.HWModPer = Math.Round(((hwmuw / ben) * 100), 1).ToString() == "NaN" ? "0" : Math.Round(((hwmuw / ben) * 100), 1).ToString();//.ToString("f2");
                //ViewBag.HWSevPer = Math.Round(((hwsuw / ben) * 100), 1).ToString() == "NaN" ? "0" : Math.Round(((hwsuw / ben) * 100), 1).ToString();//.ToString("f2");

                //ViewBag.HANor = data.HANormalCount;
                //ViewBag.HAMod = data.HAModerateCount;
                //ViewBag.HASev = data.HASevereCount;
                //double hanor = data.HANormalCount == "" ? 0 : Convert.ToInt32(data.HANormalCount);
                //double hamuw = data.HAModerateCount == "" ? 0 : Convert.ToInt32(data.HAModerateCount);
                //double hasuw = data.HASevereCount == "" ? 0 : Convert.ToInt32(data.HASevereCount);
                //ViewBag.HANorPer = Math.Round(((hanor / ben) * 100), 1).ToString() == "NaN" ? "0" : Math.Round(((hanor / ben) * 100), 1).ToString();//.ToString("f2");
                //ViewBag.HAModPer = Math.Round(((hamuw / ben) * 100), 1).ToString() == "NaN" ? "0" : Math.Round(((hamuw / ben) * 100), 1).ToString();//.ToString("f2");
                //ViewBag.HASevPer = Math.Round(((hasuw / ben) * 100), 1).ToString() == "NaN" ? "0" : Math.Round(((hasuw / ben) * 100), 1).ToString();//.ToString("f2");
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult SectorIndex()
        {
            string userType = Convert.ToString(Session["UserTypeID"]);
            if (string.IsNullOrEmpty(userType) || userType != "6")
                return RedirectToAction("MainIndex", "Login");

            var ResultData = Common.GetUserRights(userType, "/" + Request.RequestContext.RouteData.Values["Controller"].ToString() + "/" + Request.RequestContext.RouteData.Values["Action"].ToString());
            if (ResultData.Count != 0)
            {
                var Data = _loginBuss.GetBeneType();
                ViewBag.BeneType = Data.AsEnumerable().Select(m => new SelectListItem() { Text = m.BeneType, Value = m.BeneTypeID.ToString() });

                Dictionary<int, string> dDur = new Dictionary<int, string>();
                dDur.Add(0, "--Select Month--");
                for (int i = 1; i <= 12; i++)
                {
                    dDur.Add(i, CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i));
                }
                ViewBag.Months = dDur.Select(item => new SelectListItem { Value = item.Key.ToString(), Text = item.Value });
                //var data = GetStats();
                //ViewBag.Total = data.Count;
                //ViewBag.TotalCenters = data.CentersCount;
                //ViewBag.AttendanceCount = data.AttendanceCount;
                //ViewBag.BeneCount = data.BeneCount;
                //ViewBag.Absent = data.AbsentCount == "" ? 0 : Convert.ToInt32(data.AbsentCount);
                //double abs = data.AbsentCount == "" ? 0 : Convert.ToInt32(data.AbsentCount);
                ////double ben = data.BeneCount == "" ? 0 : Convert.ToInt32(data.BeneCount);
                //double ben = Convert.ToInt32(data.WANormalCount == "" ? 0 : Convert.ToInt32(data.WANormalCount)) +
                //             Convert.ToInt32(data.WAModerateCount == "" ? 0 : Convert.ToInt32(data.WAModerateCount)) +
                //             Convert.ToInt32(data.WASevereCount == "" ? 0 : Convert.ToInt32(data.WASevereCount));
                //double ase = (abs / ben) * 100;
                //ViewBag.AbsPer = ((abs / ben) * 100).ToString("f1");

                //ViewBag.Normal = data.WANormalCount == "" ? 0 : Convert.ToInt32(data.WANormalCount);
                //ViewBag.Moderate = data.WAModerateCount == "" ? 0 : Convert.ToInt32(data.WAModerateCount);
                //ViewBag.Severe = data.WASevereCount == "" ? 0 : Convert.ToInt32(data.WASevereCount);
                //double wanor = data.WANormalCount == "" ? 0 : Convert.ToInt32(data.WANormalCount);
                //double wamuw = data.WAModerateCount == "" ? 0 : Convert.ToInt32(data.WAModerateCount);
                //double wasuw = data.WASevereCount == "" ? 0 : Convert.ToInt32(data.WASevereCount);
                //ViewBag.WANorPer = Math.Round(((wanor / ben) * 100), 1).ToString() == "NaN" ? "0" : Math.Round(((wanor / ben) * 100), 1).ToString();//.ToString("f2");
                //ViewBag.WAModPer = Math.Round(((wamuw / ben) * 100), 1).ToString() == "NaN" ? "0" : Math.Round(((wamuw / ben) * 100), 1).ToString();//.ToString("f2");
                //ViewBag.WASevPer = Math.Round(((wasuw / ben) * 100), 1).ToString() == "NaN" ? "0" : Math.Round(((wasuw / ben) * 100), 1).ToString();//.ToString("f2");

                //ViewBag.HWNor = data.HWNormalCount;
                //ViewBag.HWMod = data.HWModerateCount;
                //ViewBag.HWSev = data.HWSevereCount;
                //double hwnor = data.HWNormalCount == "" ? 0 : Convert.ToInt32(data.HWNormalCount);
                //double hwmuw = data.HWModerateCount == "" ? 0 : Convert.ToInt32(data.HWModerateCount);
                //double hwsuw = data.HWSevereCount == "" ? 0 : Convert.ToInt32(data.HWSevereCount);
                //ViewBag.HWNorPer = Math.Round(((hwnor / ben) * 100), 1).ToString() == "NaN" ? "0" : Math.Round(((hwnor / ben) * 100), 1).ToString();//.ToString("f2");
                //ViewBag.HWModPer = Math.Round(((hwmuw / ben) * 100), 1).ToString() == "NaN" ? "0" : Math.Round(((hwmuw / ben) * 100), 1).ToString();//.ToString("f2");
                //ViewBag.HWSevPer = Math.Round(((hwsuw / ben) * 100), 1).ToString() == "NaN" ? "0" : Math.Round(((hwsuw / ben) * 100), 1).ToString();//.ToString("f2");

                //ViewBag.HANor = data.HANormalCount;
                //ViewBag.HAMod = data.HAModerateCount;
                //ViewBag.HASev = data.HASevereCount;
                //double hanor = data.HANormalCount == "" ? 0 : Convert.ToInt32(data.HANormalCount);
                //double hamuw = data.HAModerateCount == "" ? 0 : Convert.ToInt32(data.HAModerateCount);
                //double hasuw = data.HASevereCount == "" ? 0 : Convert.ToInt32(data.HASevereCount);
                //ViewBag.HANorPer = Math.Round(((hanor / ben) * 100), 1).ToString() == "NaN" ? "0" : Math.Round(((hanor / ben) * 100), 1).ToString();//.ToString("f2");
                //ViewBag.HAModPer = Math.Round(((hamuw / ben) * 100), 1).ToString() == "NaN" ? "0" : Math.Round(((hamuw / ben) * 100), 1).ToString();//.ToString("f2");
                //ViewBag.HASevPer = Math.Round(((hasuw / ben) * 100), 1).ToString() == "NaN" ? "0" : Math.Round(((hasuw / ben) * 100), 1).ToString();//.ToString("f2");
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult AWCIndex()
        {
            string userType = Convert.ToString(Session["UserTypeID"]);
            if (string.IsNullOrEmpty(userType) || userType != "7")
                return RedirectToAction("MainIndex", "Login");


            var ResultData = Common.GetUserRights(userType, "/" + Request.RequestContext.RouteData.Values["Controller"].ToString() + "/" + Request.RequestContext.RouteData.Values["Action"].ToString());
            if (ResultData.Count != 0)
            {
                var Data = _loginBuss.GetBeneType();
                ViewBag.BeneType = Data.AsEnumerable().Select(m => new SelectListItem() { Text = m.BeneType, Value = m.BeneTypeID.ToString() });

                Dictionary<int, string> dDur = new Dictionary<int, string>();
                dDur.Add(0, "--Select Month--");
                for (int i = 1; i <= 12; i++)
                {
                    dDur.Add(i, CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i));
                }
                ViewBag.Months = dDur.Select(item => new SelectListItem { Value = item.Key.ToString(), Text = item.Value });
                //var data = GetStats();
                //ViewBag.Total = data.Count;
                //ViewBag.TotalCenters = data.CentersCount;
                //ViewBag.AttendanceCount = data.AttendanceCount;
                //ViewBag.BeneCount = data.BeneCount;
                //ViewBag.Absent = data.AbsentCount == "" ? 0 : Convert.ToInt32(data.AbsentCount);
                //double abs = data.AbsentCount == "" ? 0 : Convert.ToInt32(data.AbsentCount);
                ////double ben = data.BeneCount == "" ? 0 : Convert.ToInt32(data.BeneCount);
                //double ben = Convert.ToInt32(data.WANormalCount == "" ? 0 : Convert.ToInt32(data.WANormalCount)) +
                //             Convert.ToInt32(data.WAModerateCount == "" ? 0 : Convert.ToInt32(data.WAModerateCount)) +
                //             Convert.ToInt32(data.WASevereCount == "" ? 0 : Convert.ToInt32(data.WASevereCount));
                //double ase = (abs / ben) * 100;
                //ViewBag.AbsPer = ((abs / ben) * 100).ToString("f1");

                //ViewBag.Normal = data.WANormalCount == "" ? 0 : Convert.ToInt32(data.WANormalCount);
                //ViewBag.Moderate = data.WAModerateCount == "" ? 0 : Convert.ToInt32(data.WAModerateCount);
                //ViewBag.Severe = data.WASevereCount == "" ? 0 : Convert.ToInt32(data.WASevereCount);
                //double wanor = data.WANormalCount == "" ? 0 : Convert.ToInt32(data.WANormalCount);
                //double wamuw = data.WAModerateCount == "" ? 0 : Convert.ToInt32(data.WAModerateCount);
                //double wasuw = data.WASevereCount == "" ? 0 : Convert.ToInt32(data.WASevereCount);
                //ViewBag.WANorPer = Math.Round(((wanor / ben) * 100), 1).ToString() == "NaN" ? "0" : Math.Round(((wanor / ben) * 100), 1).ToString();//.ToString("f2");
                //ViewBag.WAModPer = Math.Round(((wamuw / ben) * 100), 1).ToString() == "NaN" ? "0" : Math.Round(((wamuw / ben) * 100), 1).ToString();//.ToString("f2");
                //ViewBag.WASevPer = Math.Round(((wasuw / ben) * 100), 1).ToString() == "NaN" ? "0" : Math.Round(((wasuw / ben) * 100), 1).ToString();//.ToString("f2");

                //ViewBag.HWNor = data.HWNormalCount;
                //ViewBag.HWMod = data.HWModerateCount;
                //ViewBag.HWSev = data.HWSevereCount;
                //double hwnor = data.HWNormalCount == "" ? 0 : Convert.ToInt32(data.HWNormalCount);
                //double hwmuw = data.HWModerateCount == "" ? 0 : Convert.ToInt32(data.HWModerateCount);
                //double hwsuw = data.HWSevereCount == "" ? 0 : Convert.ToInt32(data.HWSevereCount);
                //ViewBag.HWNorPer = Math.Round(((hwnor / ben) * 100), 1).ToString() == "NaN" ? "0" : Math.Round(((hwnor / ben) * 100), 1).ToString();//.ToString("f2");
                //ViewBag.HWModPer = Math.Round(((hwmuw / ben) * 100), 1).ToString() == "NaN" ? "0" : Math.Round(((hwmuw / ben) * 100), 1).ToString();//.ToString("f2");
                //ViewBag.HWSevPer = Math.Round(((hwsuw / ben) * 100), 1).ToString() == "NaN" ? "0" : Math.Round(((hwsuw / ben) * 100), 1).ToString();//.ToString("f2");

                //ViewBag.HANor = data.HANormalCount;
                //ViewBag.HAMod = data.HAModerateCount;
                //ViewBag.HASev = data.HASevereCount;
                //double hanor = data.HANormalCount == "" ? 0 : Convert.ToInt32(data.HANormalCount);
                //double hamuw = data.HAModerateCount == "" ? 0 : Convert.ToInt32(data.HAModerateCount);
                //double hasuw = data.HASevereCount == "" ? 0 : Convert.ToInt32(data.HASevereCount);
                //ViewBag.HANorPer = Math.Round(((hanor / ben) * 100), 1).ToString() == "NaN" ? "0" : Math.Round(((hanor / ben) * 100), 1).ToString();//.ToString("f2");
                //ViewBag.HAModPer = Math.Round(((hamuw / ben) * 100), 1).ToString() == "NaN" ? "0" : Math.Round(((hamuw / ben) * 100), 1).ToString();//.ToString("f2");
                //ViewBag.HASevPer = Math.Round(((hasuw / ben) * 100), 1).ToString() == "NaN" ? "0" : Math.Round(((hasuw / ben) * 100), 1).ToString();//.ToString("f2");
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult Report(string distCode, string projCode = "", string secCode = "", string awcCode = "",string CenterType = "")
        {

            string UserCenterType = Convert.ToString(Session["UserCenterType"]);
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
                return RedirectToAction("MainIndex", "Login");

            var _loginBuss = new LoginBusiness();

            List<SpGetStates_Result> States = _loginBuss.GetStates().ToList();
            States.Insert(0, new SpGetStates_Result { StateCode = "0", StateName = "--Select State--" });
            ViewBag.States = States.Select(m => new SelectListItem() { Text = m.StateName.ToString(), Value = m.StateCode.ToString() });


            //Get Center Types
            List<SPGetCentersType_Result> CenterTypes = _loginBuss.GetCenterTypes(UserCenterType).ToList();
            ViewBag.CenterType = CenterTypes.Select(m => new SelectListItem() { Text = m.CenterType.ToString(), Value = m.CenterTypeID.ToString() });

            if(UserCenterType == "0")
            {
                Session["centertype"] = CenterType == "" ? " " : CenterType;
            }
            else
            {
                Session["centertype"] = UserCenterType;
            }

            

            Session["statecode"] = Session["UserState"].ToString();
            Session["distcode"] = distCode == "" ? " " : distCode;
            Session["projcode"] = projCode == "" ? " " : projCode;
            Session["seccode"] = secCode == "" ? " " : secCode;
            Session["awccode"] = awcCode == "" ? " " : awcCode;
            return View();
        }

        public ActionResult GetProjects(string DistCd, string StateCode)
        {
            var _loginBuss = new LoginBusiness();
            var Data = _loginBuss.GetProject(DistCd, StateCode);
            return Json(Data.AsEnumerable().Select(m => new SelectListItem() { Text = m.ProjectName, Value = m.ProjectCode.ToString() }), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSector(string Distcode, string Project, string StateCode)
        {
            var _loginBuss = new LoginBusiness();
            var Data = _loginBuss.GetSector(Distcode.Trim(), Project.Trim(), StateCode.Trim());
            return Json(Data.AsEnumerable().Select(m => new SelectListItem() { Text = m.SectorName, Value = m.SectorCode.ToString() }), JsonRequestBehavior.AllowGet);
        }

        public ActionResult AdminIndex()
        {
            string userType = Convert.ToString(Session["UserTypeID"]);
            if (string.IsNullOrEmpty(userType))
                return RedirectToAction("MainIndex", "Login");

            var data = GetStats();
            var dashStats = GetCounts();
            ViewBag.Total = data.Count;
            ViewBag.Normal = data.WANormalCount;
            ViewBag.Moderate = data.WAModerateCount;
            ViewBag.Severe = data.WASevereCount;
            ViewBag.Absent = data.AbsentCount;
            ViewBag.BeneCount = data.BeneCount;
            ViewBag.DistCount = dashStats.Dists;
            ViewBag.ProjCount = dashStats.Projs;
            ViewBag.SecCount = dashStats.Sectors;
            ViewBag.AWCs = dashStats.AWCs;

            double abs = data.AbsentCount == "" ? 0 : Convert.ToInt32(data.AbsentCount);
            double ben = data.BeneCount == "" ? 0 : Convert.ToInt32(data.BeneCount);

            double wanor = data.WANormalCount == "" ? 0 : Convert.ToInt32(data.WANormalCount);
            double wamuw = data.WAModerateCount == "" ? 0 : Convert.ToInt32(data.WAModerateCount);
            double wasuw = data.WASevereCount == "" ? 0 : Convert.ToInt32(data.WASevereCount);

            double hwnor = data.WANormalCount == "" ? 0 : Convert.ToInt32(data.WANormalCount);
            double hwmuw = data.WAModerateCount == "" ? 0 : Convert.ToInt32(data.WAModerateCount);
            double hwsuw = data.WASevereCount == "" ? 0 : Convert.ToInt32(data.WASevereCount);

            double hanor = data.WANormalCount == "" ? 0 : Convert.ToInt32(data.WANormalCount);
            double hamuw = data.WAModerateCount == "" ? 0 : Convert.ToInt32(data.WAModerateCount);
            double hasuw = data.WASevereCount == "" ? 0 : Convert.ToInt32(data.WASevereCount);

            double ase = (abs / ben) * 100;

            ViewBag.AbsPer = ((abs / ben) * 100).ToString("f1");
            ViewBag.NorPer = ((wanor / ben) * 100).ToString("f1");
            ViewBag.ModPer = ((wamuw / ben) * 100).ToString("f1");
            ViewBag.SevPer = ((wasuw / ben) * 100).ToString("f1");
            return View();
        }

        public ActionResult AdminGridData(SearchFilter search)
        {
            int RecCount = 0;
            var StateCode = Session["UserCode"].ToString();
            var items = _homeBuss.GetAdminData(search, StateCode);
            if (items.Count == 0) return null;
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

        public ActionResult CallCenter()
        {
            return View();
        }

        public ActionResult Doctor()
        {
            return View();
        }

        public ActionResult GetDistrictDetails(string BeneType = "", string Month = "", string Year = "")
        {
            string userType = Convert.ToString(Session["UserTypeID"]);
            if (string.IsNullOrEmpty(userType))
                return RedirectToAction("MainIndex", "Login");

            int RecCount = 0;
            var search = new SearchFilter();
            string stateCode = Convert.ToString(Session["UserState"]);
            string distCode = Convert.ToString(Session["UserDistrict"]);
            string projCode = Convert.ToString(Session["UserProject"]);
            string secCode = Convert.ToString(Session["UserSector"]);
            string awcCode = Convert.ToString(Session["UserCenter"]);

           
            string  CenterType = Session["UserCenterType"].ToString();
            

            var items = _homeBuss.GetDistrictStats(stateCode, distCode, projCode, secCode, awcCode, BeneType, Month,Year, CenterType);
            if (items.Count == 0) return null;
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

        public DashboardData GetStats()
        {
            string stateCode = Convert.ToString(Session["UserState"]);
            string distCode = Convert.ToString(Session["UserDistrict"]);
            string projCode = Convert.ToString(Session["UserProject"]);
            string secCode = Convert.ToString(Session["UserSector"]);
            string awcCode = Convert.ToString(Session["UserCenter"]);
            return _homeBuss.GetStats(stateCode, distCode, projCode, secCode, awcCode, "1", "04", "2017","");
        }

        public ActionResult GetDashStats(string BeneType = "", string StateCode = "", string Month = "", string Year = "",string CenterType="")
        {
            string userType = Convert.ToString(Session["UserTypeID"]);
            if (string.IsNullOrEmpty(userType))
                return RedirectToAction("MainIndex", "Login");

            var StateCode1 = "";
            if (StateCode != "" && StateCode != null)
            {
                StateCode1 = StateCode;
            }
            else
            {
                StateCode1 = Session["UserState"].ToString();
            }

            if (Session["Username"].ToString() != "marsadmin")
            {
                CenterType = Session["UserCenterType"].ToString();
            }


            string stateCode = Convert.ToString(StateCode1);
            string distCode = Convert.ToString(Session["UserDistrict"]);
            string projCode = Convert.ToString(Session["UserProject"]);
            string secCode = Convert.ToString(Session["UserSector"]);
            string awcCode = Convert.ToString(Session["UserCenter"]);
            AdminDashboardData Result = new Models.AdminDashboardData();
            DashboardData data = new DashboardData();

            data = _homeBuss.GetStats(stateCode, distCode, projCode, secCode, awcCode, BeneType, Month, Year, CenterType);

            Result.BeneCount = data.BeneCount;
            Result.TotalCenters = data.CentersCount;
            Result.Count = data.Count;
            Result.WANormalCount = data.WANormalCount == "" ? "0" : data.WANormalCount;
            Result.WAModerateCount = data.WAModerateCount == "" ? "0" : data.WAModerateCount;
            Result.WASevereCount = data.WASevereCount == "" ? "0" : data.WASevereCount;
            Result.AbsentCount = data.AbsentCount == "" ? "0" : data.AbsentCount;

            double abs = data.AbsentCount == "" ? 0 : Convert.ToInt32(data.AbsentCount);
            double ben = Convert.ToInt32(data.WANormalCount == "" ? 0 : Convert.ToInt32(data.WANormalCount)) +
                           Convert.ToInt32(data.WAModerateCount == "" ? 0 : Convert.ToInt32(data.WAModerateCount)) +
                           Convert.ToInt32(data.WASevereCount == "" ? 0 : Convert.ToInt32(data.WASevereCount));
            double ase = (abs / ben) * 100;
            ViewBag.AbsPer = ((abs / ben) * 100).ToString("f1");

            Result.Normal = data.WANormalCount == "" ? "0" : data.WANormalCount;
            Result.Moderate = data.WAModerateCount == "" ? "0" : data.WAModerateCount;
            Result.Severe = data.WASevereCount == "" ? "0" : data.WASevereCount;
            double wanor = data.WANormalCount == "" ? 0 : Convert.ToInt32(data.WANormalCount);
            double wamuw = data.WAModerateCount == "" ? 0 : Convert.ToInt32(data.WAModerateCount);
            double wasuw = data.WASevereCount == "" ? 0 : Convert.ToInt32(data.WASevereCount);
            Result.WANorPer = Math.Round(((wanor / ben) * 100), 1).ToString() == "NaN" ? "0" : Math.Round(((wanor / ben) * 100), 1).ToString();//.ToString("f2");
            Result.WAModPer = Math.Round(((wamuw / ben) * 100), 1).ToString() == "NaN" ? "0" : Math.Round(((wamuw / ben) * 100), 1).ToString();//.ToString("f2");
            Result.WASevPer = Math.Round(((wasuw / ben) * 100), 1).ToString() == "NaN" ? "0" : Math.Round(((wasuw / ben) * 100), 1).ToString();//.ToString("f2");

            Result.HWNor = data.HWNormalCount == "" ? "0" : data.HWNormalCount;
            Result.HWMod = data.HWModerateCount == "" ? "0" : data.HWModerateCount;
            Result.HWSev = data.HWSevereCount == "" ? "0" : data.HWSevereCount;
            double hwnor = data.HWNormalCount == "" ? 0 : Convert.ToInt32(data.HWNormalCount);
            double hwmuw = data.HWModerateCount == "" ? 0 : Convert.ToInt32(data.HWModerateCount);
            double hwsuw = data.HWSevereCount == "" ? 0 : Convert.ToInt32(data.HWSevereCount);
            Result.HWNorPer = Math.Round(((hwnor / ben) * 100), 1).ToString() == "NaN" ? "0" : Math.Round(((hwnor / ben) * 100), 1).ToString();//.ToString("f2");
            Result.HWModPer = Math.Round(((hwmuw / ben) * 100), 1).ToString() == "NaN" ? "0" : Math.Round(((hwmuw / ben) * 100), 1).ToString();//.ToString("f2");
            Result.HWSevPer = Math.Round(((hwsuw / ben) * 100), 1).ToString() == "NaN" ? "0" : Math.Round(((hwsuw / ben) * 100), 1).ToString();//.ToString("f2");

            Result.HANor = data.HANormalCount == "" ? "0" : data.HANormalCount;
            Result.HAMod = data.HAModerateCount == "" ? "0" : data.HAModerateCount;
            Result.HASev = data.HASevereCount == "" ? "0" : data.HASevereCount;
            double hanor = data.HANormalCount == "" ? 0 : Convert.ToInt32(data.HANormalCount);
            double hamuw = data.HAModerateCount == "" ? 0 : Convert.ToInt32(data.HAModerateCount);
            double hasuw = data.HASevereCount == "" ? 0 : Convert.ToInt32(data.HASevereCount);
            Result.HANorPer = Math.Round(((hanor / ben) * 100), 1).ToString() == "NaN" ? "0" : Math.Round(((hanor / ben) * 100), 1).ToString();//.ToString("f2");
            Result.HAModPer = Math.Round(((hamuw / ben) * 100), 1).ToString() == "NaN" ? "0" : Math.Round(((hamuw / ben) * 100), 1).ToString();//.ToString("f2");
            Result.HASevPer = Math.Round(((hasuw / ben) * 100), 1).ToString() == "NaN" ? "0" : Math.Round(((hasuw / ben) * 100), 1).ToString();//.ToString("f2");

            Result.TotalChildren = data.TotalChildren.ToString();
            Result.PregnantWomen = data.PregnantWomen.ToString();
            Result.LacatingWomen = data.LacatingWomen.ToString();
            Result.Child0to3 = data.Child0to3.ToString();
            Result.Child3to6 = data.Child3to6.ToString();
            Result.Others = data.Others.ToString();

            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        public SpGetCounts_Result GetCounts()
        {
            string stateCode = Convert.ToString(Session["UserState"]);
            return _homeBuss.GetCounts(stateCode);
        }

        public ActionResult GetDoughnutResult()
        {
            var jsonData = GetStats();
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        #region for EPGM Admin

        public ActionResult GetAdminDistrictDetails(string StateCode, string BeneType, string Month = "", string Year = "",string CenterType = "")
        {
            string userType = Convert.ToString(Session["UserTypeID"]);
            if (string.IsNullOrEmpty(userType))
                return RedirectToAction("MainIndex", "Login");

            int RecCount = 0;
            var search = new SearchFilter();
            string stateCode = Convert.ToString(StateCode);
            string distCode = "";
            string projCode = "";
            string secCode = "";
            string awcCode = "";

            if(Session["Username"].ToString() != "marsadmin")
            {
                CenterType = Session["UserCenterType"].ToString();
            }
            var items = _homeBuss.GetDistrictStats(stateCode, distCode, projCode, secCode, awcCode, BeneType, Month,Year, CenterType);
            if (items.Count == 0) return null;
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
        public ActionResult GetDataStats(string StateCode, string BeneType, string Month = "", string Year = "")
        {
            string stateCode = Convert.ToString(StateCode);
            string distCode = "";
            string projCode = "";
            string secCode = "";
            string awcCode = "";
            AdminDashboardData Result = new Models.AdminDashboardData();
            DashboardData data = new DashboardData();
            data = _homeBuss.GetStats(stateCode, distCode, projCode, secCode, awcCode, BeneType, Month, Year,"");
            double abs = data.AbsentCount == "" ? 0 : Convert.ToInt32(data.AbsentCount);
            Result.BeneCount = data.BeneCount;
            Result.Count = data.Count;
            Result.WANormalCount = data.WANormalCount;
            Result.WAModerateCount = data.WAModerateCount;
            Result.WASevereCount = data.WASevereCount;
            Result.AbsentCount = data.AbsentCount;

            double ben = Convert.ToInt32(data.WANormalCount == "" ? 0 : Convert.ToInt32(data.WANormalCount)) +
                            Convert.ToInt32(data.WAModerateCount == "" ? 0 : Convert.ToInt32(data.WAModerateCount)) +
                            Convert.ToInt32(data.WASevereCount == "" ? 0 : Convert.ToInt32(data.WASevereCount));
            double ase = (abs / ben) * 100;
            ViewBag.AbsPer = ((abs / ben) * 100).ToString("f1");

            Result.Normal = data.WANormalCount;
            Result.Moderate = data.WAModerateCount;
            Result.Severe = data.WASevereCount;
            double wanor = data.WANormalCount == "" ? 0 : Convert.ToInt32(data.WANormalCount);
            double wamuw = data.WAModerateCount == "" ? 0 : Convert.ToInt32(data.WAModerateCount);
            double wasuw = data.WASevereCount == "" ? 0 : Convert.ToInt32(data.WASevereCount);
            Result.WANorPer = Math.Round(((wanor / ben) * 100), 1).ToString() == "NaN" ? "0" : Math.Round(((wanor / ben) * 100), 1).ToString();//.ToString("f2");
            Result.WAModPer = Math.Round(((wamuw / ben) * 100), 1).ToString() == "NaN" ? "0" : Math.Round(((wamuw / ben) * 100), 1).ToString();//.ToString("f2");
            Result.WASevPer = Math.Round(((wasuw / ben) * 100), 1).ToString() == "NaN" ? "0" : Math.Round(((wasuw / ben) * 100), 1).ToString();//.ToString("f2");

            Result.HWNor = data.HWNormalCount;
            Result.HWMod = data.HWModerateCount;
            Result.HWSev = data.HWSevereCount;
            double hwnor = data.HWNormalCount == "" ? 0 : Convert.ToInt32(data.HWNormalCount);
            double hwmuw = data.HWModerateCount == "" ? 0 : Convert.ToInt32(data.HWModerateCount);
            double hwsuw = data.HWSevereCount == "" ? 0 : Convert.ToInt32(data.HWSevereCount);
            Result.HWNorPer = Math.Round(((hwnor / ben) * 100), 1).ToString() == "NaN" ? "0" : Math.Round(((hwnor / ben) * 100), 1).ToString();//.ToString("f2");
            Result.HWModPer = Math.Round(((hwmuw / ben) * 100), 1).ToString() == "NaN" ? "0" : Math.Round(((hwmuw / ben) * 100), 1).ToString();//.ToString("f2");
            Result.HWSevPer = Math.Round(((hwsuw / ben) * 100), 1).ToString() == "NaN" ? "0" : Math.Round(((hwsuw / ben) * 100), 1).ToString();//.ToString("f2");

            Result.HANor = data.HANormalCount;
            Result.HAMod = data.HAModerateCount;
            Result.HASev = data.HASevereCount;
            double hanor = data.HANormalCount == "" ? 0 : Convert.ToInt32(data.HANormalCount);
            double hamuw = data.HAModerateCount == "" ? 0 : Convert.ToInt32(data.HAModerateCount);
            double hasuw = data.HASevereCount == "" ? 0 : Convert.ToInt32(data.HASevereCount);
            Result.HANorPer = Math.Round(((hanor / ben) * 100), 1).ToString() == "NaN" ? "0" : Math.Round(((hanor / ben) * 100), 1).ToString();//.ToString("f2");
            Result.HAModPer = Math.Round(((hamuw / ben) * 100), 1).ToString() == "NaN" ? "0" : Math.Round(((hamuw / ben) * 100), 1).ToString();//.ToString("f2");
            Result.HASevPer = Math.Round(((hasuw / ben) * 100), 1).ToString() == "NaN" ? "0" : Math.Round(((hasuw / ben) * 100), 1).ToString();//.ToString("f2");

            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReportAdmin(string statecode = "", string distCode = "", string projCode = "", string secCode = "", string awcCode = "",string CenterType="")
        {

            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
                return RedirectToAction("MainIndex", "Login");

            var _loginBuss = new LoginBusiness();


            string UserCenterType = Convert.ToString(Session["UserCenterType"]);

            List<SpGetStates_Result> States = _loginBuss.GetStates().ToList();
            States.Insert(0, new SpGetStates_Result { StateCode = "0", StateName = "--Select State--" });
            ViewBag.States = States.Select(m => new SelectListItem() { Text = m.StateName.ToString(), Value = m.StateCode.ToString() });

            //Get Center Types
            List<SPGetCentersType_Result> CenterTypes = _loginBuss.GetCenterTypes(UserCenterType).ToList();
            ViewBag.CenterType = CenterTypes.Select(m => new SelectListItem() { Text = m.CenterType.ToString(), Value = m.CenterTypeID.ToString() });

            Session["centertype"] = CenterType == "" ? " " : CenterType;
            Session["statecode"] = statecode == "" ? " " : statecode;
            Session["distcode"] = distCode == "" ? " " : distCode;
            Session["projcode"] = projCode == "" ? " " : projCode;
            Session["seccode"] = secCode == "" ? " " : secCode;
            Session["awccode"] = awcCode == "" ? " " : awcCode;
            return View("Report");
        }

        #endregion


    }
}
