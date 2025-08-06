using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using Backend_RBAC_API.Controllers;
using Backend_RBAC_API.Repositories.Interfaces;
using Backend_RBAC_API.Models.Entities;
using Backend_RBAC_API.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend_RBAC_API.Tests
{
    public class ContentControllerTests
    {
        private readonly Mock<IContentRepository> _contentRepoMock;
        private readonly Mock<ILogger<ContentController>> _loggerMock;
        private readonly ContentController _controller;

        public ContentControllerTests()
        {
            _contentRepoMock = new Mock<IContentRepository>();
            _loggerMock = new Mock<ILogger<ContentController>>();
            _controller = new ContentController(_contentRepoMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task ViewContent_ReturnsOk_WithList()
        {
            // Arrange
            _contentRepoMock.Setup(repo => repo.GetAllContentAsync())
                .ReturnsAsync(new List<Content>
                {
                    new Content { Id = 1, Title = "Test", Body = "Body" }
                });

            // Act
            var result = await _controller.ViewContent() as OkObjectResult;

            // Assert
            Xunit.Assert.NotNull(result);
            var contents = Xunit.Assert.IsType<List<Content>>(result.Value);
            Xunit.Assert.Single(contents);
        }

        [Fact]
        public async Task EditContent_NotFound_Returns404()
        {
            // Arrange
            _contentRepoMock.Setup(repo => repo.GetContentByIdAsync(1)).ReturnsAsync((Content)null);

            // Act
            var result = await _controller.EditContent(1, new ContentRequest { Title = "New", Body = "Updated" });

            // Assert
            Xunit.Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task DeleteContent_NotFound_Returns404()
        {
            // Arrange
            _contentRepoMock.Setup(repo => repo.GetContentByIdAsync(1)).ReturnsAsync((Content)null);

            // Act
            var result = await _controller.DeleteContent(1);

            // Assert
            Xunit.Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}
