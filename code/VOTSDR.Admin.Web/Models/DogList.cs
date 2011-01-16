using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VOTSDR.Admin.Web.Models
{
    public class DogList
    {
        public Guid DogId { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Breed { get; set; }
        public DateTime? Birthday { get; set; }
        public DateTime? AdoptedDate { get; set; }
        public byte[] Thumbnail { get; set; }
    }
}