using System;
using Newtonsoft.Json;
using Needletail.DataAccess.Attributes;
namespace MySqlTestProject.Models
{

    public class MonitorResult
    {

        [JsonProperty("Test Id"),  TableKey( CanInsertKey = true)]      
        public string Id { get; set; }

        public int Result {get;set;}

        public string Details{get;set;}
        public string Application {get;set;}
        public string Component {get;set;}
        
        [JsonProperty("Test Name")]
        public string TestName {get;set;}
        
        [JsonProperty("Test Day")]
        public int TestDay{get;set;}

        [JsonProperty("Test Month")]
        public int TestMonth{get;set;}

        [JsonProperty("Test Year")]
        public int TestYear{get;set;}


        [JsonProperty("Test Full Date")]
        public DateTime TestDate{get;set;}



    }
}