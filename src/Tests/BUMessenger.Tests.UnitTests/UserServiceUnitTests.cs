using BUMeesenger.Domain.Exceptions.Services.UnregisteredUserExceptions;
using BUMeesenger.Domain.Exceptions.Services.UserServiceExceptions;
using BUMessenger.Application.Services.Services;
using BUMessenger.Domain.Interfaces.Repositories;
using BUMessenger.Domain.Interfaces.Services;
using BUMessenger.Domain.Models.Models.UnregisteredUsers;
using BUMessenger.Domain.Models.Models.Users;
using Microsoft.Extensions.Logging;
using Moq;

namespace BUMessenger.Tests.UnitTests
{
    public class UserServiceUnitTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IUnregisteredUserRepository> _unregisteredUserRepositoryMock;
        private readonly Mock<IEmailService> _emailServiceMock;
        private readonly Mock<ILogger<UserService>> _loggerMock;
        private readonly UserService _userService;

        public UserServiceUnitTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _unregisteredUserRepositoryMock = new Mock<IUnregisteredUserRepository>();
            _emailServiceMock = new Mock<IEmailService>();
            _loggerMock = new Mock<ILogger<UserService>>();

            _userService = new UserService(
                _userRepositoryMock.Object,
                _unregisteredUserRepositoryMock.Object,
                _emailServiceMock.Object,
                _loggerMock.Object);
        }

        [Fact]
        public async Task AddUserAsync_WhenUnregisteredUserNotFound_ThrowsException()
        {
            // Arrange
            var userCreate = new UserCreate { Email = "test@example.com", ApproveCode = "12345" };
            _unregisteredUserRepositoryMock.Setup(x => x.FindUnregisteredUserByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync((UnregisteredUserForAddUser)null);

            // Act & Assert
            await Assert.ThrowsAsync<UnregisteredUserNotFoundServiceException>(
                () => _userService.AddUserAsync(userCreate));
        }


        [Fact]
        public async Task AddUserAsync_WhenApproveCodeExpired_ThrowsException()
        {
            // Arrange
            var userCreate = new UserCreate { Email = "test@example.com", ApproveCode = "12345" };
            var unregisteredUser = new UnregisteredUserForAddUser
            {
                Id = Guid.NewGuid(),
                Email = "test@example.com",
                PasswordHashed = "12345",
                ApproveCode = "12345",
                ExpiresAtUtc = DateTime.UtcNow.AddMinutes(-1)
            };

            _unregisteredUserRepositoryMock.Setup(x => x.FindUnregisteredUserByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(unregisteredUser);

            // Act & Assert
            await Assert.ThrowsAsync<UserServiceException>(
                () => _userService.AddUserAsync(userCreate));
        }

        [Fact]
        public async Task AddUserAsync_WhenValidData_ReturnsUser()
        {
            // Arrange
            var userCreate = new UserCreate { Email = "test@example.com", ApproveCode = "12345" };
            var unregisteredUser = new UnregisteredUserForAddUser
            {
                Id = Guid.NewGuid(),
                Email = "test@example.com",
                ApproveCode = "12345",
                ExpiresAtUtc = DateTime.UtcNow.AddHours(1),
                PasswordHashed = "hashed_password"
            };

            var expectedUser = new User { 
                Id = Guid.NewGuid(),
                Name = "Test",
                Surname = "Test",
                Fathername = "Test",
                Email = "test@example.com"
            };

            _unregisteredUserRepositoryMock.Setup(x => x.FindUnregisteredUserByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(unregisteredUser);

            _userRepositoryMock.Setup(x => x.AddUserAsync(It.IsAny<UserCreateWithPassword>()))
                .ReturnsAsync(expectedUser);

            // Act
            var result = await _userService.AddUserAsync(userCreate);

            // Assert
            Assert.Equal(expectedUser, result);
            _unregisteredUserRepositoryMock.Verify(x =>
                x.DeleteUnregisteredUserByEmailAsync(userCreate.Email), Times.Once);
        }
    }
}