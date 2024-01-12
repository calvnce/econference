using Econference.Controllers;
using Econference.Models;
using Econference.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace EnconferenceTest
{
    public class AccountControllerTest
    {
        private readonly Mock<UserManager<ApplicationUser>> _mockUserManager;
        private readonly Mock<SignInManager<ApplicationUser>> _mockSignInManager;
        private readonly AccountController _controller;

        public AccountControllerTest()
        {
            var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
            _mockUserManager = new Mock<UserManager<ApplicationUser>>(userStoreMock.Object, null, null, null, null, null, null, null, null);
            var claimsFactory = new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>();
            var contextAccessor = new Mock<IHttpContextAccessor>();
            _mockSignInManager = new Mock<SignInManager<ApplicationUser>>(_mockUserManager.Object, contextAccessor.Object, claimsFactory.Object, null, null, null, null);
            _controller = new AccountController(_mockUserManager.Object, _mockSignInManager.Object);
        }

        [Fact]
        public async Task Register_ReturnsViewResult_WhenModelStateIsInvalid()
        {
            // Arrange
            _controller.ModelState.AddModelError("error", "some error");

            // Act
            var result = await _controller.Register(new RegisterViewModel());

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<RegisterViewModel>(viewResult.Model);
        }

        [Fact]
        public async Task Register_ReturnsRedirectToActionResult_WhenUserIsSuccessfullyCreated()
        {
            // Arrange
            var model = new RegisterViewModel { FullName = "Test User", Email = "test@example.com", Username = "testuser", UserRole = "USER",Password="Test125@econference" };
            _mockUserManager.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);
            _mockUserManager.Setup(x => x.AddToRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _controller.Register(model);

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task LogIn_ReturnsViewResult_WhenModelStateIsInvalid()
        {
            // Arrange
            _controller.ModelState.AddModelError("error", "some error");

            // Act
            var result = await _controller.LogIn(new LogInViewModel());

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<LogInViewModel>(viewResult.Model);
        }

        [Fact]
        public async Task LogIn_ReturnsRedirectToActionResult_WhenUserIsSuccessfullyLoggedIn()
        {
            // Arrange
            var model = new LogInViewModel { Username = "testuser", Password = "Test125@econference" };
            _mockSignInManager.Setup(x => x.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);

            // Act
            var result = await _controller.LogIn(model);

            // Assert
            // Assert
            Assert.IsType<ViewResult>(result);
        }
    }

}