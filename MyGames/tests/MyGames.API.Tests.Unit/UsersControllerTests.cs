using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using MyGames.API.Controllers;
using MyGames.Core.Dtos;
using MyGames.Core.Services.Interfaces;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace MyGames.API.Tests.Unit;

public class UsersControllerTests
{
    private readonly UsersController _sut;
    private readonly IUsersService _usersService = Substitute.For<IUsersService>();

    public UsersControllerTests()
    {
        _sut = new UsersController(_usersService);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsOkObjectWithEmptyList_WhenNoUsersExist()
    {
        _usersService.GetUsers().Returns(new List<UserDto>());

        var result = (OkObjectResult) await _sut.GetAllAsync();

        result.StatusCode.Should().Be(200);
        result.Value.As<List<UserDto>>().Should().BeEmpty();
    }
    
    [Fact]
    public async Task GetAllAsync_ReturnsOkObjectWithListOfUsers_WhenUsersExist()
    {
        var users = new List<UserDto>
        {
            new()
            {
                Id = Guid.NewGuid().ToString(),
                Username = "Hello"
            }
        };
        
        _usersService.GetUsers().Returns(users);

        var result = (OkObjectResult) await _sut.GetAllAsync();

        result.StatusCode.Should().Be(200);
        result.Value.As<List<UserDto>>().Should().BeEquivalentTo(users);
    }

    [Fact]
    public async Task GetByUsernameAsync_ReturnsOkObject_WhenUserExists()
    {
        // Arrange
        var user = new UserDto
        {
            Username = "test"
        };
        _usersService.GetByUsername(user.Username).Returns(user);

        // Act
        var result = (OkObjectResult) await _sut.GetByUsernameAsync(user.Username);
        
        // Assert
        result.StatusCode.Should().Be(200);
        result.Value.Should().BeEquivalentTo(user);
    }

    [Fact]
    public async Task GetByUsernameAsync_ReturnsNotFound_WhenUserDoesNotExist()
    {
        _usersService.GetByUsername(Arg.Any<string>()).ReturnsNull();

        var result = (NotFoundObjectResult) await _sut.GetByUsernameAsync(Guid.NewGuid().ToString());

        result.StatusCode.Should().Be(404);
    }

    [Fact]
    public async Task AddGameToUserLibraryAsync_ShouldAddGameToUsersLibrary_WhenUserAndGameProvided()
    {
        var gameToAdd = new IgdbGameDto
        {
            Name = "test"
        };

        var username = Guid.NewGuid().ToString();

        _usersService.AddGameToUsersLibrary(username, gameToAdd).Returns(true);

        var result = (OkObjectResult) await _sut.AddGameToUserLibraryAsync(username, gameToAdd);

        result.StatusCode.Should().Be(200);
        result.Value.As<string>().Should().Be("game added to users library");
    }
    
    [Fact]
    public async Task AddGameToUserLibraryAsync_ShouldThrowException_WhenGameNotProvided()
    {
        var username = Guid.NewGuid().ToString();

        _usersService.AddGameToUsersLibrary(username, null).Throws(new Exception());

        var result = (BadRequestObjectResult) await _sut.AddGameToUserLibraryAsync(username, null);

        result.StatusCode.Should().Be(400);
        result.Should().BeOfType<BadRequestObjectResult>();
    }
    
    [Fact]
    public async Task AddGameToUserLibraryAsync_ShouldReturnFalse_WhenUsernameNotProvided()
    {
        _usersService.AddGameToUsersLibrary(string.Empty, null).Returns(false);

        var result = (BadRequestObjectResult) await _sut.AddGameToUserLibraryAsync(String.Empty, null);

        result.StatusCode.Should().Be(400);
        result.Should().BeOfType<BadRequestObjectResult>();
    }
    
    
    [Fact]
    public async Task RemoveGameFromUserLibraryAsync_ShouldRemoveGameFromUsersLibrary_WhenUserAndGameProvided()
    {
        var gameId = Guid.NewGuid().ToString();
        var username = Guid.NewGuid().ToString();
        _usersService.RemoveGameFromUsersLibrary(username, gameId).Returns(true);

        var result = (OkObjectResult) await _sut.RemoveGameFromUserLibraryAsync(username, gameId);

        result.StatusCode.Should().Be(200);
        result.Value.As<string>().Should().Be("game removed from users library");
    }
    
    [Fact]
    public async Task RemoveGameFromUserLibraryAsync_ShouldThrowException_WhenGameDoesNotExist()
    {
        var username = Guid.NewGuid().ToString();
        _usersService.RemoveGameFromUsersLibrary(username, Arg.Any<string>()).Throws(new Exception());

        var result = (BadRequestObjectResult) await _sut.RemoveGameFromUserLibraryAsync(username, Guid.NewGuid().ToString());

        result.StatusCode.Should().Be(400);
        result.Should().BeOfType<BadRequestObjectResult>();
    }
    
    [Theory]
    [InlineData("test-username", "")]
    [InlineData("", "test-game-id")]
    [InlineData("", "")]
    public async Task RemoveGameFromUserLibraryAsync_ShouldReturnFalse_WhenUsernameOrGameIdNotProvided(string username, string gameId)
    {
        _usersService.RemoveGameFromUsersLibrary(username, gameId).Returns(false);

        var result = (BadRequestObjectResult) await _sut.RemoveGameFromUserLibraryAsync(username, gameId);

        result.StatusCode.Should().Be(400);
        result.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact(Skip = "Not implemented API function yet.")]
    public async Task CreateAsync_Should_When()
    {
        
    }
    
    [Fact(Skip = "TODO.")]
    public async Task UpdateAsync_Should_When()
    {
        
    }
    
}