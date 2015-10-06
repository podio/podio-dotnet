using System.Collections.Generic;
using PodioAPI.Models;
using System.Threading.Tasks;

namespace PodioAPI.Services
{
    public class FlowService
    {
        private readonly Podio _podio;

        public FlowService(Podio currentInstance)
        {
            _podio = currentInstance;
        }

        /// <summary>
        ///     Returns the flow with the given id.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/flows/get-flow-by-id-26312313 </para>
        /// </summary>
        /// <param name="flowId"></param>
        /// <returns></returns>
        public async Task<Flow> GetFlowById(int flowId)
        {
            string url = string.Format("/flow/{0}", flowId);
            return await _podio.Get<Flow>(url);
        }

        /// <summary>
        ///     Get all the flows on the given ref.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/flows/get-flows-26312304 </para>
        /// </summary>
        /// <param name="refType"></param>
        /// <param name="refId"></param>
        /// <returns></returns>
        public async Task<List<Flow>> GetFlows(string refType, int refId)
        {
            string url = string.Format("/flow/{0}/{1}/", refType, refId);
            return await _podio.Get<List<Flow>>(url);
        }

        /// <summary>
        ///     Delete the flow with the given id.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/flows/delete-flow-32929229 </para>
        /// </summary>
        /// <param name="flowId"></param>
        public async Task<dynamic> DeleteFlow(int flowId)
        {
            string url = string.Format("/flow/{0}", flowId);
            return await _podio.Delete<dynamic>(url);
        }

        /// <summary>
        ///     Returns the possible attributes on the given flow.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/flows/get-flow-context-26313659 </para>
        /// </summary>
        /// <param name="flowId"></param>
        /// <returns></returns>
        public async Task<Dictionary<string, FlowAttribute>> GetFlowContext(int flowId)
        {
            string url = string.Format("/flow/{0}/context/", flowId);
            return await _podio.Get<Dictionary<string, FlowAttribute>>(url);
        }

        /// <summary>
        ///     Creates a new flow on the given reference. Only valid reference is "app".
        ///     <para>Podio API Reference: https://developers.podio.com/doc/flows/add-new-flow-26309928 </para>
        /// </summary>
        /// <param name="refType"></param>
        /// <param name="refId"></param>
        /// <param name="name">The name of the flow</param>
        /// <param name="type">The type of the flow, currently only supports "item.create" and "item.update"</param>
        /// <param name="effects">The list of effects to add</param>
        /// <param name="config">The configuration for the cause of the flow</param>
        public async Task<int> AddNewFlow(string refType, int refId, string name, string type, List<Effect> effects,
            dynamic config = null)
        {
            string url = string.Format("/flow/{0}/{1}/", refType, refId);
            dynamic requestData = new
            {
                name = name,
                type = type,
                config = config,
                effects = effects
            };
            dynamic response = await _podio.Post<dynamic>(url, requestData);
            return (int) response["flow_id"];
        }

        /// <summary>
        ///     Updates the flow. The type cannot be changed.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/flows/update-flow-26310901 </para>
        /// </summary>
        /// <param name="flowId"></param>
        /// <param name="name"> The new name of the flow</param>
        /// <param name="effects">The list of effects to add</param>
        /// <param name="config">The configuration for the cause of the flow</param>
        public async Task<dynamic> UpdateFlow(int flowId, string name, List<Effect> effects = null, dynamic config = null)
        {
            string url = string.Format("/flow/{0}", flowId);
            dynamic requestData = new
            {
                name = name,
                config = config,
                effects = effects
            };
            return await _podio.Put<dynamic>(url, requestData);
        }

        /// <summary>
        ///     Get's the possible attributes to use as variables for a given effect attribute.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/flows/get-possible-attributes-33060379 </para>
        /// </summary>
        /// <param name="refType"></param>
        /// <param name="refId"></param>
        /// <param name="cause">Details about the cause</param>
        /// <param name="effect">Details about the effect</param>
        /// <returns></returns>
        public async Task<List<FlowAttribute>> GetPossibleAttributes(string refType, int refId, Cause cause, dynamic effect)
        {
            string url = string.Format("/flow/{0}/{1}/attributes/", refType, refId);
            dynamic requestData = new
            {
                cause = cause,
                effect = effect
            };
            return await _podio.Post<List<FlowAttribute>>(url, requestData);
        }
    }
}