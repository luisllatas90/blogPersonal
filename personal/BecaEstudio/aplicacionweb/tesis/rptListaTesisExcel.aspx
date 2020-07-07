<%@ Page Language="VB" AutoEventWireup="false" CodeFile="rptListaTesisExcel.aspx.vb" Inherits="academico_tesis_rptListaTesisExcel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
   <link href="../../../private/estilo.css" rel="stylesheet" type="text/css" media="screen" />
    <script type="text/javascript" language="JavaScript" src="../../../private/funciones.js"></script>
    <script type="text/javascript" language="JavaScript" src="../../../private/jq/jquery-1.4.2.min.js"></script>
	<script type="text/javascript" language="JavaScript" src="../../../private/jq/lbox/thickbox.js"></script>
	<link rel="stylesheet" href="../../../private/jq/lbox/thickbox.css" type="text/css" media="
	
    
    <style type="text/css">
    
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%">
            <tr>
                <td align="center"><b>LISTADO DE TESIS</b></td>
              
            </tr>
            <tr>
                <td>
                    <asp:Button ID="cmdExport" runat="server" Text="   Exportar" 
                        CssClass="excel2" />
                </td>
            </tr>
            <tr>
            
                <td colspan="2">
                
       <asp:GridView ID="grwListaTesis" runat="server" 
        AutoGenerateColumns="False" DataKeyNames="codigo_Tes" CellPadding="3" 
            BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" 
        Font-Size="Smaller" Width="1071px">
        <Columns>
            <asp:BoundField HeaderText="Nro" />
            <asp:BoundField DataField="titulo_tes" HeaderText="Titulo" 
                SortExpression="titulo_tes">
                <ItemStyle Font-Size="7pt" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="Autor (es)">
                <ItemTemplate>
                    <asp:BulletedList ID="bAutores" runat="server" DataTextField="autor" DataValueField="codigo_alu">
                    </asp:BulletedList>
                </ItemTemplate>
                <ItemStyle Font-Size="7pt" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Asesor (es)">
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
            <asp:BoundField HeaderText="Asignar" Visible="False">
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="Modificar" Visible="False">
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:CommandField ButtonType="Image" 
                DeleteImageUrl="../../../images/eliminar.gif" HeaderText="Eliminar" 
                ShowDeleteButton="True" Visible="False">
                <ItemStyle HorizontalAlign="Center" />
            </asp:CommandField>
            <asp:BoundField DataField="descripcion_cac" HeaderText="Ciclo Académico" />
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
                
                </td>
            </tr>
        
        </table>
    </div>
    </form>
</body>
</html>
