using System.Net;

namespace Digger.Infra.Diigo.Exceptions
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
