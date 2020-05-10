using authentication.application.Commands.Phone;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace authentication.application.Commands.User
{
    public class CreateUserRequest
    {
        [JsonPropertyName("firstName")]
        [Required(ErrorMessage = "Missing fields")]
        [MinLength(2, ErrorMessage = "Invalid fields")]
        [MaxLength(150, ErrorMessage = "Invalid fields")]
        public string FirstName { get; set; }

        [JsonPropertyName("lastname")]
        [Required(ErrorMessage = "Missing fields")]
        [MinLength(2, ErrorMessage = "Invalid fields")]
        [MaxLength(150, ErrorMessage = "Invalid fields")]
        public string LastName { get; set; }

        [JsonPropertyName("email")]
        [Required(ErrorMessage = "Missing fields")]
        [EmailAddress(ErrorMessage = "Invalid fields")]
        [MaxLength(150, ErrorMessage = "Invalid fields")]
        public string Email { get; set; }

        [JsonPropertyName("password")]
        [Required(ErrorMessage = "Missing fields")]
        [MinLength(8, ErrorMessage = "Invalid fields")]
        [MaxLength(16, ErrorMessage = "Invalid fields")]
        public string Password { get; set; }

        [JsonPropertyName("phones")]
        [Required(ErrorMessage = "Missing fields")]
        [MinLength(1, ErrorMessage = "Invalid fields")]
        public IEnumerable<CreatePhoneRequest> Phones { get; set; }
    }
}
