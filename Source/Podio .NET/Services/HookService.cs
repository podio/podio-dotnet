using System.Collections.Generic;
using PodioAPI.Models;

namespace PodioAPI.Services
{
    public class HookService
    {
        private readonly Podio _podio;

        public HookService(Podio currentInstance)
        {
            _podio = currentInstance;
        }

        /// <summary>
        ///     Create a new hook on the given object. See the area for details.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/hooks/create-hook-215056 </para>
        /// </summary>
        /// <param name="refType"></param>
        /// <param name="refId"></param>
        /// <param name="externalURL">The url of endpoint.</param>
        /// <param name="type">The type of events to listen to, see the area for options.</param>
        /// <returns></returns>
        public int CreateHook(string refType, int refId, string externalURL, string type)
        {
            string url = string.Format("/hook/{0}/{1}/", refType, refId);
            dynamic requestData = new
            {
                url = externalURL,
                type = type
            };
            dynamic response = _podio.Post<dynamic>(url, requestData);
            return (int) response["hook_id"];
        }

        /// <summary>
        ///     Deletes the hook with the given id.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/hooks/delete-hook-215291 </para>
        /// </summary>
        /// <param name="hookId"></param>
        public void DeleteHook(int hookId)
        {
            string url = string.Format("/hook/{0}", hookId);
            _podio.Delete<dynamic>(url);
        }

        /// <summary>
        ///     Returns the hooks on the object.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/hooks/get-hooks-215285 </para>
        /// </summary>
        /// <param name="refType"></param>
        /// <param name="refId"></param>
        /// <returns></returns>
        public List<Hook> GetHooks(string refType, int refId)
        {
            string url = string.Format("/hook/{0}/{1}/", refType, refId);
            return _podio.Get<List<Hook>>(url);
        }

        /// <summary>
        ///     Request the hook to be validated. This will cause the hook to send a request to the URL with the parameter "type"
        ///     set to "hook.verify" and "code" set to the verification code. The endpoint must then call the validate method with
        ///     the code to complete the verification.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/hooks/request-hook-verification-215232 </para>
        /// </summary>
        /// <param name="hookId"></param>
        public void Verify(int hookId)
        {
            string url = string.Format("/hook/{0}/verify/request", hookId);
            _podio.Post<dynamic>(url);
        }

        /// <summary>
        ///     Validates the hook using the code received from the verify call. On successful validation the hook will become
        ///     active.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/hooks/validate-hook-verification-215241 </para>
        /// </summary>
        /// <param name="hookId"></param>
        public void ValidateHookVerification(int hookId, string code)
        {
            string url = string.Format("/hook/{0}/verify/validate", hookId);
            dynamic requestData = new
            {
                code = code
            };
            _podio.Post<dynamic>(url, requestData);
        }
    }
}