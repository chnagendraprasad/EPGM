using EPGM.Business;
using EPGM.Framework;
using EPGM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EPGM.Controllers
{
    public class UsersController : Controller
    {
        private readonly LoginBusiness _loginBuss = new LoginBusiness();
        private readonly UserBusiness _ubus = new UserBusiness();
        // GET: Users
        public ActionResult Screens(string Type = "")
        {
            try
            {
                string userType = Convert.ToString(Session["UserTypeID"]);
                if (string.IsNullOrEmpty(userType))
                    return RedirectToAction("MainIndex", "Login");

                var ResultData = Common.GetUserRights(userType, "/" + Request.RequestContext.RouteData.Values["Controller"].ToString() + "/" + Request.RequestContext.RouteData.Values["Action"].ToString());
                if (ResultData.Count != 0)
                {
                    Session["RoleType"] = Type;
                    List<SpGetRoles_Result> Roles = _loginBuss.GetRoles().ToList();
                    Roles.Insert(0, new SpGetRoles_Result { RoleCode = " ", RoleName = "--Select Role--" });
                    ViewBag.Roles = Roles.Select(m => new SelectListItem() { Text = m.RoleName.ToString(), Value = m.RoleCode.ToString() });

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

        public ActionResult GetMenus(string RoleType)
        {
            RoleType = RoleType == "" ? Session["RoleType"].ToString() : RoleType;
            List<GetMenus_Result> data = new List<GetMenus_Result>();
            data = _ubus.GetMenus(RoleType);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateRole(string MenuID, string RoleType, string rName)
        {
            var UserCode = Session["UserCode"].ToString();
            var data = _ubus.UpdateRole(MenuID, RoleType, rName, UserCode);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddMenu()
        {
            try
            {
                string userType = Convert.ToString(Session["UserTypeID"]);
                if (string.IsNullOrEmpty(userType))
                    return RedirectToAction("MainIndex", "Login");

                var ResultData = Common.GetUserRights(userType, "/" + Request.RequestContext.RouteData.Values["Controller"].ToString() + "/" + Request.RequestContext.RouteData.Values["Action"].ToString());
                if (ResultData.Count != 0)
                {

                    List<SpGetRoles_Result> Roles = _loginBuss.GetRoles().ToList();
                    //Roles.Insert(0, new SpGetRoles_Result { RoleCode = " ", RoleName = "--Select Role--" });
                    ViewBag.Roles = Roles.Select(m => new SelectListItem() { Text = m.RoleName.ToString(), Value = m.RoleCode.ToString() });

                    List<GetParentMenu_Result> Menus = _ubus.GetParentMenus().ToList();
                    Menus.Insert(0, new GetParentMenu_Result { MenuID = " ", MenuName = "Select Parent" });
                    ViewBag.Menus = Menus.Select(m => new SelectListItem() { Text = m.MenuName.ToString(), Value = m.MenuID.ToString() });

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

        [HttpPost]
        public ActionResult AddMenu(AddMenu _aMenu)
        {
            ResponseStatus Res = new ResponseStatus();
            try
            {
                if (ModelState.IsValid)
                {
                    _aMenu.UserCode = Session["UserCode"].ToString();
                    Res = _ubus.AddNewMenu(_aMenu);
                    ViewBag.Message = Res.message.ToString();

                    ModelState.Clear();

                    //List<SpGetRoles_Result> Roles = _loginBuss.GetRoles().ToList();
                    ////Roles.Insert(0, new SpGetRoles_Result { RoleCode = " ", RoleName = "--Select Role--" });
                    //ViewBag.Roles = Roles.Select(m => new SelectListItem() { Text = m.RoleName.ToString(), Value = m.RoleCode.ToString() });

                    //List<GetParentMenu_Result> Menus = _ubus.GetParentMenus().ToList();
                    //Menus.Insert(0, new GetParentMenu_Result { MenuID = "0", MenuName = "Select Parent" });
                    //ViewBag.Menus = Menus.Select(m => new SelectListItem() { Text = m.MenuName.ToString(), Value = m.MenuID.ToString() });

                }
                return Json(Res, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Content(ex.Message.ToString());
            }
        }

        public ActionResult InstructionsModule()
        {
            try
            {
                string userType = Convert.ToString(Session["UserTypeID"]);
                if (string.IsNullOrEmpty(userType))
                    return RedirectToAction("MainIndex", "Login");

                var ResultData = Common.GetUserRights(userType, "/" + Request.RequestContext.RouteData.Values["Controller"].ToString() + "/" + Request.RequestContext.RouteData.Values["Action"].ToString());
                if (ResultData.Count != 0)
                {

                    List<SpGetRoles_Result> Roles = _loginBuss.GetRoles().ToList();
                    //Roles.Insert(0, new SpGetRoles_Result { RoleCode = "", RoleName = "--Select Role--" });
                    ViewBag.Roles = Roles.Select(m => new SelectListItem() { Text = m.RoleName.ToString(), Value = m.RoleCode.ToString() });
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

        public ActionResult InstructionUpdate(string Msgid = "", string Message = "", string Role = "", string StartDate = "", string EndDate = "", string btnText = "")
        {
            var UserCode = Session["UserCode"].ToString();
            var Res = _ubus.InstructionUpdate(Msgid, Message, Role, StartDate, EndDate, UserCode, btnText);
            return Json(Res, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Notifications()
        {
            try
            {
                string userType = Convert.ToString(Session["UserTypeID"]);
                if (string.IsNullOrEmpty(userType))
                    return RedirectToAction("MainIndex", "Login");

                //var ResultData = Common.GetUserRights(userType, "/" + Request.RequestContext.RouteData.Values["Controller"].ToString() + "/" + Request.RequestContext.RouteData.Values["Action"].ToString());
                //if (ResultData.Count != 0)
                //{
                var Res = _ubus.Notifications(Session["UserTypeID"].ToString());
                ViewBag.Notifications = Res;
                return View();
                //}
                //else { return RedirectToAction("Index", "Login"); }
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                    return RedirectToAction("Index", "Login");
                else
                    return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult GetInstructions()
        {
            var Res = _ubus.GetMessagesData();
            return Json(Res, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteMessage(string msgid = "", string Message = "")
        {
            var Res = _ubus.DeleteMessage(msgid);
            return Json(Res, JsonRequestBehavior.AllowGet);
        }

        public ActionResult MobileAppMenu()
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

        public ActionResult GetMobileAppMenus(string StateCode = "", string distCode = "", string projCode = "", string secCode = "", string awcCode = "",string CenterType = "")
        {
            List<GetAppMenuData_Result> data = new List<GetAppMenuData_Result>();
            data = _ubus.GetMobileAppMenus(awcCode, CenterType);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateAppMenuRole(string StateCode = "", string distCode = "", string projCode = "", string secCode = "", string awcCode = "", string MenuID = "", string rName = "",string CenterType = "")
        {
            var UserCode = Session["UserCode"].ToString();
            var data = _ubus.UpdateMenuRole_MobileApp(awcCode, MenuID, rName, UserCode, CenterType);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}