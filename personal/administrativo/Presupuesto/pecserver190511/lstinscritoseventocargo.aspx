﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="lstinscritoseventocargo.aspx.vb" Inherits="lstinscritoseventocargo" Theme="Acero" %>
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
                Text="Nueva Persona Jurídica" Width="170px" SkinID="BotonSinTextoNuevo" 
        Visible="False" />
        &nbsp;<asp:Button ID="cmdNuevoPersonaSinCargo" runat="server" 
                Text="Persona Natural sin Cargo" Width="200px" 
      SkinID="BotonSinTextoNuevo" Visible="False" />
        &nbsp;<asp:Button ID="cmdActualizar" runat="server" 
                Text="Actualizar" Width="100px" SkinID="BotonBuscar" />
        &nbsp;<asp:Button ID="cmdReporte" runat="server" 
        SkinID="BotonSinTextoAExcel" Text="Ver reporte" UseSubmitBehavior="False" />
        <br />
    <br />
       <asp:GridView ID="grwListaPersonas" runat="server"
        AutoGenerateColumns="False" DataKeyNames="codigo_pso" CellPadding="3" 
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
            <asp:BoundField DataField="clave" 
                HeaderText="Clave" />
            <asp:BoundField DataField="estado" 
                HeaderText="Estado" />
            <asp:BoundField HeaderText="Cargo Total" DataField="cargototal" />
            <asp:BoundField HeaderText="Abono Total" 
                DataField="AbonoTotal" />
            <asp:BoundField DataField="saldototal" HeaderText="Saldo Total" />
            <asp:BoundField DataField="fechaRegistro_Deu" DataFormatString="{0:dd/MM/yyyy}" 
                HeaderText="Fecha Reg." />
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
        </Columns>
        <PagerStyle BackColor="Silver" HorizontalAlign="Center" />
    </asp:GridView>
</form>
    <p>
        (*). Los cargos, abonos y saldos son calculados en base a todos los servicios 
        que tiene generado el participante para el Centro de Costos.</p>
</body>
</html>