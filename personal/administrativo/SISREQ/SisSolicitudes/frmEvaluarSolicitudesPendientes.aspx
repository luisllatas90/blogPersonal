<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmEvaluarSolicitudesPendientes.aspx.vb" Inherits="SisSolicitudes_ListaDeSolicitudes" %>

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
                <b>EVALUAR MIS SOLICITUDES</b></td>
        </tr>
        
        <tr>
            <td class="FondoPagina" colspan="2">
                <asp:Label ID="Label2" Text ="Ver Solicitudes:  " runat ="server" Width="100px" 
                    style="text-align: right"></asp:Label>&nbsp;
                <asp:DropDownList 
                    ID="CboVer" runat="server" Width="120px" AutoPostBack="True">
                    <%--<asp:ListItem Value="0">Todos</asp:ListItem>--%>
                    <asp:ListItem Value="P">Pendientes</asp:ListItem>
                    <%--<asp:ListItem Value="T">Finalizadas</asp:ListItem>--%>
                    <asp:ListItem Value="1">Aprobadas</asp:ListItem>
                    <asp:ListItem Value="2">Desaprobadas</asp:ListItem>
                    <%--<asp:ListItem Value="A">Anuladas</asp:ListItem>--%>
                </asp:DropDownList>
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label3" Text ="Ciclo académico:" runat ="server" Width="100px" 
                    style="text-align: right"></asp:Label>&nbsp;
                <asp:DropDownList ID="cboCicloAcad" runat="server" 
                    AutoPostBack="True" Width="120px">
                </asp:DropDownList>
            </td>
      <%-- 
            <td class="FondoPagina" colspan="1">
            
                    
            </td>--%>
        </tr>
        <tr>
            <td class="FondoPagina" colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <%--<td valign="top" align="left" style="background:#FFFFC6">
                 <asp:Label ID="LblTotal" runat="server"></asp:Label>
            </td>--%>
            <%--<td valign="top" align="right" style="background:#FFFFC6">
                <span > Para seleccionar una solicitud dar clic en la imagen</span><asp:Image 
                    ID="Image1" runat="server" ImageUrl="~/images/Okey.gif" />
&nbsp;</td>--%>
        </tr>
        <tr>
            <td colspan="2">
                <img  src="../images/raya980b.gif" style="height: 1px; width: 100%" /></td>
        </tr>

        <tr>
            <td valign="top" height="200" align="center" colspan="2" >
                <asp:Panel ID="Panel1" runat="server" Height="113px">
                    <asp:GridView ID="GvSolicitudes" runat="server" AutoGenerateColumns="False" 
                    DataKeyNames="codigo_sol,codigouniver_alu,estado" Width="98%" 
    HorizontalAlign="Left" AllowPaging="True" BorderColor="White" BorderWidth="1px" 
                        CellPadding="4" ForeColor="#333333" GridLines="None" PageSize="5">
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
                                <ItemStyle Width="450px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="estado_Alu" HeaderText="Estado Estudiante" > 
                                <ItemStyle Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="abreviatura_cpf" HeaderText="Carrera Profesional" 
                            SortExpression="abreviatura_cpf" >
                                <ItemStyle Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="responsable_sol" HeaderText="Responsable" 
                            SortExpression="responsable_sol" Visible="False" >
                                <ItemStyle Width="300px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Estado" HeaderText="Estado" ReadOnly="True"
                            SortExpression="Estado" >
                                <ItemStyle Width="85px" />
                            </asp:BoundField>
                           <%-- <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" 
                                        CommandName="Select" ImageUrl="~/Images/aprobar1.png" Text="" />
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                           <asp:CommandField SelectText="" ShowSelectButton="True" />
                          </Columns>
                        <EmptyDataTemplate>
                            <asp:Label ID="Label1" runat="server" ForeColor="Red" 
                            Text="No se encontraron registros para este estudiante"></asp:Label>
                        </EmptyDataTemplate>
            <FooterStyle Font-Bold="True" ForeColor="White" />
            <PagerStyle ForeColor="#003399" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#FF3300" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                </asp:Panel>
            </td>
        </tr>
        
        <tr>
            <td colspan="1">
                <table style="border:opx; width:100%; visibility: hidden;" 
                    id="tblDatos" cellpadding="0" cellspacing="1" runat ="server" >
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
                        
                        <td align="left" width="40%">
                <asp:DetailsView ID="DvEstudiante" runat="server" AutoGenerateRows="False" 
                    DataSourceID="SqlDataSource4" Height="50px" Width="100%" GridLines="None" 
                    BorderWidth="0px">
                    <Fields>
                        <asp:BoundField DataField="codigouniver_alu" HeaderText="Código Universitario" 
                            SortExpression="codigouniver_alu">
                            <ItemStyle Width="70%" />
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
                        <td width="42%" >
                            <table  width="100%" >
                                
                                <tr>
                                    <td>
                                        <asp:Label ID="lblObservacion" runat="server" Text="Observación:" Font-Bold="True" ></asp:Label>   
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan ="2">
                                        <asp:TextBox runat="server" ID="txtObservacion" TextMode="MultiLine" width="100%"  Rows="2"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                                    </td>
                                </tr>
                                <tr style =" text-align  :right ">
                                    <td>
                                        <asp:Label ID="lblUltAsistencia" runat="server" Text="Fecha última asistencia: " ForeColor="red" ></asp:Label>
                                    </td>
                                </tr>
                                <tr style =" text-align  :right ">
                                    <td id="BOTONES" runat ="server" colspan ="2" rowspan ="1">       
                                         <asp:Button runat ="server" ID="cmdAprobarEval" Text ="Aprobar" CssClass ="aprobar1" />&nbsp;&nbsp;
                                         <asp:Button runat ="server" ID="cmdDesaprobarEval" Text ="    Desaprobar" CssClass="desaprobar1"  />&nbsp;&nbsp;
                                         <asp:Button runat ="server" ID="cmdCancelar" Text ="Cancelar" CssClass="cancelar" Visible="false"   />
                                    </td>
                                </tr>
                        </table>
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
                        <td colspan="2">
                        <asp:Label ID="LblEvaluacion" runat="server" Text="Evaluación" Font-Bold="True"></asp:Label>
                    
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
                            <ItemStyle Width="200px" />
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
                   
                    <FooterStyle Font-Bold="True" ForeColor="White" />
            <PagerStyle ForeColor="#003399" HorizontalAlign="Center" />
            <HeaderStyle BackColor="#FF3300" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
                        </td>
                        
                    </tr>
<%--                    <tr>
                         <td colspan ="2">
                            <br />
                            <asp:Label ID="lblObservacion" runat="server" Text="Observación:" Font-Bold="True" ></asp:Label>   
                            <asp:TextBox runat="server" ID="txtObservacion" Width ="60%" TextMode="MultiLine" ></asp:TextBox>&nbsp;&nbsp;&nbsp;
                         </td>
                    </tr>
                    <tr>
                        <td id="BOTONES" runat ="server" style ="TEXT-ALIGN: right; PADDING-RIGHT: 31em" colspan ="2" rowspan ="1">       
                             <asp:Label ID="lblUltAsistencia" runat="server" Text="Fecha última asistencia: " ForeColor="red" ></asp:Label>
                             <asp:Button runat ="server" ID="cmdAprobarEval" Text ="Aprobar" CssClass ="aprobar1" />&nbsp;&nbsp;
                             <asp:Button runat ="server" ID="cmdDesaprobarEval" Text ="    Desaprobar" CssClass="desaprobar1"  />&nbsp;&nbsp;
                             <asp:Button runat ="server" ID="cmdCancelar" Text ="Cancelar" CssClass="cancelar" Visible="false"   />
                        </td>
                    </tr>--%>
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
    <asp:HiddenField ID="hddCodigo_tas" runat="server" />
    <asp:HiddenField ID="HddCodigoSol" runat="server" />
    <asp:HiddenField ID="HddCodigoCco" runat="server" />
    <asp:HiddenField ID="HddEsDirector" runat="server" />

    </form>
</body>
</html>
