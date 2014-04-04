using DataAnnotationsExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PodioAspNetSample.ViewModels
{
    public class LeadViewModel
    {
        public int PodioItemID { get; set; }

        [Required]
        public string Company { get; set; }

        public int?[] LeadContacts { get; set; }

        public MultiSelectList LeadContactsOptions { get; set; }

        public int?[] LeadOwners { get; set; }

        public MultiSelectList LeadOwnersOptions { get; set; }

        [Numeric]
        public decimal? ExpectedValue { get; set; }

        [Integer]
        [Range(0, 100)]
        public int? ProbabilityOfSale { get; set; }

        public int Status { get; set; }

        public SelectList StatusOptions { get; set; }

        public DateTime? NextFollowUp { get; set; }

        public string StreetAddress { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Zip { get; set; }

        public string Country { get; set; }

        public string OfficeAddressMap { get; set; }
    }
}