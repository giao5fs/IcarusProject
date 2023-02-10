using Icarus.Application.Members.RegisterMember;
using Icarus.Domain.Entity;
using Icarus.Domain.Errors;
using Icarus.Domain.Repositories;
using Icarus.Domain.Shared;
using Icarus.Domain.ValueObjects;

namespace Icarus.Application.UnitTests.Members.Commands;

public class RegisterMemberCommandHandlerTest
{
    private readonly Mock<IMemberRepository> _memberRepositoryMock;

    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    public RegisterMemberCommandHandlerTest()
    {
        _memberRepositoryMock = new();
        _unitOfWorkMock = new();
    }


    [Fact]
    public async void Handle_Should_ReturnSuccessResult_WhenEmailIsUnique()
    {
        // Arrange
        var command = new RegisterMemberCommand("email@test.com", "first", "last");

        _memberRepositoryMock.Setup(
            x => x.IsEmailUniqueAsync("email@test.com", default))
            .ReturnsAsync(true);

        var handler = new RegisterMemberCommandHandler(
            _memberRepositoryMock.Object,
            _unitOfWorkMock.Object);
        // Act
        Result<Guid> result = await handler.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async void Handle_Should_ReturnFailureResult_WhenEmailIsNotUnique()
    {
        // Arrange
        var command = new RegisterMemberCommand("email@test.com", "first", "last");

        _memberRepositoryMock.Setup(
            x => x.IsEmailUniqueAsync("email@test.com", default))
            .ReturnsAsync(false);

        var handler = new RegisterMemberCommandHandler(
            _memberRepositoryMock.Object,
            _unitOfWorkMock.Object);
        // Act
        Result<Guid> result = await handler.Handle(command, default);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainError.Member.EmailAlreadyInUse);
    }

    [Fact]
    public async void Handle_Should_AddOnRepository_WhenEmailIsUnique()
    {
        // Arrange
        var command = new RegisterMemberCommand("email@test.com", "first", "last");

        _memberRepositoryMock.Setup(
            x => x.IsEmailUniqueAsync("email@test.com", default))
            .ReturnsAsync(true);

        var handler = new RegisterMemberCommandHandler(
            _memberRepositoryMock.Object,
            _unitOfWorkMock.Object);
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
        var command = new RegisterMemberCommand("email@test.com", "first", "last");

        _memberRepositoryMock.Setup(
            x => x.IsEmailUniqueAsync("email@test.com", default))
            .ReturnsAsync(false);

        var handler = new RegisterMemberCommandHandler(
            _memberRepositoryMock.Object,
            _unitOfWorkMock.Object);
        // Act
        Result<Guid> result = await handler.Handle(command, default);

        // Assert
        _unitOfWorkMock.Verify(
            x => x.SaveChangeAsync(It.IsAny<CancellationToken>()),
            Times.Never);
    }
}
