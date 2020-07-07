<HTML>
	<HEAD>
		<Title>Acceso al Sistema</Title>
		<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
			<script language="JavaScript">
			function ResaltarBoton(boton)
			{
				if (boton==1)
					{
					img1.src="images/on1.jpg"
					img2.src="images/off2.jpg"
					<%if session("codigo_tfu")=1 then%>
					img3.src="images/off3.jpg"
					<%end if%>
					img4.src="images/off4.jpg"
					}

				if (boton==2)
					{
					img2.src="images/on2.jpg"
					img1.src="images/off1.jpg"
					<%if session("codigo_tfu")=1 then%>
					img3.src="images/off3.jpg"
					<%end if%>
					img4.src="images/off4.jpg"
					}
			
				if (boton==3)
					{
					img3.src="images/on3.jpg"
					img1.src="images/off1.jpg"
					img2.src="images/off2.jpg"
					img4.src="images/off4.jpg"
					}
			}
			
			
			function cerrarSistema()
			{
				window.open("../cerrar.asp?Decision=Si","cerrandoSistema","Width=150,height=80,statusbar=no,scrollbars=no,top=100,left=100,resizable=no,toolbar=no,menubar=no")
			}
			</script>
			<base target="Contenido"> 
	</HEAD>
	<BODY topmargin="0" leftmargin="0" class="menusuperior" onUnload="cerrarSistema()">
		<table width="100%" cellpadding="0" cellspacing="0" height="100%" style="border-collapse: collapse" bordercolor="#111111">
			<tr>
				<td width="25%" rowspan="4"><img src="../../../images/logo.jpg"></td>
			</tr>
			<tr>
				<td width="77%" class="e1" height="40" align="left">Modelo de Acreditación: <%=session("titulomodelo")%></td>
			</tr>
			<tr valign="bottom">
				<td><a href="listasecciones.asp">
                <img id="img1" onClick="ResaltarBoton('1')" src="images/on1.jpg" border="0"></a><a href="listatareas.asp"><img id="img2" border="0" onClick="ResaltarBoton('2')" src="images/off2.jpg"></a><%if session("codigo_tfu")=1 then%><a href="modeloacreditacion.asp"><img id="img3" border="0" onClick="ResaltarBoton('3')" src="images/off3.jpg"></a><%end if%><a href="Javascript:top.window.close()"><img id="img4" border="0" src="images/off4.jpg"></a></td>
			</tr>
		</table>
	</BODY>
</HTML>