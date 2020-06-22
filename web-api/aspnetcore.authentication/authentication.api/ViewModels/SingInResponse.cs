using System;

namespace authentication.api.ViewModels
{
    public class SingInResponse
    {
        public object Sub { get; set; }
        public string Token { get; set; }
        public DateTime ExpirationToken { get; set; }
    }
}
