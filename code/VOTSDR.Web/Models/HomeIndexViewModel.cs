using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace VOTSDR.Web.Models
{
    public class HomeIndexViewModel
    {
        public Guid FeaturedDogId { get; set; }
        public string FeaturedDogName { get; set; }
        public string FeaturedDogThumbnailUrl { get; set; }
        public string FeaturedDogDescription { get; set; }
        public string FeaturedDogProfile { get; set; }

        public string SpecialNeedThumbnailUrl { get; set; }
        public string SpecialNeedDescription { get; set; }

        public IEnumerable<NewsOrEventSummaryViewModel> NewsAndEvents { get; set; }
    }
}