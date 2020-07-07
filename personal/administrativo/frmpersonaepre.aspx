<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmpersonaepre.aspx.vb" Inherits="frmpersonaepre" Theme="Acero" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registrar Persona</title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="JavaScript" src="../../private/funciones.js"></script>
    <script type="text/javascript" language="JavaScript" src="../../private/jq/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" language="javascript" src="../../private/jq/jquery.mascara.js"></script> 
    <script type="text/javascript" language="JavaScript" src="../../private/jq/lbox/thickbox.js"></script>
	<link rel="stylesheet" href="../../private/jq/lbox/thickbox.css" type="text/css" media="screen" />
	
	<!----------- Para DropDowList con CheckBoxs !--------------------->
	<link href="../css/MyStyles.css" rel="stylesheet" type="text/css" />
	<!----------------------------------------------------------------->
	
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


        /********************* Para DropDowList con CheckBoxs ******************************/
	    var timoutID;

	    function ShowMList() {
	        var divRef = document.getElementById("divCheckBoxList");
	        divRef.style.display = "block";
	        var divRefC = document.getElementById("divCheckBoxListClose");
	        divRefC.style.display = "block";
	    }
	    
	    function ShowMList2() {
	        var divRef2 = document.getElementById("divCheckBoxList2");
	        divRef2.style.display = "block";
	        var divRefC2 = document.getElementById("divCheckBoxListClose2");
	        divRefC2.style.display = "block";
	    }

	    function HideMList() {
	        document.getElementById("divCheckBoxList").style.display = "none";
	        document.getElementById("divCheckBoxListClose").style.display = "none";
	    }

	    function HideMList2() {
	        document.getElementById("divCheckBoxList2").style.display = "none";
	        document.getElementById("divCheckBoxListClose2").style.display = "none";
	    }

	    function FindSelectedItems(sender, textBoxID) {
	        var cblstTable = document.getElementById(sender.id);	        
	        var checkBoxPrefix = sender.id + "_";
	        var noOfOptions = cblstTable.rows.length;
	        var selectedText = "";
	        for (i = 0; i < noOfOptions; ++i) {
	            if (document.getElementById(checkBoxPrefix + i).checked) {
	                if (selectedText == "")
	                    selectedText = document.getElementById(checkBoxPrefix + i).parentNode.innerText;
	                else
	                    selectedText = selectedText + "," + document.getElementById(checkBoxPrefix + i).parentNode.innerText;
	            }
	        }
	        document.getElementById(textBoxID.id).value = selectedText;
	    }

	    function MarcarItems(sender, textBoxID, item) {	        
	        var cblstTable = document.getElementById(sender);	        	        
	        var checkBoxPrefix = sender + "_";
	        var noOfOptions = cblstTable.rows.length;
	        var selectedText = document.getElementById(textBoxID).value;
	        
	        //Recorrer lista
	        //Marcar el item que coincida
	        //Armar el texto
	        //Asignar el texto al txt

	        for (i = 0; i < noOfOptions; ++i) {
	            if (document.getElementById(checkBoxPrefix + i).parentNode.innerText == item) {
	                document.getElementById(checkBoxPrefix + i).checked = true;

	                if (selectedText == "")
	                    selectedText = document.getElementById(checkBoxPrefix + i).parentNode.innerText;
	                else
	                    selectedText = selectedText + "," + document.getElementById(checkBoxPrefix + i).parentNode.innerText;

	            }
	        }	        
	        document.getElementById(textBoxID).value = selectedText;

	    }

	    function LimpiarItems(sender, textBoxID) {
	        var cblstTable = document.getElementById(sender);
	        var checkBoxPrefix = sender + "_";
	        var noOfOptions = cblstTable.rows.length;	        
	        
	        for (i = 0; i < noOfOptions; ++i) {
	            document.getElementById(checkBoxPrefix + i).checked = false;
	            document.getElementById(textBoxID).value = "";
	        }
	     }
	    /************************************************************************************/
	    
	</script>
    <style type="text/css">
        .style1
        {
            height: 20px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <span class="usatTitulo">Escuela Pre-Universitaria: Registro de postulantes</span>
    <table style="width: 100%">
                    <tr>
                        <td colspan="2" 
                            style="border: 1px ridge #808080; background-color: #FF4040; height: 25px; color: #FFFFFF;" 
                            class="style1">
                            Datos de Postulación</td>
                    </tr>
                    <tr>
                        <td style="width:20%">
                            Ciclo de Ingreso</td>
                        <td>
                            <asp:DropDownList ID="dpCicloIng_alu" runat="server" SkinID="ComboObligatorio" AutoPostBack=true>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:20%">
                            Escuela Profesional</td>
                        <td>
                            <asp:DropDownList ID="dpCodigo_cpf" runat="server" SkinID="ComboObligatorio" Width="300px" AutoPostBack=true>
                            </asp:DropDownList>
                            <asp:CompareValidator ID="CompareValidator8" runat="server" 
                                ControlToValidate="dpCodigo_cpf" ErrorMessage="Seleccione la Escuela Profesional" 
                                Operator="NotEqual" ValueToCompare="-1">*</asp:CompareValidator>
                        &nbsp;Modalidad:
                            <asp:DropDownList ID="dpModalidad" runat="server" SkinID="ComboObligatorio">
                            </asp:DropDownList>
                            <asp:CompareValidator ID="CompareValidator7" runat="server" 
                                ControlToValidate="dpModalidad" ErrorMessage="Seleccione la Modalidad" 
                                Operator="NotEqual" ValueToCompare="-1">*</asp:CompareValidator>
                        </td>
                    </tr>
                    
                    <tr>
                        <td style="width:20%">
                            Categoría</td>
                         <td>
                            <!----------------------------- Para DropDowList con CheckBoxs !--------------------------------->
                            <div id="divCustomCheckBoxList2" runat="server" onmouseover="clearTimeout(timoutID);" onmouseout="timoutID = setTimeout('HideMList2()', 750);"  style="float:left; width:300px">
                                <table>
                                    <tr>
                                        <td align="right" class="DropDownLook2">
                                            <input id="txtSelectedMLValues2" type="text" readonly="readonly" onclick="ShowMList2()" style="width:229px;" runat="server" />
                                        </td>
                                        <td align="left" class="DropDownLook2">
                                            <img id="imgShowHide2" runat="server" src="~/Iconos/drop.gif" onclick="ShowMList2()" align="left" />                
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" class="DropDownLook2">
                                            <div>
            	                                <div runat="server" id="divCheckBoxListClose2" class="DivClose2">			                        
		                                            <label runat="server" onclick="HideMList2();" class="LabelClose2" id="lblClose2" style="font-size:18px; font-weight:bold; color:red"> x</label>
		                                        </div>
                                                <div runat="server" id="divCheckBoxList2" class="DivCheckBoxList2">
		                                            <asp:CheckBoxList ID="lstMultipleValues2" runat="server" Width="250px" CssClass="CheckBoxList2"></asp:CheckBoxList>						        			           			        
		                                        </div>
		                                    </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <!------------------------------------------------------------------------->
                            
                            <div style="float:left; padding-left:20px">Beneficio:&nbsp&nbsp</div>                                                        
                            
                            <div id="divCustomCheckBoxList" runat="server" onmouseover="clearTimeout(timoutID);" onmouseout="timoutID = setTimeout('HideMList()', 750);" style="float:left">
                                <table>
                                    <tr>
                                        <td align="right" class="DropDownLook">
                                            <input id="txtSelectedMLValues" type="text" readonly="readonly" onclick="ShowMList()" style="width:229px;" runat="server" />
                                        </td>
                                        <td align="left" class="DropDownLook">
                                            <img id="imgShowHide" runat="server" src="~/Iconos/drop.gif" onclick="ShowMList()" align="left" />                
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" class="DropDownLook">
                                            <div>
            	                                <div runat="server" id="divCheckBoxListClose" class="DivClose">			                        
		                                            <label runat="server" onclick="HideMList();" class="LabelClose" id="lblClose" style="font-size:18px; font-weight:bold; color:red"> x</label>
		                                        </div>
                                                <div runat="server" id="divCheckBoxList" class="DivCheckBoxList">
		                                            <asp:CheckBoxList ID="lstMultipleValues" runat="server" Width="250px" CssClass="CheckBoxList"></asp:CheckBoxList>						        			           			        
		                                        </div>
		                                    </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                         </td>                                                
                    </tr>
                    
                    
                    <tr>
                        <td style="width:20%">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="usatTablaInfo" colspan="2">
                            Datos Personales</td>
                    </tr>
                    <tr>
                        <td style="width:20%">
                            Doc. de Identidad</td>
                        <td>
                            <asp:DropDownList ID="dpTipoDoc" runat="server" SkinID="ComboObligatorio">
                                <asp:ListItem>DNI</asp:ListItem>
                                <asp:ListItem>CARNÉ DE EXTRANJERÍA</asp:ListItem>
                                <asp:ListItem>PASAPORTE</asp:ListItem>
                            </asp:DropDownList>
                            &nbsp;<asp:HyperLink ID="lnkreniec0" runat="server" Font-Bold="False" 
                                Font-Underline="True" ForeColor="Red" 
                                NavigateUrl="http://ww4.essalud.gob.pe:7777/acredita/" 
                                Target="_blank">[Obtener DNI de EsSalud]</asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:20%">
                            <b>Nro. Doc. Identidad</b></td>
                        <td>
                            <asp:TextBox ID="txtdni" runat="server" CssClass="cajas" MaxLength="15" 
                                SkinID="CajaTextoObligatorio"></asp:TextBox>
                            &nbsp;<asp:LinkButton ID="lnkComprobarDNI" runat="server" Font-Bold="True" 
                                Font-Underline="True" ForeColor="Blue" ValidationGroup="Comprobar">[Buscar]</asp:LinkButton>&nbsp;&nbsp;<span id="errornrodoc" style="color:red"></span>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:20%">
                            Apellido Paterno                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                ControlToValidate="txtAPaterno" 
                                ErrorMessage="Debe ingresar el Apellido Paterno">*</asp:RequiredFieldValidator>
                            </b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAPaterno" runat="server" MaxLength="100" 
                                Width="250px" SkinID="CajaTextoObligatorio"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:20%">
                            Apellido Materno<b><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                ControlToValidate="txtAMaterno" 
                                ErrorMessage="Debe ingresar el Apellido Materno">*</asp:RequiredFieldValidator></b></td>
                        <td>
                            <asp:TextBox ID="txtAMaterno" runat="server" MaxLength="100" 
                                Width="250px" SkinID="CajaTextoObligatorio"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:20%">
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
                                Font-Underline="True" ForeColor="Blue" ValidationGroup="Comprobar">Clic aquí para buscar coincidencias</asp:LinkButton></td>
                    </tr>
                    <tr runat="server" id="trConcidencias">
                        <td style="width:20%">
                            &nbsp;</td>
                        <td>
                            <div id="listadiv" style="width:100%;height:200px">
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
                        <td style="width:20%">
                            Fecha Nac. <b>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                    ControlToValidate="txtFechaNac" 
                    ErrorMessage="Debe ingresar la Fecha de Nac.">*</asp:RequiredFieldValidator>
                            </b>
                            <asp:RangeValidator ID="RangeValidator1" runat="server" 
                                ControlToValidate="txtFechaNac" 
                                ErrorMessage="La fecha de Nacimiento es incorrecta" MaximumValue="31/12/2050" 
                                MinimumValue="01/01/1920" Type="Date">*</asp:RangeValidator>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFechaNac" runat="server" MaxLength="11" 
                    Width="110px" SkinID="CajaTextoObligatorio"></asp:TextBox>
                            &nbsp;&nbsp;&nbsp;&nbsp;Sexo:&nbsp;<asp:DropDownList ID="dpSexo" runat="server" 
                                SkinID="ComboObligatorio">
                                <asp:ListItem Value="-1">&gt;&gt;Seleccione&lt;&lt;</asp:ListItem>
                                <asp:ListItem Value="M">Masculino</asp:ListItem>
                                <asp:ListItem Value="F">Femenino</asp:ListItem>
                            </asp:DropDownList>
                            <asp:CompareValidator ID="CompareValidator3" runat="server" 
                                ControlToValidate="dpSexo" ErrorMessage="Seleccione el sexo" 
                                Operator="NotEqual" ValueToCompare="-1">*</asp:CompareValidator>
                            &nbsp;Estado Civil:
                            <asp:DropDownList ID="dpEstadoCivil" runat="server" SkinID="ComboObligatorio">
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
                        <td style="width:20%">
                            Email Principal<b><asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
                                ControlToValidate="txtemail1" ErrorMessage="Ingrese un Email principal válido." 
                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator></b></td>
                        <td>
                            <asp:TextBox ID="txtemail1" runat="server" Width="300px" 
                                MaxLength="80"></asp:TextBox>
                        &nbsp;Alternativo:
                            <asp:TextBox ID="txtemail2" runat="server" Width="300px" 
                                MaxLength="80" style="margin-bottom: 0px"></asp:TextBox>
                            <b><asp:RegularExpressionValidator 
                                ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtemail2" 
                                ErrorMessage="Ingrese un Email secundario válido." 
                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                            </b>
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
                            <asp:TextBox ID="txtdireccion" runat="server" Width="60%" 
                                MaxLength="150" SkinID="CajaTextoObligatorio"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:20%">
                            Departamento</td>
                        <td>
                            <asp:DropDownList ID="dpdepartamento" runat="server" 
                    AutoPostBack="True" SkinID="ComboObligatorio">
                            </asp:DropDownList>
                            <asp:CompareValidator ID="CompareValidator4" runat="server" 
                                ControlToValidate="dpdepartamento" ErrorMessage="Seleccione el Departamento" 
                                Operator="NotEqual" ValueToCompare="-1">*</asp:CompareValidator>
                            &nbsp;Provincia:
                            <asp:DropDownList ID="dpprovincia" runat="server" 
                    AutoPostBack="True" SkinID="ComboObligatorio">
                            </asp:DropDownList>
                            <asp:CompareValidator ID="CompareValidator5" runat="server" 
                                ControlToValidate="dpprovincia" ErrorMessage="Seleccione la provincia" 
                                Operator="NotEqual" ValueToCompare="-1">*</asp:CompareValidator>
                            &nbsp;Distrito:
                            <asp:DropDownList ID="dpdistrito" runat="server" SkinID="ComboObligatorio">
                            </asp:DropDownList>
                            <asp:CompareValidator ID="CompareValidator6" runat="server" 
                                ControlToValidate="dpdistrito" ErrorMessage="Seleccione el Distrito" 
                                Operator="NotEqual" ValueToCompare="-1">*</asp:CompareValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:20%">
                            Teléfono</td>
                        <td>
                            Fijo:
                            <asp:TextBox ID="txttelefono" runat="server" MaxLength="20"></asp:TextBox>
                            &nbsp;&nbsp;&nbsp;&nbsp;Celular:&nbsp;<asp:TextBox ID="txtcelular" 
                                runat="server" MaxLength="20"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="width:20%">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="usatTablaInfo" colspan="2">
                            Datos de Estudios Secundarios</td>
                    </tr>
                    <tr>
                        <td style="width:20%">
                            País</td>
                        <td>
                            <asp:DropDownList ID="dpPaisColegio" runat="server" AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:20%">
                            Departamento</td>
                        <td>
                            <asp:DropDownList ID="dpdepartamentocolegio" runat="server" 
                    AutoPostBack="True" SkinID="ComboObligatorio">
                            </asp:DropDownList>
                            <asp:CompareValidator ID="CompareValidator9" runat="server" 
                                ControlToValidate="dpdepartamentocolegio" ErrorMessage="Seleccione el Departamento del colegio" 
                                Operator="NotEqual" ValueToCompare="-1">*</asp:CompareValidator>
                            &nbsp;Provincia:
                            <asp:DropDownList ID="dpprovinciacolegio" runat="server" 
                    AutoPostBack="True" SkinID="ComboObligatorio">
                            </asp:DropDownList>
                            <asp:CompareValidator ID="CompareValidator10" runat="server" 
                                ControlToValidate="dpprovinciacolegio" ErrorMessage="Seleccione la provincia del colegio" 
                                Operator="NotEqual" ValueToCompare="-1">*</asp:CompareValidator>
                            &nbsp;Distrito:
                            <asp:DropDownList ID="dpdistritocolegio" runat="server" 
                                SkinID="ComboObligatorio" AutoPostBack="True">
                            </asp:DropDownList>
                            <asp:CompareValidator ID="CompareValidator11" runat="server" 
                                ControlToValidate="dpdistritocolegio" ErrorMessage="Seleccione el Distrito del Colegio" 
                                Operator="NotEqual" ValueToCompare="-1">*</asp:CompareValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:20%">
  Colegio<asp:CompareValidator ID="CompareValidator12" runat="server" 
                                ControlToValidate="dpCodigo_col" ErrorMessage="Seleccione el Colegio" 
                                Operator="NotEqual" ValueToCompare="-1">*</asp:CompareValidator></td>
                        <td>
                            <asp:DropDownList ID="dpCodigo_col" runat="server" SkinID="ComboObligatorio" AutoPostBack =true >
                            </asp:DropDownList>
                            <asp:ImageButton ID="imgActualizarColegio" runat="server" 
                                ImageUrl="../../images/menus/refresh.png" 
                                ToolTip="Actualizar lista de colegios" ValidationGroup="Colegio" 
                                Visible="False" />
                            <asp:Label ID="lblAgregarColegio" runat="server" Font-Underline="True" 
                                ForeColor="Blue" Text="[Agregar]" Visible="False"></asp:Label><asp:TextBox 
                                ID="txtColegio" runat="server" Width="139px" 
                                ValidationGroup="BuscarColegio"></asp:TextBox>
                            <asp:ImageButton ID="ImgBuscarColegios" runat="server" 
                                ImageUrl="~/images/busca.gif" ValidationGroup="BuscarColegio" />
                            <asp:Label ID="lblTextBusqueda" runat="server" Text="(clic aquí)"></asp:Label>
&nbsp;<asp:LinkButton ID="lnkBusquedaAvanzada" runat="server" ForeColor="Blue" 
                                ValidationGroup="BuscarColegio">Busqueda Avanzada</asp:LinkButton></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Panel ID="pnlDatos" runat="server" Height="150px" ScrollBars="Vertical" 
                                Width="100%">
                                <asp:GridView ID="gvColegios" runat="server" AutoGenerateColumns="False" 
                                    BorderColor="#628BD7" BorderStyle="Solid" BorderWidth="1px" CellPadding="4" 
                                    DataKeyNames="codigo_ied,codigo_dis,codigo_pro,codigo_dep" 
                                    ForeColor="#333333" ShowHeader="False" 
                                    Width="98%">
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <RowStyle BackColor="#EFF3FB" />
                                    <Columns>
                                        <asp:BoundField DataField="codigo_ied" HeaderText="Código" />
                                        <asp:BoundField DataField="nombre_ied" HeaderText="Colegio" />
                                        <asp:BoundField DataField="Nivel_ied" HeaderText="Nivel" />
                                        <asp:BoundField DataField="nombre_Dep" HeaderText="Departamento" />
                                        <asp:BoundField DataField="nombre_Pro" HeaderText="Provincia" />
                                        <asp:BoundField DataField="nombre_Dis" HeaderText="Distrito" />
                                        <asp:BoundField DataField="Direccion_ied" HeaderText="Dirección" />
                                        <asp:CommandField ShowSelectButton="True" />
                                    </Columns>
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <EmptyDataTemplate>
                                        <b>No se encontraron items con el término de búsqueda</b>
                                    </EmptyDataTemplate>
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <EditRowStyle BackColor="#2461BF" />
                                    <AlternatingRowStyle BackColor="White" />
                                </asp:GridView>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:20%">
                            Promoción</td>
                        <td>
                            <asp:DropDownList ID="dpPromocion" runat="server" SkinID="ComboObligatorio">
                            </asp:DropDownList>
                            <asp:CompareValidator ID="CompareValidator13" runat="server" 
                                ControlToValidate="dpPromocion" ErrorMessage="Seleccione el año de Promoción" 
                                Operator="NotEqual" ValueToCompare="-1">*</asp:CompareValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:20%">
                            &nbsp;</td>
                        <td>
                            <asp:CheckBox ID="chkCentroAplicacion" runat="server" 
                                Text="Viene de Colegio de Aplicación" />
                        </td>
                    </tr>
                    <tr>
                        <td class="usatTablaInfo" colspan="2">
                            Categoria Por Colegio y Escuela Profesional(Seleccione Escuela Profesional y Colegio) - No incluye cursos complementarios ni Idiomas</td>
                    </tr>
                    <tr>
                        <td  colspan="2">
                            <div id="divCategoria" runat=server></div></td>
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
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                                ShowMessageBox="True" ShowSummary="False" />
                        </td>
                        <td>
                            <asp:Button ID="cmdGuardar" runat="server" Enabled="False" 
                                Text="Guardar" SkinID="BotonGuardar" style="height: 26px" />
                            &nbsp;<asp:Button ID="cmdLimpiar" runat="server" SkinID="BotonLimpiar" 
                                Text="Limpiar" ValidationGroup="Limpiar" />
                            &nbsp;<asp:Button ID="cmdCancelar" runat="server" SkinID="BotonSalir" 
                                Text="Cerrar" ValidationGroup="Salir" 
                                EnableTheming="True" />
                            <asp:HiddenField ID="hdcodigo_cco" 
                                runat="server" Value="0" />
                            <asp:HiddenField ID="hdgestionanotas" runat="server" Value="0" />
                            <asp:HiddenField ID="hdcodigo_pso" runat="server" Value="0" />
                            <asp:HiddenField ID="hdcodigo_cpf" runat="server" Value="0" />
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
