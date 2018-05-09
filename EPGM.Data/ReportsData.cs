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
    public class ReportsData
    {
        public ChildGrowthCalculator_Result ChildGrowthGradeNames(ChildGrade _grade)
        {
            try
            {
                using (var db = new DBEntities())
                {
                    var Data = db.ChildGrowthCalculator(Convert.ToDouble(_grade.Weight), Convert.ToDouble(_grade.Height),
                       Convert.ToDateTime(DateTime.ParseExact(_grade.DOB, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)),
                       _grade.Gender, _grade.BeneType).FirstOrDefault();

                    return Data;
                }
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }

        public List<SpGetWeightAgeMasterDatabyGender_Result> GetMasterDataWeightAge(SearchFilter search, string Gender)
        {
            using (DBEntities db = new DBEntities())
            {
                var data = db.SpGetWeightAgeMasterDatabyGender(Gender).ToList();
                return data;

            }
        }

        public List<SpGetHeightWeightMasterDatabyGender_Result> MasterDataHeightWeight(SearchFilter search, string Gender)
        {
            using (DBEntities db = new DBEntities())
            {
                var data = db.SpGetHeightWeightMasterDatabyGender(Gender).ToList();
                return data;

            }
        }
        public List<SpGetHeightAgeMasterDatabyGender_Result> MasterDataHeightAge(SearchFilter search, string Gender)
        {
            using (DBEntities db = new DBEntities())
            {
                var data = db.SpGetHeightAgeMasterDatabyGender(Gender).ToList();
                return data;

            }
        }

    }

}
