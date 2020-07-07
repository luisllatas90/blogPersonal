<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmConsultaPedido.aspx.vb" Inherits="frmConsultaPedido" %>


<script src="../private/funciones.js" type="text/javascript"></script>
<link href="../private/estiloweb.css" rel="stylesheet" type="text/css" />
<link href="../private/estilo.css" rel="stylesheet" type="text/css" />
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Consulta Pedido por Estudiante</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>

        <asp:ScriptManager ID="smConfirmarPedido" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="upPedido" runat="server">
        <ContentTemplate>
        
        <table style="width:100%;">
            <tr>
                <td>
                    <table border="0" style="border: 1px solid #8ED792; width:100%; border-collapse: collapse;">
                        <tr>
                            <td colspan="2" 
                                style="width: 100%; background-color: #8ED792; color: #FFFFFF; font-weight: bold;">
                                &nbsp; Datos Alumno</td>
                        </tr>
                        <tr>
                            <td style="width:10%">
                                &nbsp;</td>
                            <td  style="width:90%">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width:10%">
                                Código</td>
                            <td style="width:90%">
                                <asp:Label ID="lblCodigo" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:10%">
                                Alumno</td>
                            <td  style="width:90%">
                                <asp:Label ID="lblAlumno" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:10%">
                                Carrera</td>
                            <td style="width:90%">
                                <asp:Label ID="lblCarrera" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:10%">
                                Facultad</td>
                            <td style="width:90%">
                                <asp:Label ID="lblFacultad" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:10%">
                                &nbsp;</td>
                            <td style="width:90%">
                                <asp:HiddenField ID="hfCodPedido" runat="server" />
                                <asp:HiddenField ID="hfCodigoAlu" runat="server" Value="1631" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                <table border="0" style="border: 1px solid #8ED792; width:100%; border-collapse: collapse;">
                   <tr>
                         <td style="width: 100%; background-color: #8ED792; color: #FFFFFF; font-weight: bold;">
                         
                             &nbsp; Pedidos</td>
                   </tr>
                   <tr>
                        <td width="100%">
                            <asp:GridView ID="gvPedido" runat="server" AutoGenerateColumns="False" 
                                Width="100%">
                                <Columns>
                                    <asp:BoundField DataField="CODPED" HeaderText="Código" />
                                    <asp:BoundField DataField="HISTORIAL" HeaderText="Historial" />
                                    <asp:BoundField DataField="TRATAMIENTO" HeaderText="Tratamiento" />
                                    <asp:BoundField DataField="REGISTRO" HeaderText="Registro" />
                                    <asp:BoundField DataField="APROBACION" HeaderText="Aprobación" />
                                    <asp:BoundField DataField="ESTADO" HeaderText="Estado" />
                                    <asp:BoundField DataField="TOTAL" HeaderText="Total" >
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:CommandField SelectText="" ShowSelectButton="True" />
                                </Columns>
                                <EmptyDataTemplate>
                                    No se han encontrado Pedidos en estado Pendiente.
                                </EmptyDataTemplate>
                                <SelectedRowStyle BackColor="#BEFCD3" />
                            </asp:GridView>
                        </td>
                   </tr>
                </table>
                    
                    </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="pnlDetalle" runat="server" Visible="false">
                    
                                <table border="0" style="border: 1px solid #8ED792; width:100%; border-collapse: collapse;">
                   <tr>
                         <td style="width: 80%; background-color: #8ED792; color: #FFFFFF; font-weight: bold;">
                         
                             &nbsp; Detalle Pedido </td>
                   </tr>
                   <tr>
                        <td>
                            <asp:GridView ID="gvDetallePedido" runat="server" AutoGenerateColumns="False" 
                                Width="100%">
                                <Columns>
                                    <asp:BoundField DataField="ARTICULO" HeaderText="Artículo" />
                                    <asp:BoundField DataField="ENTREGADO" HeaderText="Entregado" >
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="FECHAENTREGA" HeaderText="Fecha Entrega" >
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CANTIDAD" HeaderText="Cantidad" >
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PRECIO" HeaderText="Precio" >
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TOTAL" HeaderText="Total" >
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                </Columns>
                                <SelectedRowStyle BackColor="#BEFCD3" />
                            </asp:GridView>
                        </td>
                   </tr>
                </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>

        </ContentTemplate>
        </asp:UpdatePanel>

    </div>
    </form>
</body>
</html>
