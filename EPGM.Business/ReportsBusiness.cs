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
    public class ReportsBusiness
    {
        private readonly ReportsData _RepData = new ReportsData();
        public ChildGrowthCalculator_Result ChildGrowthGradeNames(ChildGrade _grade)
        {
            var Data = _RepData.ChildGrowthGradeNames(_grade);
            return Data;
        }
        public List<SpGetWeightAgeMasterDatabyGender_Result> GetMasterDataWeightAge(SearchFilter search, string Gender)
        {
            return _RepData.GetMasterDataWeightAge(search, Gender);
        }
        public List<SpGetHeightWeightMasterDatabyGender_Result> MasterDataHeightWeight(SearchFilter search, string Gender)
        {
            return _RepData.MasterDataHeightWeight(search, Gender);
        }
        public List<SpGetHeightAgeMasterDatabyGender_Result> MasterDataHeightAge(SearchFilter search, string Gender)
        {
            return _RepData.MasterDataHeightAge(search, Gender);
        }
    }
}
