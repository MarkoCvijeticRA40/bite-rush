using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Users.Register;
using Domain.Users;
using FluentAssertions;
using Moq;
using Moq.EntityFrameworkCore;
using SharedKernel;
using UnitTests.Fixtures;

namespace UnitTests.Endpoints.Users.Commands;

public class RegisterUserCommandHandlerTests
{
    private readonly Mock<IApplicationDbContext> _dbContext;
    private readonly Mock<IPasswordHasher> _passwordHasher;
    private readonly Mock<IEventBus> _eventBus;
    private readonly List<User> users;

    public RegisterUserCommandHandlerTests()
    {
        _dbContext = new();
        _passwordHasher = new();
        _eventBus = new();
        users = UserFixtures.GetUsers();
        _dbContext.Setup(db => db.Users).ReturnsDbSet(users);
    }

    [Fact]
    public async Task Handle_Should_ReturnOkResult_WhenEmailIsUnique()
    {
        // Arrange
        var command = new RegisterUserCommand("markocv2023@gmail.com", "Marko", "Cvijetic", "#stabw124");
        var handler = new RegisterUserCommandHandler(_dbContext.Object, _passwordHasher.Object, _eventBus.Object);

        // Act
        Result<Guid> result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Value.Should().NotBeEmpty();
    }

    [Fact]
    public async Task Handle_Should_ReturnFailureResult_WhenEmailIsNotUnique()
    {
        // Arrange
        var command = new RegisterUserCommand("johndoe@microsoft.com", "John", "Doe", "#johndoe1234");
        var handler = new RegisterUserCommandHandler(_dbContext.Object, _passwordHasher.Object, _eventBus.Object);

        // Act
        Result<Guid> result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Error.Should().Be(UserErrors.EmailNotUnique);
    }

    [Fact]
    public async Task Handle_Should_CallSaveChangesAsync_WhenEmailIsUnique()
    {
        // Arrange
        var command = new RegisterUserCommand("markocv2023@gmail.com", "John", "Doe", "#johndoe1234");
        var handler = new RegisterUserCommandHandler(_dbContext.Object, _passwordHasher.Object, _eventBus.Object);

        // Act
        Result<Guid> result = await handler.Handle(command, CancellationToken.None);

        // Assert
        _dbContext.Verify(db => db.SaveChangesAsync(CancellationToken.None), Times.Once);
    }
}
