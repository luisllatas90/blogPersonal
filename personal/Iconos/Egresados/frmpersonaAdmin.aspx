<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmpersonaAdmin.aspx.vb" Inherits="frmpersona" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registrar Persona</title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="JavaScript" src="../../private/funciones.js"></script>
    <script type="text/javascript" language="JavaScript" src="../../private/jq/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" language="javascript" src="../../private/jq/jquery.mascara.js"></script> 
    <script src="../../private/calendario.js"></script>
	<script type="text/javascript" language="javascript">
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
	        $("#txtFechaNac").mask("99/99/9999");
	       
	        $("#txtdni").blur(function() {
	            if ($("#txtdni").val().length > 0) { //Validar solamente si hay datos ingresados en el DNI
                    $("#errornrodoc").empty(); 
	                if ($("#dpTipoDoc").val() == "DNI" && ($("#txtdni").val().length < 8 || $("#txtdni").val().length > 8)) {    
	                    $("#errornrodoc").text("El número de DNI es incorrecto. Debe contener 8 caracteres.");
	                }
	                if ($("#dpTipoDoc").val() != "DNI" && $("#txtdni").val().length < 9) {
	                    $("#errornrodoc").text("El número de pasaporte es incorrecto. Mínimo 9 caracteres.");
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
    <style type="text/css">
        .style1
        {
            height: 39px;
        }
        .style2
        {
            width: 175px;
            height: 22px;
        }
        .style3
        {
            height: 22px;
        }
        .style4
        {
            width: 10%;
        }
        .style5
        {
            width: 175px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">    
    <table width="100%">
        <tr>
            <td style="width: 20%">
            <img src="../../librerianet/Egresado/archivos/logousat.png" />
            </td>            
            <td>
            <span class="usatTitulo">DIRECCIÓN DE ALUMNI - REGISTRO DE EGRESADO</span>
            </td>
        </tr>
    </table>                  
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>        
                <table width="100%">
                    <tr>
                        <td align="right">
                            <asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="salir" Width="100px" Height="22px"  />        
                        </td>
                    </tr>
                </table>                
                <br />
                <table style="width: 100%" class="contornotabla">
                    <tr>
                        <td class="style5">
                            Doc. de Identidad</td>
                        <td>
                            <asp:DropDownList ID="dpTipoDoc" runat="server" SkinID="ComboObligatorio" 
                                AutoPostBack="True">
                                <asp:ListItem>DNI</asp:ListItem>
                                <asp:ListItem>CARNÉ DE EXTRANJERÍA</asp:ListItem>
                                <asp:ListItem>COD. UNIVERSITARIO</asp:ListItem>
                            </asp:DropDownList>
                            &nbsp;&nbsp;
                            <b>Nro. Doc. Identidad<asp:RequiredFieldValidator ID="RequiredFieldValidator11" 
                                runat="server" ControlToValidate="txtdni" 
                                ErrorMessage="Debe ingresar el número  de doc. de indentificación">*</asp:RequiredFieldValidator>
                            </b>
                            <asp:TextBox ID="txtdni" runat="server" CssClass="cajas" MaxLength="15" 
                                SkinID="CajaTextoObligatorio"></asp:TextBox>
                            &nbsp;<asp:LinkButton ID="lnkComprobarDNI" runat="server" Font-Bold="True" 
                                Font-Underline="True" ForeColor="Blue" ValidationGroup="Comprobar">[Buscar]</asp:LinkButton>
                        &nbsp;&nbsp;&nbsp;
                            <asp:Label ID="lblEgresado" runat="server" Font-Bold="True" Font-Names="Arial" 
                                Font-Size="X-Small" ForeColor="#009999"></asp:Label>
                        &nbsp;<asp:Label ID="lbMensajeEgresado" runat="server" Font-Bold="True" 
                                ForeColor="Red"></asp:Label>
                        </td>
                    </tr>   
                    </table>
                    <table style="width: 100%" class="contornotabla" id="tablaNombre" runat="server">                 
                    <tr>
                        <td class="style5">
                            Apellido Paterno                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                ControlToValidate="txtAPaterno" 
                                ErrorMessage="Debe ingresar el Apellido Paterno">*</asp:RequiredFieldValidator>
                           
                        </td>
                        <td>
                            <asp:TextBox ID="txtAPaterno" runat="server" MaxLength="100" 
                                Width="250px" SkinID="CajaTextoObligatorio"></asp:TextBox>
                        &nbsp;&nbsp;&nbsp;&nbsp; Apellido Materno<b><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                ControlToValidate="txtAMaterno" 
                                ErrorMessage="Debe ingresar el Apellido Materno">*</asp:RequiredFieldValidator>
                                <asp:TextBox ID="txtAMaterno" runat="server" MaxLength="100" 
                                Width="250px" SkinID="CajaTextoObligatorio"></asp:TextBox>
                            </b>
                        </td>
                    </tr>                    
                    <tr>
                        <td class="style5">
                            Nombres <b>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                    ControlToValidate="txtNombres" 
                    ErrorMessage="Debe ingresar los Nombres">*</asp:RequiredFieldValidator>
                            </b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNombres" runat="server" MaxLength="80" 
                    Width="250px" SkinID="CajaTextoObligatorio"></asp:TextBox>
                            &nbsp;<asp:LinkButton ID="lnkComprobarNombres" runat="server" Font-Bold="True" 
                                Font-Underline="True" ForeColor="Blue" ValidationGroup="Comprobar" 
                                Visible="False">Clic aquí para buscar coincidencias</asp:LinkButton>
                        </td>
                    </tr>
                    <tr runat="server" id="trConcidencias">
                        <td colspan="2">                            
                            <div id="listadiv" style="width:100%;heigh:200px">
                            <asp:GridView ID="grwCoincidencias" runat="server" AutoGenerateColumns="False" 
                                DataKeyNames="codigo_pso" Width="100%" SkinID="skinGridViewLineas">
                                <Columns>
                                    <asp:BoundField DataField="apellidoPaterno_Pso" HeaderText="Ap. Paterno" >
                                        <ItemStyle Width="15%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="apellidoMaterno_Pso" HeaderText="Ap. Materno" >
                                        <ItemStyle Width="15%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="nombres_Pso" HeaderText="Nombres" >
                                        <ItemStyle Width="15%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="numeroDocIdent_Pso" HeaderText="Doc. Ident." >
                                        <ItemStyle Width="10%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="fechaNacimiento_pso" DataFormatString="{0:d}" 
                                        HeaderText="Fecha Nac." />
                                    <asp:BoundField DataField="direccion_pso" HeaderText="Dirección" />
                                    <asp:BoundField DataField="emailprincipal_pso" HeaderText="Email" />
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
                        <td class="style5">
                            Fecha Nac. <b>
                            <!-- <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                    ControlToValidate="txtFechaNac" 
                    ErrorMessage="Debe ingresar la Fecha de Nac.">*</asp:RequiredFieldValidator> -->
                            </b>
                            <!-- <asp:RangeValidator ID="RangeValidator1" runat="server" 
                                ControlToValidate="txtFechaNac" 
                                ErrorMessage="La fecha de Nacimiento es incorrecta" MaximumValue="31/12/2050" 
                                MinimumValue="01/01/1920" Type="Date">*</asp:RangeValidator> -->
                        </td>
                        <td>
                            <asp:TextBox ID="txtFechaNac" runat="server" MaxLength="11" 
                    Width="20%" SkinID="CajaTextoObligatorio" Enabled="False"></asp:TextBox>
                            &nbsp;&nbsp;&nbsp;&nbsp;Sexo:&nbsp;<asp:DropDownList ID="dpSexo" runat="server" 
                                SkinID="ComboObligatorio" Width="20%" Enabled="False">
                                <asp:ListItem Value="-1">&gt;&gt;Seleccione&lt;&lt;</asp:ListItem>
                                <asp:ListItem Value="M">Masculino</asp:ListItem>
                                <asp:ListItem Value="F">Femenino</asp:ListItem>
                            </asp:DropDownList>
                            <asp:CompareValidator ID="CompareValidator3" runat="server" 
                                ControlToValidate="dpSexo" ErrorMessage="Seleccione el sexo" 
                                Operator="NotEqual" ValueToCompare="-1">*</asp:CompareValidator>
                            &nbsp;Estado Civil:
                            <asp:DropDownList ID="dpEstadoCivil" runat="server" SkinID="ComboObligatorio" Width="20%">
                                <asp:ListItem Value="-1">&gt;&gt;Seleccione&lt;&lt;</asp:ListItem>
                                <asp:ListItem Value="SOLTERO">SOLTERO</asp:ListItem>
                                <asp:ListItem Value="CASADO">CASADO</asp:ListItem>
                                <asp:ListItem Value="VIUDO">VIUDO</asp:ListItem>
                                <asp:ListItem Value="DIVORCIADO">DIVORCIADO</asp:ListItem>
                            </asp:DropDownList>
                            <asp:CompareValidator ID="CompareValidator2" runat="server" 
                                ControlToValidate="dpEstadoCivil" ErrorMessage="Seleccione el Estado Civil" 
                                Operator="NotEqual" ValueToCompare="-1">*</asp:CompareValidator>
                            
                        </td>
                    </tr>
                    <tr>
                        <td class="style5">Conyugue</td>
                        <td><asp:TextBox ID="txtConyuge" runat="server" Width="33%"></asp:TextBox> &nbsp;&nbsp;
                            Fecha de Matrimonio:&nbsp;<asp:TextBox ID="txtFecha" runat="server" Font-Names="Arial" Font-Size="X-Small" 
                        Width="127px"></asp:TextBox>
                    <input onclick="MostrarCalendario('txtFecha')" type="button" value="..." /></td>
                    </tr>
                    <tr>
                        <td class="style5">
                            Email Principal<b><asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
                                ControlToValidate="txtemail1" ErrorMessage="Ingrese un Email principal válido." 
                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                            </b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtemail1" runat="server" Width="33%" 
                                MaxLength="80"></asp:TextBox>
                         &nbsp;&nbsp; Email Alternativo<b>:<asp:RegularExpressionValidator 
                                ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtemail2" 
                                ErrorMessage="Ingrese un Email secundario válido." 
                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                            </b>&nbsp;&nbsp;
                            <asp:TextBox ID="txtemail2" runat="server" Width="31.5%" 
                                MaxLength="80"></asp:TextBox>
                        </td>
                    </tr>                    
                    <tr>
                        <td class="style5">
                            Dirección <b>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
                    ControlToValidate="txtdireccion" 
                    ErrorMessage="Debe ingresar la dirección de la persona">*</asp:RequiredFieldValidator>
                            </b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtdireccion" runat="server" Width="77.3%" 
                                MaxLength="150" SkinID="CajaTextoObligatorio"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style5">
                            Departamento</td>
                        <td>
                            <asp:DropDownList ID="dpdepartamento" runat="server" 
                    AutoPostBack="True" SkinID="ComboObligatorio" Width="20%">
                            </asp:DropDownList>
                            <asp:CompareValidator ID="CompareValidator4" runat="server" 
                                ControlToValidate="dpdepartamento" ErrorMessage="Seleccione el Departamento" 
                                Operator="NotEqual" ValueToCompare="-1">*</asp:CompareValidator>
                            &nbsp;Provincia:
                            <asp:DropDownList ID="dpprovincia" runat="server" 
                    AutoPostBack="True" SkinID="ComboObligatorio" Width="20%">
                            </asp:DropDownList>
                            <asp:CompareValidator ID="CompareValidator5" runat="server" 
                                ControlToValidate="dpprovincia" ErrorMessage="Seleccione la provincia" 
                                Operator="NotEqual" ValueToCompare="-1">*</asp:CompareValidator>
                            &nbsp;Distrito:
                            <asp:DropDownList ID="dpdistrito" runat="server" SkinID="ComboObligatorio" Width="20%">
                            </asp:DropDownList>
                            <asp:CompareValidator ID="CompareValidator6" runat="server" 
                                ControlToValidate="dpdistrito" ErrorMessage="Seleccione el Distrito" 
                                Operator="NotEqual" ValueToCompare="-1">*</asp:CompareValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="style5">
                            Teléfono Fijo:</td>
                        <td>                            
                            <asp:TextBox ID="txttelefono" runat="server" MaxLength="20" Width="20%"></asp:TextBox>
                            &nbsp;&nbsp;&nbsp;&nbsp;Celular:&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtcelular" 
                                runat="server" MaxLength="20"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style5">
                            Modalidad Ingreso</td>
                        <td>
                            <asp:DropDownList ID="dpModalidad" runat="server" SkinID="ComboObligatorio" 
                                Width="20%" Enabled="False">
                            </asp:DropDownList>
                            <!-- <asp:CompareValidator ID="CompareValidator7" runat="server" 
                                ControlToValidate="dpModalidad" ErrorMessage="Seleccione la Modalidad" 
                                Operator="NotEqual" ValueToCompare="-1">*</asp:CompareValidator> -->
                        &nbsp;&nbsp;RUC: &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtruc" runat="server" MaxLength="12"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style5">
                        Foto:</td>
                        <td>                        
                            <asp:FileUpload ID="fileFoto" runat="server" Width="32%" />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                ErrorMessage="Tipo de archivo no permitido (*.jpeg, *.gif, *.png)" ControlToValidate="fileFoto" 
                                ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.jpg|.JPG|.gif|.GIF|.png|.PNG|.jpeg|.JPEG)$">
                            </asp:RegularExpressionValidator>                            
                        </td>
                    </tr>                                                          
                    <tr>
                        <td colspan="2">&nbsp;</td>
                    </tr> 
                    <tr>
                        <td colspan="2"><b>Datos Profesionales</b></td>                        
                    </tr> 
                    <tr>
                        <td class="style5">Se encuentra laborando: </td>
                        <td>                           
                            <asp:RadioButtonList ID="rblSituacionLaboral" runat="server" Width="140px" 
                                RepeatDirection="Horizontal" AutoPostBack="True">
                                <asp:ListItem Text="Si" Value="S"></asp:ListItem><asp:ListItem Text="No" Value="N"></asp:ListItem>
                            </asp:RadioButtonList>                                                   
                        </td>
                    </tr>                                        
                    <tr>
                        <td class="style5">Tipo de Empresa</td>
                        <td>
                            <asp:RadioButtonList ID="rblTipoEmpresa" runat="server" 
                                RepeatDirection="Horizontal">
                                <asp:ListItem Text="Privada" Value="P"></asp:ListItem>
                                <asp:ListItem Text="Pública" Value="N"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr> 
                    <tr>
                        <td class="style5">Sector:</td>
                        <td>
                            <asp:DropDownList ID="dpSector" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr> 
                    <tr>
                        <td class="style5">Empresa donde labora:</td>
                        <td>
                            <asp:TextBox ID="txtEmpresaLabora" runat="server" Width="32%"></asp:TextBox>
                        &nbsp; Direccion Empresa:<asp:TextBox ID="txtDireccionEmpresa" runat="server" Width="32%"></asp:TextBox>
                        </td>
                    </tr>                                          
                    <tr>
                        <td class="style5">Telefono</td>
                        <td>
                            <asp:TextBox ID="txtTelefonoProfesional" runat="server" Width="32%"></asp:TextBox>
                        &nbsp; Correo Profesional:<asp:TextBox ID="txtCorreoProfesional" runat="server" 
                                Width="32%"></asp:TextBox>
                        </td>
                    </tr>  
                    <tr>
                        <td class="style5">Cargo Actual:</td>
                        <td>
                            <asp:TextBox ID="txtCargoActual" runat="server" Width="32%"></asp:TextBox>
                        </td>
                    </tr> 
                    <tr>
                        <td colspan="2">&nbsp;</td>
                    </tr> 
                    <tr>
                        <td colspan="2"><b>Currriculum Vitae</b></td> 
                    </tr>                    
                    <tr>
                        <td class="style5">Nivel</td>
                        <td>
                            <asp:DropDownList ID="dpNivel" runat="server" Enabled="False">                             
                                <asp:ListItem Value="BACHILLER">BACHILLER</asp:ListItem>
                                <asp:ListItem Value="EGRESADO">EGRESADO</asp:ListItem>
                                <asp:ListItem Value="TITULADO">TITULADO</asp:ListItem>
                            </asp:DropDownList>                            
                        </td>
                    </tr>                    
                    <tr>
                        <td class="style5">
                        Formacion Académica:</td>
                        <td> 
                            <asp:TextBox ID="txtFormacion" runat="server" TextMode="MultiLine" 
                                Width="77.3%"></asp:TextBox>                  
                        </td>
                    </tr>
                    <tr>
                        <td class="style5">
                        Experiencia Laboral:</td>
                        <td>   
                            <asp:TextBox ID="txtExperiencia" runat="server" TextMode="MultiLine" 
                                Width="77.3%"></asp:TextBox>                    
                        </td>
                    </tr>                    
                    <tr>
                        <td class="style5">
                        Otros Estudios:</td>
                        <td>   
                            <asp:TextBox ID="txtOtrosEstudios" runat="server" TextMode="MultiLine" 
                                Width="77.3%"></asp:TextBox>                  
                        </td>
                    </tr>                                    
                    <tr>
                        <td class="style5">
                        Curriculum:</td>
                        <td>                        
                            <asp:FileUpload ID="fileCV" runat="server" Width="32%" />                               
                            <asp:LinkButton ID="lnkFormato" runat="server" Font-Bold="True" 
                                Font-Overline="False" Font-Underline="True" ForeColor="#0683FF">Descargar Formato</asp:LinkButton>
                               <br />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                ErrorMessage="Solo documentos (*.doc, *.docx, *.pdf) " ControlToValidate="fileCV" 
                                ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.doc|.DOC|.docx|.DOCX|.pdf|.PDF)$">
                            </asp:RegularExpressionValidator>                            
                        </td>
                    </tr>                                        
                    <tr>
                        <td colspan="2"><b>Ultima actualizacion al <asp:Label ID="lblActualizacion" runat="server"></b></asp:Label>
                        </td>                        
                    </tr>
                    <tr>
                        <td colspan="2" class="style1"><br />En promedio, cuantos meses le llevó conseguir un puesto de trabajo acorde a la formación que recibió después de titulado <br />
                            <asp:CheckBox ID="chkTresMeses" runat="server" Text="Antes de los 3 meses" 
                                AutoPostBack="True" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; Indique el Número de meses
                            <asp:TextBox ID="txtNumMeses" runat="server"></asp:TextBox>
                        </td>                        
                    </tr> 
                    <tr>
                        <td colspan="2">                        
                            <br /><asp:CheckBox ID="chkMostrar" runat="server" Text="Mostrar Perfil" />
                        &nbsp;&nbsp;&nbsp;<asp:Label ID="Label1" runat="server" 
                                Text="[Permite que su perfil sea visible por las empresas]"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            </td>
                        <td class="style3">                                    
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="cmdGuardar" runat="server" Enabled="False" 
                                Text="Actualizar" SkinID="BotonGuardar" />
                            &nbsp;<asp:Button ID="cmdLimpiar" runat="server" SkinID="BotonLimpiar" 
                                Text="Limpiar" ValidationGroup="Limpiar" Width="86px" />
                            &nbsp;
                        </td>
                    </tr>           
                    <tr>
                        <td class="style4" colspan="2">
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                                ShowMessageBox="True" ShowSummary="False" />
                        </td>                                                
                    </tr>                            
                    
                </table>
                <asp:Label ID="lblmensaje" runat="server" CssClass="rojo" Font-Bold="True"></asp:Label>
            <asp:HiddenField ID="hdcodigo_pso" runat="server" Value="0" />
            <asp:HiddenField ID="HdFileCV" runat="server" />
            <asp:HiddenField ID="HdFileFoto" runat="server" />

    </form>
</body>
</html>
