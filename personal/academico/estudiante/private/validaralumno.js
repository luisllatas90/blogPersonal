var nombreficha=""

function BuscarAlumno(modulo)
{
	if (txtParam.value==""){
		alert("Especifique el parámetro de búsqueda")
		txtParam.focus()
		return(false)
	}
	
	location.href="lstalumnos.asp?resultado=S&param=" + txtParam.value + "&tipobus=" + cboTipoBus.value + "&mod=" + modulo
}

function BuscarAlumnoSinMatricula(modulo) {
    location.href = "lstalumnosSinMatricular.asp?resultado=S&cpf=" + cboCpf.value + "&ccini=" + cboCicloAnterior.value + "&ccfin=" + cboCicloActual.value + "&mod=" + modulo
}

function BuscarAlumno_CuadroMeritos(modulo)
{
	if (txtParam.value==""){
		alert("Especifique el parámetro de búsqueda")
		txtParam.focus()
		return(false)
	}
	
	location.href="lstalumnos_cuadromeritos.asp?resultado=S&param=" + txtParam.value + "&tipobus=" + cboTipoBus.value + "&mod=" + modulo
}
function BuscarAlumnoPP(modulo)
{
	if (txtParam.value==""){
		alert("Especifique el parámetro de búsqueda")
		txtParam.focus()
		return(false)
	}
	
	location.href="lstalumnospe.asp?resultado=S&param=" + txtParam.value + "&tipobus=" + cboTipoBus.value + "&mod=" + modulo
}

function BuscarAlumnoPrevencion(modulo) {
    if (txtParam.value == "") {
        alert("Especifique el parámetro de búsqueda")
        txtParam.focus()
        return (false)
    }

    location.href = "lstalumnosprevencion.asp?resultado=S&param=" + txtParam.value + "&tipobus=1&mod=0"
}

function AbrirFicha(pagina,obj,t,ctf)
{
	var fila=document.getElementById(txtelegido.value)
	nombreficha=obj.innerText
	var codigo_alu=fila.cells[1].innerText
	var codigouniver_alu=fila.cells[2].innerText
	var alumno=fila.cells[3].innerText
	var nombre_cpf=fila.cells[4].innerText
    
    if (t=="1"){
        fradetalle.location.href=pagina + "?id=" + codigo_alu + "&ctf=" + ctf
    }
    else{
        fradetalle.location.href=pagina + "?IncluirDatos=N&actualizoDatos=true&codigo_alu=" + codigo_alu + "&codigouniver_alu=" + codigouniver_alu + "&tipo=E&alumno=" + alumno + "&nombre_cpf=" + nombre_cpf
    }
}

function AbrirFichaPopUp(pagina,obj)
{
	var fila=document.getElementById(txtelegido.value)
	nombreficha=obj.innerText
	var codigo_alu=fila.cells[1].innerText
	var codigouniver_alu=fila.cells[2].innerText
	var alumno=fila.cells[3].innerText
	var nombre_cpf=fila.cells[4].innerText

	AbrirPopUp(pagina + "?IncluirDatos=A&actualizoDatos=true&codigo_alu=" + codigo_alu + "&codigouniver_alu=" + codigouniver_alu + "&tipo=E&alumno=" + alumno + "&nombre_cpf=" + nombre_cpf,"450","750")
}

function ImprimirFicha()
{
	fradetalle.document.title=nombreficha
	fradetalle.focus();
	fradetalle.print();
}