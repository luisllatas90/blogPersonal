<%@ Page Language="VB" AutoEventWireup="false" CodeFile="totalmatriculados.aspx.vb" Inherits="librerianet_academico_totalmatriculados" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register assembly="BusyBoxDotNet" namespace="BusyBoxDotNet" tagprefix="busyboxdotnet" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Total de Matriculados</title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="JavaScript" src="../../private/funciones.js"></script>
     <script type="text/javascript" language="javascript">
        /*if(top.location==self.location)
        {location.href='../../tiempofinalizado.asp'} //El ../ depende de la ruta de la página
        */
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
    <p class="usatTitulo">Resumen de Matriculados</p>

    <table style="width:100%">
        <tr>
            <td width="style: 80%">
                Ciclo Académico:
                <asp:DropDownList ID="dpCodigo_cac" runat="server" AutoPostBack="True">
                </asp:DropDownList>
            &nbsp;&nbsp;&nbsp;&nbsp;
                Ciclo de Ingreso:
                <asp:DropDownList ID="dpCicloIng_alu" runat="server">
                </asp:DropDownList>
                    </td>
            <td align="right" width="style: 20%">
                <asp:Button ID="cmdBuscar" runat="server" Text="    Buscar" CssClass="buscar2" 
                    Height="22px" />
            &nbsp;<asp:Button ID="cmdExportar" runat="server" CssClass="imprimir2" 
                    Text="    Imprimir" onclientclick="imprimir('N','','');return(false)" 
                    UseSubmitBehavior="False" Height="22px" />
                    <asp:Button ID="cmdPopUp" runat="server" Text="PopUp" style="display:none" />
            </td>
        </tr>
        <tr>
            <td width="style: 80%" colspan="2">

                &nbsp;</td>
        </tr>
    </table>
     <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" ShowFooter="True" 
        Width="80%" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" 
                    CellPadding="3" DataKeyNames="codigo_cpf">
                    <EmptyDataRowStyle CssClass="usatSugerencia" />
                    <Columns>
                        <asp:BoundField DataField="nombre_cpf" HeaderText="Escuela Profesional" />
                        <asp:TemplateField HeaderText="PreMatriculados">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkPreMatriculados" runat="server" Font-Underline="True" 
                                    ForeColor="Blue" onclick="lnkPreMatriculados_Click" 
                                    Text='<%# eval("PreEscuela") %>'></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Matriculados" DataField="MatEscuela" >
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Retirados">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkRetirados" runat="server" Font-Underline="True" 
                                    ForeColor="Blue" Text='<%# eval("RetEscuela") %>' OnClick="lnkRetirados_Click"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Matriculados - Retirados">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkMatriculados" runat="server" Font-Underline="True" 
                                    ForeColor="Blue" onclick="lnkMatriculados_Click"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        
                    </Columns>
                    <FooterStyle 
            HorizontalAlign="Center" BackColor="#e8eef7" Font-Bold="True" ForeColor="#3366CC" />
                    <EmptyDataTemplate>
                        &nbsp;&nbsp;&nbsp;&nbsp; No se encontraron resultados según los criterios 
            seleccionados&nbsp;&nbsp;&nbsp;&nbsp; No se encontraron resultados según los criterios 
            seleccionados
                    </EmptyDataTemplate>
                    <HeaderStyle BackColor="#e8eef7" ForeColor="#3366CC" BorderColor="#99BAE2" 
            BorderStyle="Solid" BorderWidth="1px" />
                </asp:GridView>
                <br />
                <asp:Label ID="lblMensaje" runat="server" CssClass="rojo" Font-Size="10pt" 
                    Visible="False"></asp:Label>
        
    <br />
     <asp:Panel ID="PanelDatos" runat="server" Height="500px" ScrollBars="Auto" 
        Width="90%" CssClass="contornotabla"  >
            <table style="width: 100%;" cellpadding="3" cellspacing="0">
                <tr>
                    <td style="width: 70%">
                                            Escuela Profesional:
            <asp:Label ID="lblEscuela" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                    <td align="right" style="width: 30%">
                        <asp:Button ID="cmdExportarLista" runat="server" CssClass="excel2" 
                            Text="    Exportar" />&nbsp;
                        &nbsp;<asp:Button ID="cmdCancelar" runat="server" Text="    Cerrar" 
                            CssClass="usatSalir" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="width: 100%">
                        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                            Width="100%" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" 
                    CellPadding="3">
                            <Columns>
                                <asp:BoundField HeaderText="#" >
                                    <ItemStyle Width="5%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="codigouniver_alu" HeaderText="Código" >
                                    <ItemStyle Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="alumno" HeaderText="Estudiante" >
                                    <ItemStyle Width="35%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="cicloIng_alu" HeaderText="Ciclo Ingreso" >
                                    <ItemStyle Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="descripcion_pes" HeaderText="Plan de Estudios" >
                                    <ItemStyle Width="25%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nombre_min" HeaderText="Modalidad">
                                    <ItemStyle Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="estadoDeuda_Alu" HeaderText="Deuda Venc.">
                                    <ItemStyle Width="5%" />
                                </asp:BoundField>
                            </Columns>
                            <HeaderStyle BackColor="#e8eef7" ForeColor="#3366CC" BorderColor="#99BAE2" 
            BorderStyle="Solid" BorderWidth="1px" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
    </asp:Panel>
    <cc1:ModalPopupExtender ID="mpeFicha" runat="server"
        CancelControlID="cmdCancelar"
        PopupControlID="PanelDatos"
        TargetControlID="cmdPopUp"  BackgroundCssClass="FondoAplicacion" Y="50" />
       
                                    <busyboxdotnet:BusyBox ID="BusyBox1" runat="server" BackColor="White" 
                                    Overlay="False" Text="Se está procesando su información" 
                                    Title="Por favor espere" />
       
</form>
    </body>
</html>
