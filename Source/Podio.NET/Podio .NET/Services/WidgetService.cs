using System.Collections.Generic;
using PodioAPI.Models;
using System.Threading.Tasks;

namespace PodioAPI.Services
{
    public class WidgetService
    {
        private readonly Podio _podio;

        public WidgetService(Podio currentInstance)
        {
            _podio = currentInstance;
        }

        /// <summary>
        ///     Create a new widget on the given reference.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/widgets/create-widget-22491 </para>
        /// </summary>
        /// <param name="refType"></param>
        /// <param name="refId"></param>
        /// <param name="type">The type of widget, see the area for possible values</param>
        /// <param name="title">The title of the widget</param>
        /// <param name="config">The configuration, depends on the types. See the area for details</param>
        /// <returns></returns>
        public async Task<int> CreateWidget(string refType, int refId, string type, string title, dynamic config)
        {
            string url = string.Format("/widget/{0}/{1}/", refType, refId);
            dynamic requestData = new
            {
                type = type,
                title = title,
                config = config
            };

            dynamic respone =  await _podio.Post<dynamic>(url, requestData);
            return (int) respone["widget_id"];
        }

        /// <summary>
        ///     Updates a widget with a new title and configuration.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/widgets/update-widget-22490 </para>
        /// </summary>
        /// <param name="widgetId"></param>
        /// <param name="title"></param>
        /// <param name="config"></param>
        public async Task<dynamic> UpdateWidget(int widgetId, string title, dynamic config)
        {
            string url = string.Format("/widget/{0}", widgetId);
            dynamic requestData = new
            {
                title = title,
                config = config
            };

             return await _podio.Put<dynamic>(url, requestData);
        }

        /// <summary>
        ///     Clones the widget to a new position.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/widgets/clone-widget-105850650 </para>
        /// </summary>
        /// <param name="widgetId">Widget id to be cloned</param>
        /// <param name="type">The type of the new position, either "user" or "space",</param>
        /// <param name="id">The id of the new position</param>
        /// <returns>The id of the cloned widget</returns>
        public async Task<int> CloneWidget(int widgetId, string type, string id)
        {
            string url = string.Format("/widget/{0}/clone", widgetId);
            dynamic requestData = new
            {
                type = type,
                id = id
            };

            dynamic respone =  await _podio.Post<dynamic>(url, requestData);
            return (int) respone["widget_id"];
        }

        /// <summary>
        ///     Deletes the given widget.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/widgets/delete-widget-22492 </para>
        /// </summary>
        /// <param name="widgetId"></param>
        public async Task<dynamic> DeleteWidget(int widgetId)
        {
            string url = string.Format("/widget/{0}", widgetId);
           return   await _podio.Delete<dynamic>(url);
        }

        /// <summary>
        ///     Updates the order of the widgets on a reference.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/widgets/update-widget-order-22495 </para>
        /// </summary>
        /// <param name="refType"></param>
        /// <param name="refId"></param>
        /// <param name="widgetIds">The ids of the widgets in the new requested order.</param>
        public async Task<dynamic> UpdateWidgetOrder(string refType, string refId, List<int> widgetIds)
        {
            string url = string.Format("/widget/{0}/{1}/order", refType, refId);
            return  await _podio.Put<dynamic>(url, widgetIds);
        }

        /// <summary>
        ///     Returns the widget with the given id.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/widgets/get-widget-22489 </para>
        /// </summary>
        /// <param name="widgetId"></param>
        /// <returns></returns>
        public async Task<Widget> GetWidget(int widgetId)
        {
            string url = string.Format("/widget/{0}", widgetId);
            return  await _podio.Get<Widget>(url);
        }

        /// <summary>
        ///     Returns the widgets on the given reference.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/widgets/get-widgets-22494 </para>
        /// </summary>
        /// <param name="refType"></param>
        /// <param name="refId"></param>
        /// <returns></returns>
        public async Task<List<Widget>> GetWidgets(string refType, int refId)
        {
            string url = string.Format("/widget/{0}/{1}/", refType, refId);
            return  await _podio.Get<List<Widget>>(url);
        }
    }
}