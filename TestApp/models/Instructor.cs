using System;
using System.Collections.Generic;
using System.Linq;
using Needletail.DataAccess.Attributes;
using DataAccess.Scaffold.Attributes;
using DataAccess.Scaffold.Attributes.UI;

namespace ConUniv.Models
{
    public class Instructor
    {
        
        [Required][TableKey(CanInsertKey = true)]
        public Guid Id { get; set; }
        
        [Required][MaxLen(50)]
        public string LastName { get; set; }
        
        [Required][MaxLen(50)]
        public string FirstName { get; set; }
        
        
        public DateTime HireDate { get; set; }

        [Hidden]
        public string Discriminator { get { return "Instructor"; } }
        
    }
}
