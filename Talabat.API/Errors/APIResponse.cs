
namespace Talabat.API.Errors
{
    public class APIResponse
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }

        public APIResponse(int StatusCode, string? Message = null)
        {
            this.StatusCode = StatusCode;
            this.Message = Message ?? GetDefaultMessageForStatusCode(StatusCode);
        }

        private string? GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "Not Found",
                401 => "Unauthorized",
                404 => "Resource wasn't found",
                500 => "500 Error",
                _ => null
            };
        }
    }
}
