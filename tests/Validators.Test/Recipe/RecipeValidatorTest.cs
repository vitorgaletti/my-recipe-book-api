using System.Diagnostics.CodeAnalysis;
using CommonTestUtilities.Requests;
using MyRecepiBook.Exceptions;
using MyRecipeBook.Application.UseCases.Recipe;
using Shouldly;

namespace Validators.Test.Recipe;

public class RecipeValidatorTest
{
    [Fact]
    public void Success()
    {
        var validator = new RecipeValidator();

        var request = RequestRecipeJsonBuilder.Build();

        var result = validator.Validate(request);

        result.IsValid.ShouldBeTrue();
    }

    [Fact]
    public void Error_Invalid_Cooking_Time()
    {
        var validator = new RecipeValidator();

        var request = RequestRecipeJsonBuilder.Build();
        request.CookingTime = (MyRecepiBook.Communication.Enums.CookingTime?)1000;

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceMessagesException.COOKING_TIME_NOT_SUPPORTED));
    }

    [Fact]
    public void Error_Invalid_Difficulty()
    {
        var validator = new RecipeValidator();

        var request = RequestRecipeJsonBuilder.Build();
        request.Difficulty = (MyRecepiBook.Communication.Enums.Difficulty?)1000;

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldContain(e =>
            e.ErrorMessage.Equals(ResourceMessagesException.DIFFICULTY_LEVEL_NOT_SUPPORTED));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("          ")]
    [InlineData("")]
    public void Error_Empty_Title(string title)
    {
        var validator = new RecipeValidator();

        var request = RequestRecipeJsonBuilder.Build();
        request.Title = title;

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceMessagesException.RECIPE_TITLE_EMPTY));
    }

    [Fact]
    public void Success_Cooking_Time_Null()
    {
        var validator = new RecipeValidator();

        var request = RequestRecipeJsonBuilder.Build();
        request.CookingTime = null;

        var result = validator.Validate(request);

        result.IsValid.ShouldBeTrue();
    }

    [Fact]
    public void Success_Difficulty_Null()
    {
        var validator = new RecipeValidator();

        var request = RequestRecipeJsonBuilder.Build();
        request.Difficulty = null;

        var result = validator.Validate(request);

        result.IsValid.ShouldBeTrue();
    }
}