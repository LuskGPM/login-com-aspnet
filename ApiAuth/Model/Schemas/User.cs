using Microsoft.AspNetCore.Identity;

namespace ApiAuth.Model.Schemas;

public class User : IdentityUser
{
    public string? Cpf { get; set; }
    public string? Nome { get; set; }
    public DateOnly Data_Nascimento { get; set; }
}