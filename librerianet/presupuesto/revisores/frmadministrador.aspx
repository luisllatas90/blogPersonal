<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmadministrador.aspx.vb" Inherits="frmadministrador" %>
<%@ Register assembly="BusyBoxDotNet" namespace="BusyBoxDotNet" tagprefix="busyboxdotnet" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Revisión de Presupuesto:: Administración General</title>
    <script type="text/javascript" language="JavaScript" src="../../../private/funciones.js"></script>
    <link href="../../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../../private/jq/jquery.js"></script>
    <script type="text/javascript" language="javascript">
        function MarcarTodo(obj,cmd)
        {
           $(".CssCheckBox > input").each(function(){
                this.checked = obj;
            });
            if (obj==false)
                {cmd.disabled=true}
            else
                {cmd.disabled=false}
        }
        
        $(document).ready(function(){
            //Aquí va el código JQuery
            
        });
        
        function HabilitarEnvio(idcheck,boton)
        {
            var total=0
            $(".CssCheckBox > input").each(function(){
                if (this.checked ==true)
                    {total=total+1}
            });
            
            //Pintar Fila
		    if (idcheck.parentNode.parentNode.tagName=="TR"){
		        PintarFilaMarcada(idcheck.parentNode.parentNode,idcheck.checked)
		    }	
	        //Habilitar botón
            if (total==0)
                {boton.disabled=true}
            else
                {boton.disabled=false}
         }
         
        function PintarFilaMarcada(obj,estado)
        {
            if (estado==true){
                obj.style.backgroundColor="#E6E6FA"//#395ACC
            }
            else{
                obj.style.backgroundColor="white"
            }
        }
        
        function HabilitarAcciones(chk,control)
        {
            if (chk.checked==true){
                control.disabled=false
                control.className=""
                control.focus()
            }
            else {
                control.disabled=true
                control.className="disabled"
            }
        }
        
        function MostrarBox(obj)
        {
            //obj.visibility="visible"
        }
    </script>
    <style type="text/css">
    .disabled {
            background-color: #DADADA; /*color: gris*/
            font-size:8pt;
        }
    .CeldaImagen {height: 100%; background-image: url('../../images/barracabecera.jpg'); width: 25%;}
    </style>
</head>
<body style="border-top=0">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <table cellpadding="2" cellspacing="0" style="width:100%">
        <tr>
            <td style="height: 25px;" colspan="2" class="usatTitulo">
    <p>Aprobación del presupuesto: Administración General</p>
            </td>
        </tr>
        <tr>
            <td style="width: 10%">
                Proceso</td>
            <td style="width: 90%">
                <asp:DropDownList ID="dpProceso" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 10%">
                Estado</td>
            <td style="width: 90%">
                <asp:DropDownList ID="dpEstado" runat="server">
                </asp:DropDownList>
                <asp:Button ID="cmdConsultar" runat="server" Text="   Consultar" 
                    CssClass="buscar2" />
            </td>
        </tr>
        <tr>
            <td style="width: 10%">
                Acciones:</td>
            <td style="width: 90%">
                <asp:Button ID="cmdIniciar" runat="server" Text="Iniciar Revisión" 
                    CssClass="nuevo" Height="20px" />
                &nbsp;<asp:Button ID="cmdRevisar" runat="server" Text="Revisar Presupuesto" 
                    Enabled="False" CssClass="modificar2" Height="20px" />
                <asp:Button ID="cmdDetalle" runat="server" Text="Detalle" OnClientClick="MostrarBox(document.getElementById('PanelDetalle'))" style="display:none" />
            </td>
        </tr>
    </table>
    
    <asp:GridView ID="grwPresupuestos" runat="server" AutoGenerateColumns="False" 
                    CellPadding="3" DataKeyNames="codigo_Pto,codigo_cco" 
                    Width="100%" EnableViewState="False" 
        DataSourceID="objPresupuesto" AllowSorting="True">
                    <Columns>
                        <asp:BoundField HeaderText="#" />
                        <asp:TemplateField HeaderText="Elegir">
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkHeader" runat="server" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkElegir" runat="server" CssClass="CssCheckBox" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Centro de Costo" SortExpression="CentroCosto">
                            <ItemTemplate>
                            <asp:LinkButton ID="lnkCentroCostos" runat="server" Font-Underline="True" 
                                    ForeColor="Blue" onclick="lnkCentroCostos_Click" 
                                    Text='<%# eval("CentroCosto") %>'></asp:LinkButton>
                            </ItemTemplate>
                            <ControlStyle Font-Underline="True" />
                            <HeaderStyle Font-Underline="True" />
                            <ItemStyle Font-Size="7pt" />
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Director" DataField="Director" 
                            SortExpression="Director" >
                            <ItemStyle Font-Size="7pt" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Estado" DataField="Estado" SortExpression="Estado" >
                            <ItemStyle Font-Size="7pt" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Total Ing." DataField="TotalIngresos" 
                            SortExpression="TotalIngresos" DataFormatString="{0:F2}" >
                            <HeaderStyle BackColor="#339966" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Techo Ing." DataField="TechoIngresos" 
                            SortExpression="TechoIngresos" DataFormatString="{0:F2}">
                            <HeaderStyle BackColor="#339966" />
                            <ItemStyle Font-Size="7pt" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Diferencia Ing." >
                            <HeaderStyle BackColor="#339966" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Total Egr." DataField="TotalEgresos" 
                            SortExpression="TotalEgresos" DataFormatString="{0:F2}" >
                            <HeaderStyle BackColor="#FF6600" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Techo Egr." DataField="TechoEgresos" 
                            SortExpression="TechoEgresos" DataFormatString="{0:F2}" >
                            <HeaderStyle BackColor="#FF6600" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Diferencia Egr." >
                            <HeaderStyle BackColor="#FF6600" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Comentarios">
                            <ItemTemplate>
                                <asp:Label ID="lblcomentarios" runat="server" 
                                    Text='<%# eval("CantObservaciones") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <p class="rojo"><b>No se han encontrado Presupuestos registrados</b></p>
                    </EmptyDataTemplate>
                    <HeaderStyle ForeColor="White" CssClass="TituloTabla" BackColor="#3366CC" 
                        Height="22px" />
                </asp:GridView>

    <asp:ObjectDataSource ID="objPresupuesto" runat="server" 
        SelectMethod="ConsultarEstadoProceso" TypeName="clsAprobarPresupuesto">
        <SelectParameters>
            <asp:ControlParameter ControlID="dpProceso" DefaultValue="" Name="codigo_pct" 
                PropertyName="SelectedValue" Type="Int16" />
            <asp:ControlParameter ControlID="dpEstado" DefaultValue="-1" Name="codigo_epr" 
                PropertyName="SelectedValue" Type="Int16" />
        </SelectParameters>
    </asp:ObjectDataSource>

<asp:Panel ID="PanelDatos" runat="server" Height="350px" ScrollBars="Auto" 
        Width="80%" CssClass="contornotabla"  >
            <table cellpadding="3" cellspacing="0" style="width:100%; border-collapse: collapse">
            <tr style="background-color: #3366FF; color: #FFFFFF; font-size: 14px; font-weight: bold;">
                <td style="width: 3%; ">
                    <asp:Button ID="cmdCancelar" runat="server" Text="X" 
                        ValidationGroup="Ninguna" />
                </td>
                <td style="width: 97%;">
                    Acciones a realizar</td>
            </tr>
            <tr>
                <td style="width: 3%" valign="top">
                    &nbsp;</td>
                <td valign="top" style="width: 97%">
                    <b>Marque las acciones que desea realizar:</b></td>
            </tr>
            <tr>
                <td style="width: 3%" valign="top">
                    &nbsp;</td>
                <td style="background-color: #E8EEF7; width: 97%;" valign="top">
                    <asp:CheckBox ID="chkHabilitarEstado" runat="server" Text="Estado del Presupuesto" />
                    &nbsp;<asp:DropDownList ID="dpEstadoPresupuesto" runat="server" 
                        CssClass="disabled" Enabled="False">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 3%" valign="top">
                    &nbsp;</td>
                <td valign="top" style="width: 97%">
                    <asp:CheckBox ID="chkHabilitarIngreso" runat="server" Text="Techo de Ingresos" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="txtTechoIngresos" runat="server" CssClass="disabled" 
                        Enabled="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 3%" valign="top">
                    &nbsp;</td>
                <td valign="top" style="width: 97%">
                    <asp:CheckBox ID="chkHabilitarEgreso" runat="server" Text="Techo de Egresos" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="txtTechoEgresos" runat="server" CssClass="disabled" 
                        Enabled="False"></asp:TextBox>
                </td>
            </tr>
                <tr>
                    <td style="width: 3%" valign="top">
                        &nbsp;</td>
                    <td style="width: 97%" valign="top">
                        <asp:CheckBox ID="chkHabilitarComentario" runat="server" Text="Comentarios" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 3%" valign="top">
                        &nbsp;</td>
                    <td style="width: 97%; " valign="top">
                        <asp:TextBox ID="txtComentarios" runat="server" CssClass="disabled" Rows="4" 
                            TextMode="MultiLine" Width="98%" Enabled="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 3%" valign="top">
                        &nbsp;</td>
                    <td style="width: 97%" valign="top" align="right">
                        <asp:Button ID="cmdGuardar" runat="server" Text="Guardar" /></td>
                </tr>
        </table>
    </asp:Panel>
<cc1:ModalPopupExtender ID="mpeFicha" runat="server"
        CancelControlID="cmdCancelar"
        PopupControlID="PanelDatos"
        TargetControlID="cmdRevisar"  BackgroundCssClass="FondoAplicacion" Y="50" />    
        
        <busyboxdotnet:BusyBox ID="BusyBox1" runat="server" BackColor="White" 
                                    Overlay="False" Text="Se está procesando su información" 
                                    Title="Por favor espere" />
    <asp:Panel ID="PanelDetalle" runat="server" Height="500px" ScrollBars="Auto" 
            Width="90%" CssClass="contornotabla"  >
    <table cellpadding="3" cellspacing="0" style="width:100%; border-collapse: collapse">
            <tr style="background-color: #3366FF; color: #FFFFFF; font-size: 14px; font-weight: bold;">
                <td style="width: 3%; ">
                    <asp:Button ID="cmdCerrar" runat="server" Text="X" 
                        ValidationGroup="Ninguna" />
                </td>
                <td style="width: 97%;">
                    Detalle de Presupuesto</td>
            </tr>
            <tr>
                <td style="width: 3%" valign="top">
                    &nbsp;</td>
                <td valign="top" style="width: 97%">
                    Centro de Costo:
                    <asp:Label ID="lblcentrocosto" runat="server" Font-Bold="True" Font-Size="10pt"></asp:Label>
                    <br />
                    Director:
                    <asp:Label ID="lbldirector" runat="server" Font-Bold="True" Font-Size="10pt"></asp:Label>
                    <br />
                    Estado:
                    <asp:Label ID="lblestado" runat="server" Font-Bold="True" Font-Size="10pt"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 3%" valign="top">
                    &nbsp;</td>
                <td style="width: 97%; background-color: #FFFF99; font-weight: bold;" valign="top">
                    » OBSERVACIONES:
                    </td>
            </tr>           
            <tr>
                <td style="width: 3%" valign="top">
                    &nbsp;</td>
                <td style="width: 97%" valign="top">
                <asp:GridView ID="grwobservaciones" runat="server" AutoGenerateColumns="False" 
                        Width="100%" CellPadding="3">
                        <Columns>
                            <asp:BoundField DataField="fecha_Rpr" 
                                HeaderText="Fecha y hora" />
                            <asp:BoundField DataField="Revisor" HeaderText="Revisor" />
                            <asp:BoundField DataField="observacion_Rpr" HeaderText="Observación" />
                        </Columns>
                        <EmptyDataTemplate>
                           <p class="rojo"><b>No se han encontrado comentarios registrados</b></p>
                        </EmptyDataTemplate>
                        <HeaderStyle BackColor="#e8eef7" ForeColor="#3366CC" BorderColor="#99BAE2" 
                BorderStyle="Solid" BorderWidth="1px" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td style="width: 3%" valign="top">
                    &nbsp;</td>
                <td style="width: 97%; background-color: #FFFF99; font-weight: bold;" valign="top">
                    » INGRESOS. Techo: <asp:Label ID="lblTechoIngresos" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 3%" valign="top">
                    &nbsp;</td>
                <td style="width: 97%" valign="top">
                    <asp:GridView ID="grwDetallePresupuestoIngresos" runat="server" 
                AutoGenerateColumns="False" CellPadding="3" ShowFooter="True">
                <Columns>
                    <asp:BoundField HeaderText="Nro." />
                    <asp:BoundField DataField="Clase" HeaderText="Clase" />
                    <asp:BoundField DataField="Cod.Item" HeaderText="Código" />
                    <asp:TemplateField HeaderText="Descripción Item">
                        <ItemTemplate>
                            <asp:Label ID="lblDesEstandar" runat="server" Text='<%# eval("item") %>'></asp:Label>
                            <br />
                            <asp:Label ID="lblDetDescripcion" runat="server" Font-Italic="True" 
                                Text='<%# eval("Detalle Item") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Cuenta Contable">
                        <ItemTemplate>
                            <asp:Label ID="lblNombre_cta" runat="server" Text='<%# eval("Cuenta Contable") %>'></asp:Label>
                            <br />
                            Nro. Cuenta:
                            <asp:Label ID="lblNumero_cta" runat="server" Font-Italic="True" Text='<%# eval("Nro Cta Contable") %>'></asp:Label>
                            <br />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Precio Unit." HeaderText="Precio Unit." >
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Cantidad" HeaderText="Cant." >
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="subTotal" HeaderText="SubTotal" >
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                </Columns>
                <EmptyDataTemplate>
                            <p class="rojo"><b>No se han encontrado el Detalle INGRESOS del Presupuesto</b></p>
                  </EmptyDataTemplate>
                  <FooterStyle HorizontalAlign="Center" BackColor="#e8eef7" Font-Bold="True" ForeColor="#3366CC" />
                 <HeaderStyle BackColor="#e8eef7" ForeColor="#3366CC" BorderColor="#99BAE2" 
                BorderStyle="Solid" BorderWidth="1px" />
            </asp:GridView>
                </td>
            </tr>
            <tr>
                <td style="width: 3%" valign="top">
                    &nbsp;</td>
                <td style="width: 97%; background-color: #FFFF99; font-weight: bold;" valign="top">
                    » EGRESOS. Techo: <asp:Label ID="lblTechoEgresos" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 3%" valign="top">
                    &nbsp;</td>
                <td style="width: 97%" valign="top">
                    <asp:GridView ID="grwDetallePresupuestoEgresos" runat="server" 
                AutoGenerateColumns="False" CellPadding="3" ShowFooter="True">
                <Columns>
                    <asp:BoundField HeaderText="Nro." />
                    <asp:BoundField DataField="Clase" HeaderText="Clase" />
                    <asp:BoundField DataField="Cod.Item" HeaderText="Código" />
                    <asp:TemplateField HeaderText="Descripción Item">
                        <ItemTemplate>
                            <asp:Label ID="lblDesEstandar" runat="server" Text='<%# eval("Item") %>'></asp:Label>
                            <br />
                            <asp:Label ID="lblDetDescripcion" runat="server" Font-Italic="True" 
                                Text='<%# eval("Detalle Item") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Cuenta Contable">
                        <ItemTemplate>
                            <asp:Label ID="lblNombre_cta" runat="server" Text='<%# eval("Cuenta Contable") %>'></asp:Label>
                            <br />
                            Nro. Cuenta:
                            <asp:Label ID="lblNumero_cta" runat="server" Font-Italic="True" Text='<%# eval("Nro Cta Contable") %>'></asp:Label>
                            <br />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Precio Unit." HeaderText="Precio Unit." >
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Cantidad" HeaderText="Cant." >
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="subTotal" HeaderText="SubTotal" >
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                </Columns>
                <EmptyDataTemplate>
                            <p class="rojo"><b>No se han encontrado el Detalle EGRESOS del Presupuesto</b></p>
                  </EmptyDataTemplate>
                  <FooterStyle HorizontalAlign="Center" BackColor="#e8eef7" Font-Bold="True" ForeColor="#3366CC" />
                 <HeaderStyle BackColor="#e8eef7" ForeColor="#3366CC" BorderColor="#99BAE2" 
                BorderStyle="Solid" BorderWidth="1px" />
            </asp:GridView>
                </td>
            </tr>
            
           </table>
       </asp:Panel>
<cc2:ModalPopupExtender ID="mpeDetalle" runat="server"
        CancelControlID="cmdCerrar"
        PopupControlID="PanelDetalle"
        TargetControlID="cmdDetalle"  BackgroundCssClass="FondoAplicacion" Y="50" />
    </form>
</body>
</html>
