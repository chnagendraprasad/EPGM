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
    public class ProjectData
    {
        /// <summary>
        /// This method is used to get Project records from DB based on search criteria
        /// </summary>
        /// <param name="filter">search criteria</param>
        /// <param name="RecCount">total record count</param>
        /// <returns></returns>
        public List<SpGetEPGMProjectDetails_Result> Get(SearchFilter search, string stateCode, string distCode, string whoType,string BeneType, string Month, string Year,string CenterType = "")
        {
            using (DBEntities db = new DBEntities())
            {
                int i = 1, RecCount = 0;
                var list = new List<SpGetEPGMProjectDetails_Result>();
                var data = db.SpGetEPGMProjectDetails((whoType == "") ? "Total" : Helper.GetGradeCode(whoType), stateCode, distCode, BeneType,  Month,  Year,Convert.ToInt16(CenterType)).ToList();
                foreach (var n in data)
                {
                    var m = new SpGetEPGMProjectDetails_Result();
                    m.Sno = i.ToString();
                    m.DistrictCode = n.DistrictCode;
                    m.ProjectID = n.ProjectID;
                    m.ProjectCode = n.ProjectCode;
                    m.ProjectName = n.ProjectName;
                    m.Cnt = n.Cnt;
                    list.Add(m);
                    i++;
                }
                return list;
            }
        }

        public List<SpGetProjectDetails_Result> GetDataGrid(SearchFilter search, string stateCode, string distCode,string CenterType)
        {
            using (DBEntities db = new DBEntities())
            {
                int i = 1, RecCount = 0;
                var list = new List<SpGetProjectDetails_Result>();
                var data = db.SpGetProjectDetails(stateCode, distCode,Convert.ToInt16(CenterType)).ToList();
                foreach (var n in data)
                {
                    var m = new SpGetProjectDetails_Result();
                    m.Sno = i;
                    m.DistrictCode = n.DistrictCode;
                    m.ProjectID = n.ProjectID;
                    m.ProjectCode = n.ProjectCode;
                    m.ProjectName = n.ProjectName;
                    list.Add(m);
                    i++;
                }
                return list;
            }
        }

        /// <summary>
        /// This method is used to get Project record from DB
        /// </summary>
        /// <param name="Project">Project object to fetch</param>
        /// <returns>Project object</returns>
        public VWGetProject Get(int ID)
        {
            using (DBEntities db = new DBEntities())
            {
                var items = db.VWGetProjects.FirstOrDefault(m => m.ProjectID == ID);
                return items;
            }
        }

        public string GetProjectName(string projCode = "")
        {
            using (DBEntities db = new DBEntities())
            {
                return (from x in db.VWGetProjects where x.ProjectCode == projCode select x.ProjectName).FirstOrDefault();
            }
        }

        /// <summary>
        /// Get active Project list for listing
        /// </summary>
        /// <returns>Project List</returns>
        public List<SelectListItem> GetList(string TypeID)
        {
            using (var db = new DBEntities())
            {
                var result = new List<SelectListItem>();

                if (TypeID == "")
                    result = (from x in db.TblProjects
                              where x.IsActive == true
                              select new SelectListItem { Text = x.ProjectName.ToString(), Value = x.ProjectCode.ToString() }).ToList();
                else
                    result = (from x in db.TblProjects
                              where x.IsActive == true && x.DistrictCode == TypeID
                              select new SelectListItem { Text = x.ProjectName.ToString(), Value = x.ProjectCode.ToString() }).ToList();

                result.Insert(0, new SelectListItem { Text = "-- Select --", Value = "All" });

                return result;
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
                return (from item in db.VWGetProjects
                        select new
                        {
                            ProjectCode = item.ProjectCode,
                            ProjectName = item.ProjectName,
                            DistrictCode = item.DistrictName,
                        }).ToList<object>();
            }
        }

        public SPUpdateProject_Result UpdateProject(TblProjectMst _proj)
        {
            try
            {
                using (var db = new DBEntities())
                {
                    var Data = db.SPUpdateProject(_proj.StateID, _proj.DistrictID, _proj.ProjectID, _proj.ProjectName, Convert.ToInt32(_proj.UserCode),Convert.ToInt16(_proj.CenterType)).FirstOrDefault();
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
