using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Options;
using MyGames.Core.AppSettings;
using MyGames.Core.Dtos;
using MyGames.Core.Services;
using MyGames.Core.Services.Interfaces;
using NSubstitute;
using Xunit;

namespace MyGames.Tests;

public class UsersServiceTests
{
    private readonly IUsersService _sut;
    private readonly IOptions<MongoDbSettings> _dbOptions = Options.Create(new MongoDbSettings()); // = Substitute.For<IOptions<MongoDbSettings>>();
    public UsersServiceTests()
    {
        _dbOptions.Value.ConnectionString = "mongodb+srv://fakeuser:password@cluster0.pqof2.mongodb.net/database-fake";
        _dbOptions.Value.DatabaseName = "database";
        _dbOptions.Value.UsersCollectionName = "users";

        _sut = new UsersService(_dbOptions);
    }

    [Fact]
    public async Task GetUsers_ShouldReturnEmptyList_WhenNoUsersExist()
    {
        _sut.GetUsers().Returns(new List<UserDto>()); // How to substitute the usersCollection in usersService???????
        var result = await _sut.GetUsers();

        result.Should().BeEmpty();


    }

    [Fact]
    public void GetUsers_ShouldReturnListOfUsers_WhenUsersExist()
    {
        
    }
}