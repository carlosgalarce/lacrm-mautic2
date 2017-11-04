// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using QuickType;
//
//    var data = MauticCompanyObject.FromJson(jsonString);
//
namespace LACRM_Mautic
{
    using System;
    using System.Net;
    using System.Collections.Generic;

    using Newtonsoft.Json;

    public partial class MauticCompanyObject
    {
        [JsonProperty("company")]
        public Company Company { get; set; }
    }

    public partial class Company
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("fields")]
        public Fields Fields { get; set; }

        [JsonProperty("score")]
        public long Score { get; set; }
    }

    public partial class Fields
    {
        [JsonProperty("core")]
        public Core Core { get; set; }

        [JsonProperty("professional")]
        public Professional Professional { get; set; }

        [JsonProperty("all")]
        public All All { get; set; }

        [JsonProperty("personal")]
        public object[] Personal { get; set; }

        [JsonProperty("social")]
        public object[] Social { get; set; }
    }

    public partial class Core
    {
        [JsonProperty("companycity")]
        public OtherCompany Companycity { get; set; }

        [JsonProperty("companyphone")]
        public OtherCompany Companyphone { get; set; }

        [JsonProperty("companyaddress1")]
        public OtherCompany Companyaddress1 { get; set; }

        [JsonProperty("company_crm_contactid")]
        public OtherCompany CompanyCrmContactid { get; set; }

        [JsonProperty("companyaddress2")]
        public OtherCompany Companyaddress2 { get; set; }

        [JsonProperty("companyemail")]
        public OtherCompany Companyemail { get; set; }

        [JsonProperty("companycountry")]
        public OtherCompany Companycountry { get; set; }

        [JsonProperty("companyname")]
        public OtherCompany Companyname { get; set; }

        [JsonProperty("companywebsite")]
        public OtherCompany Companywebsite { get; set; }

        [JsonProperty("companystate")]
        public OtherCompany Companystate { get; set; }

        [JsonProperty("companyzipcode")]
        public OtherCompany Companyzipcode { get; set; }
    }

    public partial class OtherCompany
    {
        [JsonProperty("group")]
        public string Group { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("alias")]
        public string Alias { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public partial class Professional
    {
        [JsonProperty("companydescription")]
        public OtherCompany Companydescription { get; set; }

        [JsonProperty("companyindustry")]
        public OtherCompany Companyindustry { get; set; }

        [JsonProperty("companyannual_revenue")]
        public OtherCompany CompanyannualRevenue { get; set; }

        [JsonProperty("companyfax")]
        public OtherCompany Companyfax { get; set; }

        [JsonProperty("companynumber_of_employees")]
        public OtherCompany CompanynumberOfEmployees { get; set; }
    }

    public partial class All
    {
        [JsonProperty("companyemail")]
        public string Companyemail { get; set; }

        [JsonProperty("companyannual_revenue")]
        public object CompanyannualRevenue { get; set; }

        [JsonProperty("companyaddress1")]
        public string Companyaddress1 { get; set; }

        [JsonProperty("company_crm_contactid")]
        public object CompanyCrmContactid { get; set; }

        [JsonProperty("companyaddress2")]
        public object Companyaddress2 { get; set; }

        [JsonProperty("companycountry")]
        public string Companycountry { get; set; }

        [JsonProperty("companycity")]
        public string Companycity { get; set; }

        [JsonProperty("companydescription")]
        public object Companydescription { get; set; }

        [JsonProperty("companynumber_of_employees")]
        public object CompanynumberOfEmployees { get; set; }

        [JsonProperty("companyindustry")]
        public object Companyindustry { get; set; }

        [JsonProperty("companyfax")]
        public object Companyfax { get; set; }

        [JsonProperty("companyname")]
        public string Companyname { get; set; }

        [JsonProperty("companystate")]
        public string Companystate { get; set; }

        [JsonProperty("companyzipcode")]
        public string Companyzipcode { get; set; }

        [JsonProperty("companyphone")]
        public object Companyphone { get; set; }

        [JsonProperty("companywebsite")]
        public object Companywebsite { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }
    }

    public partial class MauticCompanyObject
    {
        public static MauticCompanyObject FromJson(string json) => JsonConvert.DeserializeObject<MauticCompanyObject>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this MauticCompanyObject self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    public class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore
        };
    }
}
