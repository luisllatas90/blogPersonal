<%@ Page Language="VB" AutoEventWireup="false" CodeFile="seguimientoparticipante.aspx.vb" Inherits="librerianet_aulavirtual_eva_seguimientoparticipante" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Seguimiento por Participante</title>
    <link href="../../../private/estilo.css" rel="stylesheet" 
        type="text/css" />
    <script type="text/javascript" language="JavaScript" src="../../../private/funciones.js"></script>
    <script type="text/javascript" language="javascript">
    function VerFichaParticipante(fila)
  	{
  	    document.form1.txtelegido.value=fila.id
  	    SeleccionarFila();
    	    fradetalle.location.href="../../cargando.asp?rutapagina=aulavirtualprofesores/eva/fichaseguimientoparticipante.aspx?idusuario=" + document.form1.txtelegido.value
	}    
    </script> 
</head>
<body>
    <form id="form1" runat="server">
    <p class="usatTitulo">Registro de accesos a la Plataforma Virtual del Curso</p>
    <table style="width: 100%; height: 90%">
        <tr>
            <td width="35%" height="5%" 
                
                style="background-color: #A9C1D7; color: #FFFFFF; font-weight: bold; text-align: center; border: 1px solid #C0C0C0">
                &nbsp;APELLIDOS Y NOMBRES&nbsp;</td>
            <td width="65%" height="5%" align="right" 
                style="background-image: url('../../../images/fondopestana.gif')">
                <asp:Button ID="cmdImprimir" runat="server" CssClass="imprimir2" 
                    Text="   Imprimir Ficha" UseSubmitBehavior="False" Width="100px" 
                    
                    onclientclick="ImprimirDetalle('REGISTRO DE ACCESO A LA PLATAFORMA VIRTUAL DEL CURSO');return(false);" 
                    Height="25px" />
&nbsp;<asp:Button ID="cmdEmail" runat="server" BackColor="#FEFFE1" BorderColor="#999999" 
                    BorderStyle="Solid" BorderWidth="1px" Font-Size="11px" Height="25px" 
                    Text="Enviar e-mail" UseSubmitBehavior="False" Width="100px" 
                    Visible="False" />
            </td>
        </tr>
        <tr>
            <td valign="top" width="35%" height="95%" class="contornotabla">
                <div id="listadiv" style="width:100%; height:100%">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                        GridLines="Horizontal" ShowHeader="False" Width="100%" 
                        DataKeyNames="idusuario">
                    <Columns>
                        <asp:BoundField HeaderText="Participante" DataField="nombreusuario">
                            <HeaderStyle Width="95%" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
                    <asp:HiddenField ID="txtelegido" runat="server" Value="0" />
                </div>
            </td>
            <td valign="top" width="65%" height="95%" class="contornotabla">
             <iframe id="fradetalle" height="100%" width="100%" border="0" frameborder="0" scrolling="yes" style="height: 100%"> </iframe>
             </td>
        </tr>
    </table>
    </form>
</body>
</html>
