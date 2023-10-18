const rolSelect = document.getElementById("rolSelect");
var urlActual = window.location.href;

function AgregarUsuario() {

    const Email = document.getElementById("Emailinput").value;
    const Pass = document.getElementById("PasswrdInput").value;
    const IdRol = document.getElementById("rolSelect").value;

    console.log(IdRol);

    const respuesta = confirm("¿desea agregar el usuario?");

    if (respuesta) {
        fetch(`${urlActual}/RegistrarUsuario?Email=${Email}&Pass=${Pass}&IdRol=${IdRol}`)
            .then(response => {
                if (!response.ok) {
                    throw new Error("Error al asignar la materia.");
                }
                return response.json();
            })
            .then(data => {
                alert(data);
                location.reload();
            })
            .catch(error => {
                alert(`Ocurrió un error en el proceso: ${error}`);
                console.log(`Ocurrió un error en el proceso: ${error}`);
            });
    }    
}