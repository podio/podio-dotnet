using System;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using PodioAPI.Utils;

namespace PodioAPI.Exceptions
{
    [Serializable]
    public class PodioException : Exception
    {
        /// <summary>
        ///     Response from the API
        /// </summary>
        public PodioError Error { get; internal set; }

        /// <summary>
        ///   HTTP Status code of the response
        /// </summary>
        public int StatusCode { get; internal set; }

        public PodioException(int statusCode, PodioError error) : base(JSONSerializer.Serilaize(error))
        {
            this.Error = error;
            this.StatusCode = statusCode;
        }

        public PodioException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }

    [Serializable]
    public class PodioInvalidGrantException : PodioException
    {
        public PodioInvalidGrantException(int status, PodioError error)
            : base(status, error)
        {
        }

        public PodioInvalidGrantException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    [Serializable]
    public class PodioBadRequestException : PodioException
    {
        public PodioBadRequestException(int status, PodioError error)
            : base(status, error)
        {
        }

        public PodioBadRequestException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    [Serializable]
    public class PodioAuthorizationException : PodioException
    {
        public PodioAuthorizationException(int status, PodioError error)
            : base(status, error)
        {
        }

        public PodioAuthorizationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    [Serializable]
    public class PodioForbiddenException : PodioException
    {
        public PodioForbiddenException(int status, PodioError error)
            : base(status, error)
        {
        }

        public PodioForbiddenException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    [Serializable]
    public class PodioNotFoundException : PodioException
    {
        public PodioNotFoundException(int status, PodioError error)
            : base(status, error)
        {
        }

        public PodioNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    [Serializable]
    public class PodioConflictException : PodioException
    {
        public PodioConflictException(int status, PodioError error)
            : base(status, error)
        {
        }

        public PodioConflictException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    [Serializable]
    public class PodioGoneException : PodioException
    {
        public PodioGoneException(int status, PodioError error)
            : base(status, error)
        {
        }

        public PodioGoneException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    [Serializable]
    public class PodioRateLimitException : PodioException
    {
        public PodioRateLimitException(int status, PodioError error)
            : base(status, error)
        {
        }

        public PodioRateLimitException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    [Serializable]
    public class PodioServerException : PodioException
    {
        public PodioServerException(int status, PodioError error)
            : base(status, error)
        {
        }

        public PodioServerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }


    public class PodioUnavailableException : PodioException
    {
        public PodioUnavailableException(int status, PodioError error)
            : base(status, error)
        {
        }

        public PodioUnavailableException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
    public class PodioInvalidJsonException : PodioException
    {
        public PodioInvalidJsonException(int status, PodioError error)
            : base(status, error)
        {
        }

        public PodioInvalidJsonException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
    /// <summary>
    ///     Represent the error response from API
    /// </summary>
    public class PodioError
    {
        [JsonProperty(PropertyName = "error_propagate")]
        public bool ErrorPropagate { get; set; }

        [JsonProperty(PropertyName = "error")]
        public string Error { get; set; }

        [JsonProperty(PropertyName = "error_description")]
        public string ErrorDescription { get; set; }

        [JsonProperty(PropertyName = "error_detail")]
        public string ErrorDetail { get; set; }

        [JsonProperty(PropertyName = "request")]
        public Request Request { get; set; }
    }

    public class Request
    {
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [JsonProperty(PropertyName = "query_string")]
        public string QueryString { get; set; }

        [JsonProperty(PropertyName = "method")]
        public string Method { get; set; }
    }
}