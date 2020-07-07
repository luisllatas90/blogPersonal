<%@ Page Language="VB" AutoEventWireup="false" CodeFile="rendimientoacadprogramas.aspx.vb" Inherits="rendimientoacadprogramas" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register assembly="BusyBoxDotNet" namespace="BusyBoxDotNet" tagprefix="busyboxdotnet" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Rendimiento Académico de Programas de Profesionalización</title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="JavaScript" src="../../private/funciones.js"></script>
    <script type="text/javascript" language="javascript">
        if(top.location==self.location)
        {location.href='../../tiempofinalizado.asp'} //El ../ depende de la ruta de la página
    </script>
    <style type="text/css">
    /* .... */
    .FondoAplicacion
    {
        background-color: Gray;
        filter: alpha(opacity=70);
        opacity: 0.7;
    }
</style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <h3>Rendimiento Académico de Programas de Profesionalización</h3>
    <table cellpadding="0" cellspacing="0" style="width: 100%; border:1px solid #96ACE7" border="0">
        <tr bgcolor="#91b4de" style="height:30px">
            <td>
                &nbsp;&nbsp;&nbsp;Programa: 
                <asp:DropDownList ID="dpPlanEstudio" 
                    runat="server" AutoPostBack="True" Font-Size="7pt">
                </asp:DropDownList>
                &nbsp;&nbsp; Grupo: <asp:DropDownList ID="dpVersion" 
                    runat="server">
                </asp:DropDownList>
                &nbsp;
                <asp:Button ID="cmdBuscar" runat="server" Text="    Buscar" 
                    CssClass="buscar2" Enabled="False" />
                &nbsp;<asp:Button ID="cmdExportar" runat="server" CssClass="excel" 
                            Text="    Exportar" Enabled="False" />
                <asp:Button ID="cmdPopUp" runat="server" Text="PopUp" style="display:none" />
            </td>
        </tr>
        <tr>
            <td style="margin-left: 80px" valign="top">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        CellPadding="2" GridLines="Horizontal" BorderStyle="None" 
                    DataKeyNames="codigo_cur,nombre_cur,docente">
        <RowStyle BorderColor="#C2CFF1" BorderStyle="Solid" BorderWidth="1px" />
        <EmptyDataRowStyle CssClass="sugerencia" />
        <Columns>
            <asp:BoundField DataField="ciclo_cur" HeaderText="Ciclo">
                <ItemStyle Width="5%" Font-Size="7pt" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="nombre_cur" HeaderText="Asignatura">
                <ItemStyle Width="40%" />
            </asp:BoundField>
            <asp:BoundField DataField="creditos_cur" HeaderText="Créditos">
                <ItemStyle Width="5%" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="Profesor" DataField="docente">
                <ItemStyle Width="30%" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="Aprobados">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkAprobados" runat="server" onclick="lnkAprobados_Click" 
                        Text='<%# eval("Aprobados") %>'></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="10%" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Desaprobados">
                <ItemTemplate>
                 <asp:LinkButton ID="lnkDesaprobados" runat="server"
                        Text='<%# eval("Desaprobados") %>' onclick="lnkDesaprobados_Click"></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="10%" />
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>
            No se encontraron asignaturas según los criterios seleccionados
        </EmptyDataTemplate>
        <HeaderStyle BackColor="#e8eef7" ForeColor="#3366CC" BorderColor="#99BAE2" 
            BorderStyle="Solid" BorderWidth="1px" />
    </asp:GridView>
    
            </td>
        </tr>
        <tr bgcolor="#91b4de" style="height:30px">
            <td align="right">
            &nbsp;&nbsp;
            </td>
        </tr>
    </table>
    <asp:Panel ID="PanelDatos" runat="server" Height="500px" ScrollBars="Auto" 
        Width="80%" CssClass="contornotabla" style="display:none" >
            <table style="width: 100%;" cellpadding="3" cellspacing="0">
                <tr>
                    <td style="width: 80%">
                                            Asignatura:
            <asp:Label ID="lblCurso" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                    <td align="right" style="width: 20%">
                        <asp:Button ID="cmdExportarLista" runat="server" CssClass="excel2" 
                            Text="    Exportar" />&nbsp;
                        &nbsp;<asp:Button ID="cmdCancelar" runat="server" Text="    Cerrar" 
                            CssClass="usatSalir" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="width: 100%;">
                        Profesor:
                        <asp:Label ID="lblProfesor" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="width: 100%">
                        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                            CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%">
                            <RowStyle BackColor="#E3EAEB" />
                            <Columns>
                                <asp:BoundField HeaderText="#" >
                                    <ItemStyle Width="5%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="codigouniver_alu" HeaderText="Código" >
                                    <ItemStyle Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="alumno" HeaderText="Estudiante" >
                                    <ItemStyle Width="60%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="cicloIng_alu" HeaderText="Ciclo Ingreso" >
                                    <ItemStyle Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="notafinal_dma" HeaderText="Promedio" >
                                    <ItemStyle Width="10%" />
                                </asp:BoundField>
                            </Columns>
                            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#7C6F57" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
    </asp:Panel>
    <cc1:ModalPopupExtender ID="mpeFicha" runat="server"
        CancelControlID="cmdCancelar"
        PopupControlID="PanelDatos"

        TargetControlID="cmdPopUp"  BackgroundCssClass="FondoAplicacion" Y="50" />
    
   
    <busyboxdotnet:BusyBox ID="BusyBox1" runat="server" 
                                    Overlay="False" Text="Se está procesando su información" 
                                    Title="Por favor espere" />
        
   
    </form>
</body>
</html>
