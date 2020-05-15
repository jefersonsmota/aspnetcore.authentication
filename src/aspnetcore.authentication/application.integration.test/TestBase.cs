using authentication.application.Common.Mappers;
using authentication.application.Handlers.Interfaces;
using authentication.application.Handlers.Services;
using authentication.infrastructure.Interfaces;
using AutoMapper;
using Moq;
using NUnit.Framework;

namespace application.integration.test
{
     /// <summary>
     /// Classe base para mocks dos demais testes. 
     /// Repository e ValidationHandler
     /// </summary>
    public class TestBase
    {
        protected Mock<IUserRespository> _mockRepository;
        protected IUserValidationHandler _mockValidation;
        protected IMapper _mockMapper;

        [SetUp]
        public void RunBeforeAnyTests()
        {

            _mockRepository = new Mock<IUserRespository>();
            _mockValidation = new UserValidationHandler();

            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });

            _mockMapper = mapperConfiguration.CreateMapper();

        }
    }
}