using ApiAuth.Model.Schemas;
using AutoMapper;

namespace ApiAuth.Controller;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserUpdateDto, User>()
            .ForMember(campo => campo.Id, action => action.Ignore())
            .ForMember(campo => campo.PasswordHash, action => action.Ignore())
            .ForMember(campo => campo.Email, action => action.Ignore())
            .ForAllMembers(opt => opt.Condition((userdto, user, dados) => dados != null));
    }
}