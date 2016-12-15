using Needletail.DataAccess.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Generator.Models
{
    public class TestModel
    {

        [TableKey(CanInsertKey =true)]
        public Guid Id { get; set; }
    }
}
