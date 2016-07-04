using System.Collections.Generic;
using PodioAPI.Models;
using System.Threading.Tasks;

namespace PodioAPI.Services
{
    public class FormService
    {
        private readonly Podio _podio;

        public FormService(Podio currentInstance)
        {
            _podio = currentInstance;
        }

        /// <summary>
        ///     Enables the form with the given id. Only disabled forms can be enabled, which makes it once again possible to
        ///     create items in the app using the form.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/forms/activate-form-1107439 </para>
        /// </summary>
        /// <param name="formId"></param>
        public async Task<dynamic> ActivateForm(int formId)
        {
            string url = string.Format("/form/{0}/activate", formId);
            return await  _podio.Post<dynamic>(url);
        }

        /// <summary>
        ///     Disables the form with given id. This makes it impossible to create new items using the form. Instead, a message
        ///     about the form being disabled is shown.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/forms/deactivate-form-1107378 </para>
        /// </summary>
        /// <param name="formId"></param>
        public async Task<dynamic> DeactivateForm(int formId)
        {
            string url = string.Format("/form/{0}/deactivate", formId);
            return await _podio.Post<dynamic>(url);
        }

        /// <summary>
        ///     Returns the form with the given id.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/forms/get-form-53754 </para>
        /// </summary>
        /// <param name="formId"></param>
        /// <returns></returns>
        public async Task<Form> GetForm(int formId)
        {
            string url = string.Format("/form/{0}", formId);
            return await  _podio.Get<Form>(url);
        }

        /// <summary>
        ///     Returns all the active forms on the given app.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/forms/get-forms-53771 </para>
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        public async Task<List<Form>> GetForms(int appId)
        {
            string url = string.Format("/form/app/{0}/", appId);
            return await  _podio.Get<List<Form>>(url);
        }

        /// <summary>
        ///     Deletes the form with the given id.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/forms/delete-from-53810 </para>
        /// </summary>
        /// <param name="formId"></param>
        public async Task<dynamic> DeleteFrom(int formId)
        {
            string url = string.Format("/form/{0}", formId);
            return await _podio.Delete<dynamic>(url);
        }

        /// <summary>
        ///     Creates a new form on the app.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/forms/create-form-53803 </para>
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="fromSettings">The settings of the form.</param>
        /// <param name="domains">The list of domains where the form can be used.</param>
        /// <param name="fields">The id and settings for each field.</param>
        /// <param name="attachments">True if attachments are allowed, false otherwise.</param>
        /// <returns></returns>
        public async Task<int> CreateForm(int appId, FormSettings fromSettings, string[] domains, List<FormField> fields,
            bool attachments)
        {
            string url = string.Format("/form/app/{0}/", appId);
            var requestData = new
            {
                settings = fromSettings,
                domains = domains,
                fields = fields,
                attachments = attachments
            };
            dynamic response = await  _podio.Post<dynamic>(url, requestData);
            return (int) response["form_id"];
        }

        /// <summary>
        ///     Updates the form with new settings, domains, fields, etc.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/forms/update-form-53808 </para>
        /// </summary>
        /// <param name="formId"></param>
        /// <param name="fromSettings">The settings of the form.</param>
        /// <param name="domains">The list of domains where the form can be used.</param>
        /// <param name="fields">The id and settings for each field.</param>
        /// <param name="attachments">True if attachments are allowed, false otherwise.</param>
        public async Task<dynamic> UpdateForm(int formId, FormSettings fromSettings, string[] domains, List<FormField> fields,
            bool attachments)
        {
            string url = string.Format("/form/{0}", formId);
            var requestData = new
            {
                settings = fromSettings,
                domains = domains,
                fields = fields,
                attachments = attachments
            };
            return await  _podio.Put<dynamic>(url, requestData);
        }
    }
}