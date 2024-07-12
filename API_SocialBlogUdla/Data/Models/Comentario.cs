using System;
using System.Collections.Generic;

namespace API_SocialBlogUdla.Data.Models;

public partial class Comentario
{
    public Guid Id { get; set; }

    public string Contenido { get; set; } = null!;

    public Guid BlogPostId { get; set; }

    public virtual BlogPost BlogPost { get; set; } = null!;
}
