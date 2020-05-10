using authentication.application.Commands.User;
using authentication.application.Handlers.Services;
using authentication.domain.Constants;
using authentication.domain.Entities;
using authentication.domain.Exceptions;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace application.integration.test.Commands
{
    public class SignInUserCommandTest : TestBase
    {
        [Test]
        public void ShouldRequireFieldsTest()
        {
            _mockRepository.Setup(x => x.GetByEmail(It.IsAny<string>())).Returns(Task.FromResult(new User()));

            var singInUserRequest = new SingInUserRequest()
            {
                Email = "",
                Password = "12345678"
            };

            var command = new UserCommandHandler(_mockRepository.Object, _mockMapper, _mockValidation);

            ValidationException ex = Assert.ThrowsAsync<ValidationException>(async () => await command.Handler(singInUserRequest));

            Assert.That(ex.Message, Is.EqualTo(Messages.MISSING_FIELDS));
        }

        [Test]
        public void ShouldInvalidFieldsTest()
        {
            _mockRepository.Setup(x => x.GetByEmail(It.IsAny<string>())).Returns(Task.FromResult(new User()));

            var singInUserRequest = new SingInUserRequest()
            {
                Email = "Email.com",
                Password = "12345678"
            };

            var command = new UserCommandHandler(_mockRepository.Object, _mockMapper, _mockValidation);

            ValidationException ex = Assert.ThrowsAsync<ValidationException>(async () => await command.Handler(singInUserRequest));

            Assert.That(ex.Message, Is.EqualTo(Messages.INVALID_FIELDS));
        }

        [Test]
        public void ShouldInvalidUserFieldsTest()
        {
            _mockRepository.Setup(x => x.GetByEmail(It.IsAny<string>())).Returns(Task.FromResult<User>(null));

            var singInUserRequest = new SingInUserRequest()
            {
                Email = "email@other.com",
                Password = "12345678"
            };

            var command = new UserCommandHandler(_mockRepository.Object, _mockMapper, _mockValidation);

            NotFoundException ex = Assert.ThrowsAsync<NotFoundException>(async () => await command.Handler(singInUserRequest));

            Assert.That(ex.Message, Is.EqualTo(Messages.INVALID_EMAIL_OR_PASSWORD));
        }
    }
}
