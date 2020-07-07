<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmOfertaLaboral.aspx.vb" Inherits="Egresado_frmOfertaLaboral" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />    
    <script type="text/javascript" language="JavaScript" src="../../private/funciones.js"></script>
    <script type="text/javascript" language="JavaScript" src="../../private/jq/jquery-1.4.2.min.js"></script>
	<script type="text/javascript" language="JavaScript" src="../../private/jq/lbox/thickbox.js"></script>
	<link rel="stylesheet" href="../../private/jq/lbox/thickbox.css" type="text/css" media="screen" />
    <script src="../../private/calendario.js"></script>
    <script language="javascript">
        function solonumeros() {
            var tecla = window.event.keyCode;
            if (tecla < 48 || tecla > 57) {
                window.event.keyCode = 0;
            }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table cellpadding="3" cellspacing="0" style="border: 1px solid #C2CFF1; width:100%">
        <tr>
            <td colspan="4" style="width: 100%; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #0099FF;" 
                height="40px" bgcolor="#E6E6FA">
            <asp:Label ID="lblTitulo" runat="server" Text="Ofertas Laborales" 
                Font-Bold="True" Font-Size="11pt"></asp:Label></td>
        </tr>
        <tr>
            <td style="width:12%">Título:</td>               
            <td colspan="3">
                <asp:TextBox ID="txtTitulo" runat="server" Width="50%"></asp:TextBox>
            </td>          
        </tr>
        <tr>
            <td style="width:12%">Descripción:</td>               
            <td colspan="3">
                <asp:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine" 
                    Width="50%"></asp:TextBox>
            </td>            
        </tr>
        <tr>
            <td style="width:12%">Requisitos</td>               
            <td colspan="3">
                <asp:TextBox ID="txtRequisitos" runat="server" TextMode="MultiLine" Width="50%"></asp:TextBox>
            </td>               
        </tr>
        <tr>
            <td style="width:12%">Empresa:</td>               
            <td colspan="3">
                <asp:TextBox ID="txtEmpresa" runat="server" Width="42%"></asp:TextBox>
                <asp:Button ID="btnBusca" runat="server" Text="Buscar" CssClass="buscar2" Width="70px" Height="22px" />
                </td>               
        </tr>
        <tr>
            <td></td>
            <td colspan="3">
                <asp:Label ID="lblFiltroRuc" runat="server" Text="RUC ó Nombre:"></asp:Label>
                &nbsp;&nbsp;
                <asp:TextBox ID="txtFiltro" runat="server" Width="40%"></asp:TextBox>
                <asp:Button ID="btnBusqueda" runat="server" CssClass="buscar2" Text="Buscar" Width="80px" Height="22px" />
                <asp:GridView ID="gvEmpresa" runat="server" Width="100%" 
                    AutoGenerateColumns="False" DataKeyNames="idPro" AllowPaging="True" 
                    PageSize="5">
                    <Columns>
                        <asp:BoundField DataField="idPro" HeaderText="idPro" Visible="False" />
                        <asp:BoundField DataField="nombrePro" HeaderText="Empresa" >
                            <ItemStyle Width="45%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="emailPro" HeaderText="Correo">
                            <ItemStyle Width="35%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="telefonoPro" HeaderText="Telefono">
                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                        </asp:BoundField>                        
                        <asp:CommandField ShowSelectButton="True">
                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                        </asp:CommandField>
                    </Columns>
                    <HeaderStyle BackColor="#0B3861" ForeColor="White" Height="25px" />                
                    <RowStyle Height="20px" />  
                </asp:GridView>
                
            </td>
        </tr>
        <tr>
            <td style="width:12%">Departamento:</td>               
            <td style="width:38%">                
                <asp:DropDownList ID="dpDepartamento" runat="server" Width="40%">
                </asp:DropDownList>
            </td> 
            <td style="width:12%">Lugar:</td>                           
            <td style="width:38%">
                <asp:TextBox ID="txtLugar" runat="server" Width="48.5%"></asp:TextBox>
            </td>                         
        </tr>
        <tr>
            <td style="width:12%">Tipo de Trabajo:</td>               
            <td>
                <asp:DropDownList ID="dpTrabajo" runat="server" Width="40%" >
                </asp:DropDownList>
            </td>    
            <td style="width:12%">Duración:</td>               
            <td>
                <asp:TextBox ID="txtDuracion" runat="server" Width="10%" MaxLength="2" ></asp:TextBox>
                &nbsp;<asp:DropDownList ID="dpDuracion" runat="server" Height="20px" Width="36.5%">
                </asp:DropDownList>
            </td>                
        </tr>
        <tr>
            <td style="width:12%">Contacto:</td>               
            <td>
                <asp:TextBox ID="txtContacto" runat="server" Width="95%"></asp:TextBox>
            </td>          
            <td style="width:12%">Teléfono:</td>               
            <td>
                <asp:TextBox ID="txtTelefono" runat="server" Width="48.5%"></asp:TextBox>
            </td>                
        </tr>
        <tr>
            <td style="width:12%">Correo:</td>               
            <td>
                <asp:TextBox ID="txtCorreo" runat="server" Width="95%"></asp:TextBox>
            </td>          
            <td style="width:12%">Sector:</td>               
            <td>
                <asp:DropDownList ID="dpSector" runat="server" Width="50%">
                </asp:DropDownList>
            </td>                
        </tr>
        <tr>
            <td style="width:12%;">Publicación:</td>               
            <td>
                <asp:TextBox ID="txtInicioPublica" runat="server"></asp:TextBox>
                <input onclick="MostrarCalendario('txtInicioPublica')" type="button" value="..." />
            </td>          
            <td style="width:12%">Fin Publicación:</td>               
            <td>
                <asp:TextBox ID="txtFinPublica" runat="server"></asp:TextBox>
                <input onclick="MostrarCalendario('txtFinPublica')" type="button" value="..." />
            </td>                
        </tr>        
        <tr>
            <td style="width:12%">
                <asp:Label ID="Label1" runat="server" Text="Carrera Profesional: "></asp:Label>  
                </td>               
            <td colspan="3">
                <asp:DropDownList ID="dpCarrera" runat="server">
                </asp:DropDownList>  
                <asp:Button ID="btnAgregar" runat="server" Text="Agregar" CssClass="agregar2" Width="100px" Height="22px" />
                <br />
                <asp:GridView ID="gvDetalles" runat="server" Width="50%" DataKeyNames="codigo_ofc,codigo_cpf" 
                    AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="codigo_ofc" HeaderText="codigo_ofc"  
                            Visible="False" />
                        <asp:BoundField DataField="nombre_Cpf" HeaderText="Carrera Profesional" />
                        <asp:CommandField ShowDeleteButton="True" ButtonType="Image" 
                            DeleteImageUrl="../../images/eliminar.gif" HeaderText="Eliminar">
                            <ItemStyle HorizontalAlign="Center" Width="20%" />
                        </asp:CommandField>
                        <asp:BoundField DataField="codigo_cpf" HeaderText="codigo_cpf" 
                            Visible="False" />
                    </Columns>                 
                    <EmptyDataTemplate>
                        <table style="background:#0B3861" width="100%">
                            <tr style="height:25px">
                                <td align="center">
                                    <asp:Label ID="Label2" runat="server" Text="Carrera Profesional" ForeColor="White" Width="100%"></asp:Label>
                                </td>                            
                                <td align="center" style="width:20%">
                                    <asp:Label ID="Label3" runat="server" Text="Eliminar" ForeColor="White" Width="100%"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>                       
                    <HeaderStyle BackColor="#0B3861" ForeColor="White" Height="25px" />                     
                    <RowStyle Height="20px" />                      
                </asp:GridView>
            </td>                
        </tr>
        <tr>
            <td style="width:12%">&nbsp;</td>               
            <td colspan="3">
                <asp:CheckBox ID="chkMostrar" runat="server" Text="Mostrar Oferta Laboral" />
            </td>                
        </tr>
        <tr>
            <td style="width:12%">&nbsp;</td>               
            <td>
                <asp:Label ID="lblMensaje" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
            </td>          
            <td style="width:12%"></td>               
            <td></td>                
        </tr>
        <tr>
            <td style="width:12%">&nbsp;</td>               
            <td>
                <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="agregar2" Width="110px" Height="22px" />&nbsp;&nbsp;
                <asp:Button ID="btnSalir" runat="server" Text="Salir" CssClass="regresar2" Width="110px" Height="22px" />
            </td>          
            <td style="width:12%">&nbsp;</td>               
            <td>&nbsp;</td>                
        </tr>
    </table>
    </div>
    <asp:HiddenField ID="HdCodigo_pro" runat="server" />
    <asp:HiddenField ID="HdAccion" runat="server" />
    <asp:HiddenField ID="HdDirectorAlumni" runat="server" />
    <asp:HiddenField ID="HdCodigo_Ofe" runat="server" />
    </form>
</body>
</html>
