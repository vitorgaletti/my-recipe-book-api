using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace WebApi.Test;

public class MyRecipeBookClassFixture(CustomWebApplicationFactory factory) : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _httpClient = factory.CreateClient();

    protected async Task<HttpResponseMessage> DoPost(
        string method,
        object request,
        string token = "",
        string culture = "en")
    {
        ChangeRequestCulture(culture);
        AuthorizeRequest(token);

        return await _httpClient.PostAsJsonAsync(method, request);
    }

    protected async Task<HttpResponseMessage> DoGet(string method, string token = "", string culture = "en")
    {
        ChangeRequestCulture(culture);
        AuthorizeRequest(token);

        return await _httpClient.GetAsync(method);
    }
    
    protected async Task<HttpResponseMessage> DoPut(string method, object request, string token, string culture = "en")
    {
        ChangeRequestCulture(culture);
        AuthorizeRequest(token);

        return await _httpClient.PutAsJsonAsync(method, request);
    }
    
    protected async Task<HttpResponseMessage> DoDelete(string method, string token, string culture = "en")
    {
        ChangeRequestCulture(culture);
        AuthorizeRequest(token);

        return await _httpClient.DeleteAsync(method);
    }

    private void ChangeRequestCulture(string culture)
    {
        _httpClient.DefaultRequestHeaders.AcceptLanguage.Clear();
        _httpClient.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue(culture));
    }

    private void AuthorizeRequest(string token)
    {
        if (string.IsNullOrWhiteSpace(token))
            return;

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }
}