using PodioAPI.Models;
using System;
using System.Collections.Generic;
using PodioAPI.Utils;

namespace PodioAPI.Services
{
    public class NotificationService
    {
        private Podio _podio;
        public NotificationService(Podio currentInstance)
        {
            _podio = currentInstance;
        }

        /// <summary>
        /// Returns the number of unread notifications for the active user.
        /// <para>Podio API Reference: https://developers.podio.com/doc/notifications/get-inbox-new-count-84610 </para>
        /// </summary>
        /// <returns></returns>
        public int GetInboxNewCount()
        {
            string url = "/notification/inbox/new/count";
            dynamic response = _podio.Get<dynamic>(url);
            return (int)response["new"];
        }

        /// <summary>
        /// Star the given notification to move it to the star list.
        /// <para>Podio API Reference: https://developers.podio.com/doc/notifications/star-notification-295910 </para>
        /// </summary>
        /// <param name="notificationId"></param>
        public void StarNotification(int notificationId)
        {
            string url = string.Format("/notification/{0}/star", notificationId);
            _podio.Post<dynamic>(url);
        }

        /// <summary>
        /// Removes the star on the notification.
        /// <para>Podio API Reference: https://developers.podio.com/doc/notifications/un-star-notification-295911 </para>
        /// </summary>
        /// <param name="notificationId"></param>
        public void UnStarNotification(int notificationId)
        {
            string url = string.Format("/notification/{0}/star", notificationId);
            _podio.Delete<dynamic>(url);
        }

        /// <summary>
        /// Marks all notifications on the given object as viewed.
        /// <para>Podio API Reference: https://developers.podio.com/doc/notifications/mark-notifications-as-viewed-by-ref-553653 </para>
        /// </summary>
        /// <param name="refType"></param>
        /// <param name="refId"></param>
        public void MarkNotificationsAsViewedByRef(string refType, int refId)
        {
            string url = string.Format("/notification/{0}/{1}/viewed", refType, refId);
            _podio.Post<dynamic>(url);
        }

        /// <summary>
        /// Mark the notification as viewed. This will move the notification from the inbox to the viewed archive.
        /// <para>Podio API Reference: https://developers.podio.com/doc/notifications/mark-notification-as-viewed-22436 </para>
        /// </summary>
        /// <param name="notificationId"></param>
        public void MarkNotificationAsViewed(int notificationId)
        {
            string url = string.Format("/notification/{0}/viewed",notificationId);
            _podio.Post<dynamic>(url);
        }

        /// <summary>
        /// Marks all the users notifications as viewed.
        /// <para>Podio API Reference: https://developers.podio.com/doc/notifications/mark-all-notifications-as-viewed-58099 </para>
        /// </summary>
        public void MarkAllNotificationsAsViewed()
        {
            string url = "/notification/viewed";
            _podio.Post<dynamic>(url);
        }
    }
}
