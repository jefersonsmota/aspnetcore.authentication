using System.Text.Json.Serialization;

namespace authentication.api.ViewModels
{
    public class DataResponse
    {
        [JsonPropertyName("message")]
        public string Message { get; set; }
        
        [JsonPropertyName("errorCode")]
        public int ErrorCode { get; set; }

        [JsonPropertyName("data")]
        public dynamic Data { get; set; }
        
        public DataResponse(string message = "", int errorCode = 0, dynamic data = null)
        {
            Message = message;
            ErrorCode = errorCode;
            Data = data;
        }
    }
}
