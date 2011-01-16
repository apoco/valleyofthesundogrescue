using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace VOTSDR.Admin.Web.Models
{
    #region SpecialNeedsStory

    [MetadataType(typeof(SpecialNeedsStory_Validation))]
    public partial class SpecialNeedsStory {}
    public class SpecialNeedsStory_Validation
    {
        [Required(ErrorMessage = "Story should be filled out.")]
        public bool Text{get;set;}
    }
    #endregion


    #region NewsStory
    [MetadataType(typeof(NewsStory_Validation))]
    public partial class NewsStory { }
    public class NewsStory_Validation
    {
        [StringLength(300, MinimumLength = 1)]
        public bool Date { get; set; }
        [StringLength(100, MinimumLength = 1)]
        public bool Title { get; set; }
        [Required(ErrorMessage = "Story is required.")]
        public bool Text { get; set; }
    }
    #endregion

    #region Event
    [MetadataType(typeof(NewsStory_Validation))]
    public partial class Event { }
    public class Event_Validation
    {
        [StringLength(100, MinimumLength = 1)]
        public bool Name { get; set; }
        [StringLength(4000, MinimumLength = 1)]
        public bool Location { get; set; }
        [Required(ErrorMessage = "Description is required.")]
        public bool Description { get; set; }
        [StringLength(300, MinimumLength = 1)]
        public bool Date { get; set; }
    }
    #endregion

}