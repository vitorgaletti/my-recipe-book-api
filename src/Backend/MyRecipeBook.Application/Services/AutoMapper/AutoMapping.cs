using AutoMapper;
using MyRecepiBook.Communication.Requests;
using MyRecepiBook.Communication.Responses;
using MyRecipeBook.Domain.Entities;
using Sqids;

namespace MyRecipeBook.Application.Services.AutoMapper;

public class AutoMapping : Profile
{
    private readonly SqidsEncoder<long> _idEnconder;

    public AutoMapping(SqidsEncoder<long> idEnconder)
    {
        _idEnconder = idEnconder;
        RequestToDomain();
        RequestToResponse();
    }

    private void RequestToDomain()
    {
        CreateMap<RequestRegisterUserJson, Domain.Entities.User>()
            .ForMember(dest => dest.Password, opt => opt.Ignore());

        CreateMap<RequestRecipeJson, Domain.Entities.Recipe>()
            .ForMember(dest => dest.Instructions, opt => opt.Ignore())
            .ForMember(dest => dest.Ingredients, opt => opt.MapFrom(source => source.Ingredients.Distinct()))
            .ForMember(dest => dest.DishTypes, opt => opt.MapFrom(source => source.DishTypes.Distinct()));

        CreateMap<string, Domain.Entities.Ingredient>()
            .ForMember(dest => dest.Item, opt => opt.MapFrom(source => source));

        CreateMap<MyRecepiBook.Communication.Enums.DishType, Domain.Entities.DishType>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(source => source));

        CreateMap<RequestInstructionJson, Domain.Entities.Instruction>();
    }

    private void RequestToResponse()
    {
        CreateMap<User, ResponseUserProfileJson>();
        CreateMap<Recipe, ResponseRegiteredRecipeJson>()
            .ForMember(dest => dest.Id, config => config.MapFrom(source => _idEnconder.Encode(source.Id)));
        
        CreateMap<Recipe, ResponseShortRecipeJson>()
            .ForMember(dest => dest.Id, config => config.MapFrom(source => _idEnconder.Encode(source.Id)))
            .ForMember(dest => dest.AmountIngredients, config => config.MapFrom(source => source.Ingredients.Count));
    }
}