<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmpersonajuridica.aspx.vb" Inherits="administrativo_frmpersonajuridica" Theme="Acero" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registrar Persona Jurídica</title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="JavaScript" src="../../private/funciones.js"></script>
    <script type="text/javascript" language="JavaScript" src="../../private/jq/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" language="javascript" src="../../private/jq/jquery.mascara.js"></script> 
	<script type="text/javascript" language="javascript">
	  function ValidaMinimoNombre(source, arguments) {
	    if ($("#txtNombres").val().length < 3)
	     {
	      arguments.IsValid = false;
	      return;
	      }
	    else
	      arguments.IsValid = true;
	  }
	
	    function enter2tab(e) {
	        if (e.keyCode == 13) {
	            cb = parseInt($(this).attr('tabindex'));

	            if ($(':input[tabindex=\'' + (cb + 1) + '\']') != null) {
	                $(':input[tabindex=\'' + (cb + 1) + '\']').focus();
	                $(':input[tabindex=\'' + (cb + 1) + '\']').select();
	                e.preventDefault();

	                return false;
	            }
	        }
	    }

	    $(document).ready(function() {
	       $("#txtruc").blur(function() {
	        if ($("#txtruc").val().length > 0) { //Validar solamente si hay datos ingresados en el DNI
                    $("#errornrodoc").empty();
                    if ($("#txtruc").val().length < 11 || $("#txtruc").val().length > 11) {    
	                    $("#errornrodoc").text("El número de RUC es incorrecto. Debe contener 11 caracteres.");
	                }
	            }
	        });

	        /* Aquí podría filtrar que controles necesitará manejar,en el caso de incluir un dropbox $('input, select');*/
	        tb = $('input');
	        if ($.browser.mozilla)
	            { $(tb).keypress(enter2tab); }
	        else { $(tb).keydown(enter2tab); }
	    })
	</script>
</head>
<body>
   <form id="form1" runat="server">
    <span class="usatTitulo">Registro de Persona Jurídica</span><asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        
                <table style="width: 100%" class="contornotabla">
                    <tr>
                        <td style="width:20%">
                            <b>Nro. RUC<asp:RequiredFieldValidator ID="RequiredFieldValidator11" 
                                runat="server" ControlToValidate="txtruc" 
                                ErrorMessage="Debe ingresar el número  de RUC">*</asp:RequiredFieldValidator>
                            </b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtruc" runat="server" CssClass="cajas" MaxLength="11" 
                                SkinID="CajaTextoObligatorio"></asp:TextBox>
                            &nbsp;<asp:LinkButton ID="lnkComprobarDNI" runat="server" Font-Bold="True" 
                                Font-Underline="True" ForeColor="Blue" ValidationGroup="Comprobar">[Buscar]</asp:LinkButton>
                        &nbsp;&nbsp;<span id="errornrodoc" style="color:red"></span>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:20%">
                            Razón Social <b>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                    ControlToValidate="txtNombres" 
                    ErrorMessage="Debe ingresar Razón Social">*</asp:RequiredFieldValidator>
                            </b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNombres" runat="server" MaxLength="80" 
                    Width="338px" SkinID="CajaTextoObligatorio"></asp:TextBox>
                            &nbsp;<asp:CustomValidator ID="CustomValidator1" runat="server" 
                              ErrorMessage="Minimo 3 caracteres para buscar." 
                              ClientValidationFunction="ValidaMinimoNombre" ValidationGroup="BuscarNombre" 
                              ValidateEmptyText="True">*</asp:CustomValidator>
                            <asp:LinkButton ID="lnkComprobarNombres" runat="server" Font-Bold="True" 
                                Font-Underline="True" ForeColor="Blue" ValidationGroup="BuscarNombre" 
                                Visible="False">Clic aquí para buscar coincidencias</asp:LinkButton>
                        </td>
                    </tr>
                    <tr runat="server" id="trConcidencias">
                        <td style="width:20%">
                            &nbsp;</td>
                        <td>
                            <div id="listadiv" style="width:100%;heigh:200px">
                            
                            <asp:GridView ID="grwCoincidencias" runat="server" AutoGenerateColumns="False" 
                                DataKeyNames="codigo_Pot,codigo_pso" Width="100%" 
                                SkinID="skinGridViewLineas">
                                <Columns>
                                    <asp:BoundField DataField="apellidoPatRazSoc_Pot" HeaderText="Razon Social" >
                                        <ItemStyle Width="50%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="nroDocIde_Pot" HeaderText="RUC" >
                                        <ItemStyle Width="10%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="direccion_Pot" HeaderText="Dirección" />
                                    <asp:CommandField ShowSelectButton="True">
                                        <ControlStyle Font-Underline="True" ForeColor="Blue" />
                                        <ItemStyle Width="5%" />
                                    </asp:CommandField>  
                                </Columns>
                            </asp:GridView>
                            
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:20%">
                            Dirección <b>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
                    ControlToValidate="txtdireccion" 
                    ErrorMessage="Debe ingresar la dirección de la persona">*</asp:RequiredFieldValidator>
                            </b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtdireccion" runat="server" Width="80%" 
                                MaxLength="150" SkinID="CajaTextoObligatorio"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:20%">
                            Teléfono</td>
                        <td>
                            <asp:TextBox ID="txttelefono" runat="server" MaxLength="20"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:20%">
                          &nbsp;</td>
                        <td>
        
                <asp:Label ID="lblmensaje" runat="server" CssClass="rojo" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:20%">
                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" 
                              ShowMessageBox="True" ShowSummary="False" ValidationGroup="BuscarNombre" />
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                                ShowMessageBox="True" ShowSummary="False" />
                        </td>
                        <td>
                            <asp:Button ID="cmdGuardar" runat="server" Enabled="False" 
                                Text="Guardar" SkinID="BotonGuardar" />
                            &nbsp;<asp:Button ID="cmdLimpiar" runat="server" SkinID="BotonLimpiar" 
                                Text="Limpiar" ValidationGroup="Limpiar" />
                            &nbsp;<asp:Button ID="cmdCancelar" runat="server" SkinID="BotonSalir" 
                                Text="Cerrar" ValidationGroup="Salir" 
                                EnableTheming="True" />
                            <asp:HiddenField ID="hdcodigo_pot" runat="server" Value="0" />
                            <asp:HiddenField ID="hdcodigo_pso" runat="server" Value="0" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width:20%">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width:20%">
                            &nbsp;</td>
                        <td>
                            <asp:GridView ID="grwDeudas" runat="server" AutoGenerateColumns="False" 
                                Visible="False" SkinID="skinGridViewLineasIntercalado">
                                <Columns>
                                    <asp:BoundField DataField="descripcion_Sco" HeaderText="Servicio" />
                                    <asp:BoundField DataField="montoTotal_Deu" HeaderText="Deuda" />
                                    <asp:BoundField DataField="Pago_Deu" HeaderText="Pago" />
                                    <asp:BoundField DataField="Saldo_Deu" HeaderText="Saldo" />
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            

    </form>
</body>
</html>
