using EPGM.Data;
using EPGM.Framework;
using EPGM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EPGM.Business
{
    public class AWCBusiness
    {
        /// <summary>
        /// Data access object for AWC
        /// </summary>
        private readonly AWCData _awcData = new AWCData();
        private readonly SectorData _secData = new SectorData();

        /// <summary>
        /// Business method to get AWC list
        /// </summary>
        /// <param name="search">Search Criteria</param>
        /// <param name="RecCount">Record count</param>
        /// <returns>AWC List</returns>
        public List<SpGetEPGMAWCDetails_Result> Get(SearchFilter search, string StateCode, string SectorCode, string whoType,string BeneType,string Month,string Year,string CenterType = "")
        {
            return _awcData.Get(search, StateCode, SectorCode, whoType, BeneType,  Month,  Year, CenterType);
        }

        public List<SpGetAWCDetails_Result> GetGridData(SearchFilter search, string StateCode, string DistrictCode, string ProjectCode, string SectorCode,string CenterType)
        {
            return _awcData.GetGridData(search, StateCode, DistrictCode, ProjectCode, SectorCode, CenterType);
        }

        /// <summary>
        /// Business method to get specific AWC
        /// </summary>
        /// <param name="ID">ID of AWC</param>
        /// <returns>AWC object</returns>
        public TblAWC Get(int ID)
        {
            return _awcData.Get(ID);
        }

        public string GetSectorName(string secCode = "")
        {
            return _secData.GetSectorName(secCode);
        }

        /// <summary>
        /// Business method for getting active AWC list for listing
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetList(string TypeID = "")
        {
            return _awcData.GetList(TypeID);
        }

        public List<object> Export()
        {
            return _awcData.Export();
        }

        public SPCreateAWC_Result CreateAWC(NewAWC _newawc)
        {
            return _awcData.CreateAWC(_newawc);
        }

        public SPUpdateAWC_Result UpdateAWC(TblAWCMst _awc)
        {
            return _awcData.UpdateAWC(_awc);
        }

        #region Face Api for Group(Hierarchy) Creation ,Activation and Deletion
        public ResponseStatus CreateHierarchy(string StateCode, string AWCCode, string AWCName)
        {
            return _awcData.CreateHierarchy(StateCode, AWCCode, AWCName);
        }
        public ResponseStatus ActivateHierarchy(string StateCode, string AWCCode)
        {
            return _awcData.ActivateHierarchy(StateCode, AWCCode);
        }
        public ResponseStatus DeleteHierarchy(UserData _UData)
        {
            return _awcData.DeleteHierarchy(_UData);
        }
        public SPGetStaffDetailsFF_Result GetStaffDetails(string StaffCode)
        {
            return _awcData.GetStaffDetails(StaffCode);
        }
        public ResponseStatus AssignStaff(string StateID, string DistrictID, string ProjectID, string SectorID, string AwcCode, int StaffId, string UpdatedBy)
        {
            return _awcData.AssignStaff(StateID, DistrictID, ProjectID, SectorID, AwcCode, StaffId, UpdatedBy);
        }


            #endregion
        }
    }
