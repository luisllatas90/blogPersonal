<%@ Page Language="VB" AutoEventWireup="false" CodeFile="lsteventos.aspx.vb" Inherits="lsteventos" Theme="Acero" %>
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
    <p class="usatTitulo">Lista de eventos registrados</p>
    
    <table cellpadding="2" cellspacing="0">
    <tr>
        <td>Indicar el nombre:
            &nbsp;<asp:TextBox ID="txtbusqueda" runat="server"  
                MaxLength="200" Width="200px" Font-Size="8"></asp:TextBox>
            <asp:Button ID="cmdBuscar" runat="server" 
                Text="Buscar" SkinID="BotonBuscar" />
            &nbsp;<asp:Button ID="cmdNuevo" runat="server" 
                Text="Nuevo" SkinID="BotonNuevo" />
        </td>
    </tr>
    </table>
    <br />
       <asp:GridView ID="grwListaEventos" runat="server"
        AutoGenerateColumns="False" DataKeyNames="codigo_cco" CellPadding="3" 
        SkinID="skinGridViewLineas" >
        <Columns>
            <asp:BoundField HeaderText="Nro" />
            <asp:BoundField DataField="nombre_dev" HeaderText="Nombre"
                SortExpression="titulo_tes">
                <ItemStyle Font-Size="7pt" />
            </asp:BoundField>
            <asp:BoundField DataField="fechainiciopropuesta_dev" HeaderText="Fecha Inicio" 
                DataFormatString="{0:d}">
            </asp:BoundField>
            <asp:BoundField HeaderText="Fecha Fin" DataField="fechafinpropuesta_dev" 
                DataFormatString="{0:d}" />
            <asp:BoundField DataField="coordinadorgral" HeaderText="Coord. General" >
                <ItemStyle Font-Overline="False" Font-Size="7pt" />
            </asp:BoundField>
            <asp:BoundField DataField="ingresostotalesproyectada_dev" 
                HeaderText="Ingresos" />
            <asp:BoundField DataField="egresostotalesproyectada_dev" 
                HeaderText="Egresos" />
            <asp:BoundField HeaderText="Utilidad" DataField="utilidadproyectada_dev" />
            <asp:BoundField HeaderText="Nro participantes" 
                DataField="nroparticipantes_dev" />
            <asp:TemplateField HeaderText="Es programa académico"> 
                <ItemTemplate>
                    <asp:Label ID="lblGestionaNotas" runat="server" 
                        Text='<%# iif(eval("gestionanotas_dev")=true,"Sí","No") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
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
        <EmptyDataTemplate>
            <strong style="width: 100%; color: red; text-align: center">
                <br />
                No se encontraron Eventos registrados según los criterios de búsqueda.
                <br />
            </strong>
        </EmptyDataTemplate>
    </asp:GridView>
</form>
</body>
</html>