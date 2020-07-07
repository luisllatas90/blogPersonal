<%
Dim mostrarScript

estadomatricula=request.QueryString("estadomatricula")

if estadomatricula="previo" then
	mostrarScript="MostrarHorarioCursosElegido()"
	titulo="HORARIO DE CURSOS ELEGIDOS (" & descripcionCac & ")"
else
	mostrarScript="MostrarHorarioCursosMatriculados()"
	titulo="HORARIO DE CURSOS MATRICULADOS (" & descripcionCac & ")"
	end if
%>
<HTML>
	<HEAD>
		<title>HORARIO DE CURSOS ELEGIDOS</title>
		
		<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">
		<script language="JavaScript" src="../../../../private/funciones.js"></script>
		<script language="JavaScript" src="private/validarhorario.js"></script>
	    <style>
<!--
table        { border-style: solid; border-width: 1;}
td           { border: 1px solid #EEEEEE; font-size:8pt }
-->
        </style>
	</HEAD>
	<body onLoad="<%=mostrarScript%>">
		<p align="right"><span class="NoImprimir">
		  <input onClick="imprimir('N')" type="button" value="    Imprimir" name="cmdImprimir" class="usatimprimir">
		</span></p>
		<TABLE id="tblhorario" width="100%" style="border-collapse: collapse" bordercolor="#111111" cellpadding="3" cellspacing="0">
			<TR class="usatPPS">
				<TD width="18%" height="28">HORA</TD>
				<TD width="12%" height="28">LUNES</TD>
				<TD width="12%" height="28">MARTES</TD>
				<TD width="12%" height="28">MIERCOLES</TD>
				<TD width="12%" height="28">JUEVES</TD>
				<TD width="12%" height="28">VIERNES</TD>
				<TD width="12%" height="28">SABADO</TD>
			</TR>
			<TR>
				<TD width="18%">07:00 am - 07:50am</TD>
				<TD id="LU07" align="middle" width="12%">&nbsp;</TD>
				<TD id="MA07" align="middle" width="12%">&nbsp;</TD>
				<TD id="MI07" align="middle" width="12%">&nbsp;</TD>
				<TD id="JU07" align="middle" width="12%">&nbsp;</TD>
				<TD id="VI07" align="middle" width="12%">&nbsp;</TD>
				<TD id="SA07" align="middle" width="12%">&nbsp;</TD>
			</TR>
			<TR>
				<TD width="18%">08:00am - 08:50am</TD>
				<TD id="LU08" align="middle" width="12%"></TD>
				<TD id="MA08" align="middle" width="12%"></TD>
				<TD id="MI08" align="middle" width="12%"></TD>
				<TD id="JU08" align="middle" width="12%"></TD>
				<TD id="VI08" align="middle" width="12%"></TD>
				<TD id="SA08" align="middle" width="12%"></TD>
			</TR>
			<TR>
				<TD width="18%">09:00am - 09:50am</TD>
				<TD id="LU09" align="middle" width="12%"></TD>
				<TD id="MA09" align="middle" width="12%"></TD>
				<TD id="MI09" align="middle" width="12%"></TD>
				<TD id="JU09" align="middle" width="12%"></TD>
				<TD id="VI09" align="middle" width="12%"></TD>
				<TD id="SA09" align="middle" width="12%"></TD>
			</TR>
			<TR>
				<TD width="18%">10:00pm - 10:50am</TD>
				<TD id="LU10" align="middle" width="12%"></TD>
				<TD id="MA10" align="middle" width="12%"></TD>
				<TD id="MI10" align="middle" width="12%"></TD>
				<TD id="JU10" align="middle" width="12%"></TD>
				<TD id="VI10" align="middle" width="12%"></TD>
				<TD id="SA10" align="middle" width="12%"></TD>
			</TR>
			<TR>
				<TD width="18%">11:00am - 11:50am</TD>
				<TD id="LU11" align="middle" width="12%"></TD>
				<TD id="MA11" align="middle" width="12%"></TD>
				<TD id="MI11" align="middle" width="12%"></TD>
				<TD id="JU11" align="middle" width="12%"></TD>
				<TD id="VI11" align="middle" width="12%"></TD>
				<TD id="SA11" align="middle" width="12%"></TD>
			</TR>
			<TR>
				<TD width="18%">12:00m -  12:50m</TD>
				<TD id="LU12" align="middle" width="12%"></TD>
				<TD id="MA12" align="middle" width="12%"></TD>
				<TD id="MI12" align="middle" width="12%"></TD>
				<TD id="JU12" align="middle" width="12%"></TD>
				<TD id="VI12" align="middle" width="12%"></TD>
				<TD id="SA12" align="middle" width="12%"></TD>
			</TR>
			<TR>
				<TD width="18%">01:00pm - 01:50pm</TD>
				<TD id="LU13" align="middle" width="12%">&nbsp;</TD>
				<TD id="MA13" align="middle" width="12%">&nbsp;</TD>
				<TD id="MI13" align="middle" width="12%">&nbsp;</TD>
				<TD id="JU13" align="middle" width="12%">&nbsp;</TD>
				<TD id="VI13" align="middle" width="12%">&nbsp;</TD>
				<TD id="SA13" align="middle" width="12%">&nbsp;</TD>
			</TR>
			<TR>
				<TD width="18%">03:00pm - 03:50pm</TD>
				<TD id="LU15" align="middle" width="12%"></TD>
				<TD id="MA15" align="middle" width="12%"></TD>
				<TD id="MI15" align="middle" width="12%"></TD>
				<TD id="JU15" align="middle" width="12%"></TD>
				<TD id="VI15" align="middle" width="12%"></TD>
				<TD id="SA15" align="middle" width="12%"></TD>
			</TR>
			<TR>
				<TD width="18%">04:00pm - 04:50pm</TD>
				<TD id="LU16" align="middle" width="12%"></TD>
				<TD id="MA16" align="middle" width="12%"></TD>
				<TD id="MI16" align="middle" width="12%"></TD>
				<TD id="JU16" align="middle" width="12%"></TD>
				<TD id="VI16" align="middle" width="12%"></TD>
				<TD id="SA16" align="middle" width="12%"></TD>
			</TR>
			<TR>
				<TD width="18%">05:00pm - 05:50pm</TD>
				<TD id="LU17" align="middle" width="12%"></TD>
				<TD id="MA17" align="middle" width="12%"></TD>
				<TD id="MI17" align="middle" width="12%"></TD>
				<TD id="JU17" align="middle" width="12%"></TD>
				<TD id="VI17" align="middle" width="12%"></TD>
				<TD id="SA17" align="middle" width="12%"></TD>
			</TR>
			<TR>
				<TD width="18%">06:00pm - 06:50pm</TD>
				<TD id="LU18" align="middle" width="12%"></TD>
				<TD id="MA18" align="middle" width="12%"></TD>
				<TD id="MI18" align="middle" width="12%"></TD>
				<TD id="JU18" align="middle" width="12%"></TD>
				<TD id="VI18" align="middle" width="12%"></TD>
				<TD id="SA18" align="middle" width="12%"></TD>
			</TR>
			<TR>
				<TD width="18%">07:00pm - 07:50pm</TD>
				<TD id="LU19" align="middle" width="12%"></TD>
				<TD id="MA19" align="middle" width="12%"></TD>
				<TD id="MI19" align="middle" width="12%"></TD>
				<TD id="JU19" align="middle" width="12%"></TD>
				<TD id="VI19" align="middle" width="12%"></TD>
				<TD id="SA19" align="middle" width="12%"></TD>
			</TR>
			<TR>
				<TD width="18%">08:00pm - 08:50pm</TD>
				<TD id="LU20" align="middle" width="12%"></TD>
				<TD id="MA20" align="middle" width="12%"></TD>
				<TD id="MI20" align="middle" width="12%"></TD>
				<TD id="JU20" align="middle" width="12%"></TD>
				<TD id="VI20" align="middle" width="12%"></TD>
				<TD id="SA20" align="middle" width="12%"></TD>
			</TR>
	</TABLE>
    </body>
</HTML>