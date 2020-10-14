using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OneSignal.Models
{
    public class CreateAppModel
    {
        [Required]
        [Key]
        [Display(Name = "Name")]
        public string name { get; set; }
        public string chrome_web_origin { get; set; }
        public string chrome_web_default_notification_icon { get; set; }
        public string chrome_web_sub_domain { get; set; }
        public string site_name { get; set; }
        public string safari_site_origin { get; set; }
        public string safari_apns_p12 { get; set; }
        public string safari_apns_p12_password { get; set; }
        public string safari_icon_256_256 { get; set; }
        public string chrome_key { get; set; }
        [Display(Name ="Organization ID")]
        public string organization_id { get; set; }

    }

    public class ViewApps
    {
        public string id { get; set; }
        [Display(Name = "App Name")]
        public string name { get; set; }
    }

    public class UpdateAppModel
    {
        public string id { get; set; }
        [Display(Name = "App Name")]
        public string name { get; set; }
    }
}