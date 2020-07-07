<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GrupoAviso.aspx.vb" Inherits="GruposAviso_GrupoAviso" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Grupo de Avisos Camp&uacte;s Estudiante</title>
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
        .style1
     {
         width: 50%;
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
    <asp:GridView ID="lstGrupo" runat="server" AutoGenerateColumns="False" 
                    DataKeyNames="codigo_Gav"  BorderStyle="None" 
                 AlternatingRowStyle-BackColor="#F7F6F4" Width="100%" 
            Font-Size="X-Small"  class="display">
                    <Columns> <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="BtnMostrar" runat="server" Text="Editar" class="btn" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                 CommandName="Editar"  />
                                 <asp:Button ID="BtnAcceso" runat="server" Text="Acceso" class="btn" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                 CommandName="Acceso"  />
                        </ItemTemplate>
                        <HeaderStyle BackColor="#E33439" ForeColor="White" Height="22px" />
                        <ItemStyle Width="15%" />
                    </asp:TemplateField>
                        <asp:BoundField DataField="nombre_Gav" HeaderText="GRUPO" >
                            <HeaderStyle Font-Bold="True" Font-Size="Small" BackColor="#E33439" 
                            ForeColor="White" Height="22px" />
                            <ItemStyle Width="65%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nombre_tga" HeaderText="TIPO" >
                            <HeaderStyle Font-Bold="True" Font-Size="Small" BackColor="#E33439" 
                            ForeColor="White" Height="22px" />
                            <ItemStyle Width="20%" />
                        </asp:BoundField>
                    </Columns>

    <AlternatingRowStyle BackColor="#F7F6F4"></AlternatingRowStyle>
                </asp:GridView>
    <table style="width:90%; border: 1px black;" align="center">
    <tbody>
        <tr>
            <td colspan="2" style="text-align:center">
                <h3>Configuraci&oacute;n Grupo De Avisos Camp&uacute;s Estudiante</h3></td>
              
        </tr>
        <tr>
           <!-- <td style="width:50%;">
                &nbsp;</td>-->
            <td style="width:100%;text-align:center;" colspan="2">
                <asp:Button ID="btnConsultar" runat="server" class="buscar" Text="Consultar"  Width="100px" Height="22px"  />
                <asp:Button ID="btnNuevo" runat="server" class="nuevo" Text="Registrar"   Width="100px" Height="22px" />
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
    <div id="Lista" runat="server" >
     </div>
    <div id="Registro" runat="server" visible="false">
                 <input type="hidden" id="txtid" value="" runat="server" />
                 <table width="100%" >
                 <tr>
                 <td colspan="2" style="text-align:center; background-color:#E33439; font-weight:bold; color:White; height=22px" id="tdRegistro" runat="server"></td>
                 </tr>
                 <tr>
                 <td style="width:30%">Nombre</td>
                 <td>
                 <input type="text" id="txtNombre" value="" runat="server" 
                         style="width:98%;text-align:left;"  autocomplete="off" 
                         onclick="return txtMaxAdelantar_onclick()" readonly="readonly" /></td>
                 </tr>
                 <tr>
                 <td>Tipo Grupo</td>
                 <td><asp:DropDownList ID="ddlTipo" runat="server" style="width:98%;font-size:11px;" onSelectedIndexChanged="ddlTipo_SelectedIndexChanged" AutoPostBack="True" >
                    </asp:DropDownList>
                                            </td>
                 </tr>
                 <tr>
                 <td>Facultad</td>
                 <td><asp:DropDownList ID="ddlFacultad" runat="server" AutoPostBack="True"  onSelectedIndexChanged="ddlFacultad_SelectedIndexChanged" style="width:98%;font-size:11px;">
                    </asp:DropDownList>
                                            </td>
                 </tr>
                 <tr>
                 <td>Tipo Estudio</td>
                 <td><asp:DropDownList ID="ddlTipoEstudio" runat="server" style="width:98%;font-size:11px;"  AutoPostBack="True" onSelectedIndexChanged="ddlTipoEstudio_SelectedIndexChanged">
                    </asp:DropDownList>
                                            </td>
                 </tr>
                 <tr>
                 <td>Escuela</td>
                 <td><asp:DropDownList ID="ddlEscuela"   runat="server" AutoPostBack="True"  onSelectedIndexChanged="ddlEscuela_SelectedIndexChanged" style="width:98%;font-size:11px;" >
                    </asp:DropDownList>
                                            </td>
                 </tr>
                 <tr>
                 <td colspan="2" style="text-align:center">
                 
                    <asp:Button ID="btnGrabar" runat="server" class="btn" Text="Grabar" />
                    <asp:Button ID="btnCancelar" runat="server" class="btn" Text="Cancelar" />
                 </td>
                 </tr>
                 </table>
                 </div>
    <div id="Accesos" runat="server" visible="false">
    <input type="hidden" id="txtidg" value="" runat="server" />
                 <table width="100%" >
                 <tr>
                 <td colspan="2" style="text-align:center; background-color:#E33439; font-weight:bold; color:White; height=22px" id="tdRegistroG" runat="server"></td>
                 </tr>
                 <tr>
                 </table>
                    <div style="text-align:right;">
                    <asp:Button ID="btnGrabarAcceso" runat="server" class="btn" Text="Grabar" />
                    <asp:Button ID="btnCancelarAcceso" runat="server" class="btn" Text="Cancelar" />
                    </div>
        <asp:GridView ID="lstAcceso" runat="server" AutoGenerateColumns="False" DataKeyNames="acceso,codigo_per"
            class="display" Width="100%">
            <Columns>
                <asp:BoundField HeaderText="Personal" DataField="personal" />
                <asp:TemplateField HeaderText="">
                    <HeaderTemplate>
                        <asp:CheckBox ID="chkHeader" runat="server" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkElegir" runat="server" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField DataField="acceso" Visible="False" />
                <asp:BoundField DataField="codigo_per" HeaderText="codigo_per" Visible="False" />
            </Columns>
        </asp:GridView>
                        
             
               
                    


    </div>
    </form>
    <script language="javascript" type="text/javascript">
        $(document).ready(function() {
            var oTableAcceso = $("#lstAcceso").DataTable({
                "bPaginate": false,
                "bFilter": true,
                "bLengthChange": false,
                "bInfo": true
            });
        });
                 </script>
</body>
</html>
