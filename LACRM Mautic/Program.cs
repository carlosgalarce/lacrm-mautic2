using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Configuration;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using Google.Maps;
using Google.Maps.Geocoding;
using System.Web;

using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;


namespace LACRM_Mautic
{
    public partial class httpAddresses
    {
        public httpAddresses()
        {
            string MauticHostAddress = "http://ec2-34-236-159-214.compute-1.amazonaws.com/";
            string LACRMHostAddress =
                "https://api.lessannoyingcrm.com/?UserCode=44F62&amp;APIToken=4YDJ4WS12N4DQ7B2VWQBKD0WJCB8D7SZ1NNBGZ47PB63V1G1JF&amp";
        }
        public string MauticHostAddress { get; set; }
        public string LACRMHostAddress { get; set; }

        //public  string MauticHostAddress = "ec2-34-236-159-214.compute-1.amazonaws.com/";
        //public static string LACRMHostAddress" = "https://api.lessannoyingcrm.com/?UserCode=44F62&APIToken=4YDJ4WS12N4DQ7B2VWQBKD0WJCB8D7SZ1NNBGZ47PB63V1G1JF&;

        
    }
    public static class Globals
    {
        public static readonly String MauticHostAddress = "http://ec2-34-236-159-214.compute-1.amazonaws.com/";
        public static readonly String LACRMHostAddress =
                "https://api.lessannoyingcrm.com/?UserCode=44F62&APIToken=4YDJ4WS12N4DQ7B2VWQBKD0WJCB8D7SZ1NNBGZ47PB63V1G1JF&";
    }

 
    internal class Program
    {
       httpAddresses serviceLocations = new httpAddresses();

        private static void Main(string[] args)
        {
            //httpAddresses serviceLocations = new httpAddresses();
          //  serviceLocations.MauticHostAddress = "http://ec2-34-236-159-214.compute-1.amazonaws.com/";
            //serviceLocations.LACRMHostAddress = "https://api.lessannoyingcrm.com/?UserCode=44F62&amp;APIToken=4YDJ4WS12N4DQ7B2VWQBKD0WJCB8D7SZ1NNBGZ47PB63V1G1JF&amp";

            var mauticCompanies = new RestClient(Globals.MauticHostAddress + "api/companies");
            
            var request = new RestRequest(Method.GET);
            request.AddHeader("postman-token", "2c0f8356-451f-114f-4320-276791d02f49");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", "Basic Y2FybG9zOmlsb3ZlYXRsYXNrcGk=");
            var response = mauticCompanies.Execute(request);

            var companiesList = JsonConvert.DeserializeObject<dynamic>(response.Content);

            var jo = JObject.Parse(response.Content);

            foreach (var token in jo.FindTokens("all"))
                Console.WriteLine(token.Path + ": " + token);

            // go thru the data in the CRM system 
            //var getContacts =
            //    new RestClient(serviceLocations.LACRMHostAddress + "&Function = SearchContacts & NumRows = 500 & Page = 1 % 22");
            //       // "https://api.lessannoyingcrm.com/?UserCode=44F62&APIToken=4YDJ4WS12N4DQ7B2VWQBKD0WJCB8D7SZ1NNBGZ47PB63V1G1JF&Function=SearchContacts&NumRows=500&Page=1%22");
            //var contactRequest = new RestRequest(Method.GET);
            //request.AddHeader("postman-token", "4c77876a-c1d2-bc8c-278e-94124de4f0b3");
            //request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "Basic Q2FybG9zIEdhbGFyY2U6aWxvdmVhdGxhcw==");
            //var contactResponse = getContacts.Execute(contactRequest);

            //var contactsList = JsonConvert.DeserializeObject<dynamic>(contactResponse.Content);

            //var contactsObject = JObject.Parse(contactResponse.Content);

            //var companiesArray = companiesList.companies;
            //// JArray compArray = JArray.Parse(companiesArray.ToString());

            ////Console.WriteLine(companiesList.data);

            var lacrmHttpCall = Globals.LACRMHostAddress + "Function=SearchContacts&Parameters={\"NumRows\":\"500\"}";
            var client = new WebClient();
            var stream =
                client.OpenRead(lacrmHttpCall);
            // "https://api.lessannoyingcrm.com?UserCode=44F62&APIToken=4YDJ4WS12N4DQ7B2VWQBKD0WJCB8D7SZ1NNBGZ47PB63V1G1JF&Function=SearchContacts&Parameters={\"NumRows\":\"500\"}");
           // Globals.LACRMHostAddress+"Function=SearchContacts&Parameters={\"NumRows\":\"500\"}");
            using (var reader = new StreamReader(stream))
            {
                var jsonString = reader.ReadLine();
                var jObject = JObject.Parse(jsonString);
                string crmContactId;

                dynamic o = JsonConvert.DeserializeObject(jsonString);

                var objArray = o.Result;

                JArray array = JArray.Parse(objArray.ToString());

                // First load companies so that we have a company to attach a contact
                for (var i = 0; i < array.Count; i++)
                {
                    var val = (string) array[i]["IsCompany"];
                    var cm = "1";
                 

                    if (string.Compare(val, cm) == 0)
                    {
                        Console.Write("Record is a company:\t");
                        Console.WriteLine(
                            "Contact ID = {0} User ID = {1} Company ID = {2} IsCompany Flag = {3} Company Name = {4} Company Address {5}\n",
                            array[i]["ContactId"], array[i]["UserId"],
                            array[i]["CompanyId"], array[i]["IsCompany"], array[i]["CompanyName"], array[i]["Address"]);

                        crmContactId = (string) array[i]["ContactId"];
                        
                        var companyObject = JObject.Parse(array[i].ToString());

                        //foreach (var token in companyObject.FindTokens("MauticID"))
                        //    Console.WriteLine(token.Path + ": " + token);

                        // start building the json string to pass to Mautic if we need to create a new record in Mautic
                        var company = new Company();
                        string address, city, state, zipcode, country, emailAddress;

                        GetAddress(array, i, "Work", out address, out city, out state, out zipcode, out country);

                        company.companyname = (string) array[i]["CompanyName"] != null
                            ? (string)array[i]["CompanyName"]
                            : "";
                        company.companyaddress1 = address;
                        company.companycity = city;
                        company.companystate = state;
                        company.companyzipcode = zipcode;
                        company.companycountry = country;
                        company.company_crm_contactid = crmContactId;

                        GetEmail(array, i, "Work", out emailAddress);
                        company.companyemail = emailAddress;

                        // does Mautic Know about this client? 
                        //        "CustomFields": {
                        //            "MauticStage": "",
                        //"MauticID": "",
                        //"MauticUpdateDat = eTime": ""
                        // First get the MauticID
                        var crmMauticId = companyObject.FindTokens("MauticID");
                        string storedCrmMauticId = "0";


                        if (crmMauticId.Count > 0)
                        {
                            if (!string.IsNullOrEmpty(crmMauticId[0].ToString()))
                            {
                                storedCrmMauticId = crmMauticId[0].ToString();
                            }
                        }

                        // create the JSON object to send to Mautic to create the contact object*/
                        string json = JsonConvert.SerializeObject(company);
                        Console.WriteLine(json);

                        // Make sure that no Nulls are in the object before we Upload to Mautic
                        var deserializedObject = JsonConvert.DeserializeObject<Company>(json);
                        var serializerSettings = new JsonSerializerSettings
                        {
                            NullValueHandling = NullValueHandling.Ignore,
                            DefaultValueHandling = DefaultValueHandling.Ignore
                        };

                        var cleanJson = JsonConvert.SerializeObject(deserializedObject, serializerSettings);
                        //  Console.WriteLine(cleanJson);

                        // Upload to Mautic
                        string mauticID = "";
                        string mauticStage;
                        string mauticUpdateDateTime;
                        if (!PublishCompanyToMautic(cleanJson, storedCrmMauticId, out mauticID, out mauticStage, out mauticUpdateDateTime))
                        {
                            Console.WriteLine("Matic Company Publish Failed for {0}", json);
                        }
                        // update MauticID in the CRM System
                        if (!PublishToCRM(company.company_crm_contactid, mauticID, mauticStage, mauticUpdateDateTime))
                        {
                            Console.WriteLine("Updating CRM failed for mauticID {0}", mauticID);
                        }
                    }
                }

                // now load the contacts (users)

                for (var i = 0; i < array.Count; i++)
                {
                    var val = (string)array[i]["IsCompany"];
                    var cm = "1";


                    if (string.Compare(val, cm) != 0)
                    {
                        Console.Write("Record is a user:\t");
                        Console.WriteLine(
                            "Contact ID = {0} User ID = {1} Company ID = {2} First Name = {3} Lst Name = {4}\n",
                            array[i]["ContactId"], array[i]["UserId"],
                            array[i]["CompanyId"], array[i]["FirstName"], array[i]["LastName"]);

                        crmContactId = (string) array[i]["ContactId"];

                        // start building the json string to pass to Mautic if we need to create a new record in Mautic
                        //{
                        //"firstname": "Test Name",
                        //    "email": "email@example.com"
                        //    }
                        var contact = new MauticContact();
                        string stAddress, city, state, zipcode, country, emailAddress, mobilePhone, regularPhone;

                        contact.title = (string) array[i]["Salutation"] != null ? (string) array[i]["Salutation"] : "";
                        contact.firstname =
                            (string) array[i]["FirstName"] != null ? (string) array[i]["FirstName"] : "";
                        contact.lastname = (string) array[i]["LastName"] != null ? (string) array[i]["LastName"] : "";
                        contact.position = (string) array[i]["Title"] != null ? (string) array[i]["Title"] : "";

                        GetAddress(array, i, "Work", out stAddress, out city, out state, out zipcode, out country);
                        contact.address1 = stAddress;
                        contact.city = city;
                        contact.zipcode = zipcode;
                        contact.country = country;

                        contact.company = (string) array[i]["CompanyName"] != null
                            ? (string) array[i]["CompanyName"]
                            : "";

                        GetEmail(array, i, "Work", out emailAddress);
                        contact.email = emailAddress;

                        contact.crm_contactid =
                            (string) array[i]["ContactId"] != null ? (string) array[i]["ContactId"] : "";

                        GetPhone(array, i, "Mobile", out mobilePhone);
                        GetPhone(array, i, "Work", out regularPhone);

                        contact.phone = regularPhone;
                        contact.mobile = mobilePhone;

                        var contactObject = JObject.Parse(array[i].ToString());

                        string storedCrmMauticId = "0";
                        string mID, mMs, mUDT;

                        // First get the MauticID
                        var crmMauticId = contactObject.FindTokens("MauticID");
                        
                        if (crmMauticId.Count > 0)
                        {
                            if (!string.IsNullOrEmpty(crmMauticId[0].ToString()))
                            {
                                storedCrmMauticId = crmMauticId[0].ToString();
                            }
                        }
                        //  GetCustomFields(array, i, out mID, out mMs, out mUDT);
                        //var data = LACRMContactObject.FromJson(array[i].ToString());

                        //string storedCrmMauticId = "0";
                        //// First get the MauticID
                        //if(data.CustomFields != null)
                        //{
                        //    storedCrmMauticId = data.CustomFields.MauticID != null ? data.CustomFields.MauticID.ToString() : "0";
                        //}


                        // RemoveEmpty(JsonConvert.SerializeObject(contact));

                        // create the JSON object to send to Mautic to create the contact object */
                        string json = JsonConvert.SerializeObject(contact);

                        // Make sure that no Nulls are in the object before we Upload to Mautic
                        var deserializedObject = JsonConvert.DeserializeObject<MauticContact>(json);
                        var serializerSettings = new JsonSerializerSettings
                        {
                            NullValueHandling = NullValueHandling.Ignore,
                            DefaultValueHandling = DefaultValueHandling.Ignore
                        };

                        var cleanJson = JsonConvert.SerializeObject(deserializedObject, serializerSettings);
                        //  Console.WriteLine(cleanJson);

                        string mauticID = "";
                        string mauticStage;
                        string mauticUpdateDateTime;
                        if (!PublishContactToMautic(cleanJson, storedCrmMauticId, out mauticID, out mauticStage, out mauticUpdateDateTime))
                        {
                            Console.WriteLine("Matic Publish Failed for {0}", json);
                            // need to go the next record 
                            continue;
                        }
                        


                        // update MauticID in the CRM System
                        if (!PublishToCRM(contact.crm_contactid, mauticID, mauticStage, mauticUpdateDateTime))
                        {
                            Console.WriteLine("Updating CRM failed for mauticID {0}", mauticID);
                        }
                        
                    }

                }
            }
            stream.Close();
        }

        private static void RemoveEmpty(string json)
        {
            string originalSerializedObject = "{myObj: { x: \"\", y: \"test str\" }, myStr: \"hello world!\"}";
            var deserializedObject = JsonConvert.DeserializeObject<Rootobject>(originalSerializedObject);

            var jDeserial = JsonConvert.DeserializeObject<MauticContact>(json);

            var serializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            var newSerializedObject = JsonConvert.SerializeObject(deserializedObject, serializerSettings);
            Console.WriteLine(newSerializedObject);
            //{"myObj":{"y":"test str"},"myStr":"hello world!"}

            var newJDeserial = JsonConvert.SerializeObject(jDeserial, serializerSettings);
            Console.Write(newJDeserial);

        }

        private static bool PublishToCRM(string crmContactId,  string mauticID, string mauticStage,
            string mauticUpdateDateTime)
        {
            string crmUpdateParms = "{ \"ContactId\":\"" + crmContactId + "\",\"CustomFields\":" + "{" +
                                    "\"MauticStage\":\"" + mauticStage + "\"" +
                                    ",\"MauticUpdateDateTime\":\"" + mauticUpdateDateTime + "\"" +
                                    ",\"MauticID\":\"" + mauticID + "\"} }";
            string postRequest =
                 //"https://api.lessannoyingcrm.com/?UserCode=44F62&APIToken=4YDJ4WS12N4DQ7B2VWQBKD0WJCB8D7SZ1NNBGZ47PB63V1G1JF&Function=EditContact&Parameters="
                 Globals.LACRMHostAddress+"Function=EditContact&Parameters=" +
                crmUpdateParms;

            var client = new RestClient(postRequest);
            var request = new RestRequest(Method.PUT);
            request.AddHeader("postman-token", "60de556b-ac8e-3326-4e5a-9afd7e8a44ea");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", "Basic Q2FybG9zIEdhbGFyY2U6aWxvdmVhdGxhcw==");
            request.AddHeader("content-type", "multipart/form-data; boundary=----WebKitFormBoundary7MA4YWxkTrZu0gW");
            request.AddParameter("multipart/form-data; boundary=----WebKitFormBoundary7MA4YWxkTrZu0gW",
                "------WebKitFormBoundary7MA4YWxkTrZu0gW\r\nContent-Disposition: form-data; name=\"ContactId\"\r\n\r\n3479607908164783618069392693372\r\n------WebKitFormBoundary7MA4YWxkTrZu0gW--",
                ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            var content = response.Content;

            return(true);
        }

        private static void GetEmail(JArray array, int i, string addressType, out string emailAddress)
        {
            var emailExist = array[i]["Email"];

            emailAddress = "";

            if (emailExist.HasValues)
            {
                var emailAddresses = (JArray) array[i]["Email"];


                for (var j = 0; j < emailAddresses.Count; j++)
                {
                    if ((string) emailAddresses[j]["Type"] == addressType)
                    {
                        emailAddress = (string) emailAddresses[j]["Text"] != null
                            ? (string) emailAddresses[j]["Text"]
                            : "";
                        break;
                    }

                    // Console.WriteLine("Email address = {0}, Type = {1}", email, emailAddresses[j]["Type"]);
                }
            }
        }

        private static void GetPhone(JArray array, int i, string phoneType, out string phone)
        {
            var phoneExist = array[i]["Phone"];

            phone = "";

            if (phoneExist.HasValues)
            {
                var phones = (JArray) array[i]["Phone"];
                
                for (var j = 0; j < phones.Count; j++)
                {
                    if ((string) phones[j]["Type"] == phoneType)
                    {
                        phone = (string) phones[j]["Text"] != null ? (string) phones[j]["Text"] : "";
                        break;
                    }

                    // Console.WriteLine("Email address = {0}, Type = {1}", email, emailAddresses[j]["Type"]);
                }
            }
        }

        private static void GetCustomFields(JArray array, int i, out string mauticId, out string mauticStage, out string mauticUpdateDateTime)
        {
            var customFieldsExist = array[i]["CustomFields"];

            mauticId = "0";
            mauticStage = "Blank";
            mauticUpdateDateTime = "0";

            if (customFieldsExist.HasValues)
            {
                var customFields = (JArray)array[i]["CustomFields"];

                for (var j = 0; j < customFields.Count; j++)
                {
                    if ((string)customFields[j]["MauticID"] == "MauticID")
                    {
                        mauticId = (string)customFields[j]["Text"] != null ? (string)customFields[j]["Text"] : "";
                        break;
                    }

                    // Console.WriteLine("Email address = {0}, Type = {1}", email, emailAddresses[j]["Type"]);
                }
            }
        }

        private static void GetAddress(JArray array, int i, string addressType, out string street, out string city,
            out string state,
            out string zip, out string country)
        {
            var addressExist = array[i]["Address"];

            street = "";
            city = "";
            state = "";
            zip = "";
            country = "";

            if (addressExist.HasValues)
            {
                var address = (JArray) array[i]["Address"];
                
                for (var j = 0; j < address.Count; j++)
                {

                    if ((string) address[j]["Type"] == addressType)
                    {
                        street = (string) address[j]["Street"] != null ? (string) address[j]["Street"] : "";
                        city = (string) address[j]["City"] != null ? (string) address[j]["City"] : "";
                        state = (string) address[j]["State"] != null ? (string) address[j]["State"] : "";
                        zip = (string) address[j]["Zip"] != null ? (string) address[j]["Zip"] : "";
                        country = (string) address[j]["Country"] != null ? (string) address[j]["Country"] : "";

						RootObject t =
							RootObject.GetLatLongByAddress(
								street + " " + city + " " + state + " " + zip + " " + country);

                        
                        for (var ac = 0; ac < t.results[0].address_components.Count; ac++)
                        {
                            var component = t.results[0].address_components[ac];

                            
                            switch (component.types[0])
                            {
                                case "locality":
                                    city = component.long_name;
                                    break;
                                case "administrative_area_level_1":
                                    state = component.long_name;
                                    break;
                                case "country":
                                    country = component.long_name;
                                    break;
                            }
                        };
                        
					}

                    //Console.WriteLine("Street= {0}, City= {1}, State= {2}, Country= {3}, Address Type= {4}", street, city, state,
                    //    country, addressType);
                }
            }
        }

        private static bool PublishContactToMautic(string json, string crmStoredMauticId, out string mauticID, out string mauticStage,
            out string mauticUpdateDateTime)
        {
            // string webCall = "http://54.210.98.34/api/contacts" + crmStoredMauticId + "/edit";
            string webCall = Globals.MauticHostAddress + "api/contacts/" + crmStoredMauticId + "/edit";
            var client = new RestClient(webCall);
            var request = new RestRequest(Method.PUT);
            //var client = new RestClient("http://54.210.98.34/api/contacts/new");
            //var request = new RestRequest(Method.POST);
            request.AddHeader("postman-token", "67fe23d3-9eb7-b749-d607-dfd9440c5fae");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            request.AddHeader("authorization", "Basic Y2FybG9zOmlsb3ZlYXRsYXNrcGk=");
            request.AddParameter("application/json", json,ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            var rc = response.StatusCode;
            var jsonReturn = response.Content;

            mauticID = "0";
            mauticStage = "Blank";
            mauticUpdateDateTime = "Blank";

            if (rc == HttpStatusCode.OK || rc == HttpStatusCode.Created)
            {
                var data = MauticContactObject.FromJson(jsonReturn);
                
                mauticID = data.Contact.Id.ToString();


                mauticUpdateDateTime =
                    data.Contact.DateModified != null
                        ? data.Contact.DateModified.ToString()
                        : data.Contact.DateAdded;
                mauticStage = data.Contact.Stage != null ? data.Contact.Stage.ToString() : "Blank";

                return (true);
            }
            {
                return (false);

            }
        }

        private static bool PublishCompanyToMautic(string json, string crmStoredMautidId, out string mauticID, out string mauticStage,
            out string mauticUpdateDateTime)
        {
            string webCall = Globals.MauticHostAddress + "api/companies/" + crmStoredMautidId +"/edit";
            var client = new RestClient(webCall);
            var request = new RestRequest(Method.PUT);
            request.AddHeader("postman-token", "f1717cfc-b823-f0a8-3f30-26e9a0a0f239");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            request.AddHeader("authorization", "Basic Y2FybG9zOmlsb3ZlYXRsYXNrcGk=");
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            var rc = response.StatusCode;
            var jsonReturn = response.Content;

            mauticID = "0";
            mauticStage = "Blank";
            mauticUpdateDateTime = "Blank";
            if (rc == HttpStatusCode.OK || rc == HttpStatusCode.Created)
            {
                JObject j = JObject.Parse(jsonReturn);

                var data = MauticCompanyObject.FromJson(jsonReturn);

                mauticID = data.Company.Id.ToString();
                // mauticUpdateDateTime = 

                var datemod = j.FindTokens("dateModified");
                if (datemod.Count > 0)
                {
                    if (!string.IsNullOrEmpty(datemod[0].ToString()))
                    {
                        mauticUpdateDateTime = datemod[0].ToString();
                    }
                }
                var stage = j.FindTokens("stage");
                if (stage.Count > 0)
                {
                    if (!string.IsNullOrEmpty(stage[0].ToString()))
                    {
                        mauticStage = stage[0].ToString();
                    }
                }
                return (true);
            }
            {
                return (false);

            }
        }

    }
}