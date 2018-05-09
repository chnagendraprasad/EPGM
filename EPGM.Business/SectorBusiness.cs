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
    public class SectorBusiness
    {
        /// <summary>
        /// Data access object for Sector
        /// </summary>
        private readonly SectorData _secData = new SectorData();
        private readonly ProjectData _projData = new ProjectData();

        public List<SpGetEPGMSectorDetails_Result> Get(SearchFilter search, string stateCode, string projCode,string  whoType,string BeneType, string Month, string Year,string CenterType = "")
        {
            return _secData.Get(search, stateCode, projCode, whoType, BeneType,Month,Year, CenterType);
        }

        public VWGetSector Get(int ID)
        {
            return _secData.Get(ID);
        }

        public string GetProjectName(string projCode = "")
        {
            return _projData.GetProjectName(projCode);
        }

        public List<SelectListItem> GetList(string TypeID = "")
        {
            return _secData.GetList(TypeID);
        }

        public List<object> Export()
        {
            return _secData.Export();
        }

        public List<SpGetSector_Result> GetGridData(SearchFilter search, string stateCode, string DistCode, string projCode,string CenterType)
        {
            return _secData.GetGridData(search, stateCode, DistCode, projCode, CenterType);
        }
        public SPUpdateSector_Result UpdateSector(TblSectorMst _sec)
        {
            return _secData.UpdateSector(_sec);
        }
    }
}
