using System.Collections.Generic;
using System.Dynamic;
using PodioAPI.Models;

namespace PodioAPI.Services
{
    public class StatusService
    {
        private readonly Podio _podio;

        public StatusService(Podio currentInstance)
        {
            _podio = currentInstance;
        }

        /// <summary>
        ///     Retrieves a status message by its id.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/status/get-status-message-22337 </para>
        /// </summary>
        /// <param name="statusId"></param>
        /// <returns></returns>
        public Status GetStatusMessage(int statusId)
        {
            string url = string.Format("/status/{0}", statusId);
            return _podio.Get<Status>(url);
        }

        /// <summary>
        ///     Creates a new status message for a user on a specific space.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/status/add-new-status-message-22336 </para>
        /// </summary>
        /// <param name="spaceId"></param>
        /// <param name="text">The actual status message</param>
        /// <param name="fileIds">Temporary files that have been uploaded and should be attached to this item</param>
        /// <param name="embedId">
        ///     The id of an embedded link that has been created with the Add an mebed operation in the Embed
        ///     area
        /// </param>
        /// <param name="embedUrl">The url to be attached</param>
        /// <param name="questionText">The text of the question if any</param>
        /// <param name="questionOptions">The list of answer options as strings</param>
        /// <returns></returns>
        public Status AddNewStatusMessage(int spaceId, string text, List<int> fileIds = null, int? embedId = null,
            string embedUrl = null, string questionText = null, List<string> questionOptions = null)
        {
            string url = string.Format("/status/space/{0}/", spaceId);

            dynamic requestData = new ExpandoObject();
            requestData.value = text;
            requestData.file_ids = fileIds;
            requestData.embed_id = embedId;
            requestData.embedUrl = embedUrl;
            if (!string.IsNullOrEmpty(questionText) && questionOptions != null)
            {
                requestData.question = new
                {
                    text = questionText,
                    options = questionOptions
                };
            }

            return _podio.Post<Status>(url, requestData);
        }

        /// <summary>
        ///     This will update an existing status message.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/status/update-a-status-message-22338 </para>
        /// </summary>
        /// <param name="statusId"></param>
        /// <param name="text">The actual status message</param>
        /// <param name="fileIds">Temporary files that have been uploaded and should be attached to this item</param>
        /// <param name="embedId">
        ///     The id of an embedded link that has been created with the Add an mebed operation in the Embed
        ///     area
        /// </param>
        /// <param name="embedUrl">The url to be attached</param>
        public void UpdateStatusMessage(int statusId, string text, List<int> fileIds = null, int? embedId = null,
            string embedUrl = null)
        {
            string url = string.Format("/status/{0}", statusId);
            dynamic requestData = new
            {
                value = text,
                file_ids = fileIds,
                embed_id = embedId,
                embed_url = embedUrl
            };
            _podio.Put<dynamic>(url, requestData);
        }

        /// <summary>
        ///     This is used to delete a status message
        ///     <para>Podio API Reference: https://developers.podio.com/doc/status/delete-a-status-message-22339 </para>
        /// </summary>
        /// <param name="statusId"></param>
        public void DeleteStatusMessage(int statusId)
        {
            string url = string.Format("/status/{0}", statusId);
            _podio.Delete<dynamic>(url);
        }
    }
}