<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmperosnaepresa2.aspx.vb" Inherits="administrativo_frmperosnaepresa2" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registrar Persona</title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="JavaScript" src="../../private/funciones.js"></script>
    <script type="text/javascript" language="JavaScript" src="../../private/jq/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" language="javascript" src="../../private/jq/jquery.mascara.js"></script> 
    <script type="text/javascript" language="JavaScript" src="../../private/jq/lbox/thickbox.js"></script>
	<link rel="stylesheet" href="../../private/jq/lbox/thickbox.css" type="text/css" media="screen" />
	<link rel="stylesheet" type="text/css" href="css/ui-lightness/jquery-ui-1.8.2.custom.css" />  
	<script src="jspostulantes.js" type="text/javascript"></script>    
	
	
	<style type="text/css">
			#demoWrapper {
				padding : 1em;
				width : 915px;
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
				width:120px;
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
			<form id="demoForm" method="post" action="frmperosnaepresa2.aspx" class="bbq" runat="server">
				<asp:ScriptManager ID="ScriptManager1" runat="server">
						</asp:ScriptManager>
				<div id="fieldWrapper">
				<div class="step" id="first">
					<span class="font_normal_07em_black">Primer Paso - Datos de Postulación</span><br />
					<div class="input">
						<label for="firstname">Ciclo de Ingreso</label>
						<asp:DropDownList ID="dpCicloIng_alu" runat="server" SkinID="ComboObligatorio"></asp:DropDownList><br />
						<label for="surname">Escuela Profesional</label>
						<asp:DropDownList ID="dpCodigo_cpf" runat="server" SkinID="ComboObligatorio"></asp:DropDownList><br />                            
						<asp:CompareValidator 
										ID="CompareValidator8"
										runat="server" 
										ControlToValidate="dpCodigo_cpf" 
										ErrorMessage="Seleccione la Escuela Profesional" 
										Operator="NotEqual" 
										ValueToCompare="-1"
						>*</asp:CompareValidator>
						<label for="surname">Modalidad</label>
						<asp:DropDownList ID="dpModalidad" runat="server" SkinID="ComboObligatorio"></asp:DropDownList><br />                            
						<asp:CompareValidator 
										ID="CompareValidator7" 
										runat="server" 
										ControlToValidate="dpModalidad" 
										ErrorMessage="Seleccione la Modalidad" 
										Operator="NotEqual" 
										ValueToCompare="-1"
						>*</asp:CompareValidator>
						<label for="surname">Categoría</label>
						<asp:DropDownList ID="dpCategorias" runat="server" SkinID="ComboObligatorio"></asp:DropDownList><br />                            
						<asp:CompareValidator 
										ID="CompareValidator1" 
										runat="server" 
										ControlToValidate="dpCategorias" 
										ErrorMessage="Seleccione la Categoría" 
										Operator="NotEqual" 
										ValueToCompare="-1"
						>*</asp:CompareValidator>
					</div>
				</div>
				<div id="sweden" class="step">
					<span class="font_normal_07em_black">Paso 2 - Datos Personales</span><br />
					<div class="input">
						<label for="day_fi">Doc. de Identidad</label>					
						<asp:DropDownList ID="dpTipoDoc" runat="server" SkinID="ComboObligatorio">
									<asp:ListItem>DNI</asp:ListItem>
									<asp:ListItem>CARNÉ DE EXTRANJERÍA</asp:ListItem>
						</asp:DropDownList>
						<asp:HyperLink ID="lnkreniec0" runat="server" Font-Bold="False" 
									Font-Underline="True" ForeColor="Red" 
									NavigateUrl="http://ww4.essalud.gob.pe:7777/acredita/" 
									Target="_blank">[Obtener DNI de EsSalud]</asp:HyperLink><br />       
									
						<label for="day_fi">Nro. Doc. Identidad</label>					
						<asp:TextBox ID="txtdni" runat="server" CssClass="cajas" MaxLength="15" 
									SkinID="CajaTextoObligatorio"></asp:TextBox>
						<asp:LinkButton ID="lnkComprobarDNI" runat="server" Font-Bold="True" 
									Font-Underline="True" ForeColor="Blue" ValidationGroup="Comprobar">[Buscar]</asp:LinkButton>
									<span id="errornrodoc" style="color:red"></span>                                
									<br />															
						
						<asp:UpdatePanel ID="UpdatePanel1" runat="server">
						<ContentTemplate>
											
						<label for="firstname">Apellido paterno</label>					
						<asp:TextBox ID="txtAPaterno" runat="server" MaxLength="100" 
									Width="250px" SkinID="CajaTextoObligatorio"></asp:TextBox>
						<asp:RequiredFieldValidator 
												ID="RequiredFieldValidator2" 
												runat="server" 
												ControlToValidate="txtAPaterno" 
												ErrorMessage="Debe ingresar el Apellido Paterno"
						>*</asp:RequiredFieldValidator><br />
						
						<label for="surname">Apellido materno</label>					
						<asp:TextBox ID="txtAMaterno" runat="server" MaxLength="100" 
									Width="250px" SkinID="CajaTextoObligatorio"></asp:TextBox>
						<b><asp:RequiredFieldValidator 
												ID="RequiredFieldValidator3" 
												runat="server" 
												ControlToValidate="txtAMaterno" 
												ErrorMessage="Debe ingresar el Apellido Materno"
						>*</asp:RequiredFieldValidator></b><br />
						
					   <label for="surname">Nombres</label>				   
					   <asp:TextBox ID="txtNombres" runat="server" MaxLength="80" 
						Width="250px" SkinID="CajaTextoObligatorio"></asp:TextBox>                            
								
							<%-- <asp:UpdatePanel ID="UpdatePanel2" runat="server">
							<ContentTemplate>                                                --%>
								<asp:LinkButton ID="lnkComprobarNombres" runat="server" Font-Bold="True" 
									Font-Underline="True" ForeColor="Blue" ValidationGroup="Comprobar">Clic aquí para buscar coincidencias</asp:LinkButton>
							<%-- </ContentTemplate>
							</asp:UpdatePanel>--%>
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
									<asp:UpdatePanel ID="UpdatePanel3" runat="server">
									<ContentTemplate>
																	
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
								</ContentTemplate>
										<Triggers>
											<asp:AsyncPostBackTrigger ControlID="lnkComprobarDNI" EventName="Click" />
										</Triggers>
									</asp:UpdatePanel>
								</div>
							</td>
						</tr>                    
						</table><br />
						<label for="surname">Fecha Nac</label>	                    
						<asp:TextBox ID="txtFechaNac" runat="server" MaxLength="11" 
								Width="130px" SkinID="CajaTextoObligatorio"></asp:TextBox>					
						<b><asp:RequiredFieldValidator 
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
							>*</asp:RangeValidator>                         					
						
						<label style="width:60px">Sexo</label> 
								<asp:DropDownList ID="dpSexo" runat="server" SkinID="ComboObligatorio" width="130px">
									
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
						<asp:DropDownList ID="dpEstadoCivil" runat="server" SkinID="ComboObligatorio" >
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
						<asp:DropDownList ID="dpPaisNacimiento" runat="server" AutoPostBack="True" width="130px">
						</asp:DropDownList><br /> 
											
						<label>Lugar de nacimiento</label><br /> 
						
						<label>Departamento</label>                
								<asp:DropDownList ID="dpdepartamentonac" runat="server" 
									AutoPostBack="True" SkinID="ComboObligatorio" width="130px">
								</asp:DropDownList>
								<asp:CompareValidator ID="CompareValidator19" runat="server" 
									ControlToValidate="dpdepartamentonac" ErrorMessage="Seleccione el Departamento de nacimiento" 
									Operator="NotEqual" ValueToCompare="-1">*</asp:CompareValidator>
						<label style="width:60px">Provincia</label>
								<asp:DropDownList ID="dpprovincianac" runat="server" 
									AutoPostBack="True" SkinID="ComboObligatorio" width="130px">
								</asp:DropDownList>
								<asp:CompareValidator ID="CompareValidator20" runat="server" 
									ControlToValidate="dpprovincianac" ErrorMessage="Seleccione la Provincia de nacimiento" 
									Operator="NotEqual" ValueToCompare="-1">*</asp:CompareValidator>
						<label style="width:60px">Distrito</label>
								<asp:DropDownList ID="dpdistritonac" runat="server" 
									SkinID="ComboObligatorio" AutoPostBack="True" width="130px">
								</asp:DropDownList>
								<asp:CompareValidator ID="CompareValidator21" runat="server" 
									ControlToValidate="dpdistritonac" ErrorMessage="Seleccione el Distrito de nacimiento" 
									Operator="NotEqual" ValueToCompare="-1">*</asp:CompareValidator><br />
																						
						
						<label for="surname">Email Principal</label> 					
						<asp:TextBox ID="txtemail1" runat="server" Width="280px" MaxLength="80">
						</asp:TextBox>
						<b><asp:RegularExpressionValidator 
									ID="RegularExpressionValidator3" 
									runat="server" 
									ControlToValidate="txtemail1" 
									ErrorMessage="Ingrese un Email principal válido." 
									ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*
							</asp:RegularExpressionValidator></b>
						<label for="surname" style="width:60px">Alternativo</label> 
						<asp:TextBox ID="txtemail2" runat="server" Width="280px" MaxLength="80" 
						style="margin-bottom: 0px"></asp:TextBox>                                                    
						<b><asp:RegularExpressionValidator 
									ID="RegularExpressionValidator4" 
									runat="server" 
									ControlToValidate="txtemail2" 
									ErrorMessage="Ingrese un Email secundario válido." 
									ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
						 >*</asp:RegularExpressionValidator></b><br />
						 
						 <label for="surname">Dirección</label> 					 
						 <asp:TextBox ID="txtdireccion" runat="server" Width="60%" MaxLength="150" SkinID="CajaTextoObligatorio">
						 </asp:TextBox><br />
						 <b><asp:RequiredFieldValidator 
									ID="RequiredFieldValidator9" 
									runat="server" 
									ControlToValidate="txtdireccion" 
									ErrorMessage="Debe ingresar la dirección de la persona"
							>*</asp:RequiredFieldValidator></b>
						
						 
						 <label for="surname">Departamento</label> 					 
						 <asp:DropDownList ID="dpdepartamento" runat="server" 
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
						 <asp:DropDownList ID="dpprovincia" runat="server" 
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
						 <asp:DropDownList ID="dpdistrito" runat="server" SkinID="ComboObligatorio" width="130px">
						 </asp:DropDownList>
						 
						 <asp:CompareValidator 
									ID="CompareValidator6" 
									runat="server" 
									ControlToValidate="dpdistrito" 
									ErrorMessage="Seleccione el Distrito" 
									Operator="NotEqual" 
									ValueToCompare="-1"
						 >*</asp:CompareValidator><br />
						 
						 <label for="surname">Teléfono Fijo</label> 					 
						 <asp:TextBox ID="txttelefono" runat="server" MaxLength="20" width="130px"></asp:TextBox>
						 <label for="surname" style="width:60px">Celular</label>
						 <asp:TextBox ID="txtcelular" runat="server" MaxLength="20" width="130px"></asp:TextBox>
						 <label for="surname" style="width:60px">Operador</label>
						 <asp:DropDownList ID="dpOperador" runat="server" width="130px">
									<asp:ListItem>Movistar</asp:ListItem>
									<asp:ListItem>Claro</asp:ListItem>
									<asp:ListItem>Nextel</asp:ListItem>
						 </asp:DropDownList><br />
						
						<label for="surname">Tipo Discapacidad</label> 		
							<asp:CheckBoxList ID="chkDiscapacidad" runat="server" width="130px">
								<asp:ListItem>Auditiva</asp:ListItem>
								<asp:ListItem>Física</asp:ListItem>
								<asp:ListItem>Visual</asp:ListItem>
							</asp:CheckBoxList>                        				                                          
						 </ContentTemplate><Triggers>
								<asp:AsyncPostBackTrigger ControlID="lnkComprobarDNI" EventName="Click" />
							</Triggers>
						</asp:UpdatePanel>	<br />									
					</div>
				</div>
				<div id="finland" class="step">
					<span class="font_normal_07em_black">Estudios secundarios</span><br />
					<div class="input">
					<label>País</label>
                    <asp:DropDownList ID="dpPaisColegio" runat="server" AutoPostBack="True" width="130px">                                       
                    </asp:DropDownList>
                    
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                             
                     <ContentTemplate>
                                        
                    <label>Departamento</label>                
                            <asp:DropDownList ID="dpdepartamentocolegio" runat="server" 
								AutoPostBack="True" SkinID="ComboObligatorio" width="130px">
                            </asp:DropDownList>
                            <asp:CompareValidator ID="CompareValidator9" runat="server" 
                                ControlToValidate="dpdepartamentocolegio" ErrorMessage="Seleccione el Departamento del colegio" 
                                Operator="NotEqual" ValueToCompare="-1">*</asp:CompareValidator>
					<label style="width:60px">Provincia</label>					
						<asp:DropDownList ID="dpprovinciacolegio" runat="server" 
						AutoPostBack="True" SkinID="ComboObligatorio" width="130px">
						</asp:DropDownList>
						<asp:CompareValidator ID="CompareValidator10" runat="server" 
							ControlToValidate="dpprovinciacolegio" ErrorMessage="Seleccione la provincia del colegio" 
						Operator="NotEqual" ValueToCompare="-1">*</asp:CompareValidator>
							
					<label style="width:60px">Distrito</label>
                            <asp:DropDownList ID="dpdistritocolegio" runat="server" 
                                SkinID="ComboObligatorio" AutoPostBack="True" width="130px">
                            </asp:DropDownList>
                            <asp:CompareValidator ID="CompareValidator11" runat="server" 
                                ControlToValidate="dpdistritocolegio" ErrorMessage="Seleccione el Distrito del Colegio" 
                                Operator="NotEqual" ValueToCompare="-1">*</asp:CompareValidator><br />
					
					<label>Colegio</label>
                            <asp:CompareValidator ID="CompareValidator12" runat="server" 
                                ControlToValidate="dpCodigo_col" ErrorMessage="Seleccione el Colegio" 
                                Operator="NotEqual" ValueToCompare="-1">*</asp:CompareValidator>
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
							
                    <label>Promoción</label>
					<asp:DropDownList ID="dpPromocion" runat="server" SkinID="ComboObligatorio">
                            </asp:DropDownList>
					<asp:CompareValidator ID="CompareValidator13" runat="server" 
                                ControlToValidate="dpPromocion" ErrorMessage="Seleccione el año de Promoción" 
                                Operator="NotEqual" ValueToCompare="-1">*</asp:CompareValidator><br />
					<label></label>
					<asp:CheckBox ID="chkCentroAplicacion" runat="server" 
                                Text="Viene de Colegio de Aplicación" /> 	 												
					</div>
				</div>
				<div id="car" class="step">
					<span class="font_normal_07em_black">Datos del Padre</span><br />
					<div class="input">
					<label for="firstname">Apellido paterno</label>					
                    <asp:TextBox ID="txtAPaternoPadre" runat="server" MaxLength="100" 
                                Width="250px" SkinID="CajaTextoObligatorio"></asp:TextBox>
                    <asp:RequiredFieldValidator 
                                            ID="RequiredFieldValidator22" 
                                            runat="server" 
                                            ControlToValidate="txtAPaternoPadre" 
                                            ErrorMessage="Debe ingresar el Apellido Paterno del Padre"
                    >*</asp:RequiredFieldValidator><br />
					
					<label for="surname">Apellido materno</label>					
                    <asp:TextBox ID="txtAMaternoPadre" runat="server" MaxLength="100" 
                                Width="250px" SkinID="CajaTextoObligatorio"></asp:TextBox>
                    <b><asp:RequiredFieldValidator 
                                            ID="RequiredFieldValidator23" 
                                            runat="server" 
                                            ControlToValidate="txtAMaternoPadre" 
                                            ErrorMessage="Debe ingresar el Apellido Materno"
                    >*</asp:RequiredFieldValidator></b><br />
					
                   <label for="surname">Nombres</label>				   
                   <asp:TextBox ID="txtNombresPadre" runat="server" MaxLength="80" 
                    Width="250px" SkinID="CajaTextoObligatorio"></asp:TextBox>         
					<asp:RequiredFieldValidator 
					                        ID="RequiredFieldValidator24" 
					                        runat="server" 
                                            ControlToValidate="txtNombresPadre" 
                                            ErrorMessage="Debe ingresar los Nombres"
                     >*</asp:RequiredFieldValidator><br />
					 					 					
					 <label for="surname">Dirección</label> 					 
					 <asp:TextBox ID="txtdireccionPadre" runat="server" Width="340px" MaxLength="150" SkinID="CajaTextoObligatorio">
                     </asp:TextBox>
					 <b><asp:RequiredFieldValidator 
                                ID="RequiredFieldValidator25" 
                                runat="server" 
                                ControlToValidate="txtdireccionPadre" 
                                ErrorMessage="Debe ingresar la dirección del Padre"
                        >*</asp:RequiredFieldValidator></b>
						
					<label for="surname" style="width:55px">Urbanización</label> 					 
					 <asp:TextBox ID="txturbanizacionPadre" runat="server" Width="300px" MaxLength="150" SkinID="CajaTextoObligatorio">
                     </asp:TextBox>
					 <b><asp:RequiredFieldValidator 
                                ID="RequiredFieldValidator35" 
                                runat="server" 
                                ControlToValidate="txturbanizacionPadre" 
                                ErrorMessage="Debe ingresar la urbanización del Padre"
                        >*</asp:RequiredFieldValidator></b><br />
										 
					 <label for="surname">Departamento</label> 					 
					 <asp:DropDownList ID="dpdepartamentoPadre" runat="server" 
                        AutoPostBack="True" SkinID="ComboObligatorio" width="130px">
                     </asp:DropDownList>
                     
                     <asp:CompareValidator 
                                ID="CompareValidator26" 
                                runat="server" 
                                ControlToValidate="dpdepartamentoPadre" 
                                ErrorMessage="Seleccione el Departamento" 
                                Operator="NotEqual" 
                                ValueToCompare="-1"
                     >*</asp:CompareValidator>
					 <label for="surname" style="width:60px">Provincia</label> 	
																				
					<asp:UpdatePanel ID="UpdatePanel12" runat="server">                                            
						<ContentTemplate>
						 <asp:DropDownList ID="dpprovinciaPadre" runat="server" 
							AutoPostBack="True" SkinID="ComboObligatorio" width="130px">
						 </asp:DropDownList>
                     
						 <asp:CompareValidator 
									ID="CompareValidator27" 
									runat="server" 
									ControlToValidate="dpprovinciaPadre" 
									ErrorMessage="Seleccione la provincia"                     
									Operator="NotEqual" 
									ValueToCompare="-1"
						 >*</asp:CompareValidator> 
						</ContentTemplate>	
							<Triggers>
                                <asp:AsyncPostBackTrigger ControlID="dpdepartamentoPadre" 
                                 EventName="SelectedIndexChanged" />
                            </Triggers>							
					</asp:UpdatePanel>
                                         
					 <label for="surname" style="width:60px">Distrito</label> 
					 <asp:UpdatePanel ID="UpdatePanel13" runat="server">                                            
						<ContentTemplate>
							 <asp:DropDownList ID="dpdistritoPadre" runat="server" SkinID="ComboObligatorio" width="130px">
							 </asp:DropDownList>
							 
							 <asp:CompareValidator 
										ID="CompareValidator28" 
										runat="server" 
										ControlToValidate="dpdistritoPadre" 
										ErrorMessage="Seleccione el Distrito" 
										Operator="NotEqual" 
										ValueToCompare="-1"
							 >*</asp:CompareValidator><br />
						</ContentTemplate>	
							<Triggers>
                                <asp:AsyncPostBackTrigger ControlID="dpprovinciaPadre" 
                                 EventName="SelectedIndexChanged" />
                            </Triggers>							
					</asp:UpdatePanel>
					 
					 <label for="surname">Teléfono Fijo</label> 					 
					 <asp:TextBox ID="txttelefonoPadre" runat="server" MaxLength="20" width="130px"></asp:TextBox>
					 <label for="surname" style="width:60px">Oficina</label> 					 
					 <asp:TextBox ID="txttelefonooficinaPadre" runat="server" MaxLength="20" width="130px"></asp:TextBox>
					 <label for="surname" style="width:60px">Celular</label>
					 <asp:TextBox ID="txtcelularPadre" runat="server" MaxLength="20" width="130px"></asp:TextBox>
					 <label for="surname" style="width:60px">Operador</label>
					 <asp:DropDownList ID="dpOperadorPadre" runat="server" width="110px">
                                <asp:ListItem>Movistar</asp:ListItem>
                                <asp:ListItem>Claro</asp:ListItem>
								<asp:ListItem>Nextel</asp:ListItem>
                     </asp:DropDownList><br />
					 
					 <label for="surname">Email</label> 					
					<asp:TextBox ID="txtemailPadre" runat="server" Width="280px" MaxLength="80">
                    </asp:TextBox>
					<b><asp:RegularExpressionValidator 
                                ID="RegularExpressionValidator29" 
                                runat="server" 
                                ControlToValidate="txtemailPadre" 
                                ErrorMessage="Ingrese un Email principal válido." 
                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*
                        </asp:RegularExpressionValidator></b>
					</div>
				</div>
				<div id="confirmation" class="step">
					<span class="font_normal_07em_black">Último paso - Datos del Apoderado</span><br />
					<div class="input">
					<label for="firstname">Apellido paterno</label>					
                    <asp:TextBox ID="txtAPaternoApoderado" runat="server" MaxLength="100" 
                                Width="250px" SkinID="CajaTextoObligatorio"></asp:TextBox>
                    <asp:RequiredFieldValidator 
                                            ID="RequiredFieldValidator30" 
                                            runat="server" 
                                            ControlToValidate="txtAPaternoApoderado" 
                                            ErrorMessage="Debe ingresar el Apellido Paterno del Apoderado"
                    >*</asp:RequiredFieldValidator><br />
					
					<label for="surname">Apellido materno</label>					
                    <asp:TextBox ID="txtAMaternoApoderado" runat="server" MaxLength="100" 
                                Width="250px" SkinID="CajaTextoObligatorio"></asp:TextBox>
                    <b><asp:RequiredFieldValidator 
                                            ID="RequiredFieldValidator31" 
                                            runat="server" 
                                            ControlToValidate="txtAMaternoApoderado" 
                                            ErrorMessage="Debe ingresar el Apellido Materno"
                    >*</asp:RequiredFieldValidator></b><br />
					
                   <label for="surname">Nombres</label>				   
                   <asp:TextBox ID="txtNombresApoderado" runat="server" MaxLength="80" 
                    Width="250px" SkinID="CajaTextoObligatorio"></asp:TextBox>         
					<asp:RequiredFieldValidator 
					                        ID="RequiredFieldValidator32" 
					                        runat="server" 
                                            ControlToValidate="txtNombresApoderado" 
                                            ErrorMessage="Debe ingresar los Nombres"
                     >*</asp:RequiredFieldValidator><br />
					 					 					
					 <label for="surname">Dirección</label> 					 
					 <asp:TextBox ID="txtdireccionApoderado" runat="server" Width="340px" MaxLength="150" SkinID="CajaTextoObligatorio">
                     </asp:TextBox>
					 <b><asp:RequiredFieldValidator 
                                ID="RequiredFieldValidator33" 
                                runat="server" 
                                ControlToValidate="txtdireccionApoderado" 
                                ErrorMessage="Debe ingresar la dirección del Apoderado"
                        >*</asp:RequiredFieldValidator></b>
						
					<label for="surname" style="width:55px">Urbanización</label> 					 
					 <asp:TextBox ID="txturbanizacionApoderado" runat="server" Width="300px" MaxLength="150" SkinID="CajaTextoObligatorio">
                     </asp:TextBox>
					 <b><asp:RequiredFieldValidator 
                                ID="RequiredFieldValidator36" 
                                runat="server" 
                                ControlToValidate="txturbanizacionApoderado" 
                                ErrorMessage="Debe ingresar la urbanización del Apoderado"
                        >*</asp:RequiredFieldValidator></b><br />
										 
					 <label for="surname">Departamento</label> 					 
					 <asp:DropDownList ID="dpdepartamentoApoderado" runat="server" 
                        AutoPostBack="True" SkinID="ComboObligatorio" width="130px">
                     </asp:DropDownList>
                     
                     <asp:CompareValidator 
                                ID="CompareValidator34" 
                                runat="server" 
                                ControlToValidate="dpdepartamentoApoderado" 
                                ErrorMessage="Seleccione el Departamento" 
                                Operator="NotEqual" 
                                ValueToCompare="-1"
                     >*</asp:CompareValidator>
					 <label for="surname" style="width:60px">Provincia</label> 					 
					 
					 <asp:UpdatePanel ID="UpdatePanel14" runat="server">                                            
						<ContentTemplate>
						 <asp:DropDownList ID="dpprovinciaApoderado" runat="server" 
                        AutoPostBack="True" SkinID="ComboObligatorio" width="130px">
						 </asp:DropDownList>
						 
						 <asp:CompareValidator 
									ID="CompareValidator35" 
									runat="server" 
									ControlToValidate="dpprovinciaApoderado" 
									ErrorMessage="Seleccione la provincia"                     
									Operator="NotEqual" 
									ValueToCompare="-1"
						 >*</asp:CompareValidator> 
						</ContentTemplate>	
							<Triggers>
                                <asp:AsyncPostBackTrigger ControlID="dpdepartamentoApoderado" 
                                 EventName="SelectedIndexChanged" />
                            </Triggers>							
					</asp:UpdatePanel>
					
					 <asp:UpdatePanel ID="UpdatePanel15" runat="server">                                            
						<ContentTemplate>                                         
							 <label for="surname" style="width:60px">Distrito</label> 
							 <asp:DropDownList ID="dpdistritoApoderado" runat="server" SkinID="ComboObligatorio" width="130px">
							 </asp:DropDownList>
							 
							 <asp:CompareValidator 
										ID="CompareValidator36" 
										runat="server" 
										ControlToValidate="dpdistritoApoderado" 
										ErrorMessage="Seleccione el Distrito" 
										Operator="NotEqual" 
										ValueToCompare="-1"
							 >*</asp:CompareValidator><br />
						</ContentTemplate>	
							<Triggers>
                                <asp:AsyncPostBackTrigger ControlID="dpprovinciaApoderado" 
                                 EventName="SelectedIndexChanged" />
                            </Triggers>							
					</asp:UpdatePanel>
					 
					 <label for="surname">Teléfono Fijo</label> 					 
					 <asp:TextBox ID="txttelefonoApoderado" runat="server" MaxLength="20" width="130px"></asp:TextBox>
					 <label for="surname" style="width:60px">Oficina</label> 					 
					 <asp:TextBox ID="txttelefonooficinaApoderado" runat="server" MaxLength="20" width="130px"></asp:TextBox>
					 <label for="surname" style="width:60px">Celular</label>
					 <asp:TextBox ID="txtcelularApoderado" runat="server" MaxLength="20" width="130px"></asp:TextBox>
					 <label for="surname" style="width:60px">Operador</label>
					 <asp:DropDownList ID="dpOperadorApoderado" runat="server" width="110px">
                                <asp:ListItem>Movistar</asp:ListItem>
                                <asp:ListItem>Claro</asp:ListItem>
								<asp:ListItem>Nextel</asp:ListItem>
                     </asp:DropDownList><br />
					 
					 <label for="surname">Email</label> 					
					<asp:TextBox ID="txtemailApoderado" runat="server" Width="280px" MaxLength="80">
                    </asp:TextBox>
					<b><asp:RegularExpressionValidator 
                                ID="RegularExpressionValidator37" 
                                runat="server" 
                                ControlToValidate="txtemailApoderado" 
                                ErrorMessage="Ingrese un Email principal válido." 
                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*
                        </asp:RegularExpressionValidator></b><br />
					
					<label>Observaciones</label>
					<asp:TextBox ID="txtObservaciones" runat="server" Width="715px" MaxLength="80">
                    </asp:TextBox>
						<input type="hidden" class="link" value="summary" />
					</div>
				</div>
				<div id="summary" class="step">
					<span class="font_normal_07em_black">Resumen</span><br />
					<p>Por favor verifique la información.</p>
					<div id="summaryContainer"></div>
				</div>
				</div>
				<div id="demoNavigation"> 							
					<input class="navigation_button" id="back" value="Back" type="reset" />
					<input class="navigation_button" id="next" value="Next" type="submit" />
				</div>
				
				<table style="width: 100%">   
				<tr>
                <td>                
					<asp:Label ID="lblmensaje" runat="server" CssClass="rojo" Font-Bold="True"></asp:Label></td></tr><tr>
                <td style="width:20%">
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                    ShowMessageBox="True" ShowSummary="False" />
                </td>
                <td>
					<input id="hide" name="hide" value="0" />
					<input id="btntest" value="grabar" type="button" onclick="enviar();" /> 					
					<asp:LinkButton ID="lnkGrabar" runat="server" Font-Bold="True" 
									Font-Underline="True" ForeColor="Blue" ValidationGroup="Grabar">[Grabar]</asp:LinkButton>
                    <asp:Button ID="cmdGuardar" runat="server" Enabled="False" 
                    Text="Guardar" SkinID="BotonGuardar" style="height: 26px" />
                    <asp:Button ID="cmdLimpiar" runat="server" SkinID="BotonLimpiar" 
                                        Text="Limpiar" ValidationGroup="Limpiar" />
                                    <asp:Button ID="cmdCancelar" runat="server" SkinID="BotonSalir" 
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
                                </td>
                            </tr>
        </table>
			</form>
			<hr />
			
			<p id="data"></p>
		</div>

    <script type="text/javascript" src="js/jquery-1.4.2.min.js"></script>		
    <script type="text/javascript" src="js/jquery.form.js"></script>
    <script type="text/javascript" src="js/jquery.validate.js"></script>
    <script type="text/javascript" src="js/bbq.js"></script>
    <script type="text/javascript" src="js/jquery-ui-1.8.5.custom.min.js"></script>
    <script type="text/javascript" src="js/jquery.form.wizard.js"></script>
    
    <script type="text/javascript">

			var cache = {}; // caching inputs for the visited steps

			$("#demoForm").bind("step_shown", function(event,data){	
				if(data.isLastStep){ // if this is the last step...then
					$("#summaryContainer").empty(); // empty the container holding the 
					$.each(data.activatedSteps, function(i, id){ // for each of the activated steps...do
						if(id === "summary") return; // if it is the summary page then just return
						cache[id] = $("#" + id).find(".input"); // else, find the div:s with class="input" and cache them with a key equal to the current step id
						cache[id].detach().appendTo('#summaryContainer').show().find(":input").removeAttr("disabled"); // detach the cached inputs and append them to the summary container, also show and enable them
					});
				}else if(data.previousStep === "summary"){ // if we are movin back from the summary page
					$.each(cache, function(id, inputs){ // for each of the keys in the cache...do
						var i = inputs.detach().appendTo("#" + id).find(":input");  // put the input divs back into their normal step
						if(id === data.currentStep){ // (we are moving back from the summary page so...) if enable inputs on the current step
							 i.removeAttr("disabled");
						}else{ // disable the inputs on the rest of the steps
							i.attr("disabled","disabled");
						}
					});
					cache = {}; // empty the cache again
				}
			})

			$(function(){
				$("#demoForm").formwizard({ 
				 	formPluginEnabled: true,
				 	validationEnabled: true,
				 	focusFirstInput : true,
				 	formOptions :{
						success: function(data){$("#status").fadeTo(500,1,function(){ $(this).html("You are now registered!").fadeTo(5000, 0); })},
						beforeSubmit: function(data){$("#data").html("data sent to the server: " + $.param(data));},
						dataType: 'json',
						resetForm: true
				 	}	
				 }
				);
  		});
    </script>
	</body>
</html>
