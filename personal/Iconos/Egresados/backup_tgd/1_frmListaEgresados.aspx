<%@ Page Language="VB" AutoEventWireup="false" CodeFile="1_frmListaEgresados.aspx.vb" Inherits="Egresado_frmListaEgresados" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />    
    <script type="text/javascript" language="JavaScript" src="../../private/funciones.js"></script>
    <script type="text/javascript" language="JavaScript" src="../../private/jq/jquery-1.4.2.min.js"></script> 
	<script type="text/javascript" language="JavaScript" src="../../private/jq/lbox/thickbox.js"></script>
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
        .style1
        {
            width: 50px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table width="100%">
        <tr>
            <td style="width: 100%; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #0099FF;" 
                height="40px" bgcolor="#E6E6FA" colspan="3">
            <asp:Label ID="lblTitulo" runat="server" Text="Lista de Egresados" 
                Font-Bold="True" Font-Size="11pt"></asp:Label></td>
        </tr>
        <tr>
            <td class="style1">Tipo:<br /><br />Estado:</td>
            <td colspan="2">
                <!--
                <select id="cboTipo" name="cboTipo">
                    <option value="1">Valor1</option>
                    <option value="2">Valor2</option>
                    <option value="3">Valor3</option>
                </select>
                -->
                &nbsp;
                <asp:DropDownList ID="dpTipo" runat="server" Width="15%">
                    <asp:ListItem Value="A" Text="TODOS"></asp:ListItem>
                    <asp:ListItem Value="E" Text="EGRESADO"></asp:ListItem>
                    <asp:ListItem Value="B" Text="BACHILLER"></asp:ListItem>
                    <asp:ListItem Value="T" Text="TITULADO"></asp:ListItem> 
                </asp:DropDownList> &nbsp;&nbsp;&nbsp; 
                Facultad: &nbsp; 
                <asp:DropDownList ID="dpFacultad" runat="server" Width="30%">
                    <asp:ListItem Value="0" Text="TODOS"></asp:ListItem>
                </asp:DropDownList> &nbsp;
                Escuela: &nbsp; 
                <asp:DropDownList ID="dpEscuela" runat="server" Width="33%">
                    <asp:ListItem Value="0" Text="TODOS"></asp:ListItem>
                </asp:DropDownList> <br />
                &nbsp; 
                <asp:DropDownList ID="dpEstado" runat="server" Width="15%">
                    <asp:ListItem Value="1" Text="CONFIRMADO"></asp:ListItem>
                    <asp:ListItem Value="0" Text="NO CONFIRMADO"></asp:ListItem>
                    <asp:ListItem Value="2" Text="OBSERVADO"></asp:ListItem>
                </asp:DropDownList>&nbsp;&nbsp;&nbsp;
                <asp:DropDownList ID="Nivel" runat="server" Width="15%">
                    <asp:ListItem Value="T" Text="TODOS"></asp:ListItem>
                    <asp:ListItem Value="PG" Text="POST GRADO"></asp:ListItem>
                    <asp:ListItem Value="PT" Text="POST TITULO"></asp:ListItem>
                    <asp:ListItem Value="G" Text="PRE GRADO"></asp:ListItem> 
                </asp:DropDownList> &nbsp;&nbsp;&nbsp; 
            </td>          
        </tr>
        <tr>
            <td class="style1">Nombre:</td> 
            <td>&nbsp;<asp:TextBox ID="txtNombre" runat="server" Width="40.5%"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            Año de egresado:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:DropDownList ID="dpAnio" runat="server" Width="10%">
                </asp:DropDownList>
                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="buscar2"
                    Width="109px" Height="21px" />
            </td>
        </tr>                                        
    </table>
    <br />        
    <table width="100%">
        <tr>
            <td style="width: 50%">                           
                <asp:Button ID="btnMensaje" runat="server" Text="Enviar Mensaje" CssClass="guardar2" Width="120px" Height="22px" />
            </td>
            <td style="width: 50%" align="right">
                <asp:Button ID="btnNuevoEgresado" runat="server" Text="Nuevo Egresado" CssClass="agregar2" Width="120px" Height="22px"  />
                <asp:Button ID="btnExportar" runat="server" Text="Exportar" CssClass="excel" Width="120px" Height="22px" />          
            </td>
        </tr>        
        <tr>
            <td colspan="2" style="height:18px">
                <asp:Label ID="lblMensajeFormulario" runat="server" Font-Bold="True" 
                    ForeColor="Red"></asp:Label>
                &nbsp;</td>
        </tr>        
    </table> 
    <br />
    <table cellspacing="0" width="100%"  style="width: 100%; border-color: #0099FF; border-bottom-width: 1px; 
            border-bottom-style:solid; border-left-width:1px; border-left-style:solid; 
            border-right-style:solid; border-right-width:1px; border-top-width:1px; border-top-style:solid" runat="server" id="tbMensaje" >
        <tr>
            <td colspan="2" align="center" style="height:22px; border-color: #0099FF; border-bottom-width: 1px; border-bottom-style:solid" bgcolor="#E6E6FA">
                <asp:Label ID="lblEnvioMultiple" runat="server" Text="Envio de Mensajes Múltiples" 
                    Font-Bold="True" Font-Size="Small"></asp:Label>
            </td>
        </tr>
        <tr style="height:22px">
            <td>
                <asp:Label ID="lblEnviado" runat="server" Text="Enviado a:"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblDestinatario" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 15%">                           
                <asp:Label ID="lblAsunto" runat="server" Text="Asunto:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtAsunto" runat="server" Width="99%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 15%">                           
                <asp:Label ID="lblMensaje" runat="server" Text="Mensaje:"></asp:Label>
            </td>
            <td >
                <asp:TextBox ID="txtMensaje" runat="server" TextMode="MultiLine" Width="99%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="right">
                <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="guardar2" Width="100px" Height="20px" />&nbsp;&nbsp;
                <asp:Button ID="btnSalir" runat="server" Text="Regresar" CssClass="regresar2" Width="100px" Height="20px" />
            </td>            
        </tr>
    </table>           
    <br />
    <asp:GridView ID="gvwEgresados" runat="server" Width="100%" 
            AutoGenerateColumns="False" DataKeyNames="codigo_pso,foto_Ega,cv_Ega" 
            PageSize="15">
        <Columns>
            <asp:TemplateField HeaderText="" >
                <HeaderTemplate>
                    <asp:CheckBox ID="chkHeader" runat="server" onclick="MarcarCursos(this)" />
                </HeaderTemplate>
                <ItemTemplate>                
                    <asp:CheckBox ID="chkElegir" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Foto" />
            <asp:BoundField DataField="codigo_pso" HeaderText="codigo_pso" />
            <asp:BoundField DataField="cv_Ega" HeaderText="cv_Ega" />
            <asp:BoundField DataField="foto_Ega" HeaderText="foto_Ega" />
            <asp:BoundField DataField="codigoUniver_Alu" HeaderText="Cod. Univ." >
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="nroDocIdent_Alu" HeaderText="DNI" >
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="NombreCompleto" HeaderText="Egresado" />
            <asp:BoundField DataField="nombre_Cpf" HeaderText="Carrera Profesional" />
            <asp:BoundField DataField="FechaEgresado" HeaderText="Egresado" >
                <ItemStyle HorizontalAlign="Center" Width="7%" />
            </asp:BoundField>            
            <asp:BoundField DataField="nivel_Ega" HeaderText="Estado" />
            <asp:TemplateField HeaderText="Ficha">
                <ItemStyle Width="7%" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="CV">
                <ItemStyle Width="7%" />            
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Mensaje">
                <ItemStyle Width="7%" />
            </asp:TemplateField>
            <asp:CommandField ButtonType="Image" EditImageUrl="../../images/ok.gif" 
                HeaderText="Activar" ShowEditButton="True" >
                <ItemStyle HorizontalAlign="Center" />
            </asp:CommandField>
        </Columns>
        <HeaderStyle BackColor="#0B3861" ForeColor="White" Height="25px" />                
        <RowStyle Height="22px" />
    </asp:GridView>
    </div>    
    <br />
    <asp:GridView ID="gvExporta" runat="server">
    </asp:GridView>
    </form>
</body>
</html>
