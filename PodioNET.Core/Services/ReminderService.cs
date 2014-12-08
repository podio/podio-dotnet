﻿
namespace PodioAPI.Services
{
    public class ReminderService
    {
        private Podio _podio;
        public ReminderService(Podio currentInstance)
        {
            _podio = currentInstance;
        }

        /// <summary>
        /// Returns the reminder for the given object.
        /// <para>Podio API Reference: https://developers.podio.com/doc/reminders/get-reminder-3415569 </para>
        /// </summary>
        /// <param name="refType"></param>
        /// <param name="refId"></param>
        /// <returns></returns>
        public int GetReminder(string refType, int refId)
        {
            string url = string.Format("/reminder/{0}/{1}", refType, refId);
            dynamic response = _podio.Get<dynamic>(url);
            return (int)response["remind_delta"];
        }

        /// <summary>
        /// Deletes the reminder, if any, on the given object.
        /// <para>Podio API Reference: https://developers.podio.com/doc/reminders/delete-reminder-3315117 </para>
        /// </summary>
        /// <param name="refType"></param>
        /// <param name="refId"></param>
        /// <param name="reminderId"></param>
        public void DeleteReminder(string refType, int refId, int reminderId)
        {
            string url = string.Format("/reminder/{0}/{1}?reminder_id={2}", refType, refId, reminderId);
            _podio.Delete<dynamic>(url);
        }

        /// <summary>
        /// Snoozes the reminder for 10 minutes.
        /// <para>Podio API Reference: https://developers.podio.com/doc/reminders/snooze-reminder-3321049 </para>
        /// </summary>
        /// <param name="refType"></param>
        /// <param name="refId"></param>
        /// <param name="reminderId"></param>
        public void SnoozeReminder(string refType, int refId, int reminderId)
        {
            string url = string.Format("/reminder/{0}/{1}/snooze?reminder_id={2}", refType, refId, reminderId);
            _podio.Post<dynamic>(url);
        }

        /// <summary>
        /// Creates or updates the reminder on a object. Possible ref_types are: task.
        /// <para>Podio API Reference: https://developers.podio.com/doc/reminders/create-or-update-reminder-3315055 </para>
        /// </summary>
        /// <param name="refType"></param>
        /// <param name="refId"></param>
        /// <param name="remindDelta">minutes to remind before the due date of the object</param>
        public void Update(string refType, int refId, int remindDelta)
        {
            string url = string.Format("/reminder/{0}/{1}", refType, refId);
            dynamic requestData = new
            {
                remind_delta = remindDelta
            };
            _podio.Put<dynamic>(url, requestData);
        }
    }
}
