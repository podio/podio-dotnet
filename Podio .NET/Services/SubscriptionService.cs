using PodioAPI.Models;

namespace PodioAPI.Services
{
    public class SubscriptionService
    {
        private readonly Podio _podio;

        public SubscriptionService(Podio currentInstance)
        {
            _podio = currentInstance;
        }

        /// <summary>
        ///     Returns the subscription with the given id.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/subscriptions/get-subscription-by-id-22446 </para>
        /// </summary>
        /// <param name="subscriptionId"></param>
        /// <returns></returns>
        public Subscription GetSubscriptionById(int subscriptionId)
        {
            string url = string.Format("/subscription/{0}", subscriptionId);
            return _podio.Get<Subscription>(url);
        }

        /// <summary>
        ///     Subscribes the user to the given object. Based on the object type, the user will receive notifications when actions
        ///     are performed on the object. See the area for more details.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/subscriptions/subscribe-22409 </para>
        /// </summary>
        /// <param name="refType"></param>
        /// <param name="refId"></param>
        /// <returns></returns>
        public int Subscribe(string refType, int refId)
        {
            string url = string.Format("/subscription/{0}/{1}", refType, refId);
            dynamic response = _podio.Post<dynamic>(url);
            return (int) response["subscription_id"];
        }

        /// <summary>
        ///     Unsubscribe from getting notifications on actions on the given object.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/subscriptions/unsubscribe-by-reference-22410 </para>
        /// </summary>
        /// <param name="refType"></param>
        /// <param name="refId"></param>
        public void UnsubscribeByReference(string refType, int refId)
        {
            string url = string.Format("/subscription/{0}/{1}", refType, refId);
            _podio.Delete<dynamic>(url);
        }

        /// <summary>
        ///     Get the subscription for the given object.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/subscriptions/get-subscription-by-reference-22408 </para>
        /// </summary>
        /// <param name="refType"></param>
        /// <param name="refId"></param>
        /// <returns></returns>
        public Subscription GetSubscriptionByReference(string refType, int refId)
        {
            string url = string.Format("/subscription/{0}/{1}", refType, refId);
            return _podio.Get<Subscription>(url);
        }

        /// <summary>
        ///     Stops the subscription with the given id
        ///     <para>Podio API Reference: https://developers.podio.com/doc/subscriptions/unsubscribe-by-id-22445 </para>
        /// </summary>
        /// <param name="subscriptionId"></param>
        public void UnsubscribeById(int subscriptionId)
        {
            string url = string.Format("/subscription/{0}", subscriptionId);
            _podio.Delete<dynamic>(url);
        }
    }
}