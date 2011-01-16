using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace VOTSDR.Web.Models
{
    public class AdoptableDogViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string ThumbnailUrl { get; set; }
        public string Profile { get; set; }
        public string Age { get; set; }
        public bool Featured { get; set; }
        public string Breed { get; set; }
    }
}