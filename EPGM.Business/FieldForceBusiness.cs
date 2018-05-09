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
    public class FieldForceBusiness
    {
        private readonly FieldForceData _beneData = new FieldForceData();
        public ResponseStatus InsertFieldForceStaffDetails(NewFieldForceStaff newFFStaff)
        {
            return _beneData.InsertFFStaffDetails(newFFStaff);
        }

        public List<SpFieldForceStaffDetails_Result> GetFieldForceStaff(SearchFilter search, out int RecCount, string stateCode)
        {
            return _beneData.GetFieldForceStaff(search, out RecCount, stateCode);
        }

        public SpUpdateStatusOfFieldForce_Result FielForceStatusChange(string StaffId, string StateCode, string enabled)
        {
            return _beneData.FielForceStatusChange(StaffId, StateCode, enabled);
        }
    }
}
