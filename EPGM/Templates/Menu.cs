using EPGM.Business;
using EPGM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPGM.Templates
{
    public class Menu
    {
        private readonly HomeBusiness _homebuss = new HomeBusiness();
        private readonly LoginBusiness _loginBuss = new LoginBusiness();

        public static List<SPBindMenuByUserType_Result> BindMenu(string UserTypeID)
        {
            List<SPBindMenuByUserType_Result> Data = new List<SPBindMenuByUserType_Result>();
            Data = new HomeBusiness().BindMenu(UserTypeID);
            return Data;
        }

        public static SPStateSpecificData_Result StateSpecificData(string StateCode)
        {
            SPStateSpecificData_Result Data = new SPStateSpecificData_Result();
            Data = new LoginBusiness().StateSpeicificData(StateCode);
            return Data;
        }
    }
}