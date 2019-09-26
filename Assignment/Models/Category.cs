using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment.Models
{
    public class Category
    {
        
        public Guid ID { get; set; }

        
        public string CategoryName { get; set; }
    }
}