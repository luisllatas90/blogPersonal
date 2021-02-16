<%@ Page Language="VB" AutoEventWireup="false" CodeFile="categoria.aspx.vb" Inherits="categoria" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
</head>
<script type="text/javascript" language=javascript">
    var nav1 = window.Event ? true : false;
    var nav2 = window.Event ? true : false;
    function solonumerosentero(evt) {
        // Backspace = 8, Enter = 13, '0' = 48, '9' = 57, '.' = 46
        var key = nav1 ? evt.which : evt.keyCode;
        return (key <= 13 || (key >= 48 && key <= 57));
    }
    function solonumerodecimal(evt) {
        // Backspace = 8, Enter = 13, '0' = 48, '9' = 57, '.' = 46
        var key = nav2 ? evt.which : evt.keyCode;
        return (key <= 13 || (key >= 48 && key <= 57) || key == 46);
    }



</script>
 <style type="text/css">
         body
        { font-family: 'Roboto', sans-serif;
        font-size: 13px;
          cursor:hand;
          background-color:white;	
        }
        .tit
        {
            background-color: #E8EEF7; font-weight: bold;  padding: 10px 10px 10px 0px;
            }
             .btn
       {
            border:1px solid #5D7B9D; 
            background:#F7F6F3; 
            font-family:Tahoma; 
            font-size:8pt; 
            font-weight:bold;  padding:3px;
         }
        .style1
     {
         width: 12%;
     }
        .style2
     {
         width: 50%;
         height: 29px;
         text-align:center;
     }
        .style3
     {
         width: 50%;
     }
        </style>
<body>
    <form id="form1" runat="server">
    <div>
            
    <table style="width:90%; border: 1px black;" align="center">
    <tbody>
        <tr>
            <td colspan="2" style="text-align:center">
                <h3>Categorias</h3></td>
              
        </tr>
        <tr>
            
            <td class="style2" colspan=2> <asp:Button ID="btnConsultar" runat="server" class="btn" Text="Consultar" /><asp:Button ID="btnNuevo" runat="server" class="btn" Text="Registrar"  />
                        </td>
           
    
        </tr>
     </tbody>
 <tbody>
 </tbody>
     <tbody>
        <tr valign="top"><td class="style3">
            <asp:GridView ID="lstReglas" runat="server" AutoGenerateColumns="False" DataKeyNames="codigo"  BorderStyle="None" 
             AlternatingRowStyle-BackColor="#F7F6F4" >
                <Columns> <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="BtnMostrar" runat="server" Text="Editar" class="btn" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                             CommandName="Editar"  />
                    </ItemTemplate>
                </asp:TemplateField>
                    <asp:BoundField DataField="nombre" HeaderText="DESCRIPCION" />
                    <asp:BoundField DataField="precio" HeaderText="PRECIO CREDITO">
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                </Columns>

<AlternatingRowStyle BackColor="#F7F6F4"></AlternatingRowStyle>
            </asp:GridView>
            
            </td>
             <td class="style1" >
             <div id="Registro" runat="server" visible="false">
             <input type="hidden" id="txtid" value="" runat="server" />
             <table width="100%" >
             <tr>
             <td colspan="2" style="text-align:center; background-color:ActiveBorder; font-weight:bold;" id="tdRegistro" runat="server"></td>
             </tr>
             <tr>
             <td>Nombre</td>
             <td>
             <input type="text" id="txtnombre" value="" runat="server" 
                     style="text-align: right"  autocomplete="off" /></td>
             </tr>
             <tr>
             <td>Precio Cred</td>
             <td>
             <input type="text" id="txtprecio" value="" runat="server" 
                     style="text-align: right"  autocomplete="off"   onkeypress="javascript:return solonumerodecimal(event);" /></td>
             </tr>
             <tr>
             <td colspan="2" style="text-align:center">
             
                <asp:Button ID="btnGrabar" runat="server" class="btn" Text="Grabar" />
                <asp:Button ID="btnCancelar" runat="server" class="btn" Text="Cancelar" />
             </td>
             </tr>
             </table>
             </div>
             </td>
             
        </tr>
     </tbody>  
    </table>
    
    
    
    </div>
    </form>
</body>
</html>
