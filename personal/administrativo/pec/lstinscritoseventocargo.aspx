<%@ Page Language="VB" AutoEventWireup="false" CodeFile="lstinscritoseventocargo.aspx.vb" Inherits="lstinscritoseventocargo" Theme="Acero" %>
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
    <p class="usatTitulo">Lista de participantes inscritos con cargo generado</p>
    
            <asp:Button ID="cmdNuevo" runat="server" 
                Text="Nueva Persona Natural" Width="160px" 
      SkinID="BotonSinTextoNuevo" />
        &nbsp;<asp:Button ID="cmdNuevoJuridica" runat="server" 
                Text="Nueva Persona Jurídica" Width="170px" SkinID="BotonSinTextoNuevo" />
        &nbsp;<asp:Button ID="cmdNuevoPersonaSinCargo" runat="server" 
                Text="Persona Natural sin Cargo" Width="200px" 
      SkinID="BotonSinTextoNuevo" Visible="False" />
      <!--
        &nbsp;<asp:Button ID="cmdActualizar" runat="server" 
                Text="Actualizar" Width="100px" SkinID="BotonBuscar" />
                -->
        &nbsp;<asp:Button ID="cmdReporte" runat="server" 
        SkinID="BotonSinTextoAExcel" Text="Ver reporte" UseSubmitBehavior="False" 
        Height="26px" />
        &nbsp;&nbsp;<asp:Label ID="Label1" runat="server" Text="Estado:"></asp:Label>
        <asp:DropDownList ID="ddlEstado" runat="server" AutoPostBack="True">        
            <asp:ListItem Value="T">TODOS</asp:ListItem>
            <asp:ListItem Value="A">ACTIVO</asp:ListItem>
            <asp:ListItem Value="I">INACTIVO</asp:ListItem>
            </asp:DropDownList>
        &nbsp;
        &nbsp;&nbsp;<asp:Label ID="lblbuscar" runat="server" Text="DNI/Apellidos y Nombres:"></asp:Label>
        <asp:TextBox ID="txtbuscar" runat="server"></asp:TextBox>
        <asp:Button ID="btnBuscar" runat="server" Text="Buscar" SkinID="BotonBuscar" />
        <br /> 
        <br />
        <div style="text-align:right; color:Red; font-size:12px">
        <asp:label runat="server" Text="Se muestran los"></asp:label>
        <b><asp:label ID="Label2" runat="server" Text=" (200) "></asp:label></b>
        <asp:label ID="Label3" runat="server" Text="primeros registros, Para visualizar Todos los registros ingresar a Ver Reporte"></asp:label>
        </div>
        &nbsp;<asp:GridView ID="grwListaPersonas" runat="server"
        AutoGenerateColumns="False" DataKeyNames="codigo_pso, cli" CellPadding="3" 
        SkinID="skinGridViewLineas">
        <Columns>
            <asp:BoundField HeaderText="Nro" />
            <asp:BoundField DataField="TipoDoc" HeaderText="Tipo Doc.">
            </asp:BoundField>
            <asp:BoundField DataField="Nrodoc" HeaderText="Nro. Doc.">
            </asp:BoundField>
            <asp:BoundField HeaderText="Participante" DataField="participante" >
            <ItemStyle Width="30%" />
            </asp:BoundField>
            <asp:BoundField DataField="codUniversitario" HeaderText="Cód. Univ." >
            </asp:BoundField>
            <asp:BoundField DataField="cicloIng_Alu" HeaderText="Ciclo Ingreso">
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="clave" 
                HeaderText="Clave" Visible="False" />
            <asp:BoundField DataField="estado" 
                HeaderText="Estado" Visible="False" />
            <asp:BoundField HeaderText="Cargo Total" DataField="cargototal" />
            <asp:BoundField HeaderText="Abono Total" 
                DataField="AbonoTotal" />
            <asp:BoundField DataField="saldototal" HeaderText="Saldo Total" />            
            <asp:BoundField HeaderText="Mov.">
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="Modificar">
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>            
            <asp:CommandField ButtonType="Image" 
                DeleteImageUrl="../../../images/eliminar.gif" HeaderText="Eliminar" 
                ShowDeleteButton="True" Visible="False">
                <ItemStyle HorizontalAlign="Center" />
            </asp:CommandField>
            <asp:CommandField ButtonType="Image" HeaderText="Enviar" ItemStyle-HorizontalAlign="Center" 
                SelectImageUrl="../../../images/nota.gif" ShowSelectButton="True" >
<ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:CommandField>
             <asp:BoundField HeaderText="Imprimir">
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>                                     
            <asp:BoundField HeaderText="Sol. Anulación"> <%--TRF--%>
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="UltimaCpa" HeaderText="Convenio" >
            </asp:BoundField>
            <asp:BoundField DataField="codigo_tcl" HeaderText="tcl" >
            </asp:BoundField>
            <asp:BoundField DataField="codigo_pso" HeaderText="pso" >
            </asp:BoundField>
        </Columns>
        <PagerStyle BackColor="Silver" HorizontalAlign="Center" />
    </asp:GridView>
           
    <br />
    <p>
        (*). Los cargos, abonos y saldos son calculados en base a todos los servicios 
        que tiene generado el participante para el Centro de Costos.</p>
    <asp:HiddenField ID="HdID" runat="server" />
</form>
    </body>
</html>