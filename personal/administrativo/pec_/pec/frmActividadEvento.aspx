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
     <%-- <script src="js/MaskHora.js" type="text/javascript"></script>
        <script src="../../../private/calendario.js"></script> --%> 
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
     <script type="text/javascript" language="JavaScript">
         function validarnumeros(e) {
             var unicode = e.charCode ? e.charCode : e.keyCode
             if (unicode != 8) {
                 if (unicode < 48 || unicode > 57) //if not a number
                 { return false } //disable key press    
             }
         }
         function validardecimal(e) {
             var unicode = e.charCode ? e.charCode : e.keyCode
             if (unicode != 8 && unicode != 46) {
                 if (unicode < 48 || unicode > 57) //if not a number
                 { return false } //disable key press    
             }
         }
         function ConfirmarEliminar() {
             if (confirm('¿Estás seguro de eliminar el registro?')) {
                 document.getElementById("HdRespEliminar").value = "True";
                 return true;
             }
             else {
                 document.getElementById("HdRespEliminar").value = "False";
                 return false;
             }
         }
      </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:Button ID="btnRefrescar" runat="server" Text="Refrescar" CssClass="regresar2" Width="100px" Height="22px" />
                </td>
            </tr>
        </table>
        <table width="100%">            
            <tr>
                <td class="style5">Tipo Actividad</td>
                <td class="style3" colspan="3">
                    <asp:DropDownList ID="cboActividad" runat="server" Height="22px" 
                        Width="310px" Font-Names="Arial" Font-Overline="False" Font-Size="X-Small">
                    </asp:DropDownList>                    
                    &nbsp;&nbsp;                    
                    <a href='frmActividades.aspx?KeepThis=true&TB_iframe=true&height=200&width=410&modal=true' title='Actividades' class='thickbox'><img src="../../../images/librohoja.gif" width="12" height="15" style="border:0px" />Nuevo Tipo de Actividad</a>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                    <asp:CheckBox ID="chkPermiso" runat="server" Text="Permitir Asistencia" />
</td>
            </tr>
            
            <tr>
                <td class="style5">Nombre</td>
                <td class="style6">
                    <asp:TextBox ID="txtNombre" runat="server" MaxLength="80" Width="305px"  style="text-transform:uppercase"
                        TextMode="MultiLine" Font-Names="Arial" Font-Size="X-Small"></asp:TextBox>
                </td>
                <td class="style4"></td>    
                <td rowspan="6">
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
            <%--Inicio - fatima.vasquez 15-08-2018 --%>            
            <%--
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
            --%> 
            <tr>
                <td class="style5">Cantidad de Cupos</td>
                <td class="style6">
                    <asp:TextBox ID="txtCupos" runat="server" Width="127px" Font-Names="Arial" 
                        Font-Size="X-Small" onkeypress="return validarnumeros(event);"></asp:TextBox>
                </td>
                <td class="style4">&nbsp;</td>                
            </tr>
            <tr>
                <td class="style5">Costo (S/)</td>
                <td class="style6">
                    <asp:TextBox ID="txtCosto" runat="server" Width="127px" Font-Names="Arial" 
                        Font-Size="X-Small" Text="0.00" Style="text-align: right" onkeypress="return validardecimal(event);"></asp:TextBox>
                </td>
                <td class="style4">&nbsp;</td>                
            </tr>
             <tr>
                <td class="style5">Servicio Concepto</td>
                <td class="style6">
                   <asp:DropDownList ID="cboServicioConcepto" runat="server" Height="22px" 
                        Width="310px" Font-Names="Arial" Font-Overline="False" Font-Size="X-Small">
                        <asp:ListItem Value="0"></asp:ListItem>
                    </asp:DropDownList> 
                </td>
                <td class="style4">&nbsp;</td>                
            </tr>
            <%--Fin - fatima.vasquez 15-08-2018 --%>
            <%--<tr>
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
            </tr>--%>
            <tr>
                <td class="style5"></td>
                <td class="style6">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                    <asp:Button ID="cmdAgregar" runat="server" BackColor="White" 
                        CssClass="agregar2" Height="22px" Text="Guardar" Width="100px" />
                    
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
                        AutoGenerateColumns="False" DataKeyNames="codigo_aev" 
                        EmptyDataText="No se encontraron registros ">
                        <Columns>
                            <asp:BoundField DataField="codigo_aev" HeaderText="Codigo" Visible="false" />
                            <asp:BoundField DataField="nombre_aev" HeaderText="Nombre">
                                <ItemStyle Width="50%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="descripcion_Act" HeaderText="Tipo Actividad" >
                                <ItemStyle Width="8%" />
                            </asp:BoundField>
                            <%--Inicio - fatima.vasquez 15-08-2018 --%>
                            <%--<asp:BoundField DataField="grupo_aev" HeaderText="Grupo" >
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                             
                            <asp:BoundField DataField="lugar_aev" HeaderText="Lugar" >
                            </asp:BoundField>
                            <asp:BoundField DataField="fechahora_aev" HeaderText="Fecha y hora" >
                                <ItemStyle Width="15%" HorizontalAlign="Center" />
                            </asp:BoundField> --%>
                            <asp:BoundField DataField="costo_aev" HeaderText="Costo" >
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cupos_aev" HeaderText="Cupos" >
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Programación">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="inscritos" HeaderText="Inscritos">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Lista">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <%--Fin - fatima.vasquez 15-08-2018 --%>
                            <asp:TemplateField HeaderText="Eliminar">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEliminar" CommandArgument='<%# Eval("codigo_aev") %>' CommandName="Eliminar" runat="server" OnClientClick="return confirm('¿Está seguro de eliminar el registro?');" style="display:block;text-align:center">Eliminar</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>      
                            <asp:TemplateField HeaderText="Modificar">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkModificar" CommandArgument='<%# Eval("codigo_aev") %>' CommandName="Modificar" runat="server" style="display:block;text-align:center">Modificar</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>                      
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
    <asp:HiddenField ID="HdRespEliminar" runat="server" />
    <asp:HiddenField ID="Hdcodigo_aev" runat="server" Value = "0" />
    </form>
</body>
</html>
