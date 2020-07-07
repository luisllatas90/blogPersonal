<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmevaluaciones.aspx.vb" Inherits="frmevaluaciones" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registro de notas</title>
    <script language="javascript" src="../../../private/funcionesaulavirtual.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
    function validarnota(ctrl){
	  if(eval(ctrl.value)>eval(form1.hdlimiteval_aev.value)){
		alert("La nota no debe ser mayor a " + form1.hdlimiteval_aev.value)
		ctrl.focus()
		ctrl.value=0
		return(false)
	  }
    }
</script>
    <link href="../../../private/estiloaulavirtual.css" rel="stylesheet" 
        type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <p class="e1">
        Registro de evaluación del participante</p>
    <table style="width:100%; height:95%">
        <tr>
            <td style="width:15%;height:5%">
                <b>Criterios:</b></td>
            <td style="width:30%;">
                <asp:DropDownList ID="dtcodigo_aev" runat="server" AutoPostBack="True">
                </asp:DropDownList>
            </td>
            <td style="width:55%;">
            <asp:Button ID="cmdAgregar" runat="server" CssClass="agregar2" 
                    Text="   Agregar" UseSubmitBehavior="False" Visible="False" />
&nbsp;<asp:Button ID="cmdModificar" runat="server" CssClass="modificar2" Text="    Modificar" 
                    UseSubmitBehavior="False" Visible="False" />
&nbsp;<asp:Button ID="cmdEliminar" runat="server" CssClass="eliminar2" Text="    Eliminar" 
                    UseSubmitBehavior="False" Visible="False" />
            &nbsp;<asp:Button ID="cmdReporte" runat="server" Text="    Ver reporte" 
                    CssClass="exportar" />
            </td>
        </tr>
        <tr>
            <td colspan="3" style="width:100%;height:80%">
                <table style="width:100%;height:100%;" border="1" cellpadding="3" cellspacing="0" bordercolor="#808080">
                    <tr class="etabla2">
                        <td width="5%">
                            &nbsp;#&nbsp;</td>
                        <td width="10%">
                            Código</td>
                        <td width="60%">
                            Participante</td>
                        <td width="15%">
                            Tipo</td>
                        <td width="10%">
                            Calificativo</td>
                    </tr>
                    <tr>
                        <td colspan="5" style="width:100%;height:100%">
                            <div style="width:100%;height:100%" id="listadiv">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                                CellPadding="4" ForeColor="#333333" GridLines="None" ShowHeader="False" 
                                Width="100%">
                                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                <RowStyle BackColor="#E3EAEB" />
                                <Columns>
                                    <asp:BoundField HeaderText="#">
                                        <ItemStyle Width="5%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="idusuario" HeaderText="Código"></asp:BoundField>
                                    <asp:BoundField HeaderText="Participante" DataField="nombreusuario">
                                        <ItemStyle Width="10%" />
                                        <ItemStyle Width="60%" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Tipo" DataField="nombretipousuario">
                                        <ItemStyle Width="15%" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Evaluación" DataField="descripcion_evp">
                                        <ItemStyle Width="10%" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                </Columns>
                                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#7C6F57" />
                                <AlternatingRowStyle BackColor="White" />
                            </asp:GridView>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="width:100%;height:10%" colspan="3" align="right">
                <asp:Button ID="cmdGuardar" runat="server" Text="Guardar todo" 
                    UseSubmitBehavior="False" Visible="False" />
                <asp:HiddenField ID="hdtipoval_aev" runat="server" />
                <asp:HiddenField ID="hdlimiteval_aev" runat="server" />
            </td>
        </tr>
    </table>
    </form>
    </body>
</html>
