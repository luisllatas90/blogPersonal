<%
'---------------------------------------------------------------------------------
'CV-USAT
'Fecha de Creaci�n: 17/01/2006
'Autor			: Gerardo Chunga Chinguel
'Observaciones	: Permite enviar el n�mero de mensaje y recuperarlo en script
'---------------------------------------------------------------------------------

  function MensajeCliente(byVal num,ByVal otroscript)
  	Dim mensaje,tipoicono
  	select case num
  		case "1"
  			mensaje="El estudiante no Existe en la Base de Datos"

  		case "2"
  			mensaje="Los datos han sido guardados correctamente\n\n" & _
					"Ac�rquese a la Oficina de Evaluaci�n y Registro para firmar el Acta correspondiente"

  		case "3"
  			mensaje="Se ha habilitado correctamente el llenado de registro de Notas"
  		
  		case "4"
  			mensaje="Si desea modificar alguna nota, ac�rquese a la Oficina de Evaluaci�n y Registro"
  		
  		case "5"
  			mensaje="Se ha producido un error al guardar la matr�cula\n Intente denuevo"
  		
  		case "6"
  			mensaje="Los cursos seleccionados se programaron correctamente"

  		case "7"
  			mensaje="Se ha producido un error al guardar la Notas\n Por favor intente m�s tarde"
	end select
	MensajeCliente="<script>alert('" & mensaje & "');" & otroscript & "</script>"
  End function


  function CerrarPopUp()
	CerrarPopUp="<script>window.opener.location.reload();window.close()</script>"
  End function
%>