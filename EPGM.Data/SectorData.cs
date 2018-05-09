using EPGM.Framework;
using EPGM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EPGM.Data
{
    public class SectorData
    {
        /// <summary>
        /// This method is used to get Sector records from DB based on search criteria
        /// </summary>
        /// <param name="filter">search criteria</param>
        /// <param name="RecCount">total record count</param>
        /// <returns></returns>
        public List<SpGetEPGMSectorDetails_Result> Get(SearchFilter search, string stateCode = "", string projCode = "", string whoType="",string BeneType="", string Month = "", string Year = "",string CenterType = "")
        {
            using (DBEntities db = new DBEntities())
            {
                int i = 1, RecCount=0;
                var list = new List<SpGetEPGMSectorDetails_Result>();
                RecCount = db.SpGetEPGMSectorDetails((whoType == "") ? "Total" : Helper.GetGradeCode(whoType), stateCode, projCode, BeneType, Month,Year,Convert.ToInt16(CenterType)).Count();
                var data = db.SpGetEPGMSectorDetails((whoType == "") ? "Total" : Helper.GetGradeCode(whoType), stateCode, projCode, BeneType, Month, Year, Convert.ToInt16(CenterType)).OrderBy(x => x.SectorName).ToList();
                foreach (var n in data)
                {
                    var m = new SpGetEPGMSectorDetails_Result();
                    m.Sno = Convert.ToString(i);
                    m.SectorID = n.SectorID;
                    m.SectorCode = n.SectorCode;
                    m.SectorName = n.SectorName;
                    m.ProjectCode = n.ProjectCode;
                    m.DistrictCode = n.DistrictCode;
                    m.Cnt = n.Cnt;
                    list.Add(m);
                    i++;
                }
                return list;
            }
        }

        public List<SpGetSector_Result> GetGridData(SearchFilter search, string stateCode = "", string DistCode = "", string projCode = "",string CenterType="")
        {
            using (DBEntities db = new DBEntities())
            {
                int i = 1;
                var list = new List<SpGetSector_Result>();
                var data = db.SpGetSector(stateCode, DistCode, projCode,Convert.ToInt32(CenterType)).OrderBy(x => x.SectorName).ToList();
                foreach (var n in data)
                {
                    var m = new SpGetSector_Result();
                    m.Sno = Convert.ToString(i);
                    m.SectorID = n.SectorID;
                    m.SectorCode = n.SectorCode;
                    m.SectorName = n.SectorName;
                    m.StateCode = n.StateCode;
                    m.DistrictCode = n.DistrictCode;
                    m.ProjectCode = n.ProjectCode;
                    m.SupName = n.SupName;
                    m.SupMobile = n.SupMobile;
                    m.SupEmail = n.SupEmail;
                    m.SupAddress = n.SupAddress;
                    list.Add(m);
                    i++;
                }
                return list;
            }
        }

        /// <summary>
        /// This method is used to get Sector record from DB
        /// </summary>
        /// <param name="Sector">Sector object to fetch</param>
        /// <returns>Sector object</returns>
        public VWGetSector Get(int ID)
        {
            using (DBEntities db = new DBEntities())
            {
                var items = db.VWGetSectors.FirstOrDefault(m => m.SectorID == ID);
                return items;
            }
        }

        public string GetSectorName(string secCode = "")
        {
            using (DBEntities db = new DBEntities())
            {
                var items = (from x in db.VWGetSectors where x.SectorCode == secCode select x.SectorName).FirstOrDefault();
                return items;
            }
        }

        /// <summary>
        /// Get active Sector list for listing
        /// </summary>
        /// <returns>Sector List</returns>
        public List<SelectListItem> GetList(string TypeID)
        {
            using (var db = new DBEntities())
            {
                return (from x in db.TblSectors
                        where x.IsActive == true
                        select new SelectListItem { Text = x.SectorName.ToString(), Value = x.SectorCode.ToString() }).ToList();
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
                return (from item in db.TblSectors
                        select new
                        {
                            SectorCode = item.SectorCode,
                            SectorName = item.SectorName,
                            DistrictCode = item.DistrictCode,
                            ProjectCode = item.ProjectCode,
                            IsActive = (item.IsActive == true) ? "Active" : "Inactive"
                        }).ToList<object>();
            }
        }


        public SPUpdateSector_Result UpdateSector(TblSectorMst _sec)
        {
            try
            {
                using (var db = new DBEntities())
                {
                    var Data = db.SPUpdateSector(_sec.StateID, _sec.DistrictID, _sec.ProjectID,_sec.SectorID, _sec.SectorName, Convert.ToInt32(_sec.UserCode),Convert.ToInt16(_sec.CenterType)).FirstOrDefault();
                    return Data;
                }
            }
            catch (Exception ex)
            {
                throw null;
            }
        }
    }
}
