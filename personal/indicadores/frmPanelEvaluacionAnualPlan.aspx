<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmPanelEvaluacionAnualPlan.aspx.vb" Inherits="indicadores_frmPanelEvaluacionAnualPlan" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <link href="../css/estilo.css" rel="stylesheet" type="text/css" media="screen" />
        <script src="../../private/funciones.js" type ="text/javascript" language ="javascript"></script>
        <script src="../../private/PopCalendar.js" language="javascript" type="text/javascript"></script>
        <script src="../../private/jq/jquery.js" type="text/javascript"></script>
        <script src="../../private/jq/jquery.mascara.js" type="text/javascript"></script>
        
        <script language="javascript" type="text/javascript">
            $(document).ready(function() {
                jQuery(function($) {
                    $("#txtFechaInicio").mask("99/99/9999");
                });
            })

            $(document).ready(function() {
                jQuery(function($) {
                    $("#txtFechaFin").mask("99/99/9999");
                });
            })
    
         </script>
        <script type="text/javascript" language="javascript">
             function MarcarCursos(obj) {
                 //asignar todos los controles en array
                 var arrChk = document.getElementsByTagName('input');
                 for (var i = 0; i < arrChk.length; i++) {
                     var chk = arrChk[i];
                     //verificar si es Check
                     if (chk.type == "checkbox") {
                         chk.checked = obj.checked;
                         if (chk.id != obj.id) {
                             PintarFilaMarcada(chk.parentNode.parentNode, obj.checked)
                         }
                     }
                 }
             }


             function PintarFilaMarcada(obj, estado) {
                 if (estado == true) {
                     obj.style.backgroundColor = "#FFE7B3"
                 }
                 else {
                     obj.style.backgroundColor = "white"
                 }
             }        
    </script>
        
</head>
<body>
    <form id="form1" runat="server">
        <% Response.Write(ClsFunciones.CargaCalendario())%>
        
    <div>
        <table cellpadding="3" cellspacing="0" style="border: 1px solid #C2CFF1; width:100%">
            <tr>
                <td>
                    <asp:Panel ID="pnlTabs" runat="server">
                            <table cellspacing="0" cellpadding="0" style="border-collapse: collapse;bordercolor: #111111;width:100%">
                            <tr>
                                <td class="pestanabloqueada" id="Td2" align="center" style="height:25px;width:15%" onclick="ResaltarPestana2('0','','');">
                                    <asp:LinkButton 
                                                    ID="lnkEvaluacion" 
                                                    Text="Configuración Evaluación Anual" 
                                                    runat="server" 
                                                    Font-Bold="True" 
                                                    Font-Underline="True" 
                                                    ForeColor="Black">
                                   </asp:LinkButton>
                                </td>
                                <td class="bordeinf" style="height:25px;width:1%">&nbsp;</td>
                                <td class="pestanabloqueada" id="tab" align="center" style="height:25px;width:15%" onclick="ResaltarPestana2('1','','');">
                                    <asp:LinkButton 
                                                    ID="lnkAccesoInformes" 
                                                    runat="server" 
                                                    Font-Bold="True" 
                                                    Text="Configuración de Accesos " 
                                                    Font-Underline="True" 
                                                    ForeColor="Black"> 
                                    </asp:LinkButton>
                                </td>
			                    <td class="bordeinf" style="height:25px;width:1%">&nbsp;</td>
			                    <td class="bordeinf" style="height:25px;width:1%">&nbsp;</td>
			                    
                            </tr>
                        
                        </table>
                        </asp:Panel>
                </td>
            </tr>
        </table>
        <asp:Panel ID="pnlEvaluacion" runat="server">
            <table cellpadding="3" cellspacing="0" style="border: 1px solid #C2CFF1; width:100%">
            <tr>
                <td bgcolor="#E1F1FB" colspan="6" height="30px">
                <b>
                    <asp:Label ID="Label4" runat="server" Text=""></asp:Label></b></td>
            </tr>
             <tr>
                <td width="20%">
                    <asp:Label ID="Label1" runat="server" Text="Seleccione Plan"></asp:Label>
                </td>
                <td width="70%" colspan="5">
                    <asp:DropDownList ID="ddlPlan" Width="99%" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Fecha Inicio"></asp:Label>
                </td>
                <td width="21%">
                    <asp:TextBox ID="txtFechaInicio" runat="server" Width="150px" 
                            ValidationGroup="Subasta" TabIndex="1"></asp:TextBox>
                        <input 
                                id="btnFechaInicio" 
                                onClick="PopCalendar.selectWeekendHoliday(1,1);PopCalendar.show(document.form1.txtFechaInicio,'dd/mm/yyyy')" class="cunia" type="button" />
                                <asp:RequiredFieldValidator 
                                        ID="rfvFechaInicio" 
                                        runat="server" 
                                        ControlToValidate="txtFechaInicio" 
                                        ErrorMessage="Debe de ingresar la Fecha de Inicio" 
                                        ValidationGroup="Enviar">*
                                        </asp:RequiredFieldValidator>
                    </td>
                <td width="10%" align="right">
                        <asp:Label ID="Label3" runat="server" Text="Fecha Fin"></asp:Label>
                </td>
                <td width="21%">
                    <asp:TextBox ID="txtFechaFin" runat="server" Width="150px" 
                            ValidationGroup="Subasta" TabIndex="1"></asp:TextBox>
                        <input 
                                id="btnFechaFin" 
                                onClick="PopCalendar.selectWeekendHoliday(1,1);PopCalendar.show(document.form1.txtFechaFin,'dd/mm/yyyy')" class="cunia" type="button" />
                                <asp:RequiredFieldValidator 
                                        ID="RequiredFieldValidator1" 
                                        runat="server" 
                                        ControlToValidate="txtFechaFin" 
                                        ErrorMessage="Debe de ingresar la Fecha de Fin" 
                                        ValidationGroup="Enviar">*
                                        </asp:RequiredFieldValidator>    
                    
                    </td>
                    <td width="12%" align="right" >
                    <asp:Label ID="Label5" runat="server" Text="Seleccione Año"></asp:Label>
                    </td>
                <td width="21%" align="left">
                                 
                        
                    
                    <asp:DropDownList ID="ddlAño" Width="94%" runat="server">
                    </asp:DropDownList>
                    
                </td>
                
            </tr>
            <tr>
                <td width="20%">
                    <asp:Label ID="Label7" runat="server" Text="Seleccione Centro Costo"></asp:Label>
                </td>
                <td width="80%" colspan="5">
                    <asp:DropDownList ID="ddlCentroCosto" Width="99%" runat="server" 
                        AutoPostBack="True">
                    </asp:DropDownList>
                </td>
            </tr>
             <tr>
                <td width="20%">
                    <asp:Label ID="Label6" runat="server" Text="Seleccione Responsable"></asp:Label>
                </td>
                <td width="80%" colspan="5">
                    <asp:DropDownList ID="ddlResponsable" Width="99%" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td width="20%">
                    <asp:HiddenField ID="hfCodigo_eval" runat="server" />
                </td>
                <td width="80%" align="left" colspan="5">
                    <asp:Button ID="btnAgregar" runat="server" CssClass="agregar2" Height="35px" 
                        TabIndex="18" Text="Agregar" ToolTip="Agregar" 
                        Width="100px" />
                    </b>
                </td>
            </tr>
            <tr>
                <td width="20%">
                    &nbsp;</td>
                <td width="80%" align="right" colspan="5">
                    <asp:GridView ID="gvLista" Width="100%" runat="server" BackColor="White" 
                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                        AutoGenerateColumns="False" DataKeyNames="codigo_eval,codigo_plan,responsable_eval">
                        <RowStyle ForeColor="#000066" />
                        <Columns>
                            <asp:BoundField DataField="codigo_eval" HeaderText="codigo_eval" 
                                Visible="False" >
                            <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Periodo_pla" HeaderText="Plan" >
                            <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="anio_eval" HeaderText="Año" >
                            <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fechaini_eval" HeaderText="Fecha Inicio" >
                            <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fechafin_eval" HeaderText="Fecha Final" >
                            <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="responsable" HeaderText="Responsable" >
                            <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="estado_eval" HeaderText="Estado" Visible="False" />
                            <asp:HyperLinkField DataNavigateUrlFields="rutadocumento_eval" 
                                DataNavigateUrlFormatString="{0}" DataTextField="DocumentoPlan" 
                                HeaderText="Documento" Target="_blank" />
                            <asp:CommandField ButtonType="Image" SelectImageUrl="~/Images/editar.gif" 
                                ShowSelectButton="True" />
                            <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/eliminar.gif" 
                                ShowDeleteButton="True" />
                        </Columns>
                        <FooterStyle BackColor="White" ForeColor="#000066" />
                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                    </asp:GridView>
                    
                </td>
            </tr>
        </table>
        </asp:Panel>
        
        <!-- Para la configuracion de las personas que podran ver el informe de los planes -->
            <asp:Panel ID="pnlVistaDocs" Visible="false" runat="server">
            <table cellpadding="3" cellspacing="0" style="border: 1px solid #C2CFF1; width:100%">
            <tr>
                <td bgcolor="#E1F1FB" colspan="2" height="30px">
                <b>
                    <asp:Label ID="Label8" runat="server" Text=""></asp:Label></b></td>
            </tr>
             <tr>
                <td width="20%">
                    <asp:Label ID="Label9" runat="server" Text="Seleccione el Responsable"></asp:Label>
                </td>
                <td width="70%">
                    <asp:DropDownList ID="ddlPersonalAcceso" Width="99%" runat="server" 
                        AutoPostBack="True">
                    </asp:DropDownList>
                </td>
            </tr>
         </table>
            <table cellspacing="0" cellpadding="0" style="border-collapse: collapse;bordercolor: #111111;width:100%">
                <tr>
                    <td width="20%"></td>
                    <td>
                        <asp:Panel ID="Panel1" runat="server">
                            <table cellspacing="0" cellpadding="0" style="border-collapse: collapse;bordercolor: #111111;width:100%">
                            <tr>
                                <td class="pestanabloqueada" id="Td1" align="center" style="height:25px;width:15%" onclick="ResaltarPestana2('0','','');">
                                    <asp:LinkButton 
                                                    ID="lnkAsignaciones" 
                                                    Text="Asignación de planes por Responsable" 
                                                    runat="server" 
                                                    Font-Bold="True" 
                                                    Font-Underline="True" 
                                                    ForeColor="Black">
                                   </asp:LinkButton>
                                </td>
                                <td class="bordeinf" style="height:25px;width:1%">&nbsp;</td>
                                <td class="pestanabloqueada" id="Td3" align="center" style="height:25px;width:15%" onclick="ResaltarPestana2('1','','');">
                                    <asp:LinkButton 
                                                    ID="lnkConsultaAsignaciones" 
                                                    runat="server" 
                                                    Font-Bold="True" 
                                                    Text="Consulta de Asignaciones por Responsable" 
                                                    Font-Underline="True" 
                                                    ForeColor="Black">  
                                    </asp:LinkButton>
                                </td>
			                    <td class="bordeinf" style="height:25px;width:1%">&nbsp;</td>
			                    <td class="bordeinf" style="height:25px;width:1%">&nbsp;</td>
			                    
                            </tr>
                        
                        </table>
                        </asp:Panel>
                    </td>
                </tr>    
            </table>
            
            <!--Asignaciones-->
                <asp:Panel ID="pnlAsignaciones" runat="server">
                    <table cellpadding="3" cellspacing="0" style="border: 1px solid #C2CFF1; width:100%">
                <tr>
                    <td width="20%">
                        
                    </td>
                    <td width="80%" align="left">
                        <asp:Button ID="btnAgregarDocs" runat="server" CssClass="agregar2" Height="35px" 
                            TabIndex="18" Text="Agregar" ToolTip="Agregar" 
                            Width="100px" />
                        </b>
                    </td>
                </tr>
                <tr>
                    <td width="20%">
                        &nbsp;</td>
                    <td width="80%" align="right">
                        <asp:GridView ID="gvListaPlanesDocs" Width="100%" runat="server" BackColor="White" 
                            BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                            AutoGenerateColumns="False" 
                            DataKeyNames="codigo_eval" 
                            EmptyDataText="Se han asignado todos los planes, verifique en la pestaña consulta  de asignaciones ...">
                            <RowStyle ForeColor="#000066" />
                            <EmptyDataRowStyle BackColor="#FFFF99" Font-Bold="True" />
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkHeader" runat="server" onclick="MarcarCursos(this)" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkElegir" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="codigo_eval" HeaderText="codigo_eval" 
                                    Visible="False" >
                                <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Periodo_pla" HeaderText="Plan" >
                                <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="anio_eval" HeaderText="Año" >
                                <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="fechaini_eval" HeaderText="Fecha Inicio" >
                                <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="fechafin_eval" HeaderText="Fecha Final" >
                                <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="responsable_eval" HeaderText="Responsable" >
                                <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="estado_eval" HeaderText="Estado" Visible="False" />
                                <asp:HyperLinkField DataNavigateUrlFields="rutadocumento_eval" 
                                    DataNavigateUrlFormatString="{0}" DataTextField="DocumentoPlan" 
                                    HeaderText="Documento" Target="_blank" />
                            </Columns>
                            <FooterStyle BackColor="White" ForeColor="#000066" />
                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                        </asp:GridView>
                        
                    </td>
                </tr>
             </table>
                </asp:Panel>
            <!--Fin Asignaciones-->
            
            <!--Muestra las configuraciones de los planes por trabajador-->
                <asp:Panel ID="pnlConsultaAsignaciones" Visible="false" runat="server">
                    <table cellpadding="3" cellspacing="0" style="border: 1px solid #C2CFF1; width:100%">
                <tr>
                    <td width="20%">
                        
                    </td>
                    <td width="80%" align="left">
                        <asp:Button ID="cmdEliminar" runat="server" CssClass="eliminar2" Height="35px" 
                            TabIndex="18" Text="Eliminar" ToolTip="Eliminar" 
                            Width="100px" />
                        </b>
                    </td>
                </tr>
                <tr>
                    <td width="20%">
                        &nbsp;</td>
                    <td width="80%" align="right">
                        <asp:GridView ID="gvListaResponsableDocs" Width="100%" runat="server" BackColor="White" 
                            BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                            AutoGenerateColumns="False" 
                            DataKeyNames="codigo_cae" EmptyDataText="No se encontraron registros ....">
                            <RowStyle ForeColor="#000066" />
                            <EmptyDataRowStyle BackColor="#FFFF99" BorderColor="#FF0066" Font-Bold="True" />
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkHeader" runat="server" onclick="MarcarCursos(this)" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkElegir" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="10px" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="codigo_cae" HeaderText="codigo_cae" 
                                    Visible="False" >
                                <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Periodo_pla" HeaderText="Plan" >
                                <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="anio_eval" HeaderText="Año" />
                                <asp:BoundField DataField="fechareg_cae" HeaderText="Fecha Registro" >
                                <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:HyperLinkField DataNavigateUrlFields="rutadocumento_eval" 
                                    DataNavigateUrlFormatString="{0}" DataTextField="DocumentoPlan" 
                                    HeaderText="Documento" Target="_blank" />
                            </Columns>
                            <FooterStyle BackColor="White" ForeColor="#000066" />
                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                        </asp:GridView>
                        
                    </td>
                </tr>
             </table>
                </asp:Panel>    
            <!--Fin Listado!-->
         
        </asp:Panel>
        <!-- Fin panel vista informes-->
    </div>
    </form>
</body>
</html>
