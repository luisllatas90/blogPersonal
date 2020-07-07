<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmConfig.aspx.vb" Inherits="academico_asistencia_jquery_Config" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

        <style type="text/css">
         body
        { font-family:Trebuchet MS;
          font-size:11.5px;
          cursor:hand;
          background-color:white;	 
        }
     .btn
       {
            border:1px solid #5D7B9D; 
            background:#F7F6F3 ; 
            font-family:Tahoma; 
            font-size:8pt; 
            font-weight:bold;  padding:3px; 
       }
        </style>
    <script type="text/javascript">
        function MarcarCursos(obj) {
            //asignar todos los controles en array
            var arrChk = document.getElementsByTagName('input');
            for (var i = 0; i < arrChk.length; i++) {
                var chk = arrChk[i];
                //verificar si es Check
                if (chk.type == "checkbox") {
                    chk.checked = obj.checked;
                    if (chk.id != obj.id) {
                        PintarFilaMarcada(chk.parentNode.parentNode, obj.checked)
                    }
                }
            }
        }


        function PintarFilaMarcada(obj, estado) {
            if (estado == true) {
                obj.style.backgroundColor = "#FFE7B3"
            }
            else {
                obj.style.backgroundColor = "white"
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
   
    <table>
    
     <tr style="background-color: #E8EEF7; font-weight: bold; height: 30px; font-size:large; ">
            <td colspan="2"><b>Configuración de Accesos</b></td>            
            
        </tr>
        <tr>
            <td>
                Tipo de Asistencia</td>
            <td>
                <asp:DropDownList ID="ddlTipoAsistencia" runat="server" AutoPostBack="True">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="style2">
                &nbsp;</td>
            <td class="style2">
            </td>
        </tr>
        <tr>
            <td class="style2" colspan="2">
                Accesos Seleccionados para 
                <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="False" 
                    DataKeyNames="codigo_tfu,administrar,registrar" BorderStyle="None" CellPadding="4" 
             AlternatingRowStyle-BackColor="#F7F6F4">
             
                 <RowStyle BorderColor="#C2CFF1" />
                    <Columns>
                        <asp:BoundField DataField="codigo_enc" HeaderText="codigo_enc" Visible="false" />
                        <asp:BoundField DataField="codigo_tfu" HeaderText="codigo_tfu" Visible="false" />
                        <asp:BoundField DataField="descripcion_tfu" HeaderText="Tipo Función"/>
                        
                         
                         <asp:BoundField DataField="registrar" HeaderText="Registrar" Visible="false"/>
                         <asp:BoundField DataField="administrar" HeaderText="Borrar" Visible="false"/>
                          
            
                        
             <asp:TemplateField >
                    <HeaderTemplate > Puede Registrar
                    
                    </HeaderTemplate>

                    <ItemTemplate>
                    <asp:CheckBox ID="chkElegirR" runat="server" />
                    </ItemTemplate> 
                    <ItemStyle HorizontalAlign="Center" />
              </asp:TemplateField> 
                
                <asp:TemplateField>
                    <HeaderTemplate>
                     Puede Borrar 
                    </HeaderTemplate>

                
                <ItemTemplate>
                    <asp:CheckBox ID="chkElegirA" runat="server" />
                    </ItemTemplate> 
                    <ItemStyle HorizontalAlign="Center" />
             </asp:TemplateField> 
             
               <asp:TemplateField HeaderText="Acceso" 
                     ItemStyle-HorizontalAlign="Center" ItemStyle-Width="45px">
                     <ItemTemplate>
                                                  
                         <asp:Button ID="btnAsignar" runat="server" Text="Asignar / Quitar"
                            CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                             CommandName="CmdAsignar"  CssClass="btn" 
                          />
                     </ItemTemplate>

<ItemStyle HorizontalAlign="Center" Width="45px"></ItemStyle>
                     </asp:TemplateField> 
                     
                        
              </Columns>
                <HeaderStyle BackColor="#e8eef7" BorderColor="#99BAE2" BorderStyle="Solid" 
                                                        BorderWidth="1px" 
                 ForeColor="#3366CC" />
             <AlternatingRowStyle BackColor="#E7EFF7" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
   
    </form>
</body>
</html>
