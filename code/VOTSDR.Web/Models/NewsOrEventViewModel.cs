using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VOTSDR.Web.Models
{
    public class NewsOrEventViewModel
    {
        public Guid Id { get; set; }

        public string Date { get; set; }

        public DateTime? SortDate { get; set; }

        public string Title { get; set; }

        public string Summary { get; set; }

        public string Location { get; set; }

        public bool IsEvent { get; set; }

        public bool IsUpcoming { get; set; }
    }
}