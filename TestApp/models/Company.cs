using DataAccess.Scaffold.Attributes;
using Needletail.DataAccess.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApp.models
{
    public class Company
    {

        [Required]
        [TableKey(CanInsertKey = true)]
        public Guid Id { get; set; }

        [MaxLen(250)]
        public string Name { get; set; }

        [MaxLen(200)]
        public string CompanyAddress { get; set; }

        [MaxLen(200)]
        public string BillingContact { get; set; }

        [MaxLen(200)]
        public string BillingPhone { get; set; }

        [MaxLen(200)]
        public string BillingAddress { get; set; }

        public Guid? CompanyTypeId { get; set; }

        public Guid? StateId { get; set; }

        //public int IncrementalId { get; set; }

        //public string IncrementalPrefix { get; set; }

    }
}
