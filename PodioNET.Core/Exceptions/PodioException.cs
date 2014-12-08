﻿using Newtonsoft.Json;
using System;

namespace PodioAPI.Exceptions
{
    public class PodioException : Exception
    {
        /// <summary>
        /// Response from the API
        /// </summary>
        public PodioError Error { get; internal set; }

        /// <summary>
        /// Status code of the response
        /// </summary>
        public int Status { get; internal set; }
        public PodioException(int status, PodioError error)
        {
            this.Error = error;
            this.Status = status;
        }
    }

    class PodioInvalidGrantException : PodioException
    {
        public PodioInvalidGrantException(int status, PodioError error)
            : base(status, error)
        {
        }
    }

    class PodioBadRequestException : PodioException
    {
        public PodioBadRequestException(int status, PodioError error)
            : base(status, error)
        {
        }
    }

    class PodioAuthorizationException : PodioException
    {
        public PodioAuthorizationException(int status, PodioError error)
            : base(status, error)
        {
        }
    }

    class PodioForbiddenException : PodioException
    {
        public PodioForbiddenException(int status, PodioError error)
            : base(status, error)
        {
        }
    }

    class PodioNotFoundException : PodioException
    {
        public PodioNotFoundException(int status, PodioError error)
            : base(status, error)
        {
        }
    }

    class PodioConflictException : PodioException
    {
        public PodioConflictException(int status, PodioError error)
            : base(status, error)
        {
        }
    }

    class PodioGoneException : PodioException
    {
        public PodioGoneException(int status, PodioError error)
            : base(status, error)
        {
        }
    }

    class PodioRateLimitException : PodioException
    {
        public PodioRateLimitException(int status, PodioError error)
            : base(status, error)
        {
        }
    }

    class PodioServerException : PodioException
    {
        public PodioServerException(int status, PodioError error)
            : base(status, error)
        {
        }
    }

    class PodioUnavailableException : PodioException
    {
        public PodioUnavailableException(int status, PodioError error)
            : base(status, error)
        {
        }
    }

    /// <summary>
    /// Represent the error response from API
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
