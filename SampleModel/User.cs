using DataAccess.Scaffold.Attributes;
using Needletail.DataAccess.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleModel
{
    public class User
    {
        [TableKey(CanInsertKey =true)]
        public Guid Id { get; set; }


        public string UserName { get; set; }

        [Phone]
        public string Phone { get; set; }


        public DateTime MyDate { get; set; }
    }
}
