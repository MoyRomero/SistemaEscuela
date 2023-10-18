

const MostrarModal = (Id, materia) => {

    let Modal = document.getElementById("ModalMaterias");

    const ContenidoModal =
        `
        <div>
        <button class="btnCerrarModal boton" onclick="cerrarModalMaterias()"> X </button>
        </div>
        <div class="ModalContenedor">
            <div class="EdicionModalEncabezado">
                <h2> OPCIONES PARA MATERIA: ${materia} </h2>
            </div>
            <div class="EdicionModalFormulario">
                <button class="boton"> ASIGNAR MATERIA </button>
            </div>
        </div>
        `;

    Modal.innerHTML = ContenidoModal;
    Modal.style.display = "flex";
}

function MostrarDetallesMateriaModal(calificacion, nombreMateria, idMateria) {
    let Modal = document.getElementById("ModalMaterias");
    const idAlumno = document.getElementById("IdAlumnoHidden").value;

    let contenido =
        `
        <div>
        <button class="btnCerrarModal boton" onclick="cerrarModalMaterias()"> X </button>
        </div>                  
        
        <div class="VisualizarMateriaAlumno"> 

            <div class="InformacionMateriaAlumno">
                <span>Materia: ${nombreMateria}</span>
                <span id="CalificacionActual">Calificacion: ${ConfirmarCalificacion(calificacion)}</span>
            </div>

            <div class="FormularioCalificacion">
                <div class="centrarElementos">

                    <div class="GuardarCalificacionBtn">
                        <button id="btnRemoverMateria" class="boton" onclick="RemoverMateriaAsignada(${idMateria},${idAlumno})">Quitar Asignacion</button>
                    </div>
                </div>                
            </div>
                
        </div>`;

    Modal.innerHTML = contenido;
    Modal.style.display = "flex";   

    RevisarCalificacion(document.getElementById("CalificacionActual").textContent, document.getElementById("btnRemoverMateria"));
}

function RevisarCalificacion(calificacion,btnRemover) {
    if (calificacion.includes("Sin calificacion")) {
        return;
    }
    else {
        btnRemover.style.display = "none";
    }
}
RemoverMateriaAsignada
function ConfirmarCalificacion(calificacion) {

    if (calificacion == null) return "Sin calificacion";
    else {
        if (calificacion < 7) return calificacion + " (reprobado)";
        else return calificacion + " (aprobado)";
    }
}

const cerrarModalMaterias = () =>{
    let Modal = document.getElementById("ModalMaterias");
    Modal.style.display = "none";
    Modal.innerHTML = "";
}

const SeleccionarMateria = (IdMateria, nombreMateria) => {

    const contenedor = document.getElementById("contenedorMateriasAsignadas");
    const confirmacion = confirm(`¿Deseas asignar la materia ${nombreMateria} a la lista de tus materias a cursar?`);

    if (confirmacion) {        
        AsignarMateria(IdMateria, nombreMateria, contenedor);
        window.location.reload;
    }     
}

const GuardarMateriasSeleccionadas = () =>{
    const contenedor = document.getElementById("contenedorMateriasAsignadas");
    if (contenedor.children.length == 0) {
        alert("No se han seleccionado materias para asignar.");
        return;
    }

    else {

    }
    console.log(contenedor.children.length);
}

function crearNuevoBotonMateria(Id, nombre) {

    const idAlumno = document.getElementById("IdAlumnoHidden").value;
    const boton = document.createElement("BUTTON");

    boton.classList.add("boton");
    boton.setAttribute("onclick", `MostrarDetalles(${Id},${idAlumno})`);
    boton.textContent = nombre;
    boton.value = Id;
    return boton;
}


