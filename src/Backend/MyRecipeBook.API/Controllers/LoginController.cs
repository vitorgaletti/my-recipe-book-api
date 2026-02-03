using Microsoft.AspNetCore.Mvc;
using MyRecepiBook.Communication.Requests;
using MyRecepiBook.Communication.Responses;
using MyRecipeBook.Application.UseCases.Login.DoLogin;

namespace MyRecipeBook.API.Controllers;

public class LoginController : MyRecipeBookBaseController
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromServices] IDoLoginUseCase useCase, [FromBody] RequestLoginJson request)
    {
        var response = await useCase.Execute(request);

        return Ok(response);
    }
}