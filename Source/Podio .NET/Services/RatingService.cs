using System.Collections.Generic;
using PodioAPI.Models;

namespace PodioAPI.Services
{
    public class RatingService
    {
        private readonly Podio _podio;

        public RatingService(Podio currentInstance)
        {
            _podio = currentInstance;
        }

        /// <summary>
        ///     Add a new rating of the user to the object. The rating can be one of many different types. For more details see the
        ///     area.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/ratings/add-rating-22377 </para>
        /// </summary>
        /// <param name="refType"></param>
        /// <param name="refId"></param>
        /// <param name="ratingType"></param>
        /// <param name="value">The value of the rating, see the area for information on the value to use.</param>
        /// <returns>The id of the rating created.</returns>
        public int AddRating(string refType, int refId, string ratingType, int value)
        {
            string url = string.Format("/rating/{0}/{1}/{2}", refType, refId, ratingType);
            dynamic requestData = new
            {
                value = value
            };
            dynamic response = _podio.Post<dynamic>(url, requestData);
            return (int) response["rating_id"];
        }

        /// <summary>
        ///     Get the number of users who liked the given object.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/ratings/get-like-count-32161225 </para>
        /// </summary>
        /// <param name="refType"></param>
        /// <param name="refId"></param>
        /// <returns></returns>
        public int GetLikeCount(string refType, int refId)
        {
            string url = string.Format("/rating/{0}/{1}/like_count", refType, refId);
            dynamic response = _podio.Get<dynamic>(url);
            return (int) response["like_count"];
        }

        /// <summary>
        ///     Returns the rating value for the given rating type, object and user.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/ratings/get-rating-22407 </para>
        /// </summary>
        /// <param name="refType"></param>
        /// <param name="refId"></param>
        /// <param name="ratingType"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int GetRating(string refType, int refId, string ratingType, int userId)
        {
            string url = string.Format("/rating/{0}/{1}/{2}/{3}", refType, refId, ratingType, userId);
            dynamic response = _podio.Get<dynamic>(url);
            return (int) response["value"];
        }

        /// <summary>
        ///     Returns the active users rating value for the given rating type and object.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/ratings/get-rating-own-84128 </para>
        /// </summary>
        /// <param name="refType"></param>
        /// <param name="refId"></param>
        /// <param name="ratingType"></param>
        /// <returns></returns>
        public int GetRatingOwn(string refType, int refId, string ratingType)
        {
            string url = string.Format("/rating/{0}/{1}/{2}/self", refType, refId, ratingType);
            dynamic response = _podio.Get<dynamic>(url);
            return (int) response["value"];
        }

        /// <summary>
        ///     Get a list of profiles of everyone that liked the given object.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/ratings/get-who-liked-an-object-29007011 </para>
        /// </summary>
        /// <param name="refType"></param>
        /// <param name="refId"></param>
        /// <param name="limit">How many profiles that liked something to return. Default value: 10</param>
        /// <returns></returns>
        public List<Contact> GetWhoLikedAnObject(string refType, int refId, int limit = 10)
        {
            string url = string.Format("/rating/{0}/{1}/liked_by/", refType, refId);
            Dictionary<string, string> requestData = new Dictionary<string, string>
            {
                {"limit", limit.ToString()}
            };
            return _podio.Get<List<Contact>>(url, requestData);
        }

        /// <summary>
        ///     Removes a previous rating of the given type by the user of the specified object.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/ratings/remove-rating-22342 </para>
        /// </summary>
        /// <param name="refType"></param>
        /// <param name="refId"></param>
        /// <param name="ratingType"></param>
        public void RemoveRating(string refType, int refId, string ratingType)
        {
            string url = string.Format("/rating/{0}/{1}/{2}", refType, refId, ratingType);
            _podio.Delete<dynamic>(url);
        }

        /// <summary>
        ///     Returns all the ratings for the given object. It will only return the ratings that are enabled for the object.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/ratings/get-all-ratings-22376 </para>
        /// </summary>
        /// <param name="refType"></param>
        /// <param name="refId"></param>
        /// <returns></returns>
        public Rating GetAllRatings(string refType, int refId)
        {
            string url = string.Format("/rating/{0}/{1}", refType, refId);
            return _podio.Get<Rating>(url);
        }

        /// <summary>
        ///     Get the rating average (for fivestar) and totals for the given rating type on the specified object.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/ratings/get-ratings-22375 </para>
        /// </summary>
        /// <param name="refType"></param>
        /// <param name="refId"></param>
        /// <param name="ratingType"></param>
        /// <returns></returns>
        public RatingType GetRatings(string refType, int refId, string ratingType)
        {
            string url = string.Format("/rating/{0}/{1}/{2}", refType, refId, ratingType);
            return _podio.Get<RatingType>(url);
        }
    }
}