using System;
using System.Collections.Generic;
using PodioAPI.Models;
using PodioAPI.Utils;

namespace PodioAPI.Services
{
    public class NotificationService
    {
        private readonly Podio _podio;

        public NotificationService(Podio currentInstance)
        {
            _podio = currentInstance;
        }

        /// <summary>
        ///     Returns the number of unread notifications for the active user.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/notifications/get-inbox-new-count-84610 </para>
        /// </summary>
        /// <returns></returns>
        public int GetInboxNewCount()
        {
            string url = "/notification/inbox/new/count";
            dynamic response = _podio.Get<dynamic>(url);
            return (int) response["new"];
        }

        /// <summary>
        ///     Star the given notification to move it to the star list.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/notifications/star-notification-295910 </para>
        /// </summary>
        /// <param name="notificationId"></param>
        public void StarNotification(int notificationId)
        {
            string url = string.Format("/notification/{0}/star", notificationId);
            _podio.Post<dynamic>(url);
        }

        /// <summary>
        ///     Removes the star on the notification.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/notifications/un-star-notification-295911 </para>
        /// </summary>
        /// <param name="notificationId"></param>
        public void UnStarNotification(int notificationId)
        {
            string url = string.Format("/notification/{0}/star", notificationId);
            _podio.Delete<dynamic>(url);
        }

        /// <summary>
        ///     Marks all notifications on the given object as viewed.
        ///     <para>
        ///         Podio API Reference:
        ///         https://developers.podio.com/doc/notifications/mark-notifications-as-viewed-by-ref-553653
        ///     </para>
        /// </summary>
        /// <param name="refType"></param>
        /// <param name="refId"></param>
        public void MarkNotificationsAsViewedByRef(string refType, int refId)
        {
            string url = string.Format("/notification/{0}/{1}/viewed", refType, refId);
            _podio.Post<dynamic>(url);
        }

        /// <summary>
        ///     Mark the notification as viewed. This will move the notification from the inbox to the viewed archive.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/notifications/mark-notification-as-viewed-22436 </para>
        /// </summary>
        /// <param name="notificationId"></param>
        public void MarkNotificationAsViewed(int notificationId)
        {
            string url = string.Format("/notification/{0}/viewed", notificationId);
            _podio.Post<dynamic>(url);
        }

        /// <summary>
        ///     Marks all the users notifications as viewed.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/notifications/mark-all-notifications-as-viewed-58099 </para>
        /// </summary>
        public void MarkAllNotificationsAsViewed()
        {
            string url = "/notification/viewed";
            _podio.Post<dynamic>(url);
        }

        /// <summary>
        ///     Retrieves a single notification grouped similarly to the get notifications operation.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/notifications/get-notification-2973737 </para>
        /// </summary>
        /// <param name="notificationId"></param>
        /// <returns></returns>
        public Notifications GetNotification(int notificationId)
        {
            string url = string.Format("/notification/{0}", notificationId);
            return _podio.Get<Notifications>(url);
        }

        /// <summary>
        ///     Returns a list of notifications based on the query parameters. The notifications will be grouped based on their
        ///     context.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/notifications/get-notifications-290777 </para>
        /// </summary>
        /// <param name="contextType">The type of the context to get notifications for, f.ex. "conversation", "item" or "task"</param>
        /// <param name="createdFrom">The earliest date and time to return notifications from</param>
        /// <param name="createdTo">The latest date and time to return notifications from</param>
        /// <param name="direction">
        ///     "incoming" to get incoming notifications, "outgoing" to get outgoing notifications. Default
        ///     value: incoming
        /// </param>
        /// <param name="limit">The maximum number of notifications to return, maxium is "100". Default value: 20</param>
        /// <param name="offSet">The offet into the returned notifications. Default value: 0</param>
        /// <param name="starred">
        ///     False to get only unstarred notifications, true to get only starred notifications, leave blank
        ///     for both
        /// </param>
        /// <param name="type">A type of notification, see the area for possible types</param>
        /// <param name="userId">The user id of the other part of the notification</param>
        /// <param name="viewed">
        ///     False to get all unviewed notifications, true to get all viewed notifications, leave blank for
        ///     both
        /// </param>
        /// <param name="viewedFrom">
        ///     When returning only unviewed notifications (above), notifications viewed after the given date
        ///     and time will also be returned. Use this to keep pagination even when some notifications has been viewed after
        ///     initial page load.
        /// </param>
        /// <returns></returns>
        public List<Notifications> GetNotifications(string contextType = null, DateTime? createdFrom = null,
            DateTime? createdTo = null, string direction = "incoming", int limit = 20, int offSet = 0,
            bool? starred = null, string type = null, int? userId = null, bool? viewed = null,
            DateTime? viewedFrom = null)
        {
            string url = "/notification/";
            var requestData = new Dictionary<string, string>
            {
                {"context_type", contextType},
                {"created_from", createdFrom.ToStringOrNull()},
                {"created_to", createdTo.ToStringOrNull()},
                {"direction", direction},
                {"limit", limit.ToString()},
                {"offset", offSet.ToString()},
                {"starred", starred.ToStringOrNull()},
                {"type", type},
                {"user_id", userId.ToStringOrNull()},
                {"viewed", viewed.ToStringOrNull()},
                {"viewed_from", viewedFrom.ToStringOrNull()}
            };
            return _podio.Get<List<Notifications>>(url, requestData);
        }
    }
}