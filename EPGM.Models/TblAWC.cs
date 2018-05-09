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
    
    public partial class TblAWC
    {
        public int AWCID { get; set; }
        public string AWCCode { get; set; }
        public string AWCDeptCode { get; set; }
        public string AWCName { get; set; }
        public string AWCType { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string AWWCode { get; set; }
        public string AWWName { get; set; }
        public string AWWMobile { get; set; }
        public string AWWType { get; set; }
        public string InchargeAWWCode { get; set; }
        public string SectorCode { get; set; }
        public string ProjectCode { get; set; }
        public string DistrictCode { get; set; }
        public string StateCode { get; set; }
        public string MandalCode { get; set; }
        public string GPCode { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string Langitude { get; set; }
        public string Latitude { get; set; }
        public string AWCImageInBytes { get; set; }
        public string AWCImagePath { get; set; }
        public Nullable<System.DateTime> AWCImageCreatedDate { get; set; }
        public Nullable<System.TimeSpan> AWCImageCreatedTime { get; set; }
        public string IsHierarchyCreated { get; set; }
        public Nullable<System.DateTime> HierarchyCreatedDate { get; set; }
        public string IsHierarchyActived { get; set; }
        public Nullable<System.DateTime> HierarchyActivatedDate { get; set; }
        public string AttendanceEnable { get; set; }
        public Nullable<int> FieldForceStaffID { get; set; }
        public Nullable<System.DateTime> FieldForceStaffCreatedDate { get; set; }
        public int CenterTypeID { get; set; }
    }
}
