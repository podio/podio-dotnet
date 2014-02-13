using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    class PodioForbiddenError : PodioException
    {
        public PodioForbiddenError(int status, PodioError error)
            : base(status, error)
        {
        }
    }

    class PodioNotFoundError : PodioException
    {
        public PodioNotFoundError(int status, PodioError error)
            : base(status, error)
        {
        }
    }

    class PodioConflictError : PodioException
    {
        public PodioConflictError(int status, PodioError error)
            : base(status, error)
        {
        }
    }

    class PodioGoneError : PodioException
    {
        public PodioGoneError(int status, PodioError error)
            : base(status, error)
        {
        }
    }

    class PodioRateLimitError : PodioException
    {
        public PodioRateLimitError(int status, PodioError error)
            : base(status, error)
        {
        }
    }

    class PodioServerError : PodioException
    {
        public PodioServerError(int status, PodioError error)
            : base(status, error)
        {
        }
    }

    class PodioUnavailableError : PodioException
    {
        public PodioUnavailableError(int status, PodioError error)
            : base(status, error)
        {
        }
    }

    /// <summary>
    /// Represent the error response by API
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
