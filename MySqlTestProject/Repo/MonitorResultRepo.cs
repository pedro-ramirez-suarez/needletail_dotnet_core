using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Linq;
using Needletail.DataAccess;
using System.Threading;
using System.Threading.Tasks;
using Needletail.DataAccess.Engines;
using MySqlTestProject.Models;

namespace MySqlTestProject.Repositories
{

    public class MonitorResultRepo
    {


        public void AddTestResult(string application, string component, string testName, int testResult, string detail)
        {
            try
            {
                var monitor = new DBTableDataSourceBase<MonitorResult, string>(MySqlServerConfiguration.ConnectionString, true);
                var tDate = DateTime.Now;
                var monResult = new MonitorResult
                {
                    Id = Guid.NewGuid().ToString(),
                    Result = testResult,
                    Application = application,
                    Component = component,
                    TestName = testName,
                    TestDay = tDate.Day,
                    TestMonth = tDate.Month,
                    TestYear = tDate.Year,
                    TestDate = tDate
                };

                monitor.Insert(monResult);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }


        public async Task<IEnumerable<MonitorResult>> GetAllMonitorResult()
        {
            var results = new DBTableDataSourceBase<MonitorResult, string>(MySqlServerConfiguration.ConnectionString, true);
            var all = await results.GetAllAsync();
            return all;

        }

        public async Task<IEnumerable<MonitorResult>> GetTicketsByYear(int year)
        {
            var results = new DBTableDataSourceBase<MonitorResult, string>(MySqlServerConfiguration.ConnectionString, true);
            var all = await results.GetManyAsync(where: new { TestYear = year });
            return all;
        }

        public async Task<IEnumerable<MonitorResult>> GetTicketsFromPeriod(DateTime dateLimit, DateTime dateStartLimit)
        {
            dateLimit = dateLimit.AddDays(1);
            var results = new DBTableDataSourceBase<MonitorResult, string>(MySqlServerConfiguration.ConnectionString, true);

            var all = await results.GetManyAsync(select: "Select *", where: string.Format("STR_TO_DATE(TestDate, '%m/%d/%Y') >= '{0}' AND STR_TO_DATE(TestDate, '%m/%d/%Y') < '{1}'", dateStartLimit.ToString("yyyy/MM/dd"), dateLimit.ToString("yyyy/MM/dd")), orderBy: "");

            return all;
        }

        public async Task<object[][]> GetHearthbeatFromPeriod(DateTime dateLimit, DateTime dateStartLimit)
        {
            dateLimit = dateLimit.AddDays(1);
            var resultsDB = new DBTableDataSourceBase<MonitorResult, string>(MySqlServerConfiguration.ConnectionString, true);
            var all = await resultsDB.GetManyAsync(
                select: "Select *",
                where: string.Format("TestDate >= '{0}' AND TestDate < '{1}'", dateStartLimit.ToString("yyyy/MM/dd"), dateLimit.ToString("yyyy/MM/dd")),
                orderBy: "TestDate ASC");
            var allTestsNames = await resultsDB.JoinGetTypedAsync<MonitorResult>(
                selectColumns: "TestName",
                joinQuery: string.Format(" WHERE TestDate >= '{0}' AND TestDate < '{1}'", dateStartLimit.ToString("yyyy/MM/dd"), dateLimit.ToString("yyyy/MM/dd")),
                whereQuery: "",
                orderBy: "GROUP BY TestName",
                args: new Dictionary<string, object>());

            var aggregateData = new Dictionary<string, List<object>>();
            var returnData = new List<object[]>();
            aggregateData.Add("x", new List<object>());
            var allData = all as List<MonitorResult>;
            var samples = allData.GroupBy(s => s.TestDate.ToString("yyyyMMddHHmm"));
            var tests = allData.GroupBy(s => s.TestName).Select(g => g.Key); //TODO: Move this to MySQL

            foreach (var s in samples)
            {
                var sampled = tests.ToList();
                //Set the time of the sample(monitor) in the X axis
                aggregateData["x"].Add(s.First().TestDate.ToString("yyyy-MM-dd HH:mm:ss"));
                //fill all the samples for the particular time
                foreach (var m in s)
                {
                    InsertMonitorResult(m.TestName, aggregateData, m.Result);
                    sampled.Remove(m.TestName);
                }
                //Should we insert empty results or possitive? 
                //Inserting empty results shows an accurate chart but the line may appear interrupted
                //insert nulls for tests that were not sampled at this specific time
                foreach (var missing in sampled)
                {
                    InsertMonitorResult(missing, aggregateData, null);
                }

            }
            //format the results as needed by the Chart library
            foreach (var i in aggregateData)
            {
                i.Value.Insert(0, i.Key);
                returnData.Add(i.Value.ToArray());
            }

            return returnData.ToArray();
        }

        private void InsertMonitorResult(string key, Dictionary<string, List<object>> data, object result)
        {
            if (!data.ContainsKey(key))
            {
                data.Add(key, new List<object>());
            }
            data[key].Add(result);
        }
    }
}

    
