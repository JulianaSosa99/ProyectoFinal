using Microsoft.EntityFrameworkCore;
using API_SocialBlogUdla.Data.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
namespace API_SocialBlogUdla.Controllers;

public static class BlogPostCommentEndpoints
{
    public static void MapBlogPostCommentEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/BlogPostComment").WithTags(nameof(BlogPostComment));

        group.MapGet("/", async (SocialBlogUdlaContext db) =>
        {
            return await db.BlogPostComments.ToListAsync();
        })
        .WithName("GetAllBlogPostComments")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<BlogPostComment>, NotFound>> (Guid id, SocialBlogUdlaContext db) =>
        {
            return await db.BlogPostComments.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is BlogPostComment model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetBlogPostCommentById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (Guid id, BlogPostComment blogPostComment, SocialBlogUdlaContext db) =>
        {
            var affected = await db.BlogPostComments
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.Id, blogPostComment.Id)
                    .SetProperty(m => m.Description, blogPostComment.Description)
                    .SetProperty(m => m.BlogPostId, blogPostComment.BlogPostId)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateBlogPostComment")
        .WithOpenApi();

        group.MapPost("/", async (BlogPostComment blogPostComment, SocialBlogUdlaContext db) =>
        {
            db.BlogPostComments.Add(blogPostComment);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/BlogPostComment/{blogPostComment.Id}",blogPostComment);
        })
        .WithName("CreateBlogPostComment")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (Guid id, SocialBlogUdlaContext db) =>
        {
            var affected = await db.BlogPostComments
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteBlogPostComment")
        .WithOpenApi();
    }
}
