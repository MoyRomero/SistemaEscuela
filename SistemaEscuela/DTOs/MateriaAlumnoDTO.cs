namespace SistemaEscuela.DTOs
{
    public class MateriaAlumnoDTO
    {
        public int IdAlumno { get; set;}
        public string? NombreAlumno { get; set; }
        public string? ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        public int? IdMateria { get; set; }
        public string? NombreMateria { get; set; }
        public int? Calificacion { get; set; }
    }
}
