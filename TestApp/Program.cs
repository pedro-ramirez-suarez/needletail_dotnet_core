using ConUniv.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApp
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var repo = new InstructorRepository();
            var all =  repo.GetAll();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("All instructors");
            Console.ForegroundColor = ConsoleColor.Yellow;
            foreach (var i in all)
            {
                Console.WriteLine("{0}, {1}", i.FirstName + " " +i.LastName, i.HireDate);
            }
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Press any key!");
            Console.ReadKey();
        }
    }
}
