// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using QuickType;
//
//    var data = MauticContactObject.FromJson(jsonString);
//
namespace LACRM_Mautic
{
    using System;
    using System.Net;
    using System.Collections.Generic;

    using Newtonsoft.Json;

    public partial class MauticContactObject
    {
        [JsonProperty("contact")]
        public ContactObj Contact { get; set; }
    }

    public partial class ContactObj
    {
        [JsonProperty("fields")]
        public FieldsObj Fields { get; set; }

        [JsonProperty("dateAdded")]
        public string DateAdded { get; set; }

        [JsonProperty("createdBy")]
        public long CreatedBy { get; set; }

        [JsonProperty("color")]
        public object Color { get; set; }

        [JsonProperty("createdByUser")]
        public string CreatedByUser { get; set; }

        [JsonProperty("dateModified")]
        public object DateModified { get; set; }

        [JsonProperty("dateIdentified")]
        public string DateIdentified { get; set; }

        [JsonProperty("doNotContact")]
        public object[] DoNotContact { get; set; }

        [JsonProperty("isPublished")]
        public bool IsPublished { get; set; }

        [JsonProperty("owner")]
        public object Owner { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("frequencyRules")]
        public object[] FrequencyRules { get; set; }

        [JsonProperty("ipAddresses")]
        public object[] IpAddresses { get; set; }

        [JsonProperty("modifiedBy")]
        public object ModifiedBy { get; set; }

        [JsonProperty("lastActive")]
        public object LastActive { get; set; }

        [JsonProperty("modifiedByUser")]
        public object ModifiedByUser { get; set; }

        [JsonProperty("preferredProfileImage")]
        public object PreferredProfileImage { get; set; }

        [JsonProperty("tags")]
        public object[] Tags { get; set; }

        [JsonProperty("points")]
        public long Points { get; set; }

        [JsonProperty("stage")]
        public object Stage { get; set; }

        [JsonProperty("utmtags")]
        public object Utmtags { get; set; }
    }

    public partial class FieldsObj
    {
        [JsonProperty("core")]
        public Dictionary<string, Core> Core { get; set; }

        [JsonProperty("professional")]
        public object[] Professional { get; set; }

        [JsonProperty("all")]
        public All All { get; set; }

        [JsonProperty("personal")]
        public object[] Personal { get; set; }

        [JsonProperty("social")]
        public Social Social { get; set; }
    }

    public partial class Core
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

    public partial class AllObj
    {
        [JsonProperty("crm_contactid")]
        public string CrmContactid { get; set; }

        [JsonProperty("instagram")]
        public object Instagram { get; set; }

        [JsonProperty("attribution_date")]
        public object AttributionDate { get; set; }

        [JsonProperty("address2")]
        public object Address2 { get; set; }

        [JsonProperty("address1")]
        public object Address1 { get; set; }

        [JsonProperty("attribution")]
        public object Attribution { get; set; }

        [JsonProperty("company")]
        public object Company { get; set; }

        [JsonProperty("city")]
        public object City { get; set; }

        [JsonProperty("country")]
        public object Country { get; set; }

        [JsonProperty("firstname")]
        public string Firstname { get; set; }

        [JsonProperty("facebook")]
        public object Facebook { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("fax")]
        public object Fax { get; set; }

        [JsonProperty("googleplus")]
        public object Googleplus { get; set; }

        [JsonProperty("foursquare")]
        public object Foursquare { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("phone")]
        public object Phone { get; set; }

        [JsonProperty("skype")]
        public object Skype { get; set; }

        [JsonProperty("linkedin")]
        public object Linkedin { get; set; }

        [JsonProperty("lastname")]
        public string Lastname { get; set; }

        [JsonProperty("mobile")]
        public object Mobile { get; set; }

        [JsonProperty("position")]
        public object Position { get; set; }

        [JsonProperty("points")]
        public object Points { get; set; }

        [JsonProperty("preferred_locale")]
        public object PreferredLocale { get; set; }

        [JsonProperty("title")]
        public object Title { get; set; }

        [JsonProperty("website")]
        public object Website { get; set; }

        [JsonProperty("state")]
        public object State { get; set; }

        [JsonProperty("twitter")]
        public object Twitter { get; set; }

        [JsonProperty("zipcode")]
        public object Zipcode { get; set; }
    }

    public partial class Social
    {
        [JsonProperty("instagram")]
        public Core Instagram { get; set; }

        [JsonProperty("foursquare")]
        public Core Foursquare { get; set; }

        [JsonProperty("facebook")]
        public Core Facebook { get; set; }

        [JsonProperty("googleplus")]
        public Core Googleplus { get; set; }

        [JsonProperty("skype")]
        public Core Skype { get; set; }

        [JsonProperty("linkedin")]
        public Core Linkedin { get; set; }

        [JsonProperty("twitter")]
        public Core Twitter { get; set; }
    }

    public partial class MauticContactObject
    {
        public static MauticContactObject FromJson(string json) => JsonConvert.DeserializeObject<MauticContactObject>(json, MauticContactObjectConverter.Settings);
    }

    public static class MauticContactObjectSerialize
    {
        public static string ToJson(this MauticContactObject self) => JsonConvert.SerializeObject(self, MauticContactObjectConverter.Settings);
    }

    public class MauticContactObjectConverter
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
