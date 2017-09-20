using System.Collections.Generic;
using PodioAPI.Models;
using System.Threading.Tasks;
using System.IO;
using PodioAPI.Utils;

namespace PodioAPI.Services
{
    public class FileService
    {
        private readonly Podio _podio;

        public FileService(Podio currentInstance)
        {
            _podio = currentInstance;
        }

        /// <summary>
        ///     Returns the file with the given id
        ///     <para>Podio API Reference: https://developers.podio.com/doc/files/get-file-22451 </para>
        /// </summary>
        /// <param name="fileId"></param>
        /// <returns></returns>
        public async Task<FileAttachment> GetFile(int fileId)
        {
            string url = string.Format("/file/{0}", fileId);
            return await _podio.Get<FileAttachment>(url);
        }

        /// <summary>
        ///     Uploads a new file
        ///     <para>Podio API Reference: https://developers.podio.com/doc/files/upload-file-1004361 </para>
        /// </summary>
        /// <param name="filePath">Full physical path to the file</param>
        /// <param name="fileName">File Name with extension</param>
        /// <returns></returns>
        public async Task<FileAttachment> UploadFile(string filePath, string fileName)
        {
            if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
            {
                byte[] data = await FileUtils.ReadAllBytesAsync(filePath);
                string mimeType = MimeTypeMapping.GetMimeType(Path.GetExtension(filePath));

                return await UploadFile(fileName, data, mimeType);
            }
            else
            {
                throw new FileNotFoundException("File not found in the specified path");
            }
        }

        /// <summary>
        ///     Uploads a new file
        ///     <para>Podio API Reference: https://developers.podio.com/doc/files/upload-file-1004361 </para>
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="data"></param>
        /// <param name="mimeType"></param>
        /// <returns></returns>
        public async Task<FileAttachment> UploadFile(string fileName, byte[] data, string mimeType)
        {
            string url = "/file/v2/";
            dynamic requestData = new
            {
                fileName,
                data,
                mimeType
            };
            return await _podio.PostMultipartFormData<FileAttachment>(url, data, fileName, mimeType);
        }

        /// <summary>
        ///     Upload a new file from URL
        /// </summary>
        /// <param name="fileUrl"></param>
        /// <returns></returns>
        public async Task<FileAttachment> UploadFileFromUrl(string fileUrl)
        {
            string url = "/file/from_url/";
            dynamic requestData = new
            {
                url = fileUrl
            };

            return await _podio.Post<FileAttachment>(url, requestData);
        }

        /// <summary>
        ///     Used to update the description of the file.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/files/update-file-22454 </para>
        /// </summary>
        /// <param name="fileId"></param>
        /// <param name="description">The new description of the file</param>
        /// <returns></returns>
        public async System.Threading.Tasks.Task UpdateFile(int fileId, string description)
        {
            string url = string.Format("/file/{0}", fileId);
            dynamic requestData = new
            {
                description = description
            };
            await _podio.Put<dynamic>(url, requestData);
        }

        /// <summary>
        ///     Deletes the file with the given id
        ///     <para>Podio API Reference: https://developers.podio.com/doc/files/delete-file-22453 </para>
        /// </summary>
        /// <param name="fileId"></param>
        public async System.Threading.Tasks.Task DeleteFile(int fileId)
        {
            string url = string.Format("/file/{0}", fileId);
            await _podio.Delete<dynamic>(url);
        }

        /// <summary>
        ///     Marks the current file as an replacement for the old file. Only files with type of "attachment" can be replaced.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/files/replace-file-22450 </para>
        /// </summary>
        /// <param name="oldFileId">The id of the old file that should be replacd with the new file</param>
        /// <param name="fileId"></param>
        /// <returns></returns>
        public async System.Threading.Tasks.Task ReplaceFile(int oldFileId, int fileId)
        {
            string url = string.Format("/file/{0}/replace", fileId);
            dynamic requestData = new
            {
                old_file_id = oldFileId
            };
            await _podio.Post<dynamic>(url, requestData);
        }

        /// <summary>
        ///     Attaches the uploaded file to the given object.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/files/attach-file-22518 </para>
        /// </summary>
        /// <param name="fileId"></param>
        /// <param name="refType">
        ///     The type of object the file should be attached to.
        ///     <para>Valid objects are "status", "item", "comment", "space", or "task".</para>
        /// </param>
        /// <param name="refId"></param>
        /// <returns></returns>
        public async System.Threading.Tasks.Task AttachFile(int fileId, string refType, int refId)
        {
            string url = string.Format("/file/{0}/attach", fileId);
            dynamic requestData = new
            {
                ref_type = refType,
                ref_id = refId
            };
            await _podio.Post<dynamic>(url, requestData);
        }

        /// <summary>
        ///     Copies the file, which makes it available for attaching to another object.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/files/copy-file-89977 </para>
        /// </summary>
        /// <param name="fileId"></param>
        /// <returns>The id of the newly created file</returns>
        public async Task<int> CopyFile(int fileId)
        {
            string url = string.Format("/file/{0}/copy", fileId);
            dynamic response = await _podio.Post<dynamic>(url);
            return (int)response["file_id"];
        }

        /// <summary>
        ///     Returns a list of all files matching the given filters and sorted by the specified attribute.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/files/get-files-4497983 </para>
        /// </summary>
        /// <param name="attachedTo">
        ///     The type of the entity the file is attached to. Can be one of {"item", "status", "task" and
        ///     "space"}
        /// </param>
        /// <param name="createdBy">
        ///     The entities that created the file. See auth objects on the view area in Podio API
        ///     documentation for details.
        /// </param>
        /// <param name="createdOn">
        ///     The from and to date the file was created between. For valid operations see date filtering
        ///     under the view area in Podio API documentation.
        /// </param>
        /// <param name="filetype">The type of the file. Can be one of {"image", "application", "video", "text", "audio"}.</param>
        /// <param name="hostedBy">
        ///     Which provider actually hosts the files. Currently can be one of {"podio", "google", "boxnet",
        ///     "dropbox", "evernote", "live", "sharefile", "sugarsync", "yousendit"}.
        /// </param>
        /// <param name="limit">The maximum number of files to return Default value: 20 </param>
        /// <param name="sortBy">How the files should be sorted. Can be one of {"name", "created_on"} Default value: name</param>
        /// <param name="sortDesc">true for to sort in descending order, false in ascending Default value: false</param>
        /// <returns></returns>
        public async Task<List<FileAttachment>> GetFiles(string attachedTo = null, string createdBy = null, string createdOn = null,
            string filetype = null, string hostedBy = null, int limit = 20, string sortBy = null, bool sortDesc = false)
        {
            string url = "/file/";
            var requestData = new Dictionary<string, string>()
            {
                {"attached_to", attachedTo},
                {"created_by", createdBy},
                {"created_on", createdOn},
                {"filetype", filetype},
                {"hosted_by", hostedBy},
                {"limit", limit.ToString()},
                {"sort_by", sortBy},
                {"sort_desc", sortDesc.ToString()}
            };
            return await _podio.Get<List<FileAttachment>>(url, requestData);
        }

        /// <summary>
        ///     Returns all the files related to the items in the application. This includes files both on the item itself and in
        ///     comments on the item.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/files/get-files-on-app-22472 </para>
        /// </summary>
        /// <param name="appId">App Id</param>
        /// <param name="attachedTo">
        ///     The type of the entity the file is attached to. Can be one of {"item", "status", "task" and
        ///     "space"}
        /// </param>
        /// <param name="createdBy">
        ///     The entities that created the file. See auth objects on the view area in Podio API
        ///     documentation for details.
        /// </param>
        /// <param name="createdOn">
        ///     The from and to date the file was created between. For valid operations see date filtering
        ///     under the view area in Podio API documentation.
        /// </param>
        /// <param name="filetype">The type of the file. Can be one of {"image", "application", "video", "text", "audio"}.</param>
        /// <param name="hostedBy">
        ///     Which provider actually hosts the files. Currently can be one of {"podio", "google", "boxnet",
        ///     "dropbox", "evernote", "live", "sharefile", "sugarsync", "yousendit"}.
        /// </param>
        /// <param name="limit">The maximum number of files to return Default value: 20 </param>
        /// <param name="offset">The offset to use when returning files to be used for pagination. Default value: 0</param>
        /// <param name="sortBy">How the files should be sorted. Can be one of {"name", "created_on"} Default value: name</param>
        /// <param name="sortDesc">true for to sort in descending order, false in ascending Default value: false</param>
        /// <returns></returns>
        public async Task<List<FileAttachment>> GetFilesOnApp(int appId, string attachedTo = null, string createdBy = null,
            string createdOn = null, string filetype = null, string hostedBy = null, int limit = 20, int offset = 0,
            string sortBy = null, bool sortDesc = false)
        {
            string url = string.Format("/file/app/{0}/", appId);
            var requestData = new Dictionary<string, string>()
            {
                {"attached_to", attachedTo},
                {"created_by", createdBy},
                {"created_on", createdOn},
                {"filetype", filetype},
                {"hosted_by", hostedBy},
                {"limit", limit.ToString()},
                {"offset", offset.ToString()},
                {"sort_by", sortBy},
                {"sort_desc", sortDesc.ToString()}
            };
            return await _podio.Get<List<FileAttachment>>(url, requestData);
        }

        /// <summary>
        ///     Returns all the files on the space order by the file name.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/files/get-files-on-space-22471 </para>
        /// </summary>
        /// <param name="spaceId"></param>
        /// <param name="attachedTo">
        ///     The type of the entity the file is attached to. Can be one of {"item", "status", "task" and
        ///     "space"}
        /// </param>
        /// <param name="createdBy">
        ///     The entities that created the file. See auth objects on the view area in Podio API
        ///     documentation for details.
        /// </param>
        /// <param name="createdOn">
        ///     The from and to date the file was created between. For valid operations see date filtering
        ///     under the view area in Podio API documentation.
        /// </param>
        /// <param name="filetype">The type of the file. Can be one of {"image", "application", "video", "text", "audio"}.</param>
        /// <param name="hostedBy">
        ///     Which provider actually hosts the files. Currently can be one of {"podio", "google", "boxnet",
        ///     "dropbox", "evernote", "live", "sharefile", "sugarsync", "yousendit"}.
        /// </param>
        /// <param name="limit">The maximum number of files to return Default value: 20 </param>
        /// <param name="offset">The offset to use when returning files to be used for pagination. Default value: 0</param>
        /// <param name="sortBy">How the files should be sorted. Can be one of {"name", "created_on"} Default value: name</param>
        /// <param name="sortDesc">true for to sort in descending order, false in ascending Default value: false</param>
        /// <returns></returns>
        public async Task<List<FileAttachment>> GetFilesOnSpace(int spaceId, string attachedTo = null, string createdBy = null,
            string createdOn = null, string filetype = null, string hostedBy = null, int limit = 20, int offset = 0,
            string sortBy = null, bool sortDesc = false)
        {
            string url = string.Format("/file/space/{0}/", spaceId);
            var requestData = new Dictionary<string, string>()
            {
                {"attached_to", attachedTo},
                {"created_by", createdBy},
                {"created_on", createdOn},
                {"filetype", filetype},
                {"hosted_by", hostedBy},
                {"limit", limit.ToString()},
                {"offset", offset.ToString()},
                {"sort_by", sortBy},
                {"sort_desc", sortDesc.ToString()}
            };
            return await _podio.Get<List<FileAttachment>>(url, requestData);
        }

        /// <summary>
        ///     To directly download the file of a FileAttachment
        ///     <para>For more information see https://developers.podio.com/examples/files </para>
        /// </summary>
        /// <param name="fileAttachment"></param>
        /// <returns></returns>
        public async Task<FileResponse> DownloadFile(FileAttachment fileAttachment)
        {
            var fileLink = fileAttachment.Link;
            var options = new Dictionary<string, object>()
            {
                {"file_download", true}
            };
            return await _podio.Get<FileResponse>(fileLink, new Dictionary<string, string>(), true);
        }

        public async Task<FileAttachment> UploadLinkedAccountFile(int linkedAccountId, string externalFileId, bool preservePermissions = true)
        {
            var url = $"/file/linked_account/{linkedAccountId}/";
            var request = new
            {
                external_file_id = externalFileId
            };
            return await _podio.Post<FileAttachment>(url, request);
        }
    }
}