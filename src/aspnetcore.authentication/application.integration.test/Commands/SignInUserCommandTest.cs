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
        public async Task ShouldRequireFieldsTest()
        {
            _mockRepository.Setup(x => x.GetByEmail(It.IsAny<string>())).Returns(Task.FromResult<User>(null));

            var singInUserRequest = new SingInUserRequest()
            {
                Email = "",
                Password = "12345678"
            };

            var command = new UserCommandHandler(_mockRepository.Object, _mockMapper, _mockNotificationContext);

            var result = await command.Handler(singInUserRequest);

            Assert.IsFalse(result.Success);
            Assert.IsTrue(result.Error);
            Assert.AreEqual(400, result.StatusCode);
            Assert.AreEqual(Messages.MISSING_FIELDS, result.Message);
        }

        [Test]
        public async Task ShouldInvalidFieldsTest()
        {
            _mockRepository.Setup(x => x.GetByEmail(It.IsAny<string>())).Returns(Task.FromResult<User>(null));

            var singInUserRequest = new SingInUserRequest()
            {
                Email = "Email.com",
                Password = "12345678"
            };

            var command = new UserCommandHandler(_mockRepository.Object, _mockMapper, _mockNotificationContext);

            var result = await command.Handler(singInUserRequest);

            Assert.IsFalse(result.Success);
            Assert.IsTrue(result.Error);
            Assert.AreEqual(404, result.StatusCode);
            Assert.AreEqual(Messages.INVALID_EMAIL_OR_PASSWORD, result.Message);
        }

        [Test]
        public async Task ShouldInvalidUserFieldsTest()
        {
            _mockRepository.Setup(x => x.GetByEmail(It.IsAny<string>())).Returns(Task.FromResult<User>(null));

            var singInUserRequest = new SingInUserRequest()
            {
                Email = "email@other.com",
                Password = "12345678"
            };

            var command = new UserCommandHandler(_mockRepository.Object, _mockMapper, _mockNotificationContext);

            var result = await command.Handler(singInUserRequest);

            Assert.IsFalse(result.Success);
            Assert.IsTrue(result.Error);
            Assert.AreEqual(404, result.StatusCode);
            Assert.AreEqual(Messages.INVALID_EMAIL_OR_PASSWORD, result.Message);
        }
    }
}
