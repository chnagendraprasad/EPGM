using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Xml.Serialization;

namespace EPGM.Models
{
    public class BeneDetails
    {
        public int BeneID { get; set; }
        public string BeneCode { get; set; }
        public string BeneName { get; set; }
        public string BeneSurname { get; set; }
        public string DOB { get; set; }
        public string Gender { get; set; }
        public string MobileNumber { get; set; }
        public string Contact { get; set; }
        public string AadhaarNo { get; set; }
        public string MotherName { get; set; }
        public string FatherName { get; set; }
        public string BeneClass { get; set; }
        public string BeneType { get; set; }
        public string Photo { get; set; }
        public List<BeneWeighDet> WeightDets { get; set; }
    }

    public class BeneWeighDet
    {
        public string WeighedDate { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string Grade { get; set; }
        public string BMI { get; set; }
    }

    public class NewBene
    {
        public string BeneType { get; set; }
        public string ChildName { get; set; }
        public string ConfirmChildName { get; set; }
        public string MotherName { get; set; }
        public string FatherName { get; set; }
        public string AadhaarCardType { get; set; }
        public string AadhaarNo { get; set; }
        public string RationCardNo { get; set; }
        public string IncomeRange { get; set; }
        public string ContactNo { get; set; }
        public string DOB { get; set; }
        public string Gender { get; set; }
        public string Category { get; set; }
        public string RegisDate { get; set; }
        public string ResidentType { get; set; }
        public string StateID { get; set; }
        public string DistrictID { get; set; }
        public string ProjectID { get; set; }
        public string SectorID { get; set; }
        public string CenterID { get; set; }
        public string CreatedBy { get; set; }
        public string BirthWeight { get; set; }
        public string DeptUID { get; set; }
        public string EDD { get; set; }
        public int CenterType { get; set; }
    }

    public class NewFieldForceStaff
    {
        public string StaffCode { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string MiddleName { get; set; }
        public string DOB { get; set; }
        public string DOJ { get; set; }
        public string Gender { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhotoImagePath { get; set; }
        public string CreatedBy { get; set; }

        public string StateId { get; set; }

        public string Qualification { get; set; }
    }

    public class NewUser
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string MobileNo { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string RoleID { get; set; }
        [Required]
        public string StateID { get; set; }
        [Required]
        public string DistrictID { get; set; }
        [Required]
        public string ProjectID { get; set; }
        [Required]
        public string SectorID { get; set; }
        [Required]
        public string CenterID { get; set; }
        public string Password { get; set; }
        public string UserCode { get; set; }

        public int Type { get; set; }
    }

    public class ChangePassword
    {
        [Required]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
        public string UserCode { get; set; }
    }

    public class ForgotPassword
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Useremail { get; set; }
        public string Password { get; set; }
    }

    public class NewAWC
    {
        [Required]
        public string StateID { get; set; }
        [Required]
        public string DistrictID { get; set; }
        [Required]
        public string ProjectID { get; set; }
        [Required]
        public string SectorID { get; set; }
        [Required]
        public string Center { get; set; }
        [Required]
        public string MobileNo { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string AWCType { get; set; }
        public string UserCode { get; set; }

    }

    public class TblAWCMst
    {
        public string StateID { get; set; }
        [Required]
        public string DistrictID { get; set; }
        [Required]
        public string ProjectID { get; set; }
        [Required]
        public string SectorID { get; set; }
        [Required]
        public string AWCID { get; set; }
        [Required]
        public string AWCName { get; set; }
        [Required]
        public string AWWName { get; set; }
        [Required]
        public string AWWMobileNo { get; set; }
        public string UserCode { get; set; }

        public string StaffID { get; set; }

        public int CenterType { get; set; }

    }

    public class TblSectorMst
    {
        public string SectorID { get; set; }
        public string ProjectID { get; set; }
        [Required]
        public string DistrictID { get; set; }
        [Required]
        public string SectorName { get; set; }
        public string UserCode { get; set; }
        public string StateID { get; set; }

        public string CenterType { get; set; }

    }

    public class TblProjectMst
    {
        public string ProjectID { get; set; }
        [Required]
        public string DistrictID { get; set; }
        [Required]
        public string ProjectName { get; set; }
        public string UserCode { get; set; }
        public string StateID { get; set; }

        public string CenterType { get; set; }
    }

    public class TblDistrictMst
    {
        [Required]
        public string DistrictCode { get; set; }
        [Required]
        public string DistrictName { get; set; }
        [Required]
        public string StateID { get; set; }
        public string UserCode { get; set; }

    }

    public class UpdateBene
    {
        public string AWCCode { get; set; }
        public string BeneCode { get; set; }
        public string BeneName { get; set; }
        public string BeneSurname { get; set; }
        public string MotherName { get; set; }
        public string FatherName { get; set; }
        public string Gender { get; set; }
        public string DOB { get; set; }
        public string AadhaarType { get; set; }
        public string AadhaarNumber { get; set; }
        public string MobileNumber { get; set; }
        public string FatherRationCardNumber { get; set; }
        public Nullable<double> Income { get; set; }
        public string BeneCategory { get; set; }
        public string RegisteredDateTime { get; set; }
        public string ResidentType { get; set; }
        public string StateID { get; set; }
        public string DistrictID { get; set; }
        public string ProjectID { get; set; }
        public string SectorID { get; set; }
        public string AWCID { get; set; }
        public string UserCode { get; set; }
        public string BirthWeight { get; set; }
        public string DeptUID { get; set; }
        public string EDD { get; set; }

        public int CenterType { get; set; }
    }

    public class DashStats
    {
        public string BeneCount { get; set; }
        public string Count { get; set; }
        public string NormalCount { get; set; }
        public string ModerateCount { get; set; }
        public string SevereCount { get; set; }
        public string AbsentCount { get; set; }
        public string AbsPer { get; set; }
        public string NorPer { get; set; }
        public string ModPer { get; set; }
        public string SevPer { get; set; }
    }

    public partial class SpGetUpdatedBeneInfo_Result
    {
        public string DateOfBirth { get; set; }
    }

    public partial class SpGetSectorDetails_Result
    {
        public string Sno { get; set; }
    }

    public partial class SpGetDistLevelDetails_Result
    {
        public int BeneCount { get; set; }
    }

    public partial class SpGetProject_Result
    {
        public string Sno { get; set; }
    }
    public partial class SpGetSector_Result
    {
        public string Sno { get; set; }
    }

    public partial class SpGetAWC_Result
    {
        public string Sno { get; set; }
    }

    public partial class SpGetEPGMDistrictDetails_Result
    {
        public string Sno { get; set; }
    }

    public partial class SpGetEPGMDistDetails_Result
    {
        public string Sno { get; set; }
    }

    public partial class SPGetPWWomenData_Result
    {
        public string Sno { get; set; }
    }
    public partial class SpGetEPGMBeneficiaries_Result
    {
        public string Sno { get; set; }
    }
    public partial class SpGetDistrict_Result
    {
        public string Sno { get; set; }
    }

    public partial class SpGetEPGMBenes_Result
    {
        public string Sno { get; set; }
        public string DateofBirth { get; set; }
    }

  

    public partial class SpGetAllBenesofCenter_Result
    {
        public string Sno { get; set; }
    }

    public class TblUser
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class DashboardData
    {
        public string Count { get; set; }
        public string BeneCount { get; set; }

        public string WANormalCount { get; set; }
        public string WAModerateCount { get; set; }
        public string WASevereCount { get; set; }

        public string HWNormalCount { get; set; }
        public string HWModerateCount { get; set; }
        public string HWSevereCount { get; set; }

        public string HANormalCount { get; set; }
        public string HAModerateCount { get; set; }
        public string HASevereCount { get; set; }

        public string AbsentCount { get; set; }
        public string CentersCount { get; set; }
        public string AttendanceCount { get; set; }

        public string TotalChildren { get; set; }
        public string PregnantWomen { get; set; }
        public string LacatingWomen { get; set; }
        public string Child0to3 { get; set; }
        public string Child3to6 { get; set; }
        public string Others { get; set; }
    }

    public class ProjLoad
    {
        public string DistrictID { get; set; }
        public List<SelectListItem> Districts { get; set; }
    }

    public class SecLoad
    {
        public string DistrictID { get; set; }
        public string ProjectID { get; set; }
        public List<SelectListItem> Districts { get; set; }
        public List<SelectListItem> Projects { get; set; }
    }

    public class AWCLoad
    {
        public string DistrictID { get; set; }
        public string ProjectID { get; set; }
        public string SectorID { get; set; }
        public List<SelectListItem> Districts { get; set; }
        public List<SelectListItem> Projects { get; set; }
        public List<SelectListItem> Sectors { get; set; }
    }

    public class ChartData
    {
        public string value { get; set; }
        public string color { get; set; }
        public string highlight { get; set; }
        public string label { get; set; }
    }

    public class LoginUserDetails
    {
        public string DistrictCode { get; set; }
        public string District { get; set; }
        public string ProjectCode { get; set; }
        public string ProjectName { get; set; }
        public string SectorCode { get; set; }
        public string SectorName { get; set; }
        public string CenterCode { get; set; }
        public string CenterName { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int PortalUserID { get; set; }
        public string Password { get; set; }
        public int RoleID { get; set; }
        public string StateCode { get; set; }
        public string UserCode { get; set; }
        public bool UserStatus { get; set; }
        public string StatusCode { get; set; }
        public string StatusMessage { get; set; }

        public int CenterType { get; set; }
    }

    public class AWCClass
    {
        public string SectorCode { get; set; }
    }

    public class ProjClass
    {
        public string DistrictCode { get; set; }
    }

    public class SecClass
    {
        public string DistrictCode { get; set; }
        public string ProjectCode { get; set; }
    }


    public partial class SpGetAllBeneficiaryDetails_Result
    {
        public string Sno { get; set; }
    }

    public partial class SpGetEPGMProjectDetails_Result
    {
        public string Sno { get; set; }
    }
    public partial class SpGetEPGMSectorDetails_Result
    {
        public string Sno { get; set; }
    }
    public partial class SpGetEPGMAWCDetails_Result
    {
        public string Sno { get; set; }
    }

    public class GetDashStats
    {
        public string Total { get; set; }
        public string TotalCenters { get; set; }
        public string AttendanceCount { get; set; }
        public string BeneCount { get; set; }
        public string Absent { get; set; }
        public string Normal { get; set; }
        public string Moderate { get; set; }
        public string Severe { get; set; }
    }

    public class AdminDashboardData
    {
        public string Count { get; set; }
        public string BeneCount { get; set; }
        public string Total { get; set; }
        public string TotalCenters { get; set; }

        public string WANormalCount { get; set; }
        public string WAModerateCount { get; set; }
        public string WASevereCount { get; set; }

        public string HWNormalCount { get; set; }
        public string HWModerateCount { get; set; }
        public string HWSevereCount { get; set; }

        public string HANormalCount { get; set; }
        public string HAModerateCount { get; set; }
        public string HASevereCount { get; set; }

        public string AbsentCount { get; set; }
        public string CentersCount { get; set; }
        public string AttendanceCount { get; set; }

        public string WANorPer { get; set; }
        public string WAModPer { get; set; }
        public string WASevPer { get; set; }

        public string HWNorPer { get; set; }
        public string HWModPer { get; set; }
        public string HWSevPer { get; set; }

        public string HANorPer { get; set; }
        public string HAModPer { get; set; }
        public string HASevPer { get; set; }

        public string Normal { get; set; }
        public string Moderate { get; set; }
        public string Severe { get; set; }

        public string HWNor { get; set; }
        public string HWMod { get; set; }
        public string HWSev { get; set; }

        public string HANor { get; set; }
        public string HAMod { get; set; }
        public string HASev { get; set; }

        public string TotalChildren { get; set; }
        public string PregnantWomen { get; set; }
        public string LacatingWomen { get; set; }
        public string Child0to3 { get; set; }
        public string Child3to6 { get; set; }
        public string Others { get; set; }
    }

    public class ChildGrade
    {
        public string Gender { get; set; }
        public string DOB { get; set; }
        public string Weight { get; set; }
        public string Height { get; set; }
        public string BeneType { get; set; }
    }

    public class AddMenu
    {
        [Required]
        public string MenuName { get; set; }
        [Required]
        public string URL { get; set; }
        [Required]
        public string MenuType { get; set; }
        public string Parent { get; set; }

        [Required]
        public string Role { get; set; }
        public string UserCode { get; set; }
    }
    #region AddUser XML Output

    [XmlRoot(ElementName = "task")]
    public class Task
    {
        [XmlAttribute(AttributeName = "value")]
        public string Value { get; set; }
    }

    [XmlRoot(ElementName = "user")]
    public class User
    {
        [XmlElement(ElementName = "username")]
        public string Username { get; set; }
        [XmlElement(ElementName = "unique_id")]
        public string Unique_id { get; set; }
        [XmlElement(ElementName = "first_name")]
        public string First_name { get; set; }
        [XmlElement(ElementName = "last_name")]
        public string Last_name { get; set; }
        [XmlElement(ElementName = "email")]
        public string Email { get; set; }
        [XmlElement(ElementName = "mobile")]
        public string Mobile { get; set; }
        [XmlElement(ElementName = "ack")]
        public string Ack { get; set; }
    }

    [XmlRoot(ElementName = "users")]
    public class Users
    {
        [XmlElement(ElementName = "user")]
        public User User { get; set; }
    }

    [XmlRoot(ElementName = "state")]
    public class State
    {
        [XmlElement(ElementName = "users")]
        public Users Users { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
    }

    [XmlRoot(ElementName = "states")]
    public class States
    {
        [XmlElement(ElementName = "state")]
        public State State { get; set; }
    }

    [XmlRoot(ElementName = "organization")]
    public class Organization
    {
        [XmlElement(ElementName = "states")]
        public States States { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
    }

    [XmlRoot(ElementName = "organizations")]
    public class Organizations
    {
        [XmlElement(ElementName = "task")]
        public Task Task { get; set; }
        [XmlElement(ElementName = "organization")]
        public Organization Organization { get; set; }
    }

    #endregion

    #region Face API
    public class ResponseStatus
    {
        public string statusCode { get; set; }
        public string code { get; set; }
        public string message { get; set; }
        public string Result { get; set; }
        public string PersonID { get; set; }
    }

    public class FaceAPIResponse
    {
        public ResponseStatus error { get; set; }
        public string PersonID { get; set; }
        public string persistedFaceId { get; set; }
    }
    public class PersonGroupStatus
    {
        public ResponseStatus error { get; set; }
        public string status { get; set; }
        public string createdDateTime { get; set; }
        public string lastActionDateTime { get; set; }
        public string message { get; set; }
    }

    public class FaceAPIReg : UserData
    {
        public string BeneCode { get; set; }
        public string PersonID { get; set; }
        public string PersistedFaceID { get; set; }
        public string BeneName { get; set; }
        public string MotherName { get; set; }

        public int CenterType { get; set; }
    }
    public class UserData
    {
        public string StateCode { get; set; }
        public string DistrictCode { get; set; }
        public string ProjectCode { get; set; }
        public string SectorCode { get; set; }
        public string AWCCode { get; set; }
        public string UserCode { get; set; }
    }

    #endregion

}
