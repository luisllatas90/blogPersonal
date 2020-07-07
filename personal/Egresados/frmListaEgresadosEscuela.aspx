<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmListaEgresadosEscuela.aspx.vb" Inherits="Egresado_frmListaEgresados" ValidateRequest="false" %>
<%@ Register assembly="BusyBoxDotNet" namespace="BusyBoxDotNet" tagprefix="busyboxdotnet" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />    
    <script type="text/javascript" language="JavaScript" src="../../private/funciones.js"></script>
    <script type="text/javascript" language="JavaScript" src="../../private/jq/jquery-1.4.2.min.js"></script> 
	<script type="text/javascript" language="JavaScript" src="../../private/jq/lbox/thickbox.js"></script>	
	
	<script type="text/javascript" src="scripts/jquery-1.3.2.js"></script>
    <script type="text/javascript" src="scripts/jHtmlArea-0.7.5.min.js"></script>
    <link rel="Stylesheet" type="text/css" href="style/jHtmlArea.css" />
    <script type="text/javascript" src="scripts/jHtmlArea.ColorPickerMenu-0.7.0.js"></script>
    <link rel="Stylesheet" type="text/css" href="style/jHtmlArea.ColorPickerMenu.css" />

    
    <script type="text/javascript">
        $(function() {
        $("#txtMensaje").htmlarea({
                toolbar: ["forecolor",
                        "|", "bold", "italic", "underline", "|", "p", "h1", "h2", "h3", "|", "link", "unlink", "|", "justifyLeft", "justifyCenter", "justifyRight", "|", "decreaseFontSize", "increaseFontSize", "|", "orderedlist", "unorderedlist", "|", "cut", "copy", "paste"]
            });
        });

    </script>
	
	<link rel="stylesheet" href="../../private/jq/lbox/thickbox.css" type="text/css" media="screen" />
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
	        } else {
	            obj.style.backgroundColor = "white"
	        }
	    }        
    </script>
    <style type="text/css">
        div.jHtmlArea { border: solid 1px #0099FF; }
        .style3
        {
            width: 100%;
            height: 146px;
        }
        #form1
        {
            height: 100%;
            width: 100%;
        }
        .style5
        {
            width: 295px;
        }
        .style6
        {
            width: 129px;
        }
        .style7
        {
        }
        .style8
        {
            width: 15%;
            height: 27px;
        }
        .style9
        {
            height: 27px;
        }
        .style10
        {
            width: 20%;
        }
        #txtMensaje
        {
            width: 481px;
        }
        .style11
        {
            width: 630px;
        }
        .style12
        {
            height: 27px;
            width: 630px;
        }
        .style15
        {
            width: 132px;
            height: 16px;
        }
        .style16
        {
            height: 16px;
            width: 630px;
        }
        .style18
        {
            width: 132px;
            height: 27px;
        }
        .style19
        {
            width: 132px;
        }
        .style20
        {
            width: 171px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
     
    <table width="100%">
        <tr>
            <td style="width: 100%; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #0099FF;" 
                height="40px" bgcolor="#E6E6FA"><br />
            <asp:Label ID="lblTitulo" runat="server" Text="Lista de Egresados" 
                Font-Bold="True" Font-Size="11pt"></asp:Label></td>
        </tr>
     </table>
     <table class="style3" width="100%">
      <tr>
        <td class="style20"> Escuela: </td>
        <td class="style5">
            <asp:DropDownList ID="ddlEscuela" runat="server" Height="23px" Width="310px">
            </asp:DropDownList>
        </td>
        <td class="style6"> &nbsp;</td>
        <td class="style10">
            &nbsp;</td>
        </tr>
      <tr>
        <td class="style20"> Año Egreso:</td>
        <td class="style5">
                    <asp:DropDownList ID="ddlEgreso" runat="server" Height="20px" Width="95px">
                    </asp:DropDownList>
        </td>
        <td class="style6"> Semestre:</td>
        <td> 
            <asp:DropDownList ID="cboCiclo" runat="server">
            </asp:DropDownList>
          </td>
       </tr>
       <tr>
          <td class="style20"> Año Bachiller:</td>
          <td class="style5">
                    <asp:DropDownList ID="ddlBachiller" runat="server" Height="20px" Width="95px">
                    </asp:DropDownList>
          </td>
          <td class="style6"> Sexo:</td>
          <td class="style10">
                    <asp:DropDownList ID="ddlSexo" runat="server">
                        <asp:ListItem Selected="True" Value="%">TODOS</asp:ListItem>
                        <asp:ListItem>M</asp:ListItem>
                        <asp:ListItem>F</asp:ListItem>
                    </asp:DropDownList>
                </td>
       </tr>
       <tr>
          <td class="style20"> Año Título:</td>
          <td class="style5">
                    <asp:DropDownList ID="ddlTitulo" runat="server" Height="20px" Width="95px">
                    </asp:DropDownList>
          </td>
          <td class="style6"> Apellidos o Nombres:</td>
          <td class="style10">
                    <asp:TextBox ID="txtApellidoNombre" runat="server" Width="70%"></asp:TextBox></td>
       </tr>
       <tr>
                <td class="style20">
                <asp:Button ID="btnMensaje" Visible="false" runat="server" Text="Enviar Mensaje" CssClass="guardar2" Width="120px" Height="22px" />
                <asp:Button ID="btnBusca" runat="server" Text="Buscar" CssClass="buscar2"
                    Width="109px" Height="21px" />
                </td>
                <td class="style5">
                <!--<asp:Button ID="btnNuevoEgresado" runat="server" Text="Nuevo Egresado" CssClass="agregar2" Width="120px" Height="22px"  />---->
                <asp:Button ID="btnActualizar" visible="false" runat="server" Text="Actualizar Datos" CssClass="agregar2" Width="120px" Height="22px"  />
                <asp:Button ID="btnExportar" runat="server" Text="Exportar" CssClass="excel" Width="120px" Height="22px" />          
                </td>
                <td class="style6">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
       </tr>
       <tr>
                <td class="style20">
                    &nbsp;</td>
                <td class="style5">
                    &nbsp;</td>
                <td class="style6">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
       </tr>
       <tr>
                <td class="style7" colspan="4">
                <asp:Label ID="lblMensajeFormulario" runat="server" Font-Bold="True" 
                    ForeColor="#3366CC" Width="99%" style="margin-top: 0px"></asp:Label>
                </td>
       </tr>
        </table>
        <asp:GridView ID="gvwEgresados" runat="server" Width="100%" 
            AutoGenerateColumns="False" DataKeyNames="codigo_pso,foto_Ega,cv_Ega,NOMBRES,CORREO,APELLIDOS,EMAIL,SEXO,EMAIL_PER,codigo_alu"
            PageSize="15" CellPadding="2" ForeColor="#333333" Font-Size="Smaller">
        <Columns>
            <asp:TemplateField HeaderText="" >
                <HeaderTemplate>
                    <asp:CheckBox ID="chkHeader" runat="server" onclick="MarcarCursos(this)" />
                </HeaderTemplate>
                <ItemTemplate>                
                    <asp:CheckBox ID="chkElegir" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="codigo_pso" HeaderText="codigo_pso" />
            
            <asp:BoundField DataField="cv_Ega" HeaderText="cv_Ega" />
            <asp:BoundField DataField="foto_Ega" HeaderText="Foto"/>
            <asp:BoundField DataField="NIVEL" HeaderText="NIVEL" />
            <asp:BoundField DataField="MODALIDAD" HeaderText="MOD"/>                
            <asp:BoundField DataField="FACULTAD" HeaderText="FACULTAD" />
            <asp:BoundField DataField="ESCUELA PROFESIONAL" HeaderText="ESCUELA PROFESIONAL" />
            <asp:BoundField DataField="SEXO" HeaderText="SEXO">
                <ItemStyle HorizontalAlign="Center" />
              </asp:BoundField>
            <asp:BoundField DataField="AÑO EGRESADO" HeaderText="AÑO EGR.">
                <ItemStyle HorizontalAlign="Center" />
              </asp:BoundField>
            <asp:BoundField DataField="AÑO BACHILLER" HeaderText="AÑO BACH." >
                <ItemStyle HorizontalAlign="Center" />
              </asp:BoundField>
            <asp:BoundField DataField="AÑO TITULADO" HeaderText="AÑO TIT." >
                    <ItemStyle HorizontalAlign="Center" />
              </asp:BoundField>
            <asp:BoundField DataField="CORREO" HeaderText="CORREO PER." >
                    <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>            
            <asp:BoundField DataField="codigoUniver_Alu" HeaderText="COD. UNIV." />
            <asp:BoundField DataField="APELLIDOS" HeaderText="APELLIDOS" />
            <asp:BoundField DataField="NOMBRES" HeaderText="NOMBRES" />
            
            <asp:TemplateField HeaderText="Ficha" Visible="false">
                <ItemStyle Width="12px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="CV" Visible = "false">
                <ItemStyle Width="7%"  />            
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Mensaje" Visible="false">
                <ItemStyle Width="7%" />
            </asp:TemplateField>
            <asp:CommandField ButtonType="Image" EditImageUrl="../../images/ok.gif" 
                HeaderText="Activar" ShowEditButton="True" Visible="false" >
                <ItemStyle HorizontalAlign="Center"/>
            </asp:CommandField>
            <asp:BoundField DataField="EMAIL" HeaderText="EMAIL" Visible="false" />
            <asp:BoundField DataField="TELEFONO" HeaderText="TELEFONO" Visible="false" />
            <asp:BoundField DataField="email_per" HeaderText="email_per" Visible="false" />
            <asp:BoundField DataField="codigo_alu" HeaderText="codigo_alu" Visible="true" />
            <asp:BoundField DataField="CicloEgre" HeaderText="CICLO EGRESO" />
            <asp:BoundField DataField="cicloIng_Alu" HeaderText="CICLO INGRESO" />
            <asp:BoundField DataField="descripcion_Pes" HeaderText="PLAN DE ESTUDIOS" />
        </Columns>
                 <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                 <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                 <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#5D7B9D" ForeColor="White" Height="25px" Font-Bold="True" />                
        <RowStyle Height="22px" Wrap="False" BackColor="#F7F6F3" ForeColor="#333333" />
                 <EditRowStyle BackColor="#999999" />
                 <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
    </asp:GridView>
    <table cellspacing="0" width="70%"  style="width: 70%; border-color: #0099FF; border-bottom-width: 1px; 
            border-bottom-style:solid; border-left-width:1px; border-left-style:solid; 
            border-right-style:solid; border-right-width:1px; border-top-width:1px; border-top-style:solid" runat="server" id="tbMensaje" >
        <tr>
            <td colspan="2" align="center" style="height:22px; border-color: #0099FF; border-bottom-width: 1px; border-bottom-style:solid" bgcolor="#E6E6FA">
                <asp:Label ID="lblEnvioMultiple" runat="server" Text="Envio de Mensajes Múltiples" 
                    Font-Bold="True" Font-Size="Small"></asp:Label>
            </td>
        </tr>
        <tr style="height:22px">
            <td class="style19">
                <asp:Label ID="lblEnviado" runat="server" Text="Enviar a" Font-Bold="True"></asp:Label>
            </td>
            <td class="style11">
                
                <label ID="lblDestinatario1" runat="server" style="font-weight:bold" ></label>
            </td>
        </tr>
        <tr>
            <td class="style18">                           
            
                <asp:Label ID="lblAsunto" runat="server" Text="Asunto " Font-Bold="True"></asp:Label>
            </td>
            <td class="style12">
                <asp:TextBox ID="txtAsunto" runat="server" Width="55%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style19">                           
                <asp:Label ID="lblMensaje" runat="server" Text="Mensaje" Font-Bold="True" ></asp:Label>
            </td>
            <td class="style11" >
               
            </td>
        </tr>
        <tr>
            <td class="style19">                           
                &nbsp;</td>
            <td class="style11" >
                <textarea runat="server" id="txtMensaje" 
                    style="font-family: Cambria; height: 130px;"></textarea></td>
        </tr>
        <tr>
            <td class="style15">                           
                <asp:Label ID="lblMensaje1" runat="server" Text="Adjuntar archivo" 
                    Font-Bold="True"></asp:Label>
            </td>
            <td class="style16" >
                <asp:FileUpload ID="FileUpload1" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2" align="right">
                <asp:Button ID="btnGuardar" runat="server" Text="Enviar" CssClass="guardar2" 
                    Width="100px" Height="20px" />&nbsp;&nbsp;
                <asp:Button ID="btnSalir" runat="server" Text="Regresar" CssClass="regresar2" Width="100px" Height="20px" />
            </td>            
        </tr>
    </table>          
          <br />
    <table cellspacing="0" width="85%"  style="width: 85%; border-color: #0099FF; border-bottom-width: 1px; 
            border-bottom-style:solid; border-left-width:1px; border-left-style:solid; 
            border-right-style:solid; border-right-width:1px; border-top-width:1px; border-top-style:solid" runat="server" id="tblDatos" >
        <tr>
            <td colspan="2" align="center" style="height:22px; border-color: #0099FF; border-bottom-width: 1px; border-bottom-style:solid" bgcolor="#E6E6FA">
                <asp:Label ID="lblTit" runat="server" Text="Actualizar Correos y/o Teléfonos" 
                    Font-Bold="True" Font-Size="Small"></asp:Label>
            </td>
        </tr>
        <tr style="height:22px">
            <td class="style10">
                <asp:Label ID="lblEgresado" runat="server" Text="Egresado:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtEgresado" runat="server" Width="365px" ReadOnly="True" 
                    BackColor="#FFFFCC"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style8">                           
                <asp:Label ID="lblCorreo1" runat="server" Text="Correo Personal:"></asp:Label></td>
            <td class="style9">
                <asp:TextBox ID="txtCorreo1" runat="server" Width="80%" MaxLength="100"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style8">                           
                <asp:Label ID="lblCorreo2" runat="server" Text="Correo Profesional:"></asp:Label></td>
            <td class="style9">
                <asp:TextBox ID="txtCorreo2" runat="server" Width="80%" MaxLength="100"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style10">                           
                <asp:Label ID="lblFijo" runat="server" Text="Teléfono Fijo:"></asp:Label></td>
            <td >
                <asp:TextBox ID="txtFijo" runat="server" Width="20%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style10">                           
                <asp:Label ID="lblCelular1" runat="server" Text="Celular 01:"></asp:Label></td>
            <td >
                <asp:TextBox ID="txtCelular1" runat="server" Width="20%" MaxLength="20"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style10">                           
                <asp:Label ID="lblCelular2" runat="server" Text="Celular 02:"></asp:Label></td>
            <td >
                <asp:TextBox ID="txtCelular2" runat="server" Width="20%" MaxLength="20"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="right">
                <asp:HiddenField ID="psotoupdate" runat="server" />
                <asp:Button ID="btnActualizarDatos" runat="server" Text="Actualizar" CssClass="guardar2" 
                    Width="100px" Height="20px" />&nbsp;&nbsp;
                <asp:Button ID="btnRegresar2" runat="server" Text="Regresar" CssClass="regresar2" Width="100px" Height="20px" />
            </td>            
        </tr>
    </table>          
     
   
     
    <asp:GridView ID="gvExporta" runat="server" CellPadding="4" ForeColor="#333333" 
        AutoGenerateColumns="False"  Font-Size="Smaller">
        
           <Columns>
            <asp:BoundField DataField="NIVEL" HeaderText="NIVEL" />
            <asp:BoundField DataField="MODALIDAD" HeaderText="MODALIDAD"/>                
            <asp:BoundField DataField="FACULTAD" HeaderText="FACULTAD" />
            <asp:BoundField DataField="ESCUELA PROFESIONAL" HeaderText="ESCUELA PROFESIONAL" />
            <asp:BoundField DataField="SEXO" HeaderText="SEXO">
                <ItemStyle HorizontalAlign="Center" />
              </asp:BoundField>
            <asp:BoundField DataField="AÑO EGRESADO" HeaderText="AÑO EGRESADO">
                <ItemStyle HorizontalAlign="Center" />
              </asp:BoundField>
            <asp:BoundField DataField="AÑO BACHILLER" HeaderText="AÑO BACHILLER" >
                <ItemStyle HorizontalAlign="Center" />
              </asp:BoundField>
            <asp:BoundField DataField="AÑO TITULADO" HeaderText="AÑO TITULADO" >
                    <ItemStyle HorizontalAlign="Center" />
              </asp:BoundField>
            <asp:BoundField DataField="CORREO" HeaderText="CORREO" >
                    <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>            
               <asp:BoundField DataField="codigoUniver_Alu" HeaderText="COD. UNIV." />
            <asp:BoundField DataField="APELLIDOS" HeaderText="APELLIDOS" />
            <asp:BoundField DataField="NOMBRES" HeaderText="NOMBRES" />           
            <asp:BoundField DataField="EMAIL" HeaderText="EMAIL" />           
            <asp:BoundField DataField="CicloEgre" HeaderText="CICLO EGRESO" />
               <asp:BoundField DataField="cicloIng_Alu" HeaderText="CICLO ING." />
        </Columns>
                 <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                 <PagerStyle BackColor="#284775" ForeColor="White" 
               HorizontalAlign="Center" />
                 <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" 
               ForeColor="#333333" />
        <HeaderStyle BackColor="#5D7B9D" ForeColor="White" Height="25px" 
               Font-Bold="True" />                
        <RowStyle Height="22px" Wrap="False" BackColor="#F7F6F3" ForeColor="#333333" />
                 <EditRowStyle BackColor="#999999" />
                 <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
    </asp:GridView>
    
    
   
    </form>
</body>
</html>
