using System.Text.Json.Serialization;

namespace authentication.api.ViewModels
{
    public class DataResponse
    {
        [JsonPropertyName("message")]
        public string Message { get; set; }
        
        [JsonPropertyName("errorCode")]
        public int ErrorCode { get; set; }


        public DataResponse(string message, int errorCode)
        {
            Message = message;
            ErrorCode = errorCode;
        }
    }
}
