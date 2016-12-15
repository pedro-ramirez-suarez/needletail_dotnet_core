using Needletail.DataAccess;
using Needletail.DataAccess.Engines;
using Needletail.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConUniv.Repositories
{

    /*
     * 
     *  
        using System;
        using System.Collections.Generic;
        using System.Linq;
        using System.Threading.Tasks;

        namespace ConUniv.Repositories
        {
            public class EmptyRepo : RepositoryBase<class,key>
            {
            }
        }
     */
    public abstract class RepositoryBase<E,K>: IDisposable, IDataSource<E, K>, IDataSourceAsync<E,K> where  E: class, new()
    {
        
        private DBTableDataSourceBase<E, K> tableAccess;

        string ConnectionString { get; set; }

        string tableName;
        private string TableName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(tableName))
                {
                }
                return tableName;
            }
            set
            {
                tableName = value;
            }
        }

        public RepositoryBase()
        {
            //change this if necessary
            this.TableName = typeof(E).Name;
            this.ConnectionString = "DefaultConnection";

            tableAccess = new DBTableDataSourceBase<E, K>(this.ConnectionString,this.TableName);
        }


        public virtual bool Delete(object where, FilterType filterType)
        {
            return this.tableAccess.Delete(where: where, filterType: filterType);
        }

        public virtual bool Delete(object where)
        {
            return this.tableAccess.Delete(where: where);
        }

        public virtual bool DeleteEntity(E item)
        {
            return this.tableAccess.DeleteEntity(item: item);
        }

        public virtual IEnumerable<E> GetAll()
        {
            return this.tableAccess.GetAll();
        }

        public virtual IEnumerable<E> GetAll(object orderBy)
        {
            return this.tableAccess.GetAll(orderBy: orderBy);
        }

        public virtual IEnumerable<E> GetMany(string select, string where, string orderBy)
        {
            return this.tableAccess.GetMany(select: select, where: where, orderBy: orderBy);
        }

        public virtual IEnumerable<E> GetMany(object where)
        {
            return this.tableAccess.GetMany(where: where);
        }

        public virtual IEnumerable<E> GetMany(object where, object orderBy)
        {
            return this.tableAccess.GetMany(where: where, orderBy: orderBy);
        }

        public virtual IEnumerable<E> GetMany(object where, FilterType filterType, object orderBy, int? topN)
        {
            return this.tableAccess.GetMany(where: where, filterType: filterType, orderBy: orderBy, topN: topN);
        }

        public virtual IEnumerable<E> GetMany(object where, object orderBy, FilterType filterType, int page, int pageSize)
        {
            return this.tableAccess.GetMany(where: where, orderBy: orderBy, filterType: filterType, page: page, pageSize: pageSize);
        }

        public virtual IEnumerable<E> GetMany(string where, string orderBy, Dictionary<string, object> args, int page, int pageSize)
        {
            return this.tableAccess.GetMany(where: where, orderBy: orderBy, args: args, page: page, pageSize: pageSize);
        }

        public virtual IEnumerable<E> GetMany(string where, string orderBy, Dictionary<string, object> args, int? topN)
        {
            return this.tableAccess.GetMany(where: where, orderBy: orderBy, args: args, topN: topN);
        }

        public virtual IEnumerable<DynamicEntity> Join(string selectColumns, string joinQuery, string whereQuery, string orderBy, Dictionary<string, object> args)
        {
            return this.tableAccess.Join(selectColumns: selectColumns, joinQuery: joinQuery, whereQuery: whereQuery, orderBy: orderBy, args: args);
        }

        public virtual IEnumerable<T> JoinGetTyped<T>(string selectColumns, string joinQuery, string whereQuery, string orderBy, Dictionary<string, object> args)
        {
            return this.tableAccess.JoinGetTyped<T>(selectColumns: selectColumns, joinQuery: joinQuery, whereQuery: whereQuery, orderBy: orderBy, args: args);
        }

        public virtual E GetSingle(object where)
        {
            return this.tableAccess.GetSingle(where: where);
        }

        public virtual E GetSingle(string where, Dictionary<string, object> args)
        {
            return this.tableAccess.GetSingle(where: where, args: args);
        }

        public virtual E GetSingle(object where, FilterType filterType)
        {
            return this.tableAccess.GetSingle(where: where, filterType: filterType);
        }

        public virtual async Task<E> GetSingleAsync(object where)
        {
            return await this.tableAccess.GetSingleAsync(where: where);
        }


        public virtual E GetSingleAsync(object where, FilterType filterType)
        {
            return this.tableAccess.GetSingle(where: where, filterType: filterType);
        }

        public virtual async Task<E> GetSingleAsync(string where, Dictionary<string, object> args)
        {
            return await this.tableAccess.GetSingleAsync(where: where, args: args);
        }



        public virtual K Insert(E newItem)
        {
            return this.tableAccess.Insert(newItem: newItem);
        }

        public virtual bool Update(object item)
        {
            return this.tableAccess.Update(item: item);
        }

        public virtual bool UpdateWithWhere(object values, object where, FilterType filterType)
        {
            return this.tableAccess.UpdateWithWhere(values: values, where: where, filterType: filterType);
        }

        public virtual bool UpdateWithWhere(object values, object where)
        {
            return this.tableAccess.UpdateWithWhere(values: values, where: where);
        }

        public virtual void ExecuteNonQuery(string query, Dictionary<string, object> args)
        {
            this.tableAccess.ExecuteNonQuery(query: query, args: args);
        }

        public virtual T ExecuteScalar<T>(string query, Dictionary<string, object> args)
        {
            return this.tableAccess.ExecuteScalar<T>(query: query, args: args);
        }

        public virtual void ExecuteStoredProcedure(string name, object parameters)
        {
            this.tableAccess.ExecuteStoredProcedure(name, parameters);
        }

        public virtual IEnumerable<T> ExecuteStoredProcedureReturnRows<T>(string name, object parameters)
        {
            return this.tableAccess.ExecuteStoredProcedureReturnRows<T>(name, parameters);
        }

        public virtual void Dispose()
        {
            this.tableAccess.Dispose();
        }


        public virtual IEnumerable<DynamicEntity> ExecuteStoredProcedureReturnDynaimcRows(string name, object parameters)
        {
            return this.tableAccess.ExecuteStoredProcedureReturnDynaimcRows(name, parameters);
        }

        public virtual void BeginTransaction(System.Data.IsolationLevel level)
        {
            this.tableAccess.BeginTransaction(level);
        }

        public virtual void CommitTransaction()
        {
            this.tableAccess.CommitTransaction();
        }

        public virtual void RollbackTransaction()
        {
            this.tableAccess.RollbackTransaction();
        }

        public virtual Task<bool> DeleteAsync(object where, FilterType filterType)
        {
            return this.tableAccess.DeleteAsync(where, filterType);
        }

        public virtual Task<bool> DeleteAsync(object where)
        {
            return this.tableAccess.DeleteAsync(where);
        }

        public virtual Task<bool> DeleteEntityAsync(E item)
        {
            return this.tableAccess.DeleteEntityAsync(item);
        }

        public virtual Task<IEnumerable<E>> GetAllAsync()
        {
            return this.tableAccess.GetAllAsync();
        }

        public virtual Task<IEnumerable<E>> GetAllAsync(object orderBy)
        {
            return this.tableAccess.GetAllAsync(orderBy);
        }

        public virtual Task<IEnumerable<E>> GetManyAsync(string select, string where, string orderBy)
        {
            return this.tableAccess.GetManyAsync(select, where, orderBy);
        }

        public virtual Task<IEnumerable<E>> GetManyAsync(object where)
        {
            return this.tableAccess.GetManyAsync(where);
        }

        public virtual Task<IEnumerable<E>> GetManyAsync(object where, object orderBy)
        {
            return this.tableAccess.GetManyAsync(where, orderBy);
        }

        public virtual Task<IEnumerable<E>> GetManyAsync(object where, FilterType filterType, object orderBy, int? topN)
        {
            return this.tableAccess.GetManyAsync(where, filterType, orderBy, topN);
        }

        public virtual Task<IEnumerable<E>> GetManyAsync(object where, object orderBy, FilterType filterType, int page, int pageSize)
        {
            return this.tableAccess.GetManyAsync(where, orderBy, filterType, page, pageSize);
        }

        public virtual Task<IEnumerable<E>> GetManyAsync(string where, string orderBy, Dictionary<string, object> args, int page, int pageSize)
        {
            return this.tableAccess.GetManyAsync(where, orderBy, args, page, pageSize);
        }

        public virtual Task<IEnumerable<E>> GetManyAsync(string where, string orderBy, Dictionary<string, object> args, int? topN)
        {
            return this.tableAccess.GetManyAsync(where, orderBy, args, topN);
        }

        public virtual Task<IEnumerable<DynamicEntity>> JoinAsync(string selectColumns, string joinQuery, string whereQuery, string orderBy, Dictionary<string, object> args)
        {
            return this.tableAccess.JoinAsync(selectColumns, joinQuery, whereQuery, orderBy, args);
        }

        public virtual Task<IEnumerable<T>> JoinGetTypedAsync<T>(string selectColumns, string joinQuery, string whereQuery, string orderBy, Dictionary<string, object> args)
        {
            return this.tableAccess.JoinGetTypedAsync<T>(selectColumns, joinQuery, whereQuery, orderBy, args);
        }

        Task<E> IDataSourceAsync<E, K>.GetSingleAsync(object where, FilterType filterType)
        {
            return this.tableAccess.GetSingleAsync(where, filterType);
        }

        public virtual Task<K> InsertAsync(E newItem)
        {
            return this.tableAccess.InsertAsync(newItem);
        }

        public virtual Task<bool> UpdateAsync(object item)
        {
            return this.tableAccess.UpdateAsync(item);
        }

        public virtual Task<bool> UpdateWithWhereAsync(object values, object where, FilterType filterType)
        {
            return this.tableAccess.UpdateWithWhereAsync(values, where, filterType);
        }

        public virtual Task<bool> UpdateWithWhereAsync(object values, object where)
        {
            return this.tableAccess.UpdateWithWhereAsync(values, where);
        }

        public virtual Task ExecuteNonQueryAsync(string query, Dictionary<string, object> args)
        {
            return this.tableAccess.ExecuteNonQueryAsync(query, args);
        }

        public virtual Task<T> ExecuteScalarAsync<T>(string query, Dictionary<string, object> args)
        {
            return this.tableAccess.ExecuteScalarAsync<T>(query, args);
        }

        public virtual Task ExecuteStoredProcedureAsync(string name, object parameters)
        {
            return this.tableAccess.ExecuteStoredProcedureAsync(name, parameters);
        }

        public virtual Task<IEnumerable<T>> ExecuteStoredProcedureReturnRowsAsync<T>(string name, object parameters)
        {
            return this.tableAccess.ExecuteStoredProcedureReturnRowsAsync<T>(name, parameters);
        }

        public virtual Task<IEnumerable<DynamicEntity>> ExecuteStoredProcedureReturnDynamicRowsAsync(string name, object parameters)
        {
            return this.tableAccess.ExecuteStoredProcedureReturnDynamicRowsAsync(name, parameters);
        }
    }
}
