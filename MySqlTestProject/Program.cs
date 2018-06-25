using System;
using tickets.Repositories;
using System.Linq;

namespace MySqlTestProject
{
    class Program
    {
        static  void Main(string[] args)
        {
            var tRepo = new TicketRepo();
            var allT =  tRepo.GetAllTickets();
            allT.Wait();
            Console.WriteLine("Total Tickets: {0}",allT.Result.Count());
            Console.ReadKey();
        }
    }
}
