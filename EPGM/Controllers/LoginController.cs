using EPGM.Business;
using EPGM.Framework;
using EPGM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EPGM.Controllers
{
    public class LoginController : Controller
    {

        private readonly LoginBusiness _loginBuss = new LoginBusiness();

        [HttpPost]
        public ActionResult Mainpage(string StateCode)
        {
            Session["StateCode"] = StateCode.ToString() == null ? "" : StateCode.ToString();
            Session["State"] = StateCode.ToString() == null ? "" : StateCode.ToString();
            return Json("/login/Index", JsonRequestBehavior.AllowGet);
        }
        public ActionResult Index()
        {
            try
            {
                if (Session["StateCode"].ToString() != null && Session["StateCode"].ToString() != "")
                {
                    SPStateSpecificData_Result Data = new SPStateSpecificData_Result();
                    Data = new LoginBusiness().StateSpeicificData(Session["StateCode"].ToString());
                    Session["StateLogo"] = Data.StateLogo.ToString();
                    Session["CMImage"] = Data.CMImageURL.ToString();
                    return View();
                }
                else
                {
                    return RedirectToAction("MainIndex", "Login");
                }
            }
            catch (Exception ex)
            {
                //return Content(ex.Message);
                return RedirectToAction("MainIndex", "Login");
            }
        }

        public ActionResult GetDistricts(string StateID)
        {
            var _loginBuss = new LoginBusiness();
            var Data = _loginBuss.GetDistricts(StateID);
            return Json(Data.AsEnumerable().Select(m => new SelectListItem() { Text = m.DistrictName, Value = m.DistrictCode.ToString() }), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetProjects(string DistCd, string StateCode, string CenterType = "")
        {
            var _loginBuss = new LoginBusiness();
            var Data = _loginBuss.GetProject(DistCd, StateCode, CenterType);
            return Json(Data.AsEnumerable().Select(m => new SelectListItem() { Text = m.ProjectName, Value = m.ProjectCode.ToString() }), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSector(string Distcode, string Project, string StateCode, string CenterType = "")
        {
            var _loginBuss = new LoginBusiness();
            var Data = _loginBuss.GetSector(Distcode.Trim(), Project.Trim(), StateCode.Trim(), CenterType);
            return Json(Data.AsEnumerable().Select(m => new SelectListItem() { Text = m.SectorName, Value = m.SectorCode.ToString() }), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCenters(string Seccode, string Project, string StateCode, string CenterType = "")
        {
            var _loginBuss = new LoginBusiness();
            var Data = _loginBuss.GetCenters(Seccode.Trim(), Project.Trim(), StateCode.Trim(), CenterType);
            return Json(Data.AsEnumerable().Select(m => new SelectListItem() { Text = m.AWCName, Value = m.AWCCode.ToString() }), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetBeneType()
        {
            var _loginBuss = new LoginBusiness();
            var Data = _loginBuss.GetBeneType();
            ViewBag.BeneType = Data.AsEnumerable().Select(m => new SelectListItem() { Text = m.BeneType, Value = m.BeneTypeID.ToString() });
            return View();
        }
        public ActionResult RegisterUser(string Username, string password, string MobileNumber, string Email, string RoleID, string District, string Project, string Sector, string Center)
        {
            var _loginBuss = new LoginBusiness();
            var Data = _loginBuss.RegisterUser(Username, password, MobileNumber, Email, RoleID, District, Project, Sector, Center);
            return Json(Data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CheckValidUserName(string Name)
        {
            var _loginBuss = new LoginBusiness();
            var Data = _loginBuss.CheckValidUserName(Name);
            if (Data == null)
            {
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Login page
        /// </summary>
        /// <returns></returns>
        public ActionResult OurMission()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        /// <summary>
        /// Login page
        /// </summary>
        /// <returns></returns>
        public ActionResult UsefulLinks()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        /// <summary>
        /// Login page
        /// </summary>
        /// <returns></returns>
        public ActionResult Gallery()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        /// <summary>
        /// Login page
        /// </summary>
        /// <returns></returns>
        public ActionResult Feedback()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        /// <summary>
        /// Login page
        /// </summary>
        /// <returns></returns>
        public ActionResult ContactUs()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        public ActionResult AboutEPGM()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
        /// <summary>
        /// Login page post event: authenticate and authorize the user trying to login
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(TblUser user)
        {
            try
            {
                var _loginBuss = new LoginBusiness();
                var status = _loginBuss.ValidateUser(user.Username, user.Password, Session["StateCode"].ToString());

                string returnUrl = Request.QueryString["returnUrl"];

                if (status.StatusCode == "000")
                {
                    FormsAuthentication.SetAuthCookie(user.Username.ToString(), false);
                    Session["UserCode"] = status.UserCode;
                    Session["User"] = status;
                    Session["Username"] = status.UserName;
                    Session["Name"] = status.Name;
                    Session["UserEmail"] = status.Email;
                    Session["UserTypeID"] = status.RoleID;
                    Session["UserState"] = status.StateCode;
                    Session["UserDistrict"] = status.DistrictCode;
                    Session["UserProject"] = status.ProjectCode;
                    Session["UserSector"] = status.SectorCode;
                    Session["UserCenter"] = status.CenterCode;
                    if (user.Username == "marsadmin")
                    {
                        Session["UserCenterType"] = "0";
                    }
                    else
                    {
                        Session["UserCenterType"] = status.CenterType;
                    }


                    switch (status.RoleID)
                    {
                        case 0:
                            return RedirectToAction("EPGMIndex", "Home");
                        case 1:
                            return RedirectToAction("Index", "Home");
                        case 2:
                            return RedirectToAction("DistrictIndex", "Home");
                        case 3:
                            return RedirectToAction("DistrictIndex", "Home");
                        case 4:
                            return RedirectToAction("DistrictIndex", "Home");
                        case 5:
                            return RedirectToAction("ProjectIndex", "Home");
                        case 6:
                            return RedirectToAction("SectorIndex", "Home");
                        case 7:
                            return RedirectToAction("AWCIndex", "Home");
                            //case 8:
                            //    return RedirectToAction("Doctor", "Home");
                            //case 12:
                            //    return RedirectToAction("CallCenter", "Home");
                    }
                }
                else if (status.StatusCode == "001" || status.StatusCode == "002")
                {
                    throw new Exception(status.StatusMessage);
                }
                else
                {
                    throw new Exception("Invalid Username or Password.");
                }

                if (Url.IsLocalUrl(returnUrl) && returnUrl != "/")
                    return Redirect(returnUrl);

                return View();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message.ToString());
            }
            return View();

        }

        /// <summary>
        /// Logout page: Clears the session and redirects to login page
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            try
            {
                FormsAuthentication.SignOut();
                Session.Abandon();
                Session.RemoveAll();
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.AppendHeader("Cache-Control", "no-store");
                return RedirectToAction("MainIndex", "Login");
            }
            catch (Exception ex)
            {
                return RedirectToAction("MainIndex", "Login");
            }
        }

        public ActionResult AddUser()
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
                    List<SpGetRoles_Result> Roles = _loginBuss.GetRoles().ToList();
                    Roles.Insert(0, new SpGetRoles_Result { RoleCode = "0", RoleName = "--Select Role--" });
                    ViewBag.Roles = Roles.Select(m => new SelectListItem() { Text = m.RoleName.ToString(), Value = m.RoleCode.ToString() });

                    List<SpGetStates_Result> States = _loginBuss.GetStates().ToList();
                    States.Insert(0, new SpGetStates_Result { StateCode = "0", StateName = "--Select State--" });
                    ViewBag.States = States.Select(m => new SelectListItem() { Text = m.StateName.ToString(), Value = m.StateCode.ToString() });

                    //Get Center Types
                    List<SPGetCentersType_Result> CenterTypes = _loginBuss.GetCenterTypes(UserCenterType).ToList();
                    ViewBag.CenterType = CenterTypes.Select(m => new SelectListItem() { Text = m.CenterType.ToString(), Value = m.CenterTypeID.ToString() });

                    return View();
                }
                else { return RedirectToAction("Index", "Login"); }
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                    return Content(ex.Message.ToString());
                else
                    return Content(ex.InnerException.Message.ToString());
            }
        }

        public ActionResult CheckUser(string UserName)
        {
            try
            {
                string userType = Convert.ToString(Session["UserTypeID"]);
                if (string.IsNullOrEmpty(userType))
                    return RedirectToAction("MainIndex", "Login");

                var Data = _loginBuss.CheckUser(UserName);
                return Json(Data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult CheckUserFF(string UserName)
        {
            try
            {
                string userType = Convert.ToString(Session["UserTypeID"]);
                if (string.IsNullOrEmpty(userType))
                    return RedirectToAction("MainIndex", "Login");

                var Data = _loginBuss.CheckUserFF(UserName);
                return Json(Data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult AddUser(NewUser _Newuser)
        {
            SpInsertLoginUser_Result Data = new SpInsertLoginUser_Result();
            try
            {
                if (ModelState.IsValid)
                {
                    _Newuser.Password = "Admin";
                    _Newuser.UserCode = Session["UserCode"].ToString();
                    var _loginBuss = new LoginBusiness();
                    Data = _loginBuss.InsertNewUser(_Newuser);
                    return Json(Data, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    Data = new SpInsertLoginUser_Result() { Code = "001", Message = "Please fill All the fields" };
                    return Json(Data, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                Data = new SpInsertLoginUser_Result() { Code = "001", Message = ex.Message.ToString() };
                return Json(Data, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult View1()
        {
            var Password = Helper.Encrypt("Admin");
            return View();
        }

        public ActionResult ChangePassword()
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
                return Content(ex.Message.ToString());
            }
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePassword _Change)
        {
            SPChangePassword_Result Data = new SPChangePassword_Result();
            try
            {
                if (ModelState.IsValid)
                {
                    {
                        _Change.UserCode = Session["UserCode"].ToString();
                        var _loginBuss = new LoginBusiness();
                        Data = _loginBuss.ChangePassword(_Change);
                        return Json(Data, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    Data = new SPChangePassword_Result() { Code = "001", Message = "Please fill All the fields" };
                    return Json(Data, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                Data = new SPChangePassword_Result() { Code = "001", Message = ex.Message.ToString() };
                return Json(Data, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ForgotPassword()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return Content(ex.Message.ToString());
            }
        }

        [HttpPost]
        public ActionResult ForgotPassword(ForgotPassword _Forgot)
        {
            SPUpdatePassword_Result Data = new SPUpdatePassword_Result();
            try
            {
                if (ModelState.IsValid)
                {
                    var _loginBuss = new LoginBusiness();
                    _Forgot.Password = "Admin";
                    Data = _loginBuss.ForgotPassword(_Forgot);
                    if (Data.Code == "000")
                    {
                        var body = _loginBuss.PasswordEmail(_Forgot.Password, _Forgot.Username);
                        NotificationService.Instance.SendMail(_Forgot.Useremail, "Password Recovery/Change Password", body);
                    }
                    return Json(Data, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    Data = new SPUpdatePassword_Result() { Code = "001", Message = "Please fill All the fields" };
                    return Json(Data, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                Data = new SPUpdatePassword_Result() { Code = "001", Message = ex.Message.ToString() };
                return Json(Data, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetUserMail(string Username)
        {
            var _loginBuss = new LoginBusiness();
            var Data = _loginBuss.GetUserMail(Username);
            if (Data == null)
            {
                Data = "";
            }
            return Json(Data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult MainIndex()
        {
            List<SpGetStates_Result> States = _loginBuss.GetStates().ToList();
            States.Insert(0, new SpGetStates_Result { StateCode = "0", StateName = "--Select State--" });
            ViewBag.States = States.Select(m => new SelectListItem() { Text = m.StateName.ToString(), Value = m.StateCode.ToString() });
            return View();
        }

        public ActionResult CheckLogin(string Username, string Password, string StateCode)
        {
            var status = _loginBuss.CheckUserLogin(Username, Password, StateCode);
            return Json(status, JsonRequestBehavior.AllowGet);
        }
    }
}
