using System.Collections.Generic;
using PodioAPI.Models;

namespace PodioAPI.Services
{
    public class BatchService
    {
        private readonly Podio _podio;

        public BatchService(Podio currentInstance)
        {
            _podio = currentInstance;
        }

        /// <summary>
        ///     Returns the batch with the given id.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/batch/get-batch-6144225 </para>
        /// </summary>
        /// <param name="batchId"></param>
        /// <returns></returns>
        public Batch GetBatch(int batchId)
        {
            string url = string.Format("/batch/{0}", batchId);
            return _podio.Get<Batch>(url);
        }

        /// <summary>
        ///     Returns the batches created by the user. The batches are sorted descending by date of creation.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/batch/get-batches-6078877 </para>
        /// </summary>
        /// <returns></returns>
        public List<Batch> GetBatches()
        {
            string url = "/batch/";
            return _podio.Get<List<Batch>>(url);
        }

        /// <summary>
        ///     Returns the currently running batches on the given reference.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/batch/get-running-batches-15856178 </para>
        /// </summary>
        /// <param name="refType"> The reference can either be a space or an app.</param>
        /// <param name="refId"></param>
        /// <param name="plugin">The plugin can either be "app_import", "app_export", "space_contact_import" or "app_content".</param>
        /// <returns></returns>
        public List<Batch> GetRunningBatches(string refType, int refId, string plugin)
        {
            string url = string.Format("/batch/{0}/{1}/{2}/running/", refType, refId, plugin);
            return _podio.Get<List<Batch>>(url);
        }
    }
}