<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TipoPropuestaLista_bk.aspx.vb" Inherits="administrativo_propuestas2_proponente_TipoPropuestaLista" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../estilo.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../funciones.js"> </script>
    <script src="../../../scripts/js/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script type="text/javascript"> 
        function HabilitarBoton(tipo,fila)
  	    {

  	        document.form1.txtelegido.value=fila.id
  	       // alert(document.form1.txtelegido.value)
      	   
    	    if (document.form1.cmdModificar!=undefined){
                document.form1.cmdModificar.disabled=false 
                if (document.form1.cmdConforme!=undefined){
                    document.form1.cmdConforme.disabled=false 
                    document.form1.cmdNoConforme.disabled=false 
                    document.form1.cmdObservado.disabled=false 
                    
                    if (document.form1.cmdDerivar!=undefined){
                        document.form1.cmdDerivar.disabled=false
                    }
                }
                
    	    }
            if (document.form1.ddlInstanciaRevision.value!='P'){
            document.form1.cmdModificar.disabled=true;
            }
    	    SeleccionarFila();
    	    AbrirPestana(0,'datospropuesta.aspx')
	    }

	    function AbrirPestana(tab, pagina) {
	        if (document.form1.txtelegido.value > 0) {
	            ResaltarPestana(tab, '', '')
	            pagina = pagina + "?codigo_prp=" + $("#txtelegido").val() + "&codigo_per=" + $("#lblUsuario").val();
	            $("#fradetalle").attr("src", pagina);
	        }
	    }


    </script>
    <title>Página sin título</title>
    <style type="text/css">
        .style1
        {
            width: 54px;
        }
    </style>
    </head>
<body bgcolor="#F0F0F0">
    <form id="form1" runat="server">
   <table style="width: 100%; margin-bottom: 229px;" align="left">
            <tr>
                <td class="contornotabla" valign="top" width="100%" colspan="6">
                    <table  width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                          <td bgcolor="#F0F0F0" class="bordeinf" width="20%"><table style="width:100%;">
                            <tr>
                                <td>TIPO DE PROPUESTA</td>
                                <td>
                                    <asp:DropDownList ID="ddlTipoPropuesta" runat="server" Height="20px" Width="400px"></asp:DropDownList>
                                </td>
                                <td class="style1">Estado</td>
                                <td>
                                    <asp:DropDownList ID="ddlEstado" runat="server" Height="21px" Width="114px">
                                        <asp:ListItem>ACTIVO</asp:ListItem>
                                        <asp:ListItem>NO ACTIVO</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>CENTRO DE COSTOS</td>
                                <td>
                                    <asp:DropDownList ID="ddlCentroCostos" runat="server" Height="20px" Width="400px"></asp:DropDownList>
                                </td>
                                <td class="style1"></td>
                                <td>
                                    <asp:HiddenField ID="hdcodigo_tpc" runat="server" Value="0"/>
                                </td>
                                <td></td>
                                <td></td>
                            </tr>
                            
                            <tr>
                              <td colspan="6">
                                  <asp:Button ID="cmdConsultar" runat="server" CssClass="nuevo1" Height="47px" Text="        Consultar" Width="120px" />                          
                                  <asp:Button ID="cmdGuardar" runat="server" Text="        Guardar" CssClass="guardar_prp" Height="35px" Width="120px" ValidationGroup="Guardar" />                            
                              </td>
                                
                            </tr>
                          </table>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                    </tr>
                        
                    </table>
                        <asp:GridView ID="dgvPropuestas" runat="server" Width="100%" AutoGenerateColumns="False"
                            PageSize="15" CellPadding="2" ForeColor="#333333" Font-Size="Smaller" DataKeyNames="codigo_Tpr,codigo_Cco,codigo_tpc">
                            <RowStyle Height="20px" />
                            <Columns>
                                <asp:BoundField DataField="Descripcion_Tpr" HeaderText="Tipo de Propuesta" />
                                <asp:BoundField DataField="descripcion_Cco" HeaderText="Centro de Costo" />
                                <asp:BoundField DataField="estado" HeaderText="Estado" />                               
                                <asp:TemplateField HeaderText="Eliminar" ShowHeader="False">
                                <ItemTemplate>
                                    <asp:ImageButton ID="Eliminar" runat="server" CausesValidation="False" 
                                        CommandName="Delete" ImageUrl="../../../Images/menus/noconforme_small.gif"
                                        OnClientClick="return confirm('¿Desea Eliminar Registro?.')"  Text="Eliminar" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="60px" />
                            </asp:TemplateField>
                            </Columns>
                            <HeaderStyle BackColor="#666666" ForeColor="White" Height="22px" />
                        </asp:GridView>
                </td>
            </tr>
            <tr>
                <td class="contornotabla" colspan="6">
                    <asp:Panel ID="Panel1" runat="server" Height="100%"></asp:Panel>
                </td>
            </tr>
            <tr>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
            </tr>
        </table>
    </form>
</body>
</html>
