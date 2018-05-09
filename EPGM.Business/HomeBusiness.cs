using EPGM.Data;
using EPGM.Framework;
using EPGM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPGM.Business
{
    public class HomeBusiness
    {
        private readonly HomeData _homeData = new HomeData();

        public List<SpGetEPGMDashboardCount_Result> GetAdminData(SearchFilter search,string StateCode)
        {
            return _homeData.GetAdminData(search, StateCode);
        }

        public DashboardData GetStats(string stateCode, string distCode, string projCode, string secCode, string awcCode,string BeneType, string Month, string Year,string CenterType)
        {
            return _homeData.GetStats(stateCode, distCode, projCode, secCode, awcCode, BeneType, Month,Year, CenterType);
        }

        public SpGetCounts_Result GetCounts(string stateCode)
        {
            return _homeData.GetCounts(stateCode);
        }

        public List<SpGetDistLevelDetails_Result> GetDistrictStats(string stateCode, string distCode, string projCode, string secCode, string awcCode,string BeneType,string Month,string Year,string CenterType)
        {
            return _homeData.GetDistrictStats(stateCode, distCode, projCode, secCode, awcCode, BeneType, Month, Year, CenterType);
        }
        public List<SPBindMenuByUserType_Result> BindMenu(string UserTypeID)
        {
            return _homeData.BindMenu(UserTypeID);
        }
    }
}
