using System;
using CsvHelper.Configuration;
using CsvHelper;

namespace MySqlTestProject.Models.Mappings
{

    public class OpsReviewMap: ClassMap<OpsReview>
    {
        public OpsReviewMap()
        {
            AutoMap();
            Map(m=> m.case_id).Name("case_id");
            Map(m=> m.work_group).Name("work_group");
            Map(m=> m.assigned_to_group).Name("assigned_to_group");
            Map(m=>m.impact).Name("impact");
            Map(m=>m.ticket_create_date).Name("ticket_create_date");
            Map(m=>m.ticket_create_month).Name("ticket_create_month");
            Map(m=>m.ticket_create_week).Name("ticket_create_week");
            Map(m=>m.ticket_create_year).Name("ticket_create_year"); 
            Map(m=>m.ticket_status).Name("ticket_status");
            Map(m=>m.resolved_date).Name("resolved_date");
            Map(m=>m.resolved_month).Name("resolved_month");
            Map(m=>m.resolved_week).Name("resolved_week");
            Map(m=>m.resolved_year).Name("resolved_year");
            Map(m=>m.ticket_age).Name("ticket_age");
            Map(m=>m.root_cause).Name("root_cause");
            Map(m=>m.root_cause_details).Name("root_cause_details");
            Map(m=>m.assigned_to).Name("assigned_to");
            Map(m=>m.ticket_category).Name("ticket_category");
            Map(m=>m.ticket_type).Name("ticket_type");
            Map(m=>m.ticket_item).Name("ticket_item");
            Map(m=>m.maximpact).Name("maximpact");

         
        }
    }
}