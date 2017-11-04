using System.Security.Claims;

namespace LACRM_Mautic
{
    public partial class Company
    {
        public Company()
        {
             companyemail= null;
             companyaddress1= null;
             companyaddress2= null;
             companyphone= null;
             companycity= null;
             companystate= null;
             companyzipcode= null;
             companycountry= null;
             companyname= null;
             companywebsite= null;
             company_crm_contactid= null;
             companyindustry= null;
             companydescription= null;
             companynumber_of_employees= null;
             companyfax= null;
             companyannual_revenue= null;
        }
        public string companyemail { get; set; }
        public string companyaddress1 { get; set; }
        public string companyaddress2 { get; set; }
        public string companyphone { get; set; }
        public string companycity { get; set; }
        public string companystate { get; set; }
        public string companyzipcode { get; set; }
        public string companycountry { get; set; }
        public string companyname { get; set; }
        public string companywebsite { get; set; }
        public string company_crm_contactid { get; set; }
        public string companyindustry { get; set; }
        public string companydescription { get; set; }
        public string companynumber_of_employees { get; set; }
        public string companyfax { get; set; }
        public string companyannual_revenue { get; set; }
    }

    public partial class MauticContact
    {
        public MauticContact()
        {
            points = null;
                title= null;
                firstname= null;
                lastname= null;
                company= null;
                position= null;
                email= null;
                phone= null;
                mobile= null;
                address1= null;
                address2= null;
                city= null;
                state= null;
                zipcode= null;
                country= null;
                fax= null;
                preferred_locale= null;
                attribution_date= null;
                attribution= null;
                website= null;
                crm_contactid= null;
                facebook= null;
                foursquare= null;
                googleplus= null;
                instagram= null;
                linkedin= null;
                skype= null;
                twitter= null;
    }
       // [System.ComponentModel.DefaultValue(null)]
        public string points {get; set;}
        public string title { get; set;}
        public string firstname { get; set;}
        public string lastname {get; set;}
        public string company {get; set;}
        public string position {get; set;}
        public string email {get; set;}
        public string phone {get; set;}
        public string mobile {get; set;}
        public string address1 {get; set;}
        public string address2 {get; set;}
        public string city {get; set;}
        public string state {get; set;}
        public string zipcode {get; set;}
        public string country {get; set;}
        public string fax {get; set;}
        public string preferred_locale {get; set;}
        public string attribution_date {get; set;}
        public string attribution {get; set;}
        public string website {get; set;}
        public string crm_contactid {get; set;}
        public string facebook {get; set;}
        public string foursquare {get; set;}
        public string googleplus {get; set;}
        public string instagram {get; set;}
        public string linkedin {get; set;}
        public string skype {get; set;}
        public string twitter {get; set;}
    }
}