<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GrupoAvisoRegistro.aspx.vb" Inherits="GrupoAviso_GrupoAvisoRegistro" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../javascript/js/jquery.js"></script>
    <script type="text/javascript" src="../javascript/js/jquery.dataTables.min.js"></script>
    <link rel='stylesheet' href='../javascript/css/jquery.dataTables.min.css'/>
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
        </style>
            <script language="javascript">

                $(document).ready(function() {
                    var oTable = $('#lstGrupo').DataTable({
                        "bPaginate": false,
                        "bFilter": true,
                        "bLengthChange": false,
                        "bInfo": true
                    });

                });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="Filtro">
            <table style="width:90%; border: 1px black;" align="center">
            <tbody>
                <tr>
                    <td colspan="2" style="text-align:center">
                        <h3>Registrar Notificaci&oacute;n</h3></td>             
                </tr>
                <tr>
                   <!-- <td style="width:50%;">
                        &nbsp;</td>-->
                    <td style="width:100%;text-align:center;" colspan="2">
                        <asp:Button ID="btnConsultar" runat="server" class="buscar" Text="Consultar" Width="100px" Height="22px" />
                        <asp:Button ID="btnNuevo" runat="server" class="nuevo" Text="Registrar" Width="100px" Height="22px" />
                    </td>          
                </tr>
             </tbody>
             <tbody>
         <tr>
         <td>&nbsp;</td>
          <td>&nbsp;</td>
         </tr>
         </tbody>
         </table>
    </div>
    <hr />
    <div id="Lista" runat="server">
    <asp:GridView ID="lstGrupo" runat="server" AutoGenerateColumns="False" 
                    DataKeyNames="codigo_noti"  BorderStyle="None" 
                 AlternatingRowStyle-BackColor="#F7F6F4" Width="100%" 
            Font-Size="X-Small"  class="display">
                    <Columns> <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="BtnMostrar" runat="server" Text="" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                 CommandName="Editar" Width="30px" Height="20px" class="editar1" ToolTip="Editar"  />
                                  
                                 <asp:Button ID="BtnEliminar" runat="server" Text="" class="eliminar2" Width="15px" Height="20px"  ToolTip="Eliminar" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                 CommandName="Eliminar"  />
                        </ItemTemplate>
                        <HeaderStyle BackColor="#E33439" Width="10%" />
                        <ItemStyle  />
                    </asp:TemplateField>
                        <asp:BoundField DataField="titulo" HeaderText="TITULO" >
                            <HeaderStyle Font-Bold="True" Height="22px" Font-Size="Small"  BackColor="#E33439" 
                            ForeColor="White" />
                            <ItemStyle Width="50%" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="nombre_Tnot" HeaderText="TIPO" >
                        <HeaderStyle Font-Bold="True" Height="22px" Font-Size="Small"  BackColor="#E33439" 
                            ForeColor="White" />
                            <ItemStyle Width="20%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="enviado_desc" HeaderText="ESTADO" >
                            <HeaderStyle Font-Bold="True" Height="22px" Font-Size="Small" BackColor="#E33439" 
                            ForeColor="White" />
                            <ItemStyle Width="15%" />
                        </asp:BoundField>
                        
                        <asp:TemplateField HeaderText="ENVIAR" HeaderStyle-Width="10%" HeaderStyle-ForeColor="White">
                                <ItemTemplate>
                                
                                           <asp:Button ID="Button1" runat="server" Text="" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                         CommandName="Enviar" Width="18px" Height="20px" class="aprobar" ToolTip="Enviar"  />
                                      
                                </ItemTemplate>
                        <HeaderStyle BackColor="#E33439" />
                        <ItemStyle  />
                    </asp:TemplateField>
                    </Columns>

    <AlternatingRowStyle BackColor="#F7F6F4"></AlternatingRowStyle>
                </asp:GridView>
    </div>
     <div id="Registro" runat="server" visible="false">
      <input type="hidden" id="txtid" value="" runat="server" />
      <asp:HiddenField ID="txts" Value="" runat="server" />      
                 <table width="100%" >
                 <tr>
                 <td colspan="2" style="text-align:center; background-color:#E33439; font-weight:bold; color:White; height:22px" id="tdRegistro" runat="server"></td>
                 </tr>
                 <tr>
                 <td style="width:20%">T&iacute;tulo</td>
                 <td>
                 <input type="text" id="txtTitulo" value="" runat="server" style="width:98%;text-align:left;"  autocomplete="off"  /></td>
                 </tr>
                 <tr>
                 <td>Descripci&oacute;n</td>
                 <td>
                     <asp:TextBox ID="txtDescripcion" runat="server"  TextMode="multiline" style="width:98%;" Rows="5"></asp:TextBox>
                 </td>
                 </tr>
                 <tr>
                 <td>Noti Personal</td>
                 <td>
                    <asp:DropDownList ID="ddlTipoNot"   runat="server" AutoPostBack="False"  style="width:98%;font-size:12px;" >
                    </asp:DropDownList>
                 
                 </td>
                 </tr>
                 <tr>
                 <td>Archivo</td>
                 <td>
                  
                 <asp:FileUpload ID="fileArchivo" runat="server" CssClass="etiqueta" />
                 <div id="divLnk" runat="server"></div>  
                 </td>
                 </tr>
                  <tr>
                 <td>Desc. Archivo</td>
                 <td>
                     <asp:TextBox ID="txtFileDesc" runat="server"  TextMode="multiline" style="width:98%;" Rows="5"></asp:TextBox>
                 </td>
                 </tr>
                 <tr>
                 <td>Imagen</td>
                 <td>
                <asp:Image ID="img" runat="server" Width="100px" Height="100px" BorderWidth="1" />
                 <asp:FileUpload ID="fileImagen" runat="server"  CssClass="etiqueta" Width/>
                 <asp:RegularExpressionValidator ID="REGEXFileUploadLogo" runat="server" ErrorMessage="Solo Imagenes" ControlToValidate="fileImagen" ValidationExpression= "(.*).(.jpg|.JPG|.gif|.GIF|.jpeg|.JPEG|.bmp|.BMP|.png|.PNG)$" />
                 </td>
                 </tr>
                  <tr>
                 <td>Desc. Imagen</td>
                 <td><asp:TextBox ID="txtImagenDesc" runat="server"  TextMode="multiline" style="width:98%;" Rows="5"></asp:TextBox></td>
                 </tr>
                 <tr>
                 <td colspan=2 style="text-align:center">
                 <div id="divEnviar" runat=server >
                 <fieldset>
                 <legend>Enviar a: </legend>
                 <asp:DropDownList ID="ddlGrupo"   runat="server" AutoPostBack="True"  style="width:98%;font-size:12px;" >
                    </asp:DropDownList>
                 </fieldset>
                 </div>
                 </td>
                 </tr>
                 <tr>                
                    <td colspan="2" style="text-align:center">
                    <asp:Button ID="btnGrabar" runat="server" class="guardar2" Text="Grabar" Width="100px" Height="20px"/>
                    <asp:Button ID="btnCancelar" runat="server" class="regresar2" Text="Cancelar" Width="100px" Height="20px"/>
                    </td>
                 </tr>
                 </table>
     </div>
    </form>
</body>
</html>
