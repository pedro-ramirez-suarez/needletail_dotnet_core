﻿using DataAccess.Scaffold.Attributes;
using Needletail.DataAccess;
using Needletail.DataAccess.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Scaffold.ViewModels
{
    public abstract class ViewModelAutoLoadAndSaveAsync
    {


        /// <summary>
        /// This will fill all the entities with the proper records
        /// </summary>
        public ViewModelAutoLoadAndSaveAsync()
        {



        }

        public async virtual Task FillDataAsync(object primaryKey)
        {
            await FillDataAsync("Default", primaryKey, this);
        }

        public async virtual Task FillDataAsync(object primaryKey, object fillMe)
        {
            await FillDataAsync("Default", primaryKey, fillMe);
        }

        public async virtual Task FillDataAsync(string connectionString, object primaryKey, object me)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                connectionString = "Default";
            if (me == null)
                me = this;
            var props = me.GetType().GetTypeInfo().GetProperties();
            Dictionary<string, List<object>> vmAttributes = new Dictionary<string, List<object>>();
            Dictionary<string, List<object>> catalogs = new Dictionary<string, List<object>>();

            //It's a view model, get all  the Relation and UI attributes first for all the properties
            foreach (var p in props)
            {
                var relAtts = p.GetCustomAttributes(typeof(NeedletailRelationAttribute), true);
                var uiAtts = p.GetCustomAttributes(typeof(NeedletailUIAttribute), true);
                var cat = p.GetCustomAttributes(typeof(Catalog), true);
                if (relAtts.Any() || uiAtts.Any())
                {
                    vmAttributes.Add(p.Name, new List<object>());
                    vmAttributes[p.Name].AddRange(relAtts);
                    vmAttributes[p.Name].AddRange(uiAtts);
                }
                if (cat.Any())
                {
                    var cats = new List<object>();
                    cats.AddRange(cat);
                    catalogs.Add(p.Name, cats);
                }

            }
            PropertyInfo key;
            //process catalogs first
            if (catalogs.Count > 0)
                foreach (var c in catalogs)
                {
                    //get the property
                    var cl = props.FirstOrDefault(p => p.Name == c.Key);
                    var prop = props.FirstOrDefault(p => p.Name == c.Key);
                    //get the whole list
                    var item = cl.PropertyType.GetTypeInfo().GetProperties()[0];
                    dynamic context = CreateDataContextFor(item.PropertyType, out key);
                    dynamic list = await context.GetAllAsync();
                    prop.SetValue(me, list);
                    context.Dispose();
                }

            //First process all objects that are independent from others
            var ind = props.Where(p => vmAttributes.ContainsKey(p.Name));
            //In theory just one property should be here

            dynamic record = null;
            foreach (var i in ind)
            {
                dynamic context = CreateDataContextFor(i.PropertyType, out key);
                var pars = new Dictionary<string, object>();
                pars.Add("id", primaryKey);
                record = await context.GetSingleAsync(string.Format("{0}=@id", key.Name), pars);
                if (record == null)
                    record = Activator.CreateInstance(i.PropertyType);
                i.SetValue(me, record);
                context.Dispose();
            }

            //Fill SelectFrom
            var attribs = new List<object>();
            foreach (var e in vmAttributes)
                attribs.AddRange(e.Value.Where(a => (a as SelectFrom) != null));
            foreach (dynamic s in attribs)
            {
                //Get the property that represents the list
                var prop = props.FirstOrDefault(p => p.Name == s.LocalList);
                //Get the type of the list
                var item = prop.PropertyType.GetTypeInfo().GetProperties()[0];
                dynamic context = CreateDataContextFor(item.PropertyType, out key);
                dynamic list = await context.GetAllAsync();
                prop.SetValue(me, list);
                context.Dispose();
            }


            //Fill HasOne
            attribs = new List<object>();
            foreach (var e in vmAttributes)
                attribs.AddRange(e.Value.Where(a => (a as HasOne) != null));
            foreach (dynamic s in attribs)
            {
                //Get the type of the list
                var item = props.FirstOrDefault(p => p.Name == s.LocalObject);
                dynamic context = CreateDataContextFor(item.PropertyType, out key);
                var pars = new Dictionary<string, object>();
                //Get the value of the foreign key
                PropertyInfo[] recordProps = record.GetType().GetProperties() as PropertyInfo[];
                var fkProp = recordProps.FirstOrDefault(p => p.Name == s.ForeignKey);
                var fkVal = fkProp.GetValue(record);
                pars.Add("foreignKey", fkVal);
                string where = string.Format("{0} = @foreignKey", s.ReferencedField);
                dynamic list = await context.GetManyAsync(where, string.Empty, pars, 0, int.MaxValue);
                if (list.Count > 0)
                    item.SetValue(me, list[0]);
                else
                    item.SetValue(me, Activator.CreateInstance(item.PropertyType));
                context.Dispose();
            }

            //Fill HasMany
            attribs = new List<object>();
            foreach (var e in vmAttributes)
                attribs.AddRange(e.Value.Where(a => (a as HasMany) != null));
            foreach (dynamic s in attribs)
            {
                //Get the property that represents the list
                var prop = props.FirstOrDefault(p => p.Name == s.LocalList);
                //Get the type of the list
                var item = prop.PropertyType.GetTypeInfo().GetProperties()[0];
                dynamic context = CreateDataContextFor(item.PropertyType, out key);
                var pars = new Dictionary<string, object>();
                //Get the value of the foreign key
                PropertyInfo[] recordProps = record.GetType().GetProperties() as PropertyInfo[];
                var fkProp = recordProps.FirstOrDefault(p => p.Name == s.ForeignKey);
                var fkVal = fkProp.GetValue(record);
                pars.Add("foreignKey", fkVal);
                string where = string.Format("{0} = @foreignKey", s.ReferencedKey);
                dynamic list = await context.GetManyAsync(where, string.Empty, pars, 0, int.MaxValue);
                prop.SetValue(me, list);
                context.Dispose();
            }

            //Fill HasManyNtoN
            //Do a join
            attribs = new List<object>();
            foreach (var e in vmAttributes)
                attribs.AddRange(e.Value.Where(a => (a as HasManyNtoN) != null));
            foreach (dynamic s in attribs)
            {
                //Get the property that represents the list
                var prop = props.FirstOrDefault(p => p.Name == s.LocalList);
                //Get the type of the list
                var item = prop.PropertyType.GetTypeInfo().GetProperties()[0];
                dynamic context = CreateDataContextFor(item.PropertyType, out key);
                var pars = new Dictionary<string, object>();
                //Get the value of the foreign key
                PropertyInfo[] recordProps = record.GetType().GetProperties() as PropertyInfo[];
                var fkProp = recordProps.FirstOrDefault(p => p.Name == s.ForeignKey);
                var fkVal = fkProp.GetValue(record);
                pars.Add("foreignKey", fkVal);
                string select = string.Format("[{0}].*", item.PropertyType.Name);
                string join = string.Format("Inner Join [{0}] On [{1}].{2} = [{3}].{4} AND [{0}].[{5}] = @foreignKey", s.RelationTable, item.PropertyType.Name, fkProp.Name, s.RelationTable, s.RelationTableReferencedKey, s.RelationTableForeignKey);
                string where = string.Empty;

                MethodInfo method = context.GetType().GetMethod("JoinGetTyped");
                MethodInfo generic = method.MakeGenericMethod(item.PropertyType);

                dynamic list = generic.Invoke(context, new object[] { select, join, where, string.Empty, pars });
                prop.SetValue(me, list);
                context.Dispose();
            }
        }

        protected dynamic CreateDataContextFor(Type itemType, out PropertyInfo key)
        {
            //get the type of the property marked as the ID
            key = itemType.GetTypeInfo().GetProperties().FirstOrDefault(p => p.GetCustomAttributes(typeof(TableKeyAttribute), false) != null);

            Type d1 = typeof(DBTableDataSourceBase<,>);
            Type[] typeArgs = { itemType, key.PropertyType };
            Type generic = d1.MakeGenericType(typeArgs);
            dynamic context = Activator.CreateInstance(generic);

            return context;
        }

        private bool IsNew(object entity, dynamic context)
        {
            var key = entity.GetType().GetTypeInfo().GetProperties().FirstOrDefault(p => p.GetCustomAttributes(typeof(TableKeyAttribute), false) != null);
            object keyValue = key.GetValue(entity);
            //Check if the record already exists
            var pars = new Dictionary<string, object>();
            pars.Add("id", keyValue);
            string sql = string.Format("Select count(*) from [{0}] Where {1} = @id", entity.GetType().Name, key.Name);
            int total = context.ExecuteScalar<int>(sql, pars);
            if (total <= 0)
                return true;
            return false;

        }

        /// <summary>
        /// Saves all the data
        /// </summary>
        public async virtual Task SaveAsync(string connectionString = "")
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                connectionString = "Default";
            object me = this;

            var props = me.GetType().GetTypeInfo().GetProperties();
            Dictionary<string, List<object>> vmAttributes = new Dictionary<string, List<object>>();

            //It's a view model, get all  the Relation and UI attributes first for all the properties
            foreach (var p in props)
            {
                var relAtts = p.GetCustomAttributes(typeof(NeedletailRelationAttribute), true);
                var uiAtts = p.GetCustomAttributes(typeof(NeedletailUIAttribute), true);
                if (relAtts.Any() || uiAtts.Any())
                {
                    vmAttributes.Add(p.Name, new List<object>());
                    vmAttributes[p.Name].AddRange(relAtts);
                    vmAttributes[p.Name].AddRange(uiAtts);
                }
            }
            //First process all objects that are independent from others
            var ind = props.Where(p => vmAttributes.ContainsKey(p.Name));
            //In theory just one property should be here
            PropertyInfo key;
            dynamic record = null;
            foreach (var i in ind)
            {
                dynamic context = CreateDataContextFor(i.PropertyType, out key);
                record = i.GetValue(me);
                //Check if is an add or update
                if (IsNew(record, context))
                    await context.InsertAsync(record);
                else
                    await context.UpdateAsync(record);
                context.Dispose();
            }


            if (record == null)
                return;
            //Process HasOne
            var attribs = new List<object>();
            foreach (var e in vmAttributes)
                attribs.AddRange(e.Value.Where(a => (a as HasOne) != null));
            foreach (dynamic s in attribs)
            {
                //Get the type of the list
                var item = props.FirstOrDefault(p => p.Name == s.LocalObject);
                dynamic context = CreateDataContextFor(item.PropertyType, out key);
                record = item.GetValue(me);
                if (IsNew(record, context))
                    await context.InsertAsync(record);
                else
                    await context.UpdateAsync(record);
                context.Dispose();
            }

            /* For now only single entities and has one are being saved
             * HasMany and HasManyNtoN are not for now
             */

            ////Fill HasMany
            //attribs = new List<object>();
            //foreach (var e in vmAttributes)
            //    attribs.AddRange(e.Value.Where(a => (a as HasMany) != null));
            //foreach (dynamic s in attribs)
            //{
            //    //Get the property that represents the list
            //    var prop = props.FirstOrDefault(p => p.Name == s.LocalList);
            //    //Get the type of the list
            //    var item = prop.PropertyType.GetProperties()[0];
            //    dynamic context = CreateDataContextFor(item.PropertyType, out key);
            //    var pars = new Dictionary<string, object>();
            //    //Get the value of the foreign key
            //    PropertyInfo[] recordProps = record.GetType().GetProperties() as PropertyInfo[];
            //    var fkProp = recordProps.FirstOrDefault(p => p.Name == s.ForeignKey);
            //    var fkVal = fkProp.GetValue(record);
            //    pars.Add("foreignKey", fkVal);
            //    string where = string.Format("{0} = @foreignKey", s.ReferencedKey);
            //    dynamic list = context.GetMany(where, string.Empty, pars, 0, int.MaxValue);
            //    prop.SetValue(me, list);
            //}

            ////Fill HasManyNtoN
            ////Do a join
            //attribs = new List<object>();
            //foreach (var e in vmAttributes)
            //    attribs.AddRange(e.Value.Where(a => (a as HasManyNtoN) != null));
            //foreach (dynamic s in attribs)
            //{
            //    //Get the property that represents the list
            //    var prop = props.FirstOrDefault(p => p.Name == s.LocalList);
            //    //Get the type of the list
            //    var item = prop.PropertyType.GetProperties()[0];
            //    dynamic context = CreateDataContextFor(item.PropertyType, out key);
            //    var pars = new Dictionary<string, object>();
            //    //Get the value of the foreign key
            //    PropertyInfo[] recordProps = record.GetType().GetProperties() as PropertyInfo[];
            //    var fkProp = recordProps.FirstOrDefault(p => p.Name == s.ForeignKey);
            //    var fkVal = fkProp.GetValue(record);
            //    pars.Add("foreignKey", fkVal);
            //    string select = string.Format("[{0}].*", item.PropertyType.Name);
            //    string join = string.Format("Inner Join [{0}] On [{1}].{2} = [{3}].{4} AND [{0}].[{5}] = @foreignKey", s.RelationTable, item.PropertyType.Name, fkProp.Name, s.RelationTable, s.RelationTableReferencedKey, s.RelationTableForeignKey);
            //    string where = string.Empty;

            //    MethodInfo method = context.GetType().GetMethod("JoinGetTyped");
            //    MethodInfo generic = method.MakeGenericMethod(item.PropertyType);

            //    dynamic list = generic.Invoke(context, new object[] { select, join, where, string.Empty, pars });


            //    prop.SetValue(me, list);
            //}
        }
    }
}
