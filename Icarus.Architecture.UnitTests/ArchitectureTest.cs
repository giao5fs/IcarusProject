namespace Icarus.Architecture.UnitTests;
public class ArchitectureTest
{
    private const string DomainNamespace = "Icarus.Domain";
    private const string ApplicationNamespace = "Icarus.Application";
    private const string InfrastructureNamespace = "Icarus.Infrastructure";
    private const string PersistenceNamespace = "Icarus.Persistence";
    private const string PresentationNamespace = "Icarus.Presentation";
    private const string AppNamespace = "Icarus.App";

    [Fact]
    public void Domain_Should_Not_HaveDenpendencyOnOtherProjects()
    {
        // Arrange
        var assembly = typeof(Icarus.Domain.AssemblyReference).Assembly;

        var otherProjects = new[] {
            ApplicationNamespace,
            InfrastructureNamespace,
            PersistenceNamespace,
            PresentationNamespace,
            AppNamespace
        };

        // Act
        var testResult = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAll(otherProjects)
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Application_Should_Not_HaveDenpendencyOnOtherProjects()
    {
        // Arrange
        var assembly = typeof(Icarus.Application.AssemblyReference).Assembly;

        var otherProjects = new[] {
            InfrastructureNamespace,
            PersistenceNamespace,
            PresentationNamespace,
            AppNamespace
        };

        // Act
        var testResult = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAll(otherProjects)
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Infrastructure_Should_Not_HaveDenpendencyOnOtherProjects()
    {
        // Arrange
        var assembly = typeof(Icarus.Infrastructure.AssemblyReference).Assembly;

        var otherProjects = new[] {
            PersistenceNamespace,
            PresentationNamespace,
            AppNamespace
        };

        // Act
        var testResult = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAll(otherProjects)
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Persistence_Should_Not_HaveDenpendencyOnOtherProjects()
    {
        // Arrange
        var assembly = typeof(Icarus.Persistence.AssemblyReference).Assembly;

        var otherProjects = new[] {
            PresentationNamespace,
            AppNamespace
        };

        // Act
        var testResult = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAll(otherProjects)
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Presentation_Should_Not_HaveDenpendencyOnOtherProjects()
    {
        // Arrange
        var assembly = typeof(Icarus.Presentation.AssemblyReference).Assembly;

        var otherProjects = new[] {
            InfrastructureNamespace,
            PersistenceNamespace,
            AppNamespace
        };

        // Act
        var testResult = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAll(otherProjects)
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Handler_Should_HaveDependencyOnDomain()
    {
        // Arrange
        var assembly = typeof(Icarus.Application.AssemblyReference).Assembly;

        // Act
        var testResult = Types
            .InAssembly(assembly)
            .That()
            .HaveNameEndingWith("Handler")
            .Should()
            .HaveDependencyOn(DomainNamespace)
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Controllers_Should_HaveDependencyOnMediatR()
    {
        // Arrange
        var assembly = typeof(Icarus.Presentation.AssemblyReference).Assembly;

        // Act
        var testResult = Types
            .InAssembly(assembly)
            .That()
            .HaveNameEndingWith("Controller")
            .Should()
            .HaveDependencyOn("MediatR")
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }
}
