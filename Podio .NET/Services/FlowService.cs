using PodioAPI.Models;
using System.Collections.Generic;

namespace PodioAPI.Services
{
    public class FlowService
    {
        private Podio _podio;
        public FlowService(Podio currentInstance)
        {
            _podio = currentInstance;
        }

        /// <summary>
        /// Returns the flow with the given id.
        /// <para>Podio API Reference: https://developers.podio.com/doc/flows/get-flow-by-id-26312313 </para>
        /// </summary>
        /// <param name="flowId"></param>
        /// <returns></returns>
        public Flow GetFlowById(int flowId)
        {
            string url = string.Format("/flow/{0}", flowId);
            return _podio.Get<Flow>(url);
        }

        /// <summary>
        /// Get all the flows on the given ref.
        /// <para>Podio API Reference: https://developers.podio.com/doc/flows/get-flows-26312304 </para>
        /// </summary>
        /// <param name="refType"></param>
        /// <param name="refId"></param>
        /// <returns></returns>
        public List<Flow> GetFlows(string refType, int refId)
        {
            string url = string.Format("/flow/{0}/{1}/", refType, refId);
            return _podio.Get <List<Flow>>(url);
        }

        /// <summary>
        /// Delete the flow with the given id.
        /// <para>Podio API Reference: https://developers.podio.com/doc/flows/delete-flow-32929229 </para>
        /// </summary>
        /// <param name="flowId"></param>
        public void DeleteFlow(int flowId)
        {
            string url = string.Format("/flow/{0}", flowId);
            _podio.Delete<dynamic>(url);
        }

        /// <summary>
        /// Returns the possible attributes on the given flow.
        /// <para>Podio API Reference: https://developers.podio.com/doc/flows/get-flow-context-26313659 </para>
        /// </summary>
        /// <param name="flowId"></param>
        /// <returns></returns>
        public Dictionary<string, FlowAttribute> GetFlowContext(int flowId)
        {
            string url = string.Format("/flow/{0}/context/", flowId);
            return _podio.Get<Dictionary<string, FlowAttribute>>(url);
        }
    }
}
