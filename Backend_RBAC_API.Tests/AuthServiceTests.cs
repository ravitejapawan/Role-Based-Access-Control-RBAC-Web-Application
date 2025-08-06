using Backend_RBAC_API.Controllers;
using Backend_RBAC_API.Models.DTOs;
using Backend_RBAC_API.Models.Entities;
using Backend_RBAC_API.Repositories.Interfaces;
using Backend_RBAC_API.Services.Implementations;
using Backend_RBAC_API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Backend_RBAC_API.Tests
{
    public class AuthServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly IConfiguration _configuration;
        private readonly AuthService _authService;
        private readonly Mock<IAuthService> _authServiceMock;
        private readonly Mock<ILogger<AuthController>> _loggerMock;
        private readonly AuthController _controller;

        public AuthServiceTests()
        {
            _authServiceMock = new Mock<IAuthService>();
            _loggerMock = new Mock<ILogger<AuthController>>();
            _controller = new AuthController(_authServiceMock.Object, _loggerMock.Object);
            _userRepositoryMock = new Mock<IUserRepository>();

            var inMemorySettings = new Dictionary<string, string>
                    {
                        { "Jwt:Key", "ThisIsASecretKeyForJwtToken12345678" }, // Base64 key
                        { "Jwt:Issuer", "https://localhost:7254" },
                        { "Jwt:Audience", "https://localhost:7254" }
                    };
            _configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            _authService = new AuthService(_userRepositoryMock.Object, _configuration);
        }

        [Fact]
        public void Authenticate_ValidUser_ReturnsToken()
        {
            // Arrange
            var user = new User { Username = "admin", Role = "Admin" };
            _userRepositoryMock.Setup(repo => repo.ValidateUser(It.IsAny<string>(), It.IsAny<string>()))
                               .Returns(user);

            var request = new LoginRequest { Username = "admin", Password = "password" };

            // Act
            var result = _authService.Authenticate(request);

            // Assert
            Xunit.Assert.NotNull(result);
            Xunit.Assert.False(string.IsNullOrEmpty(result.Token));
            Xunit.Assert.Equal("Admin", result.Role);
        }

        [Fact]
        public void Authenticate_InvalidUser_ReturnsNull()
        {
            // Arrange
            _userRepositoryMock.Setup(repo => repo.ValidateUser(It.IsAny<string>(), It.IsAny<string>()))
                               .Returns((User)null);

            var request = new LoginRequest { Username = "invalid", Password = "wrong" };

            // Act
            var result = _authService.Authenticate(request);

            // Assert
            Xunit.Assert.Null(result);
        }

        [Fact]
        public async Task Register_ValidRequest_ReturnsCreated()
        {
            // Arrange
            var request = new RegisterRequest
            {
                Username = "editor",
                Password = "editor123",
                Role = "Editor"
            };

            _authServiceMock.Setup(service => service.RegisterUserAsync(It.IsAny<User>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Register(request);

            // Assert
            var createdResult = Xunit.Assert.IsType<CreatedAtActionResult>(result);
            Xunit.Assert.Equal("User registered successfully", createdResult.Value);
        }



        [Fact]
        public async Task Register_InvalidModel_ReturnsBadRequest()
        {
            // Arrange
            _controller.ModelState.AddModelError("Username", "Required");

            var request = new RegisterRequest();

            // Act
            var result = await _controller.Register(request);

            // Assert
            Xunit.Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
