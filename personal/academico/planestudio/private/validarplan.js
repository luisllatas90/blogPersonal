function ExportarPlan(codigo_cpf,codigo_pes,nombre_cpf,nombre_pes)
{
	location.href="xlsexportarplan.asp?codigo_cpf=" + codigo_cpf + "&codigo_pes=" + codigo_pes + "&nombre_cpf=" + nombre_cpf + "&nombre_pes=" + nombre_pes
}

function AbrirPlan(modo)
{
	var cpf=document.all.cboEscuela
	var ncpf=cpf.options[cpf.selectedIndex].text
	   switch (modo)
	   {
		case "A":
			AbrirPopUp("frmplan.asp?accion=agregarplanestudio&codigo_cpf=" + cpf + "&nombre_cpf=" + ncpf)
			break

		case "M":
			var cpl=document.all.cboPlan.value
			if (cpl!="-2"){
				AbrirPopUp("frmplan.asp?accion=modificarplanestudio&codigo_cpf=" + cpf + "&nombre_cpf=" + ncpf + "&codigo_pes=" + cpl)
			}
			else{
				alert("Seleccione el Plan de Estudio")
			}
			break
	   }
}


function ActualizarlistaPlan(modo)
{
	var cpf=document.all.cboEscuela
	var ncpf=cpf.options[cpf.selectedIndex].text
	var cpl="-2"
	var dcpl=""

	if (modo=="P"){
		cpl=document.all.cboPlan
		dcpl=""
		if (cpl!=undefined){
			cpl=cpl.value
			dcpl=document.all.cboPlan.options[document.all.cboPlan.selectedIndex].text
		}
	}

	location.href="index.asp?codigo_cpf=" + cpf.value + "&nombre_cpf=" + ncpf + "&codigo_pes=" + cpl + "&descripcion_pes=" + dcpl

}

function Procesarcursos(modo,frm,codigo_pes)
{
	var accion=""

	switch (modo)
	{
		case "G":
			accion="procesar.asp?accion=modificarplancurso&codigo_pes=" + codigo_pes
			break
			
		case "A":
			accion="procesar.asp?accion=cambiarvigenciacurso&estado=1&codigo_pes=" + codigo_pes
			break
		case "D":
			accion="procesar.asp?accion=cambiarvigenciacurso&estado=0&codigo_pes=" + codigo_pes			
			break
	}

	frm.action=accion
	frm.submit()
}

function CalcularTotalhoras(id)
{
	var total=0

	var celda=document.getElementById("fila" + id).cells
	var ht=document.getElementById("horasteo_cur" + id).value
	var hp=document.getElementById("horaspra_cur" + id).value
	var hl=document.getElementById("horaslab_cur" + id).value
	var ha=document.getElementById("horasase_cur" + id).value
	var th=document.getElementById("totalhoras_cur" + id)

	total=eval(ht) + eval(hp) + eval(hl) + eval(ha)

	celda[10].innerHTML="<font color='#0000FF'>" + total + "</font>"
	th.value=total

}