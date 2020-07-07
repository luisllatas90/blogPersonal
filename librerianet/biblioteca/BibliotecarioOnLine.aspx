<%@ Page Language="VB" AutoEventWireup="false" CodeFile="BibliotecarioOnLine.aspx.vb" Inherits="librerianet_biblioteca_BibliotecarioOnLine" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<meta http-equiv="content-language" content="en" />
	<meta http-equiv="content-type" content="text/html;charset=utf-8" />
	<meta name="copyright" content="R. Schoo" />
	<title>Bibliotecario en Linea</title>
    <style type="text/css" >

        body.pag_info{
	        MARGIN: 0px;

	        background-color: #FFFFFF;
	        background-image:none;
	        height:100%;
        }
        .Estilo1 {
	        color: #CC0000;
	        font-weight: bold;
        }
        .Estilo4 {
	        font-size: 14px;
	        font-style: italic;
        }
        td.titulo_14{
	        text-align:center;
	        text-transform:uppercase;
	        font-weight:bold;
	        font-size:14px;
	        font-family: Arial, Helvetica, sans-serif;
	        padding: 10px;
	        padding-top:30px;
	        border-bottom:#666666 1px solid;
        }
        .cuadrado_1{
	        border-top:#CCCCCC 1px solid;
	        border-bottom: #CCCCCC 1px solid;
	        border-left: #CCCCCC 1px solid;
	        border-right: #CCCCCC 1px solid;
	        padding:20px;
        }
        td, th {
	        font-family: Arial, Helvetica, sans-serif;
	        font-size: 12px;
	        /*line-height: 12px;*/
	        color: #333333;
        }
        .ima_banner{
        background:url(../images/banner_cabecera.png) #EDCC61 no-repeat;
        }

    </style>
    <script language="javascript" type="text/javascript">
        function validarnumero() {
            if (event.keyCode < 45 || event.keyCode > 57)
            { event.returnValue = false }
        }
 
        function validarEmail(valor) {
            if (/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/.test(valor)) {
                //  alert("La dirección de email " + valor    + " es correcta.") 
                return (true)
            } else {
                //  alert("La dirección de email es incorrecta.");
                return (false);
            }
        }
        function ValidarFormulario() {
            var msj;
            msj = "";
            if (document.form1.nombre2.value == "") {
                msj = "Nombres y apellidos";
            }
            if (document.form1.DNI.value == "") {
                if (msj == "")
                    msj = "DNI";
                else
                    msj = msj + ", DNI";
            }
            if (validarEmail(document.all.email.value) == false) {
                if (msj == "")
                    msj = "Correo electrónico";
                else
                    msj = msj + ", correo electrónico";
            }
            if (document.form1.usuarios.value == "") {
                if (msj == "")
                    msj = "Tipo de usuario";
                else
                    msj = msj + ", tipo de usuario";
            }
            if (document.form1.tema.value == "") {
                if (msj == "")
                    msj = "Tema de consulta";
                else
                    msj = msj + ", tema de consulta";
            }
            if (document.form1.consulta.value == "") {
                if (msj == "")
                    msj = "Detalle de la consulta";
                else
                    msj = msj + ", detalle de la consulta";
            }
            if (msj == "") {
                form1.submit();
            }
            else {
                if (msj.lastIndexOf(',') > 0)
                    alert(msj + " son obligatorios");
                else
                    alert(msj + " es obligatorio");
                event.returnValue = false;
            }
        }
        
    </script>
</head>
<body class="pag_info">
    <form id="form1" runat="server">
 <table width="680" align="center">
<tr>
<td class="cuadrado_1">
	<table width="100%">
		<tr>
			<td class="ima_banner" height="80"></td>
		</tr>
		<tr>
			<td class="titulo_14">Bibliotecario en Línea</td>
		</tr>
		<tr>
			<td align="justify"><p>El servicio brinda apoyo y asistencia en las investigaciones y necesidades de información de nuestra comunidad universitaria, mediante correo electrónico o formulario web.</p>
			<p>Las respuestas a sus consultas se enviarán como máximo dentro de 48 horas, excepto los fines de semana y días festivos.</p>
            </td>
		</tr>
			<tr>
			<td height="45" colspan="2">
				<p align="center"><strong>Utilice este formulario para enviar su consulta al Bibliotecario en l&iacute;nea.</strong></p>
			</td>
		</tr>
		<tr>
			<td>
			<form action="BibliotecarioOnLine.aspx" method="post" enctype="multipart/form-data" name="form1" >
				<table width="100%">
					<tr>
						<td valign="top"><strong>Nombres y apellidos:</strong></td>
						<td valign="top">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                ControlToValidate="txtNombres" 
                                ErrorMessage="Nombres y apellidos es obligatorio" ValidationGroup="Enviar">*</asp:RequiredFieldValidator>
                        </td>
						<td>
                            <asp:TextBox ID="txtNombres" runat="server"></asp:TextBox>
                        </td>
					</tr>
					<tr>
						<td valign="top"><strong>DNI:</strong></td>
						<td valign="top">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                ControlToValidate="txtDni" ErrorMessage="El nro de DNI es obligatorio" 
                                ValidationGroup="Enviar">*</asp:RequiredFieldValidator>
                        </td>
						<td>
                            <asp:TextBox ID="txtDni" runat="server" MaxLength="10"></asp:TextBox>
                        </td>
					</tr>
					<tr>
						<td valign="top"> <strong>Correo Electrónico:</strong></td>
						<td valign="top"> 
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                ControlToValidate="txtEmail" ErrorMessage="El correo electrónico es obligatorio" 
                                ValidationGroup="Enviar">*</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                ControlToValidate="txtEmail" 
                                ErrorMessage="El correo electrónico es obligatorio" 
                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                                ValidationGroup="Enviar">*</asp:RegularExpressionValidator>
                            </td>
						<td>
                            <asp:TextBox ID="txtEmail" runat="server" Width="202px"></asp:TextBox>
                            </br>Ej.: nombre@mail.com </td>
					</tr>
					<tr>
						<td valign="top"><strong>Tipo de usuario:</strong></td>
						<td valign="top">
                            <asp:CompareValidator ID="CompareValidator1" runat="server" 
                                ErrorMessage="Seleccione su tipo de usuario" Operator="GreaterThan" 
                                ValueToCompare="0" ControlToValidate="cboTipoUsuario" 
                                ValidationGroup="Enviar">*</asp:CompareValidator>
						</td>
						<td>
							<asp:DropDownList ID="cboTipoUsuario" runat="server">
                                <asp:ListItem Value="0">&lt;&lt;Seleccionar&gt;&gt;</asp:ListItem>
                                <asp:ListItem Value="1">Estudiante Pre-Grado</asp:ListItem>
                                <asp:ListItem Value="2">Estudiante Profesionalización</asp:ListItem>
                                <asp:ListItem Value="3">Estudiante Post-Grado</asp:ListItem>
                                <asp:ListItem Value="4">Docente</asp:ListItem>
                                <asp:ListItem Value="5">Administrativo</asp:ListItem>
                            </asp:DropDownList>
&nbsp;</td>
					</tr>
					<tr>
						<td valign="top"><strong>Tema de consulta:</strong></td>
						<td valign="top">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                ControlToValidate="txtTemaConsulta" 
                                ErrorMessage="El tema de consulta es obligatorio" ValidationGroup="Enviar">*</asp:RequiredFieldValidator>
                        </td>
						<td>
                            <asp:TextBox ID="txtTemaConsulta" runat="server"></asp:TextBox>
                        </td>
					</tr>
					<tr>
						<td valign="top"><b>Su consulta:</b></td>
						<td valign="top">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                ControlToValidate="txtConsulta" 
                                ErrorMessage="Su consulta es obligatorio" ValidationGroup="Enviar">*</asp:RequiredFieldValidator>
                        </td>
						<td>
                            <asp:TextBox ID="txtConsulta" runat="server" Rows="5" TextMode="MultiLine" 
                                Width="99%"></asp:TextBox>
                        </td>
					</tr>
					<tr>
						<td colspan="3" align="center">
                            <asp:Button ID="cmdEnviar" runat="server" Text="Enviar" 
                                ValidationGroup="Enviar" />
&nbsp;<asp:Button ID="cmdLimpiar" runat="server" Text="Limpiar" />
                        </td>
					</tr>
					</table>
				</form>
			</td>
		</tr>
	</table>
</td>
</tr>
<tr>
	<td align="center"><a href="politicas.pdf" target="_blank">Políticas del servicio</a></td>
</tr>
</table>

     <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
         ShowMessageBox="True" ShowSummary="False" ValidationGroup="Enviar" />

    </form>
</body>

</html>
