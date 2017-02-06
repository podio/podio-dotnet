using System.Collections.Generic;
using PodioAPI.Models;
using PodioAPI.Models.Request;

namespace PodioAPI.Services
{
    public class ConversationService
    {
        private readonly Podio _podio;

        public ConversationService(Podio currentInstance)
        {
            _podio = currentInstance;
        }

        /// <summary>
        ///     The list of people to grant access to. This is a list of contact identifiers.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/conversations/add-participants-v2-37282400 </para>
        /// </summary>
        /// <param name="conversationId"></param>
        /// <param name="participants">The list of people to grant access to. This is a list of contact identifiers</param>
        public void AddParticipants(int conversationId, List<Ref> participants)
        {
            string url = string.Format("/conversation/{0}/participant/v2/", conversationId);
            dynamic requestData = new
            {
                participants = participants
            };
            _podio.Post<dynamic>(url, requestData);
        }

        /// <summary>
        ///     Creates a new conversation with a list of users. Once a conversation is started, the participants cannot (yet) be
        ///     changed.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/conversations/create-conversation-v2-37301474 </para>
        /// </summary>
        /// <param name="conversationCreateRequest"></param>
        /// <returns></returns>
        public Conversation CreateConversation(ConversationCreateRequest conversationCreateRequest)
        {
            string url = "/conversation/v2/";
            return _podio.Post<Conversation>(url, conversationCreateRequest);
        }

        /// <summary>
        ///     Creates a new conversation on the given object. Works similarly to the create notification method in other regards.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/conversations/create-conversation-on-object-22442 </para>
        /// </summary>
        /// <param name="refType"></param>
        /// <param name="refId"></param>
        /// <param name="conversationCreateRequest"></param>
        /// <returns></returns>
        public Conversation CreateConversationOnObject(string refType, int refId,
            ConversationCreateRequest conversationCreateRequest)
        {
            string url = string.Format("/conversation/{0}/{1}/", refType, refId);
            return _podio.Post<Conversation>(url, conversationCreateRequest);
        }

        /// <summary>
        ///     Gets the conversation including participants and messages with the the given id. Only participants in the
        ///     conversation is allowed to view the conversation.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/conversations/get-conversation-22369 </para>
        /// </summary>
        /// <param name="conversationId"></param>
        /// <returns></returns>
        public Conversation GetConversation(int conversationId)
        {
            string url = string.Format("/conversation/{0}", conversationId);
            return _podio.Get<Conversation>(url);
        }

        /// <summary>
        ///     Returns data about the given event.
        ///     <para>Podio API Reference:  https://developers.podio.com/doc/conversations/get-conversations-34822801 </para>
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public ConversationEvent GetConversationEvent(int eventId)
        {
            string url = string.Format("/conversation/event/{0}", eventId);
            return _podio.Get<ConversationEvent>(url);
        }

        /// <summary>
        ///     Returns the events on the conversation.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/conversations/get-conversation-events-35440697 </para>
        /// </summary>
        /// <param name="conversationId"></param>
        /// <param name="limit">The maximum number of conversations/events to return. Default value: 10</param>
        /// <param name="offset">The offset into the list of conversations/events to return. Default value: 0</param>
        /// <returns></returns>
        public List<ConversationEvent> GetConversationEvents(int conversationId, int limit = 10, int offset = 0)
        {
            string url = string.Format("/conversation/{0}/event/", conversationId);
            var requestData = new Dictionary<string, string>
            {
                {"limit", limit.ToString()},
                {"offset", offset.ToString()}
            };
            return _podio.Get<List<ConversationEvent>>(url, requestData);
        }

        /// <summary>
        ///     Returns all the users conversations ordered by time of the last event.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/conversations/get-conversations-34822801 </para>
        /// </summary>
        /// <param name="limit">The maximum number of conversations/events to return. Default value: 10</param>
        /// <param name="offset">The offset into the list of conversations/events to return. Default value: 0</param>
        /// <returns></returns>
        public List<Conversation> GetConversations(int limit = 10, int offset = 0)
        {
            string url = "/conversation/";
            var requestData = new Dictionary<string, string>
            {
                {"limit", limit.ToString()},
                {"offset", offset.ToString()}
            };
            return _podio.Get<List<Conversation>>(url);
        }

        /// <summary>
        ///     Returns a list of all the conversations on the object that the active user is part of.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/conversations/get-conversations-on-object-22443 </para>
        /// </summary>
        /// <param name="refType"></param>
        /// <param name="refId"></param>
        /// <returns></returns>
        public List<Conversation> GetConversationsOnObject(string refType, int refId)
        {
            string url = string.Format("/conversation/{0}/{1}/", refType, refId);
            return _podio.Get<List<Conversation>>(url);
        }

        /// <summary>
        ///     Returns the existing direct conversation with the user. If none exists a 404 will be returned.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/conversations/get-existing-direct-conversation-44969910 </para>
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Conversation GetExistingDirectConversation(int userId)
        {
            string url = string.Format("/conversation/direct/{0}", userId);
            return _podio.Get<Conversation>(url);
        }

        /// <summary>
        ///     Returns the number of conversations with the given flag. The flag can be one of:unread or starred.
        ///     <para>Podio API Reference:  https://developers.podio.com/doc/conversations/get-flagged-conversation-counts-35467925 </para>
        /// </summary>
        /// <param name="flag"></param>
        /// <returns></returns>
        public int GetFlaggedConversationCounts(string flag)
        {
            string url = string.Format("/conversation/{0}/count", flag);
            dynamic response = _podio.Get<dynamic>(url);
            return (int) response["value"];
        }

        /// <summary>
        ///     Returns the conversations with the given flag. The flag can be one of:unread or starred
        ///     <para>Podio API Reference: https://developers.podio.com/doc/conversations/get-flagged-conversations-35466860 </para>
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public List<Conversation> GetFlaggedConversations(string flag, int limit = 10, int offset = 0)
        {
            string url = string.Format("/conversation/{0}/", flag);
            var requestData = new Dictionary<string, string>
            {
                {"limit", limit.ToString()},
                {"offset", offset.ToString()}
            };
            return _podio.Get<List<Conversation>>(url);
        }

        /// <summary>
        ///     Leave the given conversation.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/conversations/leave-conversation-35483748 </para>
        /// </summary>
        /// <param name="converstionId"></param>
        public void LeaveConversation(int converstionId)
        {
            string url = string.Format("/conversation/{0}/leave", converstionId);
            _podio.Post<dynamic>(url);
        }

        /// <summary>
        /// Mark the conversation as read.
        ///  <para>Podio API Reference: https://developers.podio.com/doc/conversations/mark-conversation-as-read-35441525 </para>
        /// </summary>
        /// <param name="conversationId"></param>
        public void MarkConversationAsRead(int conversationId)
        {
            string url = string.Format("/conversation/{0}/read", conversationId);
            _podio.Post<dynamic>(url);
        }

        /// <summary>
        ///     Marks all the users conversations as read.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/conversations/mark-conversation-as-read-35441525 </para>
        /// </summary>
        public void MarkAllConversationsAsRead()
        {
            string url = "/conversation/read";
            _podio.Post<dynamic>(url);
        }

        /// <summary>
        ///     Mark the conversation as unread.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/conversations/mark-conversation-as-unread-35441542 </para>
        /// </summary>
        /// <param name="converstionId"></param>
        public void MarkConversationAsUnread(int converstionId)
        {
            string url = string.Format("/conversation/{0}/read", converstionId);
            _podio.Delete<dynamic>(url);
        }

        /// <summary>
        ///     Creates a reply to the conversation. Returns the conversation event this generates.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/conversations/reply-to-conversation-v2-37260916 </para>
        /// </summary>
        /// <param name="converstionId"></param>
        /// <param name="text">The text of the reply.</param>
        /// <param name="fileIds">The list of file ids to be attached to the message.</param>
        /// <param name="embedId">
        ///     [OPTIONAL] The id of an embedded link that has been created with the Add an mebed operation in
        ///     the Embed area
        /// </param>
        /// <param name="embedUrl">The url to be attached</param>
        /// <returns></returns>
        public ConversationEvent ReplyToConversation(int converstionId, string text, List<int> fileIds = null,
            int? embedId = null, string embedUrl = null)
        {
            string url = string.Format("/conversation/{0}/reply/v2", converstionId);
            dynamic requestData = new
            {
                text = text,
                file_ids = fileIds,
                embed_id = embedId,
                embed_url = embedUrl
            };
            return _podio.Post<ConversationEvent>(url, requestData);
        }

        /// <summary>
        ///     Star the given conversation. If the conversation is already starred, nothing will happen.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/conversations/star-conversation-35106944 </para>
        /// </summary>
        /// <param name="converstionId"></param>
        public void StarConversation(int converstionId)
        {
            string url = string.Format("/conversation/{0}/star", converstionId);
            _podio.Post<dynamic>(url);
        }

        /// <summary>
        ///     Removes the star from the conversation.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/conversations/unstar-conversation-35106990 </para>
        /// </summary>
        /// <param name="converstionId"></param>
        public void UnstarConversation(int converstionId)
        {
            string url = string.Format("/conversation/{0}/star", converstionId);
            _podio.Delete<dynamic>(url);
        }

        /// <summary>
        ///     Returns the users conversations that match the given text ordered by time of the last event.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/conversations/search-conversations-36885550 </para>
        /// </summary>
        /// <param name="text"></param>
        /// <param name="limit">The maximum number of conversations/events to return. Default value: 10</param>
        /// <param name="offset">The offset into the list of conversations/events to return. Default value: 0</param>
        /// <param name="participants">
        ///     If true it searches through the conversation participants names/emails. If false it searches
        ///     through the conversation subject and messages. Default value: false
        /// </param>
        /// <returns></returns>
        public List<Conversation> SearchConversations(string text, int limit = 10, int offset = 0,
            bool participants = false)
        {
            string url = string.Format("/conversation/search/");
            var requestData = new Dictionary<string, string>
            {
                {"limit", limit.ToString()},
                {"offset", offset.ToString()},
                {"participants", participants.ToString()},
                {"text", text}
            };
            return _podio.Get<List<Conversation>>(url, requestData);
        }
    }
}