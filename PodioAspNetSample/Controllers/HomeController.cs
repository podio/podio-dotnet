using PodioAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PodioAspNetSample.Models;
using System.Web.Routing;
using PodioAspNetSample.ViewModels;

namespace PodioAspNetSample.Controllers
{
    [LoadClient]
    public class HomeController : Controller
    {
        public Podio PodioClient { get; set; }

        public ActionResult Index()
        {
            var lead = new Lead();
            var spaceUsers = lead.GetUsers();
            var statuses = lead.GetAllStatuses();
            var leads = lead.GetAllLeads();

            var model = new LeadListingViewModel();
            LeadViewModelOptions leadModelOption = new LeadViewModelOptions();
            if (spaceUsers.Any())
                leadModelOption.LeadOwnersOptions = spaceUsers.Select(x => new SelectListItem { Text = x.Name, Value = x.ProfileId.ToString() });

            if (statuses.Any())
                leadModelOption.StatusOptions = statuses.Select(x => new SelectListItem { Text = x.Value.ToString(), Value = x.Key.ToString() });

            if (leads.Any())
                model.Leads = leads.Select(x => 
                    new LeadView { Company = x.Company, 
                        LeadOwners = x.LeadOwners, 
                        ExpectedValue = x.ExpectedValue, 
                        Status = x.Status, 
                        NextFollowUp = x.NextFollowUp })
                        .ToList();

            model.LeadViewModelOptions = leadModelOption;
            return View(model);        
        }

        [HttpPost]
        public ActionResult Filter(FormCollection formCollection)
        {
            var nextFollowUpFromDate = !string.IsNullOrEmpty(formCollection["inputFromDate"]) ? DateTime.Parse(formCollection["inputFromDate"]) : (DateTime?)null;
            var nextFollowUpToDate =  !string.IsNullOrEmpty(formCollection["inputToDate"]) ? DateTime.Parse(formCollection["inputToDate"]) : (DateTime?)null;
            var expectedValueFrom = !string.IsNullOrEmpty(formCollection["inputExpectedValueFrom"]) ? int.Parse(formCollection["inputExpectedValueFrom"]) : (int?)null;
            var expectedValueTo = !string.IsNullOrEmpty(formCollection["inputExpectedValueTo"]) ? int.Parse(formCollection["inputExpectedValueTo"]) : (int?)null;
            var status = !string.IsNullOrEmpty(formCollection["ddlStatus"]) && formCollection["ddlStatus"] != "-1" ? int.Parse(formCollection["ddlStatus"]) : (int?)null;
            var leadOwner = !string.IsNullOrEmpty(formCollection["ddlLeadOwner"]) && formCollection["ddlLeadOwner"] != "-1" ? int.Parse(formCollection["ddlLeadOwner"]) : (int?)null;

            var lead = new Lead();
            var spaceUsers = lead.GetUsers();
            var statuses = lead.GetAllStatuses();
            var leads = lead.GetAllLeads(nextFollowUpFromDate, nextFollowUpToDate, expectedValueFrom, expectedValueTo, status, leadOwner);

            var model = new LeadListingViewModel();
            LeadViewModelOptions leadModelOption = new LeadViewModelOptions();
            if (spaceUsers.Any())
                leadModelOption.LeadOwnersOptions = spaceUsers.Select(x => new SelectListItem { Text = x.Name, Value = x.ProfileId.ToString() });

            if (statuses.Any())
                leadModelOption.StatusOptions = statuses.Select(x => new SelectListItem { Text = x.Value.ToString(), Value = x.Key.ToString() });

            if (leads.Any())
                model.Leads = leads.Select(x =>
                    new LeadView
                    {
                        Company = x.Company,
                        LeadOwners = x.LeadOwners,
                        ExpectedValue = x.ExpectedValue,
                        Status = x.Status,
                        NextFollowUp = x.NextFollowUp})
                        .ToList();

            model.LeadViewModelOptions = leadModelOption;
            return View("Index", model);
        }

        public ActionResult Create()
        {
            var lead = new Lead();

            var spaceContacts = lead.GetSpaceContacts();
            var spaceUsers = lead.GetUsers();
            var statuses = lead.GetAllStatuses();
            var leadContactsItems = spaceContacts.Select(x => new SelectListItem { Text = x.Name, Value = x.ProfileId.ToString() });
            var leadOwnerItems = spaceUsers.Select(x => new SelectListItem { Text = x.Name, Value = x.ProfileId.ToString() });
            var statusOptions = statuses.Select(x => new SelectListItem { Text = x.Value.ToString(), Value = x.Key.ToString() });

            var model = new LeadViewModel();
            model.LeadContactsOptions = leadContactsItems;
            model.LeadOwnersOptions = leadOwnerItems;
            model.StatusOptions = statusOptions;

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(LeadViewModel leadViewModel)
        {
            var lead = new Lead();

            lead.Company = leadViewModel.Company;
            lead.ExpectedValue = leadViewModel.ExpectedValue;
            lead.ProbabilityOfSale = leadViewModel.ProbabilityOfSale;
            lead.Status = new Tuple<int, string>(leadViewModel.Status, "");
            lead.NextFollowUp = leadViewModel.NextFollowUp;
            lead.StreetAddress = leadViewModel.StreetAddress;
            lead.City = leadViewModel.City;
            lead.State = leadViewModel.State;
            lead.Zip = leadViewModel.Zip;
            lead.Country = leadViewModel.Country;

            if (leadViewModel.LeadContacts != null && leadViewModel.LeadContacts.Any())
                lead.Contacts = leadViewModel.LeadContacts.ToDictionary(k => k.Value, v => v.Value.ToString());

            if (leadViewModel.LeadOwners != null && leadViewModel.LeadOwners.Any())
                lead.LeadOwners = leadViewModel.LeadOwners.ToDictionary(k => k.Value, v => v.Value.ToString());

            int itemId = lead.CreateLead(lead);

            return RedirectToAction("Index");
        }

        public class LoadClient : ActionFilterAttribute, IActionFilter
        {
            void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
            {
                var podioConnection = new PodioConnection();
                if (podioConnection.IsAuthenticated) 
                {
                    ((HomeController)filterContext.Controller).PodioClient = podioConnection.GetClient();
                }
                else
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "Index", controller = "Authorization" }));
                }
                this.OnActionExecuting(filterContext);
            }
        }
    }
}
