using Fluendo.FluendoPlatform.Infrastructure.Authentication.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;

namespace Tests
{
    public class AuthenticationTest
    {
        private TokenController tokenController;
        private const string JwtKey = "Jwt:Key";
        private const string JwtKeyValue = "/A?D(G+KaPdSgVkYp3s6v9y$B&E)H@Mc";
        private const string JwtIssuer = "Jwt:Issuer";
        private const string JwtIssuerValue = "http://localhost:50541/";
        private const string JwtExpiration = "Jwt:Expiration";
        private const string JwtExpirationValue = "1800";

        [SetUp]
        public void Setup()
        {
            var configuration = new Mock<IConfiguration>();

            configuration.SetupGet(x => x[It.Is<string>(s => s == JwtKey)]).Returns(JwtKeyValue);
            configuration.SetupGet(x => x[It.Is<string>(s => s == JwtIssuer)]).Returns(JwtIssuerValue);
            configuration.SetupGet(x => x[It.Is<string>(s => s == JwtExpiration)]).Returns(JwtExpirationValue);

            tokenController = new TokenController(configuration.Object);
        }

        [Test]
        public void Create_Token_ReturnIsNotNull()
        {
            var result = (OkObjectResult)tokenController.CreateToken();

            Assert.IsNotNull(result);
        }

        [Test]
        public void Create_Token_ReturnSucessfull()
        {
            var result = (OkObjectResult)tokenController.CreateToken();

            Assert.AreEqual(result.StatusCode, 200);
        }

        [Test]
        public void Create_Token_ReturnTokenValueIsNotNull()
        {
            var result = (OkObjectResult)tokenController.CreateToken();

            Assert.IsNotNull(result.Value);
        }

        [TearDown]
        public void Reset_TokenController()
        {
            tokenController = null;
        }
    }
}