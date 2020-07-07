<%@ Page Language="VB" AutoEventWireup="false" CodeFile="estextranjero.aspx.vb" Inherits="librerianet_estudiantesextranjeros_estextranjero" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Página sin título</title>
    <style type="text/css">

        .style1
        {
            background-color: #9999FF;
            font-size: medium;
            font-weight: 700;
            text-align: center;
        }
        .style4        { width: 251px; }
        #txtApellidos  { width: 380px; }
        #txtNombres    { width: 380px; }
        #txtEmail      { width: 380px; }
        #txtDireccionPermanente   {  width: 380px; }
        #txtLocalidad    { width: 380px;}
        #txtPais  {width: 380px; }
        #txtUniversidadOrigen {width: 380px;}
        #txtPaisUniversidad { width: 380px;}
        #txtFacultad { width: 380px; }
        #txtGpoSanguineo {width: 380px;}
        #TextArea1 {width: 380px;height: 72px;}
        #Button1 {height: 26px;}
        #txtNombres0 {width: 380px;}
        </style>
        
<style type="text/css">
  .kclass1
  {
  font: 10pt Verdana;
 color: black;
 text-decoration:none;
}
.kdiv
{
background:#DCFFFF;
}
table
{
background : #DCFFFF;
width:127%; 
font-family: Arial; font-size: 11pt;
}
body
{ background : #DCFFFF;
}
input { border: 1px solid #9999FF; padding: 0; background-color: #D2F0FF }
textarea     { border: 1px solid #99CCFF; padding: 0; background-color: #BBDDFF }
select       { border: 1px solid #9FE0FF; padding-left: 4px; padding-right: 4px; 
               padding-top: 1px; padding-bottom: 1px; background-color: 
               #BFEBFF }
    .style6
    {
    }
    .style7
    {
    }
    .style9
    {
        width: 158px;
    }
    .style10
    {
        width: 472px;
    }
    .style11
    {
        width: 251px;
        color: #FFFFFF;
        font-weight: bold;
    }
</style>        
        
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table>
    <tr>
    <td class="style1" >
    <p align="center"><b><font size="4" color="#800000">PIMEU</font> - Programa 
Internacional de Movilidad de Estudiantes - USAT<br>
Dirección de Relaciones Internacionales</b></td>
    </tr>
    </table>
    
        <table cellpadding="0" cellspacing="0" width="944" >
<tr height="30px" valign="middle">

<td class="style1" colspan="2" bgcolor="#99CCFF">
    <p align="center"><b>Registrar Alumnos Extranjeros - Modulo RI </b> </td>
</tr>
<tr>
<td class="style4" width="904" colspan="2" bgcolor="#609EFD" height="23">
<font color="#FFFFFF"><b>&nbsp;&gt;&gt; Datos Personales</b></font></td>
</tr>
<tr>
<td class="style6">
Carné </td>
<td class="style7" colspan="1" >
<asp:TextBox ID="txtcarnet" runat="server"></asp:TextBox>
</td>
</tr>


<tr>
<td class="style6">
Apellidos&nbsp; y Nombres</td>
<td class="style7">
<asp:TextBox runat="server" Height="18px" Width="235px" ID="txtapellidos"></asp:TextBox>
<asp:TextBox runat="server" Height="18px" Width="235px" ID="txtnombres"></asp:TextBox>
</td>
</tr>


<tr>
<td class="style6">
Fecha Nacimiento </td>
<td class="style7">
<asp:TextBox ID="txtfechaNacimiento" runat="server"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
Sexo&nbsp;
<asp:DropDownList ID="cbosexo" runat="server">
<asp:ListItem >M</asp:ListItem>
<asp:ListItem >F</asp:ListItem>
</asp:DropDownList>
&nbsp;Convenio&nbsp;
<asp:DropDownList ID="cboconvenio" runat="server">
<asp:ListItem >S</asp:ListItem>
<asp:ListItem >N</asp:ListItem>
</asp:DropDownList>
</td>
</tr>


<tr>
<td class="style6">
Pasaporte </td>
<td class="style7">
<asp:TextBox ID="txtpasaporte" runat="server"></asp:TextBox>
&nbsp;Nacionalidad&nbsp; 
<asp:TextBox runat="server" Height="18px" Width="150px" ID="txtnacionalidad"></asp:TextBox>
</td>
</tr>


<tr>
<td class="style6" height="19">
País de residencia</td>
<td class="style7" height="19">
<asp:TextBox ID="txtpais_residencia" runat="server"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    Localidad&nbsp;
<asp:TextBox runat="server" Height="18px" Width="150px" ID="txtlocalidad"></asp:TextBox>
</td>
</tr>


<tr>
<td class="style6" height="19">
    Dirección Permanente</td>
<td class="style7" height="19">
<asp:TextBox runat="server" Height="18px" Width="369px" ID="txtdireccionPermanente"></asp:TextBox>
&nbsp;&nbsp;&nbsp; Zip code <asp:TextBox runat="server" Height="18px" Width="100px" 
        ID="txtzipcode"></asp:TextBox>
</td>
</tr>


<tr>
<td class="style6">
    eMail del estudiante</td>
<td class="style7">
    <asp:TextBox runat="server" Height="18px" Width="367px" ID="txteMail_Alu"></asp:TextBox>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
        ControlToValidate="txteMail_Alu" 
        ErrorMessage="Verificar el correo del estudiante" SetFocusOnError="True" 
        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>

    &nbsp; Teléfono&nbsp; 
    <asp:TextBox ID="txttelefonos" runat="server" Width="98px"></asp:TextBox>
    </td>
</tr>


<tr>
<td class="style6">
Gpo.Sangre y alergias</td>
<td class="style7">
<asp:TextBox runat="server" Height="18px" Width="368px" ID="txtgrupoSangineo"></asp:TextBox>
</td>
</tr>


<tr>
<td class="style4" width="944" colspan="2" bordercolor="#609EFD" bgcolor="#609EFD">
<font color="#FFFFFF"><b>&gt;&gt; Datos Académicos</b></font></td>
</tr>


<tr>
<td class="style6">
        Universidad de Origen </td>
<td class="style7">
<asp:TextBox ID="txtuniversidadOrigen" runat="server" Width="368px"></asp:TextBox></td>
</tr>


<tr>
<td class="style6">
        País de la Universidad</td>
<td class="style7">
<asp:TextBox ID="txtpaisUniversidadOrigen" runat="server" Width="367px"></asp:TextBox></td>
</tr>


<tr>
<td class="style6">
Área de estudio /Faculty</td>
<td class="style7">
<asp:TextBox ID="txtfaculty" runat="server"></asp:TextBox>&nbsp; Semestre&nbsp;&nbsp; 
<asp:TextBox ID="txtsemetreActual" runat="server"></asp:TextBox></td>
</tr>


<tr>
<td class="style11" height="19" bgcolor="#70A7FC" colspan="2">
    &gt;&gt; Estancia Académica en la USAT</td>
</tr>


<tr>
<td class="style6" height="19">
Asig. a Escuela USAT</td>
<td class="style7" height="19">
<asp:TextBox ID="txtescuelausat" runat="server"></asp:TextBox></td>
</tr>


<tr>
<td class="style6" height="19">
    Estancia:&nbsp; Fecha Inicio</td>
<td class="style7" height="19">
<asp:TextBox ID="txtfechaInicial" runat="server"></asp:TextBox>&nbsp; Fecha Final&nbsp; <asp:TextBox ID="txtfechaFinal" runat="server"></asp:TextBox></td>
</tr>


<tr>
<td class="style4" height="19" colspan="2" bgcolor="#609EFD">
<font color="#FFFFFF"><b>&gt;&gt; Datos de Contactos</b></font></td>
</tr>


<tr>
<td class="style4" colspan="2" >
    Contacto de Emergencia</tr>


<tr>
<td class="style4" colspan="2">
<table border="0" cellpadding="0" style="border-collapse: collapse" width="100%" id="table1">
	<tr>
		<td width="210">Nombre</td>
		<td width="210">Parentesco</td>
		<td width="210">eMail</td>
		<td>Teléfono</td>
	</tr>
	<tr>
		<td width="210">
<asp:TextBox runat="server" Height="18px" Width="210px" ID="txtcontactnombre1"></asp:TextBox>
		</td>
		<td width="210">
<asp:TextBox runat="server" Height="18px" Width="210px" ID="txtcontactparentesco1"></asp:TextBox>
		</td>
		<td width="210">
<asp:TextBox runat="server" Height="18px" Width="210px" ID="txtcontactemail1"></asp:TextBox>
		</td>
		<td>
<asp:TextBox runat="server" Height="18px" Width="150px" ID="txtcontacttelefono1"></asp:TextBox>
		</td>
	</tr>
	<tr>
		<td colspan="4">
            Cordinadores Institucionales</td>
	</tr>
	<tr>
		<td width="210">
            Nombre</td>
		<td width="210">
            Universidad</td>
		<td width="210">
            eMail</td>
		<td>
            Cargo</td>
	</tr>
	<tr>
		<td width="210">
<asp:TextBox runat="server" Height="18px" Width="210px" ID="txtcontactnombre2"></asp:TextBox>
		</td>
		<td width="210">
<asp:TextBox runat="server" Height="18px" Width="210px" ID="txtcontactparentesco2"></asp:TextBox>
		</td>
		<td width="210">
<asp:TextBox runat="server" Height="18px" Width="210px" ID="txtcontactemail2"></asp:TextBox>
		</td>
		<td>
<asp:TextBox runat="server" Height="18px" Width="150px" ID="txtcontacttelefono2"></asp:TextBox>
		</td>
	</tr>
	<tr>
		<td width="210">
<asp:TextBox runat="server" Height="18px" Width="210px" ID="txtcontactnombre3"></asp:TextBox>
		</td>
		<td width="210">
<asp:TextBox runat="server" Height="18px" Width="210px" ID="txtcontactparentesco3"></asp:TextBox>
		</td>
		<td width="210">
<asp:TextBox runat="server" Height="18px" Width="210px" ID="txtcontactemail3"></asp:TextBox>
		</td>
		<td>
<asp:TextBox runat="server" Height="18px" Width="150px" ID="txtcontacttelefono3"></asp:TextBox>
		</td>
	</tr>
</table>


</td>
</tr>


<tr>
<td class="style4" width="944" colspan="2" bgcolor="#609EFD">
<font color="#FFFFFF"><b>&gt;&gt; Resultados </b></font> </td>



<tr>
<td class="style10" colspan="2" >
<table border="0" cellpadding="0" 
        style="border-collapse: collapse; width:521px; height: 125px;" id="table1">
	<tr>
		<td width="421">Actividad o Curso </td>
		<td class="style9">Calificativo </td>
	</tr>
	<tr>
		<td width="421">
<asp:TextBox runat="server" Height="18px" Width="410px" ID="txtcurso1"></asp:TextBox>
		</td>
		<td class="style9">
<asp:TextBox runat="server" Height="18px" Width="100px" ID="txtcalificativo1"></asp:TextBox>
		</td>
	</tr>
	<tr>
		<td width="421">
<asp:TextBox runat="server" Height="18px" Width="410px" ID="txtcurso2"></asp:TextBox>
		</td>
		<td class="style9">
<asp:TextBox runat="server" Height="18px" Width="100px" ID="txtcalificativo2"></asp:TextBox>
		</td>
	</tr>
	<tr>
		<td width="421">
<asp:TextBox runat="server" Height="18px" Width="410px" ID="txtcurso3"></asp:TextBox>
		</td>
		<td class="style9">
<asp:TextBox runat="server" Height="18px" Width="100px" ID="txtcalificativo3"></asp:TextBox>
		</td>
	</tr>
	<tr>
		<td width="421">
<asp:TextBox runat="server" Height="18px" Width="410px" ID="txtcurso4"></asp:TextBox>
		</td>
		<td class="style9">
<asp:TextBox runat="server" Height="18px" Width="100px" ID="txtcalificativo4"></asp:TextBox>
		</td>
	</tr>
	<tr>
		<td width="421">
<asp:TextBox runat="server" Height="18px" Width="410px" ID="txtcurso5"></asp:TextBox>
		</td>
		<td class="style9">
<asp:TextBox runat="server" Height="18px" Width="100px" ID="txtcalificativo5"></asp:TextBox>
		</td>
	</tr>
	<tr>
		<td width="421">
<asp:TextBox runat="server" Height="18px" Width="410px" ID="txtcurso6"></asp:TextBox>
		</td>
		<td class="style9">
<asp:TextBox runat="server" Height="18px" Width="100px" ID="txtcalificativo6"></asp:TextBox>
		</td>
	</tr>
</table>
Comentario Final del Alumno <br />
<asp:TextBox ID="txtcomentariofinal" runat="server" Height="97px" 
        TextMode="MultiLine" Width="511px"></asp:TextBox>
                
</td>



</tr>

<tr height="30px" valign="middle" >
<td class="style2" colspan="2" align="left">
    Comentario Coordinador USAT<br />
    <asp:TextBox ID="txtcomentariocoordinador" runat="server" Height="97px" 
        TextMode="MultiLine" Width="509px"></asp:TextBox>
</td>
</tr>


<tr height="30px" valign="middle" >
<td class="style2" colspan="2" align="center">
<asp:Button ID="cmdNuevo" runat="server" Text="Registrar" />
&nbsp;&nbsp;</input>&nbsp;&nbsp;&nbsp;&nbsp;
<asp:Button ID="cmdCancel" runat="server" Text="Cancelar" />
</td>
</tr>


</table>
    </div>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
            ShowMessageBox="True" ShowSummary="False" />
    </form>
    
</body>
</html>