<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmGestionInvestigacion.aspx.vb" Inherits="frmGestionInvestigacion" EnableEventValidation ="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Gestión Comité</title>       
    <link href="../css/estilo.css" rel="stylesheet" type="text/css" />
    
    <script src="../../private/funciones.js" type ="text/javascript" language ="javascript"></script>
    <script src="../../private/PopCalendar.js" language="javascript" type="text/javascript"></script>
    
    <script src="../../private/jq/jquery.js" type="text/javascript"></script>
    <script src="../../private/jq/jquery.mascara.js" type="text/javascript"></script>
        <style type="text/css">
        TBODY {
	display: table-row-group;
}
a:Link {
	color: #000000;
	text-decoration: none;
}
        </style>
   
   
   
        
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
    
     <script type = "text/javascript">
         function Confirm() {
             var confirm_value = document.createElement("INPUT");
             confirm_value.type = "hidden";
             confirm_value.name = "confirm_value";
             if (confirm("¿Esta completamente seguro de dar CONFORMIDAD a los proyectos de investigacion seleccionados?")) {
                 confirm_value.value = "Yes";
             } else {
                 confirm_value.value = "No";
             }
             document.forms[0].appendChild(confirm_value);
         }
    </script>
    
    <script type="text/javascript">
        var _estadoModal;
        function pageLoad() {
            $addHandler($get("ibtnCerrarPopUpVerEliminados"), "click", OnSalir_Click);

            var mpe = $find("mpeVerEliminados");
            mpe.add_shown(OnShowModal);

            if (_estadoModal)
                mpe.show();
        }

        function OnShowModal(sender) {
            _estadoModal = true;
        }

        function OnSalir_Click(sender) {
            _estadoModal = false;
        } 

    </script>
    
</head>
<body>
    <script type="text/javascript">

        function uploadComplete(sender, args) {
            //document.getElementById('<%=lblDocumento.ClientID%>').innerText = args.get_path();
            //$('#lblDocumento').html(args.get_path());
            $("input:hidden[id$=hfDocumento]").val(args.get_path());
        }
        //hfAvance
        function uploadCompleteAvance(sender, args) {
            $("input:text[id$=txtDocAvance]").val(args.get_path());
        }

    </script>

    <form id="form1" runat="server" enctype="multipart/form-data" method="post">
<%  response.write(clsfunciones.cargacalendario) %>
    <div>
     
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="upContenido" runat="server">
        <ContentTemplate>
           
            <table style="width:100%;">
            <tr>
                <td>
                     <table style="width:100%;">
                        <tr>
                            <td style ="background-color:#D1DDEF; font-weight: bold;" height="30px">
                               <asp:Label ID="lblTit" runat="server" Text="Investigaciones"></asp:Label>
                            </td>
                            <td rowspan="2" align="center" style ="background-color:#D1DDEF">
                                <asp:Image ID="imgPerfil" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right"style ="background-color:#D1DDEF;  font-weight: bold;" height="20px">
                                <asp:Label ID="lblEstadoRevisor" runat="server" Text="Estado Revisión: " 
                                    Visible="False">
                                </asp:Label>
                                <asp:Label ID="lblCantidad" runat="server" ForeColor="Blue"></asp:Label>
                                <asp:DropDownList ID="ddlEstadoRevisor" runat="server" Visible="False" 
                                    AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td bgcolor="#FFFFCC" height="15px">
                                <asp:Panel ID="pnlAlertaProfesor" runat="server">
                                    <table style="width:100%;">
                                        <tr style="width:100%">
                                             <td colspan="6" style="height:20px">
                                                 <asp:Label ID="Label1" runat="server" Text="Resumen de mis Investigaciones según Instancia:"></asp:Label>
                                             </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <asp:Label ID="Label10" runat="server" Text="I.Autor"></asp:Label></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                         <tr>
                                            <td align="center">
                                                <asp:Label ID="lblnumAutor" runat="server" Text="10"></asp:Label></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                    </table>
                                    <asp:Label ID="lblMensajes" runat="server" 
                                        Text="(*) Las investigaciones marcadas de color rojo no se han enviado." 
                                        ForeColor="Red">
                                    </asp:Label>
                                </asp:Panel>
                            </td>
                            <td align="center" style ="background-color:#D1DDEF">
                                <asp:Label ID="lblPerfil" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>    
                                
                                
                                <td colspan="2" align="right" style ="background-color:#f0f0f0; font-weight: bold;" 
                                    height="30px">
                                        <asp:Button 
                                            ID="btnConforme" 
                                            runat="server" 
                                            CssClass="conforme1" 
                                            Width="95px" 
                                            Height="47px" 
                                            OnClick="btnConforme_Click"
                                            Text="          Conforme" 
                                            Font-Underline="False" />
                                    <asp:Button ID="btnObservar" runat="server" CssClass="observar_inv" 
                                        Font-Underline="False" Height="47px" Text="        Observar" Width="95px" />
                                    <asp:Button ID="btnRechazar" runat="server" CssClass="rechazar_inv" 
                                        Font-Underline="False" Height="47px" Text="        Rechazar" Width="95px" />
                                    <asp:Button ID="btnEnviar" runat="server" CssClass="enviarpropuesta" 
                                        Height="47px" Text="          Enviar" Width="95px" />
                                    <asp:Button ID="btnNuevo" runat="server" Width="95px" Height="47px" Text="       Nuevo" 
                                    CssClass="nuevo1" ToolTip="Registrar una Nueva Investigación" />
                                </td>
                            </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gvInvestigacion" runat="server" CellPadding="4" 
                        ForeColor="#333333" Width="100%" 
                                        BorderColor="#99BAE2" BorderStyle="Solid" BorderWidth="1px" 
                                AllowPaging="True" PageSize="5" GridLines="Horizontal" 
                        AutoGenerateColumns="False" DataKeyNames="instancia">
                        
                        <Columns>
                            <asp:BoundField DataField="id" HeaderText="ID"></asp:BoundField>
                            <asp:BoundField DataField="titulo" HeaderText="Título"></asp:BoundField>
                            <asp:BoundField DataField="linea" HeaderText="Linea"></asp:BoundField>
                            <asp:BoundField DataField="fechaRegistro" HeaderText="Registro"></asp:BoundField>
                            <asp:BoundField DataField="fechaInicio" HeaderText="Inicio"></asp:BoundField>
                            <asp:BoundField DataField="fechaFin" HeaderText="Fin"></asp:BoundField>
                            <asp:BoundField DataField="presupuesto" HeaderText="Presupuesto">
                            
                            </asp:BoundField>
                            <asp:TemplateField>
                                    <ItemTemplate>
                                        <table style="width:100%;">
                                            <tr>
                                                <td>
                                                    <asp:LinkButton ID="lbModificar" runat="server" Font-Underline="True" 
                                                        onclick="lbModificar_Click" 
                                                        Visible='<%# iif(Eval("etapa") = "Borrador", "True", "False") %>'>Modificar
                                                </asp:LinkButton>
                                                </td>
                                                <td>
                                                    <asp:LinkButton ID="hlEnviar" runat="server" Font-Underline="True" 
                                                        OnClick="hlEnviar_Click" 
                                                        Visible='<%# iif(Eval("etapa") = "Borrador", "True", "False") %>' 
                                                        Font-Bold="True">Enviar
                                                </asp:LinkButton>
                                                </td>
                                                <td>
                                                    <asp:LinkButton ID="hlVersion" runat="server" Font-Underline="True" 
                                                        OnClick="hlVersion_Click" 
                                                        Visible='<%# iif(Eval("estado_version") = "3", "True", "False") %>'>Versión
                                                </asp:LinkButton>
                                                </td>
                                                <td>
                                                    <asp:LinkButton ID="hlModificarVersion" runat="server" Font-Underline="True" 
                                                        OnClick="hlModificarVersion_Click" 
                                                        Visible='<%# iif(Eval("estado_version") = "2", "True", "False") %>'>Mod.Versión
                                                </asp:LinkButton>
                                                </td>
                                                <td>
                                                    <asp:LinkButton ID="hlEnviarVersion" runat="server" Font-Underline="True" 
                                                        OnClick="hlEnviarVersion_Click" 
                                                        Visible='<%# iif(Eval("estado_version") = "2", "True", "False") %>'>Env.Versión
                                                </asp:LinkButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                    <ItemStyle Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkHeader" runat="server"  onclick="MarcarCursos(this)" />
                                    </HeaderTemplate>
                                    <ItemTemplate>                
                                        <asp:CheckBox 
                                             ID="chkElegir" 
                                             runat="server" 
                                             />
                                    </ItemTemplate>
                                    <ItemStyle Width="5px" />
                            </asp:TemplateField>
                           <asp:CommandField SelectText="" ShowSelectButton="True" />
                        </Columns>
                        <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" />
                        <HeaderStyle BackColor="#e8eef7" ForeColor="#3366CC" BorderColor="#99BAE2" 
                                        BorderStyle="Solid" BorderWidth="1px" />
                        <EditRowStyle BackColor="#999999" />
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="pnlDetalle" runat="server" Visible="false">
                        <asp:Menu
                                ID="Menu1"
                                Width="50%"
                                runat="server"
                                Orientation="Horizontal"
                                StaticEnableDefaultPopOutImage="False"
                                OnMenuItemClick="Menu1_MenuItemClick">
                            <Items>
                                <asp:MenuItem ImageUrl="~/Images/TagButtons/btnInvDGSel.png" Text=" " Value="0"></asp:MenuItem>
                                <asp:MenuItem ImageUrl="~/Images/TagButtons/btnInvBitacora.png" Text=" " Value="1"></asp:MenuItem>
                                <asp:MenuItem ImageUrl="~/Images/TagButtons/btnInvResumen.png" Text=" " Value="2"></asp:MenuItem>
                                <asp:MenuItem ImageUrl="~/Images/TagButtons/btnInvEstado.png" Text=" " Value="3"></asp:MenuItem>
                            </Items>
                        </asp:Menu>

                        <asp:MultiView 
                            ID="MultiView1"
                            runat="server"
                            ActiveViewIndex="0"  >
                           <asp:View ID="Tab1" runat="server"  >
                                <table style="border: 1px solid #99BAE2; width:100%; border-collapse: collapse;" width="100%" height="100%" cellpadding=0 cellspacing=0>
                                    <tr>
                                        <td style="width:5%">
                                            &nbsp;</td>
                                        <td style="width:15%">
                                            <asp:HiddenField ID="hfUsuReg" runat="server" />
                                        </td>
                                        <td style="width:30%">
                                            <asp:HiddenField ID="hfCodInvestigacion" runat="server" />
                                        </td>
                                        <td style="width:5%">
                                            &nbsp;</td>
                                        <td style="width:15%">
                                        </td>
                                        <td style="width:30%">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:5%">
                                            &nbsp;</td>
                                        <td style="width:15%; font-weight: bold;">
                                            Versión</td>
                                        <td style="width:30%">
                                            <asp:DropDownList ID="ddlVersion" runat="server" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width:5%">
                                            &nbsp;</td>
                                        <td style="width:15%; font-weight: bold;">
                                            <asp:Label ID="lblAutor" runat="server" Text="Autor:"></asp:Label>
                                        </td>
                                        <td style="width:30%">
                                            
                                            <asp:Label ID="lblNombreAutor" runat="server" Font-Bold="True" ForeColor="Blue"></asp:Label>
                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:5%">
                                            &nbsp;</td>
                                        <td style="width:15%; font-weight: bold;">
                                            <asp:Label ID="Label11" runat="server" Text="Estado Envío"></asp:Label>
                                        </td>
                                        <td style="width:30%">
                                            <asp:Label ID="lblEstadoEnvio" runat="server"></asp:Label>
                                        </td>
                                        <td style="width:5%">
                                            &nbsp;</td>
                                        <td style="width:15%; font-weight: bold;">
                                            &nbsp;</td>
                                        <td style="width:30%">
                                            &nbsp;</td>
                                    </tr>
                                    
                                    <tr>
                                        <td style="width:5%">
                                            
                                        </td>
                                        <td style="width:15%; font-weight: bold;">
                                            Título</td>
                                        <td style="width:30%">
                                            <asp:Label ID="lblTitulo" runat="server"></asp:Label>
                                        </td>
                                        <td style="width:5%">
                                            &nbsp;</td>
                                        <td style="width:15%; font-weight: bold;">
                                            Fecha de Reg</td>
                                        <td style="width:30%">
                                            <asp:Label ID="lblFechaRegistro" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:5%">
                                            &nbsp;</td>
                                        <td style="width:15%; font-weight: bold;">
                                            Fecha Inicio</td>
                                        <td style="width:30%">
                                            <asp:Label ID="lblFecIni" runat="server"></asp:Label>
                                        </td>
                                        <td style="width:5%">
                                            &nbsp;</td>
                                        <td style="width:15%; font-weight: bold;">
                                            Fecha Fin</td>
                                        <td style="width:30%">
                                            <asp:Label ID="lblFecFin" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:5%">
                                            &nbsp;</td>
                                        <td style="width:15%; font-weight: bold;">
                                            Presupuesto</td>
                                        <td style="width:30%">
                                            <asp:Label ID="lblPresupuesto" runat="server"></asp:Label>
                                        </td>
                                        <td style="width:5%">
                                            &nbsp;</td>
                                        <td style="width:15%; font-weight: bold;">
                                            Financiamiento</td>
                                        <td style="width:30%">
                                            <asp:Label ID="lblFinanci" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:5%">
                                            &nbsp;</td>
                                        <td style="width:15%; font-weight: bold;">
                                            Ámbito</td>
                                        <td style="width:30%">
                                            <asp:Label ID="lblAmbito" runat="server"></asp:Label>
                                        </td>
                                        <td style="width:5%">
                                            &nbsp;</td>
                                        <td style="width:15%">
                                            &nbsp;</td>
                                        <td style="width:30%">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="width:5%">
                                            &nbsp;</td>
                                        <td style="width:15%; ">
                                            &nbsp;</td>
                                        <td style="width:30%">
                                            &nbsp;</td>
                                        <td style="width:5%">
                                            &nbsp;</td>
                                        <td style="width:15%; ">
                                            &nbsp;</td>
                                        <td style="width:30%">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="width:5%">
                                            &nbsp;</td>
                                        <td style="width:15%; font-weight: bold;">
                                            Línea</td>
                                        <td style="width:30%">
                                            <asp:Label ID="lblLinea" runat="server"></asp:Label>
                                        </td>
                                        <td style="width:5%">
                                            &nbsp;</td>
                                        <td style="width:15%; font-weight: bold;">
                                            Etapa</td>
                                        <td style="width:30%">
                                            <asp:Label ID="lblEtapa" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:5%">
                                            &nbsp;</td>
                                        <td style="width:15%; font-weight: bold;">
                                            Tipo</td>
                                        <td style="width:30%">
                                            <asp:Label ID="lblTipo" runat="server"></asp:Label>
                                        </td>
                                        <td style="width:5%">
                                            &nbsp;</td>
                                        <td style="width:15%; font-weight: bold;">
                                            Instancia</td>
                                        <td style="width:30%">
                                            <asp:Label ID="lblInstancia" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:5%">
                                            &nbsp;</td>
                                        <td style="width:15%; ">
                                            &nbsp;</td>
                                        <td style="width:30%">
                                            &nbsp;</td>
                                        <td style="width:5%">
                                            &nbsp;</td>
                                        <td style="width:15%">
                                            <asp:Label ID="lblTipDocumento" runat="server" Font-Bold="True" 
                                                Text="Subir Documento"></asp:Label>
                                        </td>
                                        <td style="width:30%">
                                            <asp:Button ID="btnTipDocumento" runat="server" Text="..." />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:5%">
                                            &nbsp;</td>
                                        <td style="width:15%; font-weight: bold;" valign="top">
                                            Beneficiarios</td>
                                        <td colspan="3">
                                            <asp:Label ID="lblBeneficiarios" runat="server"></asp:Label>
                                        </td>
                                        <td style="width:30%">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="width:5%">
                                            &nbsp;</td>
                                        <td style="width:15%; font-weight: bold;" valign="top">
                                            Resumen</td>
                                        <td colspan="3" style="text-align:justify">
                                            <asp:Label ID="lblResumen"  runat="server"></asp:Label>
                                        </td>
                                        <td style="width:30%">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="width:5%">
                                            &nbsp;</td>
                                        <td style="width:15%">
                                            &nbsp;</td>
                                        <td style="width:30%">
                                            &nbsp;</td>
                                        <td style="width:5%">
                                            &nbsp;</td>
                                        <td style="width:15%">
                                            &nbsp;</td>
                                        <td style="width:30%">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="6">
                                            <asp:GridView 
                                                ID="gvDocumentos" 
                                                runat="server" 
                                                AllowPaging="True" 
                                                AutoGenerateColumns="False" BorderColor="#99BAE2" BorderStyle="Solid" 
                                                BorderWidth="1px" CellPadding="4" ForeColor="#333333" GridLines="Horizontal" 
                                                PageSize="5" Width="70%">
                                                <Columns>
                                                    <asp:ImageField ConvertEmptyStringToNull="False" DataImageUrlField="extension" 
                                                        DataImageUrlFormatString="~/images/ext/{0}.gif" HeaderText="">
                                                        <HeaderStyle Width="1%" />
                                                        <ItemStyle HorizontalAlign="Center" Width="20px" />
                                                    </asp:ImageField>
                                                    <asp:HyperLinkField DataNavigateUrlFields="documento" 
                                                        DataNavigateUrlFormatString="{0}" 
                                                        DataTextField="nombre" 
                                                        HeaderText="Documento" 
                                                        Target="_blank">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:HyperLinkField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="hfRuta" runat="server" value='<%# Eval("ruta") %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle Width="1%" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    <table style="width:100%;">
                                                        <tr>
                                                            <td style="width:90%">
                                                                La investigación no tiene ningún Documento.</td>
                                                            <td style="width:10%">
                                                                <asp:Image ID="imgNingun" runat="server" ImageUrl="~/Images/cerrar.gif" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </EmptyDataTemplate>
                                                <HeaderStyle BackColor="#e8eef7" BorderColor="#99BAE2" BorderStyle="Solid" 
                                                    BorderWidth="1px" ForeColor="#3366CC" />
                                                <EditRowStyle BackColor="#999999" />
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="6">
                                            &nbsp;</td>
                                    </tr>
                                </table>
                           </asp:View>
                           <asp:View ID="Tab2" runat="server"  >
                               <table style="border: 1px solid #99BAE2; width:100%; border-collapse: collapse;" width="100%" height="100%" cellpadding=0 cellspacing=0>
                                    <tr style="width:100%" valign="top">
                                        <td  style="width: 100%">&nbsp;
                                        </td>
                                    </tr>
                                    <tr style="width:100%" valign="top">
                                        <td align="center" style="width: 100%">
                                            <asp:GridView ID="gvBitacora" runat="server" AllowPaging="True" 
                                                AutoGenerateColumns="False" BorderColor="#99BAE2" BorderStyle="Solid" 
                                                BorderWidth="1px" CellPadding="4" ForeColor="#333333" 
                                                GridLines="Horizontal" Width="70%">
                                                <Columns>
                                                    <asp:BoundField DataField="descripcion" HeaderText="Se actualizó" />
                                                    <asp:BoundField DataField="nombre" HeaderText="Nuevo valor" />
                                                    <asp:BoundField DataField="fecha" HeaderText="Fecha" />
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    <table style="width:100%;">
                                                        <tr>
                                                            <td style="width:90%">
                                                                No hay registros.</td>
                                                            <td style="width:10%;">
                                                                <asp:Image ID="imgNingun" runat="server" ImageUrl="~/Images/cerrar.gif" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </EmptyDataTemplate>
                                                <HeaderStyle BackColor="#e8eef7" BorderColor="#99BAE2" BorderStyle="Solid" 
                                                    BorderWidth="1px" ForeColor="#3366CC" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr style="width:100%" valign="top">
                                        <td style="width: 100%">
                                            &nbsp;</td>
                                    </tr>
                                </table>
                           </asp:View>
                           <asp:View ID="Tab3" runat="server"  >
                              <table style="border: 1px solid #99BAE2; width:100%; border-collapse: collapse;" width="100%" height="100%" cellpadding=0 cellspacing=0>
                                    <tr valign="top">
                                        <td style="width: 65%">
                                            
                                        </td>
                                        <td style="width: 35%">
                                            &nbsp;</td>
                                    </tr>
                                    <tr valign="top">
                                        <td style="width: 65%" align="center">
                                            <asp:GridView ID="gvResumen" runat="server" AllowPaging="True" 
                                                AutoGenerateColumns="False" BorderColor="#99BAE2" BorderStyle="Solid" 
                                                BorderWidth="1px" CellPadding="4" ForeColor="#333333" GridLines="Horizontal" 
                                                PageSize="5" Width="98%">
                                                <Columns>
                                                    <asp:BoundField DataField="hito_id" HeaderText="Hito" Visible="False" ><ItemStyle Width="30%" /></asp:BoundField>
                                                    <asp:BoundField DataField="hito" HeaderText="Hito" Visible="False" ><ItemStyle Width="30%" /></asp:BoundField>
                                                    <asp:TemplateField HeaderText="Hito">
                                                        <ItemTemplate>
                                                            <asp:Panel ID="pnlHito" runat="server">
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <asp:Label ID="lblFechaHito" runat="server" Text='<%# Eval("fechaHito") %>'></asp:Label>
                                                                        <asp:HiddenField ID="hfHito_id" runat="server" value='<%# Eval("hito_id") %>' />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <asp:Label ID="lblDesHito" runat="server" Text='<%# Eval("hitoDes") %>'></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width:50%">
                                                                        <asp:LinkButton ID="lbModificarHito" runat="server" Font-Underline="True" 
                                                                            ForeColor="#003399" onclick="lbModificarHito_Click">Modificar</asp:LinkButton>
                                                                    </td>
                                                                    <td style="width:50%">
                                                                        <asp:LinkButton ID="lbEliminarHito" runat="server" Font-Underline="True" 
                                                                            ForeColor="#003399" onclick="lbEliminarHito_Click">Eliminar</asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            </asp:Panel>
                                                        </ItemTemplate>
                                                        <HeaderTemplate>
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td style="width: 50%;color:#3366CC">
                                                                    Hito
                                                                    </td>
                                                                    <td style="width: 50%;">
                                                                    <asp:LinkButton ID="hlNuevoHito" runat="server" Font-Underline="True" 
                                                                            ForeColor="#003399" onclick="hlNuevoHito_Click">Nuevo Hito</asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </table> 
                                                        </HeaderTemplate>
                                                        <ItemStyle Width="42%" HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Avance"><ItemTemplate>
                                                    <table style="width:100%;">
                                                            <tr>
                                                                <td colspan="2">
                                                                    <asp:Label ID="lblFecha" runat="server" Text= '<%# Eval("fechaAvance") %>' 
                                                                        ForeColor='<%# iif(Eval("fechaAvance") = "No hay ningún Avance",System.Drawing.Color.Red,System.Drawing.Color.Black) %>'></asp:Label>
                                                                    <asp:HiddenField ID="hfAvance_id" runat="server" value='<%# Eval("avance_id") %>' />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                    <asp:HyperLink ID="hlAvance" runat="server" Target ="_blank" NavigateUrl='<%# Eval("rutaAvance") %>' Text='<%# Eval("documento") %>' />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td  style="width:50%">
                                                                    <asp:LinkButton ID="hlNuevoAvance" ForeColor="#003399"  runat="server" Font-Underline="True" 
                                                                        onclick="hlNuevoAvance_Click">Nuevo Avance</asp:LinkButton>
                                                                </td>
                                                                <td style="width:50%">
                                                                    <asp:LinkButton ID="lbEliminarAvance" ForeColor="#003399"  runat="server" Font-Underline="True" 
                                                                        onclick="lbEliminarAvance_Click" Visible='<%# iif(Eval("fechaAvance") = "No hay ningún Avance", "False","True") %>'>Eliminar</asp:LinkButton>
                                                                </td>
                                                            </tr>
                                                            </table>
                                                    </ItemTemplate><ItemStyle Width="42%" HorizontalAlign="Left" /></asp:TemplateField>
                                                    <asp:BoundField HeaderText="Observaciones" DataField="observaciones" >
                                                        <ItemStyle Width="16%" HorizontalAlign="Center" /></asp:BoundField>
                                                    <asp:CommandField SelectText="" ShowSelectButton="True" />
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    <table style="width:100%;">
                                                        <tr>
                                                            <td style="width:20%">
                                                                No hay registros.</td>
                                                            <td style="width:70%">
                                                                <asp:LinkButton ID="lbNuevo_Hito" runat="server" ForeColor="#003399" Font-Underline="True"
                                                                    onclick="lbNuevo_Hito_Click">Nuevo Hito</asp:LinkButton>
                                                            </td>
                                                            <td style="width:10%">
                                                                <asp:Image ID="imgNingun0" runat="server" ImageUrl="~/Images/cerrar.gif" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </EmptyDataTemplate>
                                                <HeaderStyle BackColor="#e8eef7" BorderColor="#99BAE2" BorderStyle="Solid" 
                                                    BorderWidth="1px" ForeColor="#3366CC" />
                                                <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" />
                                            </asp:GridView>
                                        </td>
                                        <td style="width: 35%">
                                            <asp:Panel ID="pnlObservaciones" runat="server" Height="200px" 
                                                ScrollBars="Vertical" BorderWidth="1px" BorderColor="#99BAE2" Width="98%" 
                                                HorizontalAlign="Center" >
                                                <br />
                                            <asp:DataList ID="dlObservaciones" runat="server" Width="90%">
                                                <ItemTemplate>
                                                    <table class="contornotabla"  Width="100%">
                                                        <tr>
                                                            <td width="10%">
                                                                &nbsp;</td>
                                                            <td width="90%">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="10%" align="center">
                                                                <asp:Label ID="lblNro" runat="server" Text='<%# Eval("nro") %>' Font-Bold="True" ForeColor="#3366CC"></asp:Label>
                                                                </td>
                                                            <td width="90%"  align="left">
                                                                Fecha:
                                                                <asp:Label ID="lblFecha" runat="server" ForeColor="Black" 
                                                                    Text='<%# Eval("fecha") %>' />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="10%" rowspan="3">
                                                                </td>
                                                            <td width="90%" align="left">
                                                                
                                                                <asp:Label ID="revisor" runat="server" Font-Bold="True" 
                                                                    ForeColor="Black" Visible="false" Text='<%# Eval("revisor") %>' />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="90%" align="left">
                                                                <asp:Label ID="descripcion" runat="server" ForeColor="#003399" 
                                                                    Text='<%# Eval("descripcion") %>' />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="90%" align="left">
                                                                Documento:
                                                                <asp:HyperLink ID="hlDocumento" runat="server" Target ="_blank" Font-Underline="True" NavigateUrl='<%# Eval("archivo") %>' Text='<%# Eval("documento") %>' />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="10%">
                                                                &nbsp;</td>
                                                            <td width="90%">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <br />
                                                </ItemTemplate>
                                            </asp:DataList>
                                            
                                            </asp:Panel>
                                            <br />
                                            <asp:Panel ID="pnlInsertarObs" runat="server" Height="130px" Width="98%" 
                                                BorderWidth="1px" BorderColor="#99BAE2" >
                                                <table style="width: 90%;" align="center">
                                                    <tr>
                                                        <td style="width:10%" valign="top">
                                                            &nbsp;</td>
                                                        <td style="width:90%">
                                                        
                                                            <asp:HiddenField ID="hfDocumento" runat="server" />
                                                            <asp:Label ID="lblDocumento" runat="server" Text="" ForeColor="Red"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width:10%" valign="top">
                                                            Observación:
                                                        </td>
                                                        <td style="width:90%">
                                                            <asp:TextBox ID="txtObs" runat="server" Height="35px" Width="98%" 
                                                                TextMode="MultiLine"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width:10%">
                                                            Documento:</td>
                                                        <td style="width:90%">                                                            
                                                            <asp:FileUpload ID="afuObservacion" runat="server" BorderStyle="Solid"  BorderWidth="1px"  />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" style="width:100%">
                                                            <asp:Button ID="cmdGuardar" runat="server" Text="            Guardar" 
                                                                CssClass="guardar_prp" Height="35px" Width="100px" ValidationGroup="Guardar" 
                                                                ToolTip="Guardar Observación" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr valign="top">
                                        <td style="width: 65%" align="right">
                                            <asp:LinkButton ID="lbVerEliminados" runat="server" Font-Underline="True" 
                                                ForeColor="#003399">Ver Eliminados</asp:LinkButton>
                                        </td>
                                        <td style="width: 35%">
                                            &nbsp;</td>
                                    </tr>
                                </table>
                           </asp:View>
                           <asp:View ID="Tab4" runat="server" >
                                <table>
                                    <tr>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label4" runat="server" Text="Estados:" Font-Bold="True"></asp:Label></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label5" runat="server" Text="Pendiente"></asp:Label></td>
                                                    <td>
                                                        <asp:Image ID="Image1" runat="server" ImageUrl="../images/inv_pendiente.png" /></td>
                                                    <td>
                                                        <asp:Label ID="Label6" runat="server" Text="Observado"></asp:Label></td>
                                                    </td>
                                                    <td>
                                                        <asp:Image ID="Image2" runat="server" ImageUrl="../images/inv_observado.png" /></td>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label7" runat="server" Text="Rechazado"></asp:Label></td>
                                                    </td>
                                                    <td>
                                                        <asp:Image ID="Image3" runat="server" ImageUrl="../images/inv_cancelado.png" /></td>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label8" runat="server" Text="Confirmado"></asp:Label></td>
                                                    </td>
                                                    <td>
                                                        <asp:Image ID="Image4" runat="server" ImageUrl="../images/inv_confirmado.png" /></td>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:GridView ID="gvEstadosInvestigaciones" runat="server" AutoGenerateColumns="False" 
                                                BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" 
                                                CellPadding="3">
                                                <RowStyle ForeColor="#000066" />
                                                <Columns>
                                                    <asp:BoundField HeaderText="# Revisores" />
                                                    <asp:BoundField DataField="Coodinador_Instancia" HeaderText="Tipo Revisor" />
                                                    <asp:BoundField DataField="idEstado" HeaderText="Estado Revisión" />
                                                    <asp:BoundField DataField="fechareg" HeaderText="Fecha" />
                                                </Columns>
                                               <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" />
                                                <HeaderStyle BackColor="#e8eef7" ForeColor="#3366CC" BorderColor="#99BAE2" BorderStyle="Solid" BorderWidth="1px" />
                                                <EditRowStyle BackColor="#999999" />
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                           </asp:View>
                           </asp:MultiView>
                    </asp:Panel>
                    <asp:ImageButton ID="btnMostrarPopup" runat="server" Height="5px" CssClass="hidden" />
                    <asp:Panel ID="pnlPopContenedor" runat="server" Style="display: none;width:350px" CssClass="modalPopup">
                        <asp:Panel ID="pnlPopCabecera" runat="server" Style="cursor: move;background-color:#DDDDDD;border:solid 1px Gray;color:Black;height:30px">
                            
                            <table style="width: 100%;background-color:White;">
                            <tr>
                                <td style="width: 98%">
                                    <div style="background-color: #e8eef7; color: #3366CC; font-weight: bold;" >
                                        <p>Nuevo Hito</p>
                                    </div>
                                </td>
                                <td style="width: 2%">
                                    <asp:ImageButton ID="ibtnCerrar" runat="server" 
                                        ImageUrl="~/images/cerrar.gif" />
                                </td>
                            </tr>
                            </table>
                        </asp:Panel>
                        <table style="width: 100%;background-color:White;height:160px">
                            <tr>
                                <td style="width: 20%" valign="top">
                                    <asp:HiddenField ID="hfHito_id" runat="server" />
                                </td>
                                <td style="width: 70%" valign="top">
                                    <asp:HiddenField ID="hfTransaccion" runat="server" />
                                </td>
                                <td style="width: 10%">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%" valign="top">
                                    Descripción
                                </td>
                                <td style="width: 70%" valign="top">
                                    <asp:TextBox ID="txtDesHito" runat="server" TextMode="MultiLine" 
                                        Height="50px" Width="98%"></asp:TextBox>
                                        
                                </td>
                                <td style="width: 10%" valign="top">
                                    <asp:RequiredFieldValidator 
                                    ID="frvDesHito" runat="server" ControlToValidate="txtDesHito" 
                                    ErrorMessage="Debe de ingresar una Descripción" ValidationGroup="GuardarHito">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%">
                                    Fecha:
                                </td>
                                <td style="width: 70%">                                
                                    <asp:TextBox runat="server" ID="txtFechaHito" />
                                    <asp:ImageButton runat="Server" ID="ibtnCalendario" ImageUrl="~/Images/Calendar_scheduleHS.png" AlternateText="Click to show calendar" /><br />
                                    
                                    </td>
                                <td style="width: 10%">
                                    <asp:RequiredFieldValidator 
                                    ID="rfvFechaHito" runat="server" ControlToValidate="txtFechaHito" 
                                    ErrorMessage="Debe de ingresar una Fecha" ValidationGroup="GuardarHito">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%">
                                    &nbsp;
                                </td>
                                <td style="width: 70%" align="center">
                                    <asp:Button ID="btnGuardarHito" runat="server" Text="            Guardar" 
                                                                CssClass="guardar_prp" Height="35px" Width="100px" ValidationGroup="GuardarHito" 
                                                                ToolTip="Guardar Hito" />
                                </td>
                                <td style="width: 10%">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <ajaxtoolkit:modalpopupextender ID="mpeDescripcion" runat="server" 
                    TargetControlID="btnMostrarPopup"
                    PopupControlID="pnlPopContenedor"
                    BackgroundCssClass="modalBackground"
                    DropShadow="true"
                    PopupDragHandleControlID="pnlPopCabecera" 
                    />
                    <asp:ImageButton ID="ibtnMostrarAvance" runat="server" Height="5px" CssClass="hidden" />
                    <asp:Panel ID="pnlPopContenedorAvance" runat="server" Style="display: none;width:350px" CssClass="modalPopup">
                        <asp:Panel ID="pnlPopCabeceraAvance" runat="server" Style="cursor: move;background-color:#DDDDDD;border:solid 1px Gray;color:Black;height:30px">
                            
                            <table style="width: 100%;background-color:White;">
                            <tr>
                                <td style="width: 98%">
                                    <div style="background-color: #e8eef7; color: #3366CC; font-weight: bold;" >
                                        <p>Nuevo Avance</p>
                                    </div>
                                </td>
                                <td style="width: 2%">
                                    <asp:ImageButton ID="ibtnCerrarAvance" runat="server" 
                                        ImageUrl="~/images/cerrar.gif" />
                                </td>
                            </tr>
                            </table>
                        </asp:Panel>
                        <table style="width: 100%;background-color:White;height:160px">
                            <tr>
                                <td style="width: 20%" valign="top">
                                    <asp:HiddenField ID="hfAvance_id" runat="server" />
                                </td>
                                <td style="width: 70%" valign="top">
                                    <asp:TextBox ID="txtDocAvance" runat="server" CssClass="hidden"></asp:TextBox>                                    
                                </td>
                                <td style="width: 10%">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%">
                                    Fecha:
                                </td>
                                <td style="width: 70%">                                
                                    <asp:TextBox runat="server" ID="txtFechaAvance" />
                                    <asp:ImageButton runat="Server" ID="ibtnCalendarAvance" ImageUrl="~/Images/Calendar_scheduleHS.png" AlternateText="Click to show calendar" /><br />
                                    
                                    </td>
                                <td style="width: 10%">
                                    <asp:RequiredFieldValidator 
                                    ID="rfvFechaAvance" runat="server" ControlToValidate="txtFechaAvance" 
                                    ErrorMessage="Debe de ingresar la Fecha del Avance" ValidationGroup="GuardarAvance">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%">
                                    Documento:
                                </td>
                                <td style="width: 70%">                               
                                    
                                    <asp:FileUpload ID="afuAvance" runat="server" BorderStyle="Solid"  BorderWidth="1px"  />
                                </td>
                                <td style="width: 10%">
                                    
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%">
                                    &nbsp;
                                </td>
                                <td style="width: 70%" align="center">
                                    <asp:Button ID="btnGuardarAvance" runat="server" Text="            Guardar" 
                                    CssClass="guardar_prp" Height="35px" Width="100px" ValidationGroup="GuardarAvance" 
                                    ToolTip="Guardar Avance" />
                                </td>
                                <td style="width: 10%">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <ajaxtoolkit:modalpopupextender ID="mpeAvance" runat="server" 
                    TargetControlID="ibtnMostrarAvance"
                    PopupControlID="pnlPopContenedorAvance"
                    BackgroundCssClass="modalBackground"
                    DropShadow="true"
                    PopupDragHandleControlID="pnlPopCabeceraAvance" 
                    />
                    <asp:ImageButton ID="ibtnMostrarPopUpRevisor" runat="server" Height="5px" CssClass="hidden" />
                    <asp:Panel ID="pnlContedorRevisor" runat="server" Style="display: none;width:350px" CssClass="modalPopup">
                        <asp:Panel ID="pnlCabeceraRevisor" runat="server" Style="cursor: move;background-color:#DDDDDD;border:solid 1px Gray;color:Black;height:30px">
                            
                            <table style="width: 100%;background-color:White;">
                            <tr>
                                <td style="width: 98%">
                                    <div style="background-color: #e8eef7; color: #3366CC; font-weight: bold;" >
                                        <asp:Label ID="lblTitPopUp" runat="server" Text="Observación"></asp:Label>
                                    </div>
                                </td>
                                <td style="width: 2%">
                                    <asp:ImageButton ID="ImageButton2" runat="server" 
                                        ImageUrl="~/images/cerrar.gif" />
                                </td>
                            </tr>
                            </table>
                        </asp:Panel>
                        <table style="width: 100%;background-color:White;height:160px">
                            <tr>
                                <td style="width: 20%" valign="top">
                                </td>
                                <td style="width: 70%" valign="top">
                                    <asp:HiddenField ID="hfTipoRevisor" runat="server" />
                                </td>
                                <td style="width: 10%">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%" valign="top">
                                    Descripción
                                </td>
                                <td style="width: 70%" valign="top">
                                    <asp:TextBox ID="txtRevisor" runat="server" TextMode="MultiLine" 
                                        Height="100px" Width="98%"></asp:TextBox>
                                        
                                </td>
                                <td style="width: 10%" valign="top">
                                    <asp:RequiredFieldValidator 
                                    ID="rfvRevisor" runat="server" ControlToValidate="txtRevisor" 
                                    ErrorMessage="Faltan ingresar datos." ValidationGroup="GuardarRevisor">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label9" runat="server" Text="Documento"></asp:Label>
                                 </td>
                                <td>
                                    <asp:FileUpload 
                                        ID="FileArchivo" 
                                        runat="server" 
                                        BorderStyle="Solid" 
                                        BorderWidth="1px" 
                                        Width="98%" 
                                        TabIndex="17" />
                                </td>
                                 <td style="width: 10%" valign="top">
                                    <asp:RequiredFieldValidator 
                                    ID="rfvFile" 
                                    runat="server" 
                                    ControlToValidate="FileArchivo" 
                                    ErrorMessage="Archivo Obligatorio" 
                                    ValidationGroup="GuardarRevisor">*
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%">
                                    &nbsp;
                                </td>
                                <td style="width: 70%" align="center">
                                    <asp:Button ID="btnGuardarRevisor" runat="server" Text="            Guardar" 
                                                                CssClass="guardar_prp" Height="35px" Width="100px" ValidationGroup="GuardarRevisor" 
                                                                ToolTip="Guardar" />
                                </td>
                                <td style="width: 10%">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <ajaxtoolkit:modalpopupextender ID="mpeRevisor" runat="server" 
                    TargetControlID="ibtnMostrarPopUpRevisor"
                    PopupControlID="pnlContedorRevisor"
                    BackgroundCssClass="modalBackground"
                    DropShadow="true"
                    PopupDragHandleControlID="pnlCabeceraRevisor" 
                    />
                    
                    <asp:ImageButton ID="ibtnMostrarPopUpInforme" runat="server" Height="5px" CssClass="hidden" />
                    <asp:Panel ID="pnlContedorInforme" runat="server" Style="display: none; Width:350px"  CssClass="modalPopup">
                        <asp:Panel ID="pnlCabeceraInforme" runat="server" Style="cursor: move;background-color:#DDDDDD;border:solid 1px Gray;color:Black;height:30px">
                            
                            <table style="width: 100%;background-color:White;">
                            <tr>
                                <td style="width: 98%">
                                    <div style="background-color: #e8eef7; color: #3366CC; font-weight: bold;" >
                                        <asp:Label ID="lblTitPopUpInforme" runat="server" Text="Observación"></asp:Label>
                                    </div>
                                </td>
                                <td style="width: 2%">
                                    <asp:ImageButton ID="ImageButton3" runat="server" 
                                        ImageUrl="~/images/cerrar.gif" />
                                </td>
                            </tr>
                            </table>
                        </asp:Panel>
                        <table style="width: 100%;background-color:White;height:160px">
                            <tr>
                                <td style="width: 20%" valign="top">
                                </td>
                                <td style="width: 70%" valign="top">
                                    <asp:HiddenField ID="hfTipoInforme" runat="server" />
                                </td>
                                <td style="width: 10%">
                                    &nbsp;
                                </td>
                            </tr>
                             <tr>
                                <td>
                                    <asp:Label ID="Label2" runat="server" Text="Documento"></asp:Label>
                                 </td>
                                <td>
                                    <asp:FileUpload 
                                        ID="FileArchivoInforme" 
                                        runat="server" 
                                        BorderStyle="Solid" 
                                        BorderWidth="1px" 
                                        Width="98%" 
                                        TabIndex="17" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%">
                                    &nbsp;
                                </td>
                                <td style="width: 70%" align="center">
                                    <asp:Button ID="btnGuardarInforme" runat="server" Text="            Guardar" 
                                                                CssClass="guardar_prp" Height="35px" Width="100px" ValidationGroup="btnGuardarInforme" 
                                                                ToolTip="Guardar" />
                                </td>
                                <td style="width: 10%">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <ajaxtoolkit:modalpopupextender ID="mpeInforme" runat="server" 
                    TargetControlID="ibtnMostrarPopUpInforme"
                    PopupControlID="pnlContedorInforme"
                    BackgroundCssClass="modalBackground"
                    DropShadow="true"
                    PopupDragHandleControlID="pnlCabeceraInforme" 
                    />
                    
                    
                    
                    <asp:ImageButton ID="ibtnMostrarPopUpResolucion" runat="server" Height="5px" CssClass="hidden" />
                    <asp:Panel ID="pnlContedorResolucion" runat="server" Style="display: none; Width:350px"  CssClass="modalPopup">
                        <asp:Panel ID="pnlCabeceraResolucion" runat="server" Style="cursor: move;background-color:#DDDDDD;border:solid 1px Gray;color:Black;height:30px">
                            
                            <table style="width: 100%;background-color:White;">
                            <tr>
                                <td style="width: 98%">
                                    <div style="background-color: #e8eef7; color: #3366CC; font-weight: bold;" >
                                        <asp:Label ID="lblTitPopUpResolucion" runat="server" Text="Observación"></asp:Label>
                                    </div>
                                </td>
                                <td style="width: 2%">
                                    <asp:ImageButton ID="ImageButton4" runat="server" 
                                        ImageUrl="~/images/cerrar.gif" />
                                </td>
                            </tr>
                            </table>
                        </asp:Panel>
                        <table style="width: 100%;background-color:White;height:160px">
                            <tr>
                                <td style="width: 20%" valign="top">
                                </td>
                                <td style="width: 70%" valign="top">
                                    <asp:HiddenField ID="hfTipoResolucion" runat="server" />
                                </td>
                                <td style="width: 10%">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%" valign="top">
                                    Nº Resolución
                                </td>
                                <td style="width: 70%" valign="top">
                                    <asp:TextBox ID="txtNumeroRes" runat="server" TextMode="MultiLine" 
                                        Height="100px" Width="98%"></asp:TextBox>
                                        
                                </td>
                                <td style="width: 10%" valign="top">
                                    <asp:RequiredFieldValidator 
                                    ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNumeroRes" 
                                    ErrorMessage="Faltan ingresar datos." ValidationGroup="GuardarResolucion">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                             <tr>
                                <td>
                                    <asp:Label ID="Label3" runat="server" Text="Documento"></asp:Label>
                                 </td>
                                <td>
                                    <asp:FileUpload 
                                        ID="FileArchivoResolucion" 
                                        runat="server" 
                                        BorderStyle="Solid" 
                                        BorderWidth="1px" 
                                        Width="98%" 
                                        TabIndex="17" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%">
                                    &nbsp;
                                </td>
                                <td style="width: 70%" align="center">
                                    <asp:Button ID="btnGuardarResolucion" runat="server" Text="            Guardar" 
                                                                CssClass="guardar_prp" Height="35px" Width="100px" ValidationGroup="GuardarResolucion" 
                                                                ToolTip="Guardar" />
                                </td>
                                <td style="width: 10%">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <ajaxtoolkit:modalpopupextender ID="mpeResolucion" runat="server" 
                    TargetControlID="ibtnMostrarPopUpResolucion"
                    PopupControlID="pnlContedorResolucion"
                    BackgroundCssClass="modalBackground"
                    DropShadow="true"
                    PopupDragHandleControlID="pnlCabeceraResolucion" 
                    />
                    
                    
                    <asp:ImageButton ID="ibtnMostrarPopUpVerEliminados" runat="server" Height="5px" CssClass="hidden" />
                    <asp:Panel ID="pnlContedorVerEliminados" runat="server" Style="display:none;width:500px" CssClass="modalPopup">
                        <asp:Panel ID="pnlCabeceraVerEliminados" runat="server" Style="cursor: move;background-color:#DDDDDD;border:solid 1px Gray;color:Black;height:30px">
                            <table style="width: 100%;background-color:White;">
                                <tr>
                                    <td style="width: 98%">
                                        <div style="background-color: #e8eef7; color: #3366CC; font-weight: bold;" >
                                            <asp:Label ID="lblTitPopUpVerEliminados" runat="server" Text="Eliminados"></asp:Label>
                                        </div>
                                    </td>
                                    <td style="width: 2%">
                                        <asp:ImageButton ID="ibtnCerrarPopUpVerEliminados" runat="server" 
                                            ImageUrl="~/images/cerrar.gif" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <table style="width: 100%;background-color:White;height:320px">
                            <tr>
                                <td valign="top" align="center">
                                    <asp:Panel ID="pnlEliminados" runat="server" Height="300px">
                                    <br/>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlEliminadosObs" runat="server" Height="300px" Style="display:none">
                                                <br />
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="Panel1" runat="server" Height="270px" ScrollBars="Vertical" BorderWidth="1px" BorderColor="#99BAE2" Width="98%" 
                                                HorizontalAlign="Center" >
                                                <br/>
                                                    <asp:DataList ID="dlEliminadosObs" runat="server" Width="90%">
                                                <ItemTemplate>
                                                    <table class="contornotabla"  Width="100%">
                                                        <tr>
                                                            <td width="10%">
                                                                &nbsp;</td>
                                                            <td width="90%">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="10%" align="center">
                                                                <asp:Label ID="lblNro" runat="server" Text='<%# Eval("nro") %>' Font-Bold="True" ForeColor="#3366CC"></asp:Label>
                                                                </td>
                                                            <td width="90%"  align="left">
                                                                Fecha:
                                                                <asp:Label ID="lblFecha" runat="server" ForeColor="Black" 
                                                                    Text='<%# Eval("fecha") %>' />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="10%" rowspan="3">
                                                                </td>
                                                            <td width="90%" align="left">
                                                                
                                                                <asp:Label ID="revisor" runat="server" Font-Bold="True" 
                                                                    ForeColor="Black" Visible="false" Text='<%# Eval("revisor") %>' />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="90%" align="left">
                                                                <asp:Label ID="descripcion" runat="server" ForeColor="#003399" 
                                                                    Text='<%# Eval("descripcion") %>' />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="90%" align="left">
                                                                Documento:
                                                                <asp:HyperLink ID="hlDocumento" runat="server" Target ="_blank" Font-Underline="True" NavigateUrl='<%# Eval("archivo") %>' Text='<%# Eval("documento") %>' />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="10%">
                                                                &nbsp;</td>
                                                            <td width="90%">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <br />
                                                </ItemTemplate>
                                            </asp:DataList>
                                                    </asp:Panel>                                                    
                                                </td>
                                            </tr>
                                            <tr>
                                                <td Align="right">
                                                    <asp:ImageButton ID="ibtnRegresar" runat="server" 
                                                        ImageUrl="~/Images/forward.gif" ToolTip="Regresar" />
                                                </td>
                                            </tr>
                                        </table>
                                            
                                            
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <ajaxtoolkit:modalpopupextender ID="mpeVerEliminados" runat="server" 
                    TargetControlID="ibtnMostrarPopUpVerEliminados"
                    PopupControlID="pnlContedorVerEliminados"
                    BackgroundCssClass="modalBackground"
                    DropShadow="true"
                    PopupDragHandleControlID="pnlCabeceraVerEliminados" 
                    />
                </td>
            </tr>
        </table>
        <ajaxtoolkit:calendarextender ID="calendarButtonExtender"  runat="server" PopupButtonID="ibtnCalendario" TargetControlID="txtFechaHito" Format="dd/MM/yyyy" ></ajaxtoolkit:calendarextender>
        <ajaxtoolkit:calendarextender ID="cbeAvance"  runat="server" PopupButtonID="ibtnCalendarAvance" TargetControlID="txtFechaAvance" Format="dd/MM/yyyy" ></ajaxtoolkit:calendarextender>
        </ContentTemplate>
            
            <Triggers>
                <asp:PostBackTrigger ControlID="btnNuevo"/>
                <asp:PostBackTrigger ControlID="gvInvestigacion"/>
                <asp:PostBackTrigger ControlID="btnConforme"/>
                <asp:PostBackTrigger ControlID="btnObservar"/>
                <asp:PostBackTrigger ControlID="btnRechazar"/>
                <asp:PostBackTrigger ControlID="btnEnviar"/>
                <asp:PostBackTrigger ControlID="btnGuardarRevisor"/>
                <asp:PostBackTrigger ControlID="btnGuardarAvance" />
                <asp:PostBackTrigger ControlID="cmdGuardar" />
                <asp:PostBackTrigger ControlID="ddlEstadoRevisor" />
                
                  
                <asp:PostBackTrigger ControlID="btnGuardarInforme"/>
                <asp:PostBackTrigger ControlID="btnGuardarResolucion"/>
                
            </Triggers>
            
        </asp:UpdatePanel>
 
    </div>
        <asp:ValidationSummary ID="vsValidaHito" runat="server" 
        ShowMessageBox="True" ShowSummary="False" ValidationGroup="GuardarHito" />
        <asp:ValidationSummary ID="vsValidaAvance" runat="server" 
        ShowMessageBox="True" ShowSummary="False" ValidationGroup="GuardarAvance" />
        
        <asp:ValidationSummary ID="vsValidarRevisor" runat="server" 
        ShowMessageBox="True" ShowSummary="False" ValidationGroup="GuardarRevisor" />
    </form>
</body>
</html>
