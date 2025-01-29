using Domain.BusinessEntites.DTOs;
using Domain.BusinessEntites.Entities;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<User> repo = [];


app.MapPost("/register", (RegisterUserDTO dto) =>
{
    User user = new(dto.Login,dto.NickName,dto.Password);
    repo.Add(user);
});

app.MapGet("/users", () =>
{
    return repo;
});

app.Run();
