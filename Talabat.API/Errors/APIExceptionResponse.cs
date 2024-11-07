using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Talabat.API.Errors
{
    public class APIExceptionResponse : APIResponse
    {
        public string? Description { get; set; }
        public APIExceptionResponse(int StatusCode, string? Message = null, string? Desacription = null) : base(StatusCode, Message)
        {
            this.Description = Desacription;
        }
    }
}
