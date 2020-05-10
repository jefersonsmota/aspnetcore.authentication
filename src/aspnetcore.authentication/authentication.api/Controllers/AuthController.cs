using authentication.api.Configurations;
using authentication.api.Services;
using authentication.api.ViewModels;
using authentication.application.Commands.User;
using authentication.application.Handlers.Interfaces;
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
    public class AuthController : ControllerBase
    {
        private readonly AppSettings _appSettings;

        public AuthController(AppSettings appSettings)
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
        public async Task<ActionResult<dynamic>> SignUp([FromServices] IUserCommandHandler userCommandHandler, [FromBody] CreateUserRequest createUserRequest)
        {
            if (ModelState.IsValid)
            {
                if (await userCommandHandler.Handler(createUserRequest) > 0)
                {
                    return Ok(new { message = "User created", code = 200 });
                }
            }

            return BadRequest();
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
        public async Task<ActionResult<dynamic>> SignIn([FromServices] IUserCommandHandler userCommandHandler, [FromBody] SingInUserRequest singIn)
        {
            if (ModelState.IsValid)
            {
                var user = await userCommandHandler.Handler(singIn);

                var response = TokenService.GereneteToken(user, _appSettings);

                return Ok(response);
            }

            return BadRequest(new DataResponse("Invalid email or password", 400));
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
        public async Task<ActionResult<dynamic>> Me([FromServices] IUserQueryHandler userQueryHandler, [FromHeader] string login)
        {
            var user = await userQueryHandler.Handler(login);
            return Ok(user);
        }
    }
}