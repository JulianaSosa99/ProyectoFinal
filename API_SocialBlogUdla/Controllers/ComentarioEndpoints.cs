using Microsoft.EntityFrameworkCore;
using API_SocialBlogUdla.Data.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
namespace API_SocialBlogUdla.Controllers;

public static class ComentarioEndpoints
{
    public static void MapComentarioEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Comentario").WithTags(nameof(Comentario));

        group.MapGet("/", async (SocialBlogUdlaContext db) =>
        {
            return await db.Comentarios.ToListAsync();
        })
        .WithName("GetAllComentarios")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Comentario>, NotFound>> (Guid id, SocialBlogUdlaContext db) =>
        {
            return await db.Comentarios.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is Comentario model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetComentarioById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (Guid id, Comentario comentario, SocialBlogUdlaContext db) =>
        {
            var affected = await db.Comentarios
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.Id, comentario.Id)
                    .SetProperty(m => m.Contenido, comentario.Contenido)
                    .SetProperty(m => m.BlogPostId, comentario.BlogPostId)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateComentario")
        .WithOpenApi();

        group.MapPost("/", async (Comentario comentario, SocialBlogUdlaContext db) =>
        {
            db.Comentarios.Add(comentario);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Comentario/{comentario.Id}",comentario);
        })
        .WithName("CreateComentario")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (Guid id, SocialBlogUdlaContext db) =>
        {
            var affected = await db.Comentarios
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteComentario")
        .WithOpenApi();
    }
}
