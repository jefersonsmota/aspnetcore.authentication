using authentication.application.Commands.Phone;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using System.Text.Json.Serialization;

namespace authentication.application.Commands.User
{
    public class CreateUserRequest
    {
        [JsonPropertyName("firstName")]
        [Required(ErrorMessage = "First name is required")]
        [MinLength(2, ErrorMessage = "Minimum 2 letters")]
        [MaxLength(150, ErrorMessage = "Maximum 150 letters")]
        public string FirstName { get; set; }

        [JsonPropertyName("lastname")]
        [Required(ErrorMessage = "Last name is required")]
        [MinLength(2, ErrorMessage = "Minimum 2 letters")]
        [MaxLength(150, ErrorMessage = "Maximum 150 letters")]
        public string LastName { get; set; }

        [JsonPropertyName("hometown")]
        [Required(ErrorMessage = "Hometown is required")]
        [MinLength(2, ErrorMessage = "Minimum 2 letters")]
        [MaxLength(150, ErrorMessage = "Maximum 150 letters")]
        public string Hometown { get; set; }

        [JsonPropertyName("email")]
        [Required(ErrorMessage = "E-mail is required")]
        [EmailAddress(ErrorMessage = "E-mail doesn't match")]
        [MaxLength(150, ErrorMessage = "Maximum 150 letters")]
        public string Email { get; set; }

        [JsonPropertyName("password")]
        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Minimum 8 letters")]
        [MaxLength(16, ErrorMessage = "Maximum 16 letters")]
        public string Password { get; set; }

        [JsonPropertyName("phones")]
        [Required(ErrorMessage = "Phone is requered")]
        [MinLength(1, ErrorMessage = "Invalid fields")]
        [ContractVerification(true)]
        public IEnumerable<CreatePhoneRequest> Phones { get; set; }
    }
}
