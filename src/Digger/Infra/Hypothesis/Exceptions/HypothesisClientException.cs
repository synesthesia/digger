using System.Net;

namespace Digger.Infra.Hypothesis.Exceptions
{
    public class HypothesisClientException : Exception
    {
        public HypothesisClientException(HttpStatusCode statusCode, string message)
            : this(statusCode, message, null)
        {
        }

        public HypothesisClientException(HttpStatusCode statusCode, string message, Exception? innerException)
            : base(message, innerException)
        {
            this.StatusCode = statusCode;
            Data.Add("StatusCode", statusCode);
        }

        public HttpStatusCode StatusCode { get; }
    }

}
