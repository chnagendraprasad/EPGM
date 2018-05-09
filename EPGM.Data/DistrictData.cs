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
    public class DistrictData
    {
        /// <summary>
        /// This method is used to get District records from DB based on search criteria and WHO Type
        /// </summary>
        /// <param name="filter">search criteria</param>
        /// <param name="RecCount">total record count</param>
        /// <returns></returns>
        public List<SpGetEPGMDistrictDetails_Result> Get(SearchFilter search, string stateCode, string distCode, string whoType,string BeneType, string Month, string Year,string CenterType)
        {
            using (var db = new DBEntities())
            {
                int i = 1;
                int totalCount = 0;
                //RecCount = db.SpGetEPGMDistDetails(whoType).Count();
                var list = new List<SpGetEPGMDistrictDetails_Result>();
                var data = db.SpGetEPGMDistrictDetails((whoType == "") ? "Total" : Helper.GetGradeCode(whoType), stateCode, BeneType,Month,Year,Convert.ToInt16(CenterType)).ToList();
                foreach (var dt in data)
                {
                    var dist = new SpGetEPGMDistrictDetails_Result();
                    dist.Sno = Convert.ToString(i);
                    dist.DistrictID = dt.DistrictID;
                    dist.DistrictCode = dt.DistrictCode;
                    dist.DistrictName = dt.DistrictName;
                    dist.Cnt = dt.Cnt;
                    totalCount += dt.Cnt ?? 0;
                    list.Add(dist);
                    i++;
                }
                //var disdet = new SpGetEPGMDistrictDetails_Result();
                //disdet.DistrictName = "<b>Total</b>";
                //disdet.Cnt = totalCount;
                //list.Add(disdet);
                return list;
            }
        }

        /// <summary>
        /// This method is used to get District records from DB based on search criteria
        /// </summary>
        /// <param name="filter">search criteria</param>
        /// <param name="RecCount">total record count</param>
        /// <returns></returns>
        public List<SpGetDistrict_Result> GetMasterData(SearchFilter search, string StateCode)
        {
            using (var db = new DBEntities())
            {
                int i = 1;
                var list = new List<SpGetDistrict_Result>();
                var data = (from x in db.SpGetDistrict(StateCode) select x).OrderBy(x => x.DistrictName).ToList();
                foreach (var dt in data)
                {
                    var dist = new SpGetDistrict_Result();
                    dist.Sno = Convert.ToString(i);
                    dist.DistrictID = dt.DistrictID;
                    dist.DistrictCode = dt.DistrictCode;
                    dist.DistrictName = dt.DistrictName;
                    dist.PDName = dt.PDName;
                    dist.PDMobile = dt.PDMobile;
                    dist.PDEmail = dt.PDEmail;
                    dist.PDAddress = dt.PDAddress;
                    list.Add(dist);
                    i++;
                }
                return list;
            }
        }

        public string GetDistrictName(string StateCode, string distCode)
        {
            using (var db = new DBEntities())
            {
                int DID = Convert.ToInt32(distCode);
                var distname = (from x in db.SpGetDistrict(StateCode) where x.DistrictCode == distCode select x.DistrictName).FirstOrDefault();
                return distname;
            }
        }

        /// <summary>
        /// Get active District list for listing
        /// </summary>
        /// <returns>District List</returns>
        public List<SelectListItem> GetList(string StateCode, string TypeID)
        {
            using (var db = new DBEntities())
            {
                var result = new List<SelectListItem>();

                if (string.IsNullOrEmpty(TypeID))
                    result = (from x in db.TblDistricts
                              where x.IsActive == true && x.StateCode == StateCode
                              select new SelectListItem { Text = x.DistrictName.ToString(), Value = x.DistrictCode.ToString() }).OrderBy(x => x.Text).ToList();
                else
                    result = (from x in db.TblDistricts
                              where x.IsActive == true && x.StateCode == StateCode && x.DistrictCode == TypeID
                              select new SelectListItem { Text = x.DistrictName.ToString(), Value = x.DistrictCode.ToString() }).OrderBy(x => x.Text).ToList();

                result.Insert(0, new SelectListItem { Text = "-- Select --", Value = "All" });

                return result;
            }
        }

        /// <summary>
        /// Get active District list for listing
        /// </summary>
        /// <returns>District List</returns>
        public List<SelectListItem> GetListWithoutSelect(string StateCode, string TypeID)
        {
            using (var db = new DBEntities())
            {
                var result = new List<SelectListItem>();

                if (TypeID == "")
                    result = (from x in db.TblDistricts
                              where x.IsActive == true && x.StateCode == StateCode
                              select new SelectListItem { Text = x.DistrictName.ToString(), Value = x.DistrictCode.ToString() }).OrderBy(x => x.Text).ToList();
                else
                    result = (from x in db.TblDistricts
                              where x.IsActive == true && x.StateCode == StateCode && x.DistrictCode == TypeID
                              select new SelectListItem { Text = x.DistrictName.ToString(), Value = x.DistrictCode.ToString() }).OrderBy(x => x.Text).ToList();

                return result;
            }
        }

        public SPUpdateDistrict_Result UpdateDistrict(TblDistrictMst _dist)
        {
            try
            {
                using (var db = new DBEntities())
                {
                    var Data = db.SPUpdateDistrict(_dist.StateID, _dist.DistrictCode, _dist.DistrictName, Convert.ToInt32(_dist.UserCode)).FirstOrDefault();

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
