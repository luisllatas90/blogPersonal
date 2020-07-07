<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmActividadEvento.aspx.vb" Inherits="administrativo_pec2_frmActividadEvento" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../../private/estilo.css" rel="stylesheet" type="text/css" media="screen" />
    <link rel="stylesheet" href="../../../private/jq/lbox/thickbox.css" type="text/css" media="screen" />    
    <script type="text/javascript" language="JavaScript" src="../../../private/funciones.js"></script>
    <script type="text/javascript" language="JavaScript" src="../../../private/jq/jquery-1.4.2.min.js"></script>
	<script type="text/javascript" language="JavaScript" src="../../../private/jq/lbox/thickbox.js"></script>	
    <script src="js/MaskHora.js" type="text/javascript"></script>
    
    <script src="../../../private/calendario.js"></script>
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
        .style3
        {
            height: 31px;
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
        .style7
        {
            width: 307px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%">            
            <tr>
                <td class="style5">Tipo Actividad</td>
                <td class="style3" colspan="3">
                    <asp:DropDownList ID="cboActividad" runat="server" Height="22px" 
                        Width="310px" Font-Names="Arial" Font-Overline="False" Font-Size="X-Small">
                    </asp:DropDownList>                    
                    &nbsp;&nbsp;                    
                    <img src="../../../images/librohoja.gif" width="12" height="15" /><a href='frmActividades.aspx?KeepThis=true&TB_iframe=true&height=180&width=400&modal=true' title='Actividades' class='thickbox'>Nueva Actividad<a/>
                </td>
            </tr>
            
            <tr>
                <td class="style5">Nombre</td>
                <td class="style6">
                    <asp:TextBox ID="txtNombre" runat="server" MaxLength="80" Width="305px" 
                        TextMode="MultiLine" Font-Names="Arial" Font-Size="X-Small"></asp:TextBox>
                </td>
                <td class="style4"></td>    
                <td rowspan="7">
                    <asp:GridView ID="gvGrupos" runat="server" Width="100%" 
                        AutoGenerateColumns="False">
                        <Columns>
                            <asp:BoundField DataField="grupo_aev" HeaderText="Grupo" >
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Inicio" HeaderText="Inicia Actividad" >
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Final" HeaderText="Ultima Actividad" >
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle BackColor="#0B3861" ForeColor="White" Height="22px" />                
                        <RowStyle Height="20px" />
                    </asp:GridView>
                </td>                       
            </tr>
            
            <tr>
                <td class="style5">Fecha</td>
                <td class="style6">
                    <asp:TextBox ID="txtFecha" runat="server" Font-Names="Arial" Font-Size="X-Small" 
                        Width="127px"></asp:TextBox>
                    <input onclick="MostrarCalendario('txtFecha')" type="button" value="..." 
                        style="width: 26px" /></td>
                <td class="style4">&nbsp;</td>                
            </tr>
            
            <tr>
                <td class="style5">Hora Inicio</td>
                <td class="style6">
                    <asp:TextBox ID="txtHora" runat="server" Width="127px" Font-Names="Arial" 
                        Font-Size="X-Small"></asp:TextBox>
                </td>
                <td class="style4">&nbsp;</td>                
            </tr>
            <tr>
                <td class="style5">Hora Fin</td>
                <td class="style6"><asp:TextBox ID="txtHoraFin" runat="server" Width="127px" Font-Names="Arial" 
                        Font-Size="X-Small"></asp:TextBox></td>
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
                <td class="style5">Grupo</td>
                <td class="style6"><asp:DropDownList ID="cboGrupo" runat="server">
                    </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                    <asp:Button ID="cmdAgregar" runat="server" BackColor="White" 
                        CssClass="agregar2" Height="22px" Text="Agregar" Width="100px" />
                    
                </td>
                <td class="style4">&nbsp;</td>                
            </tr>
            <tr>
                <td>                    
                </td>
                <td align="right" class="style7">                    
                    &nbsp;</td>
                <td>&nbsp;</td>                
            </tr>
            <tr>
                <td colspan="4">
                    <asp:GridView ID="gvActividadEvento" runat="server" Width="100%" 
                        AutoGenerateColumns="False" DataKeyNames="codigo_aev">
                        <Columns>
                            <asp:BoundField DataField="codigo_aev" HeaderText="Codigo" Visible="false" />
                            <asp:BoundField DataField="nombre_aev" HeaderText="Nombre">
                                <ItemStyle Width="30%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="descripcion_Act" HeaderText="Tipo Actividad" >
                                <ItemStyle Width="22%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="grupo_aev" HeaderText="Grupo" >
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="lugar_aev" HeaderText="Lugar" >
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fechahora_aev" HeaderText="Fecha y hora" >
                                <ItemStyle Width="18%" HorizontalAlign="Center" />
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
            <br />
            <asp:Button ID="cmdRptAsistencia" runat="server" Text="Reporte de Asistencia" CssClass="buscar2" Width="200px" Height="22px" />
    </div>
    <asp:HiddenField ID="Hdcodigo_cco" runat="server" />
    </form>
</body>
</html>
