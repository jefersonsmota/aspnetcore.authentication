namespace authentication.application.Common
{
    public class CommandResponse
    {
        public bool Success { get; set; }
        public bool Error { get { return !Success;  } }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public object Notifications { get; set; }

        public CommandResponse(object data)
        {
            this.Data = data;
        }

        public CommandResponse(int statusCode, string message, object data, bool success = true)
        {
            this.StatusCode = statusCode;
            this.Message = message;
            this.Data = data;
            this.Success = success;
        }
    }
}
