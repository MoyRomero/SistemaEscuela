using System;
using System.Collections.Generic;

namespace SistemaEscuela.Models;

public partial class Materia
{
    public int Id { get; set; }

    public string? NombreMateria { get; set; }

    public bool? Bhabilitado { get; set; }

    public int? IdProfesor { get; set; }

    public virtual ICollection<Matricula> Matriculas { get; set; } = new List<Matricula>();
}
