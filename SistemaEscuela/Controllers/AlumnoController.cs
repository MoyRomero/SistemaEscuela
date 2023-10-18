using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SistemaEscuela.DTOs;
using SistemaEscuela.Models;

namespace SistemaEscuela.Controllers
{
    public class AlumnoController : Controller
    {
        private SistemaEscuelaContext _context;

        public AlumnoController( SistemaEscuelaContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var materias = GetMaterias();
            return View(materias);
        }

        public JsonResult RemoverMateriaAsignada(int IdMateria, int IdAlumno)
        {
            var CalificacionExiste = _context.Calificaciones
                .Where(x=>x.MateriaId ==  IdMateria && x.EstudianteId == IdAlumno).Any();
            
            if(CalificacionExiste)
            {
                return Json("Ya se ha calificado la materia, por lo que no es posible removerla.");
            }
            else
            {
                var materia = _context.Matriculas
                    .Where(x=>x.IdMateria == IdMateria && x.IdAlumno == IdAlumno)
                    .FirstOrDefault(); 
                
                if(materia != null)
                {
                    _context.Matriculas.Remove(materia);
                    _context.SaveChanges();

                    return Json("Se removió la materia, de forma correcta.");
                }
                else
                {
                    return Json("No se encontró la materia con el Id: " + IdAlumno);
                }
            }
        }

        public JsonResult MostrarDetallesMateria(int IdMateria, int IdAlumno)
        {
            CalificacionMateriaDTO calificacionDTO = new CalificacionMateriaDTO();

            var CalificacionMateriaBD = _context.Calificaciones
                .Where(x => x.MateriaId == IdMateria && x.EstudianteId == IdAlumno).FirstOrDefault();

            if (CalificacionMateriaBD != null)
            {
                var materiaBD = _context.Materias.Where(x=>x.Id == CalificacionMateriaBD.MateriaId).FirstOrDefault();                

                if (CalificacionMateriaBD.Calificacion != null) calificacionDTO.Calificacion = (int)CalificacionMateriaBD.Calificacion;

                if (materiaBD != null)
                {
                    calificacionDTO.IdMateria = materiaBD.Id;
                    calificacionDTO.NombreMateria = materiaBD.NombreMateria;
                }
                return Json(calificacionDTO);
            }
            else
            {
                calificacionDTO.IdMateria = IdMateria;
                calificacionDTO.NombreMateria = _context.Materias.Where(x => x.Id == IdMateria).FirstOrDefault().NombreMateria;
                calificacionDTO.Calificacion = null;

                return Json(calificacionDTO);
            }          
        }
        public JsonResult AsignarMateria(int IdMateria)
        {
            int IdAlumno = 1;
            try
            {
                var consultaBD = _context.Matriculas
                    .Where(x => x.IdAlumno == IdAlumno && x.IdMateria == IdMateria).FirstOrDefault();
                
                if(consultaBD != null )
                {
                    return Json($"La materia ya se encuentra asignada.");
                }
                else
                {
                    Matricula NuevaMatricula = new Matricula() 
                    {
                        IdMateria = IdMateria,
                        IdAlumno=IdAlumno
                    };

                    _context.Matriculas.Add(NuevaMatricula);
                    _context.SaveChanges();

                    return Json("Se ha asignado la materia, de manera correcta.");
                }
            }
            catch (Exception ex)
            {
                return Json($"Ocurrió un error: {ex}");
            }
        }

        public JsonResult ObtenerMateriasAsignadas()
        {
            int idAlumno = 1;

            var materiasAsignadas = _context.Matriculas.Where(x=>x.IdAlumno == idAlumno).ToList();

            List<MateriasAsignadasAlumnoDTO> MateriasDTO = new List<MateriasAsignadasAlumnoDTO>();

            foreach ( var materia in materiasAsignadas)
            {
                var materiaBD = _context.Materias
                    .Where(x=>x.Id == materia.IdMateria)
                    .FirstOrDefault();
                if(materiaBD != null)
                {
                    MateriasDTO.Add(new MateriasAsignadasAlumnoDTO() { 
                        IdMateria = materiaBD.Id, 
                        NombreMateria = materiaBD.NombreMateria,
                        IdAlumno = idAlumno
                    });
                }
            }
            return Json(MateriasDTO);
        }

        public List<Materia> GetMaterias()
        {
            List<Materia> Materias = new List<Materia>();

            try
            { 
                var materiasBD = _context.Materias.ToList();

                if (materiasBD == null)
                {
                    Materias.Add(new Materia {NombreMateria = "No se encontraron materias disponibles para asignar."});
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
    }
}
