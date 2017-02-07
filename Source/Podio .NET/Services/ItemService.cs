using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using PodioAPI.Models;
using PodioAPI.Models.Request;
using PodioAPI.Utils;

namespace PodioAPI.Services
{
    public class ItemService
    {
        private readonly Podio _podio;

        public ItemService(Podio currentInstance)
        {
            _podio = currentInstance;
        }

        /// <summary>
        ///     Adds a new item to the given app.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/items/add-new-item-22362 </para>
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="item"></param>
        /// <param name="spaceId"> For use in Podio platform</param>
        /// <param name="silent">
        ///     If set to true, the object will not be bumped up in the stream and notifications will not be
        ///     generated
        /// </param>
        /// <param name="hook">If set to false, hooks will not be executed for the change</param>
        /// <returns>Id of the created item</returns>
        public int AddNewItem(int appId, Item item, int? spaceId = null, bool silent = false, bool hook = true)
        {
            JArray fieldValues = JArray.FromObject(item.Fields.Select(f => new {external_id = f.ExternalId, field_id = f.FieldId, values = f.Values}));

            var requestData = new ItemCreateUpdateRequest
            {
                ExternalId = item.ExternalId,
                Fields = fieldValues,
                FileIds = item.FileIds,
                Tags = item.Tags,
                Recurrence = item.Recurrence,
                LinkedAccountId = item.LinkedAccountId,
                Reminder = item.Reminder,
                Ref = item.Ref,
                SpaceId = spaceId
            };

            string url = string.Format("/item/app/{0}/", appId);
            url = Podio.PrepareUrlWithOptions(url, new CreateUpdateOptions(silent, hook));
            dynamic response = _podio.Post<Item>(url, requestData);
            return response.ItemId;
        }

        /// <summary>
        ///     Update an already existing item.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/items/update-item-22363 </para>
        /// </summary>
        /// <param name="item"></param>
        /// <param name="spaceId"> For use in Podio platform</param>
        /// <param name="revision">The revision of the item that is being updated. This is optional</param>
        /// <param name="silent">
        ///     If set to true, the object will not be bumped up in the stream and notifications will not be
        ///     generated
        /// </param>
        /// <param name="hook">If set to false, hooks will not be executed for the change</param>
        /// <returns>The id of the new revision / null if no change</returns>
        public int? UpdateItem(Item item, int? spaceId = null, int? revision = null, bool silent = false, bool hook = true)
        {
            JArray fieldValues =
                JArray.FromObject(
                    item.Fields.Select(f => new {external_id = f.ExternalId, field_id = f.FieldId, values = f.Values}));

            var requestData = new ItemCreateUpdateRequest
            {
                ExternalId = item.ExternalId,
                Revision = revision,
                Fields = fieldValues,
                FileIds = item.FileIds,
                Tags = item.Tags,
                Recurrence = item.Recurrence,
                LinkedAccountId = item.LinkedAccountId,
                Reminder = item.Reminder,
                Ref = item.Ref,
                SpaceId = spaceId
            };

            string url = string.Format("/item/{0}", item.ItemId);
            url = Podio.PrepareUrlWithOptions(url, new CreateUpdateOptions(silent, hook));
            dynamic response = _podio.Put<dynamic>(url, requestData);
            if (response != null)
                return (int) response["revision"];
            else
                return null;
        }


        /// <summary>
        ///     Update the item values for a specific field.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/items/update-item-field-values-22367 </para>
        /// </summary>
        /// <param name="item"></param>
        /// <param name="silent">
        ///     If set to true, the object will not be bumped up in the stream and notifications will not be
        ///     generated
        /// </param>
        /// <param name="hook">If set to false, hooks will not be executed for the change</param>
        /// <returns>The id of the new revision / null if no change</returns>
        public int? UpdateItemFieldValues(Item item, bool silent = false, bool hook = true)
        {
            /*
                Example Usage: Updating a text field               
                Item item = new Item();
                item.ItemId = 123456;
                var textfield = item.Field<TextItemField>("title");
                textfield.Value = "My new title";
                var newRevisionId = ItemService.UpdateItemFieldValues(item); 
            */

            string fieldIdentifier = "";
            ItemField fieldToUpdate = null;
            JToken updatedValue = null;
            if (item.Fields.Any())
            {
                fieldToUpdate = item.Fields.First();
                updatedValue = fieldToUpdate.Values != null ? fieldToUpdate.Values.First() : null;
                if (fieldToUpdate.ExternalId != null)
                    fieldIdentifier = fieldToUpdate.ExternalId;
                else if (fieldToUpdate.FieldId != null)
                    fieldIdentifier = fieldToUpdate.FieldId.ToString();
            }
            string url = string.Format("/item/{0}/value/{1}", item.ItemId, fieldIdentifier);
            url = Podio.PrepareUrlWithOptions(url, new CreateUpdateOptions(silent, hook));
            dynamic response = _podio.Put<dynamic>(url, updatedValue);

            if (response != null)
                return (int) response["revision"];
            else
                return null;
        }

        /// <summary>
        ///     Updates all the values for an item.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/items/update-item-values-22366 </para>
        /// </summary>
        /// <param name="item">Item with fields to be updated</param>
        /// <param name="silent">
        ///     If set to true, the object will not be bumped up in the stream and notifications will not be
        ///     generated
        /// </param>
        /// <param name="hook">If set to false, hooks will not be executed for the change</param>
        /// <returns>The id of the new revision / null if no change</returns>
        public int? UpdateItemValues(Item item, bool silent = false, bool hook = true)
        {
            /*
                Example Usage: Updating a text field and a date field
                Item item = new Item();
                item.ItemId = 123456;
                var textfield = item.Field<TextItemField>("title");
                textfield.Value = "My new title";
                var dateField = item.Field<DateItemField>("deadline-date");
                dateField.Start = DateTime.Now;
                dateField.End = DateTime.Now.AddMonths(2);
                var newRevisionId = ItemService.UpdateItemValues(item); 
           */

            JArray fieldValues =
                JArray.FromObject(
                    item.Fields.Select(f => new {external_id = f.ExternalId, field_id = f.FieldId, values = f.Values}));
            string url = string.Format("/item/{0}/value", item.ItemId);
            url = Podio.PrepareUrlWithOptions(url, new CreateUpdateOptions(silent, hook));
            dynamic response = _podio.Put<dynamic>(url, fieldValues);
            if (response != null)
                return (int) response["revision"];
            else
                return null;
        }

        /// <summary>
        ///     Returns the item with the specified id.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/items/get-item-22360 </para>
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="markedAsViewed">
        ///     If true marks any new notifications on the given item as viewed, otherwise leaves any
        ///     notifications untouched, Default value true
        /// </param>
        public Item GetItem(int itemId, bool markedAsViewed = true)
        {
            string markAsViewdValue = markedAsViewed == true ? "1" : "0";
            var requestData = new Dictionary<string, string>
            {
                {"mark_as_viewed", markAsViewdValue}
            };
            string url = string.Format("/item/{0}", itemId);
            return _podio.Get<Item>(url, requestData);
        }

        /// <summary>
        ///     Gets the basic details about the given item. Similar to the full get item method, but only returns data for the
        ///     item itself.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/items/get-item-basic-61768 </para>
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="markedAsViewed">
        ///     If true marks any new notifications on the given item as viewed, otherwise leaves any
        ///     notifications untouched, Default value true
        /// </param>
        /// <returns></returns>
        public Item GetItemBasic(int itemId, bool markedAsViewed = true)
        {
            string viewedVal = markedAsViewed == true ? "1" : "0";
            var requestData = new Dictionary<string, string>
            {
                {"mark_as_viewed", viewedVal}
            };
            string url = string.Format("/item/{0}/basic", itemId);
            return _podio.Get<Item>(url, requestData);
        }

        /// <summary>
        ///     Returns the full item by its app_item_id, which is a unique ID for items per app.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/items/get-item-by-app-item-id-66506688 </para>
        /// </summary>
        /// <param name="appId">todo: describe appId parameter on GetItemByAppItemId</param>
        /// <param name="appItemId">todo: describe appItemId parameter on GetItemByAppItemId</param>
        public Item GetItemByAppItemId(int appId, int appItemId)
        {
            string url = string.Format("/app/{0}/item/{1}", appId, appItemId);
            return _podio.Get<Item>(url);
        }

        /// <summary>
        ///     Retrieve an app item with the given external_id.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/items/get-item-by-external-id-19556702 </para>
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="external_id"></param>
        /// <returns></returns>
        public Item GetItemByExternalId(int appId, string external_id)
        {
            string url = string.Format("/item/app/{0}/external_id/{1}", appId, external_id);
            return _podio.Get<Item>(url);
        }


        /// <summary>
        ///     Filters the items and returns the matching items.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/items/filter-items-4496747 </para>
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="filterOptions"></param>
        /// <param name="includeFiles"></param>
        /// <returns>Collection of matching items</returns>
        public PodioCollection<Item> FilterItems(int appId, FilterOptions filterOptions, bool includeFiles = false)
        {
            filterOptions.Limit = filterOptions.Limit == 0 ? 30 : filterOptions.Limit;
            string url = string.Format("/item/app/{0}/filter/", appId);
            if (includeFiles)
            {
                url = url + "?fields=items.fields(files)";
            }
            return _podio.Post<PodioCollection<Item>>(url, filterOptions);
        }

        /// <summary>
        ///     Filters the items and returns the matching items.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/items/filter-items-4496747 </para>
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="limit">The maximum number of items to return, defaults to 30</param>
        /// <param name="offset">The offset into the returned items, defaults to 0</param>
        /// <param name="filters">The filters to apply</param>
        /// <param name="remember">True if the view should be remembered, false otherwise</param>
        /// <param name="sortBy">The sort order to use</param>
        /// <param name="sortDesc">True to sort descending, false otherwise</param>
        /// <param name="includeFiles">True to include files when getting filtered items, defaults to false</param>
        /// <returns></returns>
        public PodioCollection<Item> FilterItems(int appId, int? limit = 30, int? offset = 0, Object filters = null,
            bool? remember = null, string sortBy = null, bool? sortDesc = null, bool includeFiles = false)
        {
            var filterOptions = new FilterOptions
            {
                Limit = limit,
                Offset = offset,
                Filters = filters,
                Remember = remember,
                SortBy = sortBy,
                SortDesc = sortDesc
            };
            return FilterItems(appId, filterOptions, includeFiles);
        }

        /// <summary>
        ///     Returns the items in the Xlsx format.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/items/get-items-as-xlsx-63233 </para>
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="filters">The key with corresponding value to use for filtering the results</param>
        /// <param name="limit">The maximum number of items to return Default value: 20</param>
        /// <param name="offset">The offset from the start of the items returned</param>
        /// <param name="deletedColumns">Wether to include deleted columns. Default value: false</param>
        /// <param name="sortBy">How the items should be sorted</param>
        /// <param name="sortDesc">Use true to sort descending, use false to sort ascending Default value: true</param>
        /// <param name="viewId">Applies the given view, if set to 0, the last used view will be used</param>
        /// <returns></returns>
        public FileResponse GetItemsAsXlsx(int appId, Dictionary<string, string> filters, int limit = 20, int offset = 0,
            bool deletedColumns = false, string sortBy = null, bool sortDesc = true, int? viewId = null)
        {
            string url = string.Format("/item/app/{0}/xlsx/", appId);
            var requestData = new Dictionary<string, string>();
            var parameters = new Dictionary<string, string>
            {
                {"limit", limit.ToString()},
                {"offset", offset.ToString()},
                {"deleted_columns", deletedColumns.ToString()},
                {"sort_desc", sortDesc.ToString()},
                {"view_id", viewId.HasValue ? viewId.ToString() : null},
                {"sort_by", sortBy}
            };
            var options = new Dictionary<string, bool>
            {
                {"file_download", true}
            };
            if (filters.Any())
                requestData = parameters.Concat(filters).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            return _podio.Get<FileResponse>(url, requestData, options);
        }

        /// <summary>
        ///     Deletes items from a given app based in bulk and removes them from all views. The data can no longer be retrieved.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/items/bulk-delete-items-19406111 </para>
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="deleteRequest"></param>
        /// <param name="silent">
        ///     If set to true, the object will not be bumped up in the stream and notifications will not be
        ///     generated
        /// </param>
        /// <returns>Collection of matching items</returns>
        public BulkDeletionStatus BulkDeleteItems(int appId, BulkDeleteRequest deleteRequest, bool silent = false)
        {
            string url = string.Format("/item/app/{0}/delete", appId);
            url = Podio.PrepareUrlWithOptions(url, new CreateUpdateOptions(silent));
            return _podio.Post<BulkDeletionStatus>(url, deleteRequest);
        }

        /// <summary>
        ///     Clones the given item creating a new item with identical values in the same app.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/items/clone-item-37722742 </para>
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="silent">
        ///     If set to true, the object will not be bumped up in the stream and notifications will not be
        ///     generated
        /// </param>
        /// <returns>The id of the cloned item</returns>
        public int CloneItem(int itemId, bool silent = false)
        {
            string url = string.Format("/item/{0}/clone", itemId);
            url = Podio.PrepareUrlWithOptions(url, new CreateUpdateOptions(silent));
            dynamic tw = _podio.Post<dynamic>(url);
            return int.Parse(tw["item_id"].ToString());
        }

        /// <summary>
        ///     Deletes an item and removes it from all views. The item can no longer be retrieved.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/items/delete-item-22364 </para>
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="silent">
        ///     If set to true, the object will not be bumped up in the stream and notifications will not be
        ///     generated
        /// </param>
        /// <param name="hook">If set to false, hooks will not be executed for the change</param>
        public void DeleteItem(int itemId, bool silent = false, bool hook = true)
        {
            string url = string.Format("/item/{0}", itemId);
            url = Podio.PrepareUrlWithOptions(url, new CreateUpdateOptions(silent, hook));
            _podio.Delete<dynamic>(url);
        }

        /// <summary>
        ///     Removes the reference from the item if any
        ///     <para>Podio API Reference: https://developers.podio.com/doc/items/delete-item-reference-7302326 </para>
        /// </summary>
        /// <param name="itemId"></param>
        public void DeleteItemReference(int itemId)
        {
            string url = string.Format("/item/{0}/ref", itemId);
            _podio.Delete<dynamic>(url);
        }

        /// <summary>
        ///     Creates a batch for exporting the items.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/items/export-items-4235696 </para>
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="exporter">Valid exporters are currently "xls" and "xlsx"</param>
        /// <param name="filter">The list of filters to apply</param>
        /// <returns>The id of the batch created for this export</returns>
        public int ExportItems(int appId, string exporter, ExportFilter filter)
        {
            string url = string.Format("/item/app/{0}/export/{1}", appId, exporter);
            dynamic response = _podio.Post<dynamic>(url, filter);
            return (int) response["batch_id"];
        }

        /// <summary>
        ///     Creates a batch for exporting the items.
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="exporter">Valid exporters are currently "xls" and "xlsx"</param>
        /// <param name="limit">The maximum number of items to export, if any</param>
        /// <param name="offset">The offset into the list of items to export</param>
        /// <param name="viewId">The id of the view to use, 0 means last used view, blank means no view</param>
        /// <param name="filters">The list of filters to apply</param>
        /// <param name="sortBy">The sorting order if not using predefined filter</param>
        /// <param name="sortDesc">True if sorting should be descending, false otherwise</param>
        /// <returns>The id of the batch created for this export</returns>
        public int ExportItems(int appId, string exporter, int? limit = null, int? offset = null, int? viewId = null,
            Object filters = null, string sortBy = null, bool? sortDesc = null)
        {
            var requestData = new ExportFilter
            {
                Limit = limit,
                Offset = offset,
                ViewId = viewId,
                SortBy = sortBy,
                SortDesc = sortDesc,
                Filters = filters
            };

            return ExportItems(appId, exporter, requestData);
        }

        /// <summary>
        ///     Retrieves the items in the app based on the given view.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/items/filter-items-by-view-4540284 </para>
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="viewId"></param>
        /// <param name="limit">The maximum number of items to return, defaults to 30</param>
        /// <param name="offset">The offset into the returned items, defaults to 0</param>
        /// <param name="remember">True if the view should be remembered, defaults to false</param>
        /// <param name="includeFiles">True to include files when getting filtered items, defaults to false</param>
        /// <returns></returns>
        public PodioCollection<Item> FilterItemsByView(int appId, int viewId, int limit = 30, int offset = 0,
            bool remember = false, bool includeFiles = false)
        {
            string url = string.Format("/item/app/{0}/filter/{1}/", appId, viewId);
            if (includeFiles)
            {
                url = url + "?fields=items.fields(files)";
            }
            var filterOptions = new FilterOptions
            {
                Limit = limit,
                Offset = offset,
                Remember = remember
            };
            return _podio.Post<PodioCollection<Item>>(url, filterOptions);
        }

        /// <summary>
        ///     Returns the top possible values for the given field. This is currently only valid for fields of type "app".
        ///     <para>Podio API Reference: https://developers.podio.com/doc/items/get-top-values-for-field-68334 </para>
        /// </summary>
        /// <param name="fieldId"></param>
        /// <param name="limit">The maximum number of results to return Default value: 13</param>
        /// <param name="notItemIds">If supplied the items with these ids will not be returned</param>
        /// <returns></returns>
        public List<Item> GetTopValuesByField(int fieldId, int limit = 13, int[] notItemIds = null)
        {
            string itemIdCSV = Utilities.ArrayToCSV(notItemIds);
            var requestData = new Dictionary<string, string>
            {
                {"limit", limit.ToString()},
                {"not_item_id", itemIdCSV}
            };

            string url = string.Format("/item/field/{0}/top/", fieldId);
            return _podio.Get<List<Item>>(url, requestData);
        }

        /// <summary>
        ///     Used to get the distinct values for all items in an app.
        ///     <para>
        ///         Will return a list of the distinct item creators, as well as a list of the possible values for fields of type
        ///         "state", "member", "app", "number", "calculation", "progress" and "question".
        ///     </para>
        ///     <para>Podio API Reference: https://developers.podio.com/doc/items/get-app-values-22455 </para>
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        public AppValues GetAppValues(int appId)
        {
            string url = string.Format("/item/app/{0}/values", appId);
            return _podio.Get<AppValues>(url);
        }

        /// <summary>
        ///     Returns the values for a specified field on an item in specified type.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/items/get-item-field-values-22368 </para>
        /// </summary>
        /// <typeparam name="T">Type of field. Response will be deserialized to this type</typeparam>
        /// <param name="itemId"></param>
        /// <param name="fieldId"></param>
        /// <returns></returns>
        public T GetItemFieldValues<T>(int itemId, int fieldId) where T : new()
        {
            string url = string.Format("/item/{0}/value/{1}", itemId, fieldId);
            return _podio.Get<T>(url);
        }

        /// <summary>
        ///     Returns a preview of the item for referencing on the given field.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/items/get-item-preview-for-field-reference-7529318 </para>
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="fieldId"></param>
        /// <returns></returns>
        public Item GetItemBasicByField(int itemId, string fieldId)
        {
            string url = string.Format("/item/{0}/reference/{1}/preview", itemId, fieldId);
            return _podio.Get<Item>(url);
        }

        /// <summary>
        ///     Returns the items that have a reference to the given item. The references are grouped by app. Both the apps and the
        ///     items are sorted by title.
        ///     <para>Podio API Reference : https://developers.podio.com/doc/items/get-item-references-22439 </para>
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public List<ItemReference> GetItemReferences(int itemId)
        {
            string url = string.Format("/item/{0}/reference/", itemId);
            return _podio.Get<List<ItemReference>>(url);
        }

        /// <summary>
        ///     Returns the number of items on app.
        ///     <para>Podio API Reference : https://developers.podio.com/doc/items/get-item-count-34819997 </para>
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        public int GetItemCount(int appId)
        {
            string url = string.Format("/item/app/{0}/count", appId);
            dynamic countObj = _podio.Get<dynamic>(url);
            return int.Parse(countObj["count"].ToString());
        }

        /// <summary>
        ///     Returns the range for the given field. Only valid for fields of type "number", "calculation" and "money".
        ///     <para>Podio API Reference : https://developers.podio.com/doc/items/get-field-ranges-24242866 </para>
        /// </summary>
        /// <param name="fieldId"></param>
        /// <returns></returns>
        public Range GetFieldRanges(int fieldId)
        {
            string url = string.Format("/item/field/{0}/range", fieldId);
            return _podio.Get<Range>(url);
        }


        /// <summary>
        ///     Returns all the values for an item, with the additional data provided by the get item operation.
        ///     <para> Podio API Reference : https://developers.podio.com/doc/items/get-item-values-22365 </para>
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public List<ItemField> GetItemValues(int itemId)
        {
            string url = string.Format("/item/{0}/value", itemId);
            return _podio.Get<List<ItemField>>(url);
        }

        /// <summary>
        ///     Returns all the revisions that have been made to an item.
        ///     <para>Podio API Reference : https://developers.podio.com/doc/items/get-item-revision-22373 </para>
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public ItemRevision GetItemRevision(int itemId, int revision)
        {
            string url = string.Format("/item/{0}/revision/{1}", itemId, revision);
            return _podio.Get<ItemRevision>(url);
        }

        /// <summary>
        ///     Returns all the revisions that have been made to an item.
        ///     <para>Podio API Reference : https://developers.podio.com/doc/items/get-item-revisions-22372 </para>
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public List<ItemRevision> GetItemRevisions(int itemId)
        {
            string url = string.Format("/item/{0}/revision/", itemId);
            return _podio.Get<List<ItemRevision>>(url);
        }

        /// <summary>
        ///     Gets the URL to join the given meeting. If the user is the organizer of the meeting, the meeting will be started
        ///     and the URL will log in the user automatically.
        ///     <para>Podio API Reference : https://developers.podio.com/doc/items/get-meeting-url-14763260 </para>
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public string GetMeetingUrl(int itemId)
        {
            string url = string.Format("/item/{0}/meeting/url", itemId);
            dynamic response = _podio.Get<dynamic>(url);
            if (response["url"] != null)
                return response["url"];
            else
                return string.Empty;
        }

        /// <summary>
        ///     Updates the participation status for the active user on the item.
        ///     <para>Podio API Reference : https://developers.podio.com/doc/items/set-participation-7156154 </para>
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="status">The new status, either "invited", "accepted", "declined" or "tentative"</param>
        public void SetParticipation(int itemId, string status)
        {
            dynamic requestData = new
            {
                status = status
            };
            string url = string.Format("/item/{0}/participation", itemId);
            _podio.Put<dynamic>(url, requestData);
        }

        /// <summary>
        ///     Returns all the references to the item from the given field.
        ///     <para>Podio API Reference : https://developers.podio.com/doc/items/get-references-to-item-by-field-7403920 </para>
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="fieldId"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public List<ItemMicro> GetReferencesToItemByField(int itemId, int fieldId, int limit = 20)
        {
            var requestData = new Dictionary<string, string>
            {
                {"limit", limit.ToString()}
            };
            string url = string.Format("/item/{0}/reference/field/{1}", itemId, fieldId);
            return _podio.Get<List<ItemMicro>>(url, requestData);
        }

        /// <summary>
        ///     Updates the reference on the item.
        ///     <para>Podio API Reference : https://developers.podio.com/doc/items/update-item-reference-7421495 </para>
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="type">The type of the reference</param>
        /// <param name="referenceId">todo: describe referenceId parameter on UpdateItemReference</param>
        public void UpdateItemReference(int itemId, string type, int referenceId)
        {
            dynamic requestData = new
            {
                type = type,
                id = referenceId
            };
            string url = string.Format("/item/{0}/ref", itemId);
            _podio.Put<dynamic>(url, requestData);
        }

        /// <summary>
        ///     Reverts the change done in the given revision. This restores the changes done between the given revision and the
        ///     previous revision, overwriting any changes done on the same fields after the revision.
        ///     <para>Podio API Reference : https://developers.podio.com/doc/items/revert-item-revision-953195 </para>
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="revisionId"></param>
        /// <returns>The id of the new revision</returns>
        public int? RevertItemRevision(int itemId, int revisionId)
        {
            var url = string.Format("/item/{0}/revision/{1}", itemId, revisionId);
            var response = _podio.Delete<dynamic>(url);
            if (response["revision"] != null)
            {
                return (int) response["revision"];
            }
            return null;
        }

        /// <summary>
        /// Reverts the item to the values in the given revision. This will undo any changes made after the given revision.
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="revisionId"></param>
        /// <returns>The id of the new revision</returns>
        public int? RevertToRevision(int itemId, int revisionId)
        {
            var url = string.Format("/item/{0}/revision/{1}/revert_to", itemId, revisionId);
            var response = _podio.Post<dynamic>(url);
            if (response["revision"] != null)
            {
                return (int)response["revision"];
            }
            return null;
        }

        /// <summary>
        ///     Used to find possible items for a given application field. It searches the relevant apps for items matching the
        ///     given text.
        ///     <para>Podio API Reference : https://developers.podio.com/doc/items/get-references-to-item-by-field-7403920 </para>
        /// </summary>
        /// <param name="fieldId"></param>
        /// <param name="limit">The maximum number of results to return Default value: 13</param>
        /// <param name="notItemIds">If supplied the items with these ids will not be returned</param>
        /// <param name="sort">
        ///     The ordering of the returned items. Can be either "created_on", "title" or blank for relevance
        ///     ordering.
        /// </param>
        /// <param name="text">The text to search for. The search will be lower case, and with a wildcard in each end.</param>
        /// <returns></returns>
        public List<Item> FindReferenceableItems(int fieldId, int limit = 13, int[] notItemIds = null,
            string sort = null, string text = null)
        {
            string itemIdCSV = Utilities.ArrayToCSV(notItemIds);
            var requestData = new Dictionary<string, string>
            {
                {"limit", limit.ToString()},
                {"not_item_id", itemIdCSV},
                {"sort", sort},
                {"text", text}
            };
            string url = string.Format("/item/field/{0}/find", fieldId);
            return _podio.Get<List<Item>>(url, requestData);
        }

        /// <summary>
        ///     Return all the values cloned for an item.
        ///     <para>Podio API Reference : https://developers.podio.com/doc/items/get-item-clone-96822231 </para>
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public Clone GetItemClone(int itemId)
        {
            string url = string.Format("/item/{0}/clone", itemId);
            return _podio.Get<Clone>(url);
        }

        /// <summary>
        ///     Returns the difference in fields values between the two revisions.
        ///     <para>Podio API Reference : https://developers.podio.com/doc/items/get-item-revision-difference-22374 </para>
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="revisionFrom"></param>
        /// <param name="revisionTo"></param>
        /// <returns></returns>
        public List<ItemDiff> GetItemRevisionDifference(int itemId, int revisionFrom, int revisionTo)
        {
            string url = string.Format("/item/{0}/revision/{1}/{2}", itemId, revisionFrom, revisionTo);
            return _podio.Get<List<ItemDiff>>(url);
        }

        public ItemCalculate Calcualate(int itemId, ItemCalculateRequest ItemCalculateRequest, int limit = 30)
        {
            ItemCalculateRequest.Limit = ItemCalculateRequest.Limit == 0 ? 30 : ItemCalculateRequest.Limit;
            string url = string.Format("/item/app/{0}/calculate", itemId);
            return _podio.Post<ItemCalculate>(url, ItemCalculateRequest);
        }
    }
}