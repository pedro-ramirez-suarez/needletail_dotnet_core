using System;

using System.Linq;
using MySqlTestProject.Repositories;
using System.IO;
namespace MySqlTestProject
{
    class Program
    {
        static  void Main(string[] args)
        {
            var tRepo = new  TicketRepo();
            //import the data
            //var allT = t

            tRepo.InsertSample();
            

            Console.WriteLine("Value inserted");
            //var tStream = StreamReader();
            //Console.WriteLine("Total Tickets: {0}",allT.Result.Count());
            Console.ReadKey();
        }
    }
}


