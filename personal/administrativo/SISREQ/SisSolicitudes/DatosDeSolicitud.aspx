<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DatosDeSolicitud.aspx.vb" Inherits="SisSolicitudes_DatosDeSolicitud" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
    <link href="../private/estilo.css" rel="stylesheet" type="text/css" />
    <link href="../private/estiloweb.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
                <table ID="TablaDat" style="width:100%;" style="visibility:visible">
                    <tr>
            <td align="right">
                &nbsp;</td>
                        </tr>
                        <tr>
            <td>
                <b>
                <asp:Label ID="LblDatSolicitud" runat="server" Text="DATOS DE LA SOLICITUD"></asp:Label>
                </b></td>
                        </tr>
                        <tr>
            <td valign="top" >
                <asp:DetailsView ID="DlSolicitud" runat="server" AutoGenerateRows="False" 
                    DataSourceID="SqlDataSource1" GridLines="None" Width="98%">
                    <RowStyle Height="18px" HorizontalAlign="Left" />
                    <Fields>
                        <asp:BoundField DataField="numero_sol" HeaderText="Número de Solicitud:" 
                            SortExpression="numero_sol">
                            <HeaderStyle Width="20%" />
                            <ItemStyle Width="80%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="fecha_sol" HeaderText="Fecha de Solicitud:" 
                            SortExpression="fecha_sol" DataFormatString="{0:dd-MM-yyyy}" />
                        <asp:BoundField DataField="responsable_Sol" HeaderText="Responsable:" 
                            SortExpression="responsable_Sol" />
                        <asp:BoundField DataField="dniResponsable_Sol" 
                            HeaderText="Documento de Identidad:" SortExpression="dniResponsable_Sol" />
                        <asp:BoundField DataField="direccionresponsable_sol" 
                            HeaderText="Dirección del Responsable:" 
                            SortExpression="direccionresponsable_sol" />
                        <asp:BoundField DataField="telefonoResponsable_sol" 
                            HeaderText="Teléfono del Responsable:" 
                            SortExpression="telefonoResponsable_sol" />
                        <asp:BoundField DataField="observaciones_Sol" HeaderText="Observaciones:" 
                            SortExpression="observaciones_Sol" />
                    </Fields>
                </asp:DetailsView>
            </td>
                        </tr>
                        <tr>
            <td valign="top" style="font-weight: 700">
                &nbsp;</td>
                        </tr>
                        <tr>
            <td valign="top" style="font-weight: 700">
                <table style="width: 100%;" id="l">
                    <tr>
                        <td align="left" width="300px" valign="top" >
                            <b>
                            <asp:Label ID="LblAsuntos" runat="server" Text="Asuntos"></asp:Label>
                            </b></tr>
                        <td align="left" width="300px" valign="top" 
                            class="style1">
                            <b>
                            <asp:Label ID="LblMotivos" runat="server" Text="Motivos"></asp:Label>
                            </b></tr>
                    <tr>
                        <td valign="top" align="left">
                            <asp:DataList ID="DlAsuntos" runat="server" DataSourceID="SqlDataSource2">
                                <ItemTemplate>
                                    &nbsp;<img alt="" src="../images/vineta.gif" />
                                    <asp:Label ID="asuntoLabel" runat="server" Text='<%# Eval("asunto") %>' 
                                        Font-Bold="False" />
                                </ItemTemplate>
                                <ItemStyle Height="20px" Wrap="True" />
                            </asp:DataList>
                        </td>
                        <td valign="top" align="left">
                            <asp:DataList ID="DlMotivos" runat="server" DataSourceID="SqlDataSource3" 
                                Font-Bold="False">
                                <ItemTemplate>
                                    <img alt="" src="../images/vineta.gif" />&nbsp;<asp:Label ID="descripcion_msoLabel" 
                                        runat="server" Text='<%# Eval("descripcion_mso") %>' />
                                    <br />
                                </ItemTemplate>
                                <ItemStyle Height="20px" />
                            </asp:DataList>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="left" colspan="2">
                            &nbsp;</td>
                    </tr>
                </table>
            </td>
                        </tr>
                        <tr>
            <td valign="top" style="font-weight: 700">
                <asp:Label ID="LblEstado" runat="server" Text="Estado de la solicitud&nbsp;"></asp:Label>
                            :
                            </td>
                        </tr>
                        <tr>
            <td valign="top">
                <asp:GridView ID="GvEvaluacion" runat="server" AutoGenerateColumns="False" 
                    DataSourceID="SqlDataSource5" GridLines="Horizontal">
                    <Columns>
                        <asp:BoundField DataField="codigo_eva" HeaderText="codigo_eva" 
                            InsertVisible="False" SortExpression="codigo_eva" Visible="False" />
                        <asp:BoundField DataField="personal" HeaderText="Evaluadores" 
                            SortExpression="personal" >
                            <ItemStyle Width="250px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="codigo_res" HeaderText="Estado" 
                            InsertVisible="False" SortExpression="codigo_res" >
                            <ItemStyle Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="fecha_Eva" HeaderText="Fecha de Evaluación" 
                            SortExpression="fecha_Eva" DataFormatString="{0:dd-MM-yyyy}" >
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="estado_Eva" HeaderText="estado_Eva" 
                            SortExpression="estado_Eva" Visible="False" />
                        <asp:CheckBoxField DataField="activa_eva" HeaderText="activa_eva" 
                            SortExpression="activa_eva" Visible="False" />
                    </Columns>
                    <HeaderStyle CssClass="celdaencabezado" />
                </asp:GridView>
            </td>
                        </tr>
                        </table>
    
    </div>
                <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
                    SelectCommand="SOL_ConsultarSolicitudesPendientes" 
                    SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="3" Name="tipo" Type="Int32" />
                        <asp:QueryStringParameter DefaultValue="" Name="numero_sol" 
                            QueryStringField="codigo_sol" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
                    SelectCommand="SOL_ConsultarSolicitudesPendientes" 
                    SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="4" Name="tipo" Type="Int32" />
                        <asp:QueryStringParameter DefaultValue="" Name="numero_sol" 
                            QueryStringField="codigo_sol" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource5" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
                    SelectCommand="SOL_ConsultarEvaluacionSolicitud" 
                    SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="5" Name="Tipo" Type="Int32" />
                        <asp:QueryStringParameter DefaultValue="" Name="codigo" 
                            QueryStringField="codigo_sol" Type="String" />
                        <asp:Parameter DefaultValue="0" Name="codigo_opc" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
     <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
        SelectCommand="SOL_ConsultarSolicitudesPendientes" 
        SelectCommandType="StoredProcedure">
         <SelectParameters>
             <asp:Parameter DefaultValue="5" Name="tipo" Type="Int32" />
             <asp:QueryStringParameter DefaultValue="" Name="numero_sol" 
                 QueryStringField="codigo_sol" Type="String" />
         </SelectParameters>
    </asp:SqlDataSource>

    </form>
</body>
</html>
