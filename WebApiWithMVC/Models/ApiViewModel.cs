using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiWithMVC.Models
{
    public class ApiViewModel
    {
        //[JsonProperty("userId")]
        public string UserId { get; set; }
        //[JsonProperty("id")]
        public string Id { get; set; }
        //[JsonProperty("title")]
        public string Title { get; set; }
        //[JsonProperty("body")]
        public string Body { get; set; }
    }
    public class ApiData
    {
        public List<ApiViewModel> ApiDat { get; set; }
    }
}