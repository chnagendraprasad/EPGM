using EPGM.Framework;
using EPGM.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EPGM.Data
{
    public class FieldForceData
    {
        public ResponseStatus InsertFFStaffDetails(NewFieldForceStaff newFFStaff)
        {
            ResponseStatus Response = new ResponseStatus { statusCode = "999", message = "unable to Connect to server" };
            try
            {
                //var ftpStatus = "";
                //var AttendanceStatus = "";
                string data ;
               // data = 0;
                DateTime? dob = null;
                DateTime? doj = null;

                using (var db = new DBEntities())
                {
                    if (newFFStaff.DOB != "" && newFFStaff.DOB != null)
                    {
                        dob = Convert.ToDateTime(newFFStaff.DOB, System.Globalization.CultureInfo.InvariantCulture);
                    }

                    if (newFFStaff.DOJ != "" && newFFStaff.DOJ != null)
                    {
                        doj = Convert.ToDateTime(newFFStaff.DOJ, System.Globalization.CultureInfo.InvariantCulture);
                    }

                    data = db.SpCreateFieldStaff(newFFStaff.StaffCode, Helper.Encrypt(newFFStaff.Password), newFFStaff.Name, newFFStaff.SurName, newFFStaff.MiddleName,
                                                    Convert.ToDateTime(DateTime.ParseExact(newFFStaff.DOB, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)),
                                                    Convert.ToDateTime(DateTime.ParseExact(newFFStaff.DOJ, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)),
                                                    newFFStaff.Gender, newFFStaff.MobileNo, newFFStaff.Email, newFFStaff.Address, newFFStaff.PhotoImagePath,Convert.ToInt32(newFFStaff.CreatedBy), newFFStaff.StateId,newFFStaff.Qualification).FirstOrDefault();

                    if (data != null && data != "" && data != "999")
                    {
                        Response.statusCode = "000";
                        Response.message = "Successfully  Created new field force staff & User Name: " + newFFStaff.StaffCode.ToString();
                        return Response;
                    }
                    else if (data != null && data != "" && data == "999")
                    {
                        Response.statusCode = "999";
                        Response.message = "Staff Login UserName is already exists !";
                        return Response;
                    }
                    else
                    {
                        Response.statusCode = "999";
                        Response.message = "Failed to create new field force staff !" ;
                        return Response;
                    }
                }
            }
            catch (Exception ex)
            {
                Response.statusCode = "999";
                Response.message = ex.Message.ToString();
                return Response;
            }
        }

        public List<SpFieldForceStaffDetails_Result> GetFieldForceStaff(SearchFilter search, out int RecCount, string stateCode)
        {
            using (DBEntities db = new DBEntities())
            {
                int i = 1; RecCount = 0;
               // var list = new List<SpFieldForceStaffDetails_Result>();
                var data = db.SpFieldForceStaffDetails(stateCode).ToList();
                
                return data;
            }
        }

        public SpUpdateStatusOfFieldForce_Result FielForceStatusChange(string StaffId, string StateCode, string enabled)
        {
            try
            {
                using (DBEntities db = new DBEntities())
                {
                    var data = db.SpUpdateStatusOfFieldForce(StateCode, Convert.ToInt32(StaffId), enabled).FirstOrDefault();
                    return data;
                }
            }
            catch(Exception ex)
            {
                SpUpdateStatusOfFieldForce_Result data = new SpUpdateStatusOfFieldForce_Result();

                return data;


            }
           
        }
    }
}
