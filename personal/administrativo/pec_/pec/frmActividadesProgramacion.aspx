<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmActividadesProgramacion.aspx.vb" Inherits="administrativo_pec2_frmActividades" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="../../../private/calendario.js"></script>
    <title>Registro de Programación de Actividad</title>
    <link href="../../../private/estilo.css" rel="stylesheet" type="text/css" media="screen" />
    <script src="js/MaskHora.js" type="text/javascript"></script>
        <style type="text/css">
            table {
	            font-family: Trebuchet MS;
	            font-size: 8pt;
            }
            TBODY {
	            display: table-row-group;
            }
            tr {
	            font-family: Verdana, Geneva, Arial, Helvetica, sans-serif;
	            font-size: 8pt;
	            color: #2F4F4F;
            }
            select {
	            font-family: Verdana;
	            font-size: 8.5pt;
            }     
            .style4
            {
                width: 5px;
                height: 31px;
            }
            .style5
            {
                width: 100px;
                height: 31px;
            }
            .style6
            {
                width: 307px;
                height: 31px;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
             <tr >
             <td colspan="2">
                    <asp:Label ID="txtNombre" AssociatedControlID="txtNombre" runat="server" ></asp:Label>
             </td>
                <td  style="text-align:right">
                    <asp:Button ID="cmdCancelar" runat="server" Text="Regresar" Height="22px" Width="100px" CssClass="usatSalir" />
                </td>
             </tr>
             <tr>
                <td >&nbsp;</td>                
            </tr>
            <tr>
                <td class="style5">Fecha</td>
                <td class="style6">
                    <asp:TextBox ID="txtFecha" runat="server" Font-Names="Arial" Font-Size="X-Small" 
                        Width="127px" MaxLength="10"></asp:TextBox>
                    <input onclick="MostrarCalendario('txtFecha')" type="button" value="..." 
                        style="height: 20px" /></td>
                <td class="style4">&nbsp;</td>                
            </tr>          
            <tr>
                <td class="style5">Hora Inicio</td>
                <td class="style6">
                    <asp:TextBox ID="txtHoraInicio" runat="server" Width="127px" Font-Names="Arial" 
                        Font-Size="X-Small"></asp:TextBox>&nbsp;Formato 24h (HH:MM)
                </td>
                <td class="style4">&nbsp;</td>                
            </tr>
            <tr>
                <td class="style5">Hora Fin</td>
                <td class="style6"><asp:TextBox ID="txtHoraFin" runat="server" Width="127px" Font-Names="Arial" 
                        Font-Size="X-Small"></asp:TextBox>&nbsp;Formato 24h (HH:MM)</td>
                <td></td>
            </tr>
            <tr>
                <td class="style5">Lugar</td>
                <td class="style6">
                    <asp:TextBox ID="txtLugar" runat="server" Width="305px" Font-Names="Arial" 
                        Font-Size="X-Small"></asp:TextBox>
                </td>
                <td class="style4">&nbsp;</td>                
            </tr>
            <tr>
                <td colspan="3" style="text-align:center">
                    <asp:Button ID="cmdAgregar" runat="server" Text="Agregar" Height="22px" 
                        Width="100px" CssClass="usatGuardar" />
                </td>
            </tr>
            <tr>
                <td >&nbsp;</td>                
            </tr>
            <tr>
                <td colspan="3">
                     <asp:GridView ID="gvActividadProgramacion" runat="server" Width="100%" 
                        AutoGenerateColumns="False" DataKeyNames="codigo_apr">
                        <Columns>
                            <asp:BoundField DataField="codigo_apr" HeaderText="Codigo" Visible="false" />
                            <asp:BoundField DataField="lugar_apr" HeaderText="Lugar" >
                                <ItemStyle Width="40%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fechahoraini_apr" HeaderText="Fecha y Hora Inicio" >
                                <ItemStyle Width="25%" HorizontalAlign="Center" />
                            </asp:BoundField> 
                            <asp:BoundField DataField="fechahorafin_apr" HeaderText="Fecha y Hora Fin" >
                                <ItemStyle Width="25%" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:CommandField HeaderText="Eliminar" ShowDeleteButton="True">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:CommandField>
                        </Columns>
                        <HeaderStyle BackColor="#0B3861" ForeColor="White" Height="25px" />                
                        <RowStyle Height="22px" /> 
                    </asp:GridView> 
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="Hdcodigo_aev" runat="server" />
    </form>
</body>
</html>
