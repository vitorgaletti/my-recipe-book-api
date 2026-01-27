using System.Globalization;
using MyRecipeBook.Domain.Extensions;

namespace MyRecipeBook.API.Middleware;
public class CultureMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var supportedCultures = CultureInfo.GetCultures(CultureTypes.AllCultures).ToList();
        
        var requestedCulture = context.Request.Headers.AcceptLanguage.FirstOrDefault();

        var cultureInfo = new CultureInfo("en");

        if (string.IsNullOrWhiteSpace(requestedCulture).IsFalse() && supportedCultures.Exists(c => c.Name.Equals(requestedCulture)))
        {
            cultureInfo = new CultureInfo(requestedCulture!);
        }
        
        CultureInfo.CurrentCulture = cultureInfo;
        CultureInfo.CurrentUICulture = cultureInfo;
        
        await next(context);
    }
}