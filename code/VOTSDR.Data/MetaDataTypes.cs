using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace VOTSDR.Data
{

    #region SpecialNeedsStory

    [MetadataType(typeof(SpecialNeedsStory_Validation))]
    public partial class SpecialNeedsStory {}
    public class SpecialNeedsStory_Validation
    {
        [Required(ErrorMessage = "Story should be filled out.")]
        [Display(Name = "Story")]
        public bool Text { get; set; }

        [Required(ErrorMessage = "Date for sorting is required.")]
        [Display(Name = "Sorting Date")]
        public Nullable<DateTime> DateCreated { get; set; }
    }
    #endregion


    #region NewsStory
    [MetadataType(typeof(NewsStory_Validation))]
    public partial class NewsStory {}
    public class NewsStory_Validation
    {
        [StringLength(300, MinimumLength = 1)]
        [Required(ErrorMessage = "Friendly Date is required.")]
        [Display(Name = "Friendly Date")]
        public string Date { get; set; }

        [StringLength(100, MinimumLength = 1)]
        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Story is required.")]
        [Display(Name = "Story")]
        public string Text { get; set; }

        [Required(ErrorMessage = "Date for sorting is required.")]
        [Display(Name = "Sorting Date")]
        public Nullable<DateTime> DateCreated { get; set; }
    }
    #endregion


    #region Event
    [MetadataType(typeof(Event_Validation))]
    public partial class Event {}
    public class Event_Validation
    {
        [StringLength(100, MinimumLength = 1)]
        [Required(ErrorMessage = "Title is required.")]
        [Display(Name="Title")]
        public string Name { get; set; }

        [StringLength(4000, MinimumLength = 1)]
        [Required(ErrorMessage = "Location is required.")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }

        [StringLength(300, MinimumLength = 1)][Required()]
        public string Date { get; set; }

        [Required(ErrorMessage = "Date for sorting is required.")]
        [Display(Name = "Sorting Date")]
        public Nullable<DateTime> DateCreated { get; set; }
    }
    #endregion


}