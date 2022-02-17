using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using MyGames.Core.Repositories;
using MyGames.Core.Services;
using MyGames.Core.Services.Interfaces;
using MyGames.Database.Schemas;
using NSubstitute;
using Xunit;

namespace MyGames.Tests;

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
        _repository.GetAllAsync().Returns(new List<User>
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
        });

        var result = await _sut.GetUsers();

        result.Should().HaveCount(2);
    }
}