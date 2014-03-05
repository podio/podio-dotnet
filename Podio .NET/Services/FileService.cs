using PodioAPI.Models.Response;
using System.Collections.Generic;

namespace PodioAPI.Services
{
    public class FileService
    {
        private Podio _podio;
        public FileService(Podio currentInstance)
        {
            _podio = currentInstance;
        }

        /// <summary>
        /// Uploads a new file
        /// <para>Podio API Reference: https://developers.podio.com/doc/files/upload-file-1004361 </para>
        /// </summary>
        /// <param name="filePath">Full physical path to the file</param>
        /// <param name="fileName">File Name</param>
        /// <returns></returns>
        public FileAttachment UploadFile(string filePath, string fileName)
        {
            var url = "/file/v2/";
            var attributes = new
            {
                filePath = filePath,
                fileName = fileName
            };
            Dictionary<string, object> options = new Dictionary<string, object>() { { "upload", true } };
            return _podio.Post<FileAttachment>(url, attributes, options);
        }

        /// <summary>
        /// Used to update the description of the file.
        /// <para>Podio API Reference: https://developers.podio.com/doc/files/update-file-22454 </para>
        /// </summary>
        /// <param name="fileId"></param>
        /// <param name="description">The new description of the file</param>
        /// <returns></returns>
        public void UpdateFile(int fileId, string description)
        {
            var url = string.Format("/file/{0}", fileId);
            var attributes = new
            {
                description = description
            };
            _podio.Put<dynamic>(url, attributes);
        }

        /// <summary>
        /// Marks the current file as an replacement for the old file. Only files with type of "attachment" can be replaced.
        /// <para>Podio API Reference: https://developers.podio.com/doc/files/replace-file-22450 </para>
        /// </summary>
        /// <param name="oldFileId">The id of the old file that should be replacd with the new file</param>
        /// <param name="fileId"></param>
        /// <returns></returns>
        public void ReplaceFile(int oldFileId, int fileId)
        {
            var url = string.Format("/file/{0}/replace", fileId);
            var attributes = new
            {
                old_file_id = oldFileId
            };
            _podio.Post<dynamic>(url, attributes);
        }

        /// <summary>
        /// Attaches the uploaded file to the given object.
        /// <para>Podio API Reference: https://developers.podio.com/doc/files/attach-file-22518 </para>
        /// </summary>
        /// <param name="fileId"></param>
        /// <param name="refType">The type of object the file should be attached to. 
        /// <para>Valid objects are "status", "item", "comment", "space", or "task".</para></param>
        /// <param name="refId"></param>
        /// <returns></returns>
        public void AttachFile(int fileId, string refType, int refId)
        {
            var url = string.Format("/file/{0}/attach", fileId);
            var attributes = new
            {
                ref_type = refType,
                ref_id = refId
            };
            _podio.Post<dynamic>(url, attributes);
        }

        /// <summary>
        /// Copies the file, which makes it available for attaching to another object.
        /// <para>Podio API Reference: https://developers.podio.com/doc/files/copy-file-89977 </para>
        /// </summary>
        /// <param name="fileId"></param>
        /// <returns>The id of the newly created file</returns>
        public int CopyFile(int fileId)
        {
            var url = string.Format("/file/{0}/copy", fileId);
            var response = _podio.Post<dynamic>(url);
            return (int)response["file_id"];
        }

        /// <summary>
        /// Deletes the file with the given id
        /// <para>Podio API Reference: https://developers.podio.com/doc/files/delete-file-22453 </para>
        /// </summary>
        /// <param name="fileId"></param>
        public void DeleteFile(int fileId)
        {
            var url = string.Format("/file/{0}", fileId);
            _podio.Delete<dynamic>(url);
        }

        /// <summary>
        /// Returns the file with the given id
        /// <para>Podio API Reference: https://developers.podio.com/doc/files/get-file-22451 </para>
        /// </summary>
        /// <param name="fileId"></param>
        /// <returns></returns>
        public FileAttachment GetFile(int fileId)
        {
            var url = string.Format("/file/{0}", fileId);
            return _podio.Get<FileAttachment>(url);
        }
    }
}
