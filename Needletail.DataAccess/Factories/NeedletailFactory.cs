using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Needletail.DataAccess.Factories
{
    public class NeedletailFactory : System.Data.Common.DbProviderFactory
    {
        private bool IsMySql {get; set; }
        public NeedletailFactory(bool isMySql)
        {
            this.IsMySql = isMySql;
        }

        public override DbCommand CreateCommand()
        {
            if (IsMySql)
                return new MySqlCommand();
            else
                return new SqlCommand();
        }

        public override DbConnection CreateConnection()
        {
            if (IsMySql)
                return new MySqlConnection();
            else
                return new SqlConnection();
            }

        public override DbConnectionStringBuilder CreateConnectionStringBuilder()
        {
            if (IsMySql)
                return new MySqlConnectionStringBuilder();
            else
                return new SqlConnectionStringBuilder();    
            
        }

        public override DbParameter CreateParameter()
        {
            if (IsMySql)
                return new MySqlParameter();
            else
                return new SqlParameter();
            }

        
    }
}
