using System;
using System.Collections.Generic;

namespace SistemaEscuela.Models;

public partial class Alumno
{
    public int Id { get; set; }

    public int? IdRol { get; set; }

    public string? Nombre { get; set; }

    public string? ApellidoPaterno { get; set; }

    public bool? Bhabilitado { get; set; }

    public string? ApellidoMaterno { get; set; }

    public virtual ICollection<Matricula> Matriculas { get; set; } = new List<Matricula>();
}
