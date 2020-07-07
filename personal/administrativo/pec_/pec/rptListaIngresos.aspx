<%@ Page Language="VB" AutoEventWireup="false" CodeFile="rptListaIngresos.aspx.vb" Inherits="rpteinscritoseventocargo" Theme="Acero" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Lista de Eventos</title>
    <link href="../../../private/estilo.css" rel="stylesheet" type="text/css" media="screen" />
    <script type="text/javascript" language="JavaScript" src="../../../private/funciones.js"></script>
    <script type="text/javascript" language="JavaScript" src="../../../private/jq/jquery-1.4.2.min.js"></script>
	<script type="text/javascript" language="JavaScript" src="../../../private/jq/lbox/thickbox.js"></script>
	<link rel="stylesheet" href="../../../private/jq/lbox/thickbox.css" type="text/css" media="screen" />
	<script type="text/javascript" language="javascript">
	    function PintarFilaElegida(obj) {
	        if (obj.style.backgroundColor == "white") {
	            obj.style.backgroundColor = "#E6E6FA"//#395ACC
	        }
	        else {
	            obj.style.backgroundColor = "white"
	        }
	    }
	</script>
</head>
<body>
    <form id="form1" runat="server">
    <p class="usatTitulo">Ingresos</p>
    
            <asp:Button ID="cmdExportar" runat="server" SkinID="BotonAExcel" 
        Text="Exportar" />
&nbsp;<br />
    <br />
                <asp:GridView ID="grwListaPagos" runat="server"
                    AutoGenerateColumns="False" CellPadding="3" 
                    SkinID="skinGridViewLineas" Width="98%" ShowFooter="True">
                    <Columns>
                        <asp:BoundField HeaderText="Nro" >
                            <ItemStyle Width="1%"/>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Participante" DataField="participante" >
                            <ItemStyle Width="34%"/>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Tipo Doc." DataField="tipoDocIdent_Pso">
                            <ItemStyle HorizontalAlign="Center" Width="8%"/>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Nro. Doc." DataField="numeroDocIdent_Pso">
                            <ItemStyle HorizontalAlign="Center" Width="8%"/>
                        </asp:BoundField>            
                        <asp:BoundField DataField="fecha_Pago" DataFormatString="{0:dd/MM/yyyy HH:mm}"
                            HeaderText="Fecha Pago" >
                            <ItemStyle HorizontalAlign="Center" Width="14%"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="usuario_per" 
                            HeaderText="Operador" >
                            <ItemStyle HorizontalAlign="Center" Width="8%"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="estado_Deu" 
                            HeaderText="Estado" Visible ="false" >
                            <ItemStyle HorizontalAlign="Center" Width="8%"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="descripcion_Tdo" 
                            HeaderText="Documento" >
                            <ItemStyle HorizontalAlign="Center" Width="16%"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="montoTotal_Deu" HeaderText="Monto" >
                            <ItemStyle HorizontalAlign="Right" Width="6%"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="pago_Deu" HeaderText="Pago" >
                            <ItemStyle HorizontalAlign="Right" Width="6%"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="saldo_Deu" 
                            HeaderText="Saldo" Visible="False" >
                            <ItemStyle HorizontalAlign="Right" Width="8%"/>
                        </asp:BoundField>
                    </Columns>
                    <EmptyDataTemplate>
                        <table style="width:100%;">
                            <tr>
                                <td style="width:90%">
                                    No se encontraron pagos realizados en el Campus Virtual.</td>
                                <td style="width:10%">
                                    <asp:Image ID="imgNingun" runat="server" ImageUrl="~/Images/cerrar.gif" />
                                </td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <PagerStyle BackColor="Silver" HorizontalAlign="Center" />
                    <HeaderStyle BackColor="#0B3861" ForeColor="White" Height="25px" />                
                    <RowStyle Height="22px" />
                </asp:GridView>
</form>

</body>
</html>