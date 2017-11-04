//var arrayObject = JObject.Parse(array[i].ToString());

//var t = arrayObject.FindTokens("MauticID");

//                        if (t.Count > 0)
//                        {
//                            var s = t[0].ToString();
//                        }

//                        foreach (var token in arrayObject.FindTokens("MauticID"))
//                            Console.WriteLine(token.Path + ": " + token);

//                        //                      Email": [],
//                        //"Phone": [
//                        //  {
//                        //    "Text": "561 367 9101",
//                        //    "Type": "Work",
//                        //    "Clean": "5613679101"
//                        //  }
//                        //],


//                        // Console.ReadLine();
//                    }
//                }



//                //foreach (JObject obj in array.Children<JObject>())
//                //{
//                //    foreach (JProperty singleProp in obj.Properties())
//                //    {
//                //        string name = singleProp.Name;
//                //        string value = singleProp.Value.ToString();

//                //        Console.WriteLine("name is " + name + " and value is " + value);

//                //        //Do something with name and value
//                //        //System.Windows.MessageBox.Show("name is "+name+" and value is "+value);
//                //    }
//                //}


//                dynamic dynJson = JsonConvert.DeserializeObject(jObject.ToString());
//                foreach (var item in dynJson)
//                {
//                    //Console.WriteLine("{0} {1} {2} {3}\n", item["Result"][i]["ContactId"], item["Result"][i]["UserId"],
//                    //    item["Result"][i]["CompanyId"], item["Result"][i]["IsCompany"]);

//                    //Newtonsoft.Json.Linq.JArray jsonResponse = Newtonsoft.Json.Linq.JObject.Parse(item["Result"][0].ToString());
//                    //Newtonsoft.Json.Linq.JToken token = jsonResponse[0];

//                    //var p = item;
//                    //var k = p["Result"][0]["IsCompany"];
//                    //i++;
//                }

//                Console.Write(jObject.Count);
//                Console.WriteLine("{0} {1} {2} {3}\n", (string) jObject["Result"][0] ["ContactId"],
//                    (string) jObject["Result"][0] ["UserId"],
//                    (string) jObject["Result"][0] ["CompanyId"], (string) jObject["Result"][0] ["isCompany"]);
//                Console.WriteLine("{0} {1} {2} {3}\n", (string) jObject["Result"][2] ["ContactId"],
//                    (string) jObject["Result"][2] ["UserId"],
//                    (string) jObject["Result"][2] ["CompanyId"], (string) jObject["Result"][2] ["isCompany"]);
//                Console.WriteLine("{0} {1} {2} {3}\n", (string) jObject["Result"][10] ["ContactId"],
//                    (string) jObject["Result"][10] ["UserId"],
//                    (string) jObject["Result"][10] ["CompanyId"], (string) jObject["Result"][10] ["isCompany"]);
//                //Console.WriteLine((string)jObject["Result"][1]["ContactId"]);
//                //Console.WriteLine((string)jObject["Result"][10]["ContactId"]);

//                //Console.WriteLine((string)jObject["albums"][0]["cover_image_url"]);


//                // Console.WriteLine(reader.ReadLine());


////"id": 1,
////"points": "0",
////"title": null,
////"firstname": null,
////"lastname": null,
////"company": null,
////"position": null,
////"email": null,
////"phone": null,
////"mobile": null,
////"address1": null,
////"address2": null,
////"city": null,
////"state": null,
////"zipcode": null,
////"country": null,
////"fax": null,
////"preferred_locale": null,
////"attribution_date": null,
////"attribution": null,
////"website": null,
////"crm_contactid": null,
////"facebook": null,
////"foursquare": null,
////"googleplus": null,
////"instagram": null,
////"linkedin": null,
////"skype": null,
////"twitter": null


//                 company.companyemail =  ;
//company.companyaddress1 = ;
//company.companyaddress2 ;
//company.companyphone ;
//company.companycity ;
//company.companystate ;
//company.companyzipcode ;
//company.companycountry ;
//company.companyname ;
//company.companywebsite ;
//company.crm_contactid ;
//company.companyindustry ;
//company.companydescription ;
//company.companynumber_of_employees ;
//company.companyfax ;
//company.companyannual_revenue ;

using System;
using Newtonsoft;
using System.ComponentModel;
using Newtonsoft.Json;


namespace LACRM_Mautic
{
    public class Rootobject
    {
        public Myobj myObj { get; set; }
        public string myStr { get; set; }
    }

    public class Myobj
    {
        [DefaultValue("")]
        public string x { get; set; }

        [DefaultValue("")]
        public string y { get; set; }



        public void RemoveEmpty()
        {
            var originalSerializedObject = "{myObj: { x: \"\", y: \"test str\" }, myStr: \"hello world!\"}";
            var deserializedObject = JsonConvert.DeserializeObject<Rootobject>(originalSerializedObject);

            var serializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore
            };

        var newSerializedObject = JsonConvert.SerializeObject(deserializedObject, serializerSettings);
        Console.WriteLine(newSerializedObject);
        //{"myObj":{"y":"test str"},"myStr":"hello world!"}

    }
}
}

