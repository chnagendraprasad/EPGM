//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EPGM.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TblPortalUser
    {
        public int PortalUserID { get; set; }
        public string UserCode { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string MobileNo { get; set; }
        public string UserEmail { get; set; }
        public string Name { get; set; }
        public string RoleCode { get; set; }
        public string StateCode { get; set; }
        public string DistrictCode { get; set; }
        public string ProjectCode { get; set; }
        public string SectorCode { get; set; }
        public string AWCCode { get; set; }
        public bool Status { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public int CenterTypeID { get; set; }
    }
}
