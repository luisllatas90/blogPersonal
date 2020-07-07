﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmMaterial.aspx.vb" Inherits="administrativo_pec2_frmMaterial" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <link href="../../../private/estilo.css" rel="stylesheet" type="text/css" media="screen" />
        <style type="text/css">
            table {
	            font-family: Trebuchet MS;
	            font-size: 8pt;
            }
            TBODY {
	            display: table-row-group;
            }
            tr {
	            font-family: Verdana, Geneva, Arial, Helvetica, sans-serif;
	            font-size: 8pt;
	            color: #2F4F4F;
            }
            select {
	            font-family: Verdana;
	            font-size: 8.5pt;
            }
        </style>
        <script type="text/javascript">
            
            function actualiza() {               
//                opener.location.reload();
//                window.close();
                parent.document.location = parent.document.location;
            }
        </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table cellpadding="3" cellspacing="0" 
      style="border: 1px solid #C2CFF1; width:100%">
      <tr>
        <td bgcolor="#D1DDEF" colspan="2" height="30px">
          <b>Datos de Material</b></td>
      </tr>
      <tr>
        <td width="20%">
            Titulo:</td>
        <td width="75%">
            <asp:TextBox ID="txtTitulo" runat="server" Width="300px" MaxLength="50" 
                Font-Names="Arial"></asp:TextBox>
        </td>
      </tr>
      <tr>
        <td width="20%">
            Descripción:</td>
        <td width="75%">
            <asp:TextBox ID="txtDescripcion" runat="server" Width="416px" MaxLength="100" 
                TextMode="MultiLine" Font-Names="Arial"></asp:TextBox>
        </td>
      </tr>      
      <tr>
        <td width="20%">
            Tipo:</td>
        <td width="75%">
            <asp:DropDownList ID="cboTipo" runat="server" AutoPostBack="True" Height="22px" 
                Width="191px" >               
            </asp:DropDownList>
        </td>
      </tr>      
      <tr>
        <td width="20%">
            &nbsp;</td>
        <td width="75%">
            &nbsp;</td>
      </tr>
      <tr>
        <td width="20%">
            &nbsp;</td>
        <td width="75%">
      <asp:Button ID="cmdGuardar" runat="server" Text="Guardar" CssClass="usatGuardar"
        Height="22px" Width="100px" />
      &nbsp;<asp:Button ID="cmdCancelar" runat="server" CssClass="usatEliminar" OnClientClick ="actualiza()"
                                Text="Cerrar" Width="100px" Height="22px"/>
        </td>
      </tr>
    </table>
    </div>
    <asp:HiddenField ID="HdCodigo_Mat" runat="server" />
    </form>
</body>
</html>
