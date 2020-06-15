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

            Assert.IsFalse(result.Success);
            Assert.IsTrue(result.Error);
            Assert.AreEqual(400, result.StatusCode);
            Assert.AreEqual(Messages.INVALID_FIELDS, result.Message);
        }

        [Test]
        public async Task ShouldInvalidFieldsTest()
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

            var result = await command.Handler(createUserRequest);

            Assert.IsFalse(result.Success);
            Assert.IsTrue(result.Error);
            Assert.AreEqual(400, result.StatusCode);
            Assert.AreEqual(Messages.INVALID_FIELDS, result.Message);
        }
    
        [Test]
        public async Task ShouldEmailAlreadyExistTest()
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

            var result = await command.Handler(createUserRequest);

            Assert.IsFalse(result.Success);
            Assert.IsTrue(result.Error);
            Assert.AreEqual(400, result.StatusCode);
            Assert.AreEqual(Messages.INVALID_FIELDS, result.Message);
            Assert.IsTrue(_mockNotificationContext.HasNotifications);
        }
    }

    
}
