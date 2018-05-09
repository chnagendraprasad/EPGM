using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Face.Contract;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace EPGM.Models
{
    public class FaceAPIs
    {
        private readonly IFaceServiceClient faceServiceClient = new FaceServiceClient(ConfigurationManager.AppSettings["FaceAPIPrimaryKey"], ConfigurationManager.AppSettings["FaceAPIURL"]);


        static byte[] GetImageAsByteArray(string imageFilePath)
        {
            FileStream fileStream = new FileStream(imageFilePath, FileMode.Open);
            BinaryReader binaryReader = new BinaryReader(fileStream);
            return binaryReader.ReadBytes((int)fileStream.Length);
        }

        public ResponseStatus CreateGroupRequest(string AWCCode, string AWCName)
        {
            var client = new HttpClient();

            // Request headers - replace this example key with your valid key.
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ConfigurationManager.AppSettings["FaceAPIPrimaryKey"]);

            // Request URI string.
            string uri = ConfigurationManager.AppSettings["FaceAPIURL"] + "persongroups/" + AWCCode;

            // Here "name" is for display and doesn't have to be unique. Also, "userData" is optional.
            var json = new
            {
                name = AWCName,
                userData = ""
            };

            string Data = new JavaScriptSerializer().Serialize(json);
            HttpContent content = new StringContent(Data);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var FaceResult = client.PutAsync(uri, content).Result;
            var xx = FaceResult.Content.ReadAsStringAsync();
            FaceAPIResponse Response = JsonConvert.DeserializeObject<FaceAPIResponse>(xx.Result);
            ResponseStatus Result = new ResponseStatus();
            if (xx.Result == "")
            {
                Result.statusCode = "000";
                Result.message = "Hierarchy Created Successfully & Hierrchy ID :" + AWCCode;
                return Result;
            }
            else
            {
                Result = Response.error;
                Result.statusCode = Response.error.code == null ? Response.error.statusCode : Response.error.code;
                return Result;
            }
        }

        public ResponseStatus TrainGroupRequest(string AWCCode)
        {

            var client = new HttpClient();

            // Request headers - replace this example key with your valid key.
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ConfigurationManager.AppSettings["FaceAPIPrimaryKey"]);

            // Request URI string.        
            var uri = ConfigurationManager.AppSettings["FaceAPIURL"] + "persongroups/" + AWCCode + "/train";

            // Here "name" is for display and doesn't have to be unique. Also, "userData" is optional.
            var json = "";

            string Data = new JavaScriptSerializer().Serialize(json);
            HttpContent content = new StringContent(Data);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var FaceResult = client.PostAsync(uri, content).Result;
            var xx = FaceResult.Content.ReadAsStringAsync();
            FaceAPIResponse Response = JsonConvert.DeserializeObject<FaceAPIResponse>(xx.Result);
            ResponseStatus Result = new ResponseStatus();
            if (xx.Result == "")
            {
                Result.statusCode = "000";
                Result.message = "Hierarchy Activated Successfully & Hierrchy ID :" + AWCCode;
                return Result;
            }
            else
            {
                Result = Response.error;
                Result.statusCode = Response.error.code == null ? Response.error.statusCode : Response.error.code;
                return Result;
            }


        }

        public ResponseStatus CreatePersonRequest(FaceAPIReg _FaceAPIReg)
        {

            var client = new HttpClient();

            // Request headers - replace this example key with your valid key.
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ConfigurationManager.AppSettings["FaceAPIPrimaryKey"]);

            // Request URI string.        
            var uri = ConfigurationManager.AppSettings["FaceAPIURL"] + "persongroups/" + _FaceAPIReg.AWCCode + "/persons";

            // Here "name" is for display and doesn't have to be unique. Also, "userData" is optional.
            var json = new
            {
                name = _FaceAPIReg.BeneName,
                userData = _FaceAPIReg.BeneCode + "-" + _FaceAPIReg.MotherName
            };

            string Data = new JavaScriptSerializer().Serialize(json);
            HttpContent content = new StringContent(Data);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var FaceResult = client.PostAsync(uri, content).Result;
            var xx = FaceResult.Content.ReadAsStringAsync();
            FaceAPIResponse Response = JsonConvert.DeserializeObject<FaceAPIResponse>(xx.Result);
            ResponseStatus Result = new ResponseStatus();
            if (Response.PersonID != "" && Response.PersonID != null)
            {
                Result.PersonID = Response.PersonID;
                Result.statusCode = "000";
                Result.message = "Person Registered Successfully & PersonID :" + Response.PersonID;
                return Result;
            }
            else
            {
                Result = Response.error;
                Result.statusCode = Response.error.code == null ? Response.error.statusCode : Response.error.code;
                return Result;
            }


        }

        public ResponseStatus EnrollPersonRequest(FaceAPIReg _FaceAPIReg)
        {

            var client = new HttpClient();

            // Request headers - replace this example key with your valid key.
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "37315d4c2c684d8083797477bff4646c");

            // Request parameters and URI string.
            string queryString = "userData=001Image";
            var uri = ConfigurationManager.AppSettings["FaceAPIURL"] + "persongroups/" + _FaceAPIReg.AWCCode + "/persons/" + _FaceAPIReg.PersonID + "/persistedFaces";

            // Request body. Try this sample with a locally stored JPEG image.
            byte[] byteData = GetImageAsByteArray(System.Web.HttpContext.Current.Server.MapPath(@"~/Content/images/officials/modi.png"));

            using (var content = new ByteArrayContent(byteData))
            {
                // This example uses content type "application/octet-stream".
                // The other content types you can use are "application/json" and "multipart/form-data".
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                var FaceResult = client.PostAsync(uri, content).Result;
                var xx = FaceResult.Content.ReadAsStringAsync();
                FaceAPIResponse Response = JsonConvert.DeserializeObject<FaceAPIResponse>(xx.Result);
                ResponseStatus Result = new ResponseStatus();
                if (Response.persistedFaceId != "" && Response.persistedFaceId != null)
                {
                    Result.PersonID = Response.persistedFaceId;
                    Result.statusCode = "000";
                    Result.message = "Person Enrolled Successfully & persistedFaceId :" + Response.persistedFaceId;
                    return Result;
                }
                else
                {
                    Result = Response.error;
                    Result.statusCode = Response.error.code == null ? Response.error.statusCode : Response.error.code;
                    return Result;
                }
            }

        }

        public ResponseStatus DeletePersonFaceRequest(string AWCCode, string PersonID, string PersistedFaceID)
        {

            var client = new HttpClient();

            // Request headers - replace this example key with your valid key.
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ConfigurationManager.AppSettings["FaceAPIPrimaryKey"]);

            // Request parameters and URI string.
            var uri = ConfigurationManager.AppSettings["FaceAPIURL"] + "persongroups/" + AWCCode + "/persons/" + PersonID + "/persistedFaces/" + PersistedFaceID;

            // Request body. Try this sample with a locally stored JPEG image.
            //byte[] byteData = GetImageAsByteArray(System.Web.HttpContext.Current.Server.MapPath(@"~/Content/images/officials/modi.png"));
            string json = "";

            string Data = new JavaScriptSerializer().Serialize(json);
            HttpContent content = new StringContent(Data);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var FaceResult = client.DeleteAsync(uri).Result;
            var xx = FaceResult.Content.ReadAsStringAsync();
            if (FaceResult.ReasonPhrase == "OK")
            {
                FaceAPIResponse Response = JsonConvert.DeserializeObject<FaceAPIResponse>(xx.Result);
                ResponseStatus Result = new ResponseStatus();
                Result.statusCode = "000";
                Result.message = "Person Face Deleted Successfully";
                return Result;
            }
            else
            {
                FaceAPIResponse Response = JsonConvert.DeserializeObject<FaceAPIResponse>(xx.Result);
                ResponseStatus Result = new ResponseStatus();
                Result = Response.error;
                Result.statusCode = Response.error.code == null ? Response.error.statusCode : Response.error.code;
                return Result;
            }
        }

        public ResponseStatus DeletePersonRequest(string AWCCode, string PersonID)
        {
            var client = new HttpClient();

            // Request headers - replace this example key with your valid key.
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ConfigurationManager.AppSettings["FaceAPIPrimaryKey"]);

            // Request URI string.        
            var uri = ConfigurationManager.AppSettings["FaceAPIURL"] + "persongroups/" + AWCCode + "/persons/" + PersonID;
            // Here "name" is for display and doesn't have to be unique. Also, "userData" is optional.
            string json = "";
            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var FaceResult = client.DeleteAsync(uri).Result;
            var xx = FaceResult.Content.ReadAsStringAsync();
            if (FaceResult.ReasonPhrase == "OK")
            {
                FaceAPIResponse Response = JsonConvert.DeserializeObject<FaceAPIResponse>(xx.Result);
                ResponseStatus Result = new ResponseStatus();
                Result.statusCode = "000";
                Result.message = "Person Deleted Successfully";
                return Result;
            }
            else
            {
                FaceAPIResponse Response = JsonConvert.DeserializeObject<FaceAPIResponse>(xx.Result);
                ResponseStatus Result = new ResponseStatus();
                Result = Response.error;
                Result.statusCode = Response.error.code == null ? Response.error.statusCode : Response.error.code;
                return Result;
            }
        }

        public ResponseStatus DeletePersonGroupRequest(string AWCCode)
        {
            var client = new HttpClient();

            // Request headers - replace this example key with your valid key.
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ConfigurationManager.AppSettings["FaceAPIPrimaryKey"]);

            // Request URI string.        
            var uri = ConfigurationManager.AppSettings["FaceAPIURL"] + "persongroups/" + AWCCode;

            // Here "name" is for display and doesn't have to be unique. Also, "userData" is optional.
            string json = "";
            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var FaceResult = client.DeleteAsync(uri).Result;
            var xx = FaceResult.Content.ReadAsStringAsync();
            if (FaceResult.ReasonPhrase == "OK")
            {
                FaceAPIResponse Response = JsonConvert.DeserializeObject<FaceAPIResponse>(xx.Result);
                ResponseStatus Result = new ResponseStatus();
                Result.statusCode = "000";
                Result.message = "Heirarchy Deleted Successfully at Smart Attendance";
                return Result;
            }
            else
            {
                FaceAPIResponse Response = JsonConvert.DeserializeObject<FaceAPIResponse>(xx.Result);
                ResponseStatus Result = new ResponseStatus();
                Result = Response.error;
                Result.statusCode = Response.error.code == null ? Response.error.statusCode : Response.error.code;
                return Result;
            }
        }

        public ResponseStatus GetPersonGroupTrainingStatus(string AWCCode)
        {

            var client = new HttpClient();

            // Request headers - replace this example key with your valid key.
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ConfigurationManager.AppSettings["FaceAPIPrimaryKey"]);

            var queryString = "";
            // Request URI string.        
            var uri = ConfigurationManager.AppSettings["FaceAPIURL"] + "persongroups/" + AWCCode + "/training?" + queryString;

            // Here "name" is for display and doesn't have to be unique. Also, "userData" is optional.
            var json = "";

            string Data = new JavaScriptSerializer().Serialize(json);
            HttpContent content = new StringContent(Data);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var FaceResult = client.GetAsync(uri).Result;
            var xx = FaceResult.Content.ReadAsStringAsync();
            PersonGroupStatus Response = JsonConvert.DeserializeObject<PersonGroupStatus>(xx.Result);
            ResponseStatus Result = new ResponseStatus();
            if (Response.status != "" && Response.status != null)
            {
                Result.statusCode = "000";
                Result.message = Response.status + " & Hierrchy ID :" + AWCCode;
                return Result;
            }
            else
            {
                Result = Response.error;
                Result.statusCode = Response.error.code == null ? Response.error.statusCode : Response.error.code;
                return Result;
            }


        }
    }

}

