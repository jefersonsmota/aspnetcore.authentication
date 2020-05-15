using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace authentication.application.Commands.Phone
{
    public class CreatePhoneRequest
    {
        [JsonPropertyName("number")]
        [Required(ErrorMessage = "Number is required")]
        [Range(minimum: 100000000, maximum: 999999999, ErrorMessage = "Number doesn't match")]
        public int Number { get; set; }

        [JsonPropertyName("area_code")]
        [Required(ErrorMessage = "Area code is required")]
        [Range(minimum: 10, maximum: 99, ErrorMessage = "Area code doesn't match")]
        public int AreaCode { get; set; }

        [JsonPropertyName("country_code")]
        [Required(ErrorMessage = "Country code is required")]
        [MinLength(3, ErrorMessage = "Minimum 3 characters")]
        public string CountryCode { get; set; }
    }
}
