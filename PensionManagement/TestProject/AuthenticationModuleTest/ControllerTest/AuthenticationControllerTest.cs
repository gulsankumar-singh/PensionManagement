using AuthenticationModule.Controllers;
using AuthenticationModule.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace AuthenticationModuleTest.ControllersTest
{
    [TestFixture]
    public class AuthenticationControllerTest
    {
        private readonly IConfiguration _configuration;
        private readonly Mock<ILogger<AuthenticationController>> _loggerMock;

        public AuthenticationControllerTest()
        {
           _configuration = AuthenticationModuleConfig.GetConfiguration();
            _loggerMock = new Mock<ILogger<AuthenticationController>>();
        }

        [Test]
        public void GetAuthenticationToken_ReturnsTokenDetail()
        {
            //Arrange
            var user = new User()
            {
                UserName = "AppUser",
                Password = "Pass@123"
            };

            //Act
            var authenticationController = new AuthenticationController(_configuration, _loggerMock.Object);
            var actionResult = authenticationController.GetAuthenticationToken(user);
            var actionResultValue = (APIResponse)((OkObjectResult)actionResult.Result).Value;
            var tokenDetail = (TokenDetail)actionResultValue.Response;

            //Assert
            Assert.IsNotNull(authenticationController);
            Assert.IsNotNull(actionResult);
            Assert.AreEqual("Success", actionResultValue.Status);
            Assert.AreEqual("Token Generated Successfully", actionResultValue.Message);
            Assert.IsNotNull(actionResultValue.Response);
            Assert.IsInstanceOf<TokenDetail>(actionResultValue.Response);
            Assert.IsNotEmpty(tokenDetail.Token);
            Assert.Greater(tokenDetail.Expiration, new System.DateTime());
        }

        [Test]
        public void GetAuthenticationToken_NotReturnToken_WhenBadUserName()
        {
            //Arrange
            var user = new User()
            {
                UserName = "User",
                Password = "Pass@123"
            };

            //Act
            var authenticationController = new AuthenticationController(_configuration, _loggerMock.Object);
            var actionResult = authenticationController.GetAuthenticationToken(user);
            var actionResultValue = (APIResponse)((BadRequestObjectResult)actionResult.Result).Value;

            //Assert
            Assert.IsNotNull(authenticationController);
            Assert.IsNotNull(actionResult);
            Assert.AreEqual("Error", actionResultValue.Status);
            Assert.AreEqual("Username or password is incorrect", actionResultValue.Message);
            Assert.IsNull(actionResultValue.Response);
        }

        [Test]
        public void GetAuthenticationToken_NotReturnToken_WhenBadPassword()
        {
            //Arrange
            var user = new User()
            {
                UserName = "AppUser",
                Password = "Password"
            };

            //Act
            var authenticationController = new AuthenticationController(_configuration, _loggerMock.Object);
            var actionResult = authenticationController.GetAuthenticationToken(user);
            var actionResultValue = (APIResponse)((BadRequestObjectResult)actionResult.Result).Value;

            //Assert
            Assert.IsNotNull(authenticationController);
            Assert.IsNotNull(actionResult);
            Assert.AreEqual("Error", actionResultValue.Status);
            Assert.AreEqual("Username or password is incorrect", actionResultValue.Message);
            Assert.IsNull(actionResultValue.Response);
        }

        [Test]
        public void GetAuthenticationToken_NotReturnToken_WhenBadUserNameAndPassword()
        {
            //Arrange
            var user = new User()
            {
                UserName = "User",
                Password = "Password"
            };

            //Act
            var authenticationController = new AuthenticationController(_configuration, _loggerMock.Object);
            var actionResult = authenticationController.GetAuthenticationToken(user);
            var actionResultValue = (APIResponse)((BadRequestObjectResult)actionResult.Result).Value;

            //Assert
            Assert.IsNotNull(authenticationController);
            Assert.IsNotNull(actionResult);
            Assert.AreEqual("Error", actionResultValue.Status);
            Assert.AreEqual("Username or password is incorrect", actionResultValue.Message);
            Assert.IsNull(actionResultValue.Response);
        }
    }
}
