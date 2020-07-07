<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmDocenteAprobacion.aspx.vb" Inherits="Odontologia_Docente_FrmDocenteAprobacion" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../private/estilo.css"rel="stylesheet" type="text/css" />     
    <link rel="stylesheet" href="../../../private/jq/lbox/thickbox.css" type="text/css" media="screen" />
    <!--
    <link href="../../Scripts/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/js1_12/bootstrap.js" type="text/javascript"></script>
    <script src="../../Scripts/js1_12/jquery-1.12.3.min.js" type="text/javascript"></script>    
    -->
    <script type="text/javascript" language="JavaScript" src="../../../private/jq/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" language="JavaScript" src="../../../private/jq/lbox/thickbox.js"></script>    
    
    <style type="text/css">
        .style1
        {
            width: 416px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">    
    <div class="container">       
        <br /> 
        <div class="row">
            <div class="col-md-7">
                <asp:Label ID="lblTrabajador" runat="server" Font-Bold="True" Font-Size="Large"></asp:Label>
            </div>
            <div class="col-md-5">
                
            </div>
        </div>
        <div class="row">
            <asp:Label ID="lblMensaje" runat="server" Font-Bold="True" ForeColor="Red" 
                        Font-Size="Medium"></asp:Label>
        </div>
        <br />
        <table width="100%">                        
            <tr>                
                <td align="right">
                <asp:Button ID="btnSalir" Width="180px" class="btn btn-danger btn-lg" Height="45px" runat="server" Text="Salir" />
                <asp:Button ID="btnRefrescar" CssClass="btn btn-primary btn-lg" Width="180px" Height="45px" runat="server" Text="Refrescar" />            
                </td>
            </tr>
            <tr>
                <td colspan="2">
                <asp:GridView ID="gvPedidos" runat="server" Width="100%" 
                    AutoGenerateColumns="False" Font-Size="Small" 
                        DataKeyNames="CantMaxima,codigo_alu">
                    <Columns>
                        <asp:BoundField DataField="codigo_pod" HeaderText="Pedido" />
                        <asp:BoundField DataField="fechapedido" HeaderText="F. Pedido" />
                        <asp:BoundField DataField="NombreAlumno" HeaderText="NombreAlumno" />
                        <asp:BoundField DataField="nombre_paq" HeaderText="Tratamiento" />
                        <asp:BoundField DataField="precioTotal_pod" HeaderText="Precio" />
                        <asp:BoundField DataField="nroHistoria_pod" HeaderText="Historia" />
                        <asp:BoundField DataField="nombre_Cur" HeaderText="Curso" />
                        <asp:CommandField EditText="Aprobar" HeaderText="Aprobar" 
                            ShowEditButton="True" >
                        <ItemStyle BackColor="#DC9C87" Font-Bold="True" Font-Underline="True" 
                            HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:CommandField>
                        <asp:CommandField HeaderText="Rechazar" ShowDeleteButton="True" >
                        <ItemStyle BackColor="#C5C5C5" Font-Bold="True" HorizontalAlign="Center" 
                            VerticalAlign="Middle" />
                        </asp:CommandField>
                        <asp:BoundField DataField="CantMaxima" HeaderText="CantMaxima" />
                        <asp:BoundField DataField="codigo_alu" HeaderText="codigo_alu" 
                            Visible="False" />
                    </Columns>
                    <RowStyle Font-Size="small" />
                    <HeaderStyle BackColor="#e33439" ForeColor="White" Height="25px" />                
                </asp:GridView>
                </td>
            </tr>
        </table>            
    </div>
    </form>
</body>
</html>
