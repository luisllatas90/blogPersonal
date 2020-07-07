<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmListaInvestigacion.aspx.vb" Inherits="academico_Investigacion_Consultas_FrmListaInvestigacion" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../../scripts/css/bootstrap.css" rel="stylesheet" type="text/css" />
    <script src="../../../scripts/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="../../../scripts/js/jquery-1.12.3.min.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <br />
    <div class="container-fluid">          
        <table width="100%">
            <tr>
                <td style="width: 15%">Registrado el</td>
                <td style="width: 10%"><asp:DropDownList ID="cboAnio" runat="server"></asp:DropDownList></td>
                <td style="width: 10%">Titulo</td>
                <td style="width: 40%"><asp:TextBox ID="txtTitulo" runat="server" Width="98%"></asp:TextBox></td>                        
                <td style="width: 25%"><asp:Button ID="btnBuscar" runat="server" Text="Buscar" class="btn btn-info"/>
                                &nbsp;<asp:Button ID="btnExportar" runat="server" Text="Exportar"  class="btn btn-success"/>
                </td>
            </tr>
        </table>
        <br />
        <asp:GridView ID="gvInvestigaciones" runat="server" Width="100%" 
            AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="id" HeaderText="ID" />
                <asp:BoundField DataField="titulo" HeaderText="TITULO" />
                <asp:BoundField DataField="fechaRegistro" HeaderText="F. REGISTRO" />
                <asp:BoundField DataField="fechaInicio" HeaderText="F. INICIO" />
                <asp:BoundField DataField="fechaFin" HeaderText="F. FIN" />
                <asp:BoundField DataField="presupuesto" HeaderText="PRESUPUESTO" />
                <asp:BoundField DataField="Ambito" HeaderText="AMBITO" />
                <asp:BoundField DataField="tipoFinanciamiento" HeaderText="FINANCIMIENTO" />
                <asp:BoundField DataField="beneficiarios" HeaderText="BENEFICIARIO" />
                <asp:BoundField DataField="resumen" HeaderText="RESUMEN" />
                <asp:BoundField DataField="linea" HeaderText="LINEA" />
                <asp:BoundField DataField="etapa" HeaderText="ETAPA" />
                <asp:BoundField DataField="estado" HeaderText="ESTADO" />
                <asp:BoundField DataField="tipo" HeaderText="TIPO" />
                <asp:BoundField DataField="Personal" HeaderText="PERSONAL" />
                <asp:BoundField DataField="nombre_Dac" HeaderText="DPTO ACADEMICO" />                
                <asp:BoundField DataField="rutaproyecto" HeaderText="archivo"/>
                <asp:BoundField HeaderText="*" />
            </Columns>
            <HeaderStyle BackColor="#D9534F" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center"
                                Font-Size="12px" />
            <RowStyle Font-Size="Smaller"/>                          
        </asp:GridView>
    
    </div>
    </form>
</body>
</html>
