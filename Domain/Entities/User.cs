using Domain.Utils;
using System.Text.Json.Serialization;

namespace Domain.Entities;

public class User : Entity
{
    public User(){}
    public User(string email, string password, string? fullName = default) : base()
    {
        Email = email;
        var hashedPassword = PasswordUtils.HashPassword(password);
        Password = hashedPassword.Hash;
        Salt = hashedPassword.Salt;
        FullName = fullName;
    }

    public string Email { get;  set; }
    public string? FullName { get; set; }
    public string Password { get;  set; }
    public string Salt { get; private set; }
    [JsonIgnore]
    public IList<Product> Products { get; set; }
    [JsonIgnore]
    public IList<Order> Orders { get; set; }
}
