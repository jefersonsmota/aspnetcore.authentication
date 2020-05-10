using System;

namespace authentication.application.Commands.User
{
    public class SingInUserResponse
    {
        public object Sub { get; set; }
        public string Token { get; set; }
        public DateTime ExpirationToken { get; set; }
    }
}
