function validarfrmmenuaplicacion()
{

	if (frmmenuaplicacion.txtdescripcion_men.value == "")
	{
	alert("Por favor ingrese el campo descripcion_Men");
	frmmenuaplicacion.txtdescripcion_men.focus();
	return (false);
	}

	if (frmmenuaplicacion.txtorden_men.value == "")
	{
	alert("Por favor ingrese el campo orden_men");
	frmmenuaplicacion.txtorden_men.focus();
	return (false);
	}

	if (frmmenuaplicacion.txtvariable_men.value == "")
	{
	alert("Por favor ingrese el campo variable_men");
	frmmenuaplicacion.txtvariable_men.focus();
	return (false);
	}

return (true);
}