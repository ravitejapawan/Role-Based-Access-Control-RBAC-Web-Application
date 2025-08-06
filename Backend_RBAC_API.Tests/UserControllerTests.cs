using Xunit;
using Moq;
using Backend_RBAC_API.Controllers;
using Backend_RBAC_API.Services.Interfaces;
using Backend_RBAC_API.Models.DTOs;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Backend_RBAC_API.Tests
{
    public class UserManagerControllerTests
    {
        private readonly Mock<IAuthService> _authServiceMock;
        private readonly Mock<ILogger<UserManagerController>> _loggerMock;
        private readonly UserManagerController _controller;

        public UserManagerControllerTests()
        {
            _authServiceMock = new Mock<IAuthService>();
            _loggerMock = new Mock<ILogger<UserManagerController>>();
            _controller = new UserManagerController(_authServiceMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task ChangeUserRole_ValidUser_ReturnsOk()
        {
            var request = new ChangeUserRoleRequest { Username = "editor", NewRole = "Admin" };
            _authServiceMock.Setup(s => s.ChangeUserRoleAsync(request.Username, request.NewRole)).ReturnsAsync(true);

            var result = await _controller.ChangeUserRole(request);

            var okResult = Xunit.Assert.IsType<OkObjectResult>(result);
            Xunit.Assert.Contains("updated to Admin", okResult.Value.ToString());
        }

        [Fact]
        public async Task ChangeUserRole_UserNotFound_ReturnsNotFound()
        {
            var request = new ChangeUserRoleRequest { Username = "unknown", NewRole = "Editor" };
            _authServiceMock.Setup(s => s.ChangeUserRoleAsync(request.Username, request.NewRole)).ReturnsAsync(false);

            var result = await _controller.ChangeUserRole(request);

            Xunit.Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task ChangeUserRole_InvalidRole_ReturnsBadRequest()
        {
            var request = new ChangeUserRoleRequest { Username = "editor", NewRole = "InvalidRole" };
            _authServiceMock.Setup(s => s.ChangeUserRoleAsync(request.Username, request.NewRole))
                .ThrowsAsync(new ArgumentException("Invalid role"));

            var result = await _controller.ChangeUserRole(request);

            Xunit.Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
