using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;

namespace Needletail.DataAccess.Engines
{
    public abstract class DBMSEngineBase : Needletail.DataAccess.Engines.IDBMSEngine 
    {

        public virtual void PrepareEngine(string connectionString, DbConnection connection)
        { 
        }

        public abstract string ObjectOpen { get; }

        public abstract string ObjectClose { get; }


        public virtual void ConfigureParameterForValue(DbParameter param, object value, byte precision = 10, byte scale = 2)
        {
            param.DbType = Converters.GetDBTypeFor(param.Value);
        }


        abstract public string GetQueryForPagination(string columns, string from, string where, string orderBy, int pageSize, int pageNumber, string key);


        public abstract string GetOrderByQuery(string orderBy, SQLTokens.OrderBy direction);


        public abstract string GetQueryTemplateForTop(string columns, string from, string where, string orderBy, int? topN);

        
        public virtual bool NeedLockOnConnection
        {
            get
            {
                return false;
            }
        }
        
    }
}
