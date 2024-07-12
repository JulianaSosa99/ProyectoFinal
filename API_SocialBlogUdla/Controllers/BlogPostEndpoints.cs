using Microsoft.EntityFrameworkCore;
using API_SocialBlogUdla.Data.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
namespace API_SocialBlogUdla.Controllers;

public static class BlogPostEndpoints
{
    public static void MapBlogPostEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/BlogPost").WithTags(nameof(BlogPost));

        group.MapGet("/", async (SocialBlogUdlaContext db) =>
        {
            return await db.BlogPosts.ToListAsync();
        })
        .WithName("GetAllBlogPosts")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<BlogPost>, NotFound>> (Guid id, SocialBlogUdlaContext db) =>
        {
            return await db.BlogPosts.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is BlogPost model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetBlogPostById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (Guid id, BlogPost blogPost, SocialBlogUdlaContext db) =>
        {
            var affected = await db.BlogPosts
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.Id, blogPost.Id)
                    .SetProperty(m => m.Encabezado, blogPost.Encabezado)
                    .SetProperty(m => m.Contenido, blogPost.Contenido)
                    .SetProperty(m => m.Visible, blogPost.Visible)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateBlogPost")
        .WithOpenApi();

        group.MapPost("/", async (BlogPost blogPost, SocialBlogUdlaContext db) =>
        {
            db.BlogPosts.Add(blogPost);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/BlogPost/{blogPost.Id}",blogPost);
        })
        .WithName("CreateBlogPost")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (Guid id, SocialBlogUdlaContext db) =>
        {
            var affected = await db.BlogPosts
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteBlogPost")
        .WithOpenApi();
    }
}
