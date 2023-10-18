
var urlActual = window.location.href;

window.onload = () => RecuperarMateriasAsignadas();

function RecuperarMateriasAsignadas() {
    let contenedor = document.getElementById("contenedorMateriasAsignadas");

    fetch(`${urlActual}/ObtenerMateriasAsignadas`)
        .then(response => {
            if (!response.ok) {
                throw new Error("Error al asignar la materia.");
            }
            return response.json();
        })
        .then(data => {   
            console.log(data);
            document.getElementById("IdAlumnoHidden").value = data[0].idAlumno;
            for (let i = 0; i < data.length; i++) {
            contenedor.appendChild(crearNuevoBotonMateria(data[i].idMateria, data[i].nombreMateria));
            }            
        })
        .catch(error => {
            alert(`Ocurrió un error en el proceso: ${error}`);
            console.log(`Ocurrió un error en el proceso: ${error}`);
        });
}

const AsignarMateria = (idMateria, nombreMateria, contenedor) => {

    let respuesta = "";

    fetch(`${urlActual}/AsignarMateria?IdMateria=${idMateria}`)
        .then(response => {
            if (!response.ok) {
                throw new Error("Error al asignar la materia.");
            }
            return response.json();
        })
        .then(data => {
            alert(data);
            if (data == "La materia ya se encuentra asignada.") {
                respuesta = "0";
            }
            else {
                contenedor.appendChild(crearNuevoBotonMateria(idMateria, nombreMateria));
            }
        })
        .catch(error => {
            alert(`Ocurrió un error en el proceso: ${error}`);
            console.log(`Ocurrió un error en el proceso: ${error}`);
        });
    return respuesta;
}

const RemoverMateriaAsignada = (idMateria, idAlumno) => {

    var confirmacion = confirm("¿Desea remover la materia?");

    if (confirmacion) {
        fetch(`${urlActual}/RemoverMateriaAsignada?IdMateria=${idMateria}&IdAlumno=${idAlumno}`)
            .then(response => {
                if (!response.ok) {
                    throw new Error("Error al asignar la materia.");
                }
                return response.json();
            })
            .then(data => {
                alert(data);     
                location.reload()
                console.log(data);                
            })
            .catch(error => {
                alert(`Ocurrió un error en el proceso: ${error}`);
                console.log(`Ocurrió un error en el proceso: ${error}`);
            });
    } 
}

const MostrarDetalles = (IdMateria,idAlumno) => {
    fetch(`${urlActual}/MostrarDetallesMateria?IdMateria=${IdMateria}&IdAlumno=${idAlumno}`)
        .then(response => {
            if (!response.ok) {
                throw new Error("Error al asignar la materia.");
            }
            return response.json();
        })
        .then(data => {
            console.log(data);
            MostrarDetallesMateriaModal(data.calificacion, data.nombreMateria, data.idMateria);
        })
        .catch(error => {
            alert(`Ocurrió un error en el proceso: ${error}`);
            console.log(`Ocurrió un error en el proceso: ${error}`);
        });
}










