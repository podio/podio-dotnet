using System;
using System.Collections.Generic;
using PodioAPI.Models;
using PodioAPI.Models.Request;
using PodioAPI.Utils;

namespace PodioAPI.Services
{
    public class TaskService
    {
        private readonly Podio _podio;

        public TaskService(Podio currentInstance)
        {
            _podio = currentInstance;
        }

        /// <summary>
        ///     Returns the task summary for the active user.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/tasks/get-task-summary-1612017 </para>
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        public TaskSummary GetTaskSummary(int limit = 4)
        {
            string url = "/task/summary";
            var requestData = new Dictionary<string, string>
            {
                {"limit", limit.ToString()}
            };
            return _podio.Get<TaskSummary>(url, requestData);
        }

        /// <summary>
        ///     Returns the task summary for the organization for the active user.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/tasks/get-task-summary-for-organization-1612063 </para>
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public TaskSummary GetTaskSummaryForOrganization(int orgId, int limit = 4)
        {
            string url = string.Format("/task/org/{0}/summary", orgId);
            var requestData = new Dictionary<string, string>
            {
                {"limit", limit.ToString()}
            };
            return _podio.Get<TaskSummary>(url, requestData);
        }

        /// <summary>
        ///     Returns the tasks summary for personal tasks and tasks on personal spaces and sub-orgs.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/tasks/get-task-summary-for-personal-1657217 </para>
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        public TaskSummary GetTaskSummaryForPersonal(int limit = 4)
        {
            string url = "/task/personal/summary";
            var requestData = new Dictionary<string, string>
            {
                {"limit", limit.ToString()}
            };
            return _podio.Get<TaskSummary>(url, requestData);
        }

        /// <summary>
        ///     Returns the task summary for the given object.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/tasks/get-task-summary-for-reference-1657980 </para>
        /// </summary>
        /// <param name="refType"></param>
        /// <param name="refId"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public TaskSummary GetTaskSummaryForReference(string refType, int refId, int limit = 4)
        {
            string url = string.Format("/task/{0}/{1}/summary", refType, refId);
            var requestData = new Dictionary<string, string>
            {
                {"limit", limit.ToString()}
            };
            return _podio.Get<TaskSummary>(url, requestData);
        }

        /// <summary>
        ///     Returns the task summary for the given space for the active user.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/tasks/get-task-summary-for-space-1612130 </para>
        /// </summary>
        /// <param name="spaceId"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public TaskSummary GetTaskSummaryForSpace(int spaceId, int limit = 4)
        {
            string url = string.Format("/task/space/{0}/summary", spaceId);
            var requestData = new Dictionary<string, string>
            {
                {"limit", limit.ToString()}
            };
            return _podio.Get<TaskSummary>(url, requestData);
        }

        /// <summary>
        ///     Returns the total open tasks for the user on the reference.
        ///     <para>API Reference: https://developers.podio.com/doc/tasks/get-task-count-38316458 </para>
        /// </summary>
        /// <param name="refType"></param>
        /// <param name="refId"></param>
        /// <returns></returns>
        public int GetTaskCount(string refType, int refId)
        {
            string url = string.Format("/task/{0}/{1}/count", refType, refId);
            dynamic response = _podio.Get<dynamic>(url);
            return (int) response["count"];
        }

        /// <summary>
        ///     Returns the users task labels.
        ///     <para>API Reference: https://developers.podio.com/doc/tasks/get-labels-151534 </para>
        /// </summary>
        /// <returns></returns>
        public List<TaskLabel> GetLabels()
        {
            string url = "/task/label/";
            return _podio.Get<List<TaskLabel>>(url);
        }

        /// <summary>
        ///     Returns the task with the given id.
        ///     <para>API Reference: https://developers.podio.com/doc/tasks/get-task-22413 </para>
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public Task GetTask(int taskId)
        {
            string url = string.Format("/task/{0}", taskId);
            return _podio.Get<Task>(url);
        }

        /// <summary>
        ///     Returns the total number of tasks for the given time.
        ///     <para>API Reference: https://developers.podio.com/doc/tasks/get-task-totals-by-time-5435062 </para>
        /// </summary>
        /// <param name="time">The valid values for time are: "overdue", "due", "today" and "all".</param>
        /// <param name="spaceId"></param>
        /// <returns></returns>
        public int GetTaskTotalsByTime(string time, int spaceId)
        {
            string url = string.Format("/task/total/{0}", time);
            var requestData = new Dictionary<string, string>
            {
                {"space_id", spaceId.ToString()}
            };
            dynamic response = _podio.Get<dynamic>(url, requestData);
            return (int) response["count"];
        }

        /// <summary>
        ///     Mark the completed task as no longer being completed.
        ///     <para>API Reference: https://developers.podio.com/doc/tasks/incomplete-task-22433 </para>
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="hook">True if hooks should be executed for the change, false otherwise. Default value: true</param>
        /// <param name="silent">
        ///     If set to true, the object will not be bumped up in the stream and notifications will not be
        ///     generated. Default value: false
        /// </param>
        public void IncompleteTask(int taskId, bool hook = true, bool silent = false)
        {
            string url = string.Format("/task/{0}/incomplete", taskId);
            url = Podio.PrepareUrlWithOptions(url, new CreateUpdateOptions(silent, hook));
            _podio.Post<dynamic>(url);
        }

        /// <summary>
        ///     Creates a new task
        ///     <para>API Reference: https://developers.podio.com/doc/tasks/create-task-22419 </para>
        /// </summary>
        /// <param name="task"></param>
        /// <param name="refType">The valid types of objects are "item", "status", "app", "space" and "conversation".</param>
        /// <param name="refId">Id of the reference</param>
        /// <param name="hook">True if hooks should be executed for the change, false otherwise. Default value: true</param>
        /// <param name="silent">
        ///     If set to true, the object will not be bumped up in the stream and notifications will not be
        ///     generated. Default value: false
        /// </param>
        /// <returns></returns>
        public List<Task> CreateTask(TaskCreateUpdateRequest task, string refType = null, int? refId = null,
            bool hook = true, bool silent = false)
        {
            string url = "/task/";
            if (!string.IsNullOrEmpty(refType) && refId != null)
            {
                url = string.Format("/task/{0}/{1}/", refType, refId);
            }
            url = Podio.PrepareUrlWithOptions(url, new CreateUpdateOptions(silent, hook));
            var createdTasks = new List<Task>();
            if ((task.Responsible is IEnumerable<int> || task.Responsible is IEnumerable<Ref>) &&
                task.Responsible.Count > 1)
            {
                List<Task> response = _podio.Post<List<Task>>(url, task);
                createdTasks = response;
            }
            else
            {
                Task response = _podio.Post<Task>(url, task);
                createdTasks.Add(response);
            }
            return createdTasks;
        }

        /// <summary>
        ///     Creates a new task with no reference to other objects.
        ///     <para>API Reference: https://developers.podio.com/doc/tasks/create-task-22419 </para>
        /// </summary>
        /// <param name="text">The text of the task</param>
        /// <param name="dueDate">The due date and time of the task, if any (in local time)</param>
        /// <param name="description">The description of the task</param>
        /// <param name="responsible">user_id of the re</param>
        /// <param name="isPrivate">True if the task should be private, default true</param>
        /// <param name="refType">The valid types of objects are "item", "status", "app", "space" and "conversation".</param>
        /// <param name="refId">Id of the reference</param>
        /// <param name="hook">True if hooks should be executed for the change, false otherwise. Default value: true</param>
        /// <param name="silent">
        ///     If set to true, the object will not be bumped up in the stream and notifications will not be
        ///     generated. Default value: false
        /// </param>
        /// <returns></returns>
        public List<Task> CreateTask(string text, DateTime? dueDate = null, string description = null,
            int? responsible = null, bool isPrivate = true, string refType = null, int? refId = null, bool hook = true,
            bool silent = false)
        {
            var task = new TaskCreateUpdateRequest
            {
                Text = text,
                Description = description,
                DueDate = dueDate,
                Responsible = responsible,
                Private = isPrivate
            };
            return CreateTask(task, refType, refId, hook, silent);
        }

        /// <summary>
        ///     Updates the task with the given requestData. Any requestData not specified will remain unchanged.
        ///     <para>API Reference: https://developers.podio.com/doc/tasks/update-task-10583674 </para>
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="task"></param>
        /// <param name="hook">True if hooks should be executed for the change, false otherwise. Default value: true</param>
        /// <param name="silent">
        ///     If set to true, the object will not be bumped up in the stream and notifications will not be
        ///     generated. Default value: false
        /// </param>
        /// <returns></returns>
        public Task UpdateTask(int taskId, TaskCreateUpdateRequest task, bool hook = true, bool silent = false)
        {
            string url = string.Format("/task/{0}", taskId);
            url = Podio.PrepareUrlWithOptions(url, new CreateUpdateOptions(silent, hook));
            return _podio.Put<Task>(url, task);
        }

        /// <summary>
        ///     Deletes the task with the given id.
        ///     <para>API Reference: https://developers.podio.com/doc/tasks/delete-task-77179 </para>
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="hook">True if hooks should be executed for the change, false otherwise. Default value: true</param>
        /// <param name="silent">
        ///     If set to true, the object will not be bumped up in the stream and notifications will not be
        ///     generated. Default value: false
        /// </param>
        public void DeleteTask(int taskId, bool hook = true, bool silent = false)
        {
            string url = string.Format("/task/{0}", taskId);
            url = Podio.PrepareUrlWithOptions(url, new CreateUpdateOptions(silent, hook));
            _podio.Delete<dynamic>(url);
        }

        /// <summary>
        ///     Deletes the label with the given id. This will remove the label from all tasks.
        ///     <para>API Reference: https://developers.podio.com/doc/tasks/delete-label-151302 </para>
        /// </summary>
        /// <param name="labelId"></param>
        public void DeleteLabel(int labelId)
        {
            string url = string.Format("/task/label/{0}", labelId);
            _podio.Delete<dynamic>(url);
        }

        /// <summary>
        ///     Ranks the task in comparision to one or two other tasks.
        ///     <para>API Reference: https://developers.podio.com/doc/tasks/rank-task-81015 </para>
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="beforeTaskId"></param>
        /// <param name="afterTaskId"></param>
        public void RankTask(int taskId, int beforeTaskId, int afterTaskId)
        {
            string url = string.Format("/task/{0}/rank", taskId);
            dynamic requestData = new
            {
                after = afterTaskId,
                before = beforeTaskId
            };
            _podio.Post<dynamic>(url);
        }

        /// <summary>
        ///     Deletes the reference on the task.
        ///     <para>API Reference: https://developers.podio.com/doc/tasks/remove-task-reference-6146114 </para>
        /// </summary>
        /// <param name="taskId"></param>
        public void RemoveTaskReference(int taskId)
        {
            string url = string.Format("/task/{0}/ref", taskId);
            _podio.Delete<dynamic>(url);
        }

        /// <summary>
        ///     Updates the label with the given id.
        ///     <para>API Reference: https://developers.podio.com/doc/tasks/update-label-151289 </para>
        /// </summary>
        /// <param name="labelId"></param>
        /// <param name="text">The name of the new label</param>
        /// <param name="color">The color of the label in hex format (xxxxxx)</param>
        public void UpdateLabel(int labelId, string text, string color)
        {
            string url = string.Format("/task/label/{0}", labelId);
            dynamic requestData = new
            {
                text = text,
                color = color
            };
            _podio.Put<dynamic>(url, requestData);
        }

        /// <summary>
        ///     Updates the description of the task.
        ///     <para>API Reference: https://developers.podio.com/doc/tasks/update-task-description-76982 </para>
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="description"></param>
        /// <param name="hook"></param>
        /// <param name="silent"></param>
        public void UpdateTaskDescription(int taskId, string description, bool hook = true, bool silent = false)
        {
            string url = string.Format("/task/{0}/description", taskId);
            url = Podio.PrepareUrlWithOptions(url, new CreateUpdateOptions(silent, hook));
            dynamic requestData = new
            {
                description = description
            };
            _podio.Put<dynamic>(url, requestData);
        }

        /// <summary>
        ///     Update the private flag on the given task.
        ///     <para>API Reference: https://developers.podio.com/doc/tasks/update-task-private-22434 </para>
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="isPrivate">True if the task should be private, false otherwise</param>
        /// <param name="hook"></param>
        /// <param name="silent"></param>
        public void UpdateTaskPrivate(int taskId, bool isPrivate, bool hook = true, bool silent = false)
        {
            string url = string.Format("/task/{0}/private", taskId);
            url = Podio.PrepareUrlWithOptions(url, new CreateUpdateOptions(silent, hook));
            dynamic requestData = new
            {
                @private = isPrivate
            };
            _podio.Put<dynamic>(url, requestData);
        }

        /// <summary>
        ///     Updates the text of the task.
        ///     <para>API Reference: https://developers.podio.com/doc/tasks/update-task-text-22428 </para>
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="text">The new text of the task</param>
        /// <param name="hook"></param>
        /// <param name="silent"></param>
        public void UpdateTaskText(int taskId, string text, bool hook = true, bool silent = false)
        {
            string url = string.Format("/task/{0}/text", taskId);
            url = Podio.PrepareUrlWithOptions(url, new CreateUpdateOptions(silent, hook));
            dynamic requestData = new
            {
                text = text
            };
            _podio.Put<dynamic>(url, requestData);
        }

        /// <summary>
        ///     Updates the due on property on the task.
        ///     <para>API Reference: https://developers.podio.com/doc/tasks/update-task-due-on-3442633 </para>
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="dueOn">The date and time the task is due (in UTC)</param>
        /// <param name="dueDateTime">The date and time the task is due (in local date and time)</param>
        /// <param name="hook"></param>
        /// <param name="silent"></param>
        public void UpdateTaskDueOn(int taskId, DateTime dueOn, DateTime dueDateTime, bool hook = true,
            bool silent = false)
        {
            string url = string.Format("/task/{0}/due", taskId);
            url = Podio.PrepareUrlWithOptions(url, new CreateUpdateOptions(silent, hook));
            dynamic requestData = new
            {
                due_on = dueOn,
                due_date = dueDateTime.Date,
                due_time = dueDateTime.TimeOfDay
            };
            _podio.Put<dynamic>(url, requestData);
        }

        /// <summary>
        ///     Updates the task with new labels.
        ///     <para>API Reference: https://developers.podio.com/doc/tasks/update-task-labels-151769 </para>
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="labelIds"></param>
        public void UpdateTaskLabels(int taskId, List<int> labelIds)
        {
            string url = string.Format("/task/{0}/label/", taskId);
            dynamic requestData = labelIds;
            _podio.Put<dynamic>(url, requestData);
        }

        /// <summary>
        ///     Attached this task to an object. If the task is already attached to an object, it will be detached from that object
        ///     and reattached on the new object.
        ///     <para>API Reference: https://developers.podio.com/doc/tasks/update-task-reference-170733 </para>
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="refType"></param>
        /// <param name="refId"></param>
        /// <param name="hook"></param>
        /// <param name="silent"></param>
        public void UpdateTaskReference(int taskId, string refType, int refId, bool hook = true, bool silent = false)
        {
            string url = string.Format("/task/{0}/ref", taskId);
            url = Podio.PrepareUrlWithOptions(url, new CreateUpdateOptions(silent, hook));
            dynamic requestData = new
            {
                ref_type = refType,
                ref_id = refId,
            };
            _podio.Put<dynamic>(url, requestData);
        }

        /// <summary>
        ///     Returns a list of all tasks matching the given filters and grouped by the specified group.
        ///     <para>API Reference: https://developers.podio.com/doc/tasks/get-tasks-77949 </para>
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="completed">True to only return completed tasks, False to return open tasks.</param>
        /// <param name="completedBy">The entities that completed the task.</param>
        /// <param name="completedOn">The from and to date the task should be completed between.</param>
        /// <param name="createdBy">The entities that created the task.</param>
        /// <param name="createdOn">The from and to date the task should be created between.</param>
        /// <param name="createdVia">The id of the client the task was created via.</param>
        /// <param name="dueDate">The from and to date the task should be due between.</param>
        /// <param name="externalId">The external id of the task.</param>
        /// <param name="files">
        ///     True if there should be files on the task, False if there should be no files on the task, leave out
        ///     for no restriction.
        /// </param>
        /// <param name="grouping">
        ///     The grouping to use. Valid options are "due_date", "created_by", "responsible", "app",  "space"
        ///     and "org".
        /// </param>
        /// <param name="label">The id of the a required label on the tasks.</param>
        /// <param name="limit">The maximum number of tasks to return.</param>
        /// <param name="offset">The offset into the tasks to return. Default value: 0</param>
        /// <param name="org">The ids of the orgs the tasks are related to.</param>
        /// <param name="reassigned">
        ///     True to only return tasks the active user has assigned to someone else, false to only return
        ///     tasks that the active user has not assigned to someone else.
        /// </param>
        /// <param name="reference">The list of references on the form "type:id" separated by semi-colon.</param>
        /// <param name="responsible">The user ids that are responsible for the task.</param>
        /// <param name="sort_by">
        ///     The sort order of the tasks returned. Either "created_on", "completed_on" or "rank". Default
        ///     value: rank
        /// </param>
        /// <param name="sortDesc">true if tasks should be sorted descending, false otherwise.Default value: false</param>
        /// <param name="space">The ids of the spaces the tasks are related to.</param>
        /// <param name="view">
        ///     The level of information to return. Setting to "full" will return the full task as specific on the
        ///     get task operation.
        /// </param>
        /// <returns></returns>
        public List<Task> GetTasks(int? appId = null, bool? completed = null, int? completedBy = null,
            string completedOn = null, int? createdBy = null, string createdOn = null, int? createdVia = null,
            string dueDate = null, string externalId = null, bool? files = null, string grouping = null,
            int? label = null, int? limit = null, int offset = 0, int? org = null, bool? reassigned = null,
            string reference = null, int? responsible = null, string sort_by = "rank", bool sortDesc = false,
            int? space = null, string view = null)
        {
            string url = "/task/";
            var requestData = new Dictionary<string, string>
            {
                {"app", appId.ToStringOrNull()},
                {"completed", completed.ToStringOrNull()},
                {"completed_by", completedBy.ToStringOrNull()},
                {"completed_on", completedOn},
                {"created_by", createdBy.ToStringOrNull()},
                {"created_on", createdOn},
                {"created_via", createdVia.ToStringOrNull()},
                {"due_date", dueDate},
                {"files", files.ToStringOrNull()},
                {"grouping", grouping},
                {"label", label.ToStringOrNull()},
                {"limit", limit.ToStringOrNull()},
                {"offset", offset.ToString()},
                {"org", org.ToStringOrNull()},
                {"reassigned", reassigned.ToStringOrNull()},
                {"reference", reference},
                {"responsible", responsible.ToStringOrNull()},
                {"sort_by", sort_by},
                {"sort_desc", sortDesc.ToString()},
                {"space", space.ToStringOrNull()},
                {"view", view}
            };
            return _podio.Get<List<Task>>(url, requestData);
        }

        /// <summary>
        ///     Get the totals for the users active tasks.
        ///     <para>API Reference: https://developers.podio.com/doc/tasks/get-task-totals-v2-83590 </para>
        /// </summary>
        /// <param name="spaceIds">
        ///     An optional list space ids separated by semi-colon, which will limit the totals to the given
        ///     spaces.
        /// </param>
        /// <returns></returns>
        public TaskReport GetTaskTotals(int[] spaceIds = null)
        {
            string url = "/task/total/";
            var requestData = new Dictionary<string, string>();
            if (spaceIds != null)
            {
                string spaceIdCsv = Utilities.ArrayToCSV(spaceIds, ";");
                requestData.Add("space", spaceIdCsv);
            }
            return _podio.Get<TaskReport>(url, requestData);
        }

        /// <summary>
        ///     Creates a new personal label for the user.
        ///     <para>API Reference: https://developers.podio.com/doc/tasks/create-label-151265 </para>
        /// </summary>
        /// <param name="text">The name of the new label.</param>
        /// <param name="color">The color of the label in hex format (xxxxxx).</param>
        /// <returns></returns>
        public int CreateLabel(string text, string color)
        {
            string url = "/task/label/";
            dynamic requestData = new
            {
                text = text,
                color = color
            };
            dynamic response = _podio.Post<dynamic>(url, requestData);
            return (int) response["label_id"];
        }

        /// <summary>
        ///     Mark the given task as completed.
        ///     <para>API Reference: https://developers.podio.com/doc/tasks/complete-task-22432 </para>
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="hook">True if hooks should be executed for the change, false otherwise. Default value: true.</param>
        /// <param name="silent">
        ///     If set to true, the object will not be bumped up in the stream and notifications will not be
        ///     generated. Default value: false.
        /// </param>
        /// <returns></returns>
        public int? CompleteTask(int taskId, bool hook = true, bool silent = false)
        {
            string url = string.Format("/task/{0}/complete", taskId);
            url = Podio.PrepareUrlWithOptions(url, new CreateUpdateOptions(silent, hook));
            dynamic response = _podio.Post<dynamic>(url);
            if (response != null)
                return (int?) response["recurring_task_id"];
            return null;
        }

        /// <summary>
        ///     Assigns the task to another user. This makes the user responsible for the task and its completion.
        ///     <para>API Reference: https://developers.podio.com/doc/tasks/assign-task-22412 </para>
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="silent">
        ///     If set to true, the object will not be bumped up in the stream and notifications will not be
        ///     generated. Default value: false.
        /// </param>
        /// <param name="responsible">The contact responsible (user_id), or null if no one should be responsible.</param>
        public void AssignTask(int taskId, int? responsible = null, bool silent = false)
        {
            string url = string.Format("/task/{0}/assign", taskId);
            url = Podio.PrepareUrlWithOptions(url, new CreateUpdateOptions(silent));
            dynamic requestData = new
            {
                responsible = responsible
            };
            _podio.Post<dynamic>(url, requestData);
        }
    }
}