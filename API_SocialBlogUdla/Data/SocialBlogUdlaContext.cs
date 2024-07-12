using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using API_SocialBlogUdla.Data.Models;

    public class SocialBlogUdlaContext : DbContext
    {
        public SocialBlogUdlaContext (DbContextOptions<SocialBlogUdlaContext> options)
            : base(options)
        {
        }
        public DbSet<API_SocialBlogUdla.Data.Models.BlogPost> BlogPosts { get; set; } = default!;

        public DbSet<API_SocialBlogUdla.Data.Models.BlogPostComment> BlogPostComments { get; set; } = default!;

        public DbSet<API_SocialBlogUdla.Data.Models.Comentario> Comentarios { get; set; } = default!;

        public DbSet<API_SocialBlogUdla.Data.Models.Usuario> Usuarios { get; set; } = default!;
            
}
