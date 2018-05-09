using EPGM.Framework;
using EPGM.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EPGM.Data
{
    public class HomeData
    {
        public List<SpGetEPGMDashboardCount_Result> GetAdminData(SearchFilter search, string StateCode)
        {
            using (var db = new DBEntities())
            {
                int i = 1;
                var result = new List<SpGetEPGMDashboardCount_Result>();
                var data = db.SpGetEPGMDashboardCount(StateCode).ToList();
                foreach (var dt in data)
                {
                    var dat = new SpGetEPGMDashboardCount_Result();
                    dat.Sno = i;
                    dat.DistrictID = dt.DistrictID;
                    dat.DistrictCode = dt.DistrictCode;
                    dat.DistrictName = dt.DistrictName;
                    dat.ProjectsCount = dt.ProjectsCount;
                    dat.SectorsCount = dt.SectorsCount;
                    dat.AWCCount = dt.AWCCount;
                    result.Add(dat);
                    i++;
                }
                return result;
            }
        }

        public DashboardData GetStats(string stateCode, string distCode, string projCode, string secCode, string awcCode, string BeneType, string Month, string Year,string CenterType)
        {
            using (var db = new DBEntities())
            {
                var data = db.SpGetDashboardStat_Portal(stateCode, distCode, projCode, secCode, awcCode, Month, Year,Convert.ToInt16(CenterType)).FirstOrDefault();
                var result = new DashboardData();
                result.AbsentCount = Convert.ToString(data.Absent);
                result.BeneCount = Convert.ToString(data.Total);

                result.WANormalCount = Convert.ToString(data.Normal);
                result.WAModerateCount = Convert.ToString(data.Moderate);
                result.WASevereCount = Convert.ToString(data.Severe);

                result.HWNormalCount = Convert.ToString(data.HWNormal);
                result.HWModerateCount = Convert.ToString(data.HWModerate);
                result.HWSevereCount = Convert.ToString(data.HWSevere);

                result.HANormalCount = Convert.ToString(data.HANormal);
                result.HAModerateCount = Convert.ToString(data.HAModerate);
                result.HASevereCount = Convert.ToString(data.HASevere);
                result.CentersCount = Convert.ToString(data.TotalCenters);
                result.AttendanceCount = Convert.ToString(data.TotalAttendance);
                //int abs = (data.Normal ?? 0) + (data.Moderate ?? 0) + (data.Severe ?? 0);
                result.TotalChildren = data.Children.ToString();
                result.PregnantWomen = data.PregnantWomen.ToString();
                result.LacatingWomen = data.LactatingWomen.ToString();
                result.Child0to3 = data.Child0to3.ToString();
                result.Child3to6 = data.Child3to6.ToString();
                result.Others = data.Others.ToString();
                int tot = (data.Total ?? 0) - (data.Absent ?? 0);

                result.Count = Convert.ToString(tot);
                return result;
            }
        }

        public SpGetCounts_Result GetCounts(string stateCode)
        {
            using (var db = new DBEntities())
            {
                var data = db.SpGetCounts(stateCode).FirstOrDefault();
                return data;
            }
        }

        public List<SpGetDistLevelDetails_Result> GetDistrictStats(string stateCode, string distCode, string projCode, string secCode, string awcCode, string BeneType, string Month, string Year,string CenterType)
        {
            using (var db = new DBEntities())
            {
                int totCnt = 0, absCnt = 0, norCnt = 0, modCnt = 0, sevCnt = 0, monDonTotal = 0, AttendenceCount = 0;
                var finalData = new List<SpGetDistLevelDetails_Result>();
                var data = db.SpGetDistLevelDetails(stateCode, distCode, projCode, secCode, awcCode, BeneType, Month, Year,Convert.ToInt16(CenterType)).ToList();
                for (int i = 0; i < data.Count; i++)
                {
                    var rest = new SpGetDistLevelDetails_Result();
                    rest.Row = data[i].Row;
                    rest.DistrictID = data[i].DistrictID;
                    rest.DistrictName = data[i].DistrictName;
                    rest.DistrictCode = data[i].DistrictCode;
                    rest.ProjectName = data[i].ProjectName;
                    rest.ProjectCode = data[i].ProjectCode;
                    rest.SectorName = data[i].SectorName;
                    rest.SectorCode = data[i].SectorCode;
                    rest.AWCName = data[i].AWCName;
                    rest.AWCCode = data[i].AWCCode;
                    rest.BeneCount = data[i].Total ?? 0;
                    int abs = (data[i].Normal ?? 0) + (data[i].Moderate ?? 0) + (data[i].Severe ?? 0);
                    rest.Total = abs;
                    rest.Absent = data[i].Absent ?? 0;
                    rest.Normal = data[i].Normal ?? 0;
                    rest.Moderate = data[i].Moderate ?? 0;
                    rest.Severe = data[i].Severe ?? 0;
                    rest.HWNormal = data[i].HWNormal ?? 0;
                    rest.HWModerate = data[i].HWModerate ?? 0;
                    rest.HWSevere = data[i].HWSevere ?? 0;
                    rest.HANormal = data[i].HANormal ?? 0;
                    rest.HAModerate = data[i].HAModerate ?? 0;
                    rest.HASevere = data[i].HASevere ?? 0;
                    rest.TotalAttendance = data[i].TotalAttendance ?? 0;
                    finalData.Add(rest);
                    monDonTotal += abs;
                    totCnt += data[i].Total ?? 0;
                    absCnt += data[i].Absent ?? 0;
                    norCnt += data[i].Normal ?? 0;
                    modCnt += data[i].Moderate ?? 0;
                    sevCnt += data[i].Severe ?? 0;
                    AttendenceCount += data[i].TotalAttendance ?? 0;
                }
                //var result = new SpGetDistLevelDetails_Result();
                //result.DistrictID = 0;
                //result.DistrictName = "Total";
                //result.BeneCount = totCnt;
                //result.Total = monDonTotal;
                //result.Absent = absCnt;
                //result.Normal = norCnt;
                //result.Moderate = modCnt;
                //result.Severe = sevCnt;
                //finalData.Add(result);
                return finalData;
            }
        }

        public List<SPBindMenuByUserType_Result> BindMenu(string UserTypeID)
        {
            using (var db = new DBEntities())
            {
                var data = db.SPBindMenuByUserType(Convert.ToInt32(UserTypeID)).ToList();
                return data;
            }

        }


    }
}
