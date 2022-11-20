using Microsoft.EntityFrameworkCore;
using ToDoListApi.Data;
using ToDoListApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnection")));


var app = builder.Build();

app.UseHttpsRedirection();

app.MapGet("api/todo", async(AppDbContext context) =>
{
    var items = await context.Todos.ToListAsync();
    return Results.Ok(items);
});

app.MapPost("api/todo", async (AppDbContext context, Todo todo) =>
{
    await context.Todos.AddAsync(todo);

    await context.SaveChangesAsync();

    return Results.Created($"api/todo/{todo.Id}", todo);
});

app.MapPut("api/todo/{id}", async (AppDbContext context, int id, Todo todo) =>
{
    var todoModel = await context.Todos.FirstOrDefaultAsync(todo => todo.Id == id);

    if(todoModel == null)
    {
        return Results.NotFound();
    }
    todoModel.TodoName = todo.TodoName;

    await context.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("api/todo/{id}", async (AppDbContext context, int id) =>
{
    var todoModel = await context.Todos.FirstOrDefaultAsync(todo => todo.Id == id);

    if(todoModel == null)
    {
        return Results.NotFound();
    }
    context.Todos.Remove(todoModel);

    await context.SaveChangesAsync();

    return Results.NoContent();
});

app.Run();

