using System;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using EPGM.Data;
using EPGM.Framework;
using EPGM.Models;

namespace EPGM.Business
{
    public class DistrictBusiness
    {
        /// <summary>
        /// Data access object for District
        /// </summary>
        private readonly DistrictData DAO = new DistrictData();

        /// <summary>
        /// Business method to get District list based on WHO Type
        /// </summary>
        /// <param name="search">Search Criteria</param>
        /// <param name="RecCount">Record count</param>
        /// <returns>District List</returns>
        public List<SpGetEPGMDistrictDetails_Result> Get(SearchFilter search, string stateCode, string distCode, string whoType, string BeneType, string Month, string Year,string CenterType)
        {
            return DAO.Get(search, stateCode, distCode, whoType, BeneType,Month,Year, CenterType);
        }

        /// <summary>
        /// Business method to get District list
        /// </summary>
        /// <param name="search">Search Criteria</param>
        /// <param name="RecCount">Record count</param>
        /// <returns>District List</returns>
        public List<SpGetDistrict_Result> GetMasterData(SearchFilter search,string StateCode)
        {
            return DAO.GetMasterData(search, StateCode);
        }

        public string GetDistrictName(string StateCode = "", string distCode = "")
        {
            return DAO.GetDistrictName(StateCode, distCode);
        }

        /// <summary>
        /// Business method for getting active District list for listing
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetList(string StateCode, string TypeID)
        {
            return DAO.GetList(StateCode, TypeID);
        }

        /// <summary>
        /// Business method for getting active District list for listing
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetListWithoutSelect(string StateCode, string TypeID)
        {
            return DAO.GetListWithoutSelect(StateCode, TypeID);
        }

        public SPUpdateDistrict_Result UpdateDistrict(TblDistrictMst _dist)
        {
            return DAO.UpdateDistrict(_dist);
        }
    }
}
