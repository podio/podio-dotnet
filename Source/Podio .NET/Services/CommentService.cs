using System.Collections.Generic;
using PodioAPI.Models;
using PodioAPI.Models.Request;
using System.Threading.Tasks;
using PodioAPI.Utils;

namespace PodioAPI.Services
{
    public class CommentService
    {
        private readonly Podio _podio;

        public CommentService(Podio currentInstance)
        {
            _podio = currentInstance;
        }

        /// <summary>
        ///     Deletes a comment made by a user. This can be used to retract a comment that was made and which the user regrets.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/comments/delete-a-comment-22347  </para>
        /// </summary>
        /// <param name="commentId"></param>
        public async Task<dynamic> DeleteComment(int commentId)
        {
            string url = string.Format("/comment/{0}", commentId);
           return await  _podio.Delete<dynamic>(url);
        }

        /// <summary>
        ///     Returns the contents of a comment. It is not possible to see where the comment was made, only the comment itself.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/comments/get-a-comment-22345 </para>
        /// </summary>
        /// <param name="commentId"></param>
        /// <returns></returns>
        public async Task<Comment> GetComment(int commentId)
        {
            string url = string.Format("/comment/{0}", commentId);
            return await _podio.Get<Comment>(url);
        }

        /// <summary>
        ///     Used to retrieve all the comments that have been made on an object of the given type and with the given id. It
        ///     returns a list of all the comments sorted in ascending order by time created.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/comments/get-comments-on-object-22371 </para>
        /// </summary>
        /// <param name="type"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<Comment>> GetCommentsOnObject(string type, int id)
        {
            string url = string.Format("/comment/{0}/{1}/", type, id);
            return await _podio.Get<List<Comment>>(url);
        }

        /// <summary>
        ///     Adds a new comment to the object of the given type and id with no reference to other objects
        ///     <para>Podio API Reference: https://developers.podio.com/doc/comments/add-comment-to-object-22340 </para>
        /// </summary>
        /// <param name="type"></param>
        /// <param name="id"></param>
        /// <param name="text">The comment to be made</param>
        /// <param name="externalId">The external id of the comment</param>
        /// <param name="fieldIds">Temporary files that have been uploaded and should be attached to this comment</param>
        /// <param name="embedUrl">The url to be attached</param>
        /// <param name="embedId">
        ///     The id of an embedded link that has been created with the Add an embed operation in the Embed
        ///     area
        /// </param>
        /// <param name="alertInvite">
        ///     True if any mentioned user should be automatically invited to the workspace if the user does
        ///     not have access to the object and access cannot be granted to the object. Default value: false
        /// </param>
        /// <param name="silent">
        ///     If set to true, the object will not be bumped up in the stream and notifications will not be
        ///     generated. Default value: false
        /// </param>
        /// <returns></returns>
        public async Task<int> AddCommentToObject(string type, int id, string text, string externalId = null,
            List<int> fieldIds = null, string embedUrl = null, int? embedId = null, bool alertInvite = false,
            bool silent = false, bool hook = true)
        {
            CommentCreateUpdateRequest comment = new CommentCreateUpdateRequest()
            {
                Value = text,
                ExternalId = externalId,
                FieldIds = fieldIds,
                EmbedUrl = embedUrl,
                EmbedId = embedId
            };
            return await AddCommentToObject(type, id, comment, alertInvite, silent, hook);
        }

        /// <summary>
        ///     Adds a new comment to the object of the given type and id, f.ex. item 1.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/comments/add-comment-to-object-22340 </para>
        /// </summary>
        /// <param name="type"></param>
        /// <param name="id"></param>
        /// <param name="comment"></param>
        /// <param name="alertInvite">
        ///     True if any mentioned user should be automatically invited to the workspace if the user does
        ///     not have access to the object and access cannot be granted to the object. Default value: false
        /// </param>
        /// <param name="silent">
        ///     If set to true, the object will not be bumped up in the stream and notifications will not be
        ///     generated. Default value: false
        /// </param>
        /// <returns></returns>
        public async Task<int> AddCommentToObject(string type, int id, CommentCreateUpdateRequest comment, bool alertInvite = false,
            bool silent = false, bool hook = true)
        {
            string url = string.Format("/comment/{0}/{1}/", type, id);
            url =  Utility.PrepareUrlWithOptions(url, new CreateUpdateOptions(silent, hook, null, alertInvite));
            dynamic response = await _podio.Post<dynamic>(url, comment);
            return (int) response["comment_id"];
        }

        /// <summary>
        ///     Updates an already created comment. This should only be used to correct spelling and grammatical mistakes in the
        ///     comment.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/comments/update-a-comment-22346 </para>
        /// </summary>
        /// <param name="commentId"></param>
        /// <param name="text">The comment to be made</param>
        /// <param name="externalId">The external id of the comment</param>
        /// <param name="fieldIds">Temporary files that have been uploaded and should be attached to this comment</param>
        /// <param name="embedUrl">The url to be attached</param>
        /// <param name="embedId">
        ///     The id of an embedded link that has been created with the Add an embed operation in the Embed
        ///     area
        /// </param>
        public async Task<dynamic>  UpdateComment(int commentId, string text, string externalId = null, List<int> fieldIds = null,
            string embedUrl = null, int? embedId = null)
        {
            var requestData = new CommentCreateUpdateRequest()
            {
                Value = text,
                ExternalId = externalId,
                FieldIds = fieldIds,
                EmbedUrl = embedUrl,
                EmbedId = embedId
            };
           return await UpdateComment(commentId, requestData);
        }

        /// <summary>
        ///     Updates an already created comment. This should only be used to correct spelling and grammatical mistakes in the
        ///     comment.
        /// </summary>
        /// <param name="commentId"></param>
        /// <param name="comment"></param>
        public async Task<dynamic> UpdateComment(int commentId, CommentCreateUpdateRequest comment)
        {
            string url = string.Format("/comment/{0}", commentId);
             return await  _podio.Put<dynamic>(url, comment);
        }
    }
}