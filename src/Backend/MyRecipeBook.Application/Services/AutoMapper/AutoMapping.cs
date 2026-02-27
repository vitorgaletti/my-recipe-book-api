using AutoMapper;
using MyRecepiBook.Communication.Requests;
using MyRecepiBook.Communication.Responses;
using MyRecipeBook.Domain.Entities;

namespace MyRecipeBook.Application.Services.AutoMapper;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToDomain();
        RequestToResponse();
    }

    private void RequestToDomain()
    {
        CreateMap<RequestRegisterUserJson, User>()
            .ForMember(dest => dest.Password, opt => opt.Ignore());

        CreateMap<RequestRecipeJson, Recipe>()
            .ForMember(dest => dest.Instructions, opt => opt.Ignore())
            .ForMember(dest => dest.Ingredients, opt => opt.MapFrom(source => source.Ingredients.Distinct()))
            .ForMember(dest => dest.DishTypes, opt => opt.MapFrom(source => source.DishTypes.Distinct()));

        CreateMap<string, Ingredient>()
            .ForMember(dest => dest.Item, opt => opt.MapFrom(source => source));

        CreateMap<MyRecipeBook.Domain.Enums.DishType, DishType>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(source => source));

        CreateMap<RequestInstructionJson, Instruction>();
    }

    private void RequestToResponse()
    {
        CreateMap<User, ResponseUserProfileJson>();
    }
}