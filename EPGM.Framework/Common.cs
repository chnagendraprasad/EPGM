using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPGM.Models;

namespace EPGM.Framework
{
    public class Common
    {
        public static List<SPGETUserRights_Result> GetUserRights(string userType, string Url)
        {
            List<SPGETUserRights_Result> Data = new List<SPGETUserRights_Result>();
            using (var db = new DBEntities())
            {
                Data = db.SPGETUserRights(Url, Convert.ToInt32(userType)).ToList();
            }
            return Data;
        }

        public static string GenderName(string Gender)
        {
            if (Gender == "M" || Gender.ToUpper() == "MALE")
            {
                return "Male";
            }
            else if (Gender == "F" || Gender.ToUpper() == "FEMALE")
            {
                return "Female";
            }
            else
            {
                return null;
            }
        }

        public static string AadhaarType(string Aadhaar)
        {
            switch (Aadhaar.ToUpper())
            {
                case "C":
                    return "Child";
                case "F":
                    return "Father";
                case "M":
                    return "Mother";
                default:
                    return null;
            }
        }
        public static string BeneCategory(string CatCode)
        {
            using (var db = new DBEntities())
            {
                var Data = db.USP_S_CATEGORY().Where(x => x.CategoryCode == CatCode).ToList();
                if (Data.Count != 0 && Data.Count != null)
                {
                    return Data[0].CategoryName;
                }
                else
                {
                    return null;
                }
            }
        }

        public static string ResidentType(string ResCode)
        {
            switch (ResCode.ToUpper())
            {
                case "T":
                    return "Temporary";
                case "P":
                    return "Permanent";
                default:
                    return null;
            }
        }
    }
}
