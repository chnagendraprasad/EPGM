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
    public class BeneBusiness
    {
        /// <summary>
        /// Data access object for AWC
        /// </summary>
        private readonly BeneData _beneData = new BeneData();

        /// <summary>
        /// Business method to get AWC list
        /// </summary>
        /// <param name="search">Search Criteria</param>
        /// <param name="RecCount">Record count</param>
        /// <returns>AWC List</returns>
        public List<SpGetEPGMBeneficiaries_Result> Get(SearchFilter search, out int RecCount, string stateCode, string distCode, string projCode, string secCode, string awcCode, string whoType, string Month, string Year, string Typebene,string CenterType)
        {
            return _beneData.GetBeneficiaries(search, out RecCount, stateCode, distCode, projCode, secCode, awcCode, whoType, Month, Year, Typebene, CenterType);
        }

        //public List<SpGetEPGMBenes_Result> GetData(string distCode, string whoType)
        //{
        //    return _beneData.Get(distCode, whoType);
        //}

        /// <summary>
        /// Business method to get AWC list
        /// </summary>
        /// <param name="search">Search Criteria</param>
        /// <param name="RecCount">Record count</param>
        /// <returns>AWC List</returns>
        public List<SpGetEPGMBenes_Result> GetMasterData(SearchFilter search, out int RecCount, string stateCode, string distCode, string projCode, string secCode, string awcCode, string whoType, string month, string year, string BeneType,string CenterType)
        {
            return _beneData.GetMasterData(search, out RecCount, stateCode, distCode, projCode, secCode, awcCode, whoType, month, year, BeneType, CenterType);
        }
        public List<SpGetAllBenesofCenter_Result> GetBenesofCenter(SearchFilter search, out int RecCount, string stateCode, string distCode, string projCode, string secCode, string awcCode, string BeneType,string CenterType)
        {
            return _beneData.GetBenesofCenter(search, out RecCount, stateCode, distCode, projCode, secCode, awcCode, BeneType, CenterType);
        }

        public SpUpdateStatusOfBene_Result UpdateStatusOfBene(string stateCode, string distCode, string projCode, string secCode, string awcCode, string BeneCode, string Status,string centertype)
        {
            return _beneData.UpdateStatusOfBene(stateCode, distCode, projCode, secCode, awcCode, BeneCode, Status, centertype);
        }

        public SpGetUpdatedBeneInfo_Result GetBeneDetails(string beneCode)
        {
            return _beneData.GetBeneDetails(beneCode);
        }
        public ResponseStatus InsertBeneDetails(NewBene newbene)
        {
            return _beneData.InsertBeneDetails(newbene);
        }


        public USP_Update_BENEFICIARY_Result UpdateBeneDetails(UpdateBene _Update)
        {
            return _beneData.UpdateBeneDetails(_Update);
        }

        public List<object> GetMasterDataExport(SearchFilter search, out int RecCount, string stateCode, string distCode, string projCode, string secCode, string awcCode, string whoType, string month, string year, string BeneType,string CenterType)
        {
            return _beneData.GetMasterDataExport(search, out RecCount, stateCode, distCode, projCode, secCode, awcCode, whoType, month, year, BeneType, CenterType);
        }
        public List<SpGetAllBeneficiaryDetails_Result> GetBeneficiaryDetails(SearchFilter search, out int RecCount, string stateCode, string distCode, string projCode, string secCode, string awcCode, string BeneType,string CenterType = "")
        {
            return _beneData.GetBeneficiaryDetails(search, out RecCount, stateCode, distCode, projCode, secCode, awcCode, BeneType, CenterType);
        }

        public List<SPGetPWWomenData_Result> GetPWWomenData(SearchFilter search, out int RecCount, string stateCode, string distCode, string projCode, string secCode, string awcCode, string BeneType,string CenterType)
        {
            return _beneData.GetPWWomenData(search, out RecCount, stateCode, distCode, projCode, secCode, awcCode, BeneType, CenterType);
        }

        public ResponseStatus UpdatePWType(string BeneID = "", string BeneCode = "", string BeneType = "")
        {
            return _beneData.UpdatePWType(BeneID, BeneCode, BeneType);
        }

        #region Face Api for Beneficiary Creation ,Activation and Deletion
        public ResponseStatus RegisterPerson(FaceAPIReg _FaceAPIReg)
        {
            return _beneData.RegisterPerson(_FaceAPIReg);
        }
        public ResponseStatus EnrollPerson(FaceAPIReg _FaceAPIReg)
        {
            return _beneData.EnrollPerson(_FaceAPIReg);
        }
        public ResponseStatus DeletePersonFace(FaceAPIReg _FaceAPIReg)
        {
            return _beneData.DeletePersonFace(_FaceAPIReg);
        }

        public ResponseStatus DeletePerson(FaceAPIReg _FaceAPIReg)
        {
            return _beneData.DeletePerson(_FaceAPIReg);
        }

        #endregion
    }
}
