using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment.Models
{
    public class PostTag
    {
        public Guid ID { get; set; }

        public Guid PostID { get; set; }

        public Guid TagId { get; set; }

        //fk tables
        public Post Post { get; set; }

        public Tag Tag { get; set; }
    }
}