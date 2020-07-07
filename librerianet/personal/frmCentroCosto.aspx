<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmCentroCosto.aspx.vb" Inherits="personal_Default2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../private/estilo.css" rel="stylesheet" type="text/css" />
    <link href="../private/estiloweb.css" rel="stylesheet" type="text/css" />
    
    <script type="text/javascript" language="javascript">
        function MarcarCursos(obj){
           //asignar todos los controles en array
            var arrChk = document.getElementsByTagName('input');
            for (var i = 0 ; i < arrChk.length ; i++){
                var chk = arrChk[i];
                //verificar si es Check
                if (chk.type == "checkbox"){
                    chk.checked = obj.checked;
                    if (chk.id != obj.id) {
                        PintarFilaMarcada(chk.parentNode.parentNode,obj.checked)
                    }
                }
            }
        }
               
        function PintarFilaMarcada(obj,estado){
            if (estado==true){
                obj.style.backgroundColor="#FFE7B3"
            } else{
                obj.style.backgroundColor="white"
            }
        }        
    </script>
    
    
    <style type="text/css">
        .style2
        {
            width: 7%;
        }
    </style>
    
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    <table width="100%">
        <tr>
            <td >
                   <table width="100%" style="width: 100%" class="contornotabla">
                        <tr  bgcolor="#336666">
                            <td colspan="6" align="left" style="width:95%; height:30px" 
                            style="width: 100%; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #3B794E">
                            
                                <asp:Label ID="Label4" runat="server" Text="+Configuración de Centros de Costos por Tipo de Actividad" 
                                     ForeColor="White" ></asp:Label>
                            </td>
                            <td style="width:5%; height:30px" >&nbsp;</td>
                        </tr>
                        <tr bgcolor="#F5F9FC">
                            <td style="width:30%">
                                <asp:Label ID="Label1" runat="server" Text="Tipo de Actividad"></asp:Label>
                            </td>
                            <td bgcolor="#F5F9FC" colspan="5">
                                <asp:DropDownList ID="ddlTipoActividad" runat="server" Width="99%" 
                                    AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                            <td bgcolor="#F5F9FC">&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width:27%">
                                <asp:Label ID="lblEsFacuDep" runat="server" 
                                    Text="Escuela / Facultad / Departamento" Width="100%"></asp:Label>
                            </td>
                            <td colspan="5" rowspan="2">
                                <asp:DropDownList ID="ddlEsFacuDep" runat="server" Width="99%" 
                                    AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:RadioButton ID="rdbDepartamento" runat="server" AutoPostBack="True" 
                                                GroupName="Opciones" Text="Departamento" />
                                        </td>
                                        <td>
                                            <asp:RadioButton ID="rdbFacultad" runat="server" AutoPostBack="True" 
                                                GroupName="Opciones" Text="Facultad" />
                                        </td>
                                        <td>
                                            <asp:RadioButton ID="rdbEscuela" runat="server" AutoPostBack="True" 
                                                Text="Escuela" GroupName="Opciones" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td colspan="5">&nbsp;
                            </td>
                        </tr>                                                
                        </table>                                                            
                        <br />
                        <table width="100%">
                        <tr style="height: 32px">
                            <td  bgcolor="#F5F9FC" style="width:30%">
                                <asp:Label ID="Label3" runat="server" Text="Buscar Centro de Costo"></asp:Label>
                            </td>
                            <td  bgcolor="#F5F9FC" colspan="4" style="width:50%">
                                <asp:TextBox ID="txtCentroCosto" runat="server" Width="98%"></asp:TextBox>
                                </td>
                            <td colspan="2" bgcolor="#F5F9FC" align="right" style="width:7%">
                                <asp:Button ID="btnBuscar" runat="server" Text="         Buscar" Width="98%" 
                                    Height="25px"
                                    CssClass="Buscar" />
                            </td>                            
                        </tr>
                        <tr>
                            <td colspan>
                                &nbsp;</td>
                            <td colspan="2">&nbsp;</td>
                            <td>
                                <asp:Button ID="btnAgregar" runat="server" Text="&gt;&gt;" Width="98%" 
                                    CssClass="agregar2" Font-Bold="True"/>
                                <br /> 
                                <asp:Button ID="btnQuitar" runat="server" Text="&lt;&lt;"  Width="98%" 
                                    CssClass="agregar2" Font-Bold="True"/>
                            </td>
                            <td colspan="2">&nbsp;</td>
                            <td></td>
                        </tr>
                        <tr>
                            <td colspan="3" rowspan="6" >
                                <asp:GridView ID="gvCCO1" runat="server" AutoGenerateColumns="False" 
                                    DataKeyNames="codigo_cco" CellPadding="4" 
                                    Width="100%" BackColor="White" BorderColor="#336666" BorderStyle="Double" 
                                    BorderWidth="3px">
                                    <RowStyle BackColor="White" ForeColor="#333333" />
                                    <Columns>
                                        
                                        <asp:TemplateField HeaderText="CHECK">
                                             <ItemTemplate>
                                                <asp:CheckBox ID="chkSeleccion" runat="server" Width="5px" />
                                             </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        <asp:BoundField DataField="codigo_cco" HeaderText="Codigo" Visible="False" />
                                        <asp:BoundField DataField="descripcion_Cco" HeaderText="DESCRIPCION" />
                                    
                                    </Columns>
                                    <FooterStyle BackColor="White" ForeColor="#333333" />
                                    <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
                                </asp:GridView>
                            </td>
                            <td style="width:7%">
                                
                            </td>
                            <td colspan="2" rowspan="6" style="width:30%" valign="top">
                                <asp:GridView ID="gvCCO2" runat="server" AutoGenerateColumns="False"  Width="100%"
                                    CellPadding="4" DataKeyNames="Codigo" BackColor="White" 
                                    BorderColor="#336666" BorderStyle="Double" BorderWidth="3px">
                                    <RowStyle BackColor="White" ForeColor="#333333" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="CHECK">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSeleccion" runat="server" />
                                             </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Codigo" HeaderText="Codigo" Visible="False" />
                                        <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" />
                                    </Columns>
                                    <FooterStyle BackColor="White" ForeColor="#333333" />
                                    <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
                                </asp:GridView>
                            </td>
                          
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td></td>
                        </tr>
                        <tr>
                            <td align="center">
                                <br/><br/>
                                
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td align="center">
                                &nbsp;</td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td></td>
                            <td align="right" class="style2">
                                </td>
                            <td></td>
                            <td></td>
                            <td>
                                &nbsp;</td>
                            <td></td>
                        </tr>
                        </table>
            </td>
        </tr>
    </table>
    
    </div>
    
    <div id="divMigracion" style="visibility:hidden">
        
    </div>
    </form>
</body>
</html>
