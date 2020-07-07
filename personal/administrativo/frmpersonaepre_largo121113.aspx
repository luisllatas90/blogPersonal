<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmpersonaepre_largo.aspx.vb" Inherits="administrativo_frmpersonaepre_largo" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1">
    <title>Registrar Persona</title>
	<meta name="http-equiv" content="Content-type: text/html; charset=UTF-8"/>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="JavaScript" src="../../private/funciones.js"></script>
    <script type="text/javascript" language="JavaScript" src="../../private/jq/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" language="javascript" src="../../private/jq/jquery.mascara.js"></script> 
    <script type="text/javascript" language="JavaScript" src="../../private/jq/lbox/thickbox.js"></script>
	<script type="text/javascript" language="JavaScript" src="jsinscripcion.js"></script>
	<link rel="stylesheet" href="../../private/jq/lbox/thickbox.css" type="text/css" media="screen" />
	<link rel="stylesheet" type="text/css" href="css/ui-lightness/jquery-ui-1.8.2.custom.css" />  
	<script src="jspostulantes.js" type="text/javascript"></script>    
	<!----------- Para DropDowList con CheckBoxs !--------------------->
	<link href="../css/MyStyles.css" rel="stylesheet" type="text/css" />
	<!----------------------------------------------------------------->
		
	<style type="text/css">
			#demoWrapper {
				padding : 1em;
				width : 100%;
				border-style: solid;
			}

			#fieldWrapper {
			}

			#demoNavigation {
				margin-top : 0.5em;
				margin-right : 1em;
				text-align: right;
			}
			
			#data {
				font-size : 0.7em;
			}

			input {
				margin-right: 0.1em;
				margin-bottom: 0.5em;
			}

			.input_field_25em {
				width: 2.5em;
			}

			.input_field_3em {
				width: 3em;
			}

			.input_field_35em {
				width: 3.5em;
			}

			.input_field_12em {
				width: 12em;
			}

			label {
				margin-bottom: 0.2em;
				font-weight: bold;
				font-size: 0.8em;
				width: 110px
			}

			label.error {
				color: red;
				font-size: 0.8em;
				margin-left : 0.5em;
			}

			.step span {
				float: right;
				font-weight: bold;
				padding-right: 0.8em;				
			}

			.navigation_button {
				width : 70px;
			}
			
			#data {
					overflow : auto;
			}
			
			.deshabilitado
			{				
				display:none
			}
	</style>
    
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
		
		function enviar() {
			document.getElementById("hide").value = 1;				
			document.getElementById("demoForm").submit();		
		}
		

	    $(document).ready(function() {
									
	        //comentado porque sale error: obj doesn' support this property or method
	        //$("#txtFechaNac").mask("99/99/9999"); 
	       
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
	<div id="demoWrapper">
		<span class="usatTitulo">Escuela Pre-Universitaria: Registro de postulantes</span>    			
		<hr />
		<h5 id="status"></h5>
		<form id="demoForm" method="post" action="" class="bbq" runat="server" accept-charset="utf-8">			
			<asp:ScriptManager ID="ScriptManager1" runat="server">
						</asp:ScriptManager>
			<div id="state"></div>
			<div id="fieldWrapper">			
				<span class="step" id="first">					
					<span class="font_normal_07em_black">Primer Paso - Datos Personales</span><br />										
					<label for="day_fi">Doc. de Identidad</label>					
					<asp:DropDownList ID="dpTipoDoc" runat="server" SkinID="ComboObligatorio" onblur="$('#hddpTipoDoc').val($('#dpTipoDoc').val());">
									<asp:ListItem>DNI</asp:ListItem>
									<asp:ListItem>CARNÉ DE EXTRANJERÍA</asp:ListItem>
									<asp:ListItem>PASAPORTE</asp:ListItem>
						</asp:DropDownList>
						<asp:HyperLink ID="lnkreniec0" runat="server" Font-Bold="False" 
									Font-Underline="True" ForeColor="Red" 
									NavigateUrl="http://ww4.essalud.gob.pe:7777/acredita/" 
									Target="_blank">[Obtener DNI de EsSalud]</asp:HyperLink><br />       
									
					<label for="day_fi">Nro. Doc. Identidad</label>					
						<asp:TextBox ID="txtdni" runat="server" CssClass="cajas" MaxLength="15" onblur="$('#hdtxtdni').val($('#txtdni').val());"
									SkinID="CajaTextoObligatorio"></asp:TextBox>
						<asp:LinkButton ID="lnkComprobarDNI" runat="server" Font-Bold="True"
									Font-Underline="True" ForeColor="Blue" ValidationGroup="Comprobar">[Buscar]</asp:LinkButton>
									<span id="errornrodoc" style="color:red"></span>                                
									<br />			 												
					<%-- *buscar	
					<asp:UpdatePanel ID="UpdatePanel1" runat="server">
						<ContentTemplate> --%>
											
					    <label for="firstname">Apellido paterno</label>		
						
						<asp:TextBox ID="txtAPaterno" name="txtAPaterno" runat="server" MaxLength="100" onblur="$('#hdtxtAPaterno').val($('#txtAPaterno').val());"
									Width="250px" SkinID="CajaTextoObligatorio" class="input_field_12em required"></asp:TextBox>
						
						<asp:RequiredFieldValidator 
												ID="RequiredFieldValidator2" 
												runat="server" 
												ControlToValidate="txtAPaterno" 
												ErrorMessage="Debe ingresar el Apellido Paterno"
						>*</asp:RequiredFieldValidator><br />
						
					<label for="surname">Apellido materno</label>					
						<asp:TextBox ID="txtAMaterno" runat="server" MaxLength="100" onblur="$('#hdtxtAMaterno').val($('#txtAMaterno').val());"
									Width="250px" SkinID="CajaTextoObligatorio" class="input_field_12em required"></asp:TextBox>
						<b><asp:RequiredFieldValidator 
												ID="RequiredFieldValidator3" 
												runat="server" 
												ControlToValidate="txtAMaterno" 
												ErrorMessage="Debe ingresar el Apellido Materno"
						>*</asp:RequiredFieldValidator></b><br />
						
					<label for="surname">Nombres</label>				   
					   <asp:TextBox ID="txtNombres" runat="server" MaxLength="80" onblur="$('#hdtxtNombres').val($('#txtNombres').val());"
						Width="250px" SkinID="CajaTextoObligatorio" class="input_field_12em required"></asp:TextBox>                            
															
						<asp:LinkButton ID="lnkComprobarNombres" runat="server" Font-Bold="True" 
						Font-Underline="True" ForeColor="Blue" ValidationGroup="Comprobar">Clic aquí para buscar coincidencias</asp:LinkButton>
						
						<asp:RequiredFieldValidator 
												ID="RequiredFieldValidator4" 
												runat="server" 
												ControlToValidate="txtNombres" 
												ErrorMessage="Debe ingresar los Nombres"
						 >*</asp:RequiredFieldValidator><br />
					
						 
					<table>
							<tr runat="server" id="trConcidencias">
							<td style="width:20%">
								</td>
							<td>
								<div id="listadiv" style="width:500px;height:200px">
									<%--*buscar
									<asp:UpdatePanel ID="UpdatePanel3" runat="server">
									<ContentTemplate> --%>
																	
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
								<%--*buscar
								</ContentTemplate>
										<Triggers>
											<asp:AsyncPostBackTrigger ControlID="lnkComprobarDNI" EventName="Click" />
										</Triggers>
									</asp:UpdatePanel> --%>
								</div>
							</td>
						</tr>                    
					</table><br />
					<label for="surname">Fecha Nac</label>	                    
						<asp:TextBox ID="txtFechaNac" runat="server" MaxLength="11" onblur="$('#hdtxtFechaNac').val($('#txtFechaNac').val());" 
								Width="130px" SkinID="CajaTextoObligatorio" class="input_field_12em date required"></asp:TextBox>					
						<b>
							<%--
							<asp:RequiredFieldValidator 
										ID="RequiredFieldValidator5" 
										runat="server" 
										ControlToValidate="txtFechaNac" 
										ErrorMessage="Debe ingresar la Fecha de Nac."
							>*</asp:RequiredFieldValidator></b>
							<asp:RangeValidator 
										ID="RangeValidator1" 
										runat="server" 
										ControlToValidate="txtFechaNac" 
										ErrorMessage="La fecha de Nacimiento es incorrecta" 
										MaximumValue="31/12/2050" 
										MinimumValue="01/01/1920" 
										Type="Date"
							>*</asp:RangeValidator>              --%>
						
					<label style="width:60px">Sexo</label> 
						<asp:DropDownList ID="dpSexo" runat="server" SkinID="ComboObligatorio" width="130px"
								onblur="$('#hddpSexo').val($('#dpSexo').val());" class="input_field_12em required">
								   <asp:ListItem Value="-1">&gt;&gt;Seleccione&lt;&lt;</asp:ListItem>
								   <asp:ListItem Value="M">Masculino</asp:ListItem>
								   <asp:ListItem Value="F">Femenino</asp:ListItem>
								   </asp:DropDownList>
								   <asp:CompareValidator 
										ID="CompareValidator3" 
										runat="server" 
										ControlToValidate="dpSexo" 
										ErrorMessage="Seleccione el sexo" 
										Operator="NotEqual" 
										ValueToCompare="-1"
						>*</asp:CompareValidator> 
						
					<label for="surname" style="width:60px">Estado Civil:</label> 
						<asp:DropDownList ID="dpEstadoCivil" runat="server" SkinID="ComboObligatorio" class="input_field_12em required"
						onblur="$('#hddpEstadoCivil').val($('#dpEstadoCivil').val());">						
									<asp:ListItem Value="-1">&gt;&gt;Seleccione&lt;&lt;</asp:ListItem>
									<asp:ListItem Value="SOLTERO">SOLTERO</asp:ListItem>
									<asp:ListItem Value="CASADO">CASADO</asp:ListItem>
									<asp:ListItem Value="VIUDO">VIUDO</asp:ListItem>
									<asp:ListItem Value="DIVORCIADO">DIVORCIADO</asp:ListItem>
									</asp:DropDownList>
									<asp:CompareValidator 
									ID="CompareValidator2" 
									runat="server" 
									ControlToValidate="dpEstadoCivil" 
									ErrorMessage="Seleccione el Estado Civil" 
									Operator="NotEqual" 
									ValueToCompare="-1"
						>*</asp:CompareValidator>
										
					<label style="width:40px;">País</label>
						<asp:DropDownList ID="dpPaisNacimiento" runat="server" width="130px" class="input_field_12em required" 
						onblur="$('#hddpPaisNacimiento').val($('#dpPaisNacimiento').val());" autopostback = "true">
						</asp:DropDownList><br /> 
											
					<label>Lugar de nacimiento</label><br /> 
					
					<asp:UpdatePanel ID="UpdatePanel15" runat="server">                                            
						<ContentTemplate>										
							<label>Departamento</label>                
							<asp:DropDownList ID="dpdepartamentonac" runat="server"	class="input_field_12em required"
											AutoPostBack="True" SkinID="ComboObligatorio" width="130px">
							</asp:DropDownList>
							
							<%-- <asp:CompareValidator ID="CompareValidator19" runat="server" 
											ControlToValidate="dpdepartamentonac" ErrorMessage="Seleccione el Departamento de nacimiento" 
											Operator="NotEqual" ValueToCompare="-1">*</asp:CompareValidator> --%>
							<label style="width:60px">Provincia</label>
							<asp:DropDownList ID="dpprovincianac" runat="server" class="input_field_12em required"
											AutoPostBack="True" SkinID="ComboObligatorio" width="130px">
							</asp:DropDownList>
							
							<%--<asp:CompareValidator ID="CompareValidator20" runat="server" 
											ControlToValidate="dpprovincianac" ErrorMessage="Seleccione la Provincia de nacimiento" 
											Operator="NotEqual" ValueToCompare="-1">*</asp:CompareValidator> --%>
											
							<label style="width:60px">Distrito</label>
							<asp:DropDownList ID="dpdistritonac" runat="server" onblur="$('#hddpdistritonac').val($('#dpdistritonac').val()); "								
											SkinID="ComboObligatorio" width="130px" class="input_field_12em required">
							</asp:DropDownList>
							<%--<asp:CompareValidator ID="CompareValidator21" runat="server" 
											ControlToValidate="dpdistritonac" ErrorMessage="Seleccione el Distrito de nacimiento" 
											Operator="NotEqual" ValueToCompare="-1">*</asp:CompareValidator><br />	--%>																
						</ContentTemplate>
						<Triggers>
							<asp:AsyncPostBackTrigger ControlID="dpPaisNacimiento" 
							 EventName="SelectedIndexChanged" />
						</Triggers>							
					</asp:UpdatePanel>
					<label for="surname">Email Principal</label> 					
						<asp:TextBox ID="txtemail1" runat="server" Width="328px" MaxLength="80" onblur="$('#hdtxtemail1').val($('#txtemail1').val());"
						class="input_field_12em email required">
						</asp:TextBox>
						<b><asp:RegularExpressionValidator 
									ID="RegularExpressionValidator3" 
									runat="server" 
									ControlToValidate="txtemail1" 
									ErrorMessage="Ingrese un Email principal válido." 
									ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*
							</asp:RegularExpressionValidator></b>
					<label for="surname" style="width:60px">Alternativo</label> 
						<asp:TextBox ID="txtemail2" runat="server" Width="307px" MaxLength="80" onblur="$('#hdtxtemail2').val($('#txtemail2').val());"
						style="margin-bottom: 0px"></asp:TextBox>                                                    
						<b><asp:RegularExpressionValidator 
									ID="RegularExpressionValidator4" 
									runat="server" 
									ControlToValidate="txtemail2" 
									ErrorMessage="Ingrese un Email secundario válido." 
									ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
						 >*</asp:RegularExpressionValidator></b><br />
						 
					<label for="surname">Dirección</label> 					 
						 <asp:TextBox ID="txtdireccion" runat="server" Width="60%" MaxLength="150" SkinID="CajaTextoObligatorio"
						 onblur="$('#hdtxtdireccion').val($('#txtdireccion').val());" class="input_field_12em required">
						 </asp:TextBox><br />
						 <b><asp:RequiredFieldValidator 
									ID="RequiredFieldValidator9" 
									runat="server" 
									ControlToValidate="txtdireccion" 
									ErrorMessage="Debe ingresar la dirección de la persona"
							>*</asp:RequiredFieldValidator></b>
						
					<asp:UpdatePanel ID="UpdatePanel16" runat="server">                                            
						<ContentTemplate>	 
							<label for="surname">Departamento</label> 					 
								 <asp:DropDownList ID="dpdepartamento" runat="server" class="input_field_12em required"
									AutoPostBack="True" SkinID="ComboObligatorio" width="130px">
								 </asp:DropDownList>
								 
								 <asp:CompareValidator 
											ID="CompareValidator4" 
											runat="server" 
											ControlToValidate="dpdepartamento" 
											ErrorMessage="Seleccione el Departamento" 
											Operator="NotEqual" 
											ValueToCompare="-1"
								 >*</asp:CompareValidator>
							<label for="surname" style="width:60px">Provincia</label> 					 					 					
								 <asp:DropDownList ID="dpprovincia" runat="server" class="input_field_12em required"
									AutoPostBack="True" SkinID="ComboObligatorio" width="130px">
								 </asp:DropDownList>
								 
								 <asp:CompareValidator 
											ID="CompareValidator5" 
											runat="server" 
											ControlToValidate="dpprovincia" 
											ErrorMessage="Seleccione la provincia"                     
											Operator="NotEqual" 
											ValueToCompare="-1"
								 >*</asp:CompareValidator> 
													 
							<label for="surname" style="width:60px">Distrito</label> 
								 <asp:DropDownList ID="dpdistrito" runat="server" SkinID="ComboObligatorio" width="130px" 
								 onblur="$('#hddpdistrito').val($('#dpdistrito').val());" class="input_field_12em required">						 
								 </asp:DropDownList>
								 
								 <asp:CompareValidator 
											ID="CompareValidator6" 
											runat="server" 
											ControlToValidate="dpdistrito" 
											ErrorMessage="Seleccione el Distrito" 
											Operator="NotEqual" 
											ValueToCompare="-1"
								 >*</asp:CompareValidator><br />
						</ContentTemplate>		
						<Triggers>
							<asp:AsyncPostBackTrigger ControlID="dpdepartamento" 
							 EventName="SelectedIndexChanged" />
							</Triggers>							
							</asp:UpdatePanel>
						 
					<label for="surname">Teléfono Fijo</label> 					 
						 <asp:TextBox ID="txttelefono" runat="server" MaxLength="20" width="130px" class="input_field_12em digits"
						 onblur="$('#hdtxttelefono').val($('#txttelefono').val());"></asp:TextBox>						 
					<label for="surname" style="width:60px">Celular</label>
						 <asp:TextBox ID="txtcelular" runat="server" MaxLength="20" width="130px" class="input_field_12em digits required"
						 onblur="$('#hdtxtcelular').val($('#txtcelular').val());"></asp:TextBox>
						 
					<label for="surname" style="width:60px">Operador</label>
						 <asp:DropDownList ID="dpOperador" runat="server" width="130px" onblur="$('#hddpOperador').val($('#dpOperador').val());"
						 class="input_field_12em required">
									<asp:ListItem>--Seleccione--</asp:ListItem>
									<asp:ListItem>Movistar</asp:ListItem>
									<asp:ListItem>Claro</asp:ListItem>
									<asp:ListItem>Nextel</asp:ListItem>
						 </asp:DropDownList><br />
						
					<label for="surname">Tipo Discapacidad</label> 																					
							<asp:CheckBox ID="chkDisAuditiva" runat="server" Text="Auditiva" onclick="PasarCheck()" />
							<asp:CheckBox ID="chkDisFisica" runat="server" Text="Física" onclick="PasarCheck()" />
							<asp:CheckBox ID="chkDisVisual" runat="server" Text="Visual" onclick="PasarCheck()" />
										
						<%--*buscar
						 </ContentTemplate><Triggers>
								<asp:AsyncPostBackTrigger ControlID="lnkComprobarDNI" EventName="Click" />
							</Triggers>
						</asp:UpdatePanel> --%>
							<br />	
				</span>
				<span id="finland" class="step">					
					<span class="font_normal_07em_black">Paso 2 - Datos de Postulación</span><br />
					<label for="firstname">Ciclo de Ingreso</label>
						<asp:DropDownList ID="dpCicloIng_alu" runat="server" SkinID="ComboObligatorio" class="input_field_12em required">
						</asp:DropDownList><br />
						<label for="surname">Escuela Profesional</label>
						<asp:DropDownList ID="dpCodigo_cpf" runat="server" SkinID="ComboObligatorio" class="input_field_12em required"
						></asp:DropDownList><br />                            
						
						<%--Comentado para el wizard mvillavicencio
						<asp:CompareValidator 
										ID="CompareValidator8"
										runat="server" 
										ControlToValidate="dpCodigo_cpf" 
										ErrorMessage="Seleccione la Escuela Profesional" 
										Operator="NotEqual" 
										ValueToCompare="-1"
						>*</asp:CompareValidator> --%>
						<label for="surname">Modalidad</label>
						<asp:DropDownList ID="dpModalidad" runat="server" SkinID="ComboObligatorio"
						class="input_field_12em required"></asp:DropDownList><br />                            
						<%--Comentado para el wizard mvillavicencio
						<asp:CompareValidator 
										ID="CompareValidator7" 
										runat="server" 
										ControlToValidate="dpModalidad" 
										ErrorMessage="Seleccione la Modalidad" 
										Operator="NotEqual" 
										ValueToCompare="-1"
						>*</asp:CompareValidator> --%>
						
						<label for="surname" style="float:left">Categoría</label>
						<!----------------------------- Para DropDowList con CheckBoxs !--------------------------------->
                            <div id="divCustomCheckBoxList2" runat="server" onmouseover="clearTimeout(timoutID);" onmouseout="timoutID = setTimeout('HideMList2()', 750);">
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
                            </div><br />
                            
                            <label for="surname" style="float:left">Beneficio</label>
                            <div id="divCustomCheckBoxList" runat="server" onmouseover="clearTimeout(timoutID);" onmouseout="timoutID = setTimeout('HideMList()', 750);">
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
                            
                            
				</span>
				<span id="x" class="step">
					<span class="font_normal_07em_black">Paso 3 - Estudios Secundarios</span><br />
					<label>País</label>
                    <asp:DropDownList ID="dpPaisColegio" runat="server" AutoPostBack="True" width="130px">					
                    </asp:DropDownList> <br /> 
					
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">                                             
                    <ContentTemplate>  
					<label>Dirección</label>
					<asp:TextBox ID="txtdireccioncolegio" runat="server" Width="60%" disabled = "True"
						 class="input_field_12em  AutoPostBack="True">
						 </asp:TextBox><br />                    					
                    <label>Departamento</label>    												
                            <asp:DropDownList ID="dpdepartamentocolegio" runat="server"
								AutoPostBack="True" SkinID="ComboObligatorio" width="130px">
                            </asp:DropDownList>
						
							<%--Comentado para el wizard mvillavicencio
                            <asp:CompareValidator ID="CompareValidator9" runat="server" 
                                ControlToValidate="dpdepartamentocolegio" ErrorMessage="Seleccione el Departamento del colegio" 
                                Operator="NotEqual" ValueToCompare="-1">*</asp:CompareValidator> --%>
					<label style="width:60px">Provincia</label>					
						
								<asp:DropDownList ID="dpprovinciacolegio" runat="server"
								AutoPostBack="True" SkinID="ComboObligatorio" width="130px">
								</asp:DropDownList>
						
						<%--Comentado para el wizard mvillavicencio
						<asp:CompareValidator ID="CompareValidator10" runat="server" 
							ControlToValidate="dpprovinciacolegio" ErrorMessage="Seleccione la provincia del colegio" 
						Operator="NotEqual" ValueToCompare="-1">*</asp:CompareValidator>
						--%>
							
					<label style="width:60px">Distrito</label>
										
							<asp:DropDownList ID="dpdistritocolegio" runat="server"
								SkinID="ComboObligatorio" AutoPostBack="True" width="130px">
							</asp:DropDownList>
						
							<%--Comentado para el wizard mvillavicencio
                            <asp:CompareValidator ID="CompareValidator11" runat="server" 
                                ControlToValidate="dpdistritocolegio" ErrorMessage="Seleccione el Distrito del Colegio" 
                                Operator="NotEqual" ValueToCompare="-1">*</asp:CompareValidator>
							--%>
					<br />
					<label>Colegio</label>
					<%--Comentado para el wizard mvillavicencio
                    <asp:CompareValidator ID="CompareValidator12" runat="server" 
                                ControlToValidate="dpCodigo_col" ErrorMessage="Seleccione el Colegio" 
                                Operator="NotEqual" ValueToCompare="-1">*</asp:CompareValidator>
					--%>							
							
					<asp:DropDownList ID="dpCodigo_col" runat="server" SkinID="ComboObligatorio">
					</asp:DropDownList>
															
                    <asp:ImageButton ID="imgActualizarColegio" runat="server" 
                                ImageUrl="../../images/menus/refresh.png" 
                                ToolTip="Actualizar lista de colegios" ValidationGroup="Colegio" 
                                Visible="False" />
                    <asp:Label ID="lblAgregarColegio" runat="server" Font-Underline="True" 
                                ForeColor="Blue" Text="[Agregar]" Visible="False"></asp:Label>
					
					<asp:TextBox ID="txtColegio" runat="server" Width="139px"
                                ValidationGroup="BuscarColegio"></asp:TextBox><asp:ImageButton ID="ImgBuscarColegios" runat="server" 
                                ImageUrl="~/images/busca.gif" ValidationGroup="BuscarColegio" />
                                       
                    
					</ContentTemplate>
                    
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="dpPaisColegio" 
                            EventName="SelectedIndexChanged" />
                        </Triggers>                    
                    </asp:UpdatePanel>            
                                
                      <asp:Label ID="lblTextBusqueda" runat="server" Text="(clic aquí)"></asp:Label>
					<asp:LinkButton ID="lnkBusquedaAvanzada" runat="server" ForeColor="Blue" 
                                ValidationGroup="BuscarColegio">Busqueda Avanzada</asp:LinkButton>
								
					<asp:UpdatePanel ID="UpdatePanel61" runat="server">
						<ContentTemplate>						
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
                                        <b>No se encontraron items con el término de búsqueda</b></EmptyDataTemplate>
										<SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <EditRowStyle BackColor="#2461BF" />
                                    <AlternatingRowStyle BackColor="White" />
                                </asp:GridView>
                            </asp:Panel><br />
						</ContentTemplate>
						<Triggers>
							<asp:AsyncPostBackTrigger ControlID="lnkBusquedaAvanzada" EventName="Click" />
						</Triggers>
					</asp:UpdatePanel>           
							
                    <label>Promoción</label>
					<%--*buscar
					<asp:UpdatePanel ID="UpdatePanel116" runat="server">                                            
						<ContentTemplate> --%>
							<asp:DropDownList ID="dpPromocion" runat="server" SkinID="ComboObligatorio" autpostbacj="true">
									</asp:DropDownList>
						<%--*buscar
						</ContentTemplate>
						<Triggers>
							<asp:AsyncPostBackTrigger ControlID="lnkComprobarDNI" EventName="Click" />
						</Triggers>
					</asp:UpdatePanel> --%>
					<%--Comentado para el wizard mvillavicencio
					<asp:CompareValidator ID="CompareValidator13" runat="server" 
                                ControlToValidate="dpPromocion" ErrorMessage="Seleccione el año de Promoción" 
                                Operator="NotEqual" ValueToCompare="-1">*</asp:CompareValidator><br />
					--%>
					<br />
					<label></label>
					<label style="width:500px">
					<asp:CheckBox ID="chkCentroAplicacion" runat="server"/>
                    Viene de Colegio de Aplicación
					</label>
					
				</span>	
				<span id="y" class="step">				
					<span class="font_normal_07em_black">Paso 4 - Datos del padre</span><br />					
					                    					
					<label for="surname">Nombres</label>					   
				    <%--*buscar
					<asp:UpdatePanel ID="UpdatePanel100" runat="server">
						<ContentTemplate>				   --%>
						   <asp:TextBox ID="txtNombresPadre" runat="server" MaxLength="80" 
							Width="250px" SkinID="CajaTextoObligatorio"></asp:TextBox>   

							<%--Comentado para el wizard mvillavicencio
							<asp:RequiredFieldValidator 
													ID="RequiredFieldValidator24" 
													runat="server" 
													ControlToValidate="txtNombresPadre" 
													ErrorMessage="Debe ingresar los Nombres"
							 >*</asp:RequiredFieldValidator>
							 --%>
							<br />		
							 <label for="surname">Dirección</label> 					 
							 <asp:TextBox ID="txtdireccionPadre" runat="server" Width="340px" MaxLength="150" SkinID="CajaTextoObligatorio">							 
							 </asp:TextBox>
							 
							 <%--Comentado para el wizard mvillavicencio
							 <b><asp:RequiredFieldValidator 
										ID="RequiredFieldValidator25" 
										runat="server" 
										ControlToValidate="txtdireccionPadre" 
										ErrorMessage="Debe ingresar la dirección del Padre"
								>*</asp:RequiredFieldValidator></b>
							--%>
								
							<label for="surname" style="width:55px">Urbanización</label> 					 
							 <asp:TextBox ID="txturbanizacionPadre" runat="server" Width="300px" MaxLength="150" SkinID="CajaTextoObligatorio">							 
							 </asp:TextBox>
							 
							 <%--Comentado para el wizard mvillavicencio
							 <b><asp:RequiredFieldValidator 
										ID="RequiredFieldValidator35" 
										runat="server" 
										ControlToValidate="txturbanizacionPadre" 
										ErrorMessage="Debe ingresar la urbanización del Padre"
								>*</asp:RequiredFieldValidator></b>
							--%>
							<br />		
							<asp:UpdatePanel ID="UpdatePanel12" runat="server">                                            
								<ContentTemplate>
							 <label for="surname">Departamento</label> 					 
							 <asp:DropDownList ID="dpdepartamentoPadre" runat="server"  
								AutoPostBack="True" SkinID="ComboObligatorio" width="130px">
							 </asp:DropDownList>
							 
							 <%--Comentado para el wizard mvillavicencio
							 <asp:CompareValidator 
										ID="CompareValidator26" 
										runat="server" 
										ControlToValidate="dpdepartamentoPadre" 
										ErrorMessage="Seleccione el Departamento" 
										Operator="NotEqual" 
										ValueToCompare="-1"
							 >*</asp:CompareValidator>
							 --%>
							 
							 <label for="surname" style="width:60px">Provincia</label> 																														
								 <asp:DropDownList ID="dpprovinciaPadre" runat="server" 
									AutoPostBack="True" SkinID="ComboObligatorio" width="130px">
								 </asp:DropDownList>							 								
														
							
							<%--Comentado para el wizard mvillavicencio
								 <asp:CompareValidator 
											ID="CompareValidator27" 
											runat="server" 
											ControlToValidate="dpprovinciaPadre" 
											ErrorMessage="Seleccione la provincia"                     
											Operator="NotEqual" 
											ValueToCompare="-1"
								 >*</asp:CompareValidator> 
								 --%>
												 
							 <label for="surname" style="width:60px">Distrito</label> 
							
									 <asp:DropDownList ID="dpdistritoPadre" runat="server" SkinID="ComboObligatorio" width="130px">									
									 </asp:DropDownList>									 									 
								</ContentTemplate>	
									<Triggers>
										<asp:AsyncPostBackTrigger ControlID="dpdepartamentoPadre" 
										 EventName="SelectedIndexChanged" />
									</Triggers>							
							</asp:UpdatePanel>
							<%--Comentado para el wizard mvillavicencio
									 <asp:CompareValidator 
												ID="CompareValidator28" 
												runat="server" 
												ControlToValidate="dpdistritoPadre" 
												ErrorMessage="Seleccione el Distrito" 
												Operator="NotEqual" 
												ValueToCompare="-1"
									 >*</asp:CompareValidator>
									 --%>
							 <br />
							 <label for="surname">Teléfono Fijo</label> 					 
							 <asp:TextBox ID="txttelefonoPadre" runat="server" MaxLength="20" width="130px" class="input_field_12em digits">
							 </asp:TextBox>
							 <label for="surname" style="width:60px">Oficina</label> 					 
							 <asp:TextBox ID="txttelefonooficinaPadre" runat="server" MaxLength="20" width="130px" class="input_field_12em digits">
							 </asp:TextBox>
							 <label for="surname" style="width:60px">Celular</label>
							 <asp:TextBox ID="txtcelularPadre" runat="server" MaxLength="20" width="130px" class="input_field_12em digits">
							 </asp:TextBox>
							 <label for="surname" style="width:60px">Operador</label>
							 <asp:DropDownList ID="dpOperadorPadre" runat="server" width="110px">
										<asp:ListItem>--Seleccione--</asp:ListItem>
										<asp:ListItem>Movistar</asp:ListItem>
										<asp:ListItem>Claro</asp:ListItem>
										<asp:ListItem>Nextel</asp:ListItem>
							 </asp:DropDownList><br />
							 
							 <label for="surname">Email</label> 					
							<asp:TextBox ID="txtemailPadre" runat="server" Width="280px" MaxLength="80" class="input_field_12em email">
							</asp:TextBox>
						<%--*buscar
						</ContentTemplate>
						<Triggers>
							<asp:AsyncPostBackTrigger ControlID="lnkComprobarDNI" EventName="Click" />
						</Triggers>
					</asp:UpdatePanel> --%>
					<%--Comentado para el wizard mvillavicencio
							<b><asp:RegularExpressionValidator 
										ID="RegularExpressionValidator29" 
										runat="server" 
										ControlToValidate="txtemailPadre" 
										ErrorMessage="Ingrese un Email principal válido." 
										ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*
								</asp:RegularExpressionValidator></b>
							--%>
				</span>
				<span id="confirmation" class="step submit_step">
					<span class="font_normal_07em_black">Último paso - Datos del apoderado</span><br />									
					
                   <label for="surname">Nombres</label>	
					<%--*buscar
				   <asp:UpdatePanel ID="UpdatePanel101" runat="server">
						<ContentTemplate> --%>
						   <asp:TextBox ID="txtNombresApoderado" runat="server" MaxLength="80"
							Width="250px" SkinID="CajaTextoObligatorio"></asp:TextBox>         
							
							<%--Comentado para el wizard mvillavicencio
							<asp:RequiredFieldValidator 
													ID="RequiredFieldValidator32" 
													runat="server" 
													ControlToValidate="txtNombresApoderado" 
													ErrorMessage="Debe ingresar los Nombres"
							 >*</asp:RequiredFieldValidator>
							 --%>
							<br />								
							 <label for="surname">Dirección</label> 					 
							 <asp:TextBox ID="txtdireccionApoderado" runat="server" Width="340px" MaxLength="150" SkinID="CajaTextoObligatorio">							 
							 </asp:TextBox>
							 
							 <%--Comentado para el wizard mvillavicencio
							 <b><asp:RequiredFieldValidator 
										ID="RequiredFieldValidator33" 
										runat="server" 
										ControlToValidate="txtdireccionApoderado" 
										ErrorMessage="Debe ingresar la dirección del Apoderado"
								>*</asp:RequiredFieldValidator></b>
							--%>
								
							<label for="surname" style="width:55px">Urbanización</label> 					 
							 <asp:TextBox ID="txturbanizacionApoderado" runat="server" Width="300px" MaxLength="150" SkinID="CajaTextoObligatorio">							 
							 </asp:TextBox>
							 <%--Comentado para el wizard mvillavicencio
							 <b><asp:RequiredFieldValidator 
										ID="RequiredFieldValidator36" 
										runat="server" 
										ControlToValidate="txturbanizacionApoderado" 
										ErrorMessage="Debe ingresar la urbanización del Apoderado"
								>*</asp:RequiredFieldValidator></b>
							--%>
							<br />			

							<asp:UpdatePanel ID="UpdatePanel14" runat="server">                                            
							<ContentTemplate>
								 <label for="surname">Departamento</label> 					 
								 <asp:DropDownList ID="dpdepartamentoApoderado" runat="server"  
									AutoPostBack="True" SkinID="ComboObligatorio" width="130px">
								 </asp:DropDownList>
								 
								 <%--Comentado para el wizard mvillavicencio
								 <asp:CompareValidator 
											ID="CompareValidator34" 
											runat="server" 
											ControlToValidate="dpdepartamentoApoderado" 
											ErrorMessage="Seleccione el Departamento" 
											Operator="NotEqual" 
											ValueToCompare="-1"
								 >*</asp:CompareValidator>
								 --%>
								 
								<label for="surname" style="width:60px">Provincia</label> 																														
								 <asp:DropDownList ID="dpprovinciaApoderado" runat="server" 
									AutoPostBack="True" SkinID="ComboObligatorio" width="130px">
								 </asp:DropDownList>							 															
							
								<%--Comentado para el wizard mvillavicencio
									 <asp:CompareValidator 
												ID="CompareValidator35" 
												runat="server" 
												ControlToValidate="dpprovinciaApoderado" 
												ErrorMessage="Seleccione la provincia"                     
												Operator="NotEqual" 
												ValueToCompare="-1"
									 >*</asp:CompareValidator> 
									 --%>
											 
								<label for="surname" style="width:60px">Distrito</label> 
								<asp:DropDownList ID="dpdistritoApoderado" runat="server" SkinID="ComboObligatorio" width="130px">									
								</asp:DropDownList>									 									 
								
								<%--Comentado para el wizard mvillavicencio
										 <asp:CompareValidator 
													ID="CompareValidator36" 
													runat="server" 
													ControlToValidate="dpdistritoApoderado" 
													ErrorMessage="Seleccione el Distrito" 
													Operator="NotEqual" 
													ValueToCompare="-1"
										 >*</asp:CompareValidator>
										 --%>							
							</ContentTemplate>	
							<Triggers>
							<asp:AsyncPostBackTrigger ControlID="dpdepartamentoApoderado" 
							 EventName="SelectedIndexChanged" />
							</Triggers>							
							</asp:UpdatePanel>
							 
							 <label for="surname">Teléfono Fijo</label> 					 
							 <asp:TextBox ID="txttelefonoApoderado" runat="server" MaxLength="20" width="130px" class="input_field_12em digits">
							 </asp:TextBox>
							 <label for="surname" style="width:60px">Oficina</label> 					 
							 <asp:TextBox ID="txttelefonooficinaApoderado" runat="server" MaxLength="20" width="130px" class="input_field_12em digits">
							 </asp:TextBox>
							 <label for="surname" style="width:60px">Celular</label>
							 <asp:TextBox ID="txtcelularApoderado" runat="server" MaxLength="20" width="130px" class="input_field_12em digits">
							 </asp:TextBox>
							 <label for="surname" style="width:60px">Operador</label>
							 <asp:DropDownList ID="dpOperadorApoderado" runat="server" width="110px">
										<asp:ListItem>--Seleccione--</asp:ListItem>
										<asp:ListItem>Movistar</asp:ListItem>
										<asp:ListItem>Claro</asp:ListItem>
										<asp:ListItem>Nextel</asp:ListItem>
							 </asp:DropDownList><br />
							 
							 <label for="email">Email</label> 					
							<asp:TextBox ID="txtemailApoderado" runat="server" Width="280px" MaxLength="80" class="input_field_12em email">
							</asp:TextBox>
							
							<%--Comentado para el wizard mvillavicencio
							<b><asp:RegularExpressionValidator 
										ID="RegularExpressionValidator37" 
										runat="server" 
										ControlToValidate="txtemailApoderado" 
										ErrorMessage="Ingrese un Email principal válido." 
										ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*
								</asp:RegularExpressionValidator></b><br />
							--%>
							
							<label>Observaciones</label>
							<asp:TextBox ID="txtObservaciones" runat="server" Width="715px" MaxLength="80">
							</asp:TextBox>
						<%--
						</ContentTemplate>
						<Triggers>
							<asp:AsyncPostBackTrigger ControlID="lnkComprobarDNI" EventName="Click" />
						</Triggers>
					</asp:UpdatePanel>
					--%>
				</span>				
			
				<span class="step" id="details">
					<span class="font_normal_07em_black">Hidden step</span><br />
					<span>This step is not possible to see without using the show method</span>
				</span>
				</div>
				
				<div id="demoNavigation" runat="server"> 							
					<input class="navigation_button" id="back" value="Reset" type="reset" />
					<input class="navigation_button" id="next" value="Submit" type="submit" runat="server" onclick="return next_onclick()" />						
				</div>																
				
			<table style="width: 100%">   
				<tr>
                <td>
					<%--*buscar
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate> --%>
                            <asp:Label ID="lblmensaje" runat="server" CssClass="rojo" Font-Bold="True"></asp:Label>
                        <%--
						</ContentTemplate>                        
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="lnkComprobarDNI" EventName="Click"/>
                        </Triggers>                        
                    </asp:UpdatePanel> --%>
                                
					</td></tr><tr>
					
					
                <td style="width:20%">
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                    ShowMessageBox="True" ShowSummary="False" />
                </td>
                <td>															
					<input type="button" onclick="Limpiar()" Value="Limpiar" id="btnLimpiar" runat="server">
					<input type="button" onclick="Salir()" Value="Salir" id="btnCancelar" runat="server">
                    
					<%--
					<asp:Button ID="cmdLimpiar" runat="server" SkinID="BotonLimpiar" 
                                        Text="Limpiar" ValidationGroup="Limpiar" /> --%>

					<%--
                    <asp:Button ID="cmdCancelar" runat="server" SkinID="BotonSalir" 
                                        Text="Cerrar" ValidationGroup="Salir"
                                        EnableTheming="True" /> --%>
										
                    <asp:HiddenField ID="hdcodigo_cco" runat="server" Value="0" />                                   
					<asp:HiddenField ID="hdgestionanotas" runat="server" Value="0" />                                    																		
                    <asp:HiddenField ID="hdcodigo_cpf" runat="server" Value="0" />									
					<asp:HiddenField ID="hdurlaccion" runat="server" Value="0" />
					<asp:HiddenField ID="hdurlpso" runat="server" Value="0" />
					<asp:HiddenField ID="hdurlid" runat="server" Value="0" />
					<asp:HiddenField ID="hdurcodigo_test" runat="server" Value="0" />
					<asp:HiddenField ID="hdurlcco" runat="server" Value="0" />
					<asp:HiddenField ID="hdurltcl" runat="server" Value="0" />
					<asp:HiddenField ID="hdurlcli" runat="server" Value="0" />
					<asp:HiddenField ID="hdurlctf" runat="server" Value="0" />
									
									<%-- *Buscar
									<asp:UpdatePanel ID="UpdatePanel115" runat="server">                                            
										<ContentTemplate>       --%>                                  
										<asp:HiddenField ID="hdcodigo_pso" runat="server" Value="0" />
										<asp:HiddenField ID="hdtxtAPaterno" runat="server" Value="0" />									
										<asp:HiddenField ID="hdtxtAMaterno" runat="server" Value="0" />
										<asp:HiddenField ID="hdtxtNombres" runat="server" Value="0" />
										<asp:HiddenField ID="hdtxtFechaNac" runat="server" Value="0" />
										<asp:HiddenField ID="hddpSexo" runat="server" Value="0" />
										<asp:HiddenField ID="hddpTipoDoc" runat="server" Value="0" />
										<asp:HiddenField ID="hdtxtdni" runat="server" Value="0" />
										<asp:HiddenField ID="hdtxtemail1" runat="server" Value="0" />
										<asp:HiddenField ID="hdtxtemail2" runat="server" Value="0" />
										<asp:HiddenField ID="hdtxtdireccion" runat="server" Value="0" />
										<asp:HiddenField ID="hddpdistrito" runat="server" Value="0" />
										<asp:HiddenField ID="hdtxttelefono" runat="server" Value="0" />
										<asp:HiddenField ID="hdtxtcelular" runat="server" Value="0" />
										<asp:HiddenField ID="hddpEstadoCivil" runat="server" Value="0" />
										<asp:HiddenField ID="hddpPaisNacimiento" runat="server" Value="0" />																			
										<asp:HiddenField ID="hddpdistritonac" runat="server" Value="0" />																			
										<asp:HiddenField ID="hddpOperador" runat="server"/>
										<asp:HiddenField ID="hdcodigobd" runat="server" Visible="true"/>																												
										<asp:HiddenField ID="hdPaso" runat="server" Value="1" />
										<asp:HiddenField ID="hdchkDisAuditiva" runat="server" Value="0" />
										<asp:HiddenField ID="hdchkDisFisica" runat="server" Value="0" />
										<asp:HiddenField ID="hdchkDisVisual" runat="server" Value="0" />
										<asp:HiddenField ID="hdcodigopso" runat="server" Visible="true"/>	

									 <%--
									 </ContentTemplate>
									 <Triggers>
										<asp:AsyncPostBackTrigger ControlID="lnkComprobarDNI" EventName="Click" />
									</Triggers>
								</asp:UpdatePanel> --%>
                                </td>
                            </tr>
                            <tr>
                                <td style="width:20%">
                                    </td><td>
                                    </td></tr><tr>
                                <td style="width:20%">
                                    </td><td>
                                    
									<asp:GridView ID="grwDeudas" runat="server" AutoGenerateColumns="False" 
											Visible="False" SkinID="skinGridViewLineasIntercalado">
											<Columns>
												<asp:BoundField DataField="descripcion_Sco" HeaderText="Servicio" />
												<asp:BoundField DataField="montoTotal_Deu" HeaderText="Deuda" />
												<asp:BoundField DataField="Pago_Deu" HeaderText="Pago" />
												<asp:BoundField DataField="Saldo_Deu" HeaderText="Saldo" />
											</Columns>
										</asp:GridView>
										
									<%--*Buscar
									<asp:UpdatePanel ID="UpdatePanel51" runat="server">
									<ContentTemplate> --%>
										
									<%--</ContentTemplate>
									<Triggers>
										<asp:AsyncPostBackTrigger ControlID="lnkComprobarDNI" EventName="Click" />
									</Triggers>
								</asp:UpdatePanel>--%>
                                </td>
                            </tr>
        </table>
		
		</form>
		<hr />
			
		<p id="data" style="display:none;"></p>
	</div>

    <script type="text/javascript" src="js/jquery-1.4.2.min.js"></script>		
    <script type="text/javascript" src="js/jquery.form.js"></script>
    <script type="text/javascript" src="js/jquery.validate.js"></script>
    <script type="text/javascript" src="js/bbq.js"></script>
    <script type="text/javascript" src="js/jquery-ui-1.8.5.custom.min.js"></script>
    <script type="text/javascript" src="js/jquery.form.wizard.js"></script>
    
    <script type="text/javascript">
        $(function() {
            $("#demoForm").formwizard({
                formPluginEnabled: true,
                validationEnabled: true,
                focusFirstInput: true,				
				textNext : 'Siguiente',
				textBack : 'Anterior',				
                formOptions: {
                    //success: function(data) { $("#status").fadeTo(500, 1, function() { $(this).html("Se ha registrado correctamente.").fadeTo(5000, 0); }); IrGeneracionCargos() },
                    beforeSubmit: function(data) { $("#data").html("data sent 2 the server: " + $.param(data)); },
                    dataType: 'html',
                    resetForm: true,
					error : function(jqXHR, status, error) {
						alert('Disculpe, existió un problema'+error+' status:'+status+' jQXHR'+jqXHR);
					}					
                }
            }
		);

            var remoteAjax = {}; // empty options object

            /*$("#next").click(function() {				
                //Capturar id de span						
                //var id = $("#demoForm .step").attr("id");
                $("#demoForm .step").each(function(i){
                var titulo = $(this).attr("id");
                alert("Atributo title del enlace " + i + ": " + titulo);
                });
            });*/

            $("#back").click(function() {
                var paso = $('#hdPaso').val();
                var paso = parseInt(paso) - 1;
                $('#hdPaso').val(paso);

            });

            $("#demoForm .step").each(function() { // for each step in the wizard, add an option to the remoteAjax object...
                remoteAjax[$(this).attr("id")] = {
                    url: "store_in_database.asp", // the url which stores the stuff in db for each step
                    dataType: 'json',
                    beforeSubmit: function(data) { $("#data").html("data sent to the server: " + $.param(data)) },
                    data: $("#data").html(),
                    success: function(data) {
                        if (data > 0) { //data is either true or false (returned from store_in_database.html) simulating successful / failing store								                            						
							//alert(data);							
                            $("#data").append("    .... store done successfully");
                            //Grabar el codigo registrado (de persona, o alumno)
                            $("#hdcodigobd").val(data);							
							
							//Grabar en variable codigo_pso
							var paso = $('#hdPaso').val();
							if (paso == 1) //En el 1er paso se graba a la persona
							{
								$("#hdcodigopso").val(data);									
							}
							
							if (paso == 5) //En el ultimo paso, se redirige al formulario de generacion de cargos
							{
								IrGeneracionCargos();
							}				

                            //Modificar variable que guarda el Nº Paso del Wizard                            
                            paso = parseInt(paso) + 1;
                            $('#hdPaso').val(paso)							
                        } else {							
							if (data == 0) {
								alert("Ocurrió un error al registrar los datos del participante. Contáctese con desarrollosistemas@usat.edu.pe.");
                                $('#lblmensaje').val('Ocurrió un error al registrar los datos del participante. Contáctese con desarrollosistemas@usat.edu.pe.');								
							}
							if (data == -1) {
                                alert("No puede registrar participantes en este Programa, debido a que no se ha registrado un Plan de Estudios.");
                                $('#lblmensaje').val("No puede registrar participantes en este Programa, debido a que no se ha registrado un Plan de Estudios.");
								
								data = 0; //Se cambia el valor a 0, porque sino se pasa al paso siguiente.
                            }
							
							if (data == -2) {
                                alert("Debe ingresar el DNI");
                                $('#lblmensaje').val("Debe ingresar el DNI");
								data = 0; //Se cambia el valor a 0, porque sino se pasa al paso siguiente.
                            }	
							/*
							if (data == -3 && $('#dpTipoDoc').val() == "DNI") {
								alert("El número de DNI es incorrecto. Mínimo 8 caracteres"+data);
                                $('#lblmensaje').val("El número de DNI es incorrecto. Mínimo 8 caracteres");								
								data = 0; //Se cambia el valor a 0, porque sino se pasa al paso siguiente.
							}
							*/
							if (data == -3 && $('#dpTipoDoc').val() == "CARNÉ DE EXTRANJERÍA") {
								alert("El número de pasaporte es incorrecto. Mínimo 9 caracteres");
                                $('#lblmensaje').val("El número de pasaporte es incorrecto. Mínimo 9 caracteres");								
								data = 0; //Se cambia el valor a 0, porque sino se pasa al paso siguiente.
							}
														
							if (data != 0 && data != -1 && data != -2 && data != -3) {
                                alert("Debe completar los datos para poder continuar." + data);
                            }
                        }
                        return data; //return true to make the wizard move to the next step, false will cause the wizard to stay on the CV step (change this in store_in_database.html)
                    }
                };
            });

            $("#demoForm").formwizard("option", "remoteAjax", remoteAjax); // set the remoteAjax option for the wizard
        });
        function next_onclick() {

        }

    </script>
	</body>
</html>
