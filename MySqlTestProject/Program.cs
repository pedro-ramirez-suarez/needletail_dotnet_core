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
            var mrepo = new  MonitorResultRepo();
            var results =  mrepo.GetHearthbeatFromPeriod(DateTime.Parse("08/22/2018"), DateTime.Parse("08/20/2018"));
            //mrepo.AddTestResult("HVH", "TEST", "Needletail", 1, "This is the detail");

            //import the data
            //var allT = t






            //Console.WriteLine("Value inserted");
            //var tStream = StreamReader();
            //Console.WriteLine("Total Tickets: {0}",allT.Result.Count());
            Console.ReadKey();
        }
    }
}


