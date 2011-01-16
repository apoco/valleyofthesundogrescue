using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VOTSDR.Web.Models
{
    public class DogOfTheWeekSummaryViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string ThumbnailUrl { get; set; }

        public string Age { get; set; }

        public string Breed { get; set; }
    }
}