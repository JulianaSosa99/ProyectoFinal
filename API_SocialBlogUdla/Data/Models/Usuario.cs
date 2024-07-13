using System;
using System.Collections.Generic;

namespace API_SocialBlogUdla.Data.Models;

public partial class Usuario
{
    public Guid Id { get; set; }

    public string NombreUsuario { get; set; } = null!;

    public string Email { get; set; } = null!;
    //prubas
    public string Contrasenia { get; set; } = null!;
}
