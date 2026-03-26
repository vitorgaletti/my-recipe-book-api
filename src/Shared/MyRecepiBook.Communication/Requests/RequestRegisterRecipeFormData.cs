using Microsoft.AspNetCore.Http;

namespace MyRecepiBook.Communication.Requests;

public class RequestRegisterRecipeFormData : RequestRecipeJson
{
    public IFormFile? Image { get; set; }
}