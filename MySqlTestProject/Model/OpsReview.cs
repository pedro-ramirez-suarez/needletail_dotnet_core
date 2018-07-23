using System;
using Newtonsoft.Json;
using Needletail.DataAccess.Attributes;
namespace MySqlTestProject.Models
{


    public class OpsReview
    {

        public OpsReview()
        {

        }

        public OpsReview(string case_id, 
                        string work_group, 
                        string assigned_to_group,
                        int impact,
                        string ticket_create_date, 
                        int ticket_create_month, 
                        int ticket_create_week,
                        int ticket_create_year,
                        string ticket_status,
                        string resolved_date,
                        int resolved_month,
                        int resolved_week,
                        int resolved_year,
                        float ticket_age,
                        string root_cause,
                        string root_cause_details,
                        string assigned_to,
                        string ticket_category,
                        string ticket_type,
                        string ticket_item,
                        int maximpact)
        {
            this.case_id = case_id;
            this.work_group = work_group;
            this.assigned_to_group = assigned_to_group;
            this.ticket_create_date = ticket_create_date;
            this.ticket_create_week = ticket_create_week;
            this.ticket_create_month = ticket_create_month;
            this.ticket_create_year = ticket_create_year;
            this.ticket_status = ticket_status;
            this.resolved_date = resolved_date;
            this.resolved_month = resolved_month;
            this.resolved_week = resolved_week;
            this.resolved_year = resolved_year;
            this.ticket_age = ticket_age;
            this.root_cause = root_cause;
            this.root_cause_details = root_cause_details;
            this.assigned_to = assigned_to;
            this.ticket_category = ticket_category;
            this.ticket_type = ticket_type;
            this.ticket_item = ticket_item;
            this.maximpact = maximpact;


        }
        [JsonProperty("Case Id"),  TableKey( CanInsertKey = true)]      
        public string case_id { get; set; }
        [JsonIgnore]
        public string work_group { get; set; }
        [JsonProperty("HVH Queue")]
        public string assigned_to_group { get; set; }
        
        [JsonProperty("Impact")]
        public int impact { get; set; }
       [JsonProperty("Created Date")]
        public string ticket_create_date { get; set; }
        
        [JsonProperty("Ticket Created Month")]
        public int ticket_create_month { get; set; }
        
        [JsonProperty("Ticket Created Week")]
        public int ticket_create_week { get; set; }
        
        [JsonProperty("Ticket Created Year")]
        public int ticket_create_year { get; set; }
        
        [JsonProperty("Status")]
        public string ticket_status { get; set; }
        
        [JsonProperty("Resolved Date")]
        public string resolved_date { get; set; }
        
        [JsonProperty("Resolved Month")]
        public int resolved_month { get; set; }
        
        [JsonProperty("Resolved Week")]
        public int resolved_week { get; set; }
        
        [JsonProperty("Resolved Year")]
        public int resolved_year { get; set; }
        
        [JsonProperty("Age")]
        public float ticket_age { get; set; }
        
        [JsonProperty("Root Cause")]
        public string root_cause { get; set; }

        [JsonIgnore]
        public string root_cause_details {get;set;}
        
        [JsonProperty("Assigned To")]
        public string assigned_to {get;set;}
        
        [JsonProperty("Category")]
        public string ticket_category {get;set;}
        
        [JsonProperty("Type")]
        public string ticket_type {get;set;}
        
        [JsonProperty("Item")]
        public string ticket_item {get;set;}
        
        [JsonProperty("Max Impact")]
        public int maximpact {get;set;}

    }

}