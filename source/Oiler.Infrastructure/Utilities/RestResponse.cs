using System.Net;

namespace Oiler.Infrastructure.Utilities
{
    public class RestResponse
    {
        public bool IsSuccess { get; set; }
        public HttpStatusCode Status { get; set; }
    }

    public sealed class RestResponse<TContent> : RestResponse
    {
        public TContent Content { get; set; }
    }
}