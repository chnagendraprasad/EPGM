using EPGM.Data;
using EPGM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPGM.Business
{
    public class UserBusiness
    {
        private readonly UserRoleData _userdata = new UserRoleData();
        public List<GetMenus_Result> GetMenus(string UserType)
        {
            return _userdata.GetMenus(UserType);

        }

        public ResponseStatus UpdateRole(string MenuID, string UserType, string rName,string UserCode)
        {
            return _userdata.UpdateRole(MenuID, UserType, rName, UserCode);

        }

        public List<GetParentMenu_Result> GetParentMenus()
        {
            return _userdata.GetParentMenus();
        }
        public ResponseStatus AddNewMenu(AddMenu _Amenu)
        {
            return _userdata.AddNewMenu(_Amenu);
        }

        public ResponseStatus InstructionUpdate(string Msgid = "", string Message = "", string Role = "", string StartDate = "", string EndDate = "", string UserCode = "", string btnText = "")
        {
            return _userdata.InstructionUpdate(Msgid, Message, Role, StartDate, EndDate, UserCode,btnText);
        }
        public List<SPGetMessages_Result> Notifications(string UserTypeID)
        {
            return _userdata.GetMessagesMenu(UserTypeID);
        }

        public List<SPMessagesData_Result> GetMessagesData()
        {
            return _userdata.GetMessagesData();
        }
        public DeleteMessage_Result DeleteMessage(string msgID)
        {
            return _userdata.DeleteMessage(msgID);
        }
        public List<GetAppMenuData_Result> GetMobileAppMenus(string awcCode="",string CenterType = "")
        {
            return _userdata.GetMobileAppMenus(awcCode, CenterType);
        }

        public ResponseStatus UpdateMenuRole_MobileApp(string awcCode = "", string MenuID = "",string rName="",string UserCode="",string CenterType = "")
        {
            return _userdata.UpdateMenuRole_MobileApp(awcCode, MenuID, rName, UserCode, CenterType);

        }
    }
}
