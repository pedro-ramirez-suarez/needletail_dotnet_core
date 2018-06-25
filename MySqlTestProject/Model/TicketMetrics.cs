using System;

namespace tickets.Models
{


    public class OpsReview
    {
        public string case_id { get; set; }
        public string work_group { get; set; }
        public string assigned_to_group { get; set; }
        public int impact { get; set; }
        public string ticket_create_date { get; set; }
        public int ticket_create_month { get; set; }
        public int ticket_create_week { get; set; }
        public int ticket_create_year { get; set; }
        public string ticket_status { get; set; }
        public string resolved_date { get; set; }
        public int resolved_month { get; set; }
        public int resolved_week { get; set; }
        public int resolved_year { get; set; }
        public float ticket_age { get; set; }
        public string root_cause { get; set; }

        public string root_cause_details { get; set; }
        public string assigned_to { get; set; }
        public string ticket_category { get; set; }
        public string ticket_type { get; set; }
        public string ticket_item { get; set; }
        public int maximpact { get; set; }

    }

}