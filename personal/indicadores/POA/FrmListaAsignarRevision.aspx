<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmListaAsignarRevision.aspx.vb" Inherits="indicadores_POA_FrmListaAsignarRevision" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <%--<meta http-equiv="X-UA-Compatible" content="IE=edge"/>--%>
    <link href="css/estilo_poa.css" rel="stylesheet" type="text/css" media="screen" />
 
    <!-- Librería jQuery requerida por los plugins de JavaScript -->
    <script src="Jquery/jquery-1.12.3.min.js" type="text/javascript"></script>

    <script src="Jquery/jquery-ui-1.12.1.custom/jquery-ui.min.js" type="text/javascript"></script>
    <link href="Jquery/jquery-ui-1.12.1.custom/jquery-ui.min.css" rel="stylesheet" type="text/css" />
    <link href="Jquery/jquery-ui-1.12.1.custom/jquery-ui.theme.css" rel="stylesheet"
        type="text/css" />
        
    <script type="text/javascript">
    "use strict";
    $.datepicker.regional['es'] = {
        closeText: 'Cerrar',
//        prevText: '<Ant',
//        nextText: 'Sig>',
        currentText: 'Hoy',
        monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
        monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'],
        dayNames: ['Domingo', 'Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado'],
        dayNamesShort: ['Dom', 'Lun', 'Mar', 'Mié', 'Juv', 'Vie', 'Sáb'],
        dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá'],
        weekHeader: 'Sm',
        dateFormat: 'dd/mm/yy',
        firstDay: 1,
        isRTL: false,
        showMonthAfterYear: false,
        yearSuffix: ''
    };
    $.datepicker.setDefaults($.datepicker.regional['es']);


    $(document).ready(function() {
        $(".caja_poa").datepicker();
        $("img").click(function() {
            // alert("1");
            var valores = "";

            // Obtenemos todos los valores contenidos en los <td> de la fila

            // seleccionada

            $(this).parents("tr").find("td").each(function() {
                if ($(this).attr("class") == "fec_fin") {
                    valores += $(this).html() + "\n";
                }
            });

            //alert(valores);

            $(this).parents("tr").find("td").each(function() {
            $(this).find("input").val(valores);
            $(this).find("input").css("background", "#ffd5ff");
            });

        })
    })
    
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="titulo_poa">
        <asp:Label ID="Label1" runat="server"
            Text="Asignación de Fecha de Revisión a Proyectos"></asp:Label>
    </div>
    <div class="contorno_poa">
   
        <table width="100%" id="tabla" runat="server">
        <tr style="height:30px;">
        <td width="140px" >Plan Estratégico</td>
        <td width="510px"><asp:DropDownList ID="ddlplan" runat="server" Width="500" AutoPostBack="true"></asp:DropDownList></td>
        <td width="50px"></td>
        <td width="140px">Ejercicio Presupuestal</td>
        <td><asp:DropDownList ID="ddlEjercicio" runat="server" Width="140" AutoPostBack="true"></asp:DropDownList></td>
        <td><asp:Button ID="btnBuscar" runat="server" Text="   Buscar" CssClass="btnBuscar" /></td>
        </tr>
        <tr>
        <td>Plan Operativo Anual</td>
        <td>
        <asp:UpdatePanel id="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:DropDownList ID="ddlPoa" runat="server" Width="500" AutoPostBack="true">
            <asp:ListItem Value="0">--SELECCIONE--</asp:ListItem>
            </asp:DropDownList> 
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlplan" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlEjercicio" EventName="SelectedIndexChanged" />
        </Triggers>
        </asp:UpdatePanel>
        </td>
        <td width="50px"></td>
        <td></td>
        <td></td>
        <td></td>
        </tr>
        <tr>
        <td colspan="6">
            <div runat="server" id="aviso">
                <asp:Label ID="lblrpta" runat="server" Font-Bold="true"></asp:Label>
            </div>
        </td>
        </tr>
        </table>
         <asp:GridView ID="dgvActividades" runat="server" Width="100%" 
            AutoGenerateColumns="False" 
            DataKeyNames="codigo_dap,codigo_poa,codigo_tac,fecfin_dap" CellPadding="4" >
            <Columns>
                <asp:BoundField HeaderText="CODIGO_POA" DataField="codigo_poa" Visible="false"  >
                </asp:BoundField>
                <asp:BoundField HeaderText="NOMBRE DE POA" DataField="nombre_poa"  >
                <HeaderStyle Width="250px" />
                <ItemStyle CssClass="celda_combinada" Width="250px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="TIPO_ACT" DataField="codigo_tac" Visible="false" >
                </asp:BoundField>
                <asp:BoundField HeaderText="COD_ACTIVIDAD" DataField="codigo_acp" Visible="false" >
                <HeaderStyle Width="350px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="PROYECTO" DataField="resumen_acp" >
                <HeaderStyle Width="350px" />
                <ItemStyle CssClass="celda_combinada" />
                </asp:BoundField>
                <asp:BoundField HeaderText="CODIGO_DETALLE ACTIVIDAD" DataField="codigo_dap" Visible="false"  >
                <HeaderStyle Width="350px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="ACTIVIDAD" DataField="descripcion_dap" >
                <HeaderStyle Width="350px" />
                <ItemStyle CssClass="celda_combinada" />
                </asp:BoundField>
                <asp:BoundField HeaderText="INICIO" DataField="fecini_dap" >
                </asp:BoundField>
                <asp:BoundField HeaderText="FIN" DataField="fecfin_dap" ItemStyle-CssClass="fec_fin" >
                </asp:BoundField>
<%--                <asp:CommandField ShowSelectButton="true" ButtonType="Image"  
                    HeaderText="COPIAR" SelectImageUrl="../../images/arrow.gif" 
                    UpdateImageUrl="../../images/arrow.gif" SelectText="Copiar"  >
                <ItemStyle HorizontalAlign="Center" />
                </asp:CommandField>--%>
                <asp:TemplateField HeaderText="P. CONTROL" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate >
                        <img id="img1" name="img1" alt="copiar" src="../../images/arrow.gif" />
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="P. CONTROL">
                    <ItemTemplate>
                        <asp:textbox  ID="txthito_dap" class="caja_poa" Width="80px" 
                        runat="server" Text='<%# Bind("hito_dap") %>' ></asp:textbox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField EditImageUrl="../../images/guardar.gif" HeaderText="GUARDAR" 
                    ShowEditButton="True" ButtonType="image" EditText="Guardar" >
                    <ItemStyle HorizontalAlign="Center" />
                </asp:CommandField>     
            </Columns>
            <EmptyDataTemplate>
                No se Encontraron Registros
            </EmptyDataTemplate>
            <HeaderStyle BackColor="#3871b0" ForeColor="White" Height="25px" /> 
        </asp:GridView>

</div>
    </form>
</body>
</html>
