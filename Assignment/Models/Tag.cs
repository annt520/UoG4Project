using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment.Models
{
    public class Tag
    {
       public Guid ID { get; set; }


       [Required]
       public string TagName { get; set; }

       public virtual ICollection<PostTag> PostTag { get; set; }
    }
}