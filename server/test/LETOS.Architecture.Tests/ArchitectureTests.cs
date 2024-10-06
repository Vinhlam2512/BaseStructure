using FluentAssertions;
using LETOS.Share.Abstractions.Shared;
using NetArchTest.Rules;

namespace LETOS.Architecture.Tests;

public class ArchitectureTests
{
    private const string DomainNamespace = "LETOS.Domain";
    private const string ApplicationNamespace = "LETOS.Application";
    private const string InfrastructureNamespace = "LETOS.Infrastructure";
    private const string PersistenceNamespace = "LETOS.Persistence";
    private const string PresentationNamespace = "LETOS.Presentation";
    private const string ApiNamespace = "LETOS.Api";

    [Fact]
    public void Domain_Should_Not_HaveDependencyOnOthLETOSroject()
    {
        // Arrage
        var assembly = LETOS.Domain.AssemblyReference.Assembly;

        var othLETOSrojects = new[]
        {
            ApplicationNamespace,
            InfrastructureNamespace,
            PersistenceNamespace,
            PresentationNamespace,
            ApiNamespace,
        };

        // Act
        var testResult = Types
                            .InAssembly(assembly)
                            .ShouldNot()
                            .HaveDependencyOnAny(othLETOSrojects)
                            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Application_Should_Not_HaveDependencyOnOthLETOSrojects()
    {
        // Arrange
        var assembly = Application.AssemblyReference.Assembly;

        var othLETOSrojects = new[]
        {
            InfrastructureNamespace,
            //PersistenceNamespace, // Due to Implement sort multi columns by apply RawQuery with EntityFramework
            PresentationNamespace,
            ApiNamespace
        };

        // Act
        var testResult = Types
                            .InAssembly(assembly)
                            .ShouldNot()
                            .HaveDependencyOnAny(othLETOSrojects)
                            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Infrastructure_Should_Not_HaveDependencyOnOthLETOSrojects()
    {
        // Arrange
        var assembly = LETOS.Infrastructure.AssemblyReference.Assembly;

        var othLETOSrojects = new[]
        {
            PresentationNamespace,
            ApiNamespace
        };

        // Act
        var testResult = Types
                            .InAssembly(assembly)
                            .ShouldNot()
                            .HaveDependencyOnAny(othLETOSrojects)
                            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Persistence_Should_Not_HaveDependencyOnOthLETOSrojects()
    {
        // Arrange
        var assembly = LETOS.Persistence.AssemblyReference.Assembly;

        var othLETOSrojects = new[]
        {
            ApplicationNamespace,
            InfrastructureNamespace,
            PresentationNamespace,
            ApiNamespace
        };

        // Act
        var testResult = Types
                            .InAssembly(assembly)
                            .ShouldNot()
                            .HaveDependencyOnAny(othLETOSrojects)
                            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }


    #region =============== Command ===============

    [Fact]
    public void Command_Should_Have_NamingConventionEndingCommand()
    {
        // Arrage
        var assembly = Application.AssemblyReference.Assembly;

        // Act
        var testResult = Types
            .InAssembly(assembly)
            .That()
            .ImplementInterface(typeof(ICommand))
            .Should().HaveNameEndingWith("Command")
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void CommandT_Should_Have_NamingConventionEndingCommand()
    {
        // Arrage
        var assembly = Application.AssemblyReference.Assembly;

        // Act
        var testResult = Types
            .InAssembly(assembly)
            .That()
            .ImplementInterface(typeof(ICommand<>))
            .Should().HaveNameEndingWith("Command")
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void CommandHandlers_Should_Have_NamingConventionEndingCommandHandler()
    {
        // Arrange
        var assembly = Application.AssemblyReference.Assembly;

        // Act
        var testResult = Types.InAssembly(assembly)
            .That()
            .ImplementInterface(typeof(ICommandHandler<>))
            .Should()
            .HaveNameEndingWith("CommandHandler")
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void CommandHandlers_Should_Have_BeSealed()
    {
        // Arrange
        var assembly = Application.AssemblyReference.Assembly;

        // Act
        var testResult = Types.InAssembly(assembly)
            .That()
            .ImplementInterface(typeof(ICommandHandler<>))
            .Should()
            .BeSealed()
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void CommandHandlersT_Should_Have_NamingConventionEndingCommandHandler()
    {
        // Arrange
        var assembly = Application.AssemblyReference.Assembly;

        // Act
        var testResult = Types.InAssembly(assembly)
            .That()
            .ImplementInterface(typeof(ICommandHandler<,>))
            .Should()
            .HaveNameEndingWith("CommandHandler")
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void CommandHandlersT_Should_Have_BeSealed()
    {
        // Arrange
        var assembly = Application.AssemblyReference.Assembly;

        // Act
        var testResult = Types.InAssembly(assembly)
            .That()
            .ImplementInterface(typeof(ICommandHandler<,>))
            .Should()
            .BeSealed()
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    #endregion End Command

    #region =============== Query ===============

    [Fact]
    public void Query_Should_Have_NamingConventionEndingQuery()
    {
        // Arrage
        var assembly = Application.AssemblyReference.Assembly;

        // Act
        var testResult = Types
            .InAssembly(assembly)
            .That()
            .ImplementInterface(typeof(IQuery<>))
            .Should().HaveNameEndingWith("Query")
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void QueryHandlers_Should_Have_NamingConventionEndingQueryHandler()
    {
        // Arrange
        var assembly = Application.AssemblyReference.Assembly;

        // Act
        var testResult = Types.InAssembly(assembly)
            .That()
            .ImplementInterface(typeof(IQueryHandler<,>))
            .Should()
            .HaveNameEndingWith("QueryHandler")
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void QueryHandlers_Should_Have_BeSealed()
    {
        // Arrange
        var assembly = Application.AssemblyReference.Assembly;

        // Act
        var testResult = Types.InAssembly(assembly)
            .That()
            .ImplementInterface(typeof(IQueryHandler<,>))
            .Should()
            .BeSealed()
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    #endregion End Query
}
