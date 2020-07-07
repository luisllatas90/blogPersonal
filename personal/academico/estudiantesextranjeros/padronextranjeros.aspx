<%@ Page Language="VB" AutoEventWireup="false" CodeFile="padronextranjeros.aspx.vb" Inherits="librerianet_estudiantesextranjeros_padronextranjeros" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Estudiantes Extranjeros Registrados</title>
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
width:97%; 
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
     .style1
     {
         width: 406px;
     }
     .style2
     {
         width: 397px;
     }
     .style3
     {
         width: 393px;
     }
     .style4
     {
     }
     .style5
     {
     }
     .style6
     {
     }
     .style7
     {
     }
     .style8
     {
         width: 217px;
     }
     .style9
     {
     }
     .style10
     {
         width: 372px;
     }
 </style>


</head>
<body>
    <form id="form1" runat="server">
    <div class="kdiv">
    <table>
    <tr>
    <td class="style1" >
    <p align="center"><b><font size="4" color="#800000">PIMEU</font> - Programa 
Internacional de Movilidad de Estudiantes de la USAT<br>
Dirección de Relaciones Internacionales</b><p align="center">Módulo de Edición</td>
    </tr>
    </table>
    
        <asp:GridView ID="GridView1" runat="server" 
            AllowSorting="True" AutoGenerateColumns="False" CellPadding="1" 
            DataKeyNames="id_Estudiante" DataSourceID="SqlDataSource1" 
            ForeColor="#333333" BorderColor="#3399FF" BorderStyle="Solid" 
            BorderWidth="1px" Font-Names="Arial" 
            Font-Size="Small">
            <RowStyle BackColor="#EFF3FB" CssClass="kclass1" />
            <Columns>
                <asp:CommandField ShowSelectButton="True" SelectText="." 
                    SelectImageUrl="arrow.jpg" />
                <asp:BoundField DataField="Carnet" HeaderText="Carné" 
                    SortExpression="carnet" />
                <asp:BoundField DataField="nombres" HeaderText="Nombres" 
                    SortExpression="nombres" />
                <asp:BoundField DataField="apellidos" HeaderText="Apellidos" 
                    SortExpression="apellidos" />
                <asp:BoundField DataField="sexo" HeaderText="Sexo" 
                    SortExpression="sexo" />
                <asp:BoundField DataField="universidadOrigen" HeaderText="Universidad de Origen" 
                    SortExpression="universidadOrigen" />
                <asp:BoundField DataField="faculty" HeaderText="faculty" 
                    SortExpression="faculty" />
                <asp:BoundField DataField="nacionalidad" HeaderText="Nacionalidad" 
                    SortExpression="nacionalidad" />
                <asp:BoundField DataField="fechaInicial" HeaderText="Fecha Inicio" 
                    SortExpression="fechaInicial" DataFormatString="{0:d}" />
                <asp:BoundField DataField="fechaFinal" HeaderText="Fecha Final" 
                    SortExpression="fechaFinal" DataFormatString="{0:d}" />
            </Columns>
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="#000066" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" 
                BorderColor="#FF3300" BorderStyle="Solid" BorderWidth="1px" 
                CssClass="kclass1" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
    
    </div>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
        SelectCommand="SELECT * FROM [EstudianteExtranjero]"></asp:SqlDataSource>
    <br />
   
    <table cellpadding="0" cellspacing="0" width="944" >
<tr height="30px" valign="middle">

<td class="style1" colspan="3" bgcolor="#99CCFF">
    <p align="center"><b>Editar Alumnos Extranjeros - Modulo RI </b> </td>
</tr>
<tr>
<td class="style4" width="904" colspan="3" bgcolor="#609EFD" height="23">
<font color="#FFFFFF"><b>&nbsp;&gt;&gt; Datos Personales</b></font></td>
</tr>
<tr>
<td class="style8">
Carné </td>
<td class="style3" width="755" colspan="2">
<asp:TextBox ID="txtcarnet" runat="server"></asp:TextBox>
</td>
</tr>


<tr>
<td class="style8">
Apellidos&nbsp; y Nombres</td>
<td class="style5" colspan ="2">
    <asp:TextBox runat="server" Height="18px" Width="201px" 
        ID="txtnombres"></asp:TextBox>
<asp:TextBox runat="server" Height="18px" Width="199px" ID="txtapellidos"></asp:TextBox>
</td>
</tr>


<tr>
<td class="style8">
Fecha Nacimiento </td>
<td class="style5" colspan="2">
<asp:TextBox ID="txtfechaNacimiento" runat="server"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; sexo 
<asp:DropDownList ID="cbosexo" runat="server">
<asp:ListItem >M</asp:ListItem>
<asp:ListItem >F</asp:ListItem>
</asp:DropDownList>
&nbsp; Con convenio 
<asp:DropDownList ID="cboconvenio" runat="server">
<asp:ListItem >S</asp:ListItem>
<asp:ListItem >N</asp:ListItem>
</asp:DropDownList>
</td>
</tr>


<tr>
<td class="style8">
Pasaporte </td>
<td class="style5" colspan="2">
<asp:TextBox ID="txtpasaporte" runat="server"></asp:TextBox>
&nbsp;Nacionalidad
<asp:TextBox runat="server" Height="18px" Width="144px" ID="txtnacionalidad"></asp:TextBox>
</td>
</tr>


<tr>
<td class="style8" height="19">
País de residencia</td>
<td class="style5" height="19" colspan="2">
<asp:TextBox ID="txtpais_residencia" runat="server"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    Localidad 
<asp:TextBox runat="server" Height="18px" Width="144px" ID="txtlocalidad"></asp:TextBox>
</td>
</tr>


<tr>
<td class="style8" height="19">
    Dirección Permanente</td>
<td class="style10" height="19">
<asp:TextBox runat="server" Height="18px" Width="356px" ID="txtdireccionPermanente"></asp:TextBox>
</td>
<td class="style6" height="19">
&nbsp;
Zip code&nbsp;
<asp:TextBox runat="server" Height="18px" Width="83px" ID="txtzipcode"></asp:TextBox>
    </td>
</tr>


<tr>
<td class="style8">
eMail del estudiante</td>
<td class="style10">
<asp:TextBox runat="server" Height="18px" Width="356px" ID="txteMail_Alu"></asp:TextBox>
</td>
<td class="style6">
    Teléfonos 
    <asp:TextBox ID="txttelefonos" runat="server"></asp:TextBox>
    </td>
</tr>


<tr>
<td class="style8">
Gpo.Sangre y alergias</td>
<td class="style7" colspan="2">
<asp:TextBox runat="server" Height="18px" Width="356px" ID="txtgrupoSangineo"></asp:TextBox>
</td>
</tr>


<tr>
<td class="style4" width="944" colspan="3" bordercolor="#609EFD" bgcolor="#609EFD">
<font color="#FFFFFF"><b>&gt;&gt; Datos Académicos</b></font></td>
</tr>


<tr>
<td class="style8">
        Universidad de Origen </td>
<td class="style10">
<asp:TextBox ID="txtuniversidadOrigen" runat="server" Width="356px"></asp:TextBox></td>
<td class="style6">
    &nbsp;</td>
</tr>


<tr>
<td class="style8">
        País de la Universidad</td>
<td class="style10">
<asp:TextBox ID="txtpaisUniversidadOrigen" runat="server" Width="356px"></asp:TextBox></td>
<td class="style6">
    &nbsp;</td>
</tr>


<tr>
<td class="style8">
Área de estudio /Faculty</td>
<td class="style9" colspan="2">
<asp:TextBox ID="txtfaculty" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;
Semestre&nbsp;
<asp:TextBox ID="txtsemetreActual" runat="server" Width="145px"></asp:TextBox></td>
</tr>


<tr>
<td class="style4" width="500" height="19" colspan="3" 
        style="background-color: #70A7FC; color: #FFFFFF; font-weight: bold; font-family: Arial;">
    &gt;&gt; Estancia Académica&nbsp; en la USAT</td>
</tr>


<tr>
<td class="style8" height="19">
Asig. a Escuela USAT</td>
<td class="style7" height="19" colspan="2">
<asp:TextBox ID="txtescuelausat" runat="server"></asp:TextBox></td>
</tr>


<tr>
<td class="style8" height="19">
    Estancia Académica:&nbsp;
    <br />
    Fecha Inicial:
<td class="style7" height="19" colspan="2">
<asp:TextBox ID="txtfechaInicial" runat="server"></asp:TextBox>&nbsp;Fecha Final:<asp:TextBox ID="txtfechaFinal" runat="server"></asp:TextBox></td>
</tr>


<tr>
<td class="style4" height="19" colspan="3" bgcolor="#609EFD">
<font color="#FFFFFF"><b>&gt;&gt; Datos de Contactos</b></font></td>
</tr>


<tr>
<td class="style4" colspan="3" >
        Contacto de Emergencia </td>
</tr>


<tr>
<td colspan="3">
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
            Coordinadores Institucionales</td>
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
<td class="style4" width="944" colspan="3" bgcolor="#609EFD">
<font color="#FFFFFF"><b>&gt;&gt; Resultados </b></font> </td>



<tr>
<td class="style4" colspan="2">
<table border="0" cellpadding="0" style="border-collapse: collapse; width:535px" 
        id="table1">
	<tr>
		<td width="421">Actividad o Curso </td>
		<td>Calificativo </td>
	</tr>
	<tr>
		<td width="421">
<asp:TextBox runat="server" Height="18px" Width="410px" ID="txtcurso1"></asp:TextBox>
		</td>
		<td>
<asp:TextBox runat="server" Height="18px" Width="100px" ID="txtcalificativo1"></asp:TextBox>
		</td>
	</tr>
	<tr>
		<td width="421">
<asp:TextBox runat="server" Height="18px" Width="410px" ID="txtcurso2"></asp:TextBox>
		</td>
		<td>
<asp:TextBox runat="server" Height="18px" Width="100px" ID="txtcalificativo2"></asp:TextBox>
		</td>
	</tr>
	<tr>
		<td width="421">
<asp:TextBox runat="server" Height="18px" Width="410px" ID="txtcurso3"></asp:TextBox>
		</td>
		<td>
<asp:TextBox runat="server" Height="18px" Width="100px" ID="txtcalificativo3"></asp:TextBox>
		</td>
	</tr>
	<tr>
		<td width="421">
<asp:TextBox runat="server" Height="18px" Width="410px" ID="txtcurso4"></asp:TextBox>
		</td>
		<td>
<asp:TextBox runat="server" Height="18px" Width="100px" ID="txtcalificativo4"></asp:TextBox>
		</td>
	</tr>
	<tr>
		<td width="421">
<asp:TextBox runat="server" Height="18px" Width="410px" ID="txtcurso5"></asp:TextBox>
		</td>
		<td>
<asp:TextBox runat="server" Height="18px" Width="100px" ID="txtcalificativo5"></asp:TextBox>
		</td>
	</tr>
	<tr>
		<td width="421">
<asp:TextBox runat="server" Height="18px" Width="410px" ID="txtcurso6"></asp:TextBox>
		</td>
		<td>
<asp:TextBox runat="server" Height="18px" Width="100px" ID="txtcalificativo6"></asp:TextBox>
		</td>
	</tr>
</table>

</td>



<td class="style4" width="353" valign="top" 
        style="border-left-style: solid; border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom-width: 1px">
Comentario Final del Alumno:<br>
&nbsp;<asp:TextBox ID="txtcomentariofinal" runat="server" Height="97px" 
        TextMode="MultiLine" Width="311px"></asp:TextBox>
                </td>



<tr>
<td class="style4" colspan="3">
    Comentario del coordinador USAT<br />
    <asp:TextBox ID="txtcomentarioCoordinador" runat="server" Height="97px" 
        TextMode="MultiLine" Width="874px"></asp:TextBox>

</td>



<tr height="30px" valign="middle" >
<td class="style2" colspan="3" align="center">
<asp:Button ID="cmdNuevo" runat="server" Text="Actualizar" />
&nbsp;&nbsp;</input>&nbsp;&nbsp;&nbsp;&nbsp;
<asp:Button ID="cmdCancel" runat="server" Text="Cancelar" />
</td>



</table>
    
    </form>
</body>
</html>