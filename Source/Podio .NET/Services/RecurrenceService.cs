using PodioAPI.Models;

namespace PodioAPI.Services
{
    public class RecurrenceService
    {
        private readonly Podio _podio;

        public RecurrenceService(Podio currentInstance)
        {
            _podio = currentInstance;
        }

        /// <summary>
        ///     Returns the recurrence for the given object
        ///     <para>Podio API Reference: https://developers.podio.com/doc/recurrence/get-recurrence-3415545 </para>
        /// </summary>
        /// <param name="refType"></param>
        /// <param name="refId"></param>
        /// <returns></returns>
        public Recurrence GetRecurrence(string refType, int refId)
        {
            string url = string.Format("/recurrence/{0}/{1}", refType, refId);
            return _podio.Get<Recurrence>(url);
        }

        /// <summary>
        ///     Deletes the recurrence.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/recurrence/delete-recurrence-3349970 </para>
        /// </summary>
        /// <param name="refType"></param>
        /// <param name="refId"></param>
        public void DeleteRecurrence(string refType, int refId)
        {
            string url = string.Format("/recurrence/{0}/{1}", refType, refId);
            _podio.Delete<dynamic>(url);
        }

        /// <summary>
        ///     Creates or updates recurrence on the object.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/recurrence/create-or-update-recurrence-3349957 </para>
        /// </summary>
        /// <param name="refType"></param>
        /// <param name="refId"></param>
        /// <param name="recurrence"></param>
        public void UpdateRecurrence(string refType, int refId, Recurrence recurrence)
        {
            string url = string.Format("/recurrence/{0}/{1}", refType, refId);
            _podio.Put<dynamic>(url, recurrence);
        }
    }
}