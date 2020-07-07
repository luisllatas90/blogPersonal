<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ListaRequerimientos.aspx.vb" Inherits="Usuario_ListaRequerimientos" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Página sin título</title>
    <link rel="stylesheet" type ="text/css" href ="../private/estilo.css" />
    <link rel="stylesheet" type ="text/css" href ="../private/estiloweb.css" />
    <script language="javascript" type="text/javascript" src="../private/funcion.js"></script>
    <script language ="javascript" type="text/javascript" src ="../private/funciones.js"></script>
    
</head>
<body>
    <form id="frmlistrequerimientos" runat="server">
    <div style="text-align: center">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td style="width: 1051px">
                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                        <tr class="TituloReq">
                            <td height="20" style="font-weight: bold; text-transform: uppercase">
                                Lista de Requerimientos</td>
                            <td align="right" height="20">
                                            <asp:LinkButton ID="LnkVolver" runat="server" Font-Bold="True" 
                                                Font-Underline="False" ForeColor="White">««Regresar</asp:LinkButton>
                            &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td align="right">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <table style="width:100%;">
                                    <tr>
                            <td align="left" height="20" width="80">
                                <b>Solicitud</b></td>
                            <td align="left">
                                :
                                <asp:Label ID="LblSolicitud" runat="server" Text="Label"></asp:Label></td>
                                        </tr>
                                        <tr>
                            <td align="left" height="20">
                                <b>Tipo</b></td>
                            <td align="left">
                                :
                                <asp:Label ID="LblTipo" runat="server" Text="Label"></asp:Label></td>
                                        </tr>
                                        <tr>
                            <td align="left" height="20">
                                <b>Prioridad</b></td>
                            <td align="left">
                                :
                                <asp:Label ID="LblPrioridad" runat="server" Text="Label"></asp:Label></td>
                                        </tr>
                                        <tr>
                            <td align="left" height="20">
                                <b>Área</b></td>
                            <td align="left">
                                :
                                <asp:Label ID="LblArea" runat="server" Text="Label"></asp:Label></td>
                                        </tr>
                                    </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="height: 1px; background-color: #004182;" >
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" valign="top" >
                                <table border="0" width="100%" align="center">
                                    <tr>
                                        <td style="height: 20px; width: 771px;" align="left">
                                            &nbsp;</td>
                                        <td style="height: 22px; width: 300px;" align="right" >
                                            <strong>
                                                <asp:Button ID="CmdNuevo" runat="server" CssClass="nuevo" Text=" Nuevo" 
                                                Width="80px" Height="25px" />&nbsp;<asp:Button ID="CmdActualizar" 
                                                runat="server" CssClass="Actualizar" Text="      Actualizar" 
                                                Width="80px" Height="25px" /></strong></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="height: 100px;" align="center">
                                            <asp:GridView ID="GvReq" runat="server" AutoGenerateColumns="False"
                                                DataKeyNames="id_req" Width="98%" GridLines="Horizontal" PageSize="12">
                                                <Columns>
                                                    <asp:BoundField DataField="id_req" HeaderText="id_req" InsertVisible="False" ReadOnly="True"
                                                        SortExpression="id_req" Visible="False" />
                                                    <asp:BoundField DataField="descripcion_req" HeaderText="Requerimiento" SortExpression="descripcion_req">
                                                        <ItemStyle Width="60%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Prioridad" HeaderText="Prioridad" ReadOnly="True" 
                                                        SortExpression="Prioridad" Visible="False">
                                                        <ItemStyle Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="descripcion_tsol" HeaderText="Tipo" SortExpression="descripcion_tsol">
                                                        <ItemStyle Width="250px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="id_sol" HeaderText="id_sol" SortExpression="id_sol" Visible="False" />
                                                    <asp:BoundField DataField="descripcion_est" HeaderText="Estado" SortExpression="descripcion_est">
                                                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="id_Est" HeaderText="id_Est" SortExpression="id_Est" Visible="False" />
                                                    <asp:ButtonField ButtonType="Image" ImageUrl="~/images/agregar1.gif" 
                                                        Text="Agregar Actividades" >
                                                        <ItemStyle Width="20px" />
                                                    </asp:ButtonField>
                                                    <asp:ButtonField ButtonType="Image" ImageUrl="../images/editar.gif" Text="Editar" >
                                                        <ItemStyle Width="20px" />
                                                    </asp:ButtonField>
                                                    <asp:CommandField ButtonType="Image" DeleteImageUrl="../images/eliminar.gif" ShowDeleteButton="True" >
                                                        <ItemStyle Width="20px" />
                                                    </asp:CommandField>
                                                </Columns>
                                                <RowStyle Height="20px" />
                                                <SelectedRowStyle BackColor="#FFFFCC" />
                                                <HeaderStyle Height="15px" CssClass="Titulocel" />
                                                <EmptyDataTemplate>
                                                    <span style="vertical-align: middle; color: #ff3300; text-align: center">No se encontraron
                                                        registros</span>
                                                </EmptyDataTemplate>
                                            </asp:GridView>
                                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>"
                                                SelectCommand="paReq_ConsultarRequerimientos" SelectCommandType="StoredProcedure" DeleteCommand="paReq_EliminarRequerimiento" DeleteCommandType="StoredProcedure">
                                                <SelectParameters>
                                                    <asp:QueryStringParameter Name="id_sol" QueryStringField="id_sol" Type="Int32" />
                                                </SelectParameters>
                                                <DeleteParameters>
                                                    <asp:Parameter Name="id_req" Type="Int32" />
                                                </DeleteParameters>
                                            </asp:SqlDataSource>
                                        </td>
                                    </tr>
                                    </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" >
                                &nbsp;</td>
                        </tr>
                        </table>
                </td>
            </tr>
            </table>
    
    </div>
    </form>
</body>
</html>
