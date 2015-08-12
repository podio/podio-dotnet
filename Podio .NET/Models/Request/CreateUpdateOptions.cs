using System.Collections.Generic;

namespace PodioAPI.Models.Request
{
    public class CreateUpdateOptions
    {
        /// <summary>
        ///     If set to true, no stream events and notifications will be generated. Default value: false
        /// </summary>
        public bool Silent { get; set; }

        /// <summary>
        ///     True if hooks should be executed for the change, false otherwise. Default value: true
        /// </summary>
        public bool Hook { get; set; }

        public List<string> Fields { get; set; }

        /// <summary>
        /// True if any mentioned user should be automatically invited to the workspace if the user does not have access to the object 
        /// and access cannot be granted to the object.
        /// Default value: false
        /// </summary>
        public bool AlertInvite { get; set; }

        public CreateUpdateOptions(bool silent = false, bool hook = true, List<string> fields = null, bool alertInvite = false)
        {
            Silent = silent;
            Hook = hook;
            Fields = fields;
            AlertInvite = alertInvite;
        }
    }
}