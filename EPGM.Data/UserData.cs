using EPGM.Framework;
using EPGM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPGM.Data
{
    public class UserRoleData
    {
        public List<GetMenus_Result> GetMenus(string UserType)
        {
            using (var db = new DBEntities())
            {
                var Data = db.GetMenus(UserType).ToList();
                return Data;
            }

        }

        public ResponseStatus UpdateRole(string MenuID, string UserType, string rName, string UserCode)
        {
            using (var db = new DBEntities())
            {
                var enabled = "";
                if (rName.Contains("N"))
                {
                    enabled = "0";
                }
                else
                {
                    enabled = "1";
                }
                ResponseStatus Data = new ResponseStatus();
                var Res = db.UpdateMenurole(enabled, MenuID, UserType, UserCode).FirstOrDefault();
                Data.code = Res.Code;
                Data.message = Res.Message;
                return Data;
            }

        }

        public List<GetParentMenu_Result> GetParentMenus()
        {
            try
            {
                using (var db = new DBEntities())
                {
                    var Data = db.GetParentMenu();
                    return Data.ToList();
                }
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }

        public ResponseStatus AddNewMenu(AddMenu _Amenu)
        {
            var Res = new ResponseStatus { code = "999", message = "Unable to Connect to Server" };
            try
            {
                SPUpdateUserrightMaster_Result userRight = new SPUpdateUserrightMaster_Result();

                using (var db = new DBEntities())
                {
                    if (_Amenu.MenuType == "P")
                    {
                        _Amenu.Parent = "0";
                    }
                    var Data = db.SPADDMENU(_Amenu.MenuName, _Amenu.URL, Convert.ToInt32(_Amenu.Parent), _Amenu.UserCode, Helper.GetIPAddress()).FirstOrDefault();
                    if (Data.Code == "000")
                    {
                        string[] roledate = _Amenu.Role.Split(',');
                        for (int i = 1; i < roledate.Count(); i++)
                        {
                            var utype = roledate[i];
                            userRight = db.SPUpdateUserrightMaster(Convert.ToInt32(Data.MenuID), Convert.ToInt32(utype), _Amenu.UserCode, Helper.GetIPAddress()).FirstOrDefault();
                        }
                        if (userRight.Code == "000")
                        {
                            Res.code = userRight.Code;
                            Res.message = userRight.Message;
                            return Res;
                        }
                        else
                        {
                            Res.code = userRight.Code;
                            Res.message = userRight.Message;
                            return Res;
                        }
                    }
                    else
                    {
                        Res.code = Data.Code;
                        Res.message = Data.Message;
                    }
                }
                return Res;
            }
            catch (Exception ex)
            {
                Res.code = "999";
                if (ex.InnerException == null)
                {
                    Res.message = ex.Message.ToString();
                }
                else
                {
                    Res.message = ex.InnerException.ToString();
                }
                return Res;
            }
        }

        public ResponseStatus InstructionUpdate(string Msgid = "", string Message = "", string Role = "", string StartDate = "", string EndDate = "", string UserCode = "", string btnText = "")
        {
            var Res = new ResponseStatus { code = "999", message = "Unable to Connect to Server" };
            try
            {

                using (var db = new DBEntities())
                {
                    if (btnText == "Submit")
                    {
                        //var Data = db.SPInsertInstructionMsg(Message, Role.TrimStart(','), Convert.ToDateTime(StartDate, System.Globalization.CultureInfo.InvariantCulture),
                        //  Convert.ToDateTime(EndDate, System.Globalization.CultureInfo.InvariantCulture), UserCode, Helper.GetIPAddress()).FirstOrDefault();
                        var Data = db.SPInsertInstructionMsg(Message, Role.TrimStart(','), DateTime.ParseExact(StartDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture),
                          DateTime.ParseExact(EndDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture), UserCode, Helper.GetIPAddress()).FirstOrDefault();                        
                        Res.code = Data.Code;
                        Res.message = Data.Message;
                        return Res;
                    }
                    else
                    {
                       // var Data = db.SPUpdateInstructionMsg(Msgid, Message, Role.TrimStart(','), Convert.ToDateTime(StartDate, System.Globalization.CultureInfo.InvariantCulture),
                        //                            Convert.ToDateTime(EndDate, System.Globalization.CultureInfo.InvariantCulture), UserCode, Helper.GetIPAddress()).FirstOrDefault();
                        var Data = db.SPUpdateInstructionMsg(Msgid, Message, Role.TrimStart(','), DateTime.ParseExact(StartDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture),
                          DateTime.ParseExact(EndDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture), UserCode, Helper.GetIPAddress()).FirstOrDefault();
                        Res.code = Data.Code;
                        Res.message = Data.Message;
                        return Res;
                    }
                }
            }
            catch (Exception ex)
            {
                Res.code = "999";
                if (ex.InnerException == null)
                {
                    Res.message = ex.Message.ToString();
                }
                else
                {
                    Res.message = ex.InnerException.ToString();
                }
                return Res;
            }
        }

        public List<SPGetMessages_Result> GetMessagesMenu(string UserType)
        {
            using (var context = new DBEntities())
            {
                var Data = context.SPGetMessages(UserType).ToList();
                return Data;
            }
        }


        public List<SPMessagesData_Result> GetMessagesData()
        {
            using (var db = new DBEntities())
            {
                var Res = db.SPMessagesData().ToList();
                return Res;
            }
        }

        public DeleteMessage_Result DeleteMessage(string msgID)
        {
            using (var db = new DBEntities())
            {
                var Res = db.DeleteMessage(msgID).FirstOrDefault();
                return Res;
            }
        }
        public List<GetAppMenuData_Result> GetMobileAppMenus(string awcCode = "",string CenterType = "")
        {
            try
            {
                using (var db = new DBEntities())
                {
                    var Data = db.GetAppMenuData(1, awcCode,Convert.ToInt16(CenterType)).ToList();
                    return Data.ToList();
                }
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }

        public ResponseStatus UpdateMenuRole_MobileApp(string awcCode = "", string MenuID = "", string rName = "", string UserCode = "",string CenterType = "")
        {
            using (var db = new DBEntities())
            {
                var enabled = "";
                if (rName.Contains("N"))
                {
                    enabled = "0";
                }
                else
                {
                    enabled = "1";
                }
                ResponseStatus Data = new ResponseStatus();
                var Res = db.UpdateMenurole_MobileApp(enabled, MenuID, awcCode, UserCode,Convert.ToInt16(CenterType)).FirstOrDefault();
                Data.code = Res.Code;
                Data.message = Res.Message;
                return Data;
            }

        }
    }
}
