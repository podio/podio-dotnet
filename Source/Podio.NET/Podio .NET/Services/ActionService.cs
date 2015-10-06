using PodioAPI.Models;
using System.Threading.Tasks;

namespace PodioAPI.Services
{
    public class ActionService
    {
        private readonly Podio _podio;

        public ActionService(Podio currentInstance)
        {
            _podio = currentInstance;
        }

        /// <summary>
        ///     Returns the action with the given id.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/actions/get-action-1701120 </para>
        /// </summary>
        /// <param name="actionId"></param>
        /// <returns></returns>
        public async Task<Action> GetAction(int actionId)
        {
            string url = string.Format("/action/{0}", actionId);
            return await _podio.Get<Action>(url);
        }
    }
}