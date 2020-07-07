<%@ Page Language="VB" AutoEventWireup="false" CodeFile="lstColegios.aspx.vb" Inherits="administrativo_pec_lstColegios" Theme="Acero"%>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Lista de Escuelas Profesionales</title>
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
      <p class="usatTitulo">
        Lista de Colegios</p>
     <table cellpadding="2" cellspacing="0">
    <tr>
        <td>Departamento:<asp:DropDownList ID="cboDpto" runat="server">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>Indicar el nombre:
            &nbsp;<asp:TextBox ID="txtbusqueda" runat="server"  
                MaxLength="200" Width="200px" Font-Size="8"></asp:TextBox><asp:Button ID="cmdBuscar" runat="server" 
                Text="Buscar" SkinID="BotonBuscar" />
            <asp:Button ID="cmdNuevo" runat="server" 
                Text="Nuevo" SkinID="BotonNuevo" />
        &nbsp;
            </td>
    </tr>
    </table>
      <asp:GridView ID="DgvLista" runat="server" AutoGenerateColumns="False" 
        SkinID="skinGridViewLineas">
        <Columns>
          <asp:BoundField DataField="codigo_Col" HeaderText="Nro">
          <ItemStyle HorizontalAlign="Center" />
          </asp:BoundField>
          <asp:BoundField DataField="nombre_Col" HeaderText="Colegio" />
          <asp:BoundField DataField="nombre_Dep" HeaderText="Departamento" />
          <asp:BoundField DataField="nombre_Pro" HeaderText="Provincia" />
          <asp:BoundField DataField="nombre_Dis" HeaderText="Distrito" />
          <asp:BoundField HeaderText="Modificar">
          <ItemStyle HorizontalAlign="Center" />
          </asp:BoundField>
        </Columns>
      </asp:GridView>
    </form>
</body>
</html>
