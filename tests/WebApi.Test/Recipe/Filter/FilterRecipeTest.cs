using System.Globalization;
using System.Net;
using System.Text.Json;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Tokens;
using FluentAssertions;
using MyRecepiBook.Communication.Requests;
using MyRecepiBook.Exceptions;
using MyRecipeBook.Domain.Enums;
using WebApi.Test.InlineData;

namespace WebApi.Test.Recipe.Filter;

public class FilterRecipeTest : MyRecipeBookClassFixture
{
    private const string Method = "recipe/filter";

    private readonly Guid _userIdentifier;

    private string _recipeTitle;
    private Difficulty _recipedifficultyLevel;
    private CookingTime _recipeCookingTime;
    private IList<DishType> _recipeDishTypes;

    public FilterRecipeTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _userIdentifier = factory.GetUserIdentifier();

        _recipeTitle = factory.GetRecipeTitle();
        _recipeCookingTime = factory.GetRecipeCookingTime();
        _recipedifficultyLevel = factory.GetRecipeDifficulty();
        _recipeDishTypes = factory.GetDishTypes();
    }

    [Fact]
    public async Task Success()
    {
        var request = new RequestFilterRecipeJson
        {
            CookingTimes = [_recipeCookingTime],
            Difficulties = [_recipedifficultyLevel],
            DishTypes = _recipeDishTypes.ToList(),
            RecipeTitle_Ingredient = _recipeTitle,
        };

        var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

        var response = await DoPost(method: Method, request: request, token: token);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        responseData.RootElement.GetProperty("recipes").EnumerateArray().Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task Success_NoContent()
    {
        var request = RequestFilterRecipeJsonBuilder.Build();
        request.RecipeTitle_Ingredient = "recipeDontExist";

        var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

        var response = await DoPost(method: Method, request: request, token: token);

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_CookingTime_Invalid(string culture)
    {
        var request = RequestFilterRecipeJsonBuilder.Build();
        request.CookingTimes.Add((CookingTime)1000);

        var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

        var response = await DoPost(method: Method, request: request, token: token, culture: culture);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        var errors = responseData.RootElement.GetProperty("errors").EnumerateArray();

        var expectedMessage =
            ResourceMessagesException.ResourceManager.GetString("COOKING_TIME_NOT_SUPPORTED", new CultureInfo(culture));

        errors.Should().HaveCount(1).And.Contain(c => c.GetString()!.Equals(expectedMessage));
    }
}