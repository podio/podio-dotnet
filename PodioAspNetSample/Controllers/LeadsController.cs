using PodioAPI;
using System;
using System.Linq;
using System.Web.Mvc;
using PodioAspNetSample.Models;
using System.Web.Routing;
using PodioAspNetSample.ViewModels;
using PodioAPI.Exceptions;
using System.Collections.Generic;
using PodioAPI.Models;

namespace PodioAspNetSample.Controllers
{
    [LoadClient]
    public class LeadsController : Controller
    {
        public Podio PodioClient { get; set; }

        public ActionResult Index()
        {
            ViewBag.message = TempData["message"];
            ViewBag.error = TempData["error"];

            var model = new LeadListingViewModel();
            var lead = new Lead();

            try
            {
                List<Contact> spaceUsers = lead.GetUsers();
                Dictionary<int,string> statuses = lead.GetAllStatuses();
                IEnumerable<Lead> leads = lead.GetAllLeads();

                model.LeadOwnersOptions = new SelectList(spaceUsers, "ProfileId", "Name");
                model.StatusOptions = new SelectList(statuses, "Key", "Value");

                if (leads.Any())
                {
                    model.Leads = leads.Select(x =>
                                    new LeadView
                                    {
                                        Company = x.Company,
                                        LeadOwners = x.LeadOwners,
                                        ExpectedValue = x.ExpectedValue,
                                        Status = x.Status,
                                        NextFollowUp = x.NextFollowUp,
                                        PodioItemID = x.PodioItemID
                                    }).ToList();
                }
                    
            }
            catch (PodioException ex)
            {
                ViewBag.error = ex.Error.ErrorDescription;
            }
            
            return View(model);        
        }

        [HttpPost]
        public ActionResult Filter(LeadListingViewModel leadListingModel)
        {
            var lead = new Lead();
            var model = new LeadListingViewModel();

            try
            {
                DateTime? nextFollowUpFromDate = leadListingModel.NextFollowUpFrom;
                DateTime? nextFollowUpToDate = leadListingModel.NextFollowUpTo;
                decimal? expectedValueFrom = leadListingModel.ExpectedValueFrom;
                decimal? expectedValueTo = leadListingModel.ExpectedValueTo;
                int? status = leadListingModel.Status;
                int? leadOwner = leadListingModel.LeadOwner;


                List<Contact> spaceUsers = lead.GetUsers();
                Dictionary<int, string> statuses = lead.GetAllStatuses();
                IEnumerable<Lead> leads = lead.GetAllLeads(nextFollowUpFromDate, nextFollowUpToDate, expectedValueFrom, expectedValueTo, status, leadOwner);

                model.LeadOwnersOptions = new SelectList(spaceUsers, "ProfileId", "Name", leadOwner);
                model.StatusOptions = new SelectList(statuses, "Key", "Value", status);

                if (leads.Any())
                {
                    model.Leads = leads.Select(x =>
                                    new LeadView
                                    {
                                        Company = x.Company,
                                        LeadOwners = x.LeadOwners,
                                        ExpectedValue = x.ExpectedValue,
                                        Status = x.Status,
                                        NextFollowUp = x.NextFollowUp,
                                        PodioItemID = x.PodioItemID
                                    }).ToList();
                }
                    
            }
            catch (PodioException ex)
            {
                ViewBag.error = ex.Error.ErrorDescription;
            }
           
            return View("Index", model);
        }

        public ActionResult Create()
        {
            var lead = new Lead();
            var model = new LeadViewModel();

            try
            {
                List<Contact> spaceContacts = lead.GetSpaceContacts();
                List<Contact> spaceUsers = lead.GetUsers();
                Dictionary<int, string> statuses = lead.GetAllStatuses();

                model.LeadContactsOptions = new SelectList(spaceContacts, "ProfileId", "Name");
                model.LeadOwnersOptions = new SelectList(spaceUsers, "ProfileId", "Name");
                model.StatusOptions = new SelectList(statuses, "Key", "Value");
            }
            catch (PodioException ex)
            {
                ViewBag.error = ex.Error.ErrorDescription;
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(LeadViewModel leadViewModel)
        {
            Lead lead = LeadViewModelToLead(leadViewModel);

            try
            {
                int itemId = lead.CreateLead(lead);

                if(itemId != default(int))
                    TempData["message"] = "New lead added succesfully";
            }
            catch (PodioException ex)
            {
                ViewBag.error = ex.Error.ErrorDescription;
            }

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            ViewBag.message = TempData["message"];
            ViewBag.error = TempData["error"];

            var lead = new Lead();
            var model = new LeadViewModel();

            try
            {
                Lead leadToEdit = lead.GetLead(id);

                List<Contact> spaceContacts = lead.GetSpaceContacts();
                List<Contact> spaceUsers = lead.GetUsers();
                Dictionary<int,string> statuses = lead.GetAllStatuses();

                IEnumerable<int> selectedLeadContacts = leadToEdit.Contacts != null ? leadToEdit.Contacts.Select(x => x.Key) : null;
                IEnumerable<int> selectedLeadOwners = leadToEdit.LeadOwners != null ? leadToEdit.LeadOwners.Select(x => x.Key) : null;
                int selectedStatus = leadToEdit.Status.Item1;

                model.PodioItemID = leadToEdit.PodioItemID;
                model.Company = leadToEdit.Company;
                model.ExpectedValue = leadToEdit.ExpectedValue;
                model.ProbabilityOfSale = leadToEdit.ProbabilityOfSale;
                model.NextFollowUp = leadToEdit.NextFollowUp;
                model.StreetAddress = leadToEdit.StreetAddress;
                model.City = leadToEdit.City;
                model.Zip = leadToEdit.Zip;
                model.State = leadToEdit.State;
                model.Country = leadToEdit.Country;

                model.LeadContactsOptions = new MultiSelectList(spaceContacts, "ProfileId", "Name", selectedLeadContacts);
                model.LeadOwnersOptions = new MultiSelectList(spaceUsers, "ProfileId", "Name", selectedLeadOwners);
                model.StatusOptions = new SelectList(statuses, "Key", "Value", selectedStatus);

            }
            catch (PodioException ex)
            {
                ViewBag.error = ex.Error.ErrorDescription;
            }
            
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(LeadViewModel leadViewModel, string Command)
        {
            if(Command == "Update")
            {
                Lead lead = LeadViewModelToLead(leadViewModel);
                try
                {
                    lead.UpdateLead(lead);
                    TempData["message"] = "Lead updated successfully";
                }
                catch (PodioException ex)
                {
                    ViewBag.error = ex.Error.ErrorDescription;
                }

                return RedirectToAction("Edit", new { id = lead.PodioItemID });
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(int id)
        {
            var lead = new Lead();
            try
            {
                lead.DeleteLead(id);
                TempData["message"] = "Lead deleted successfully";
            }
            catch (PodioException ex)
            {
                TempData["error"] = ex.Error.ErrorDescription;
            }

            return RedirectToAction("Index");
        }
        
        /// <summary>
        /// Convert LeadViewModel to Lead object
        /// </summary>
        /// <param name="leadViewModel"></param>
        /// <returns></returns>
        public Lead LeadViewModelToLead(LeadViewModel leadViewModel)
        {
            var lead = new Lead();

            lead.PodioItemID = leadViewModel.PodioItemID;
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

            return lead;
        }

        public class LoadClient : ActionFilterAttribute, IActionFilter
        {
            void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
            {
                var podioConnection = new PodioConnection();
                if (podioConnection.IsAuthenticated) 
                {
                    ((LeadsController)filterContext.Controller).PodioClient = podioConnection.GetClient();
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
