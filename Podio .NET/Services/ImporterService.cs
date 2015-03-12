using System.Collections.Generic;
using PodioAPI.Models;
using PodioAPI.Models.Request;

namespace PodioAPI.Services
{
    public class ImporterService
    {
        private readonly Podio _podio;

        public ImporterService(Podio currentInstance)
        {
            _podio = currentInstance;
        }

        /// <summary>
        ///     Returns the import information about the given file.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/importer/get-info-5929504 </para>
        /// </summary>
        /// <param name="fileId"></param>
        /// <returns></returns>
        public ImporterInfo GetInfo(int fileId)
        {
            string url = string.Format("/importer/{0}/info", fileId);
            return _podio.Get<ImporterInfo>(url);
        }

        /// <summary>
        ///     Imports the file into the given app. The mapping value for a field depends on the type of field. See the area for
        ///     datails.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/importer/import-app-items-212899 </para>
        /// </summary>
        /// <param name="fileId"></param>
        /// <param name="appId">The id of the app the values should be imported into</param>
        /// <param name="mappings">The mappings between fields and columns</param>
        /// <param name="tagsColumnId">The id of the column to read tags from, if any</param>
        /// <param name="appItemIdColumnId">The id of the column to read the app item id from, if any</param>
        /// <returns></returns>
        public int ImportAppItems(int fileId, int appId, List<ImportMappingField> mappings, string tagsColumnId = null,
            string appItemIdColumnId = null)
        {
            string url = string.Format("/importer/{0}/item/app/{1}", fileId, appId);
            dynamic requestData = new
            {
                app_id = appId,
                mappings = mappings,
                tags_column_id = tagsColumnId,
                app_item_id_column_id = appItemIdColumnId
            };
            dynamic response = _podio.Post<dynamic>(url, requestData);
            return (int) response["batch_id"];
        }

        /// <summary>
        ///     Imports the given file into the space contacts on the given space.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/importer/import-space-contacts-4261072 </para>
        /// </summary>
        /// <param name="fileId"></param>
        /// <param name="spaceId"></param>
        /// <param name="mappings">The mappings of the fields on the contact</param>
        /// <returns></returns>
        public int ImportSpaceContacts(int fileId, int spaceId, List<ImportMappingContactField> mappings)
        {
            string url = string.Format("/importer/{0}/contact/space/{1}", fileId, spaceId);
            dynamic requestData = new
            {
                mappings = mappings
            };
            dynamic response = _podio.Post<dynamic>(url, requestData);
            return (int) response["batch_id"];
        }

        /// <summary>
        ///     Imports the file into the given app. The mapping value for a field depends on the type of field
        ///     <para>Podio API Reference: https://developers.podio.com/doc/importer/import-app-items-212899 </para>
        /// </summary>
        /// <param name="fileId"></param>
        /// <param name="row"></param>
        /// <param name="mappings"></param>
        /// <returns></returns>
        public RowPreview GetPreview(int fileId, string row, List<ImportMappingField> mappings)
        {
            string url = string.Format("/importer/{0}/preview/{1}", fileId, row);
            dynamic requestData = new
            {
                mappings = mappings
            };
            return _podio.Post<RowPreview>(url, requestData);
        }
    }
}