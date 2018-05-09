using System;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using EPGM.Data;
using EPGM.Models;
using EPGM.Framework;

namespace EPGM.Business
{
    public class ProjectBusiness
    {
        /// <summary>
        /// Data access object for Project
        /// </summary>
        private readonly ProjectData _distProj = new ProjectData();
        private readonly DistrictData _distData = new DistrictData();

        /// <summary>
        /// Business method to get Project list
        /// </summary>
        /// <param name="search">Search Criteria</param>
        /// <param name="RecCount">Record count</param>
        /// <returns>Project List</returns>
        public List<SpGetEPGMProjectDetails_Result> Get(SearchFilter search, string stateCode, string distCode,string whoType,string BeneType, string Month, string Year,string CenterType = "")
        {
            return _distProj.Get(search, stateCode, distCode, whoType, BeneType,Month,Year, CenterType);
        }
        public List<SpGetProjectDetails_Result> GetDataGrid(SearchFilter search, string stateCode, string distCode,string CenterType)
        {
            return _distProj.GetDataGrid(search, stateCode, distCode, CenterType);
        }

        public string GetDistrictName(string stateCode, string distCode)
        {
            return _distData.GetDistrictName(stateCode, distCode);
        }

        /// <summary>
        /// Business method to get specific Project
        /// </summary>
        /// <param name="ID">ID of Project</param>
        /// <returns>Project object</returns>
        public VWGetProject Get(int ID)
        {
            return _distProj.Get(ID);
        }

        /// <summary>
        /// Business method for getting active Project list for listing
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetList(string TypeID = "")
        {
            return _distProj.GetList(TypeID);
        }

        public List<object> Export()
        {
            return _distProj.Export();
        }


        public SPUpdateProject_Result UpdateProject(TblProjectMst _proj)
        {
            return _distProj.UpdateProject(_proj);
        }

    }
}
