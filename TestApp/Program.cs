using ConUniv.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TestApp.models;
using TestApp.repositories;

namespace TestApp
{
    public class Program
    {
        public static void Main(string[] args)
        {

            //var iRepo = new InstructorRepository();
            //var cRepo = new CourseRepository();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            var comRepo = new CompanyRepository();
            /*Testing sending a table*/
            //DataTable dt = new DataTable("");

            var courseRepo = new CourseRepository();
            var co = courseRepo.GetSingle(where: new { Title = "Chemistry 1" });
            co.Budget = 253678912345.000M;
            courseRepo.Update(co);
            co = new Course { Id = Guid.NewGuid(), Title ="Lots of Money", Credits = 50, Budget = 12345678912345.000M };
            courseRepo.Insert(co);
            var allC = courseRepo.GetAll();
            foreach (var c in allC)
            {
                Console.WriteLine("Course: {0}, Credits: {1}, Budget: {2}",c.Title,c.Credits, c.Budget.ToString("c3"));
            }
            //Console.WriteLine("New company name");
            //var comRepo = new CompanyRepository();
            //var cName = Console.ReadLine();
            //Company company = new Company();
            //company.Id = Guid.NewGuid(); // Guid.Parse("fb8a2360-501c-42bb-b3fe-ff1809197b23");
            //company.Name = cName;
            //company.CompanyAddress = null;
            //company.BillingContact = "Lilia Segundo";
            //company.BillingPhone = "1234567890";
            //company.BillingAddress = "Las Caadas 501, Interior 260, Colonia Tres Mar";
            //company.CompanyTypeId = Guid.Parse("16a5f24d-34bb-4da0-a692-c53be002f7d4");
            //company.StateId = Guid.Parse("85d1bde3-13b5-4fd6-b505-10660bb145f1");
            //comRepo.Insert(company);

            //var all =  iRepo.GetAllAsync();
            //var allC = cRepo.GetAllAsync();
            //var allCom = comRepo.GetManyAsync(where: new { Name = cName });
            //Task.WaitAll(allCom);
            //Task.WaitAll(all, allC);
            //Console.ForegroundColor = ConsoleColor.Green;
            //Console.WriteLine("All instructors");
            //Console.ForegroundColor = ConsoleColor.Yellow;
            //foreach (var i in all.Result)
            //{
            //    Console.WriteLine("{0}, {1}", i.FirstName + " " +i.LastName, i.HireDate);
            //}
            //Console.ForegroundColor = ConsoleColor.Green;
            //Console.WriteLine("All Courses");
            //Console.ForegroundColor = ConsoleColor.Blue;
            //foreach (var c in allCom.Result)
            //{
            //    Console.WriteLine("{0}, {1}", c.Name, c.CompanyAddress);
            //}

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Press any key!");
            Console.ReadKey();
        }
    }
}
