using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PodioAspNetSample.ViewModels;
using PodioAspNetSample.Models;
using PodioAPI.Exceptions;
using PodioAPI;

namespace PodioAspNetSample.Controllers
{
    public class AuthorizationController : Controller
    {
        public Podio PodioClient { get; set; }

        public string RedirectUrl { get; set; }

        public AuthorizationController()
        {
            var podioConnection = new PodioConnection();
            PodioClient = podioConnection.GetClient();
            RedirectUrl = podioConnection.RedirectUrl;
        }

        public ActionResult Index()
        {
            ViewBag.hideLogoutButton = true;
            return View();
        }

        /// <summary>
        /// This is an authentication flow using username and password. Do not use this for authenticating users other than yourself.
        /// For more details see : https://developers.podio.com/authentication/username_password
        /// </summary>
        [HttpPost]
        public ActionResult UsernamePasswordAuthentication(UsernamePasswordAuthenticationViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    PodioClient.AuthenicateWithPassword(model.Username, model.Password);
                    return RedirectToAction("Index", "Leads");
                }
                catch (PodioException ex)
                {
                    TempData["podioError"] = ex.Error.ErrorDescription;
                }    
            }

            return View("Index");
        }

        /// <summary>
        /// When you have authorized our application in Podio - you can use the code that podio returns. 
        /// </summary>
        public ActionResult HandleAuthorizationResponse(string code, string error_reason, string error, string error_description)
        {
            //If error is empty, that means the authrization is succesfull and the authrization code returns
            if (string.IsNullOrEmpty(error) && !string.IsNullOrEmpty(code))
            {
                PodioClient.AuthenicateWithAuthorizationCode(code, RedirectUrl);
                return RedirectToAction("Index", "Leads");
            }
            else
            {
                TempData["podioError"] = error;
            }
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Start server side flow
        ///  For more details see : https://developers.podio.com/authentication/server_side
        /// </summary>
        public void ServerSideAuthentication()
        {
            Response.Redirect(PodioClient.GetAuthorizeUrl(RedirectUrl));
        }

        [HttpPost]
        public ActionResult Logout()
        {
            ViewBag.hideLogoutButton = true;

            //Clear Auth token from auth store
            PodioClient.ClearAuth();

            return View("Index");
        }
    }
}
