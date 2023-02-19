using Icarus.Application.Abstractions.Cryptography;
using Icarus.Application.Abstractions.Data;
using Icarus.Application.Members.RegisterMember;
using Icarus.Domain.Entities;
using Icarus.Domain.Errors;
using Icarus.Domain.Repositories;
using Icarus.Domain.Shared;
using Icarus.Domain.ValueObjects;

namespace Icarus.Application.UnitTests.Members.Commands;

public class RegisterMemberCommandHandlerTest
{
    private readonly Mock<IMemberRepository> _memberRepositoryMock;

    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IPasswordHasher> _passwordHasher;
    public RegisterMemberCommandHandlerTest(Mock<IPasswordHasher> passwordHasher)
    {
        _memberRepositoryMock = new();
        _unitOfWorkMock = new();
        _passwordHasher = passwordHasher;
    }


    [Fact]
    public async void Handle_Should_ReturnSuccessResult_WhenEmailIsUnique()
    {
        // Arrange
        var command = new RegisterMemberCommand(
            "email@test.com",
            "123",
            "123",
            "first",
            "last");

        _memberRepositoryMock.Setup(
            x => x.IsEmailUniqueAsync(
                It.IsAny<Email>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var handler = new RegisterMemberCommandHandler(
            _memberRepositoryMock.Object,
            _unitOfWorkMock.Object,
            _passwordHasher.Object);
        // Act
        Result<Guid> result = await handler.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async void Handle_Should_ReturnFailureResult_WhenEmailIsNotUnique()
    {
        // Arrange
        var command = new RegisterMemberCommand(
            "email@test.com",
            "123",
            "123",
            "first",
            "last");

        _memberRepositoryMock.Setup(
            x => x.IsEmailUniqueAsync(
                It.IsAny<Email>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var handler = new RegisterMemberCommandHandler(
            _memberRepositoryMock.Object,
            _unitOfWorkMock.Object,
            _passwordHasher.Object);
        // Act
        Result<Guid> result = await handler.Handle(command, default);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainError.ErrorAuthentication.DuplicateEmail);
    }

    [Fact]
    public async void Handle_Should_AddOnRepository_WhenEmailIsUnique()
    {
        // Arrange
        var command = new RegisterMemberCommand(
            "email@test.com",
            "123",
            "123",
            "first",
            "last");

        _memberRepositoryMock.Setup(
            x => x.IsEmailUniqueAsync(
                It.IsAny<Email>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var handler = new RegisterMemberCommandHandler(
            _memberRepositoryMock.Object,
            _unitOfWorkMock.Object,
            _passwordHasher.Object);
        // Act
        Result<Guid> result = await handler.Handle(command, default);

        // Assert
        _memberRepositoryMock.Verify(
            x => x.Add(It.Is<Member>(m => m.Id == result.Value)),
            Times.Once);
    }

    [Fact]
    public async void Handle_Should_Not_CallUnitOfWork_WhenEmailIsNotUnique()
    {
        // Arrange
        var command = new RegisterMemberCommand(
            "email@test.com", 
            "123", 
            "123", 
            "first", 
            "last");

        _memberRepositoryMock.Setup(
            x => x.IsEmailUniqueAsync(
                It.IsAny<Email>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var handler = new RegisterMemberCommandHandler(
            _memberRepositoryMock.Object,
            _unitOfWorkMock.Object,
            _passwordHasher.Object);
        // Act
        Result<Guid> result = await handler.Handle(command, default);

        // Assert
        _unitOfWorkMock.Verify(
            x => x.SaveChangeAsync(It.IsAny<CancellationToken>()),
            Times.Never);
    }
}
