using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DiigoSharp.ApiClient.Exceptions
{
    public class DiigoClientException : Exception
    {
        public DiigoClientException(HttpStatusCode statusCode, string message)
            : this(statusCode, message, null)
        {
        }

        public DiigoClientException(HttpStatusCode statusCode, string message, Exception? innerException)
            : base(message, innerException)
        {
            this.StatusCode = statusCode;
            Data.Add("StatusCode", statusCode);
        }

        public HttpStatusCode StatusCode { get; }
    }
}
