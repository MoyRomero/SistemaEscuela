
var urlActual = window.location.href;

const MostrarAlumnos = (idMateria) => {

    fetch(`${urlActual}/ObtenerAlumnos?idMateria=${idMateria}`)
        .then(response => {
            if (!response.ok) {
                throw new Error("Error al asignar la materia.");
            }
            return response.json();
        })
        .then(data => {
            console.log(data);
            RellenarModalAlumnos(data)
        })
        .catch(error => {
            alert(`Ocurrió un error en el proceso: ${error}`);
            console.log(`Ocurrió un error en el proceso: ${error}`);
        });
}

function RellenarModalAlumnos(data) {

    let Modal = document.getElementById("ModalMaterias");

    let contenido =
        `
        <div>
        <button class="btnCerrarModal boton" onclick="cerrarModalMaterias()"> X </button>
        </div>                   
        `;
    for (let i = 0; i < data.length; i++) {
        contenido +=
            `
            <div class="VisualizarMateriaAlumno"> 

                <div class="InformacionMateriaAlumno">
                    <span>Alumno: ${data[i].nombreAlumno} ${data[i].apellidoPaterno} ${data[i].apellidoMaterno} </span>
                    <span>Materia: ${data[i].nombreMateria}</span>
                    <span>Calificacion: ${ConfirmarCalificacion(data[i].calificacion)}</span>
                </div>

                <div class="FormularioCalificacion">
                    <div class="centrarElementos">
                    
                        <span>Calificar</span>
                    
                        <div class="inputCalificacion">
                            <input type="number" id="calificacionTxtBox" oninput="validarNumero()">
                        </div>
                        <div class="GuardarCalificacionBtn">
                            <button class="boton" onclick="EnviarCalificacion(${data[i].idMateria},${data[i].idAlumno})"> Guardar </button>
                        </div>
                    </div>                
                </div> 
                
            </div>
        `;
    }

    Modal.innerHTML = contenido;
    Modal.style.display = "flex";
}

function EnviarCalificacion(idMateria, idAlumno){

    const calificacion = document.getElementById("calificacionTxtBox").value;

    fetch(`${urlActual}/EnviarCalificacion?idMateria=${idMateria}&idAlumno=${idAlumno}&calificacion=${calificacion}`)
        .then(response => {
            if (!response.ok) {
                throw new Error("Error al asignar la materia.");
            }
            return response.json();
        })
        .then(data => {
            alert(data);
            console.log(data);            
        })
        .catch(error => {
            alert(`Ocurrió un error en el proceso: ${error}`);
            console.log(`Ocurrió un error en el proceso: ${error}`);
        });
}
function ConfirmarCalificacion(calificacion) {

    if (calificacion == null) return "Sin calificacion";
    else {
        if (calificacion < 7) return calificacion + " (reprobado)";
        else return calificacion + " (aprobado)";
    }
}
function validarNumero() {
    var input = document.getElementById("calificacionTxtBox");
    var valor = parseFloat(input.value);

    if (isNaN(valor)) {
        alert("Ingrese un número válido.");
        input.value = "";
    } else if (valor < 0 || valor > 10) {
        alert("El número debe estar entre 0 y 10.");
        input.value = "";
    } 
}

const cerrarModalMaterias = () => {
    let Modal = document.getElementById("ModalMaterias");
    Modal.style.display = "none";
    Modal.innerHTML = "";
}