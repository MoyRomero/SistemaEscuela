using System;
using System.Collections.Generic;

namespace SistemaEscuela.Models;

public partial class Matricula
{
    public int Id { get; set; }

    public int? IdMateria { get; set; }

    public int? IdAlumno { get; set; }

    public virtual Alumno? IdAlumnoNavigation { get; set; }

    public virtual Materia? IdMateriaNavigation { get; set; }
}
