<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmgeneracioncargos.aspx.vb" Inherits="administrativo_pec_frmgeneracioncargos" Theme="Acero" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Generación de Cargos   sss</title>
    <link href="../../../private/Tabs.css" rel="stylesheet" type="text/css" />
    <link href="../../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="JavaScript" src="../../../private/funciones.js"></script>
    <script src="../../../private/jq/jquery.js" type="text/javascript"></script>
    <script src="../../../private/jq/jquery.mascara.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
      $(document).ready(function() {
        //Marcara de Fechas
        jQuery(function($) {
          $("#TabFormaPago_TabConConvenio_TxtFecVctoInicial").mask("99/99/9999");
          $("#TabFormaPago_TabSInConvenio_TxtFechaVencimiento").mask("99/99/9999");
        });

        $("#TxtRecargo").keyup(function() { sumatotalpagar() })
        $("#TxtDescuento").keyup(function() { sumatotalpagar() })
        $("#TabFormaPago_TabConConvenio_TxtCuotaInicial").keyup(function() { totalfinanciar() })

      })
	
	  function sumarcuotas() {
	    NumCuotas = $("#HddNumCuotas").val();
	    cuota = parseFloat(0)
	    for (i = 1; i <= NumCuotas; i++) {
	      if (isNaN(parseFloat($("#TabFormaPago_TabConConvenio_Monto_" + i + "").val())) == false) {
	        cuota = cuota + parseFloat($("#TabFormaPago_TabConConvenio_Monto_" + i + "").val())
	      }
	      else { 
	        cuota = cuota + 0
	        }
	      }
	      $("#TabFormaPago_TabConConvenio_TxtTotal").val(cuota);
	    }

	    function sumatotalpagar() {
	      precio = 0
	      recargo = 0
	      descuento = 0
      
	      if ( isNaN ( parseFloat( $("#TxtPrecio").val())) == false   )
	        precio = $("#TxtPrecio").val();
	      else
	       precio = 0

	      if (isNaN (parseFloat($("#TxtRecargo").val())) == false )
	        recargo = $("#TxtRecargo").val();
	      else
	       recargo = 0
	        
	      if (isNaN(parseFloat($("#TxtDescuento").val())) == false )
	        descuento = $("#TxtDescuento").val();
	      else
	        descuento = 0

	      $("#TxtTotalPagar").val(parseFloat(precio) + parseFloat(recargo) - parseFloat(descuento))
	      $("#TabFormaPago_TabSInConvenio_TxtTotalPagarMuestra").val(parseFloat(precio) + parseFloat(recargo) - parseFloat(descuento))
	      
	      totalfinanciar();
	    }

	    function totalfinanciar() {
	      totalpagar = 0
	      inicial = 0
	      if (isNaN(parseFloat($("#TabFormaPago_TabConConvenio_TxtCuotaInicial").val())) == false)
	        inicial = $("#TabFormaPago_TabConConvenio_TxtCuotaInicial").val()
	      else
	        inicial = 0

	      totalpagar = parseFloat($("#TxtTotalPagar").val())
	      
	      $("#TabFormaPago_TabConConvenio_TxtSaldoFinanciar").val(totalpagar - inicial)
	    }
	    
  
	</script>

	
</head>
<body>
    <form id="form1" runat="server">
   <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <table style="width:100%;">
      <tr>
        <td>
          Centro de Costos</td>
        <td class="usatTitulousat">
          <asp:Label ID="LblCodigoCCO" runat="server"></asp:Label>
&nbsp;-
          <asp:Label ID="LblNombreCCO" runat="server"></asp:Label>
        </td>
      </tr>
      <tr>
        <td>
          Cargar a
          Participante</td>
        <td>
          <asp:DropDownList ID="DDLPersona" runat="server" Height="16px" 
            SkinID="ComboObligatorio">
          </asp:DropDownList>
          <asp:Button ID="CmdBuscarAlumno" runat="server" Text=" . . . " 
            SkinID="BotonBuscar" Visible="False" />
        </td>
      </tr>
      <tr>
        <td>
          Item</td>
        <td>
          <asp:DropDownList ID="DDLItem" runat="server" AutoPostBack="True" 
            SkinID="ComboObligatorio">
          </asp:DropDownList>
        </td>
      </tr>
      <tr>
        <td>
          &nbsp;</td>
        <td>
          <table style="width:100%;">
            <tr>
              <td>
                Precio :
                <asp:TextBox ID="TxtPrecio"  runat="server" 
                  style="direction:rtl; margin-right: 0px;" Width="82px" 
                  SkinID="CajaTextoSinMarco"></asp:TextBox>
&nbsp;Recargo
                <asp:TextBox ID="TxtRecargo"  runat="server"  Width="70px" 
                  SkinID="CajaTextoObligatorio"></asp:TextBox>
&nbsp;Descuento
                <asp:TextBox ID="TxtDescuento"  runat="server"  
                  Width="70px" SkinID="CajaTextoObligatorio"></asp:TextBox>
              &nbsp;<B>Total a Pagar</B>
                <asp:TextBox ID="TxtTotalPagar" runat="server" style="direction:rtl" 
                  Width="62px" SkinID="CajaTextoSinMarco" Font-Bold="True"></asp:TextBox>
                <asp:RangeValidator ID="RangeValidator2" runat="server" 
                  ControlToValidate="TxtTotalPagar" 
                  ErrorMessage="Monto a Pagar no puede ser negativo" MaximumValue="1000000" 
                  MinimumValue="0" Type="Double" ValidationGroup="Guardar">*</asp:RangeValidator>
              </td>
            </tr>
            </table>
        </td>
      </tr>
      <tr>
        <td>
          Observación</td>
        <td>
          <asp:TextBox ID="TxtObservacion" runat="server" Height="38px" 
            TextMode="MultiLine" Width="545px" MaxLength="100" 
            SkinID="CajaTextoObligatorio"></asp:TextBox>
        </td>
      </tr>
      <tr>
        <td>
          Forma de Pago</td>
        <td style="text-align: right">
          <asp:Button runat="server" Text="Cerrar" ValidationGroup="Salir" 
            SkinID="BotonCancelar" ID="cmdCancelar1">
    </asp:Button>
        </td>
      </tr>
      <tr>
        <td colspan="2">
          <ajaxToolkit:TabContainer  ID="TabFormaPago" runat="server" ActiveTabIndex="0" 
            Height="165px" Width="100%" Enabled="False" CssClass="ajax__tab_xp2">
            <ajaxToolkit:TabPanel  runat="server"  HeaderText="SIN Convenio" ID="TabSInConvenio"><HeaderTemplate>
SIN Convenio 
</HeaderTemplate>
<ContentTemplate>
<asp:Panel ID="PanAgrupagorSinConvenio" runat="server">
<table style="width:100%;">
<tr>
<td width="100">
    Total Pagar</td>
    <td>
      <asp:TextBox ID="TxtTotalPagarMuestra" runat="server" Font-Bold="True" 
    SkinID="CajaTextoSinMarco" style="direction:rtl" Width="62px"></asp:TextBox>
  </td>
    </tr>
  <tr>
    <td width="100">
        Fecha de Vencimiento</td>
    <td>
        <asp:TextBox ID="TxtFechaVencimiento" runat="server" ValidationGroup="Guardar" 
            Width="96px"></asp:TextBox>
        <asp:RangeValidator ID="RangeValFechaVencimiento" runat="server" 
            ControlToValidate="TxtFechaVencimiento" 
            ErrorMessage="Fecha de Vencimiento Incorrecta" MaximumValue="31/12/2040" 
            MinimumValue="01/01/2009" SetFocusOnError="True" Type="Date" 
            ValidationGroup="GuardarSinCon">*</asp:RangeValidator>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
            ControlToValidate="TxtFechaVencimiento" 
            ErrorMessage="Fecha de Vencimiento requerido" SetFocusOnError="True" 
            ValidationGroup="GuardarSinCon">*</asp:RequiredFieldValidator>

      </td>
  </tr>
    <tr>
      <td>
        Nro de Partes</td>
      <td>
        <asp:DropDownList ID="DDLNumPartes" runat="server" 
        SkinID="ComboObligatorio">
        </asp:DropDownList>
        <asp:RangeValidator ID="RangeValidator3" runat="server" 
        ControlToValidate="DDLNumPartes" ErrorMessage="Seleccione Nº de partes" 
        MaximumValue="1000" MinimumValue="0" Type="Integer" 
        ValidationGroup="GuardarSinCon">*</asp:RangeValidator>
      </td>
  </tr>
  <tr>
    <td width="100">
      Empezar Agrupar con Pensión de</td>
    <td>
      <asp:DropDownList ID="DDLAgruparPension" runat="server" 
        SkinID="ComboObligatorio">
        <asp:ListItem Text="-- Seleccione Agrupación --" Value="-1"></asp:ListItem>
        <asp:ListItem Text="Abril" Value="01-04"></asp:ListItem>
        <asp:ListItem Text="Mayo" Value="01-05"></asp:ListItem>
        <asp:ListItem Text="Junio" Value="01-06"></asp:ListItem>
        <asp:ListItem Text="Julio" Value="01-07"></asp:ListItem>
        <asp:ListItem Text="Setiembre" Value="01-09"></asp:ListItem>
        <asp:ListItem Text="Octubre" Value="01-10"></asp:ListItem>
        <asp:ListItem Text="Noviembre" Value="01-11"></asp:ListItem>
        <asp:ListItem Text="Diciembre" Value="01-12"></asp:ListItem>
      </asp:DropDownList>
      <asp:CompareValidator ID="ValidaAgrupa" runat="server" 
        ControlToValidate="DDLAgruparPension" ErrorMessage="Seleccione Agrupacion" 
        Operator="NotEqual" ValidationGroup="GuardarSinCon" ValueToCompare="-1">*</asp:CompareValidator>
    </td>
  </tr>
  <tr>
    <td width="100">
      &#160;</td>
    <td>
      &#160;</td>
  </tr>
  <tr>
    <td width="100">
      &#160;</td>
    <td>
      &#160;</td>
  </tr>
  <tr>
    <td colspan="2">
      <asp:Button ID="CmdGuardarSinCon" runat="server" SkinID="BotonGuardar" 
        Text="Guardar" ValidationGroup="GuardarSinCon" >
      </asp:Button>
      &#160;<asp:Button ID="cmdCancelar" runat="server" SkinID="BotonCancelar" 
        Text="Cerrar" ValidationGroup="Salir" >
      </asp:Button>
    </td>
  </tr>
</table>
  
</asp:Panel>
























</ContentTemplate>
</ajaxToolkit:TabPanel>
            <ajaxToolkit:TabPanel ID="TabConConvenio" runat="server" HeaderText="CON Convenio"><HeaderTemplate>
CON Convenio 
</HeaderTemplate>
<ContentTemplate>
<asp:Panel ID="PanAgrupagorConvenio" runat="server">
<table style="width:100%;" align="center">
<tr>
<td valign="top">
    <table style="width:100%;">
      <tr>
        <td dir="ltr" width="100">
          Cuota Inicial</td>
        <td>
          <asp:TextBox ID="TxtCuotaInicial" runat="server" SkinID="CajaTextoObligatorio" 
            Width="75px"></asp:TextBox>
          <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
            ControlToValidate="TxtCuotaInicial" ErrorMessage="Necesario cuota inicial" 
            ValidationGroup="Generar">*</asp:RequiredFieldValidator>
          &#160;Fecha Vcto Cuota Inicial&#160;<asp:TextBox ID="TxtFecVctoInicial" runat="server" 
            SkinID="CajaTextoObligatorio" Width="81px"></asp:TextBox>
          <asp:RangeValidator ID="RangeFechaVcto" runat="server" 
            ControlToValidate="TxtFecVctoInicial" 
            ErrorMessage="Fecha Vencimiento de Cuota Inicial menor a actual o incorrecta" 
            MaximumValue="31/12/2050" MinimumValue="01/01/1900" Type="Date" 
            ValidationGroup="Generar">*</asp:RangeValidator>
          <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
            ControlToValidate="TxtFecVctoInicial" 
            ErrorMessage="Fecha de Vencimiento de Inicial Requerida" SetFocusOnError="True" 
            ValidationGroup="Generar">*</asp:RequiredFieldValidator>
        </td>
      </tr>
      <tr>
        <td>
          <b>Saldo a Financiar</b>
        </td>
        <td>
          <asp:TextBox ID="TxtSaldoFinanciar" runat="server" Font-Bold="True" 
            SkinID="CajaTextoSinMarco"  Width="75px"></asp:TextBox>
          <asp:CompareValidator ID="CompareValidator1" runat="server" 
            ControlToValidate="TxtSaldoFinanciar" 
            ErrorMessage="Saldo financiar mayor que cero" Operator="GreaterThan" 
            Type="Double" ValidationGroup="Generar" ValueToCompare="0">*</asp:CompareValidator>
        </td>
      </tr>
      <tr>
        <td>
          Nº de Cuotas</td>
        <td>
          <asp:DropDownList ID="DDLNumCuotas" runat="server" SkinID="ComboObligatorio">
          </asp:DropDownList>
          <asp:RangeValidator ID="RangeValidator1" runat="server" 
            ControlToValidate="DDLNumCuotas" ErrorMessage="Seleccione Nro de Cuotas" 
            MaximumValue="80" MinimumValue="1" Type="Integer" ValidationGroup="Generar">*</asp:RangeValidator>
          <asp:RangeValidator ID="RangeValidator4" runat="server" 
            ControlToValidate="DDLNumCuotas" ErrorMessage="Seleccione Nro de Cuotas" 
            MaximumValue="80" MinimumValue="1" Type="Integer" ValidationGroup="Guardar">*</asp:RangeValidator>
        </td>
      </tr>
      <tr>
        <td>
          Monto de Cuota</td>
        <td>
          <asp:RadioButton ID="RbVariables" runat="server" GroupName="Cuotas" 
            Text="Variable" >
          </asp:RadioButton>
          <asp:RadioButton ID="RbFijas" runat="server" Checked="True" GroupName="Cuotas" 
            Text="Fija" >
          </asp:RadioButton>
        </td>
      </tr>
      <tr>
        <td>
          Tipo de Período</td>
        <td>
          <asp:RadioButton ID="RbPerioVaria" runat="server" GroupName="Periodo" 
            Text="Variable" >
          </asp:RadioButton>
          <asp:RadioButton ID="RbPerioFijo" runat="server" Checked="True" 
            GroupName="Periodo" Text="Fija" >
          </asp:RadioButton>
          &#160;Pagando el día
          <asp:DropDownList ID="DDLFormaPago" runat="server" SkinID="ComboObligatorio">
            <asp:ListItem>01</asp:ListItem>
            <asp:ListItem>02</asp:ListItem>
            <asp:ListItem>03</asp:ListItem>
            <asp:ListItem>04</asp:ListItem>
            <asp:ListItem>05</asp:ListItem>
            <asp:ListItem>06</asp:ListItem>
            <asp:ListItem>07</asp:ListItem>
            <asp:ListItem>08</asp:ListItem>
            <asp:ListItem>09</asp:ListItem>
            <asp:ListItem>10</asp:ListItem>
            <asp:ListItem>11</asp:ListItem>
            <asp:ListItem>12</asp:ListItem>
            <asp:ListItem>13</asp:ListItem>
            <asp:ListItem>14</asp:ListItem>
            <asp:ListItem>15</asp:ListItem>
            <asp:ListItem>16</asp:ListItem>
            <asp:ListItem>17</asp:ListItem>
            <asp:ListItem>18</asp:ListItem>
            <asp:ListItem>19</asp:ListItem>
            <asp:ListItem>20</asp:ListItem>
            <asp:ListItem>21</asp:ListItem>
            <asp:ListItem>22</asp:ListItem>
            <asp:ListItem>23</asp:ListItem>
            <asp:ListItem>24</asp:ListItem>
            <asp:ListItem>25</asp:ListItem>
            <asp:ListItem>26</asp:ListItem>
            <asp:ListItem>27</asp:ListItem>
            <asp:ListItem>28</asp:ListItem>
            <asp:ListItem>29</asp:ListItem>
            <asp:ListItem>30</asp:ListItem>
          </asp:DropDownList>
          &#160;de cada mes&#160;empezando 1era cuota en :
          <asp:DropDownList ID="DDLInicioPrimCuota" runat="server" 
            SkinID="ComboObligatorio">
          </asp:DropDownList>
        </td>
      </tr>
      <tr>
        <td>
          &#160;</td>
        <td>
          <asp:Button ID="CmdCalcular" runat="server" Height="23px" SkinID="BotonLibre" 
            Text="Generar Cuotas" ValidationGroup="Generar" Width="126px" >
          </asp:Button>
          &#160;<asp:Label ID="lblMensaje" runat="server"></asp:Label>
        </td>
      </tr>
      <tr>
        <td valign="top">
          <asp:Button ID="CmdGuardar" runat="server" Enabled="False" 
            SkinID="BotonGuardar" Text="Guardar" ValidationGroup="Guardar" >
          </asp:Button>
          <asp:Button ID="cmdCancelar0" runat="server" SkinID="BotonCancelar" 
            Text="Cerrar" ValidationGroup="Salir" >
          </asp:Button>
        </td>
        <td>
          &#160;</td>
      </tr>
    </table>
  </td>
    <td valign="top">
      <asp:Panel ID="PanCuotas" runat="server">
        <asp:Table ID="TblCuotas" runat="server">
        </asp:Table>
      </asp:Panel>


  </td>
    </tr>
</table>
  
</asp:Panel>


























</ContentTemplate>
</ajaxToolkit:TabPanel>
          </ajaxToolkit:TabContainer>
        </td>
      </tr>
      <tr>
      <td colspan="2">Deudas Generadas en el Item seleccionado:</td>
      </tr>
      <tr>
      <td colspan="2">
        <asp:Panel ID="Panel3" runat="server" Height="140px" ScrollBars="Vertical" 
          EnableViewState="False">
          <asp:GridView ID="DgvDeudas" runat="server" AutoGenerateColumns="False" 
            SkinID="skinGridViewLineasIntercalado">
            <Columns>
              <asp:BoundField DataField="FechaCargo" HeaderText="Fec. Cargo" />
              <asp:BoundField DataField="Servicio" HeaderText="Servicio" />
              <asp:BoundField DataField="FechaVencimiento" DataFormatString="{0:dd/MM/yyyy}" 
                HeaderText="Fec. Vence" />
              <asp:BoundField DataField="Cargo" HeaderText="Cargo" >
              <ItemStyle HorizontalAlign="Right" />
              </asp:BoundField>
              <asp:BoundField DataField="Abonos" HeaderText="Abonos" >
              <ItemStyle HorizontalAlign="Right" />
              </asp:BoundField>
              <asp:BoundField DataField="Saldo" HeaderText="Saldo" >
              <ItemStyle HorizontalAlign="Right" />
              </asp:BoundField>
              <asp:BoundField DataField="Observacion" HeaderText="Observacion" />
              <asp:BoundField DataField="codigo_Deu" HeaderText="Codigo Deu" />
            </Columns>
            
          </asp:GridView>
        </asp:Panel>
        </td>
      </tr>
      </table>
    
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
      ShowMessageBox="True" ShowSummary="False" ValidationGroup="Generar" />
    
    <asp:ValidationSummary ID="ValidationSummary3" runat="server" 
      ShowMessageBox="True" ShowSummary="False" ValidationGroup="GuardarSinCon" />
    
    <asp:ValidationSummary ID="ValidationSummary2" runat="server" 
      ShowMessageBox="True" ShowSummary="False" ValidationGroup="Guardar" />
    
    <asp:HiddenField ID="HddNumCuotas" runat="server" />
    
    </form>
</body>
</html>
