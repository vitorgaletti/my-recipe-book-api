using Microsoft.AspNetCore.Mvc.ModelBinding;
using Sqids;

namespace MyRecipeBook.API.Binders;

public class MyRecipeBookIdBinder : IModelBinder
{
    private readonly SqidsEncoder<long> _idEncoder;

    public MyRecipeBookIdBinder(SqidsEncoder<long> idEncoder) => _idEncoder = idEncoder;

    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var modelName = bindingContext.ModelName;

        var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);

        if (valueProviderResult == ValueProviderResult.None)
            return Task.CompletedTask;

        bindingContext.ModelState.SetModelValue(modelName, valueProviderResult);

        var value = valueProviderResult.FirstValue;

        if (string.IsNullOrWhiteSpace(value))
            return Task.CompletedTask;

        long id = 0;

        try
        {
            var decodedIds = _idEncoder.Decode(value);

            if (decodedIds.Count == 1)
                id = decodedIds.Single();
        }
        catch
        {
            // If decoding fails, use default value of 0
        }

        bindingContext.Result = ModelBindingResult.Success(id);

        return Task.CompletedTask;
    }
}