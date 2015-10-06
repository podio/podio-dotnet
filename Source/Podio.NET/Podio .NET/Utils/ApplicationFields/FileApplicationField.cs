using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using PodioAPI.Models;

namespace PodioAPI.Utils.ApplicationFields
{
    public class FileApplicationField : ApplicationField
    {
        private IEnumerable<string> _allowedMimetypes;

        /// <summary>
        ///     A list of allowed mimetypes on the form "image/png" or "image/*"
        /// </summary>
        public IEnumerable<string> AllowedMimetypes
        {
            get
            {
                if (_allowedMimetypes == null)
                {
                    _allowedMimetypes = this.GetSettingsAs<string>("allowed_mimetypes");
                }
                return _allowedMimetypes;
            }
            set
            {
                InitializeFieldSettings();
                this.InternalConfig.Settings["allowed_mimetypes"] = value != null ? JToken.FromObject(value) : null;
            }
        }
    }
}