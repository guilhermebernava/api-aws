using Domain.Utils;

namespace Domain.Tests.Utils;

public class PasswordUtilsTests
{

    [Theory]
    [InlineData("abc123")]
    [InlineData("asdjAJSDJASDJAJHSDhjADS")]
    public void ItShouldGenerateHashPassword(string password)
    {
        var (Hash, Salt) = PasswordUtils.HashPassword(password);
        Assert.NotEmpty(Salt);
        Assert.NotEmpty(Hash);
        Assert.NotNull(Hash);
        Assert.NotNull(Salt);
    }

    [Fact]
    public void ItShouldByValidPassword()
    {
        var (Hash, Salt) = PasswordUtils.HashPassword("abc123");
        Assert.NotEmpty(Salt);
        Assert.NotEmpty(Hash);
        Assert.NotNull(Salt);
        Assert.NotNull(Hash);


        var isTheSamePassword = PasswordUtils.VerifyPassword("abc123", Hash, Salt);
        Assert.True(isTheSamePassword);
    }

    [Fact]
    public void ItShouldNotBeValidPassword()
    {
        var (Hash, Salt) = PasswordUtils.HashPassword("abc123");
        Assert.NotEmpty(Salt);
        Assert.NotEmpty(Hash);
        Assert.NotNull(Salt);
        Assert.NotNull(Hash);


        var isTheSamePassword = PasswordUtils.VerifyPassword("abc12", Hash,Salt);
        Assert.False(isTheSamePassword);
    }
}
