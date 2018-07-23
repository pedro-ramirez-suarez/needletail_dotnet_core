using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using MySqlTestProject.Models;
using Needletail.DataAccess;
using System.Threading;
using System.Threading.Tasks;
using Needletail.DataAccess.Engines;
using MySqlTestProject.Models.Mappings;

namespace MySqlTestProject.Repositories
{
    public class TicketRepo
    {
        
        //MySqlConnection conn;
        public TicketRepo ()
        {
            
        }
        public async  Task<IEnumerable<OpsReview>> GetAllTickets()
        {
            var tickets = new DBTableDataSourceBase<OpsReview,string>(MySqlServerConfiguration.ConnectionString,true);
            var allT = await tickets.GetAllAsync();
            return allT;

        }

        public async  Task<IEnumerable<OpsReview>> GetTicketsByYear(int year)
        {
            var tickets = new DBTableDataSourceBase<OpsReview,string>(MySqlServerConfiguration.ConnectionString,true);
            var allT = await tickets.GetManyAsync(where: new { ticket_create_year = year });
            return allT;
        }

        public async  Task<IEnumerable<OpsReview>> GetTicketsFromPeriod(DateTime dateLimit,DateTime dateStartLimit)
        {
            dateLimit = dateLimit.AddDays(1);
            var tickets = new DBTableDataSourceBase<OpsReview,string>(MySqlServerConfiguration.ConnectionString,true);
            
            var allT = await tickets.GetManyAsync(select: "Select *", where: string.Format("STR_TO_DATE(ticket_create_date, '%m/%d/%Y') >= '{0}' AND STR_TO_DATE(ticket_create_date, '%m/%d/%Y') < '{1}'",dateStartLimit.ToString("yyyy/MM/dd"),dateLimit.ToString("yyyy/MM/dd") ), orderBy:"");
            
            return allT;
        }

        public async  Task<IEnumerable<OpsReview>> GetTicketsByQueue(string queue)
        {
            var tickets = new DBTableDataSourceBase<OpsReview,string>(MySqlServerConfiguration.ConnectionString,true);
            var allT = await tickets.GetManyAsync(where: new { assigned_to_group = queue });
            return allT;
        }
        public async  Task<IEnumerable<OpsReview>> GetTicketsByYearAndQueue(int year,string queue)
        {
            var tickets = new DBTableDataSourceBase<OpsReview,string>(MySqlServerConfiguration.ConnectionString,true);
            var allT = await tickets.GetManyAsync(where: new { ticket_create_year = year, assigned_to_group = queue });
            return allT;
        }

        public async Task<IEnumerable<OpsReview>> GetOpenTickets()
        {
            var tickets = new DBTableDataSourceBase<OpsReview,string>(MySqlServerConfiguration.ConnectionString,true);
            var allT = await tickets.GetManyAsync(where: new {  ticket_status_In = new string[] {"Assigned","Researching","Work In Progress","Pending"}}, filterType: FilterType.AND, orderBy: new { ticket_age = "DESC" }, topN:20  );
            return allT;
        }

        public void InsertDemoValue()
        {
            var t = new OpsReview();
            
        }

        // public async Task<int> ImportRecords(System.IO.Stream st)
        // {
        //     var tr = new System.IO.StreamReader(st);
        //     var import = new CsvHelper.CsvReader(tr);
        //     //set the mappings
        //     import.Configuration.RegisterClassMap<OpsReviewMap>();
        //     // import.Configuration.PrepareHeaderForMatch = header =>
        //     // System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase( header );
        //     int imported = 0;
        //     var toImport = import.GetRecords<OpsReview>();
        //     var tickets = new DBTableDataSourceBase<OpsReview,string>(MySqlServerConfiguration.ConnectionString,true);
        //     try
        //     {
        //         foreach(var t in toImport)
        //         {
        //             //check if already exists
        //             var existing = (await tickets.GetSingleAsync(where: new {case_id = t.case_id}));
        //             //var existing = await tickets.ExecuteScalarAsync<int>(string.Format("Select count(case_id) from opsreview Where case_id = `{0}`",t.case_id), new Dictionary<string, object>());
        //             if(existing == null)
        //             {
        //                 //add it
        //                 await tickets.InsertAsync(t);
        //             }
        //             else
        //             {
        //                 await tickets.UpdateAsync(t);
        //             }
        //             imported++;
        //         }
                
        //     }
        //     catch(Exception e)
        //     {
                
        //     }
        //     return imported;
        // }

        public void InsertSample()
        {
            var tickets = new DBTableDataSourceBase<OpsReview,string>(MySqlServerConfiguration.ConnectionString,true);
            var t = new OpsReview(
                case_id : "E036569363",
                work_group: "HVH",
                assigned_to_group: "HVH-Technical", 
                impact: 3,
                ticket_create_date: "",
                ticket_create_month: 1,
                ticket_create_week: 1,
                ticket_create_year: 2018,
                ticket_status: "Pending",
                resolved_date: "",
                resolved_month: 1,
                resolved_week: 2,
                resolved_year: 2018,
                ticket_age: 1f,
                root_cause: "",
                root_cause_details: "",
                assigned_to: "",
                ticket_category: "",
                ticket_type: "",
                ticket_item: "",
                maximpact: 2
            );
            tickets.Insert(t);

        }

    }
}
