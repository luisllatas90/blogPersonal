<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmMaterialEvento.aspx.vb" Inherits="administrativo_pec2_frmMaterialEvento" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registro de Material-Evento</title>
    <link href="../../../private/estilo.css" rel="stylesheet" type="text/css" media="screen" />
    <link rel="stylesheet" href="../../../private/jq/lbox/thickbox.css" type="text/css" media="screen" />    
    <script type="text/javascript" language="JavaScript" src="../../../private/funciones.js"></script>
    <script type="text/javascript" language="JavaScript" src="../../../private/jq/jquery-1.4.2.min.js"></script>
	<script type="text/javascript" language="JavaScript" src="../../../private/jq/lbox/thickbox.js"></script>    		
    <script src="../../../private/calendario.js"></script>
    
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
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table cellpadding="3" cellspacing="0" 
      style="border: 1px solid #C2CFF1; width:100%">
      <tr>
        <td bgcolor="#D1DDEF" colspan="2" height="30px">
          <b>Datos de Material - Evento</b>
          </td>
      </tr>
      <tr>
        <td width="20%">
            Material:</td>
        <td width="75%">
            <asp:DropDownList ID="cboMaterial" runat="server" Height="22px" 
                Width="191px" >               
            </asp:DropDownList>  <img src="../../../images/librohoja.gif" width="12" height="15" />  
            <a href='frmMaterial.aspx?KeepThis=true&TB_iframe=true&height=220&width=500&modal=true' 
                title='Actividades' class='thickbox'>Nuevo Material</a>        
        </td>
      </tr>
      <tr>
        <td width="20%">
            Fecha:</td>
        <td width="75%">
             <asp:TextBox ID="txtFecha" runat="server" Width="190px" Font-Names="Arial" 
                 Font-Size="Small"></asp:TextBox>
                <input value="..." onclick="MostrarCalendario('txtFecha')" type="button" /> 
             <asp:Button ID="cmdAgregar" runat="server" Text="Agregar" CssClass="agregar4" 
                Width="80px" BackColor="White" Height="22px" />
        </td>
      </tr>      
      <tr>
        <td colspan="2"> <br />
            <asp:GridView ID="gvMaterialEvento" runat="server" AutoGenerateColumns="False" 
                DataKeyNames="codigo_mev" Width="50%" >                
                <Columns>
                    <asp:BoundField DataField="codigo_mev" HeaderText="Codigo" Visible="false" />
                    <asp:BoundField DataField="titulo_Mat" HeaderText="Material" >
                        <ItemStyle HorizontalAlign="Left" Width="30%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="fechaentraga_mev" HeaderText="Fecha">
                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                    </asp:BoundField>
                    <asp:CommandField HeaderText="Eliminar" ShowDeleteButton="True" >
                    <ItemStyle HorizontalAlign="Center" Width="10%" />
                    </asp:CommandField>
                </Columns>
                <HeaderStyle BackColor="#0B3861" ForeColor="White" Height="25px" />                
                <RowStyle Height="22px" />                
            </asp:GridView>            
        </td>
      </tr>      
      <tr>
        <td width="20%">
            &nbsp;</td>
        <td width="75%">
            &nbsp;</td>
      </tr>
      <tr>
        <td colspan="2">
            <asp:Button ID="cmdRptEntrega" runat="server" Text="Entrega x Participante" CssClass="buscar2" Width="200px" Height="22px" />  &nbsp;&nbsp;&nbsp;
            <asp:Button ID="cmdRptEntregaResumen" runat="server" Text="Consolidado de Material" CssClass="buscar2" Width="200px" Height="22px" />
        </td>
      </tr>
    </table>
    </div>    
    <asp:HiddenField ID="Hdcodigo_cco" runat="server" />
    </form>
</body>
</html>
