using EPGM.Framework;
using EPGM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPGM.Data
{
    public class LoginData
    {
        public LoginUserDetails ValidateUser(string Username, string Password, string StateCode)
        {
            using (var db = new DBEntities())
            {
                var user = new LoginUserDetails() { StatusCode = "123", StatusMessage = "Unable to connect to Server." };
                var EncPassword = Helper.Encrypt(Password);
                Username = Username.ToLower();
                var userDetails = (from x in db.TblPortalUsers where x.Username.ToLower() == Username select x).FirstOrDefault();
                if (userDetails != null)
                {
                    if (Username == "marsadmin")
                    {
                        userDetails.StateCode = "";
                        StateCode = "";
                    }
                    if (userDetails.StateCode == StateCode)
                    {
                        if (userDetails.Password == EncPassword)
                        {
                            user.UserName = userDetails.Username;
                            user.Name = userDetails.Name;
                            user.Mobile = userDetails.MobileNo;
                            user.UserCode = userDetails.UserCode;
                            user.Email = userDetails.UserEmail;
                            user.RoleID = Convert.ToInt32(userDetails.RoleCode);
                            user.StateCode = userDetails.StateCode;
                            user.DistrictCode = userDetails.DistrictCode;
                            user.ProjectCode = userDetails.ProjectCode;
                            user.SectorCode = userDetails.SectorCode;
                            user.CenterCode = userDetails.AWCCode;
                            user.CenterType = userDetails.CenterTypeID;
                            user.StatusCode = "000";
                            user.StatusMessage = "Login Successful.";

                            return user;
                        }
                        else
                        {
                            return user;
                        }
                    }
                    else
                    {
                        user.StatusCode = "002";
                        user.StatusMessage = "Invalid State Selection ";
                    }
                }

                else
                {
                    user.StatusCode = "001";
                    user.StatusMessage = "Username is not Valid";

                }
                return user;
            }
        }

        public LoginUserDetails AssignDetails(string type, dynamic assignedObj)
        {
            var data = new LoginUserDetails();
            data.Name = assignedObj.Username;
            data.RoleID = assignedObj.UserTypeID;
            data.Mobile = assignedObj.MobileNo;
            data.PortalUserID = assignedObj.PortalUserID;
            data.StateCode = Helper.GetSetting("StateCode");
            data.District = assignedObj.District;
            data.DistrictCode = assignedObj.DistrictCode;
            data.ProjectName = assignedObj.ProjectName;
            data.SectorName = assignedObj.SectorName;
            data.ProjectCode = assignedObj.ProjectCode;
            return data;
        }


        //public TblPortalUser ValidateUser(string Username, string Password)
        //{
        //    using (var db = new DBEntities())
        //    {
        //        var EncPassword = Helper.Encrypt(Password);
        //        var users = (from x in db.TblPortalUsers where x.UserTypeID == 4 || x.UserTypeID == 1 select x).ToList();
        //        foreach (var use in users)
        //        {
        //            var old = db.TblPortalUsers.First(m => m.PortalUserID == use.PortalUserID);     //  get object to update
        //            var newpass = Helper.Encrypt(old.Username);
        //            old.Password = newpass;
        //            db.SaveChanges();
        //        }
        //        Username = Username.ToLower();  //where x.Username.ToLower() == Username.ToLower()
        //        var user = (from x in db.TblPortalUsers where x.Username.ToLower() == Username && x.Password == EncPassword select x).FirstOrDefault();
        //        if (user == null)
        //            throw new CustomException("User does not exist.");
        //        return user;
        //    }
        //}

        //user.ContCode = userDetails.UserCode;
        //user.Name = userDetails.Username;
        //user.UserTypeID = userDetails.UserTypeID;
        //user.MobileNo = userDetails.MobileNo;
        //user.PortalUserID = userDetails.PortalUserID;
        //user.StateCode = Helper.GetSetting("StateCode");
        //user.District = "14";
        //user.ProjectName = "";
        //user.SectorName = "";


        public List<SpGetDistrict_Result> GetDistricts(string StateID)
        {
            try
            {
                using (var db = new DBEntities())
                {
                    var Data = db.SpGetDistrict(StateID);
                    return Data.ToList();
                }
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }

        public List<USP_S_CATEGORY_Result> GetCategorys()
        {
            try
            {
                using (var db = new DBEntities())
                {
                    var Data = db.USP_S_CATEGORY();
                    return Data.ToList();
                }
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }

        public List<EPGM.Models.SpGetProject_Result> GetProject(string DistCd, string StateCode,string CenterType = "")
        {
            try
            {
                using (var db = new DBEntities())
                {
                    var Data = db.SpGetProject(DistCd, StateCode,Convert.ToInt16(CenterType));
                    return Data.ToList();
                }
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }

        public List<SpGetSector_Result> GetSector(string Distcode, string Project, string StateCode,string CenterType ="")
        {
            try
            {
                using (var db = new DBEntities())
                {
                    var Data = db.SpGetSector(StateCode, Distcode, Project, Convert.ToInt16(CenterType));
                    return Data.ToList();
                }
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }

        public List<SpGetAWC_Result> GetCenters(string SecCode, string Project, string StateCode, string CenterType = "")
        {
            try
            {
                using (var db = new DBEntities())
                {
                    var Data = db.SpGetAWC(SecCode.Trim(), Project.Trim(), StateCode.Trim(), Convert.ToInt16(CenterType));
                    return Data.ToList();
                }
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }

        public List<SPGetBeneType_Result> GetBeneType()
        {
            try
            {
                using (var db = new DBEntities())
                {
                    var Data = db.SPGetBeneType().ToList();
                    return Data.ToList();
                }
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }

        public CheckValidUser_Result CheckValidUserName(string Name)
        {
            try
            {
                using (var db = new DBEntities())
                {
                    var Data = db.CheckValidUser(Name).FirstOrDefault();
                    return Data;
                }
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }

        public List<SpGetRoles_Result> GetRoles()
        {
            try
            {
                using (var db = new DBEntities())
                {
                    var Data = db.SpGetRoles(0);
                    return Data.ToList();
                }
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }

        public List<SpGetStates_Result> GetStates()
        {
            try
            {
                using (var db = new DBEntities())
                {
                    var Data = db.SpGetStates();
                    return Data.ToList();
                }
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }

        public List<SPGetCentersType_Result> GetCenterTypes(string UserCenterType)
        {
            try
            {
                int value = 0;
                if(UserCenterType != "0")
                {
                    value = Convert.ToInt16(UserCenterType);
                }
                using (var db = new DBEntities())
                {
                    var Data = db.SPGetCentersType(value);
                    return Data.ToList();
                }
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }

        public List<SPGetStaffFF_Result> GetStaff(string StateCode)
        {
            try
            {
                using (var db = new DBEntities())
                {
                    var Data = db.SPGetStaffFF(StateCode);
                    return Data.ToList();
                }
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }

        public int CheckUser(string UserName)
        {
            try
            {
                using (var db = new DBEntities())
                {
                    var Data = db.SpCheckUser(UserName).FirstOrDefault();
                    return Convert.ToInt32(Data);
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int CheckUserFF(string UserName)
        {
            try
            {
                using (var db = new DBEntities())
                {
                    var Data = db.SpCheckUserFieldForce(UserName).FirstOrDefault();
                    return Convert.ToInt32(Data);
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public string RegisterUser(string Username, string password, string MobileNumber, string Email, string RoleID, string District, string Project, string Sector, string Center)
        {
            try
            {
                using (var db = new DBEntities())
                {
                    var Data = db.SpRegisterUser(Username, Helper.Encrypt(password), MobileNumber, Email, Convert.ToInt32(RoleID), "54", District, Project, Sector, Center, 1, DateTime.Now, 1, DateTime.Now).First();
                    if (Data == 1)
                    {
                        return "Success";
                    }
                    else
                    {
                        return "Failure";
                    }
                }
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }

        public SpInsertLoginUser_Result InsertNewUser(NewUser _Newuser)
        {
            try
            {
                using (var db = new DBEntities())
                {
                    _Newuser.StateID = _Newuser.StateID == "0" ? null : _Newuser.StateID;
                    _Newuser.DistrictID = _Newuser.DistrictID == "0" ? null : _Newuser.DistrictID;
                    _Newuser.ProjectID = _Newuser.ProjectID == "0" ? null : _Newuser.ProjectID;
                    _Newuser.SectorID = _Newuser.SectorID == "0" ? null : _Newuser.SectorID;
                    _Newuser.CenterID = _Newuser.CenterID == "0" ? null : _Newuser.CenterID;

                    var Data = db.SpInsertLoginUser(_Newuser.UserName, Helper.Encrypt(_Newuser.Password), _Newuser.MobileNo, _Newuser.Email, _Newuser.RoleID, _Newuser.StateID,
                                                    _Newuser.DistrictID, _Newuser.ProjectID, _Newuser.SectorID, _Newuser.CenterID, _Newuser.UserCode, _Newuser.Name, _Newuser.Type).FirstOrDefault();

                    return Data;
                }
            }
            catch (Exception ex)
            {
                throw null;
            }
        }

        public SPChangePassword_Result ChangePassword(ChangePassword _Change)
        {
            try
            {
                using (var db = new DBEntities())
                {
                    var Data = db.SPChangePassword(_Change.UserCode, Helper.Encrypt(_Change.Password)).FirstOrDefault();
                    return Data;
                }
            }
            catch (Exception ex)
            {
                throw null;
            }
        }

        public string GetUserMail(string Username)
        {
            try
            {
                using (var db = new DBEntities())
                {
                    var Data = db.SPGetUserMail(Username).FirstOrDefault();
                    return Data;
                }
            }
            catch (Exception ex)
            {
                throw null;
            }
        }

        public SPUpdatePassword_Result ForgotPassword(ForgotPassword _Forgot)
        {
            try
            {
                using (var db = new DBEntities())
                {
                    var Data = db.SPUpdatePassword(_Forgot.Username, Helper.Encrypt(_Forgot.Password)).FirstOrDefault();
                    return Data;
                }
            }
            catch (Exception ex)
            {
                throw null;
            }
        }

        public SPStateSpecificData_Result StateSpeicificData(string Statecode)
        {
            try
            {
                using (var db = new DBEntities())
                {
                    var Data = db.SPStateSpecificData(Convert.ToInt32(Statecode)).FirstOrDefault();
                    return Data;
                }
            }
            catch (Exception ex)
            {
                throw null;
            }
        }

        public LoginUserDetails CheckUserLogin(string Username, string Password, string StateCode)
        {
            using (var db = new DBEntities())
            {
                var user = new LoginUserDetails() { StatusCode = "123", StatusMessage = "Unable to connect to Server." };
                var EncPassword = Helper.Encrypt(Password);
                Username = Username.ToLower();
                var userDetails = (from x in db.TblPortalUsers where x.Username.ToLower() == Username && x.StateCode == StateCode select x).FirstOrDefault();
                if (userDetails != null && userDetails.Password == EncPassword)
                {
                    user.UserName = userDetails.Username;
                    user.Name = userDetails.Name;
                    user.Mobile = userDetails.MobileNo;
                    user.UserCode = userDetails.UserCode;
                    user.Email = userDetails.UserEmail;
                    user.RoleID = Convert.ToInt32(userDetails.RoleCode);
                    user.StateCode = userDetails.StateCode;
                    user.DistrictCode = userDetails.DistrictCode;
                    user.ProjectCode = userDetails.ProjectCode;
                    user.SectorCode = userDetails.SectorCode;
                    user.CenterCode = userDetails.AWCCode;
                    user.StatusCode = "000";
                    user.StatusMessage = "Login Successful.";
                }
                else
                {
                    user.StatusCode = "001";
                    user.StatusMessage = "Invalid State Selection";
                }
                return user;
            }
        }
    }
}
