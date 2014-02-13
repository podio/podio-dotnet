using System.Collections.Generic;

namespace PodioAPI.Models.Request
{
    public class CreateUpdateOptions
    {
        /// <summary>
        /// If set to true, no stream events and notifications will be generated. Default value: false
        /// </summary>
        public bool Silent { get; set; }
        /// <summary>
        /// True if hooks should be executed for the change, false otherwise. Default value: true
        /// </summary>
        public bool Hook { get; set; }
        /// <summary>
        /// TODO: Fire the hook ?
        /// </summary>
        public List<string> Fields { get; set; }

        public CreateUpdateOptions(bool silent = false, bool hook = true, List<string> fields = null)
        {
            Silent = silent;
            Hook = hook;
            Fields = fields;
        }
    }
}
