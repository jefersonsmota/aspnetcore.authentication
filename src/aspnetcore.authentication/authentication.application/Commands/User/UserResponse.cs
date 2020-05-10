using System;
using System.Collections.Generic;

namespace authentication.application.Commands.User
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastLogin { get; set; }
        public IEnumerable<string> Phones { get; set; }
    }
}
