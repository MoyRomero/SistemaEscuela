using Microsoft.AspNetCore.Mvc;
using SistemaEscuela.DTOs;
using SistemaEscuela.Models;

namespace SistemaEscuela.Controllers
{
    public class ProfesorController : Controller
    {
        private SistemaEscuelaContext _context;
        //private int? IdProfesor; 

        public ProfesorController(SistemaEscuelaContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            int idProfesor = 1;
            //IdProfesor = idProfesor;
            var Materias = ObtenerMaterias(idProfesor);
            ViewBag.nombreProfesor = NombreProfesor(idProfesor);
            return View(Materias);
        }

        private string NombreProfesor(int idProfesor)
        {
            var profesor = _context.Profesors
                .Where(x => x.Id == idProfesor)
                .Select(x => new { x.Nombre, x.ApellidoPaterno, x.ApellidoMaterno })
                .FirstOrDefault();
            
            if(profesor != null)
            {
                return $"{profesor.Nombre} {profesor.ApellidoPaterno} {profesor.ApellidoMaterno}";
            }
            return "Profesor no encontrado";
        }

        public List<Materia> ObtenerMaterias(int idProfesor)
        {
            List<Materia> Materias = new List<Materia>();

            try
            {
                var materiasBD = _context.Materias.Where(x=>x.IdProfesor == idProfesor).ToList();

                if (materiasBD == null)
                {
                    Materias.Add(new Materia { NombreMateria = "No se encontraron materias impartiendo." });
                    return Materias;
                }

                Materias.AddRange(materiasBD);

                return Materias;
            }
            catch (Exception ex)
            {
                Materias.Add(new Materia { NombreMateria = $"Ocurrió un error: {ex}" });

                return Materias;
            }
        }

        public List<MateriaAlumnoDTO> ObtenerAlumnos(int idMateria)
        {
            List<MateriaAlumnoDTO> MateriaAlumno = new List<MateriaAlumnoDTO>();

            try
            {
                string nombreMateria = "";

                var alumnosInscritosBD = _context.Matriculas
                    .Where(x => x.IdMateria == idMateria)
                    .Select(x => x.IdAlumno)
                    .ToList(); 

                var materiaBD = _context.Materias.Where(x => x.Id == idMateria).FirstOrDefault();
                
                if(materiaBD != null && materiaBD.NombreMateria != null) 
                {
                    nombreMateria = materiaBD.NombreMateria;
                }

                if (alumnosInscritosBD == null || alumnosInscritosBD.Count == 0)
                {
                    MateriaAlumno.Add(new MateriaAlumnoDTO { NombreAlumno = "No se encontraron alumnos para esta materia." });
                    return MateriaAlumno;
                }

                foreach (var idAlumno in alumnosInscritosBD)
                {
                    int? calificacionRecuperada = null;

                    var alumno = _context.Alumnos
                        .FirstOrDefault(a => a.Id == idAlumno);

                    var calificacion = _context.Calificaciones
                        .Where(x => x.MateriaId == idMateria && x.EstudianteId == alumno.Id)
                        .FirstOrDefault();

                    if (calificacion != null) calificacionRecuperada = calificacion.Calificacion; 

                    if (alumno != null)
                    {
                        MateriaAlumno.Add(new DTOs.MateriaAlumnoDTO {
                            IdAlumno = alumno.Id ,
                            NombreAlumno = alumno.Nombre,
                            ApellidoPaterno = alumno.ApellidoPaterno,
                            ApellidoMaterno = alumno.ApellidoMaterno,
                            Calificacion = calificacionRecuperada,
                            IdMateria = materiaBD.Id,
                            NombreMateria = nombreMateria
                        });
                    }
                }

                return MateriaAlumno;
            }
            catch (Exception ex)
            {
                MateriaAlumno.Add(new MateriaAlumnoDTO { NombreAlumno = $"Ocurrió un error: {ex}" });

                return MateriaAlumno;
            }
        }

        public JsonResult EnviarCalificacion(int idMateria, int idAlumno,int calificacion)
        {
            try
            {
                Calificacione calificacionMateria = new Calificacione();
                int idProfesor = 1;
                var consultaMateriaBD = _context.Materias
                    .Where(x => x.Id == idMateria && x.IdProfesor == idProfesor).FirstOrDefault();
                
                var calificacionExiste = _context.Calificaciones
                         .Where(x => x.EstudianteId == idAlumno && x.MateriaId == idMateria).Any();

                if (calificacionExiste)
                {
                    var alumnoBD = _context.Alumnos
                        .Where(x=>x.Id == idAlumno)
                        .Select(x => new { x.Nombre,x.ApellidoPaterno,x.ApellidoMaterno }).FirstOrDefault();

                    return Json($"El estudiante: {alumnoBD.Nombre} {alumnoBD.ApellidoPaterno} {alumnoBD.ApellidoMaterno} ya recibió una calificación para la materia: {consultaMateriaBD.NombreMateria}.");
                }

                if (consultaMateriaBD == null)
                {
                   return Json($"La materia con id: {idMateria}, no existe.");
                }
                else
                {
                    if(ModelState.IsValid)
                    {
                        calificacionMateria.Calificacion = calificacion;
                        calificacionMateria.EstudianteId = idAlumno;
                        calificacionMateria.MateriaId = idMateria;
                        calificacionMateria.Bhabilitado = true;

                        _context.Calificaciones.Add(calificacionMateria);
                        _context.SaveChanges();

                        return Json("Se ha enviado la calificación, de manera correcta.");
                    }

                    return Json("Ocurrió un error (ModelState)");
                }
            }
            catch (Exception ex)
            {
                return Json($"Ocurrió un error: {ex}");
            }
        }
    }
}
