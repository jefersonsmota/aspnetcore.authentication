using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace authentication.application.Commands.User
{
    public class SingInUserRequest
    {
        [JsonPropertyName("email")]
        [Required(ErrorMessage = "Missing fields")]
        [EmailAddress(ErrorMessage = "Invalid fields")]
        [MaxLength(150, ErrorMessage = "Invalid fields")]
        public string Email { get; set; }

        [JsonPropertyName("password")]
        [Required(ErrorMessage = "Missing fields")]
        public string Password { get; set; }
    }
}
