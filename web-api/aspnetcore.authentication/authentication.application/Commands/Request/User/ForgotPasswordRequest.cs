using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace authentication.application.Commands.Request.User
{
    public class ForgotPasswordRequest
    {
        [JsonPropertyName("email")]
        [Required(ErrorMessage = "Missing fields")]
        [EmailAddress(ErrorMessage = "Invalid fields")]
        [MaxLength(150, ErrorMessage = "Invalid fields")]
        public string Email { get; set; }
    }
}
