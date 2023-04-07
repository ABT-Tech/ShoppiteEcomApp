using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using DellyShopApp.Models;
using Newtonsoft.Json;

namespace DellyShopApp.Views.Pages
{
    public class ChangeUserData
    {
        [JsonProperty(PropertyName = "changename")]
        public string ChangeUsername { get; set; }

        [JsonProperty(PropertyName = "changePhoneNumber")]
        public string ChangeContactNumber { get; set; }

        [JsonProperty(PropertyName = "changeemail")]
        public string ChangeEmail { get; set; }

        [JsonProperty(PropertyName = "changeState")]
        public string ChangeState { get; set; }

        [JsonProperty(PropertyName = "zipcode")]
        public string ChangeZipcode { get; set; }

        [JsonProperty(PropertyName = "changeCity")]
        public string Changecity { get; set; }

        [JsonProperty(PropertyName = "changeAddress")]
        public string ChangeAddress { get; set; }

        [JsonProperty(PropertyName = "orgid")]
        public int OrgId { get; set; }

        [JsonProperty(PropertyName = "userid")]
        public int UserId { get; set; }
    }
}
