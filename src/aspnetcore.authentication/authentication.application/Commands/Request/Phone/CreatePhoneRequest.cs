using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace authentication.application.Commands.Phone
{
    public class CreatePhoneRequest
    {
        [JsonPropertyName("number")]
        [Required(ErrorMessage = "Missing fields")]
        public int Number { get; set; }

        [JsonPropertyName("area_code")]
        [Required(ErrorMessage = "Missing fields")]
        public int AreaCode { get; set; }

        [JsonPropertyName("country_code")]
        [Required(ErrorMessage = "Missing fields")]
        public string CountryCode { get; set; }
    }
}
