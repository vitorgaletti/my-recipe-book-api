namespace MyRecepiBook.Communication.Requests;

public class RequestGenerateRecipeJson
{
    public IList<string> Ingredients { get; set; } = [];
}