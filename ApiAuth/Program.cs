using ApiAuth.Controller;
using ApiAuth.Model;
using ApiAuth.Model.Schemas;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<UserDatabase>(opt => 
    opt.UseSqlite("Data Source=app.db"));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddAuthorization();
builder.Services.AddIdentityApiEndpoints<User>()
    .AddEntityFrameworkStores<UserDatabase>();

var app = builder.Build();

app.MapGet("/", () => "Olá, Mundo!");
app.MapIdentityEndpoints();
app.MapIdentityApi<User>();

app.Run();
