using System;
using System.Collections.Generic;

namespace SistemaEscuela.Models;

public partial class Calificacione
{
    public int Id { get; set; }

    public int? MateriaId { get; set; }

    public int? EstudianteId { get; set; }

    public int? Calificacion { get; set; }

    public bool? Bhabilitado { get; set; }
}
