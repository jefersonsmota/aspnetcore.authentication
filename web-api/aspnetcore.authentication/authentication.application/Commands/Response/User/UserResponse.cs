using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace authentication.application.Commands.User
{
    public class UserResponse
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }

        [JsonPropertyName("lastname")]
        public string LastName { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("last_login")]
        public DateTime LastLogin { get; set; }

        [JsonPropertyName("phones")]
        public IEnumerable<string> Phones { get; set; }
    }
}
