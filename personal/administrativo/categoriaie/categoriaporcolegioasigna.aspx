<%@ Page Language="VB" AutoEventWireup="false" CodeFile="categoriaporcolegioasigna.aspx.vb" Inherits="categoria" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
</head>
<script type="text/javascript" language=javascript">
    var nav1 = window.Event ? true : false;

    function solonumerosentero(evt) {
        // Backspace = 8, Enter = 13, '0' = 48, '9' = 57, '.' = 46
        var key = nav1 ? evt.which : evt.keyCode;
        return (key <= 13 || (key >= 48 && key <= 57));
    }
    function txtMaxAdelantar_onclick() {

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
         height: 25px;
     }
        .style1
     {
         width: 12%;
     }
        </style>
<body>
    <form id="form1" runat="server">
    <div>
    <table style="width:90%; border: 1px black;" align="center">
    <tbody>
        <tr>
            <td colspan="2" style="text-align:center">
                <h3>Categoria por Colegios</h3>
            </td>
              
        </tr>

        <tr>
            
            <td style="width:100%;" colspan="2">
                Departamento:&nbsp;<asp:DropDownList ID="ddlDepartamento" runat="server" AutoPostBack=true >
                </asp:DropDownList>
                Provincia:&nbsp;<asp:DropDownList ID="ddlProvincia" runat="server" AutoPostBack=true >
                </asp:DropDownList>
                Distrito:&nbsp;<asp:DropDownList ID="ddlDistrito" runat="server" >
                </asp:DropDownList>
                
                
                        </td>
          
    
        </tr>
                <tr>
            
            <td style="width:100%;text-align:center;" colspan=2 > <asp:Button ID="btnConsultar" runat="server" class="btn" Text="Consultar" /><asp:Button ID="btnNuevo" runat="server" class="btn" Text="Registrar"  />
                        
                &nbsp;</td>
    
        </tr>
        <tr>
        <td colspan="2"> <hr />
        </td>
        </tr>
     </tbody>
 <tbody>
 <tr>
 <td style="width:50%;">
                        &nbsp;</td>
  <td style="width:50%;">&nbsp;</td>

 </tr>
 </tbody>
     <tbody>
        <tr valign="top"><td >
            <asp:GridView ID="lstReglas" runat="server" AutoGenerateColumns="False" DataKeyNames="codigo,codcategoria,codcolegio,codigo_Dis,codigo_Pro,codigo_Dep"  BorderStyle="None" 
             AlternatingRowStyle-BackColor="#F7F6F4" >
                <Columns> <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="BtnMostrar" runat="server" Text="Edit" class="btn" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                             CommandName="Editar"  />
                              <asp:Button ID="BtnEliminar" runat="server" Text="Elim" class="btn" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                             CommandName="Eliminar"  />
                    </ItemTemplate>
                </asp:TemplateField>
                    <asp:BoundField DataField="colegio" HeaderText="INSTITUCION EDUCATIVA" >
                        <HeaderStyle BackColor="#0066CC" BorderColor="White" ForeColor="White" />
                    </asp:BoundField>
                    <asp:BoundField DataField="categoria" HeaderText="CATEGORIA">
                        <HeaderStyle BackColor="#0066CC" BorderColor="White" ForeColor="White" />
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
             <td  style="text-align:center; background-color:#0066CC; color:white; font-weight:bold;" id="tdRegistro" runat="server"></td>
             </tr>
             <tr>
             <td><b>Departamento</b><br />
                 <asp:DropDownList ID="ddlDepartamentoReg" runat="server" AutoPostBack=true >
                </asp:DropDownList>
                 </td>
             </tr>
             <tr>
             <td><b>Provincia</b><br />
                        <asp:DropDownList ID="ddlProvinciaReg" runat="server" AutoPostBack=true >
                </asp:DropDownList>
                        </td>
             </tr>
             <tr>
             <td><b>Distrito</b><br />
                        <asp:DropDownList ID="ddlDistritoReg" runat="server" AutoPostBack=true >
                </asp:DropDownList>
                
                
                                        </td>
             </tr>
             <tr>
             <td><b>Instituci&oacute;n Educativa</b><br />
                        <asp:DropDownList ID="ddlColegioReg" runat="server" >
                </asp:DropDownList></td>
             </tr>
             <tr>
             <td><b>Categoria - PrecCred</b><br /><asp:DropDownList ID="ddlCatReg" 
                     runat="server" >
                </asp:DropDownList>
                        
             </td>
             </tr>
             <tr>
             <td style="text-align:center">
             
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
