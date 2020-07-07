<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ListaSolicitudesPorEscuela.aspx.vb" Inherits="SisSolicitudes_ListaSolicitudesPorEscuela" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
    <link href="../private/estilo.css" rel="stylesheet" type="text/css" /> 
    <link href="../private/estiloweb.css" rel="stylesheet" type="text/css" /> 
    <script src="../../../../private/funciones.js" type ="text/javascript" language ="javascript"></script>
    <style type="text/css">
        .style1
        {
            font-weight: normal;
        }
    </style>
    </head>
<body style="background:#F0F0F0" >
    <form id="form1" runat="server">

    <table align="center" width="100%" style="width: 100%">
        <tr>
            <td> 
                                            &nbsp;</td>
        </tr>

        <tr>
            <td> 
                                            Ver Solicitudes :
                                            <asp:DropDownList 
                    ID="CboVer" runat="server" AutoPostBack="True">
                                                <asp:ListItem Value="0">Todos</asp:ListItem>
                                                <asp:ListItem Value="P">Pendientes</asp:ListItem>
                                                <asp:ListItem Value="T">Finalizadas</asp:ListItem>
                                                <asp:ListItem Value="A">Anuladas</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
        </tr>

        <tr>
            <td> 
                                            Buscar por&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :
                                            <asp:DropDownList ID="CboSeleccionar" runat="server" 
                    AutoPostBack="True">
                                                <asp:ListItem Value="3">Código Universitario</asp:ListItem>
                                                <asp:ListItem Value="4">Apellidos y Nombres</asp:ListItem>
                                                <asp:ListItem Value="5">Número de solicitud</asp:ListItem>
                                            </asp:DropDownList>
                                        &nbsp;<asp:TextBox ID="TxtBuscar" runat="server" 
                    Width="30%"></asp:TextBox>
                                            &nbsp;<asp:Button ID="CmdBuscar" runat="server" Text="Buscar" CssClass="Buscar" 
                    Height="24px" Width="70px" />
                                        </td>
        </tr>

        <tr>
            <td class="bordeinf"> 
                &nbsp;</td>
        </tr>

        <tr  class="fondoblanco" style="height:500px">
            <td valign="top" align="center">
                <table style="width:100%; height: 400px;">
                    <tr>
                        <td valign="top" style="height: 400px">
                                <table style="width:100%; height: 600px;" class="ContornoTabla">
                                    <tr align="center">
                                        <td height="15px">
                                            <b>LISTA DE SOLICITUDES</b></td>
                                    </tr>
                                    <tr>
                                        <td height="15px" 
                                            style="color: #808080; font-size: xx-small; font-family: verdana; font-style: normal; font-weight: normal; text-transform: none; background-color: #FFFFCC;">
                                            <asp:Image ID="Image1" runat="server"
                                                ImageUrl="../../../../images/atencion.gif" />
                                            Para ver el detalle de la solicitud dar clic en una de ellas</td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            <asp:Panel ID="Panel1" runat="server" Height="100%" ScrollBars="Auto">
                                                <asp:GridView ID="GvSolicitudes" runat="server" AutoGenerateColumns="False" 
                                                    DataKeyNames="codigo_sol,codigouniver_alu,estado_sol" GridLines="Horizontal" 
                                                    Width="100%">
                                                    <Columns>
                                                        <asp:BoundField DataField="codigo_sol" HeaderText="codigo_sol" 
                                                            InsertVisible="False" ReadOnly="True" SortExpression="codigo_sol" 
                                                            Visible="False" />
                                                        <asp:BoundField DataField="codigouniver_alu" HeaderText="Cod. Universitario" 
                                                            SortExpression="codigouniver_alu">
                                                            <ItemStyle HorizontalAlign="Left" Width="100px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="numero_sol" HeaderText="Solicitud" 
                                                            SortExpression="numero_sol">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="alumno" HeaderText="Alumno" ReadOnly="True" 
                                                            SortExpression="alumno" />
                                                        <asp:CommandField SelectText="" ShowSelectButton="True" />
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        &nbsp;<asp:Label ID="Label1" runat="server" ForeColor="Red" 
                                                            Text="No se encontraron registros"></asp:Label>
                                                    </EmptyDataTemplate>
                                                    <SelectedRowStyle BackColor="#FFFFB9" />
                                                    <HeaderStyle CssClass="TituloTabla" Font-Size="7pt" />
                                                </asp:GridView>
                                            </asp:Panel>
                                            
                                        </td>
                                    </tr>
                                </table>
                        </td>
                        <td>
                            &nbsp;</td>
                        <td width="60%">
                <table ID="TablaDat" style="width:100%; height: 600px;" style="visibility:visible" border="0" 
                                class="ContornoTabla">
                    <tr>
            <td align="center" height="80px" valign="top" >
                <div id="DivInformes" align="justify"  
                    style="position: absolute; font-size: x-small; font-variant: normal; text-transform: none; font-family: verdana; font-weight: normal;">
                                    <b>En esta consulta tendrá las siguientes opciones:</b><br />
                                    <br />
                                    <img alt="" src="../images/vineta.gif" /> En la parte superior 
                    podrá indicar que solicitudes desea visualizar, estas pueden ser pendientes y 
                    finalizadas.<br />
                                    <br />
                                    <img alt="" src="../images/vineta.gif" /> Podrá realizar 
                    busquedas de solicitudes por:<br />
&nbsp;&nbsp;&nbsp;&nbsp; - Código universitario<br />
&nbsp;&nbsp;&nbsp;&nbsp; - Apellidos y nombres<br />
&nbsp;&nbsp;&nbsp;&nbsp; - Número de solicitud<br />
                                    <br />
                                    <img alt="" src="../images/vineta.gif" /> En la sección <b>LISTA DE SOLICITUDES</b> aparecerá la
                     relación de solicitudes de acuerdo al filtro de búsqueda de la parte superior. Para observar el 
                    detalle de cada solicitud deberá dar clic en una de las solicitudes que se 
                    encuentran en la lista</div>
                <table style="width:100%;" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="center" width="15%">
                <asp:Image ID="ImgFoto" runat="server" BorderColor="#666666" BorderWidth="1px" 
                    Height="95px" Width="75px" Visible="False" ImageAlign="Middle" />
                        </td>
                        <td id="idEstudiante" align="left" class="fondoblanco" >
                <asp:DetailsView ID="DvEstudiante" runat="server" AutoGenerateRows="False"
                    DataSourceID="SqlDataSource4" Height="50px" Width="100%" GridLines="None" 
                    BorderWidth="0px" >
                    <Fields>
                        <asp:BoundField DataField="codigouniver_alu" HeaderText="Código Universitario:" 
                            SortExpression="codigouniver_alu">
                            <HeaderStyle Width="130px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="alumno" HeaderText="Apellidos y Nombres:" 
                            ReadOnly="True" SortExpression="alumno" />
                        <asp:BoundField DataField="sexo_alu" HeaderText="Sexo:" 
                            SortExpression="sexo_alu" />
                        <asp:BoundField DataField="nombre_cpf" HeaderText="Carrera Profesional:" 
                            SortExpression="nombre_cpf" />
                        <asp:BoundField DataField="descripcion_pes" HeaderText="Plan de Estudios:" 
                            SortExpression="descripcion_pes" />
                        <asp:BoundField DataField="estadoactual_alu" HeaderText="Estado Actual:" 
                            SortExpression="estadoactual_alu" />
                        <asp:BoundField DataField="beneficio_alu" HeaderText="Beneficio:" />
                        <asp:BoundField DataField="estadoDeuda_Alu" HeaderText="Tiene deuda:" />
                    </Fields>
                </asp:DetailsView>
                        </td>
                    </tr>
                </table>
            </td>
                    </tr>
                        <tr>
            <td height="20px">
                &nbsp;</td>
                        </tr>
                        <tr>
            <td height="20px">
                <asp:Label ID="LblDatos" runat="server" Text="DATOS DE LA SOLICITUD"></asp:Label>
                            </td>
                        </tr>
                        <tr>
            <td valign="top" height="100px" >
                <asp:DetailsView ID="DlSolicitud" runat="server" AutoGenerateRows="False" 
                    DataSourceID="SqlDataSource1" GridLines="None">
                    <RowStyle Height="18px" HorizontalAlign="Left" />
                    <Fields>
                        <asp:BoundField DataField="numero_sol" HeaderText="Número de Solicitud:" 
                            SortExpression="numero_sol">
                            <HeaderStyle Width="150px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="fecha_sol" HeaderText="Fecha de Solicitud:" 
                            SortExpression="fecha_sol" />
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
            <td valign="top" class="style1" >
                <asp:Label ID="LblEstadoText" runat="server" Text="Estado de la solicitud:"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="LblEstadoDes" runat="server"></asp:Label>
            </td>
                        </tr>
                        <tr>
            <td valign="top" height="100px">
                <table style="width: 100%;">
                    <tr>
                        <td align="left" width="300px" valign="top" >
                            <b>
                            <asp:Label ID="LblAsuntos" runat="server" Text="Asuntos"></asp:Label>
                            </b></tr>
                        <td align="left" width="300px" valign="top" >
                            <b>
                            <asp:Label ID="LblMotivos" runat="server" Text="Motivos"></asp:Label>
                            </b></tr>
                    <tr>
                        <td valign="top" align="left">
                            <asp:DataList ID="DlAsuntos" runat="server" DataSourceID="SqlDataSource2">
                                <ItemTemplate>
                                    &nbsp;<img alt="" src="../images/vineta.gif" />
                                    <asp:Label ID="asuntoLabel" runat="server" Text='<%# Eval("asunto") %>' />
                                </ItemTemplate>
                                <ItemStyle Height="20px" />
                            </asp:DataList>
                        </td>
                        <td valign="top" align="left">
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
                    </table>
            </td>
                        </tr>
                        <tr>
            <td valign="top" height="20px">
                <asp:Label ID="LblEstado" runat="server" Text="EVALUACIÓN"></asp:Label>
                            </td>
                        </tr>
                        <tr>
            <td valign="top">
                <asp:GridView ID="GvEvaluacion" runat="server" AutoGenerateColumns="False" 
                    DataSourceID="SqlDataSource5" GridLines="Horizontal" Width="100%">
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
                        <asp:BoundField DataField="descripcion_res" HeaderText="Resultado" 
                            SortExpression="descripcion_res" >
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="fecha_Eva" HeaderText="Fecha de Evaluación" 
                            SortExpression="fecha_Eva" DataFormatString="{0:dd-MM-yyyy}" >
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:CheckBoxField DataField="activa_eva" HeaderText="activa_eva" 
                            SortExpression="activa_eva" Visible="False" />
                    </Columns>
                    <HeaderStyle CssClass="celdaencabezado" />
                </asp:GridView>
                <br />
            </td>
                        </tr>
                        </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="bordeinf" colspan="3">
                            <asp:Label ID="LblTotal" runat="server" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    </table>
            </td>
        </tr>

        <tr>
            <td>
                &nbsp;</td>
        </tr>
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

    <asp:HiddenField ID="HddAlumno" runat="server" />

            </td>
        </tr>
    </table>

    </form>
</body>
</html>
