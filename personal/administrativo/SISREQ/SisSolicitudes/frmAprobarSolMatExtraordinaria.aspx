<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmAprobarSolMatExtraordinaria.aspx.vb" Inherits="administrativo_SISREQ_SisSolicitudes_frmConfirmarPagoSolicitudes" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        TBODY {
	display: table-row-group;
}
.pestanabloqueada {
	border: 1px solid #808080;
	background-color: #E1F1FB;
	cursor: hand;
}
.bordeinf {
	border-left-width: 1;
	border-right-width: 1;
	border-top-width: 1;
	border-bottom: 1px solid #808080;
}
.pestanaresaltada {
	border-left: 1px solid #808080;
	border-right: 1px solid #808080;
	border-top: 1px solid #808080;
	color: #0000FF;
	background-color: #EEEEEE;
	font-weight: bold;
	cursor: hand;
}
.pestanarevez {
	border-left: 1px solid #808080;
	border-right: 1px solid #808080;
	border-top-width: 1;
	border-bottom: 1px solid #808080;
}
        #fradetalle
        {
            height: 3%;
            width: 98%;
        }
        .style1
        {
            border-left-width: 1;
            border-right-width: 1;
            border-top-width: 1;
            border-bottom: 1px solid #808080;
            height: 25px;
            width: 1%;
        }
        .style2
        {
            width: 131px;
            height: 345px;
        }
        .style5
        {
            height: 345px;
            width: 1076px;
        }
        .style7
        {
            border-left-width: 1;
            border-right-width: 1;
            border-top-width: 1;
            border-bottom: 1px solid #808080;
            height: 123px;
            width: 131px;
        }
        .style8
        {
            width: 1076px;
            height: 123px;
        }
        .style9
        {
            height: 123px;
        }
    .agregar2 {
	border: 1px solid #666666;
	background: #FEFFE1 url('../../../../images/anadir.gif') no-repeat 0% center;
	width: 80;
	font-family: Tahoma;
	font-size: 8pt;
	height: 20;
	cursor: hand;
}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        Tipo Solicitud:
                                <asp:DropDownList ID="cboEstado" runat="server" 
                                    Height="25px" Width="160px">
                                    <asp:ListItem Value="10">Examen Extraordinario</asp:ListItem>
                                </asp:DropDownList>
                                &nbsp;Estado:
                                <asp:DropDownList ID="cboTipo" runat="server" 
                                    Height="25px" Width="160px">
                                    <asp:ListItem Value="1">Evaluar</asp:ListItem>
                                    <asp:ListItem Value="2">Asignar</asp:ListItem>
                                </asp:DropDownList>
                                &nbsp;Dni/Apellido:
                                <asp:TextBox ID="txtParametroBusqueda" runat="server" 
            Width="169px"></asp:TextBox>
                                &nbsp;<asp:Button ID="btnBuscar" runat="server" 
            Text="Buscar" Width="125px" />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                &nbsp;<br />
        <asp:Label ID="Label6" runat="server" style="margin-left: 0px" 
            Text="Solicitudes Pendientes de Aprobación:" Width="242px"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Panel ID="Panel1" runat="server" BorderStyle="Groove" Height="254px" 
            Width="1250px">
            <asp:GridView ID="gvLista" Width="100%" 
            runat="server" CellPadding="4" 
                        AutoGenerateColumns="False" EmptyDataText="No se encontraron registros.." 
                        AllowPaging="True" PageSize="6" 
            
    
                DataKeyNames="codigo_Sol,codigo_Alu,estado_Sol,cicloIng_Alu,descripcion_Pes,nombre_Cpf,codigo_Pes,CodigoCac_AprobacionSol" 
                Font-Bold="True" Height="195px" 
    Font-Size="Small" GridLines="Horizontal" BackColor="White" BorderColor="#336666" 
                BorderStyle="Double" BorderWidth="3px">
                <RowStyle BackColor="White" ForeColor="#333333" />
                <EmptyDataRowStyle BackColor="#FFFF99" ForeColor="#FF3300" />
                <Columns>
                    <asp:BoundField HeaderText="Cód. Sol" Visible="False" 
                                DataField="codigo_Sol" >
                    <ItemStyle Width="15px" ForeColor="Black" />
                    </asp:BoundField>
                    <asp:BoundField DataField="numero_Sol" HeaderText="Núm. Solicitud" >
                    <ItemStyle ForeColor="Black" />
                    </asp:BoundField>
                    <asp:BoundField DataField="codigo_Alu" HeaderText="Cod. Estud." 
                        Visible="False" >
                    <ItemStyle ForeColor="Black" />
                    </asp:BoundField>
                    <asp:BoundField DataField="codigoUniver_Alu" HeaderText="Cod. Universitario" >
                    <ItemStyle ForeColor="Black" />
                    </asp:BoundField>
                    <asp:BoundField DataField="estudiante" HeaderText="Estudiante" >
                    <ItemStyle ForeColor="Black" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Fecha Reg." 
                                DataField="fechaRegistro_Sol" >
                    <ItemStyle ForeColor="Black" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Est. Sol" Visible="False" 
                                DataField="estado_Sol" >
                    <ItemStyle ForeColor="Black" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Estado" DataField="Estado" >
                    <ItemStyle ForeColor="Black" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Tipo Solicitud" DataField="descripcion_Tas" >
                    <ControlStyle BorderStyle="None" />
                    </asp:BoundField>
                    <asp:CommandField ShowSelectButton="True" ButtonType="Image" 
                        SelectImageUrl="~/administrativo/SISREQ/images/check.gif" />
                </Columns>
                <FooterStyle BackColor="White" ForeColor="#333333" />
                <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
            </asp:GridView>
        </asp:Panel>
    
    </div>
    <div>
    
	<table cellspacing="0" cellpadding="0" 
                    
            style="border-collapse: collapse;bordercolor: #111111;width:100%; height: 803px;">
		<tr>
			<td class="pestanabloqueada" id="tab4" align="center" 
                style="height:25px;width:12%" onclick="ResaltarPestana2('0','','');">
                <asp:LinkButton ID="lnkDatosEvento" runat="server" Font-Bold="True" 
                    Font-Underline="True" ForeColor="Blue">Evaluación</asp:LinkButton>
            </td>
			<td class="bordeinf" style="height:25px;width:1%">&nbsp;</td>
			<td class="pestanaresaltada" id="tab5" align="center" 
                style="height:25px;width:12%" onclick="ResaltarPestana2('1','','');">
                <asp:LinkButton ID="lnkVerSolicitud" runat="server" Font-Bold="True" 
                    Font-Underline="True" ForeColor="Blue">Ver Solicitud</asp:LinkButton>
            </td>
            <td class="bordeinf" style="height:25px;width:1%">&nbsp;</td>
			<td class="pestanaresaltada" id="tab6" align="center" 
                style="height:25px;width:12%" onclick="ResaltarPestana2('1','','');">
                <asp:LinkButton ID="lnkHistorial" runat="server" Font-Bold="True" 
                    Font-Underline="True" ForeColor="Blue">Ver Historial</asp:LinkButton>
            </td>
            <td class="bordeinf" style="height:25px;width:1%">&nbsp;</td>
			<td class="pestanaresaltada" id="tab7" align="center" 
                style="height:25px;width:12%" onclick="ResaltarPestana2('1','','');">
                <asp:LinkButton ID="lnkDeudas" runat="server" Font-Bold="True" 
                    Font-Underline="True" ForeColor="Blue">Ver Deudas</asp:LinkButton>
            </td>
            <!-- por mvillavicencio 18/07/12 -->
            <td class="style1">&nbsp;</td>
		</tr>
		<tr>
		<!-- por mvillavicencio 18/07/12 colspan='12' por "14" -->
    	<td style="height:600px;" valign="top" colspan="8" class="pestanarevez">
			<iframe id="fradetalle" width="100%" border="0" frameborder="0" runat="server" 
                name="I1">
			</iframe>
                &nbsp;<asp:Panel ID="pnlDatosEstudiante" runat="server" BorderStyle="Groove" 
            Height="787px" Visible="False" Width="1250px">
                    <table style="width:100%; height: 723px;">
                        <tr>
                            <td id="tbl" class="style7">
                                <asp:Image ID="imgFoto" runat="server" Height="120px" Width="118px" />
                            </td>
                            <td class="style8">
                                <asp:Panel ID="Panel4" runat="server" BorderStyle="Groove" Height="108px">
                                    <asp:Label ID="Label1" runat="server" Text="Cod. Universitario" Width="130px"></asp:Label>
                                    &nbsp;:<asp:TextBox ID="txtCodigoUni" runat="server" Width="302px"></asp:TextBox>
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:TextBox ID="txtCod_Cco" runat="server" Visible="False" Width="73px"></asp:TextBox>
                                    <br />
                                    <asp:Label ID="Label3" runat="server" Text="Ciclo de ingreso" Width="130px"></asp:Label>
                                    &nbsp;:<asp:TextBox ID="txtCiclo" runat="server" Width="302px"></asp:TextBox>
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:TextBox ID="txtSol" runat="server" Visible="False" Width="73px"></asp:TextBox>
                                    <br />
                                    <asp:Label ID="Label4" runat="server" Text="Carrera profesional" Width="130px"></asp:Label>
                                    &nbsp;:<asp:TextBox ID="txtCarrera" runat="server" Width="302px"></asp:TextBox>
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:TextBox ID="txtCodigoAlu" runat="server" Visible="False" Width="73px"></asp:TextBox>
                                    <br />
                                    <asp:Label ID="Label5" runat="server" Text="Plan de estudios" Width="130px"></asp:Label>
                                    &nbsp;:<asp:TextBox ID="txtPlan" runat="server" Width="302px"></asp:TextBox>
                                </asp:Panel>
                            </td>
                            <td class="style9">
                                </td>
                        </tr>
                        <tr>
                            <td class="style2">
                                </td>
                            <td class="style5">
                                <asp:Panel ID="Panel3" runat="server" BorderStyle="Groove" Height="380px" 
                                    style="margin-top: 0px">
                                    Evaluación de Solicitud:<br />
                                    Ciclo Académico&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :
                                    <asp:DropDownList ID="dpCodigo_cac" runat="server" Height="25px" Width="130px">
                                    </asp:DropDownList>
                                    &nbsp;Veredicto:
                                    <asp:DropDownList ID="cboAprobacion" runat="server" AutoPostBack="True" 
                                        Height="25px" Width="130px">
                                        <asp:ListItem Value="A">Aprobado</asp:ListItem>
                                        <asp:ListItem Value="R">Rechazado</asp:ListItem>
                                    </asp:DropDownList>
                                    <br />
                                    Observaciones:<br />
                                    <asp:TextBox ID="txtObservacion" runat="server" Height="53px" 
                                        TextMode="MultiLine" Width="443px"></asp:TextBox>
                                    <br />
                                    <asp:Button ID="btnCalificar" runat="server" Text="Calificar" Width="125px" />
                                    <br />
                                    <asp:Panel ID="Panel2" runat="server" BorderStyle="Groove" Height="228px">
                                        <asp:Label ID="Label7" runat="server" Text="Detalles de Solicitud:"></asp:Label>
                                        <br />
                                        <asp:GridView ID="gvListaCursos" runat="server" AllowPaging="True" 
                                            AutoGenerateColumns="False" BackColor="White" BorderColor="#336666" 
                                            BorderStyle="Double" BorderWidth="3px" CellPadding="4" 
                                            DataKeyNames="codigo_Sol,codigo_Cur,evaluacion" 
                                            EmptyDataText="No se encontraron registros.." Enabled="False" Font-Bold="True" 
                                            Font-Size="Small" GridLines="None" Height="168px" PageSize="6" Width="99%">
                                            <RowStyle BackColor="White" ForeColor="#333333" />
                                            <EmptyDataRowStyle BackColor="#FFFF99" ForeColor="#FF3300" />
                                            <Columns>
                                                <asp:BoundField DataField="codigo_Sol" HeaderText="Cód. Sol" Visible="False">
                                                    <ItemStyle ForeColor="Black" Width="15px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="numero_Sol" HeaderText="Núm. Solicitud">
                                                    <ItemStyle ForeColor="Black" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="codigo_Cur" HeaderText="Cod. Curso">
                                                    <ItemStyle ForeColor="Black" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="descripcion" HeaderText="Curso/Descripción">
                                                    <ItemStyle ForeColor="Black" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Estado" HeaderText="estado" />
                                                <asp:CommandField ButtonType="Image" 
                                                    SelectImageUrl="~/administrativo/SISREQ/images/Okey.gif" SelectText="Aprobar" 
                                                    ShowSelectButton="True" />
                                                <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/eliminar.gif" 
                                                    DeleteText="Rechazar" ShowDeleteButton="True" />
                                                <asp:CommandField ButtonType="Image" EditImageUrl="~/Images/asignar.gif" 
                                                    EditText="Asignar" ShowEditButton="True" />
                                            </Columns>
                                            <FooterStyle BackColor="White" ForeColor="#333333" />
                                            <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                                            <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
                                        </asp:GridView>
                                    </asp:Panel>
                                </asp:Panel>
                                <asp:Panel ID="pnlProgramarProfesor" runat="server" BorderStyle="Groove" 
                                    Height="183px" Visible="False">
                                    Programar Profesor:<br />
                                    Profesor:
                                    <asp:DropDownList ID="dpCodigo_per" runat="server">
                                    </asp:DropDownList>
                                    &nbsp;<asp:Button ID="cmdAsignarProfesor" runat="server" CssClass="agregar2" 
                                        Text="Agregar" />
                                    <asp:GridView ID="grwProfesores" runat="server" AutoGenerateColumns="False" 
                                        BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" 
                                        CaptionAlign="Top" CellPadding="4" 
                                        EnableModelValidation="True" Font-Size="Small" GridLines="None" 
                                        Width="60%" EmptyDataText="No se encontraron registros..">
                                        <RowStyle BackColor="White" BorderColor="#C2CFF1" ForeColor="#333333" />
                                        <EmptyDataRowStyle BorderStyle="None" BorderWidth="0px" Font-Bold="True" 
                                            ForeColor="Red" />
                                        <Columns>
                                            <asp:BoundField HeaderText="Profesor">
                                                <ItemStyle Font-Underline="True" ForeColor="#0066CC" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Función">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Hrs. Asesoría">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Total Hrs.">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:CommandField ButtonType="Image" 
                                                DeleteImageUrl="../../../images/eliminar.gif" ShowDeleteButton="True">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:CommandField>
                                        </Columns>
                                        <FooterStyle BackColor="White" ForeColor="#333333" />
                                        <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                                        <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#336666" BorderColor="#99BAE2" BorderStyle="Solid" 
                                            BorderWidth="1px" Font-Bold="True" ForeColor="White" />
                                    </asp:GridView>
                                </asp:Panel>
                                <br />
                            </td>
                        </tr>
                    </table>
        </asp:Panel>
    
		</td>
	  </tr>
	</table>
                <br />
    
                <br />
    
                <br />
    
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
    
    </div>
    </form>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    </body>
</html>
