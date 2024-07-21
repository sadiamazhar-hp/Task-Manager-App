using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using TaskManager.Interface;
using TaskManager.Models;
using TaskManager.Repositories;

public static class TasksEndpoints
{

    
    public static void MapTasksEndpoints(this IEndpointRouteBuilder routes)
    {
        ITaskRepo _taskdata;
        var group = routes.MapGroup("/api/Tasks").WithTags(nameof(Tasks));

        group.MapGet("/",async (IAsyncTask taskdRepo) =>
        {
            var task = await taskdRepo.GetAllTasksAsync();
            return TypedResults.Ok(task);
        })
        .WithName("GetAllTasks")
        .WithOpenApi();

        group.MapGet("/{id}", (int id) =>
        {
            //return new Tasks { ID = id };
        })
        .WithName("GetTasksById")
        .WithOpenApi();

        group.MapPut("/{id}", (int id, Tasks input) =>
        {
            return TypedResults.NoContent();
        })
        .WithName("UpdateTasks")
        .WithOpenApi();

        group.MapPost("/", (Tasks model) =>
        {
            //return TypedResults.Created($"/api/Tasks/{model.ID}", model);
        })
        .WithName("CreateTasks")
        .WithOpenApi();

        group.MapDelete("/{id}", (int id) =>
        {
            //return TypedResults.Ok(new Tasks { ID = id });
        })
        .WithName("DeleteTasks")
        .WithOpenApi();
    }


}