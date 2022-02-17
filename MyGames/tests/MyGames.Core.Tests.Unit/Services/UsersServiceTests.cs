using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using MyGames.Core.Repositories;
using MyGames.Core.Services;
using MyGames.Core.Services.Interfaces;
using MyGames.Database.Schemas;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace MyGames.Core.Tests.Unit.Services;

public class UsersServiceTests
{
    private readonly IUsersService _sut;
    // private readonly IOptions<MongoDbSettings> _dbOptions = Options.Create(new MongoDbSettings()); // = Substitute.For<IOptions<MongoDbSettings>>();
    private readonly IUserRepository _repository = Substitute.For<IUserRepository>();
    public UsersServiceTests()
    {
        _sut = new UsersService(_repository);
    }

    [Fact]
    public async Task GetUsers_ShouldReturnEmptyList_WhenNoUsersExist()
    {
        _repository.GetAllAsync().Returns(new List<User>());
        var result = await _sut.GetUsers();

        result.Should().BeEmpty();
    }

    [Fact]
    public async Task GetUsers_ShouldReturnListOfUsers_WhenUsersExist()
    {
        var expectedUsers = new List<User>
        {
            new()
            {
                Id = Guid.NewGuid().ToString(),
                Username = "Mike",
                Password = string.Empty,
            },
            new()
            {
                Id = Guid.NewGuid().ToString(),
                Username = "Sophie",
                Password = string.Empty,
            }
        };
        
        _repository.GetAllAsync().Returns(expectedUsers);

        var result = await _sut.GetUsers();

        result.Should().HaveCount(2);
    }

    [Fact]
    public async Task GetByUsername_ShouldReturnUser_WhenUsernameMatchesUserInDatabase()
    {
        var existingUser = new User
        {
            Id = Guid.NewGuid().ToString(),
            Username = "mike",
            Password = string.Empty,
        };

        _repository.GetByIdAsync(existingUser.Username).Returns(existingUser);

        var user = await _sut.GetByUsername(existingUser.Username);

        user.Should().NotBeNull();
        user!.Id.Should().BeEquivalentTo(existingUser.Id);
    }

    [Fact]
    public async Task GetByUsername_ShouldReturnNull_WhenNoUserExistsInDatabase()
    {
        // arrange
        _repository.GetByIdAsync(Arg.Any<string>()).ReturnsNull();

        var user = await _sut.GetByUsername(Guid.NewGuid().ToString());

        user.Should().BeNull();
    }

    [Fact]
    public async Task GetByUsername_ShouldReturnNull_WhenUsernameIsEmpty()
    {
        var user = await _sut.GetByUsername(string.Empty);

        user.Should().BeNull();
    }
}