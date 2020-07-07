function BuscarCliente()
{
	if (txtParam.value==""){
		alert("Especifique el parámetro de búsqueda")
		txtParam.focus()
		return(false)
	}
	
	if (cboTipoResp.value=="N"){
		alert("Especifique el Tipo de usuario")
		cboTipoResp.focus()
		return(false)
	}

	location.href="frmbuscardeuda.asp?param=" + txtParam.value + "&tipobus=" + cboTipoBus.value + "&tipoResp=" + cboTipoResp.value
}

function VerDeuda(tipo,fila)
{
	var celda=fila.getElementsByTagName('td')
	var ID=eval(celda[1].innerText)
	location.href="detalledeuda.asp?tipo=" + tipo + "&ID=" + ID
}


