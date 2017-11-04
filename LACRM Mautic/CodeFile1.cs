// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using QuickType;
//
//    var data = LACRMContactObject.FromJson(jsonString);
//
namespace LACRM_Mautic
{
    using System;
    using System.Net;
    using System.Collections.Generic;

    using Newtonsoft.Json;

    public partial class LACRMContactObject
    {
        [JsonProperty("CreationDate")]
        public string CreationDate { get; set; }

        [JsonProperty("CompanyId")]
        public object CompanyId { get; set; }

        [JsonProperty("BackgroundInfo")]
        public string BackgroundInfo { get; set; }

        [JsonProperty("Address")]
        public Address[] Address { get; set; }

        [JsonProperty("Birthday")]
        public string Birthday { get; set; }

        [JsonProperty("ContactCustomFields")]
        public bool ContactCustomFields { get; set; }

        [JsonProperty("CompanyName")]
        public string CompanyName { get; set; }

        [JsonProperty("ContactId")]
        public string ContactId { get; set; }

        [JsonProperty("EmployeeCount")]
        public string EmployeeCount { get; set; }

        [JsonProperty("OriginalGoogleId")]
        public object OriginalGoogleId { get; set; }

        [JsonProperty("EditedDate")]
        public string EditedDate { get; set; }

        [JsonProperty("CustomFields")]
        public CustomFields CustomFields { get; set; }

        [JsonProperty("Email")]
        public Email[] Email { get; set; }

        [JsonProperty("IsCompany")]
        public string IsCompany { get; set; }

        [JsonProperty("Industry")]
        public string Industry { get; set; }

        [JsonProperty("NumEmployees")]
        public string NumEmployees { get; set; }

        [JsonProperty("UserId")]
        public string UserId { get; set; }

        [JsonProperty("Phone")]
        public Phone[] Phone { get; set; }

        [JsonProperty("Website")]
        public Website[] Website { get; set; }
    }

    public partial class Address
    {
        [JsonProperty("Country")]
        public string Country { get; set; }

        [JsonProperty("Street")]
        public string Street { get; set; }

        [JsonProperty("City")]
        public string City { get; set; }

        [JsonProperty("State")]
        public string State { get; set; }

        [JsonProperty("Type")]
        public string Type { get; set; }

        [JsonProperty("Zip")]
        public string Zip { get; set; }
    }

    public partial class CustomFields
    {
        [JsonProperty("MauticStage")]
        public string MauticStage { get; set; }

        [JsonProperty("MauticID")]
        public string MauticID { get; set; }

        [JsonProperty("MauticUpdateDateTime")]
        public string MauticUpdateDateTime { get; set; }
    }

    public partial class Email
    {
        [JsonProperty("Text")]
        public string Text { get; set; }

        [JsonProperty("Type")]
        public string Type { get; set; }
    }

    public partial class Phone
    {
        [JsonProperty("Text")]
        public string Text { get; set; }

        [JsonProperty("Clean")]
        public string Clean { get; set; }

        [JsonProperty("Type")]
        public string Type { get; set; }
    }

    public partial class Website
    {
        [JsonProperty("Text")]
        public string Text { get; set; }
    }

    public partial class LACRMContactObject
    {
        public static LACRMContactObject FromJson(string json) => JsonConvert.DeserializeObject<LACRMContactObject>(json, LACRMContactObjectConverter.Settings);
    }

    public static class LACRMContactObjectSerialize
    {
        public static string ToJson(this LACRMContactObject self) => JsonConvert.SerializeObject(self, LACRMContactObjectConverter.Settings);
    }

    public class LACRMContactObjectConverter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
        };
    }
}
