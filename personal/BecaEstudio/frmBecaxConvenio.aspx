<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmBecaxConvenio.aspx.vb" Inherits="BecaEstudio_frmBecaxConvenio" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <link href="../../private/estilo.css?x=x" rel="stylesheet" type="text/css" />
        <!-- <link href="css/estilos.css" rel="stylesheet" type="text/css" /> -->
        
        <script type="text/javascript" language="JavaScript" src="../../private/funciones.js"></script>
        <script type="text/javascript" language="JavaScript" src="../../private/jq/jquery-1.4.2.min.js"></script>
        <script type="text/javascript" language="javascript" src="../../private/jq/jquery.mascara.js"></script> 
        <script type="text/javascript" language="JavaScript" src="../../private/PopCalendar.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Panel ID="pnlPrincipal" runat="server" Visible="true">
            <table style="width: 100%; class="contornotabla">
            <tr>
                <td bgcolor="#EFF3FB" height="35px" colspan="2">
                    <b>
                    <asp:Label ID="Label11" runat="server" 
                        Text="Asignar Becas por Convenio y Orfandad"></asp:Label></b>
                </td>
            </tr>
            <tr>
                <td align="left" colspan="2" height="10px" class="contornotabla" ;>
                    
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table style="width: 100%" class="">
                        <tr>
                            <td style="width:160px">
                                <asp:Label ID="Label1" runat="server" Text="Ciclo Académico"></asp:Label>
                            </td>                
                            <td style="width:160px">
                                <asp:DropDownList ID="ddlCiclo" Width="100%" runat="server" AutoPostBack="True"></asp:DropDownList>
                            </td> 
                             <td align="right">
                                <asp:Label ID="Label3" runat="server" Text="Tipo"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlTipoBeca" Width="300px" runat="server" AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                             <td>
                                <asp:Label ID="Label12" runat="server" Text="Beneficios"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlCobertura"  Width="100%" runat="server">
                                </asp:DropDownList>
                            </td>
                             <td style="width:160px" align="right">
                                <asp:Label ID="lblConvenio" runat="server" Text="Convenio"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlConvenio" Width="300px" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </td>
            
                
                
               
            </tr>
            <tr>
                <td bgcolor="#EFF3FB" colspan="2">
                    <asp:LinkButton ID="lbkBuscar" runat="server" Font-Bold="true" ForeColor="Blue">Buscar Alumno...</asp:LinkButton>
                </td>
            </tr>
            <tr>
                 <asp:Panel ID="pnlDatosAlumno" Visible="false" runat="server">
                     <!-- Datos del Alumno -->
                            <td valign="top">
                    <table style="width: 100%" class="contornotabla">
                        <tr>
                            <td style="width:100px">
                                <asp:Label ID="Label10" runat="server" Text="Alumno"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblAlumno" runat="server" Font-Bold="False" ForeColor="Blue"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label6" runat="server" Text="Código Universitario"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblCodigoUniv" Font-Bold="False" ForeColor="Blue" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label7" runat="server" Text="Ciclo Ingreso"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblcicloingreso" Font-Bold="False" ForeColor="Blue" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label8" runat="server" Text="Escuela"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblEscuelaprofesional" Font-Bold="False" ForeColor="Blue" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label9" runat="server" Text="Plan Estudios"></asp:Label></td>
                            <td>
                                <asp:Label ID="lblPlanEstudios" Font-Bold="False" ForeColor="Blue" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
                <!--Requisitos -->
                            <td valign="top" style="width:50%; height="100%">
                                <table style="width: 100%; height:100%" class="contornotabla">
                                    <tr class="row-title">                  
                                        <td class="cell cell-3">Requisito</td>
                                        <td class="cell cell-3">Requerido</td>
                                        <td class="cell cell-4">Cumplimiento</td>
                                        <td class="cell cell-5"></td>                    
                                    </tr>                
                                    <div id="tb" runat="server"></div>
                                    <tr>
                                        <td><br />
                                            <div id="btn" runat="server"></div>
                                        </td>
                                    </tr>                
                               </table>   
                </td>
                 </asp:Panel> 
                <!-- :::::::::::::::::::::::::::::::::::::::::::::::: -->
                
                
                
            </tr>
            <tr>
                <td align="left" colspan="2">
                    <asp:Button ID="btnAgregar" CssClass="asignarbecas" runat="server" Height="40" Enabled="false" Text="      Asignar Beca" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                     <asp:GridView ID="gvListaBecas" 
                         Width="100%" runat="server" BackColor="White" 
                         BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                         AutoGenerateColumns="False" 
                         DataKeyNames="codigo_bso" 
                         EmptyDataText="No se encontraron registros...">
                         <RowStyle ForeColor="#000066" />
                         <EmptyDataRowStyle BackColor="#FFFFCC" BorderColor="#3399FF" Font-Bold="True" 
                             ForeColor="#3333CC" />
                         <Columns>
                             <asp:BoundField DataField="codigo_bso" HeaderText="ID" />
                             <asp:BoundField DataField="descripcion_bec" HeaderText="Tipo Beca" />
                             <asp:BoundField DataField="alumno" HeaderText="Apellidos y Nombres" />
                             <asp:BoundField DataField="porcentaje_bco" HeaderText="Beneficio" />
                             <asp:CommandField EditText="Eliminar" HeaderText="Eliminar" 
                                ShowDeleteButton="True" ButtonType="Image" 
                                DeleteImageUrl="../../images/eliminar.gif" DeleteText="" >
                                <ItemStyle Width="8%" HorizontalAlign="Center" />
                            </asp:CommandField>
                         </Columns>
                         <FooterStyle BackColor="White" ForeColor="#000066" />
                         <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                         <SelectedRowStyle BackColor="#FFFFB1" Font-Bold="True" ForeColor="Blue" />
                         <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
        </asp:Panel>   
            
        <!--:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::: -->
            
        <asp:Panel ID="pnlBusqueda" runat="server" Visible="false">
            <!-- Busqueda del Alumno -->
                <table style="width: 100%" class="contornotabla">
            <tr>
                <td bgcolor="#EFF3FB" colspan="2" height="35px">
                    <b>
                    <asp:Label ID="Label5" runat="server" Text="Búsqueda"></asp:Label>
                    <asp:HiddenField ID="HiddenField" runat="server" Value="0" />
                    </b>
                </td>
            </tr>
            <tr>
                <td style="width:160px">
                    <asp:Label ID="Label4" runat="server" Text="Buscar por: "></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlTipo" runat="server">
                        <asp:ListItem Value="0">Apellidos y Nombres</asp:ListItem>
                        <asp:ListItem Value="1">Código</asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="txtBuscarAlumno" runat="server" Width="412px"></asp:TextBox>
                    <asp:Button ID="btnBuscar" Width="100" runat="server" CssClass="buscarBeca" Height="30" Text="    Buscar" />
                    <asp:Button ID="btnEnviar" Width="100" runat="server" CssClass="selecionarBeca" Height="30" Text="    Seleccionar" Visible="false" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:GridView ID="gvLista" 
                         Width="100%" runat="server" BackColor="White" 
                         BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                         AutoGenerateColumns="False" 
                         DataKeyNames="codigo_Alu" 
                         EmptyDataText="No se encontraron registros...">
                         <RowStyle ForeColor="#000066" />
                         <EmptyDataRowStyle BackColor="#FFFFCC" BorderColor="#3399FF" Font-Bold="True" 
                             ForeColor="#3333CC" />
                        <Columns>
                            <asp:BoundField DataField="codigo_Alu" HeaderText="ID" />
                            <asp:BoundField DataField="codigoUniver_Alu" 
                                HeaderText="Código Universitario" />
                            <asp:BoundField DataField="alumno" HeaderText="Apellidos y Nombres" />
                            <asp:CommandField SelectText="" ShowSelectButton="True" />
                        </Columns>
                         <FooterStyle BackColor="White" ForeColor="#000066" />
                         <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                         <SelectedRowStyle BackColor="#FFFFB1" Font-Bold="True" ForeColor="Blue" />
                         <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
            <!-- Fin busqueda -->
        </asp:Panel>
        
        
        
    </div>
    </form>
</body>
</html>
