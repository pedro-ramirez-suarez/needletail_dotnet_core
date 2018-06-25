using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using tickets.Models;
using Needletail.DataAccess;
using System.Threading.Tasks;

namespace tickets.Repositories
{
    public class TicketRepo
    {
        string ticketsCS= "Server=localhost;User ID=root;password=adf456;Database=Tickets;";      
        MySqlConnection conn;
        public TicketRepo ()
        {
            conn = new MySqlConnection(ticketsCS);
            conn.Open();
        }
        public async Task< IEnumerable<OpsReview>> GetAllTickets()
        {
            var tickets = new DBTableDataSourceBase<OpsReview,string>(this.ticketsCS,true);
            var allT = await tickets.GetAllAsync();
            return allT;

            // var allT = new List<Ticket>();
            // var cmd = new MySqlCommand("Select * from TicketMetrics",conn);
            // using(var reader = cmd.ExecuteReader())
            // {
            //     while(reader.Read())
            //     {
            //         var t = new Ticket();
            //         t.case_id = reader.GetValue(0).ToString();
            //         t.work_group = reader.GetValue(0).ToString();
            //         allT.Add(t);
            //     }
            // }
            // conn.Close();
            // return allT;
            //return null;
        }

    }
}
