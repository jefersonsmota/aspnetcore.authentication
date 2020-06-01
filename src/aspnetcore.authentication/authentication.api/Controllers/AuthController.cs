using authentication.api.Configurations;
using authentication.api.Services;
using authentication.api.ViewModels;
using authentication.application.Commands.User;
using authentication.application.Common;
using authentication.application.Handlers.Interfaces;
using authentication.domain.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace authentication.api.Controllers
{
    /// <summary>
    /// Api de autenticação.
    /// </summary>
    [Route("/")]
    [ApiController]
    public class AuthController : ApiController
    {
        private readonly AppSettings _appSettings;

        public AuthController(AppSettings appSettings, NotificationContext notificationContext) : base(notificationContext)
        {
            _appSettings = appSettings;
        }

        /// <summary>
        /// Efetua o cadastro de um novo usuário
        /// </summary>
        /// <param name="userCommandHandler">Command handler para efetuar a criação do novo usuário. Obtido pelo serviço</param>
        /// <param name="createUserRequest">Objeto com dados de criação de usuário.</param>
        /// <returns>Mesagem e codigo do resultado</returns>
        [HttpPost]
        [Route("signup")]
        [AllowAnonymous]
        public async Task<IActionResult> SignUp([FromServices] IUserCommandHandler userCommandHandler, [FromBody] CreateUserRequest createUserRequest)
        {
            var response = await userCommandHandler.Handler(createUserRequest);
            return Response(response);
        }

        /// <summary>
        /// Efetua a autenticação do usuário
        /// </summary>
        /// <param name="userCommandHandler">Command handler para execução do comando de sign in. Obtido pelo serviço.</param>
        /// <param name="singIn">Objeto com parametros do login: Email e Password.</param>
        /// <returns>Credenciais de autenticação</returns>
        [HttpPost]
        [Route("signin")]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn([FromServices] IUserCommandHandler userCommandHandler, [FromBody] SingInUserRequest singIn)
        {
            if (ModelState.IsValid)
            {
                var user = await userCommandHandler.Handler(singIn);

                if (user.Error) return Response(user);

                var response = TokenService.GereneteToken((UserResponse)user.Data, _appSettings);

                return Response(new CommandResponse(user.StatusCode, user.Message, response));
            }

            return BadRequest();
        }

        /// <summary>
        /// Obtem informações do usuário autenticado
        /// </summary>
        /// <param name="userQueryHandler">Query handler de obtenção de usuário. Obtido pelo serviço</param>
        /// <param name="login">Login do usuário. Obtido pelo header</param>
        /// <returns>Informações do usuário</returns>
        [HttpGet]
        [Route("me")]
        [Authorize]
        public async Task<IActionResult> Me([FromServices] IUserQueryHandler userQueryHandler, [FromHeader] string login)
        {
            var user = await userQueryHandler.Handler(login);
            return Response(user);
        }
    }
}