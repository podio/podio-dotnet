using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PodioAPI;
using PodioAPI.Models.Request;
using PodioAPI.Exceptions;

namespace PodioAPIExample
{
    public partial class Authorization : System.Web.UI.Page
    {
        Podio podio = PodioConnection.GetCilent();
        protected void Page_Load(object sender, EventArgs e)
        {
            //If podio authentication endpoint return authorization code, then do authentication with authorization code
            if (!string.IsNullOrEmpty(Request["code"]))
            {
                podio.AuthenicateWithAuthorizationCode(Request["code"], HttpContext.Current.Request.Url.ToString());
                Response.Redirect("Sample.aspx");
            }

        }

        /// <summary>
        /// Server side authenication flow.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnStartAuthorization_Click(object sender, EventArgs e)
        {
            var redirctUtl = podio.GetAuthorizeUrl(HttpContext.Current.Request.Url.ToString());
            //Redirect to podio authentication endpoint, with same page as redirect_url
            Response.Redirect(redirctUtl);
        }

        /// <summary>
        /// App authentication flow
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnConnetAsApp_Click(object sender, EventArgs e)
        {
            if(Page.IsValid)
            {
                try
                {
                    var auth = podio.AuthenicateWithApp(int.Parse(txtAppId.Text), txtAppToken.Text);
                    if(podio.IsAuthenticated())
                        Response.Redirect("/Sample.aspx");
                }
                catch (PodioException podioException)
                {
                    var error = podioException.Error.Error;
                    var errorDescripton = podioException.Error.ErrorDescription;
                    lblErrror.Text = error;
                    lblErrorDescription.Text = errorDescripton;
                }
            }
        }

        /// <summary>
        /// Username & password flow
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnConnectWithUsername_Click(object sender, EventArgs e)
        {
            if(Page.IsValid)
            {
                try
                {
                    var auth = podio.AuthenicateWithPassword(txtUsername.Text, txtPassword.Text);
                    if (podio.IsAuthenticated())
                        Response.Redirect("/Sample.aspx",false);        
                }
                catch (PodioException podioException)
                {
                    var error = podioException.Error.Error;
                    var errorDescripton = podioException.Error.ErrorDescription;
                    lblErrror.Text = error;
                    lblErrorDescription.Text = errorDescripton;
                }      
            }
        }
    }
}