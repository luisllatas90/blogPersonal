<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmAdminEscolaridad.aspx.vb" Inherits="escolaridad_frmAdminEscolaridad" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <link href="../../private/estilo.css?x=m1" rel="stylesheet" type="text/css" /> 
        <script type="text/javascript"  language="JavaScript" src="../../private/funciones.js?x=23"></script>
 
        
        <script src="../../private/PopCalendar.js" language="text/javascript" type="text/javascript"></script>
        <script src="../../private/jq/jquery.js" type="text/javascript"></script>
        <script src="../../private/jq/jquery.mascara.js" type="text/javascript"></script>
        
        <script language="javascript" type="text/javascript">
                $(document).ready(function() {
                    jQuery(function($) {
                        $("#txtdesde").mask("99/99/9999");
                    });
                })

                $(document).ready(function() {
                    jQuery(function($) {
                        $("#txthasta").mask("99/99/9999");
                    });
                })

                $(document).ready(function() {
                    jQuery(function($) {
                        $("#txtFechaCierre").mask("99/99/9999");
                    });
                })

                $(document).ready(function() {
                    jQuery(function($) {
                        $("#txtfechanolab").mask("99/99/9999");
                    });
                })

                $(document).ready(function() {
                    jQuery(function($) {
                        $("#txtFInicio_Pem").mask("99/99/9999");
                    });
                })

                $(document).ready(function() {
                    jQuery(function($) {
                        $("#txtFFin_Pem").mask("99/99/9999");
                    });
                })

                $(document).ready(function() {
                    jQuery(function($) {
                        $("#txtFInicio_Pap").mask("99/99/9999");
                    });
                })

                $(document).ready(function() {
                    jQuery(function($) {
                        $("#txtFFin_Pap").mask("99/99/9999");
                    });
                })
        </script>
        
         <script type="text/javascript" language="javascript">

             function MarcarCursos(obj) {
                 //asignar todos los controles en array
                 var arrChk = document.getElementsByTagName('input');
                 for (var i = 0; i < arrChk.length; i++) {
                     var chk = arrChk[i];
                     //verificar si es Check
                     if (chk.type == "checkbox") {
                         //** dguevara: 08.11.2013
                         //Bloque: parar marcar con check, siempre y cuando no este desabilitado.
                         if (chk.disabled == false) {
                             chk.checked = obj.checked;  //este es para marcar los checks
                             //Bloque: para pintar la fila con el check.
                             if (chk.id != obj.id) {
                                 PintarFilaMarcada(chk.parentNode.parentNode, obj.checked)
                             }
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
        
</head>
<body>
    <form id="form1" runat="server">
    <% Response.Write(ClsFunciones.CargaCalendario())%>
    <div>
        <table style="width: 100%" class="contornotabla">
            <tr>
                <td bgcolor="#EFF3FB" height="35px" colspan="5">
                    <b>
                    <asp:Label ID="Label11" runat="server" Text="GESTION DE SOLICITUDES ESCOLARIDAD"></asp:Label></b>
                </td>
            </tr>  
            <tr>
                <td align="left" height="40px" class="contornotabla" colspan="5" style="background-color:#FFFFCC";>
                    <asp:Label ID="lblInstrucciones" runat="server" Text=""></asp:Label>   
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Año"></asp:Label>
                </td>
                <td colspan="3">
                     <asp:DropDownList ID="ddlanio" Width="150px" runat="server" AutoPostBack="True">
                     </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Tipo Trabajador"></asp:Label>
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="ddltipotrabajador" Width="250px" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                    <asp:Label ID="Label3" runat="server" Text="Dedicación"></asp:Label>
                    <asp:DropDownList ID="ddldedicacion" runat="server"  Width="250px" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                                <asp:Label ID="Label4" runat="server" Text="Trabajador"></asp:Label>
                            </td>
                <td colspan="3">
                                <asp:DropDownList ID="ddltrabajador" Width="570px" runat="server" 
                                    AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
            </tr>
            <tr>
                <td>
                                <asp:Label ID="Label5" runat="server" Text="Fecha Nacimiento"></asp:Label>
                            </td>
                <td>
                              <asp:Label ID="Label7" runat="server" Text="desde "></asp:Label>
                              <asp:TextBox 
                                            ID="txtdesde" 
                                            runat="server" 
                                            Font-Names="Arial" 
                                            Font-Size="X-Small" Width="103px">
                              </asp:TextBox>
                              <input id="btnFechaInicio" onClick="PopCalendar.selectWeekendHoliday(1,1);PopCalendar.show(document.form1.txtdesde,'dd/mm/yyyy')" class="cunia" type="button" />
                              
                              <asp:Label ID="Label8" runat="server" Text="  hasta  "></asp:Label>
                              <asp:TextBox 
                                            ID="txthasta" 
                                            runat="server" 
                                            Font-Names="Arial" 
                                            Font-Size="X-Small" 
                                            Width="103px">
                             </asp:TextBox>
                                <input id="Button1" onClick="PopCalendar.selectWeekendHoliday(1,1);PopCalendar.show(document.form1.txthasta,'dd/mm/yyyy')" class="cunia" type="button" /></td>
            </tr>
             <tr>
                <td>
                                <asp:Label ID="Label6" runat="server" Text="Estado Solicitud"></asp:Label>
                            </td>
                <td>
                                <asp:DropDownList ID="ddlestado" runat="server" AutoPostBack="True" 
                                    BackColor="#99CCFF">
                                    <asp:ListItem Value="T">-- TODOS--</asp:ListItem>
                                    <asp:ListItem Value="P">PENDIENTE</asp:ListItem>
                                    <asp:ListItem Value="a">APROBADOS</asp:ListItem>
                                    <asp:ListItem></asp:ListItem>
                                </asp:DropDownList>
                            </td>
            </tr>
            <tr>
                <td>
                                &nbsp;</td>
                <td colspan="3">
                    
                                <asp:Button ID="btnBuscar" Width="100px" Height="40px" CssClass="buscarEscolaridad" runat="server" Text="  Buscar" />
                                <asp:Button ID="btnExportar" Width="100px" Height="40px" CssClass="exportarEscolaridad" runat="server" Text="  Exportar" />
                    
                </td>
            </tr>
            <tr>
                <td align="left" height="40px" colspan="5" bgcolor="#EFF3FB" height="35px">
                    <table style="width: 100%" class="contornotabla">
                        <tr>
                            <td >
                                <asp:Label ID="lblCantidad" runat="server" Text=""></asp:Label>
                            </td>
                            <td align="right">
                                <asp:Button ID="btnAprobar"  Width="100px" Height="40px" CssClass="aprobarEscolaridad"  runat="server" Text="  Aprobar" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="contornotabla" colspan="5">
                    <!-- Grid para los registros de las solicitudes -->
                    <asp:GridView 
                                ID="gvLista" 
                                runat="server" 
                                Width="100%" 
                                BackColor="White" 
                                BorderColor="#CCCCCC" 
                                BorderStyle="None" 
                                BorderWidth="1px" 
                                CellPadding="3" 
                                AutoGenerateColumns="False" 
                                EmptyDataText="No se encontraron registros.." PageSize="1000">
                        <RowStyle ForeColor="#000066" />
                        <EmptyDataRowStyle BackColor="#FFFF99" ForeColor="#FF3300" />
                        <Columns>
                            <asp:BoundField DataField="codigo_soe" HeaderText="ID" />
                            <asp:BoundField DataField="trabajador" HeaderText="Apellidos Nombres" />
                            <asp:BoundField DataField="derechohabiente" HeaderText="Hijo(a)" />
                            <asp:BoundField DataField="edad" HeaderText="Edad" />
                            <asp:BoundField DataField="fechaNacimiento_dhab" HeaderText="F.Nacimiento" />
                            <asp:BoundField DataField="nombrecentroestudio_soe" HeaderText="Centro Estudios" />
                            <asp:BoundField DataField="grado" HeaderText="Grado" />
                            <asp:BoundField DataField="estado_soe" HeaderText="Estado Solicitud" />
                            <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkHeader" runat="server"  onclick="MarcarCursos(this)" />
                                    </HeaderTemplate>
                                    <ItemTemplate>                
                                        <asp:CheckBox ID="chkElegir" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle Width="5px" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="White" ForeColor="#000066" />
                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                        <SelectedRowStyle BackColor="#FFFFB1" Font-Bold="True" ForeColor="Blue" />
                        <HeaderStyle BackColor="#EFF3FB" Font-Bold="True" ForeColor="Blue" />
                    </asp:GridView>
                </td>
            </tr>  
        </table>
    </div>
    </form>
</body>
</html>
