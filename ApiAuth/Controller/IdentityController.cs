using System.Security.Claims;
using ApiAuth.Model;
using ApiAuth.Model.Schemas;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ApiAuth.Controller;

public static class IdentityController
{
    public static void MapIdentityEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/auth");
        var user = app.MapGroup("/user");

        user.MapGet("/{Id}", GetByIdAsync).RequireAuthorization();
        user.MapDelete("/{Id}", DeleteUserAsync).RequireAuthorization();
        user.MapPatch("/{Id}", UpdateUserAsync).RequireAuthorization();

        group.MapIdentityApi<IdentityUser>();
    }
    private static async Task<IResult> GetByIdAsync(string Id, UserDatabase db)
    {
        var user = await db.Usuarios
            .Where(user => user.Id == Id)
                .Select(user => new UserGetDto
                {
                    Id = user.Id,
                    Nome = user.Nome,
                    Email = user.Email,
                    Cpf = user.Cpf,
                    Data_Nascimento = user.Data_Nascimento
                })
                    .FirstOrDefaultAsync();

        return user is null ? TypedResults.NotFound() : TypedResults.Ok(user);
    }
    private static async Task<IResult> DeleteUserAsync(string Id, UserDatabase db, ClaimsPrincipal userLogado)
    {
        var Id_inToken = userLogado.FindFirstValue(ClaimTypes.NameIdentifier);
        if (Id_inToken != Id) return TypedResults.Forbid();

        var user = await db.Usuarios.FindAsync(Id);
        if (user is null) return TypedResults.NotFound();
        
        db.Usuarios.Remove(user);
        await db.SaveChangesAsync();
        
        return TypedResults.NoContent();
    }
    private static async Task<IResult> UpdateUserAsync(string Id, UserDatabase db, ClaimsPrincipal userLogado, UserUpdateDto userUpdated, IMapper mapper)
    {
        var Id_inToken = userLogado.FindFirstValue(ClaimTypes.NameIdentifier);
        if (Id_inToken != Id) return TypedResults.Forbid();

        var user = await db.Usuarios.FindAsync(Id);
        if (user is null) return TypedResults.NotFound();

        mapper.Map(userUpdated, user);

        await db.SaveChangesAsync();
        return TypedResults.NoContent();
    }
}