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
    
    public partial class SpGetDistLevelDetails_Result
    {
        public Nullable<long> Row { get; set; }
        public string DistrictName { get; set; }
        public int DistrictID { get; set; }
        public string DistrictCode { get; set; }
        public string ProjectName { get; set; }
        public string ProjectCode { get; set; }
        public string SectorName { get; set; }
        public string SectorCode { get; set; }
        public string AWCCode { get; set; }
        public string AWCName { get; set; }
        public Nullable<int> Total { get; set; }
        public Nullable<int> Absent { get; set; }
        public Nullable<int> Normal { get; set; }
        public Nullable<int> Moderate { get; set; }
        public Nullable<int> Severe { get; set; }
        public Nullable<int> HWNormal { get; set; }
        public Nullable<int> HWModerate { get; set; }
        public Nullable<int> HWSevere { get; set; }
        public Nullable<int> HANormal { get; set; }
        public Nullable<int> HAModerate { get; set; }
        public Nullable<int> HASevere { get; set; }
        public Nullable<int> TotalAttendance { get; set; }
    }
}
