using Microsoft.AspNetCore.Mvc;
using MyRecepiBook.Communication.Requests;
using MyRecepiBook.Communication.Responses;
using MyRecipeBook.API.Attributes;
using MyRecipeBook.Application.UseCases.Recipe.Register;

namespace MyRecipeBook.API.Controllers;

[AuthenticatedUser]
public class RecipeController : MyRecipeBookBaseController
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegiteredRecipeJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterRecipeUseCase useCase,
        [FromForm] RequestRecipeJson request)
    {
        var response = await useCase.Execute(request);

        return Created(string.Empty, response);
    }
}