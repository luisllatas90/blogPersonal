<%@ Page Language="VB"  AutoEventWireup="false" CodeFile="frmConsultarCargosPorCeco.aspx.vb" Inherits="personal_administrativo_pec_frmConsultarCargosPorCeco" Theme="Acero" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Consultar Deudas por Cobrar</title>
    <link href="../../../private/estilo.css" rel="stylesheet" type="text/css" />
    
    <link type="text/css" href="http://jquery-ui.googlecode.com/svn/tags/1.7/themes/redmond/jquery-ui.css" rel="stylesheet" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.3.2/jquery.min.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/jquery-ui.min.js"></script>
    
    <!--Referencias para Paginacion. mvillavicencio 31/07/12 -->
    <script src="../js/jquery-1.4.4.min.js" type="text/javascript"></script>
    <script src="../js/jquery.tablesorter.js" type="text/javascript"></script>
    <script src="../js/jquery.tablesorter.pager.js" type="text/javascript"></script>
    <link href="../themes/blue/style.css" rel="stylesheet" type="text/css" />
    <!-- <script src="../js/jquery-latest.js" type="text/javascript"></script> -->
    <!--------------------------------------------------------------------------->
	   
    <%--Mostrar Dialogo con botones--%>
    <script type="text/javascript">
        $(document).ready(function() {           
            $("#miTabla")
                .tablesorter({ widthFixed: true, widgets: ['zebra'] })
                .tablesorterPager({ container: $("#pager") });
        });

        //Para paginación JQuery mvillavicencio 31/0/12


        function abrirdetalleabonos(id) {
            var url = "frmVerAbonos.aspx?id="+id;
            open(url, '', 'top=300,left=300,width=800,height=300');
        }

        function abriractualizarobservacion(id, usuario, observacion) {
            var url = "frmActualizarObservacionCargo.aspx?id=" + id + "&usuario=" + usuario + "&observacion=" + observacion;
            open(url, '', 'top=300,left=300,width=400,height=200');
        }

        function RefrescarGridDeudas() {
            $("#btnConsultar").click();            
        }

    

     </script>
   
     <!-- Para Calendario
     <script src="../SISREQ/private/calendario.js" language="javascript" type="text/javascript"></script>
     <script src="../SISREQ/private/PopCalendar.js" language="javascript" type="text/javascript"></script>
     -->        
      <style type="text/css">          
          .cabecera
          {
          	background-color:#003366;
          	color:White
          }               
       
      </style>
          
</head>

<body>
    <form id="form1" runat="server">    
    
    <div>
           
        <asp:Button ID="btnExportar" runat="server" Text="Exportar a Excel" 
            SkinID="BotonAExcel" />
    
    </div>

            <table style="width:100%;">
                <tr>
                    <td width="10%" >
                        Centro de Costos:</td>
                    <td width="90%"  align=left>
                        <asp:DropDownList ID="cboCecos" runat="server" AutoPostBack="True">
                        </asp:DropDownList>
                        <asp:TextBox ID="txtBuscaCecos" runat="server" ValidationGroup="BuscarCecos"></asp:TextBox>
                        <asp:ImageButton ID="ImgBuscarCecos" runat="server" style="width: 14px" 
                            Height="16px" ImageUrl="~/images/busca.gif" Width="23px" />
                        <asp:Label ID="lblTextBusqueda" runat="server" Text="(clic aquí)"></asp:Label>
                        <br />
                        <asp:LinkButton ID="lnkBusquedaAvanzada" runat="server" ForeColor="Blue">Búsqueda Avanzada</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td  colspan="2">
                        <asp:Panel ID="Panel3" runat="server" Height="150px" ScrollBars="Vertical" 
                            Width="100%">
                            <asp:GridView ID="gvCecos" runat="server" AutoGenerateColumns="False" 
                                BorderColor="#628BD7" BorderStyle="Solid" BorderWidth="1px" CellPadding="4" 
                                DataKeyNames="codigo_cco" ForeColor="#333333" ShowHeader="False" 
                              Width="98%" SkinID="skinGridView">
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <RowStyle BackColor="#EFF3FB" />
                                <Columns>
                                    <asp:BoundField DataField="codigo_cco" HeaderText="Código" />
                                    <asp:BoundField DataField="nombre" HeaderText="Centro de costos" />
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
                    <td class="style4">
                        Servicio:</td>
                    <td class="style3">
                        <asp:DropDownList ID="cboServ" runat="server">
                        </asp:DropDownList>
                                                
				           <asp:Button ID="btnConsultar" runat="server" Text="Ejecutar Consulta" 
                           SkinID="BotonBuscar"  />
                       
                        <asp:Label ID="lblMensaje" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        &nbsp;</td>
                    
                </tr>
                <tr>
                    <td class="style2" colspan="2">
                        Resultado de la consulta:<br />
                        Nota: La columna Abono es igual a Pagos en Efectivo + Notas de Credito </td>
                </tr>
                <tr>
                    <td class="style2" colspan="2">
                        <!-- 
                        <div id="paginacion" runat="server">
                            <asp:Button ID="btnprimero" runat="server" Text="<< Primero" />
                            <asp:Button ID="btnanterior" runat="server" Text="< Anterior" />
                            <asp:Label ID="lblactual" runat="server"></asp:Label>  
                            <asp:Label ID="Label1" runat="server" Text = " de "></asp:Label>  
                            <asp:Label ID="lbltotalpaginas" runat="server"></asp:Label>
                            <asp:Button ID="btnsiguiente" runat="server" Text="> Siguiente" />
                            <asp:Button ID="btnultimo" runat="server" Text=">> Ultimo" />
                            <asp:Label ID="Label2" runat="server" text="Número de registros encontrados: "></asp:Label>
                            <asp:Label ID="lbltotal" runat="server"></asp:Label>                                                        
                            <asp:Label ID="lblnroprimero" runat="server" Visible = "false" Text ="1"></asp:Label>
                            <asp:Label ID="lblnroultimo" runat="server" Visible = "false" Text ="1"></asp:Label>
                        </div>
                        -->
                        
                        
                        <!--
                         -->
                         
                         
                        <!-- 
                        <asp:GridView ID="gvResultado" runat="server" AutoGenerateColumns="False" 
                            CellPadding="4" DataKeyNames="codigo_Deu" GridLines="None" 
                          SkinID="skinGridViewLineasIntercalado"> 
                            <FooterStyle Font-Bold="True" />
                            <Columns>
                                <asp:BoundField HeaderText="Nro" />
                                <asp:BoundField DataField="COD. RESP." HeaderText="COD. RESP." ReadOnly="True" 
                                    SortExpression="COD. RESP." >
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CLIENTE" HeaderText="CLIENTE" ReadOnly="True" 
                                    SortExpression="CLIENTE" >
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CARRERA" HeaderText="CARRERA" ReadOnly="True" 
                                    SortExpression="CARRERA" >
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="FECHA_CARGO" HeaderText="FECHA_CARGO" 
                                    ReadOnly="True" SortExpression="FECHA_CARGO" >
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="SERVICIO" HeaderText="SERVICIO" 
                                    SortExpression="SERVICIO" >
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CARGOS" HeaderText="CARGOS" ReadOnly="True" 
                                    SortExpression="CARGOS" >
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ABONOS" HeaderText="ABONOS" ReadOnly="True" 
                                    SortExpression="ABONOS" >
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DIFERENCIA" HeaderText="SALDO" 
                                    SortExpression="DIFERENCIA" >
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="FECHA_VENC" HeaderText="FECHA_VENC" 
                                    SortExpression="FECHA_VENC" >
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Observacion" HeaderText="OBSERVACION">
                                    <ItemStyle Font-Size="Smaller" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="codigo_Deu" HeaderText="codigo_Deu" ReadOnly="True" 
                                    SortExpression="codigo_Deu" Visible="False" />
                                <asp:BoundField DataField="estadoActual_Alu" HeaderText="ESTADO" />
                                <asp:HyperLinkField DataNavigateUrlFields="codigo_deu" 
                                    DataNavigateUrlFormatString="frmVerAbonos.aspx?id={0}" HeaderText="DETALLE" 
                                    Text="Ver Abonos" Target="_blank" />
                            </Columns>
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle Font-Bold="True" />
                            <HeaderStyle Font-Bold="True" />
                        </asp:GridView> -->
                    </td>
                </tr>
            </table>

            <div id="reporte" runat="server"></div>
                        <div id="pager" runat="server">
                            <form>
                            <img src="../images/first.png" class="first"/>
                            <img src="../images/prev.png" class="prev"/>
                            <input type="text" class="pagedisplay" />
                            <img src="../images/next.png" class="next"/>
                            <img src="../images/last.png" class="last"/>
                                        
                            <select class="pagesize">                
                                <option selected="selected" value="10">10</option>
                                <option value="20">20</option>
                                <option value="30">30</option>
                                <option value="40">40</option>
                                <option value="100">100</option>
                                <option value="5000">Todos</option>
                            </select>
                            </form>
                        </div>
        
    <p>&nbsp;</p>                
    </div>    
    </form>
</body>
</html>
