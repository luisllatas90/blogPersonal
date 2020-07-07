<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RegistrarPresupuesto.aspx.vb" Inherits="presupuesto_areas_RegistrarPresupuesto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" /> 
    <link href="../../private/estiloweb.css" rel="stylesheet" type="text/css" /> 
    <link href="../../private/estiloCtrles.css" rel="stylesheet" type="text/css" /> 
    <script src="../../private/funciones.js" type ="text/javascript" language ="javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table style="width:100%;" border="0" cellpadding="3" cellspacing="0">
            <tr>
                <td style="font-weight: bold; height: 25px;" class="CeldaImagen">
                    &nbsp;LISTADO DE PRESUPUESTO&nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;<b>Centro de costo:</b>
                    <asp:DropDownList ID="cboCentroCostos" runat="server">
                    </asp:DropDownList>
&nbsp;
                    <asp:Button ID="cmdConsultar" runat="server" Text="      Consultar" 
                        CssClass="Buscar" Width="80px" Height="22px" 
                        BorderStyle="Outset" />
                </td>
            </tr>
            <tr>
                <td style="height: 11px">
                     </td>
            </tr>
            <tr>
                <td bgcolor="#999999" height="1px">
                </td>
            </tr>
            <tr>
                <td  height="30" class="titulocel">
                    <asp:Button ID="cmdNuevo" runat="server" Text="Nuevo" CssClass="nuevo" 
                        Width="80px" Height="22px" BorderStyle="Outset" />
                &nbsp;</td>
            </tr>
            <tr>
                 <td bgcolor="#999999" height="1px">
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gvPresupuesto" runat="server" 
        AutoGenerateColumns="False" DataKeyNames="codigo_Pto" Width="100%">
                        <Columns>
                            <asp:BoundField DataField="proceso" HeaderText="Proceso" />
                            <asp:BoundField DataField="director" HeaderText="Director" />
                            <asp:BoundField DataField="totalIngresos_Pto" HeaderText="Total de Ingresos" />
                            <asp:BoundField DataField="totalEgresos_Pto" HeaderText="Total de Egresos" />
                            <asp:BoundField DataField="diferencia" HeaderText="Diferencia" />
                            <asp:BoundField DataField="fechaInicio_Pto" HeaderText="Fecha de Inicio" 
                                DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:BoundField DataField="fechaFin_Pto" HeaderText="Fecha de Fin" 
                                DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:BoundField DataField="descripcion_Epr" HeaderText="Estado" />
                            <asp:CommandField ButtonType="Image" HeaderText="Ver" 
                                SelectImageUrl="~/images/Presupuesto/busca_small.gif" SelectText="Seleccionar" 
                                ShowSelectButton="True">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:CommandField>
                        </Columns>
                        <SelectedRowStyle BackColor="#FFFFCC" />
                        <HeaderStyle Height="20px" ForeColor="White" CssClass="TituloTabla" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
