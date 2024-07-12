using System;
using System.Collections.Generic;

namespace API_SocialBlogUdla.Data.Models;

public partial class BlogPost
{
    public Guid Id { get; set; }

    public string Encabezado { get; set; } = null!;

    public string Contenido { get; set; } = null!;

    public bool Visible { get; set; }

    public virtual ICollection<BlogPostComment> BlogPostComments { get; set; } = new List<BlogPostComment>();

    public virtual ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();
}
