using PodioAPI.Models.Request;
using PodioAPI.Models;
using System.Collections.Generic;

namespace PodioAPI.Services
{
    public class CommentService
    {
        private Podio _podio;
        public CommentService(Podio currentInstance)
        {
            _podio = currentInstance;
        }

        /// <summary>
        /// Deletes a comment made by a user. This can be used to retract a comment that was made and which the user regrets.
        /// <para>Podio API Reference: https://developers.podio.com/doc/comments/delete-a-comment-22347  </para>
        /// </summary>
        /// <param name="commentId"></param>
        public void DeleteComment(int commentId)
        {
            var url = string.Format("/comment/{0}", commentId);
            _podio.Delete<dynamic>(url);
        }

        /// <summary>
        /// Returns the contents of a comment. It is not possible to see where the comment was made, only the comment itself.
        /// <para>Podio API Reference: https://developers.podio.com/doc/comments/get-a-comment-22345 </para>
        /// </summary>
        /// <param name="commentId"></param>
        /// <returns></returns>
        public Comment GetComment(int commentId)
        {
            var url = string.Format("/comment/{0}",commentId);
            return _podio.Get<Comment>(url);
        }

        /// <summary>
        /// Used to retrieve all the comments that have been made on an object of the given type and with the given id. It returns a list of all the comments sorted in ascending order by time created.
        /// <para>Podio API Reference: https://developers.podio.com/doc/comments/get-comments-on-object-22371 </para>
        /// </summary>
        /// <param name="type"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Comment> GetCommentsOnObject(string type, int id)
        {
            var url = string.Format("/comment/{0}/{1}/", type, id);
            return _podio.Get<List<Comment>>(url);
        }
    }
}
