<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AdminSolicitud.aspx.vb" Inherits="Equipo_AdminSolicitud" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Cambiar estado de la solicitud</title>
    <link href ="../private/estilo.css" rel="stylesheet" type="text/css" />
    <link href ="../private/estiloweb.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <table style="width:100%;">
            <tr>
                <td align="center" class="TituloReq">
                    INICIAR PROCESO</td>
            </tr>
            <tr>
                <td>
        <asp:FormView ID="FormView1" runat="server" DataKeyNames="id_sol" 
            DataSourceID="ObjDatos" DefaultMode="Edit" Width="100%">
            <EditItemTemplate>
                <table width="100%">
                    <tr>
                        <td valign="top">Descripcion</td>
                        <td>
                            <asp:TextBox ID="descripcion_solTextBox" runat="server" 
                                Text='<%# Bind("descripcion_sol") %>' Rows="3" TextMode="MultiLine" 
                                Width="100%" BorderColor="White" BorderWidth="0px" ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Prioridad</td>
                        <td>
                <asp:TextBox ID="prioridadTextBox" runat="server" Text='<%# Bind("prioridad") %>' 
                                BorderColor="White" BorderWidth="0px" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            Fecha de solicitud</td>
                        <td>
                <asp:TextBox ID="fecha_solTextBox" runat="server" Text='<%# Bind("fecha_sol") %>' 
                                BorderColor="White" BorderStyle="None" BorderWidth="0px" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            Estado</td>
                        <td>
                <asp:DropDownList ID="cboEstado" runat="server" DataSourceID="SqlDatos" DataTextField="descripcion_est" DataValueField="id_est" SelectedValue='<%# Bind("id_est") %>'>
                </asp:DropDownList>
                <asp:Label ID="Label1" runat="server" Text='<%# Eval("id_sol") %>' Visible="False"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>
                            Tipo de solicitud</td>
                        <td>
                <asp:TextBox ID="descripcion_tsolTextBox" runat="server" 
                                Text='<%# Bind("descripcion_tsol") %>' Width="100%" BorderColor="White" 
                                BorderWidth="0px" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            Area solicitante</td>
                        <td>
                <asp:TextBox ID="descripcion_ccoTextBox" runat="server" 
                                Text='<%# Bind("descripcion_cco") %>' Width="100%" BorderColor="White" 
                                BorderWidth="0px" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            Aplicación</td>
                        <td>
                <asp:TextBox ID="descripcion_aplTextBox" runat="server" 
                                Text='<%# Bind("descripcion_apl") %>' BorderColor="White" 
                                BorderWidth="0px" ReadOnly="True" Width="100%"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td valign="top">
                            Observacion</td>
                        <td>
                <asp:TextBox ID="observacion_perTextBox" runat="server" 
                                Text='<%# Bind("observacion_per") %>' Rows="3" TextMode="MultiLine" 
                                Width="100%"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="2" style="height: 1px; background-color: #004182;">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="right" height="35">
                &nbsp;<asp:Button ID="UpdateButton" runat="server"
                        CommandName="Update" CssClass="guardar" Text="Guardar" ValidationGroup="Guardar"
                        Width="85px" />
                            &nbsp;
                            <asp:Button ID="UpdateCancelButton" runat="server" CommandName="Cancel" CssClass="cancelar"
                                OnClientClick="javascript: window.close(); return false;" Text="Cancelar" Width="85px" CausesValidation="False" /></td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            <asp:TextBox ID="codigo_perTextBox" runat="server" 
                                Text='<%# Bind("codigo_per") %>' Visible="False"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </EditItemTemplate>
            <InsertItemTemplate>
                descripcion_sol:
                <asp:TextBox ID="descripcion_solTextBox" runat="server" Text='<%# Bind("descripcion_sol") %>'>
                </asp:TextBox><br />
                prioridad:
                <asp:TextBox ID="prioridadTextBox" runat="server" Text='<%# Bind("prioridad") %>'>
                </asp:TextBox><br />
                fecha_sol:
                <asp:TextBox ID="fecha_solTextBox" runat="server" Text='<%# Bind("fecha_sol") %>'>
                </asp:TextBox><br />
                descripcion_est:
                <asp:TextBox ID="descripcion_estTextBox" runat="server" Text='<%# Bind("descripcion_est") %>'>
                </asp:TextBox><br />
                id_est:
                <asp:TextBox ID="id_estTextBox" runat="server" Text='<%# Bind("id_est") %>'>
                </asp:TextBox><br />
                descripcion_tsol:
                <asp:TextBox ID="descripcion_tsolTextBox" runat="server" Text='<%# Bind("descripcion_tsol") %>'>
                </asp:TextBox><br />
                descripcion_cco:
                <asp:TextBox ID="descripcion_ccoTextBox" runat="server" Text='<%# Bind("descripcion_cco") %>'>
                </asp:TextBox><br />
                codigo_per:
                <asp:TextBox ID="codigo_perTextBox" runat="server" Text='<%# Bind("codigo_per") %>'>
                </asp:TextBox><br />
                descripcion_apl:
                <asp:TextBox ID="descripcion_aplTextBox" runat="server" Text='<%# Bind("descripcion_apl") %>'>
                </asp:TextBox><br />
                observacion_per:
                <asp:TextBox ID="observacion_perTextBox" runat="server" Text='<%# Bind("observacion_per") %>'>
                </asp:TextBox><br />
                <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                    Text="Insertar">
                </asp:LinkButton>
                <asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                    Text="Cancelar">
                </asp:LinkButton>
            </InsertItemTemplate>
            <ItemTemplate>
                id_sol:
                <asp:Label ID="id_solLabel" runat="server" Text='<%# Eval("id_sol") %>'></asp:Label><br />
                descripcion_sol:
                <asp:Label ID="descripcion_solLabel" runat="server" Text='<%# Bind("descripcion_sol") %>'></asp:Label><br />
                prioridad:
                <asp:Label ID="prioridadLabel" runat="server" Text='<%# Bind("prioridad") %>'></asp:Label><br />
                fecha_sol:
                <asp:Label ID="fecha_solLabel" runat="server" Text='<%# Bind("fecha_sol") %>'></asp:Label><br />
                descripcion_est:
                <asp:Label ID="descripcion_estLabel" runat="server" Text='<%# Bind("descripcion_est") %>'></asp:Label><br />
                id_est:
                <asp:Label ID="id_estLabel" runat="server" Text='<%# Bind("id_est") %>'></asp:Label><br />
                descripcion_tsol:
                <asp:Label ID="descripcion_tsolLabel" runat="server" Text='<%# Bind("descripcion_tsol") %>'></asp:Label><br />
                descripcion_cco:
                <asp:Label ID="descripcion_ccoLabel" runat="server" Text='<%# Bind("descripcion_cco") %>'></asp:Label><br />
                codigo_per:
                <asp:Label ID="codigo_perLabel" runat="server" Text='<%# Bind("codigo_per") %>'></asp:Label><br />
                descripcion_apl:
                <asp:Label ID="descripcion_aplLabel" runat="server" Text='<%# Bind("descripcion_apl") %>'></asp:Label><br />
                observacion_per:
                <asp:Label ID="observacion_perLabel" runat="server" Text='<%# Bind("observacion_per") %>'></asp:Label><br />
            </ItemTemplate>
        </asp:FormView>
                </td>
            </tr>
        </table>
        <asp:SqlDataSource ID="SqlDatos" runat="server" ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>"
            SelectCommand="paReq_ConsultarEstado" SelectCommandType="StoredProcedure">
        </asp:SqlDataSource>
        <asp:ObjectDataSource ID="ObjDatos" runat="server" SelectMethod="ObtieneSolicitudesParaAdministrar"
            TypeName="clsRequerimientos" UpdateMethod="InsertaEstadoSolicitud">
            <SelectParameters>
                <asp:QueryStringParameter DefaultValue="" Name="id_sol" QueryStringField="id_act"
                    Type="Int32" />
                <asp:QueryStringParameter Name="cod_per" QueryStringField="id" Type="Int32" />
                <asp:QueryStringParameter Name="tipo" QueryStringField="tipo" Type="Char" />
            </SelectParameters>
            <UpdateParameters>
                <asp:Parameter Name="id_sol" Type="Int32" />
                <asp:Parameter Name="id_est" Type="Int32" />
                <asp:Parameter Name="cod_per" Type="Int32" />
                <asp:Parameter Name="observacion" Type="String" />
            </UpdateParameters>
        </asp:ObjectDataSource>
        <asp:SqlDataSource ID="SqlSolicitud" runat="server" ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>"
            SelectCommand="paReq_ConsultarSolicitudParaAdministrar" SelectCommandType="StoredProcedure"
            UpdateCommand="paReq_ActualizarEstadoSolicitud" UpdateCommandType="StoredProcedure">
            <UpdateParameters>
                <asp:Parameter Name="id_sol" Type="Int32" />
                <asp:Parameter Name="id_est" Type="Int32" />
                <asp:Parameter Name="codigo_per" Type="Int32" />
                <asp:Parameter Name="observacion" Type="String" />
            </UpdateParameters>
            <SelectParameters>
                <asp:QueryStringParameter DefaultValue="" Name="id_sol" QueryStringField="id_sol"
                    Type="Int32" />
                <asp:QueryStringParameter DefaultValue="" Name="codigo_per" QueryStringField="id"
                    Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>
    </form>
</body>
</html>
