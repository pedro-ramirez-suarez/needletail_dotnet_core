using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Scaffold.Attributes
{
    [System.AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = true)]
    public class HasMany: NeedletailRelationAttribute
    {

        public HasMany(string localList,string foreignKey, string referencedTable, string referencedKey) 
        {
            this.LocalList = localList;
            this.ForeignKey = foreignKey;
            this.ReferencedTable = referencedTable;
            this.ReferencedKey = referencedKey;
        }

        public string LocalList { get; private set; }
        public string ReferencedTable { get; private set; }
        public string ReferencedKey { get; private set; }
    }
}
