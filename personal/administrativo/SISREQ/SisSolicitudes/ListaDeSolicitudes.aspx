<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ListaDeSolicitudes.aspx.vb" Inherits="SisSolicitudes_ListaDeSolicitudes" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
    <link href="../private/estilo.css" rel="stylesheet" type="text/css" /> 
    <link href="../private/estiloweb.css" rel="stylesheet" type="text/css" /> 
    <script src="../../../../private/funciones.js" type ="text/javascript" language ="javascript"></script>

</head>
<body >
    <form id="form1" runat="server">

    <table style="width:100%;" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td align="left" class="FondoPagina" height="18" colspan="2">
                <b>LISTA DE SOLICITUDES PENDIENTES Y FINALIZADAS POR CICLO ACADÉMICO</b></td>
        </tr>
        <tr>
            <td class="FondoPagina" colspan="2">
                &nbsp;Ver Solicitudes:&nbsp;&nbsp;<asp:DropDownList 
                    ID="CboVer" runat="server" Width="84px">
                   <%-- <asp:ListItem Value="0">Todos</asp:ListItem>--%>
                    <asp:ListItem Value="P">Pendientes</asp:ListItem>
                    <asp:ListItem Value="A">Aceptadas</asp:ListItem>
                    <asp:ListItem Value="R">Rechazadas</asp:ListItem>
                    <asp:ListItem Value="N">Anuladas</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="FondoPagina" colspan="2">
                &nbsp;Ciclo académico: <asp:DropDownList ID="cboCicloAcad" runat="server">
                </asp:DropDownList>
&nbsp;&nbsp;<asp:Button ID="CmdBuscar" runat="server" Text="     Buscar" CssClass="Buscar" 
                    Height="24px" Width="80px" />
            </td>
        </tr>
        <tr>
            <td class="FondoPagina" colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td valign="top" align="left" style="background:#FFFFC6">
                 <asp:Label ID="LblTotal" runat="server"></asp:Label>
            </td>
            <td valign="top" align="right" style="background:#FFFFC6">
                <span > Para seleccionar una solicitud dar clic en la imagen</span><asp:Image 
                    ID="Image1" runat="server" ImageUrl="~/images/Okey.gif" />
&nbsp;</td>
        </tr>
        <tr>
            <td colspan="2">
                <img  src="../images/raya980b.gif" style="height: 1px; width: 100%" /></td>
        </tr>

        <tr>
            <td valign="top" height="200" align="center" colspan="2" >
                <asp:Panel ID="Panel1" runat="server" Height="200px" ScrollBars="Vertical">
                    <asp:GridView ID="GvSolicitudes" runat="server" AutoGenerateColumns="False" 
                    DataKeyNames="codigo_sol,codigouniver_alu,estado" Width="100%" 
    HorizontalAlign="Left">
                        <Columns>
                            <asp:BoundField DataField="codigo_sol" HeaderText="codigo_sol" 
                            InsertVisible="False" ReadOnly="True" SortExpression="codigo_sol" 
                            Visible="False" />
                            <asp:BoundField DataField="codigo_alu" HeaderText="codigo_alu" 
                            InsertVisible="False" ReadOnly="True" SortExpression="codigo_alu" 
                            Visible="False" />
                            <asp:BoundField DataField="numero_sol" HeaderText="Nro. de Sol." 
                            SortExpression="numero_sol" >
                                <ItemStyle Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Fecha_Sol" HeaderText="Fecha de Sol." 
                            ReadOnly="True" SortExpression="Fecha_Sol" DataFormatString="{0:dd/MM/yyyy}" >
                                <ItemStyle Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="codigouniver_alu" HeaderText="Código Estudiante" 
                                SortExpression="codigouniver_alu">
                                <ItemStyle HorizontalAlign="Center" Width="85px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="alumno" HeaderText="Estudiante" ReadOnly="True" 
                            SortExpression="alumno" >
                                <ItemStyle Width="250px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="estado_Alu" HeaderText="Estado Estudiante" />
                            <asp:BoundField DataField="abreviatura_cpf" HeaderText="Carrera Profesional" 
                            SortExpression="abreviatura_cpf" >
                                <ItemStyle Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="responsable_sol" HeaderText="Responsable" 
                            SortExpression="responsable_sol" Visible="False" >
                                <ItemStyle Width="200px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nivel" HeaderText="Nivel de Sol." ReadOnly="True" 
                            SortExpression="nivel" >
                                <ItemStyle Width="250px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Estado" HeaderText="Estado" ReadOnly="True" 
                            SortExpression="Estado" >
                                <ItemStyle Width="85px" />
                            </asp:BoundField>
                            <asp:CommandField SelectText=" Seleccionar" ShowSelectButton="True" 
                            ButtonType="Image" SelectImageUrl="~/images/Okey.gif" >
                                <ItemStyle Width="10px" />
                            </asp:CommandField>
                        </Columns>
                        <EmptyDataTemplate>
                            <asp:Label ID="Label1" runat="server" ForeColor="Red" 
                            Text="No se encontraron registros para este estudiante"></asp:Label>
                        </EmptyDataTemplate>
                        <SelectedRowStyle BackColor="#FFFF99" />
                        <HeaderStyle Height="8px" BackColor="#4182CD" ForeColor="White" />
                    </asp:GridView>
                </asp:Panel>
            </td>
        </tr>

        <tr style="height:1px; background-color:Gray">
            <td colspan="2"></td>
        </tr>
        <tr>
            <td colspan="2">
                <table style="border: medium double #CCCCCC; width:100%; visibility: hidden;" 
                    id="tblDatos" cellpadding="0" cellspacing="1">
                    <tr class="titulocel">
            <td colspan="2">
                <asp:Label ID="LblDatosEst" runat="server" Text="Datos del Estudiante" 
                    style="font-weight: 700"></asp:Label>
            </td>
                    </tr>
                    <tr>
            <td align="center" colspan="2">
                <table style="width:100%; background-color: #F7F7F7;" border="0" 
                    cellpadding="0" cellspacing="0">
                    <tr style="background-color: #F7F7F7">
                        <td align="left" width="18%">
                <asp:Image ID="ImgFoto" runat="server" BorderColor="#666666" BorderWidth="1px" 
                    Height="95px" Width="75px" Visible="False" ImageAlign="Middle" />
                        </td>
                        <td align="left">
                <asp:DetailsView ID="DvEstudiante" runat="server" AutoGenerateRows="False" 
                    DataSourceID="SqlDataSource4" Height="50px" Width="100%" GridLines="None" 
                    BorderWidth="0px">
                    <Fields>
                        <asp:BoundField DataField="codigouniver_alu" HeaderText="Código Universitario" 
                            SortExpression="codigouniver_alu">
                            <ItemStyle Width="80%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="alumno" HeaderText="Apellidos y Nombres" 
                            ReadOnly="True" SortExpression="alumno" >
                        </asp:BoundField>
                        <asp:BoundField DataField="sexo_alu" HeaderText="Sexo" 
                            SortExpression="sexo_alu" >
                        </asp:BoundField>
                        <asp:BoundField DataField="nombre_cpf" HeaderText="Carrera Profesional" 
                            SortExpression="nombre_cpf" >
                        </asp:BoundField>
                        <asp:BoundField DataField="descripcion_pes" HeaderText="Plan de Estudios" 
                            SortExpression="descripcion_pes" >
                        </asp:BoundField>
                        <asp:BoundField DataField="estadoactual_alu" HeaderText="Estado Actual" 
                            SortExpression="estadoactual_alu" >
                        </asp:BoundField>
                        <asp:BoundField DataField="beneficio_alu" HeaderText="Beneficio" />
                        <asp:BoundField DataField="estadodeuda_alu" HeaderText="Tiene deuda" />
                    </Fields>
                </asp:DetailsView>
                        </td>
                    </tr>
                </table>
            </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
            <td colspan="2">
                <b>
                <asp:Label ID="LblDatosSol" runat="server" Text="Datos de la Solicitud"></asp:Label>
                &nbsp; </b>
            </td>
                    </tr>
                    <tr>
            <td align="left" rowspan="4" valign="top">
                &nbsp;<asp:DetailsView ID="DlSolicitud" runat="server" AutoGenerateRows="False" 
                    DataSourceID="SqlDataSource1" GridLines="None" Width="100%">
                    <RowStyle Height="18px" HorizontalAlign="Left" />
                    <Fields>
                        <asp:BoundField DataField="numero_sol" HeaderText="Número de Solicitud" 
                            SortExpression="numero_sol">
                            <ItemStyle Width="70%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="fecha_sol" HeaderText="Fecha de Solicitud" 
                            SortExpression="fecha_sol" />
                        <asp:BoundField DataField="responsable_Sol" HeaderText="Responsable" 
                            SortExpression="responsable_Sol" />
                        <asp:BoundField DataField="dniResponsable_Sol" 
                            HeaderText="Documento de Identidad" SortExpression="dniResponsable_Sol" />
                        <asp:BoundField DataField="direccionresponsable_sol" 
                            HeaderText="Dirección del Responsable" 
                            SortExpression="direccionresponsable_sol" />
                        <asp:BoundField DataField="telefonoResponsable_sol" 
                            HeaderText="Teléfono del Responsable" 
                            SortExpression="telefonoResponsable_sol" />
                        <asp:BoundField DataField="observaciones_Sol" HeaderText="Observaciones" 
                            SortExpression="observaciones_Sol" />
                    </Fields>
                </asp:DetailsView>
                <asp:Label ID="LblEstadoDes" runat="server" Text="Estado de la Solicitud"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                <asp:Label ID="LblEstado" runat="server" Font-Bold="True" ></asp:Label>
            </td>
            <td align="left">
                            <asp:Label ID="LblAsuntos" runat="server" Text="Asuntos" Font-Bold="True"></asp:Label>
            </td>
                    </tr>
                    <tr>
            <td align="left" valign="top" width="40%">
                            <asp:DataList ID="DlAsuntos" runat="server" DataSourceID="SqlDataSource2">
                                <ItemTemplate>
                                    &nbsp;<img alt="" src="../images/vineta.gif" />
                                    <asp:Label ID="asuntoLabel" runat="server" Text='<%# Eval("asunto") %>' />
                                </ItemTemplate>
                                <ItemStyle Height="20px" />
                            </asp:DataList>
            </td>
                    </tr>
                    <tr>
            <td align="left">
                            <asp:Label ID="LblMotivos" runat="server" Text="Motivos" Font-Bold="True"></asp:Label>
            </td>
                    </tr>
                    <tr>
            <td align="left" valign="top">
                            <asp:DataList ID="DlMotivos" runat="server" DataSourceID="SqlDataSource3">
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
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                <asp:Label ID="LblEvaluacion" runat="server" Text="Evaluación" Font-Bold="True"></asp:Label>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="2">
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
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="descripcion_Res" HeaderText="Resultado" 
                            SortExpression="estado_Eva" >
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="150px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="fecha_Eva" HeaderText="Fecha de Evaluación" 
                            SortExpression="fecha_Eva" DataFormatString="{0:dd-MM-yyyy}" >
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:CheckBoxField DataField="activa_eva" HeaderText="activa_eva" 
                            SortExpression="activa_eva" Visible="False" />
                    </Columns>
                    <HeaderStyle CssClass="TituloTabla" />
                </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;</td>
                    </tr>
                </table>
                </td>
        </tr>
        <tr>
            <td valign="top" style="font-weight: 700" colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="2">
    <table style="width:100%;">
        <tr>
            <td>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
                    SelectCommand="SOL_ConsultarSolicitudesPendientes" 
                    SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="4" Name="tipo" Type="Int32" />
                        <asp:ControlParameter ControlID="GvSolicitudes" DefaultValue="" 
                            Name="numero_sol" PropertyName="SelectedValue" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
                    SelectCommand="SOL_ConsultarSolicitudesPendientes" 
                    SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="3" Name="tipo" Type="Int32" />
                        <asp:ControlParameter ControlID="GvSolicitudes" DefaultValue="" 
                            Name="numero_sol" PropertyName="SelectedValue" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource4" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
                    SelectCommand="SOL_ConsultarListaSolicitudes" 
                    SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="3" Name="tipo" Type="Int32" />
                        <asp:ControlParameter ControlID="GvSolicitudes" DefaultValue="" Name="param1" 
                            PropertyName="SelectedValue" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource5" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
                    SelectCommand="SOL_ConsultarEvaluacionSolicitud" 
                    SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="5" Name="Tipo" Type="Int32" />
                        <asp:ControlParameter ControlID="GvSolicitudes" DefaultValue="" Name="codigo" 
                            PropertyName="SelectedValue" Type="String" />
                        <asp:Parameter DefaultValue="0" Name="codigo_opc" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
    </table>
     <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
        SelectCommand="SOL_ConsultarSolicitudesPendientes" 
        SelectCommandType="StoredProcedure">
         <SelectParameters>
             <asp:Parameter DefaultValue="5" Name="tipo" Type="Int32" />
             <asp:ControlParameter ControlID="GvSolicitudes" DefaultValue="" 
                 Name="numero_sol" PropertyName="SelectedValue" Type="String" />
         </SelectParameters>
    </asp:SqlDataSource>
            </td>
        </tr>
    </table>

    <asp:HiddenField ID="HddAlumno" runat="server" />

    </form>
</body>
</html>
