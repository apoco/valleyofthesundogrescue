using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VOTSDR.Web.Models
{
    public class NewsOrEventSummaryViewModel
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
    }
}