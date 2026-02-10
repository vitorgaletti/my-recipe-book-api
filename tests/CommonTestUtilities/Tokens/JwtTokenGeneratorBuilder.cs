using MyRecepiBook.Infrastructure.Security.Tokens.Access.Generator;
using MyRecipeBook.Domain.Security.Tokens;

namespace CommonTestUtilities.Tokens;

public class JwtTokenGeneratorBuilder
{
    public static IAccessTokenGenerator Build() =>
        new JwtTokenGenerator(expirationTimeMinutes: 5, signingKey: "tttttttttttttttttttttttttttttttt");
}
