using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Needletail.DataAccess.Factories
{
    public class NeedletailFactory : System.Data.Common.DbProviderFactory
    {
        public override DbCommand CreateCommand()
        {
            return new SqlCommand();
        }

        public override DbConnection CreateConnection()
        {
            return new SqlConnection();
        }

        public override DbConnectionStringBuilder CreateConnectionStringBuilder()
        {
            return new SqlConnectionStringBuilder();
        }

        public override DbParameter CreateParameter()
        {
            return new SqlParameter();
        }

        
    }
}
