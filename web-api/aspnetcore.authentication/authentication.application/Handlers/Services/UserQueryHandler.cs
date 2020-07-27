using authentication.application.Commands.Request.Playlist;
using authentication.application.Commands.Request.User;
using authentication.application.Commands.User;
using authentication.application.Common;
using authentication.application.Handlers.Interfaces;
using authentication.domain.Constants;
using authentication.domain.Notifications;
using authentication.infrastructure.Interfaces;
using AutoMapper;
using Newtonsoft.Json;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace authentication.application.Handlers.Services
{
    public class UserQueryHandler : CommandHandler, IUserQueryHandler
    {
        private readonly IUserRespository _userRepository;
        public UserQueryHandler(IUserRespository userRepository, IMapper mapper, NotificationContext notificationContext) : base(mapper, notificationContext)
        {
            _userRepository = userRepository;
        }

        public async Task<CommandResponse> Handler(MeRequest me)
        {
            if (string.IsNullOrWhiteSpace(me.Login))
            {
                _notificationContext.AddNotification("Login", Messages.MISSING_FIELDS);
                return BadRequest(me.Login, Messages.MISSING_FIELDS);
            }

            var user = await _userRepository.GetByEmail(me.Login);
            if (user == null)
            {
                _notificationContext.AddNotification("Login", Messages.NOT_FOUND_USER);
                return NotFound(me.Login, Messages.NOT_FOUND_USER);
            }

            return Ok(_mapper.Map<UserResponse>(user), null);
        }

        public async Task<CommandResponse> Handler(WeatherPlaylistRequest weatherPlaylist)
        {
            if (string.IsNullOrWhiteSpace(weatherPlaylist.Email))
            {
                _notificationContext.AddNotification("User", Messages.MISSING_FIELDS);
                return BadRequest(weatherPlaylist.Email, Messages.MISSING_FIELDS);
            }

            var user = await _userRepository.GetByEmail(weatherPlaylist.Email);
            if (user == null)
            {
                _notificationContext.AddNotification("User", Messages.NOT_FOUND_USER);
                return NotFound(weatherPlaylist.Email, Messages.NOT_FOUND_USER);
            }

            WeatherCityFindResponse cities = null;
            City city = null;

            var httpClient = new HttpClient();
            var result = await httpClient.GetAsync("https://openweathermap.org/data/2.5/find?q=Vit%C3%B3ria,BR&appid=439d4b804bc8187953eb36d2a8c26a02&units=metric");
            if(result.IsSuccessStatusCode)
            {
                cities = JsonConvert.DeserializeObject<WeatherCityFindResponse>(await result.Content.ReadAsStringAsync());
            }

            result = await httpClient.GetAsync($"https://openweathermap.org/data/2.5/weather?id={cities.list.FirstOrDefault().id}&units=metric&appid=439d4b804bc8187953eb36d2a8c26a02");
            if (result.IsSuccessStatusCode)
            {
                city = JsonConvert.DeserializeObject<City>(await result.Content.ReadAsStringAsync());
            }

            return Ok(city, null);
        }
    }
}
