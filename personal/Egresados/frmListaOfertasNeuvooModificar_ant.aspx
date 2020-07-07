<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmListaOfertasNeuvooModificar_ant.aspx.vb" Inherits="Egresados_frmListaOfertasNeuvooModificar" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />    
    <script type="text/javascript" language="JavaScript" src="../../private/funciones.js"></script>
    <script type="text/javascript" language="JavaScript" src="../../private/jq/jquery-1.4.2.min.js"></script>
	<script type="text/javascript" language="JavaScript" src="../../private/jq/lbox/thickbox.js"></script>
	<link rel="stylesheet" href="../../private/jq/lbox/thickbox.css" type="text/css" media="screen" />
    <script src="jquery/jquery-1.9.1.js" type="text/javascript"></script>   
   
    <link href="../assets/bootstrap-4.1/css/bootstrap.css" rel="stylesheet" type="text/css" />
    
    <script src="../../private/calendario.js"></script>
    <script language="javascript">
        function solonumeros() {
            var tecla = window.event.keyCode;
            if (tecla < 48 || tecla > 57) {
                window.event.keyCode = 0;
            }
        }
        $(document).ready(function() {
            $(".contador").each(function() {
                var longitud = $(this).val().length;
                if (longitud == 0) {
                    longitud = 8000;
                }
                var nueva_longitud = $(this).val().length;
                $(this).parent().find('#longitud_textarea').html('Max 8000 caracteres. <i>Quedan: ' + (8000 - nueva_longitud) + ' caracteres</i>');

                $(this).keyup(function() {
                    var nueva_longitud = $(this).val().length;
                    $(this).parent().find('#longitud_textarea').html('<i>Max 8000 caracteres. Quedan: ' + (8000 - nueva_longitud) + ' caracteres</i>');
                    if (nueva_longitud >= "8000") {
                        $('#longitud_textarea').css('color', '#ff0000');
                        var text = $(".contador").val();
                        var new_text = text.substr(0, 8000);
                        $(".contador").val(new_text);
                        var nueva_longitud = $(this).val().length;
                        $(this).parent().find('#longitud_textarea').html('Max 8000 caracteres. <i>Quedan: ' + (8000 - nueva_longitud) + ' caracteres</i>');
                    }
                });
            });
            $(".contador2").each(function() {
                var longitud = $(this).val().length;
                if (longitud == 0) {
                    longitud = 8000;
                }
                var nueva_longitud = $(this).val().length;
                $(this).parent().find('#longitud_textarea2').html('Max 8000 caracteres. <i>Quedan: ' + (8000 - nueva_longitud) + ' caracteres</i>');

                $(this).keyup(function() {
                    var nueva_longitud = $(this).val().length;
                    $(this).parent().find('#longitud_textarea2').html('<i>Max 8000 caracteres. Quedan: ' + (8000 - nueva_longitud) + ' caracteres</i>');
                    if (nueva_longitud >= "8000") {
                        $('#longitud_textarea2').css('color', '#ff0000');
                        var text = $(".contador").val();
                        var new_text = text.substr(0, 8000);
                        $(".contador").val(new_text);
                        var nueva_longitud = $(this).val().length;
                        $(this).parent().find('#longitud_textarea2').html('Max 8000 caracteres. <i>Quedan: ' + (8000 - nueva_longitud) + ' caracteres</i>');
                    }
                });
            });

        });
    </script>

    <style type="text/css">
        #txtDescripcion
        {
            height: 63px;
            width: 430px;
        }
        #txtRequisitos
        {
            height: 52px;
            width: 428px;
        }
        .style1
        {
            width: 36%;
        }
    </style>

    </head>
<body>
    <form id="form1" runat="server">
    <div style="100%: ;">
    <table cellpadding="3" cellspacing="0" style="border: 1px solid #C2CFF1; width:100%">
        <tr>
            <td colspan="2" style="width: 100%; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #0099FF;" 
                height="40px" bgcolor="#E6E6FA">
            <asp:Label ID="lblTitulo" runat="server" Text="Ofertas Laborales Neuvoo" 
                Font-Bold="True" Font-Size="11pt"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
        </tr>
       <tr><td colspan=2>&nbsp;<br /><b>Datos de la Oferta Laboral</b><br /></td></tr>
        <tr>
            <td style="width:12%">Título:</td>               
            <td>
                <asp:TextBox ID="txtTitulo" runat="server" Width="47%"></asp:TextBox>
            </td>          
        </tr>
        <tr>
            <td style="width:12%">Descripción:</td>               
            <td>
                
             <textarea ID="txtDescripcion" runat="server" class="contador" 
                        maxlength="8000" name="S1"></textarea>&nbsp;&nbsp;
                    <div ID="longitud_textarea"></div>
            </td>            
        </tr>
        <tr>
            <td style="width:12%">Requisitos</td>               
            <td>
                <textarea ID="txtRequisitos" runat="server" class="contador2" 
                        maxlength="8000" name="S2"></textarea>
                    <div ID="longitud_textarea2"></div>
            </td>               
        </tr>
        <tr>
            <td style="width:12%">Empresa:</td>               
            <td>
                <asp:TextBox ID="txtEmpresa" runat="server" Width="46%" Height="22px"></asp:TextBox>
                <asp:Button ID="btnBusca" class="btn btn-primary" runat="server" Text="Buscar" CssClass="buscar2" Width="70px" Height="22px" />
                &nbsp;&nbsp;<asp:Button ID="btnCancelarBusqueda" runat="server" Text="Cerrar" 
                    CssClass="regresar2" Width="70px" Height="22px" Visible="False" />
                &nbsp;<asp:Button ID="btnCancelarBusqueda0" runat="server" Text="Nueva" 
                    CssClass="agregar2" Width="70px" Height="22px" />
                &nbsp;&nbsp;
                <a href="http://www.sunat.gob.pe/cl-ti-itmrconsruc/jcrS00Alias" target="_blank" style="text-decoration:underline">Validar en Sunat</a>
                </td>               
        </tr>
        </table>
       <!-- <tr>
            <td class="style1" colspan="4">-->
    <br /><br />
    <asp:Panel ID="PanelNuevaEmpresa" runat="server" visible="false" style="border:1px solid #C2CFF1;">
    
    <table>
    <tr>
    <td>Nombre</td>
    <td>
        <asp:TextBox ID="txtnombre" runat="server" Width="396px"></asp:TextBox>
        </td>
    </tr>
    <tr>
    <td>RUC</td>
    <td>
        <asp:TextBox ID="txtruc" runat="server" Width="179px" MaxLength="11" ></asp:TextBox>
        </td>
    </tr>
    <tr>
    <td>Dirección</td>
    <td>
        <asp:TextBox ID="txtdirecion" runat="server" Width="393px"></asp:TextBox>
        </td>
    </tr>
    <tr><td>
        <asp:Button ID="btnGuardarEmpresa" CssClass="agregar2" runat="server" Text="Registrar" Width="110px" Height="22px"  />
        </td><td>
        <asp:Button ID="btnCancelar" CssClass="regresar2" runat="server" Text="Cancelar" Width="110px" Height="22px"  />
        &nbsp;&nbsp;
                <asp:Label ID="lblMensaje0" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
        </td></tr>
    </table>
  </asp:Panel>
        <br />
                <asp:Label ID="lblFiltroRuc" runat="server" Text="RUC ó Nombre:"></asp:Label>
                &nbsp;&nbsp;
                <asp:TextBox ID="txtFiltro" runat="server" Width="40%"></asp:TextBox>
                <asp:Button ID="btnBusqueda" runat="server" CssClass="buscar2" Text="Buscar" Width="80px" Height="22px" />
                <BR />
        
                <asp:GridView ID="gvEmpresa" runat="server" Width="80%" 
                    AutoGenerateColumns="False" DataKeyNames="idPro" AllowPaging="True" 
                    PageSize="5" Visible="False" UseAccessibleHeader="False">
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
        
        <table style="border: 1px solid #C2CFF1; width:100%">
        <tr><td colspan=4><br /><br /></td></tr>
        <tr>
            <td style="width:12%">Selecciona Oferta:</td>               
            <td class="style1">                
                <asp:RadioButton ID="radioNacional" runat="server" GroupName="via" 
                    Text="Nacional" AutoPostBack="True"  />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:RadioButton ID="radioInternacional" runat="server" 
                    GroupName="via" Text="Internacional" 
                    AutoPostBack="True" />
            </td> 
            <td style="width:12%">&nbsp;</td>                           
            <td style="width:38%">
                &nbsp;</td>                         
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
                <asp:TextBox ID="txtDuracion" runat="server" Width="10%" MaxLength="2" 
                    Visible="False" ></asp:TextBox>
                <asp:DropDownList ID="dpDuracion" runat="server" Height="21px" Width="8%" 
                    Visible="false">
                </asp:DropDownList>
            </td>                         
            </tr>
            <tr>
            <td style="width:12%">Tipo de Trabajo:</td>               
            <td>
                <asp:DropDownList ID="dpTrabajo" runat="server" Width="40%" >
                </asp:DropDownList>
            </td>    
            <td style="width:12%">Tipo de Oferta:</td>               
            <td>
                <asp:DropDownList ID="dpTipoOferta" runat="server" Width="50%">
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
                <asp:TextBox ID="txtCorreo" runat="server" Width="51%"></asp:TextBox>
                <asp:CheckBox ID="chkMostrarcorreo" runat="server" Text="Mostrar Correo" />
            </td>          
            <td style="width:12%">Sector:</td>               
            <td>
                <asp:DropDownList ID="dpSector" runat="server" Width="50%">
                </asp:DropDownList>
            </td>                
            </tr>
            <tr>
            <td style="width:12%">Web:</td>               
            <td style="width:35%">
                http://<asp:TextBox ID="txtWeb" runat="server" Width="100%"></asp:TextBox>
            </td>          
            <td style="width:12%">Publicación:</td>               
            <td>
                <asp:TextBox ID="txtInicioPublica" runat="server"></asp:TextBox>
                <input onclick="MostrarCalendario('txtInicioPublica')" type="button" value="..." /></td>                
            </tr>
            <tr>
            <td style="width:12%;">Postular vía:</td>               
            <td>
                &nbsp;<asp:RadioButton ID="radioCorreo" runat="server" GroupName="via" 
                    Text="Correo" AutoPostBack="True" Enabled="False"  />
&nbsp;<asp:RadioButton ID="radioWeb" runat="server" GroupName="via" Text="Web" 
                    AutoPostBack="True" />
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
                <asp:DropDownList ID="dpCarrera" runat="server" Height="16px" Width="455px">
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
            &nbsp;&nbsp;&nbsp;
            </td>          
            <td style="width:12%">&nbsp;</td>               
            <td>&nbsp;</td>                
            </tr>
            <tr>
            <td style="width:12%">&nbsp;</td>               
            <td style="width:38%">                
                &nbsp;</td> 
            <td style="width:12%">&nbsp;</td>                           
            <td style="width:38%">
                &nbsp;</td>                         
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
