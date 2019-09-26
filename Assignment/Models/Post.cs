 using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment.Models
{
    public class Post
    {
        
        public Guid ID { get; set; }

        
        public string Image { get; set; }

        public string Title { get; set; }

        public string SubTitle { get; set; }

        [AllowHtml]
        public string Content { get; set; }

        public Guid CategoryID { get; set; }

        public int ViewCount { get; set; }

        public string YoutubeLink { get; set; }

        public int Rating { get; set; }

        //fk tables
        public Category Category { get; set; }

        public ICollection<PostTag> PostTags { get; set; }


    }
}