using DataAnnotationsExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PodioAspNetSample.ViewModels
{
    public class LeadListingViewModel
    {
       public List<LeadView> Leads { get; set; }
       public SelectList LeadOwnersOptions { get; set; }
       public SelectList StatusOptions { get; set; }

       [DisplayName("Next follow-up")]
       public DateTime? NextFollowUpFrom { get; set; }
       public DateTime? NextFollowUpTo { get; set; }

       [DisplayName("Expected total value of lead")]
       [Integer]
       public decimal? ExpectedValueFrom { get; set; }
       [Integer]
       public decimal? ExpectedValueTo { get; set; }
       public int? Status { get; set; }
       public int? LeadOwner { get; set; }   
    }
    public class LeadView
    {
        public int PodioItemID { get; set; }
        public string Company { get; set; }
        public Dictionary<int, string> LeadOwners { get; set; }
        public decimal? ExpectedValue { get; set; }
        public Tuple<int, string> Status { get; set; }
        public DateTime? NextFollowUp { get; set; }
    }
}