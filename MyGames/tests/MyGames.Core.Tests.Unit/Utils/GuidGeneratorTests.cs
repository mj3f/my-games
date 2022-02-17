using MyGames.Core.Utils;
using Xunit;

namespace MyGames.Core.Tests.Unit.Utils;

public class GuidGeneratorTests
{
    [Fact]
    public void GenerateGuidForMongoDb_ShouldGenerateGUIDWithoutSpecialCharacters()
    {
        string[] specialChars = { "(", ")", "-" };
        
        string guid = GuidGenerator.GenerateGuidForMongoDb();

        foreach (var specialChar in specialChars)
        {
            Assert.DoesNotContain(specialChar, guid);
        }
    }

    [Fact]
    public void GenerateGuidForMongoDb_ShouldGenerateGUIDOf24CharacterLength()
    {
        string guid = GuidGenerator.GenerateGuidForMongoDb();
        Assert.Equal(24, guid.Length);
    }
}