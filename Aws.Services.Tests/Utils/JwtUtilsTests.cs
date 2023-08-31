using Aws.Services.Utils;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Moq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Aws.Services.Tests.Utils;

public class JwtUtilsTests
{
    [Fact]
    public void ItShouldGenerateJWT()
    {
        var user = new User("test@test.com", "123456");
        var configuration = new Mock<IConfiguration>();
        configuration.SetupGet(x => x["Jwt:Sec"]).Returns("AHBSDHABSDHASBDBHA123674502");
        configuration.SetupGet(x => x["Jwt:Issuer"]).Returns("your_issuer");
        configuration.SetupGet(x => x["Jwt:Audience"]).Returns("your_audience");

        var token = JwtUtils.GenerateToken(configuration.Object, user);

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes("AHBSDHABSDHASBDBHA123674502");
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "your_issuer",
            ValidAudience = "your_audience",
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };

        SecurityToken validatedToken;
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out validatedToken);
        Assert.True(validatedToken is JwtSecurityToken);

        var claimsIdentity = principal.Identity as ClaimsIdentity;
        var emailClaim = claimsIdentity?.Claims.First().Value;
        Assert.Equal(user.Email, emailClaim);
    }
}
