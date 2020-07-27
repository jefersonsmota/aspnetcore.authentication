using authentication.application.Commands.Request.Playlist;
using authentication.application.Commands.Request.User;
using authentication.application.Common;
using System.Threading.Tasks;

namespace authentication.application.Handlers.Interfaces
{
    public interface IUserQueryHandler
    {
        Task<CommandResponse> Handler(MeRequest login);
        Task<CommandResponse> Handler(WeatherPlaylistRequest weatherPlaylist);
    }
}
