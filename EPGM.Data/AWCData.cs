using EPGM.Framework;
using EPGM.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EPGM.Data
{

    public class AWCData
    {
        private readonly FaceAPIs _FaceAPIs = new FaceAPIs();

        /// <summary>
        /// This method is used to get AWC records from DB based on search criteria
        /// </summary>
        /// <param name="filter">search criteria</param>
        /// <param name="RecCount">total record count</param>
        /// <returns></returns>
        public List<SpGetEPGMAWCDetails_Result> Get(SearchFilter search, string StateCode, string SectorCode, string whoType, string BeneType, string Month, string Year,string CenterType)
        {
            using (DBEntities db = new DBEntities())
            {
                int i = 1, RecCount = 0;
                RecCount = db.SpGetEPGMAWCDetails((whoType == "") ? "Total" : Helper.GetGradeCode(whoType), StateCode, SectorCode, BeneType, Month, Year,Convert.ToInt16(CenterType)).Count();
                var list = new List<SpGetEPGMAWCDetails_Result>();
                var data = db.SpGetEPGMAWCDetails((whoType == "") ? "Total" : Helper.GetGradeCode(whoType), StateCode, SectorCode, BeneType, Month, Year, Convert.ToInt16(CenterType)).ToList();
                foreach (var n in data)
                {
                    var dist = new SpGetEPGMAWCDetails_Result();
                    dist.Sno = i.ToString();
                    dist.DistrictCode = n.DistrictCode;
                    dist.ProjectCode = n.ProjectCode;
                    dist.SectorCode = n.SectorCode;
                    dist.AWCID = n.AWCID;
                    dist.AWCCode = n.AWCCode;
                    dist.AWCName = n.AWCName;
                    dist.Cnt = n.Cnt;
                    list.Add(dist);
                    i++;
                }
                return list;
            }
        }

        public List<SpGetAWCDetails_Result> GetGridData(SearchFilter search, string StateCode, string DistrictCode, string ProjectCode, string SectorCode,string CenterType)
        {
            using (DBEntities db = new DBEntities())
            {
                int i = 1, RecCount = 0;
                RecCount = db.SpGetAWCDetails(StateCode, SectorCode,Convert.ToInt16(CenterType)).Count();
                var list = new List<SpGetAWCDetails_Result>();
                var data = db.SpGetAWCDetails(StateCode, SectorCode,Convert.ToInt16(CenterType)).ToList();
                foreach (var n in data)
                {
                    var dist = new SpGetAWCDetails_Result();
                    dist.Sno = i;
                    dist.DistrictCode = n.DistrictCode;
                    dist.ProjectCode = n.ProjectCode;
                    dist.SectorCode = n.SectorCode;
                    dist.AWCID = n.AWCID;
                    dist.AWCCode = n.AWCCode;
                    dist.AWCName = n.AWCName;
                    dist.AWWCode = n.AWWCode;
                    dist.AWWName = n.AWWName;
                    dist.AWWMobile = n.AWWMobile;
                    dist.IsHierarchyCreated = n.IsHierarchyCreated;
                    dist.HierarchyCreatedDate = n.HierarchyCreatedDate == null ? "" : n.HierarchyCreatedDate;
                    dist.IsHierarchyActived = n.IsHierarchyActived;
                    dist.HierarchyActivatedDate = n.HierarchyActivatedDate == null ? "" : n.HierarchyActivatedDate;

                    dist.FieldForceName = n.FieldForceName == null ? "" : n.FieldForceName;

                    dist.FiledForceStaffCreatedDateTime = n.FiledForceStaffCreatedDateTime == null ? "" : n.FiledForceStaffCreatedDateTime;

                    dist.StaffID = n.StaffID == null ? 0 : n.StaffID;
                    dist.FiledForceStaffMobileNo = n.FiledForceStaffMobileNo == null ? "" : n.FiledForceStaffMobileNo;






                    if (n.AWCImagePath != null && n.AWCImagePath.Contains(ConfigurationManager.AppSettings["BeneURLDirectory"]))
                    {
                        var ImageData = n.AWCImagePath.Split('/');
                        dist.AWCImagePath = ConfigurationManager.AppSettings["BeneURL"] + ImageData[3] + "/" + ImageData[4];
                    }
                    else
                    {
                        dist.AWCImagePath = n.AWCImagePath == null ? "" : n.AWCImagePath;
                    }
                    dist.Address = n.Address;
                    dist.AWCImageCreatedDateTime = n.AWCImageCreatedDateTime;
                    list.Add(dist);
                    i++;
                }
                return list;
            }
        }

        /// <summary>
        /// This method is used to get AWC record from DB
        /// </summary>
        /// <param name="AWC">AWC object to fetch</param>
        /// <returns>AWC object</returns>
        public TblAWC Get(int ID)
        {
            using (DBEntities db = new DBEntities())
            {
                var items = db.TblAWCs.FirstOrDefault(m => m.AWCID == ID);
                return items;
            }
        }

        /// <summary>
        /// Get active AWC list for listing
        /// </summary>
        /// <returns>AWC List</returns>
        public List<SelectListItem> GetList(string TypeID)
        {
            using (var db = new DBEntities())
            {
                return (from x in db.TblAWCs
                        where x.IsActive == true
                        select new SelectListItem { Text = x.AWCName.ToString(), Value = x.AWCCode.ToString() }).ToList();
            }
        }

        /// <summary>
        /// To Export data
        /// </summary>
        /// <returns></returns>
        public List<object> Export()
        {
            using (var db = new DBEntities())
            {
                return (from item in db.TblAWCs
                        select new
                        {
                            AWCCode = item.AWCCode,
                            AWCName = item.AWCName,
                            AWWCode = item.AWWCode,
                            AWWType = item.AWWType,
                            ProjectCode = item.ProjectCode,
                            SectorCode = item.SectorCode,
                            IsActive = (item.IsActive == true) ? "Active" : "Inactive"
                        }).ToList<object>();
            }
        }


        public SPCreateAWC_Result CreateAWC(NewAWC _newawc)
        {
            try
            {
                using (var db = new DBEntities())
                {
                    var Data = db.SPCreateAWC(_newawc.Center, _newawc.MobileNo, _newawc.Email, _newawc.Address, _newawc.AWCType,
                                              _newawc.StateID, _newawc.DistrictID, _newawc.ProjectID, _newawc.SectorID, Convert.ToInt32(_newawc.UserCode)).FirstOrDefault();

                    return Data;
                }
            }
            catch (Exception ex)
            {
                throw null;
            }
        }
        public SPUpdateAWC_Result UpdateAWC(TblAWCMst _awc)
        {
            try
            {
                using (var db = new DBEntities())
                {
                    var Data = db.SPUpdateAWC(_awc.StateID, _awc.DistrictID, _awc.ProjectID, _awc.SectorID, _awc.AWCID, _awc.AWCName, _awc.AWWName, _awc.AWWMobileNo, _awc.UserCode,_awc.CenterType).FirstOrDefault();
                    return Data;
                }
            }
            catch (Exception ex)
            {
                throw null;
            }
        }


        #region  Face API for Group(Hierarchy) Creation,Activation and Deletion
        public ResponseStatus CreateHierarchy(string StateCode, string AWCCode, string AWCName)
        {
            ResponseStatus Response = new ResponseStatus { statusCode = "999", message = "unable to Connect to server" };
            try
            {
                Response = _FaceAPIs.CreateGroupRequest(AWCCode, AWCName);
                using (var db = new DBEntities())
                {
                    if (Response.statusCode.ToString() == "000")
                    {
                        Response.Result = db.SpUpdateHierarchyCreated(StateCode, AWCCode, 1).FirstOrDefault();
                        if (Response.Result != "1")
                        {
                            Response.statusCode = "999";
                            Response.message = "Hierachy Cetaed Successfully at Smart Attendance but failed to Update Status at Growth Monitoring";
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

        public ResponseStatus ActivateHierarchy(string StateCode, string AWCCode)
        {
            ResponseStatus Response = new ResponseStatus { statusCode = "999", message = "unable to Connect to server" };
            try
            {
                Response = _FaceAPIs.TrainGroupRequest(AWCCode);
                using (var db = new DBEntities())
                {
                    if (Response.statusCode.ToString() == "000")
                    {
                        Response.Result = db.SpUpdateHierarchyTrainStatus(StateCode, AWCCode, 1).FirstOrDefault();
                        if (Response.Result != "1")
                        {
                            Response.statusCode = "999";
                            Response.message = "Hierachy Activated Successfully at Smart Attendance but failed to Update Status at Growth Monitoring";
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

        public ResponseStatus DeleteHierarchy(UserData _UData)
        {
            ResponseStatus Response = new ResponseStatus { statusCode = "999", message = "unable to Connect to server" };
            try
            {
                Response = _FaceAPIs.DeletePersonGroupRequest(_UData.AWCCode);
                using (var db = new DBEntities())
                {
                    if (Response.statusCode.ToString() == "000")
                    {
                        var Result = db.SpUpdateHierarchyDeleted(_UData.StateCode, _UData.DistrictCode, _UData.ProjectCode, _UData.SectorCode, _UData.AWCCode, _UData.UserCode).FirstOrDefault();
                        if (Result.ToString() == "000")
                        {
                            var BenRes = db.SpUpdateBeneHierarchyDeleted(_UData.StateCode, _UData.DistrictCode, _UData.ProjectCode, _UData.SectorCode, _UData.AWCCode, _UData.UserCode).FirstOrDefault();
                            if (BenRes != "000")
                            {
                                Response.statusCode = "999";
                                Response.message = "Hierarchy Deleted Successfully at Smart Attendance but failed to Update Status at Growth Monitoring";
                                return Response;
                            }
                        }
                        else
                        {
                            return Response;
                        }
                    }
                    return Response;
                }
            }
            catch (Exception ex)
            {
                Response.statusCode = "999";
                Response.message = ex.Message.ToString();
                return Response;
            }
        }

        public SPGetStaffDetailsFF_Result GetStaffDetails(string StaffCode)
        {
           
                using (DBEntities db = new DBEntities())
                {
                    var res = db.SPGetStaffDetailsFF(StaffCode).FirstOrDefault();
                
               
                    return res;

                }
           
            
        }


        public ResponseStatus GetPersonGroupTrainingStatus(string StateCode, string AWCCode, string AWCName)
        {
            ResponseStatus Response = new ResponseStatus { statusCode = "999", message = "unable to Connect to server" };
            try
            {
                Response = _FaceAPIs.GetPersonGroupTrainingStatus(AWCCode);
                return Response;
            }
            catch (Exception ex)
            {
                Response.statusCode = "999";
                Response.message = ex.Message.ToString();
                return Response;
            }
        }

        public ResponseStatus AssignStaff(string StateID, string DistrictID, string ProjectID, string SectorID, string AwcCode, int StaffId,string UpdatedBy)
        {
            ResponseStatus Response = new ResponseStatus { statusCode = "999", message = "unable to Connect to server" };

            try
            {
                using (DBEntities db = new DBEntities())
                {
                    var res = db.SpUpdateFieldForceStaffForAWC(StateID, DistrictID, ProjectID, SectorID, AwcCode,  StaffId, UpdatedBy).FirstOrDefault();
                    Response.statusCode = res.Code;
                    Response.message = res.Message;

                    return Response;

                }
            }
            catch(Exception ex)
            {
                Response.statusCode = "999";
                Response.message = ex.Message.ToString();
                return Response;

            }
        }
        #endregion
    }
}
