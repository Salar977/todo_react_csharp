using efscaffold.Entities;
using Infrastructure.Postgres.Scaffolding;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration.GetConnectionString("Default");

builder.Services.AddDbContext<MyDbContext>(conf =>
{
    conf.UseNpgsql(connectionString);
});

var app = builder.Build();

app.MapGet("todo/add", async ([FromServices] MyDbContext dbContext) =>
{
    var myTodo = new Todo()
    {
        Id = Guid.NewGuid().ToString(),
        Title = "test title",
        Description = "test description",
        Priority = 5,
        Isdone = false
    };
    await dbContext.AddAsync(myTodo);
    await dbContext.SaveChangesAsync();
    return Results.Ok(myTodo);
});

app.MapGet("", async ([FromServices] MyDbContext dbContext) =>
{
    var objects = await dbContext.Todos.ToListAsync();
    return Results.Ok(objects);
});

app.Run();
