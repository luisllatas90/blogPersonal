<%@ Page Language="VB" AutoEventWireup="false" CodeFile="lsttesis.aspx.vb" Inherits="lsttesis" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Investigacion</title>
    <link href="../../../private/estilo.css" rel="stylesheet" type="text/css" media="screen" />
    <script type="text/javascript" language="JavaScript" src="../../../private/funciones.js"></script>
    <script type="text/javascript" language="JavaScript" src="../../../private/jq/jquery-1.4.2.min.js"></script>
	<script type="text/javascript" language="JavaScript" src="../../../private/jq/lbox/thickbox.js"></script>
	<link rel="stylesheet" href="../../../private/jq/lbox/thickbox.css" type="text/css" media="screen" />
	<script type="text/javascript" language="javascript">
	    function PintarFilaElegida(obj)
        {
            if (obj.style.backgroundColor=="white"){
                obj.style.backgroundColor="#E6E6FA"//#395ACC
            }
            else{
                obj.style.backgroundColor="white"
            }
        }
        function cmdNuevo_onclick() {

        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <p class="usatTitulo">Lista de Trabajos de investigación para Titulación</p>
    
    <table cellpadding="2" cellspacing="0">
    <tr>
        <td>Buscar:
            &nbsp;<asp:DropDownList ID="dpEscuela" runat="server" Font-Size="8" 
                AutoPostBack="True">
            </asp:DropDownList>
            <asp:DropDownList ID="ddlCiclo" runat="server" AutoPostBack="True">
            </asp:DropDownList>
            <asp:DropDownList ID="cboPor" runat="server">
                <asp:ListItem Value="9">Por Título</asp:ListItem>
                <asp:ListItem Value="10">Por Autor</asp:ListItem>
                <asp:ListItem Value="11">Por Asesor</asp:ListItem>
                <asp:ListItem Value="12">Por Jurado</asp:ListItem>
            </asp:DropDownList>
            <asp:TextBox ID="txtbusqueda" runat="server"  
                MaxLength="255" Width="200px" Font-Size="8"></asp:TextBox>
              <asp:Button ID="cmdBuscar" runat="server" CssClass="buscar2" 
                Text="      Buscar" />
                
            <a href="frmtesis.aspx?accion=A&codigo_tes=0&mod=<%=Request.QueryString("mod")%>&ctf=<%=Request.QueryString("ctf")%>&id=<%=Request.QueryString("id")%>&KeepThis=true&TB_iframe=true&height=450&width=650&modal=true" title="Registrar Trabajos de Investigación" class="thickbox">
             <input id="cmdNuevo" class="agregar2" type="button" value="     Nuevo" onclick="return cmdNuevo_onclick()" /> </a></td>
       </tr>
       <tr>
        <td align="right">
            <asp:Button ID="cmdReporte" runat="server" Text="Reporte Exportar" 
                UseSubmitBehavior="False" CssClass="boton" />
        </td>
       </tr>
    </table>
    <br />
       <asp:GridView ID="grwListaTesis" runat="server"
        AutoGenerateColumns="False" DataKeyNames="codigo_Tes" CellPadding="3" 
            BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" 
        Font-Size="Smaller">
        <Columns>
            <asp:BoundField HeaderText="Nro" />
            <asp:BoundField DataField="titulo_tes" HeaderText="Titulo"
                SortExpression="titulo_tes">
                <ItemStyle Font-Size="7pt" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="Investigador Principal (AUTOR)">
                <ItemTemplate>
                    <asp:BulletedList ID="bAutores" runat="server" DataTextField="autor" DataValueField="codigo_alu">
                    </asp:BulletedList>
                </ItemTemplate>
                <ItemStyle Font-Size="7pt" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Invetigador Secundario (ASESOR)">
                <ItemTemplate>
                    <asp:BulletedList ID="bAsesores" runat="server" DataTextField="responsable" DataValueField="codigo_per">
                    </asp:BulletedList>
                </ItemTemplate>
                <ItemStyle Font-Size="7pt"/>
            </asp:TemplateField>
            <asp:BoundField DataField="nombre_Eti" HeaderText="Etapa" >
                <ItemStyle Font-Overline="False" Font-Size="7pt" />
            </asp:BoundField>
            <asp:BoundField DataField="bloqueo" HeaderText="Bloqueado">
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="Asignar">
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
            <asp:BoundField DataField="descripcion_cac" HeaderText="Semestre Académico" />
            
            <asp:BoundField HeaderText="PDF" />
            
            <asp:BoundField DataField="codigo_cac" HeaderText="codigo_cac" 
                Visible="False" />
            <asp:BoundField DataField="codigo_cpf" HeaderText="codigo_cpf" 
                Visible="False" />
            
        </Columns>
        <PagerStyle BackColor="Silver" HorizontalAlign="Center" />
        <EmptyDataTemplate>
            <strong style="width: 100%; color: red; text-align: center">
                <br />
                No se encontraron investigaciones según los criterios de búsqueda.
                <br />
            </strong>
        </EmptyDataTemplate>
        <HeaderStyle BackColor="#E8EEF7" BorderColor="#99BAE2" BorderStyle="Solid" 
                                    BorderWidth="1px" ForeColor="#3366CC" />
    </asp:GridView>
</form>
</body>
</html>

