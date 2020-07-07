<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Busqueda.aspx.vb" Inherits="medicina_Busqueda" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Página sin título</title>
     <link href="../../../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript"  language="JavaScript" src="../../../../private/funciones.js"></script>
    <script type="text/javascript"  language="JavaScript" src="../../../../private/tooltip.js"></script>
    <script language="javascript" type="text/javascript">
     function AbrirDetalle(pagina)
	{
	    var codigo_Inv= form1.txtelegido.value
		var Tipo= form1.txtTipo.value
		var Estado= form1.txtEstado.value
		var Menu= form1.txtMenu.value
		if (form1.txtelegido.value!="" || form1.txtelegido.value!=0)
			 fradetalle.location.href=pagina + "?codigo_alu=" + codigo_Inv.substring(4,codigo_Inv.length) + "&Tipo=" + Tipo +"&Estado=" + Estado + "&Menu=" +Menu
	}
	</script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%">
            <tr>
                <td colspan="3">
                    Búsqueda de Estudiantes</td>
            </tr>
            <tr>
                <td>
                    Buscar por :</td>
                <td>
                    <asp:DropDownList ID="DDLTipo" runat="server">
                        <asp:ListItem Value="AL">Apellidos y Nombres</asp:ListItem>
                        <asp:ListItem Value="CU">Codigo Universitario</asp:ListItem>
                    </asp:DropDownList></td>
                <td>
                    <asp:TextBox ID="TxtNombre" runat="server" Width="349px"></asp:TextBox>&nbsp; &nbsp;<asp:Button
                        ID="CmdBuscar" runat="server" Text="Buscar" /></td>
            </tr>
            <tr>
                <td style="height: 15px">
                </td>
                <td style="height: 15px">
                </td>
                <td style="height: 15px">
                </td>
            </tr>
            <tr>
                <td colspan="3" style="height: 200px" valign="top">
                    <asp:Panel ID="Panel1" runat="server" Height="300px" ScrollBars="Vertical" Width="100%">
                        <asp:GridView ID="grid2" runat="server" AutoGenerateColumns="False" GridLines="Horizontal"
                            Width="100%">
                            <Columns>
                                <asp:BoundField HeaderText="N&#176;">
                                    <ItemStyle Width="30px" HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="codigo_alu" HeaderText="Codigo_alu" Visible="False" />
                                <asp:BoundField DataField="codigouniver_alu" HeaderText="Cod. Universitario">
                                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="alumno" HeaderText="Apellidos y Nombres" />
                            </Columns>
                            <HeaderStyle BackColor="Maroon" ForeColor="White" Height="25px" />
                            <EmptyDataTemplate>
                                <span style="vertical-align: middle; width: 100%; color: red; font-family: verdana;
                                    height: 142px; text-align: center">
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    No existen coincidencias a los criterios de búsqueda ingresados.
                                    <br />
                                    Seleccione tipo de búsqueda e ingrese un el texto a buscar.</span>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <table cellpadding="0" cellspacing="0" style="border-collapse: collapse;
                        height: 100%" width="100%">
                        <tr>
                            <td id="tab" align="center" class="pestanaresaltada_1" onclick="ResaltarPestana_1('0','','');AbrirDetalle('datos_personales.aspx')"
                                style="height: 32px" width="20%">
                                Datos Generales</td>
                            <td class="bordeinf" style="height: 32px" width="1%">
                                &nbsp;</td>
                            <td id="tab" align="center" class="pestanabloqueada" onclick="ResaltarPestana_1('1','','');AbrirDetalle('datos_asistencias.aspx')"
                                style="height: 32px" width="20%">
                                Actividades</td>
                            <td class="bordeinf" style="height: 32px" width="1%">
                                &nbsp;</td>
                            <td id="tab" align="center" class="pestanabloqueada" onclick="ResaltarPestana_1('2','','');AbrirDetalle('datos_evaluaciones.aspx')"
                                style="height: 32px" width="20%">
                                Evaluaciones</td>
                            <td style="height: 32px" width="45%">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="pestanarevez" colspan="6" style="height: 127%" valign="top" width="100%">
                                <span id="mensajedetalle" class="usatsugerencia">&nbsp; &nbsp; &nbsp;Seleccione un alumnos
                                    para visualizar su detalle</span>
                                <iframe id="fradetalle" border="0" frameborder="0" height="100%" scrolling="yes"
                                    style="height: 90%" width="100%"></iframe>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="3" style="height: 366px"><iframe name="fradetalle" id="fradetalle" marginwidth="0" scrolling="yes" height="400px" width="100%" style="height: 360px" frameborder="0"></iframe>
                </td>
            </tr>
        </table>
    
    </div>
    <asp:HiddenField  ID="txtelegido" runat="server" />
    <asp:HiddenField ID="txtTipo" runat="server" />
    <asp:HiddenField ID="txtEstado" runat="server" />
    <asp:HiddenField ID="txtMenu" runat="server" />
    </form>
</body>
</html>
