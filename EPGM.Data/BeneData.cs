using EPGM.Framework;
using EPGM.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EPGM.Data
{
    public class BeneData
    {
        private readonly FaceAPIs _FaceAPIs = new FaceAPIs();


        /// <summary>
        /// This method is used to get AWC records from DB based on search criteria
        /// </summary>
        /// <param name="filter">search criteria</param>
        /// <param name="RecCount">total record count</param>
        /// <returns></returns>
        public List<SpGetEPGMBenes_Result> Get(SearchFilter search, out int RecCount, string stateCode, string distCode, string projCode, string secCode, string awcCode, string whoType, string month = "", string year = "",string CenterType ="")
        {
            using (DBEntities db = new DBEntities())
            {
                int i = 1; RecCount = 0;
                RecCount = db.SpGetEPGMBenes(stateCode, distCode, projCode, secCode, awcCode, whoType, month, year, "1",Convert.ToInt16(CenterType)).Count();
                var list = new List<SpGetEPGMBenes_Result>();
                //whoType = whoType == "Total" ? "All" : Helper.GetGradeCode(whoType);
                whoType = "All";
                var data = db.SpGetEPGMBenes(stateCode, distCode, projCode, secCode, awcCode, whoType, month, year, "1", Convert.ToInt16(CenterType)).ToList();
                foreach (var n in data)
                {
                    var dist = new SpGetEPGMBenes_Result();
                    dist.Sno = Convert.ToString(i);
                    dist.BeneCode = n.BeneCode;
                    dist.BeneName = n.BeneName;
                    dist.BeneSurname = n.BeneSurname;
                    dist.Gender = n.Gender;
                    dist.DOB = n.DOB;
                    dist.MobileNumber = n.MobileNumber;
                    dist.LatestWeight = n.LatestWeight;
                    dist.LatestHeight = n.LatestHeight;
                    dist.LatestBMI = n.LatestBMI;
                    dist.GradeName = n.GradeName;
                    dist.WeightToAdd = n.WeightToAdd;
                    list.Add(dist);
                    i++;
                }
                return list;
            }
        }

        public List<SpGetEPGMBenes_Result> GetMasterData(SearchFilter search, out int RecCount, string stateCode, string distCode, string projCode, string secCode, string awcCode, string whoType, string month, string year, string BeneType,string CenterType)
        {
            using (DBEntities db = new DBEntities())
            {
                int i = 1; RecCount = 0;
                var list = new List<SpGetEPGMBenes_Result>();
                var data = db.SpGetEPGMBenes(stateCode, distCode, projCode, secCode, awcCode, whoType, month, year, BeneType,Convert.ToInt16(CenterType)).ToList();
                foreach (var n in data)
                {
                    var dist = new SpGetEPGMBenes_Result();
                    dist.Sno = Convert.ToString(i);
                    dist.BeneCode = n.BeneCode;
                    dist.BeneName = n.BeneName;
                    dist.BeneSurname = n.BeneSurname;
                    dist.DOB = n.DOB;
                    dist.MobileNumber = n.MobileNumber;
                    dist.LatestWeight = n.LatestWeight;
                    dist.LatestHeight = n.LatestHeight;
                    dist.AgeIntYears = n.AgeIntYears;
                    dist.LatestBMI = n.LatestBMI;
                    dist.LastWeighedDate = n.LastWeighedDate == null ? "" : n.LastWeighedDate;
                    dist.GradeName = n.GradeName == "" ? "" : n.GradeName;
                    dist.WeightToAdd = n.WeightToAdd;
                    dist.EnrolledStatus = n.EnrolledStatus;
                    dist.AttendanceStatus = n.AttendanceStatus;
                    dist.AttendanceTimeStamp = n.AttendanceTimeStamp == null ? "" : n.AttendanceTimeStamp;
                    dist.AttendanceImageURL = n.AttendanceImageURL;
                    dist.Address = n.Address == null ? "" : n.Address;
                    //dist.WHImageURL = n.WHImageURL == null ? "" : n.WHImageURL;
                    dist.WHTimeStamp = n.WHTimeStamp == null ? "" : n.WHTimeStamp;
                    dist.WHAddress = n.WHAddress == null ? "" : n.WHAddress;
                    dist.IsRegisteredAtAttendance = n.IsRegisteredAtAttendance;
                    dist.IsEnrolledAtAttendance = n.IsEnrolledAtAttendance;
                    dist.HBReading = n.HBReading;
                    dist.HBGrade = n.HBGrade;
                    dist.MUReading = n.MUReading;
                    dist.MUGrade = n.MUGrade;
                    dist.OXPulseReading = n.OXPulseReading;

                    if (n.DOB != "" && n.DOB != null)
                    {
                        n.DOB = n.DOB.Replace("-", "/");
                        dist.DateofBirth = n.DOB.ToString(System.Globalization.CultureInfo.GetCultureInfo("en-IN")).ToString();
                    }
                    else
                    {
                        dist.DateofBirth = "";
                    }

                    if (n.WHImageURL != null && n.WHImageURL.Contains(ConfigurationManager.AppSettings["BeneURLDirectory"]))
                    {
                        var ImageData = n.WHImageURL.Split('/');
                        dist.WHImageURL = ConfigurationManager.AppSettings["BeneURL"] + ImageData[3] + "/" + ImageData[4];
                    }
                    else
                    {
                        dist.WHImageURL = n.WHImageURL == null ? "" : n.WHImageURL;
                    }
                    dist.Gender = Common.GenderName(n.Gender == null ? "" : n.Gender);

                    if (n.ActiveStatus == "1")
                    {
                        dist.ActiveStatus = "Active";
                    }
                    else
                    {
                        dist.ActiveStatus = "InActive";
                    }

                    list.Add(dist);
                    i++;
                }
                return list;
            }
        }

        public List<SpGetAllBenesofCenter_Result> GetBenesofCenter(SearchFilter search, out int RecCount, string stateCode, string distCode, string projCode, string secCode, string awcCode, string BeneType,string CenterType)
        {
            using (DBEntities db = new DBEntities())
            {
                int i = 1; RecCount = 0;
                var list = new List<SpGetAllBenesofCenter_Result>();
                var data = db.SpGetAllBenesofCenter(stateCode, distCode, projCode, secCode, awcCode, BeneType,Convert.ToInt16(CenterType)).ToList();
                foreach (var n in data)
                {
                    var dist = new SpGetAllBenesofCenter_Result();
                    dist.Sno = Convert.ToString(i);
                    dist.AWCCode = n.AWCCode;
                    dist.BeneCode = n.BeneCode;
                    dist.BeneName = n.BeneName;
                    dist.BeneSurname = n.BeneSurname;
                    dist.Gender = n.Gender;
                    dist.DOB = n.DOB;
                    dist.Age = n.Age;
                    dist.MotherName = n.MotherName;
                    dist.FatherName = n.FatherName;
                    dist.AadhaarNumber = n.AadhaarNumber;
                    dist.MobileNumber = n.MobileNumber;
                    dist.RegisteredDateTime = n.RegisteredDateTime;
                    dist.IsActive = n.IsActive;
                    list.Add(dist);
                    i++;
                }
                return list;
            }
        }

        public SpUpdateStatusOfBene_Result UpdateStatusOfBene(string stateCode, string distCode, string projCode, string secCode, string awcCode, string BeneCode, string Status,string centertype = "")
        {
            using (DBEntities db = new DBEntities())
            {
                var data = db.SpUpdateStatusOfBene(stateCode, distCode, projCode, secCode, awcCode, BeneCode, Status,Convert.ToInt16(centertype)).FirstOrDefault();
                return data;
            }
        }

        public SpGetUpdatedBeneInfo_Result GetBeneDetails(string beneCode)
        {
            using (var db = new DBEntities())
            {
                var data = db.SpGetUpdatedBeneInfo(beneCode).FirstOrDefault();
                return data;
            }
        }

        public ResponseStatus InsertBeneDetails(NewBene newbene)
        {
            ResponseStatus Response = new ResponseStatus { statusCode = "999", message = "unable to Connect to server" };
            try
            {
                //var ftpStatus = "";
                //var AttendanceStatus = "";
                var data = "";
                DateTime? edd = null;
                using (var db = new DBEntities())
                {
                    if (newbene.EDD != "" && newbene.EDD != null)
                    {
                        edd = Convert.ToDateTime(newbene.EDD, System.Globalization.CultureInfo.InvariantCulture);
                    }
                    var CenterID = "A" + newbene.CenterID.ToString();
                    data = db.SpNewBene(CenterID, newbene.ChildName.ToUpper(), newbene.ConfirmChildName.ToUpper(), "", newbene.MotherName.ToUpper(), newbene.FatherName, newbene.RationCardNo,
                                                    Convert.ToDateTime(DateTime.ParseExact(newbene.DOB, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)),
                                                    newbene.Gender, newbene.ContactNo, "", "", newbene.Category, Convert.ToInt32(newbene.BeneType), newbene.AadhaarNo, newbene.AadhaarCardType, newbene.ResidentType,
                                                    Convert.ToDateTime(DateTime.ParseExact(newbene.RegisDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)),
                                                    Convert.ToDecimal(newbene.IncomeRange), newbene.StateID, newbene.DistrictID, newbene.ProjectID, newbene.SectorID, null, null, "", "",
                                                    Convert.ToInt32(newbene.CreatedBy), "N", "N", newbene.DeptUID, newbene.BirthWeight, edd, "C", newbene.CenterType).FirstOrDefault();

                    if (data != null && data != "")
                    {
                        //string dob = newbene.DOB.Replace("/", "");
                        //dob = dob.Substring(0, 4) + "" + dob.Substring(6, 2);
                        //var ParentName = newbene.FatherName == "" ? newbene.MotherName : newbene.FatherName;
                        //string beneData = data + "" + dob + "" + newbene.Gender + "" +
                        //                    string.Format("{0, 10}", newbene.ChildName.ToUpper().PadRight(10)) + "" +
                        //                    string.Format("{0, 10}", ParentName.ToUpper().PadRight(10)) + "\r\n";
                        //var localStatus = EPGMFTPClass.InsertIntoLocalFile(newbene.CenterID, beneData);
                        //ftpStatus = EPGMFTPClass.InsertIntoFTPFile(newbene.CenterID);
                        //if (ftpStatus == "000")
                        //{
                        //    var Status = db.SpUpdateEPGMStatus(CenterID, data, ftpStatus);
                        //if (Status == 1) 
                        //{
                        FaceAPIReg _FaceAPIReg = new FaceAPIReg();
                        _FaceAPIReg.BeneCode = data.ToString();
                        _FaceAPIReg.BeneName = newbene.ChildName;
                        _FaceAPIReg.MotherName = newbene.MotherName;
                        _FaceAPIReg.AWCCode = newbene.CenterID;

                        Response = _FaceAPIs.CreatePersonRequest(_FaceAPIReg);

                        if (Response.PersonID != "" && Response.PersonID != null)
                        {
                            Response.Result = db.SpUpdatePersonCreated(newbene.StateID, newbene.DistrictID, newbene.ProjectID, newbene.SectorID, newbene.CenterID, data, 1, Response.PersonID, newbene.CenterType).FirstOrDefault();
                            if (Response.Result == "1")
                            {
                                Response.statusCode = "000";
                                Response.message = "Beneficiary Registered Successfully at Growth Monitoring and Attendance as well & your Beneficiary ID: " + data.ToString();
                                return Response;
                            }
                            else
                            {
                                Response.statusCode = "000";
                                Response.message = "Beneficiary Registered Successfully at Growth Monitoring and Registered at Attendance but failed to Enroll at EPGM & your Beneficiary ID: " + data.ToString();
                                return Response;
                            }
                        }
                        else
                        {
                            Response.statusCode = "000";
                            Response.message = "Beneficiary Registered Successfully at Growth Monitoring but failed to Register Person at Attendance & your Beneficiary ID: " + data.ToString();
                            return Response;
                        }
                        //}
                        //else
                        //{
                        //    Response.statusCode = "000";
                        //    Response.message = "Beneficiary Registered Successfully at Growth Monitoring but failed to Register Person at Attendance and EPGM ftp Status & your Beneficiary ID: " + data.ToString();
                        //    return Response;
                        //}
                        //}

                        //else
                        //{
                        //    Response.statusCode = "000";
                        //    Response.message = "Beneficiary Registered Successfully at Growth Monitoring but failed to Register Person at Attendance and EPGM ftp & your Beneficiary ID: " + data.ToString();
                        //    return Response;
                        //}
                    }
                    else
                    {
                        Response.statusCode = "000";
                        Response.message = "Beneficiary Registered Successfully at Growth Monitoring but failed to Create Weight Height Details & your Beneficiary ID: " + data.ToString();
                        return Response;
                    }
                }
            }
            catch (Exception ex)
            {
                Response.statusCode = "999";
                Response.message = ex.Message.ToString();
                return Response;
            }
            //if (ftpStatus == "000" && AttendanceStatus == "ACK:0300")
            //{
            //    return "New Beneficiary is created Successfully at EPGM and Smart Attendance as well & your Beneficiary ID: " + data;
            //}
            //else if (ftpStatus == "000" && AttendanceStatus != "ACK:0300")
            //{
            //    return "New Beneficiary is created Successfully at EPGM as well but failed at Smart Attendance & your Beneficiary ID: " + data;
            //}
            //else if (ftpStatus != "000" && AttendanceStatus == "ACK:0300")
            //{
            //    return "New Beneficiary is created Successfully at Smart Attendance as well but failed at EPGM & your Beneficiary ID: " + data;
            //}
            //else
            //{
            //    return "New Beneficiary is created Successfully but failed at EPGM and Smart Attendance & your Beneficiary ID: " + data;
            //}
        }

        public List<object> GetMasterDataExport(SearchFilter search, out int RecCount, string stateCode, string distCode, string projCode, string secCode, string awcCode, string whoType, string month, string year, string BeneType,string CenterType)
        {
            var result = GetMasterData(search, out RecCount, stateCode, distCode, projCode, secCode, awcCode, whoType, month, year, BeneType, CenterType);
            var fnlres = (from item in result
                          select new
                          {
                              Sno = item.Sno,
                              BeneCode = item.BeneCode,
                              Name = item.BeneName,
                              Surname = item.BeneSurname,
                              DOB = item.DateofBirth,
                              Gender = item.Gender,
                              Age = item.AgeIntYears,
                              Weight = item.LatestWeight,
                              Height = item.LatestHeight,
                              GradeName = item.GradeName,
                              MobileNumber = item.MobileNumber,
                              LatestBMI = item.LatestBMI,
                              LastWeighedDate = item.LastWeighedDate,
                              ActiveStatus = item.ActiveStatus,
                              RegisteredStatus = item.IsRegisteredAtAttendance,
                              EnrolledStatus = item.IsEnrolledAtAttendance,
                              AttendanceStatus = item.AttendanceStatus,
                              FaceRecognitionTimeStamp = item.WHTimeStamp

                          }).ToList<object>();
            return fnlres;
        }

        public List<SpGetAllBeneficiaryDetails_Result> GetBeneficiaryDetails(SearchFilter search, out int RecCount, string stateCode, string distCode, string projCode, string secCode, string awcCode, string BeneType,string CenterType = "")
        {
            using (DBEntities db = new DBEntities())
            {
                int i = 1; RecCount = 0;
                var list = new List<SpGetAllBeneficiaryDetails_Result>();
                var data = db.SpGetAllBeneficiaryDetails(stateCode, distCode, projCode, secCode, awcCode, BeneType,Convert.ToInt16(CenterType)).ToList();
                foreach (var n in data)
                {
                    var dist = new SpGetAllBeneficiaryDetails_Result();
                    dist.Sno = Convert.ToString(i);
                    dist.AWCCode = n.AWCCode;
                    dist.BeneCode = n.BeneCode;
                    dist.BeneName = n.BeneName;
                    dist.BeneSurname = n.BeneSurname;
                    dist.MotherName = n.MotherName;
                    dist.FatherName = n.FatherName;
                    dist.Gender = Common.GenderName(n.Gender == null ? "" : n.Gender);
                    dist.DOB = n.DOB;
                    dist.AadhaarType = Common.AadhaarType(n.AadhaarType == null ? "" : n.AadhaarType);
                    dist.AadhaarNumber = n.AadhaarNumber;
                    dist.MobileNumber = n.MobileNumber;
                    dist.FatherRationCardNumber = n.FatherRationCardNumber == null ? "" : n.FatherRationCardNumber.ToUpper();
                    dist.Income = n.Income;
                    dist.BeneCategory = Common.BeneCategory(n.BeneCategory == null ? "" : n.BeneCategory);
                    dist.RegisteredDateTime = n.RegisteredDateTime;
                    dist.ResidentType = Common.ResidentType(n.ResidentType == null ? "" : n.ResidentType);
                    dist.IsRegisteredAtAttendance = n.IsRegisteredAtAttendance;
                    dist.RegisteredAtAttendanceDate = n.RegisteredAtAttendanceDate;
                    dist.IsEnrolledAtAttendance = n.IsEnrolledAtAttendance;
                    dist.EnrolledAtAttendanceDate = n.EnrolledAtAttendanceDate;
                    dist.PersonID = n.PersonID;
                    dist.PersistedFaceID = n.PersistedFaceID;
                    list.Add(dist);
                    i++;
                }
                return list;
            }
        }

        public USP_Update_BENEFICIARY_Result UpdateBeneDetails(UpdateBene _Update)
        {
            DateTime? edd = null;
            using (var db = new DBEntities())
            {
                if (_Update.EDD != "" && _Update.EDD != null)
                {
                    edd = Convert.ToDateTime(_Update.EDD, System.Globalization.CultureInfo.InvariantCulture);
                }
                var CenterID = "A" + _Update.AWCID.ToString();
                var Data = db.USP_Update_BENEFICIARY(CenterID, _Update.BeneCode, _Update.BeneName, _Update.BeneSurname, _Update.MotherName, _Update.FatherName, _Update.FatherRationCardNumber,
                                                     Convert.ToDateTime(DateTime.ParseExact(_Update.DOB, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)),
                                                     _Update.Gender, _Update.MobileNumber, _Update.BeneCategory, 1, _Update.AadhaarNumber, _Update.AadhaarType, _Update.ResidentType,
                                                     Convert.ToDateTime(DateTime.ParseExact(_Update.RegisteredDateTime, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)),
                                                     Convert.ToDecimal(_Update.Income), _Update.StateID, _Update.DistrictID, _Update.ProjectID, _Update.AWCID, Convert.ToInt32(_Update.UserCode),
                                                     _Update.DeptUID, _Update.BirthWeight, _Update.CenterType).FirstOrDefault();
                return Data;
            }
        }

        public List<SpGetEPGMBeneficiaries_Result> GetBeneficiaries(SearchFilter search, out int RecCount, string stateCode, string distCode, string projCode, string secCode, string awcCode, string whoType, string month = "", string year = "", string Typebene = "",string CenterType = "")
        {
            using (DBEntities db = new DBEntities())
            {
                int i = 1; RecCount = 0;
                RecCount = db.SpGetEPGMBenes(stateCode, distCode, projCode, secCode, awcCode, whoType, month, year, Typebene,1).Count();
                var list = new List<SpGetEPGMBeneficiaries_Result>();
                whoType = whoType == "Total" ? "All" : Helper.GetGradeCode(whoType);
                var data = db.SpGetEPGMBeneficiaries(stateCode, distCode, projCode, secCode, awcCode, whoType, month, year, Typebene,Convert.ToInt16(CenterType)).ToList();
                foreach (var n in data)
                {
                    var dist = new SpGetEPGMBeneficiaries_Result();
                    dist.Sno = Convert.ToString(i);
                    dist.BeneCode = n.BeneCode;
                    dist.BeneName = n.BeneName;
                    dist.BeneSurname = n.BeneSurname;
                    dist.DOB = n.DOB;
                    dist.MobileNumber = n.MobileNumber;
                    dist.GradeName = n.GradeName;
                    dist.Gender = Helper.GetGender(n.Gender);

                    list.Add(dist);
                    i++;
                }
                return list;
            }
        }

        public List<SPGetPWWomenData_Result> GetPWWomenData(SearchFilter search, out int RecCount, string stateCode, string distCode, string projCode, string secCode, string awcCode, string BeneType,string CenterType)
        {
            using (DBEntities db = new DBEntities())
            {
                int i = 1; RecCount = 0;
                var list = new List<SPGetPWWomenData_Result>();
                var data = db.SPGetPWWomenData(stateCode, distCode, projCode, secCode, awcCode, BeneType,Convert.ToInt16(CenterType)).ToList();
                foreach (var n in data)
                {
                    var dist = new SPGetPWWomenData_Result();
                    dist.Sno = Convert.ToString(i);
                    dist.BeneID = n.BeneID;
                    dist.BeneCode = n.BeneCode;
                    dist.Name = n.Name;
                    dist.HusbandName = n.HusbandName;
                    dist.MobileNo = n.MobileNo;
                    dist.BeneType = n.BeneType;

                    if (n.ImagePath != null && n.ImagePath.Contains(ConfigurationManager.AppSettings["BeneURLDirectory"]))
                    {
                        var ImageData = n.ImagePath.Split('/');
                        dist.ImagePath = ConfigurationManager.AppSettings["BeneURL"] + ImageData[3] + "/" + ImageData[4];
                    }
                    else
                    {
                        dist.ImagePath = n.ImagePath == null ? "" : n.ImagePath;
                    }

                    list.Add(dist);
                    i++;
                }
                return list;
            }
        }

        public ResponseStatus UpdatePWType(string BeneID = "", string BeneCode = "", string BeneType = "")
        {
            var Result = new ResponseStatus { code = "999", message = "Unable to Connect to Remote Server" };
            try
            {
                using (var db = new DBEntities())
                {
                    if (BeneType == "2")
                    {
                        var Res = db.SPUpdatePWType(BeneID, BeneCode).FirstOrDefault();
                        Result.code = Res.code;
                        Result.message = Res.Message;
                        return Result;
                    }
                    else
                    {
                        Result.code = "001";
                        Result.message = "failed to Update.Invalid Beneficiary Type Selection";
                        return Result;
                    }
                }
            }
            catch (Exception ex)
            {
                Result.code = "999";
                if (ex.InnerException == null)
                {
                    Result.message = ex.Message.ToString();
                }
                else
                {
                    Result.message = ex.InnerException.ToString();
                }
                return Result;
            }
        }








        #region  Face API for Beneficiary Creation,Activation and Deletion
        public ResponseStatus RegisterPerson(FaceAPIReg _FaceAPIReg)
        {
            ResponseStatus Response = new ResponseStatus { statusCode = "999", message = "unable to Connect to server" };
            try
            {
                Response = _FaceAPIs.CreatePersonRequest(_FaceAPIReg);
                using (var db = new DBEntities())
                {
                    if (Response.PersonID != "" && Response.statusCode.ToString() == "000")
                    {
                        Response.Result = db.SpUpdatePersonCreated(_FaceAPIReg.StateCode, _FaceAPIReg.DistrictCode, _FaceAPIReg.ProjectCode, _FaceAPIReg.SectorCode, _FaceAPIReg.AWCCode, _FaceAPIReg.BeneCode, 1, Response.PersonID, _FaceAPIReg.CenterType).FirstOrDefault();
                        if (Response.Result != "1")
                        {
                            Response.statusCode = "000";
                            Response.message = "Person Registered Successfully at Smart Attendance but failed to Update at EPGM";
                            return Response;
                        }
                        else
                        {
                            return Response;
                        }
                    }
                    else
                    {
                        Response.statusCode = "999";
                        Response.message = Response.message;
                        return Response;
                    }
                }
            }
            catch (Exception ex)
            {
                Response.statusCode = "999";
                Response.message = ex.Message.ToString();
                return Response;
            }
        }

        public ResponseStatus EnrollPerson(FaceAPIReg _FaceAPIReg)
        {
            ResponseStatus Response = new ResponseStatus { statusCode = "999", message = "unable to Connect to server" };
            try
            {
                Response = _FaceAPIs.EnrollPersonRequest(_FaceAPIReg);
                using (var db = new DBEntities())
                {
                    if (Response.PersonID != "" && Response.statusCode.ToString() == "000")
                    {
                        Response.Result = db.SpUpdatePersonFaceCreated(_FaceAPIReg.StateCode, _FaceAPIReg.DistrictCode, _FaceAPIReg.ProjectCode, _FaceAPIReg.SectorCode, _FaceAPIReg.AWCCode, _FaceAPIReg.BeneCode, 1, Response.PersonID).FirstOrDefault();
                        if (Response.Result != "1")
                        {
                            Response.statusCode = "999";
                            Response.message = "Person Enrolled Successfully at Smart Attendance but failed to Update at EPGM";
                            return Response;
                        }
                        else
                        {
                            return Response;
                        }
                    }
                    else
                    {
                        Response.statusCode = "999";
                        Response.message = Response.message;
                        return Response;
                    }
                }
            }
            catch (Exception ex)
            {
                Response.statusCode = "999";
                Response.message = ex.Message.ToString();
                return Response;
            }
        }

        public ResponseStatus DeletePersonFace(FaceAPIReg _FaceAPIReg)
        {
            ResponseStatus Response = new ResponseStatus { statusCode = "999", message = "unable to Connect to server" };
            try
            {
                Response = _FaceAPIs.DeletePersonFaceRequest(_FaceAPIReg.AWCCode, _FaceAPIReg.PersonID, _FaceAPIReg.PersistedFaceID);
                using (var db = new DBEntities())
                {
                    if (Response.statusCode == "000")
                    {
                        Response.Result = db.SpUpdatePersonFaceDeleted(_FaceAPIReg.StateCode, _FaceAPIReg.DistrictCode, _FaceAPIReg.ProjectCode, _FaceAPIReg.SectorCode, _FaceAPIReg.AWCCode, _FaceAPIReg.BeneCode, _FaceAPIReg.UserCode, 1).FirstOrDefault();
                        if (Response.Result != "1")
                        {
                            Response.statusCode = "999";
                            Response.message = "Person Face Deleted Successfully at Smart Attendance but failed to Update at EPGM";
                            return Response;
                        }
                        else
                        {
                            //On Sucessfull Person Register and Enroll --Train PerosnGroup(AWC)
                            ResponseStatus TrainRespone = _FaceAPIs.TrainGroupRequest(_FaceAPIReg.AWCCode);
                            return Response;
                        }
                    }
                    else
                    {
                        Response.statusCode = "999";
                        Response.message = Response.message;
                        return Response;
                    }
                }
            }
            catch (Exception ex)
            {
                Response.statusCode = "999";
                Response.message = ex.Message.ToString();
                return Response;
            }
        }

        public ResponseStatus DeletePerson(FaceAPIReg _FaceAPIReg)
        {
            ResponseStatus Response = new ResponseStatus { statusCode = "999", message = "unable to Connect to server" };
            try
            {
                Response = _FaceAPIs.DeletePersonRequest(_FaceAPIReg.AWCCode, _FaceAPIReg.PersonID);
                using (var db = new DBEntities())
                {
                    if (Response.statusCode == "000")
                    {
                        Response.Result = db.SpUpdatePersonDeleted(_FaceAPIReg.StateCode, _FaceAPIReg.DistrictCode, _FaceAPIReg.ProjectCode, _FaceAPIReg.SectorCode, _FaceAPIReg.AWCCode, _FaceAPIReg.BeneCode, _FaceAPIReg.UserCode, 1).FirstOrDefault();
                        if (Response.Result != "1")
                        {
                            Response.statusCode = "999";
                            Response.message = "Person Deleted Successfully at Smart Attendance but failed to Update at EPGM";
                            return Response;
                        }
                        else
                        {
                            //On Sucessfull Person Register and Enroll --Train PerosnGroup(AWC)
                            ResponseStatus TrainRespone = _FaceAPIs.TrainGroupRequest(_FaceAPIReg.AWCCode);
                            return Response;
                        }
                    }
                    else
                    {
                        Response.statusCode = "999";
                        Response.message = Response.message;
                        return Response;
                    }
                }
            }
            catch (Exception ex)
            {
                Response.statusCode = "999";
                Response.message = ex.Message.ToString();
                return Response;
            }
        }
        #endregion
    }
}
