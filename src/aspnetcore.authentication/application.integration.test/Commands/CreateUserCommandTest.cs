using authentication.application.Commands.Phone;
using authentication.application.Commands.User;
using authentication.application.Handlers.Services;
using authentication.domain.Constants;
using authentication.domain.Entities;
using authentication.domain.Exceptions;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace application.integration.test.Commands
{
    public class CreateUserCommandTest : TestBase
    {
        [Test]
        public async Task ShouldRequireFieldsTest()
        {
            _mockRepository.Setup(x => x.Add(It.IsAny<User>())).Returns(Task.FromResult(0));

            var createUserRequest = new CreateUserRequest()
            {
                Email = "",
                FirstName = "mock",
                LastName = "mock",
                Password = "12345678",
                Phones = new List<CreatePhoneRequest>()
                {
                    new CreatePhoneRequest() { AreaCode = 11, CountryCode = "+55", Number = 123456789 }
                }
            };

            var command = new UserCommandHandler(_mockRepository.Object, _mockMapper, _mockNotificationContext);

            var result = await command.Handler(createUserRequest);

            Assert.AreEqual(0, result);
        }

        [Test]
        public void ShouldInvalidFieldsTest()
        {
            _mockRepository.Setup(x => x.Add(It.IsAny<User>())).Returns(Task.FromResult(0));

            var createUserRequest = new CreateUserRequest()
            {
                Email = "Email",
                FirstName = "mock",
                LastName = "mock",
                Password = "12345678",
                Phones = new List<CreatePhoneRequest>()
                {
                    new CreatePhoneRequest() { AreaCode = 11, CountryCode = "+55", Number = 123456789 }
                }
            };

            var command = new UserCommandHandler(_mockRepository.Object, _mockMapper, _mockNotificationContext);

            var ex = Assert.ThrowsAsync<ValidationException>(async () => await command.Handler(createUserRequest), Messages.INVALID_FIELDS);

            Assert.That(ex.Message, Is.EqualTo(Messages.INVALID_FIELDS));
        }
    
        [Test]
        public void ShouldEmailAlreadyExistTest()
        {
            _mockRepository.Setup(x => x.CheckAlreadyExist(It.IsAny<string>())).Returns(Task.FromResult(true));

            var createUserRequest = new CreateUserRequest()
            {
                Email = "mock@email.com",
                FirstName = "mock",
                LastName = "mock",
                Password = "12345678",
                Phones = new List<CreatePhoneRequest>()
                {
                    new CreatePhoneRequest() { AreaCode = 11, CountryCode = "+55", Number = 123456789 }
                }
            };

            var command = new UserCommandHandler(_mockRepository.Object, _mockMapper, _mockNotificationContext);

            var ex = Assert.ThrowsAsync<ValidationException>(async () => await command.Handler(createUserRequest));

            Assert.That(ex.Message, Is.EqualTo(Messages.EMAIL_ALREADY_EXISTS));
        }
    }

    
}
