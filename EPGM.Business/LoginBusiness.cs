using EPGM.Data;
using EPGM.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace EPGM.Business
{
    public class LoginBusiness
    {
        private readonly LoginData _loginData = new LoginData();

        public LoginUserDetails ValidateUser(string Username, string Password,string StateCode)
        {
            return _loginData.ValidateUser(Username, Password, StateCode);
        }

        public List<SpGetDistrict_Result> GetDistricts(string StateID)
        {
            return _loginData.GetDistricts(StateID); ;
        }
        public List<USP_S_CATEGORY_Result> GetCategorys()
        {
            return _loginData.GetCategorys();
        }

        public List<EPGM.Models.SpGetProject_Result> GetProject(string DistCd, string StateCode,string CenterType = "")
        {
            return _loginData.GetProject(DistCd, StateCode, CenterType);
        }

        public List<EPGM.Models.SpGetSector_Result> GetSector(string Distcode, string Project, string StateCode,string CenterType = "")
        {
            return _loginData.GetSector(Distcode, Project, StateCode, CenterType);
        }

        public List<EPGM.Models.SpGetAWC_Result> GetCenters(string SecCode, string Project, string StateCode, string CenterType = "")
        {
            return _loginData.GetCenters(SecCode, Project, StateCode, CenterType);
        }

        public List<EPGM.Models.SPGetBeneType_Result> GetBeneType()
        {
            return _loginData.GetBeneType();
        }

        public EPGM.Models.CheckValidUser_Result CheckValidUserName(string Name)
        {
            return _loginData.CheckValidUserName(Name);
        }

        public List<EPGM.Models.SpGetRoles_Result> GetRoles()
        {
            return _loginData.GetRoles();
        }

        public int CheckUser(string UserName)
        {
            return _loginData.CheckUser(UserName);
        }

        public int CheckUserFF(string UserName)
        {
            return _loginData.CheckUserFF(UserName);
        }

        public List<EPGM.Models.SpGetStates_Result> GetStates()
        {
            return _loginData.GetStates();
        }
        public List<EPGM.Models.SPGetCentersType_Result> GetCenterTypes(string UserCenterType)
        {
            return _loginData.GetCenterTypes(UserCenterType);
        }

        public List<EPGM.Models.SPGetStaffFF_Result> GetStaff(string StateCode)
        {
            return _loginData.GetStaff(StateCode);
        }

        public String RegisterUser(string Username, string password, string MobileNumber, string Email, string RoleID, string District, string Project, string Sector, string Center)
        {
            return _loginData.RegisterUser(Username, password, MobileNumber, Email, RoleID, District, Project, Sector, Center);
        }
        public SpInsertLoginUser_Result InsertNewUser(NewUser _Newuser)
        {
            return _loginData.InsertNewUser(_Newuser);
        }
        public SPChangePassword_Result ChangePassword(ChangePassword _Change)
        {
            return _loginData.ChangePassword(_Change);
        }

        public string GetUserMail(string Username)
        {
            return _loginData.GetUserMail(Username);
        }
        public SPUpdatePassword_Result ForgotPassword(ForgotPassword _Forgot)
        {
            return _loginData.ForgotPassword(_Forgot);
        }

        public string PasswordEmail(string Password, string Name)
        {
            string body = string.Empty;
            try
            {
                using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Templates/PasswordEmail.html")))
                {
                    body = reader.ReadToEnd();
                }
                body = body.Replace("{password}", Password);
                body = body.Replace("{Name}", Name);

                return body;
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();

            }
        }

        public SPStateSpecificData_Result StateSpeicificData(string Statecode)
        {
            return _loginData.StateSpeicificData(Statecode);
        }
        public LoginUserDetails CheckUserLogin(string Username, string Password,string StateCode)
        {
            return _loginData.CheckUserLogin(Username, Password, StateCode);
        }
    }
}
