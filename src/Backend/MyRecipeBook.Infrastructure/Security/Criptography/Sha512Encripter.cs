using System.Security.Cryptography;
using System.Text;
using MyRecipeBook.Domain.Security.Cryptography;

namespace MyRecepiBook.Infrastructure.Security.Criptography;

public class Sha512Encripter(string additionKey) : IPasswordEncripter
{
    public string Encrypt(string password)
    {
        var newPassword = $"{password}{additionKey}";

        var bytes = Encoding.UTF8.GetBytes(newPassword);
        var hashBytes = SHA512.HashData(bytes);

        return StringBytes(hashBytes);
    }

    private static string StringBytes(byte[] bytes)
    {
        var sb = new StringBuilder();
        foreach (var b in bytes)
        {
            var hex = b.ToString("x2");
            sb.Append(hex);
        }

        return sb.ToString();
    }
}