<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmregistrardetallerendicion.aspx.vb" Inherits="frmregistrardetallerendicion" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<script  src="calendario.js"></script>
<link  rel ="stylesheet" href="estilo.css"/> 

    <title>Tesoreria  USAT</title>
    <script  language="javascript">
        function  procesar()
            {
                var x;
                x=confirm(" recuerde que los importes deben estar en la misma moneda del documento de egreso ¿Desea registrar el detalle con la información proporcionada?");
                if (x)
                    {
                
                        tabcontenido.style.display='none';
                        tabmensaje.style.display='';
                    }
                 return (false) ;                    
            }
</script>
</head>
<body bgcolor="#EEEEEE" scroll="auto">
    <form id="form1" runat="server">
    <div style="background-color: #EEEEEE">
        <table id="tabcontenido"  border="1" bordercolor="#EEEEEE" bordercolordark="#EEEEEE" style="border-top-style: none;
            border-right-style: none; border-left-style: none; height: 88px; border-bottom-style: none; width: 1120px;" bgcolor="white" >
            <tr>
                <td  bgcolor=LemonChiffon  colspan="4">
                    <span style="font-family: Courier New">
                    Proporcione los datos de la rendición (*) obligatorios </span></td>
            </tr>
            <tr>
                <td class ="usatCeldaTitulo" style="width: 120" colspan="4">
                    <span style="font-size: 11pt; font-family: Courier New">Detalle de la rendición :</span></td>
            </tr>
            <tr>
                <td style="width: 21383px; height: 21px">
                    <span style="border-top-style: none; border-right-style: none; border-left-style: none;
                        border-bottom-style: none"><span><span><span style="font-size: 11pt">
                            <span><span style="font-family: Courier New">
                            F<span>echa &nbsp; (*)&nbsp; : &nbsp; &nbsp;
                                &nbsp; &nbsp; </span></span></span></span></span></span></span></td>
                <td style="width: 375px; height: 21px">
                    <asp:TextBox ID="txtfecha" runat="server" Width="240px"></asp:TextBox>
                    <input id="Button1" class="cunia" contenteditable="true" onclick="MostrarCalendario('txtfecha')"
                        type="button" /></td>
                <td  style="width: 373px; height: 21px">
                    <span style="font-size: 11pt; color: black; font-family: Arial Narrow">&nbsp;&nbsp;<span
                        style="font-family: Courier New">
                        Importe (*):</span></span></td>
                <td style="width: 813px; height: 21px">
                    <asp:TextBox ID="txtimporte" runat="server" Width="120px"></asp:TextBox>
                    &nbsp; Tipo Documento :<asp:DropDownList ID="cbotipodocumento" runat="server"
                        Width="304px" AutoPostBack="True" TabIndex="1">
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td style="width: 21383px; height: 21px">
                    <span style="font-size: 9pt; font-family: Courier New">
                    Serie/Número:</span>
                </td>
                <td style="width: 375px; height: 21px">
                    <asp:TextBox ID="txtnumero" runat="server" Width="248px" BackColor="White" 
                        TabIndex="2"></asp:TextBox></td>
                <td style="width: 373px; height: 21px">
                    <span style="font-size: 9pt; font-family: Courier New">
                    Empresa / Institución :</span></td>
                <td style="width: 813px; height: 21px">
                    <asp:TextBox ID="txtinstitucion" runat="server" Width="464px" BackColor="White" 
                        TabIndex="3"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 21383px; height: 46px">
                    <span><span style="color: black"><span><span><span style="font-family: Arial Narrow">
                        <span style="font-size: 11pt"><span><span><span style="font-family: Courier New"><span
                            style="font-size: 10pt"><span>Descripción </span>:</span></span></span></span> </span>
                    </span></span></span></span></span>
                </td>
                <td colspan="3" style="height: 46px">
                    <asp:TextBox ID="txtdescripcion" runat="server" Height="64px" Width="1008px" TabIndex="4"></asp:TextBox></td>
            </tr>
           
<%--            <tr>
                <td colspan="4" style="height: 17px">
                    <span style="font-size: 10pt; font-family: Courier New">
                    Documentos / imágenes que sustenten la rendición : </span>
                </td>
            </tr>
            <tr>
                <td  colspan="4" style="height: 41px">
                    Archivo 1 :&nbsp;
                    <asp:FileUpload ID="FileUpload1" runat="server" Width="432px" Font-Names="Courier New" Font-Size="9pt" />
                    <asp:TextBox ID="txtdescripcion1" runat="server" Width="608px"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="4" style="height: 33px">
                    Archivo 2 :&nbsp;
                    <asp:FileUpload ID="FileUpload2" runat="server" Width="432px" Font-Names="Courier New" Font-Size="9pt" />
                    <asp:TextBox ID="txtdescripcion2" runat="server" Width="608px"></asp:TextBox></td>
            </tr>
--%>            <tr>
                <td colspan="4" style="height: 29px">
                    <span style="font-family: Courier New; color: #FF0000; font-weight: bold;">
                    (LOS IMPORTES DEBEN ESTAR EN LA MISMA MONEDA DEL DOCUMENTO DE EGRESO)</span></td>
            </tr>
            <tr>
                <td colspan="4" style="height: 29px">
                    <asp:Label ID="lblmensaje" runat="server" Font-Bold="True" Font-Size="9pt" ForeColor="#804000" Width="736px"></asp:Label>
                    <asp:Button ID="cmdagregar" runat="server" BackColor="LemonChiffon" Font-Bold="False"
                        Font-Names="Courier New" Font-Size="9pt" Height="24px" Style="background-image: url(iconos/guardar.gif);
                        background-repeat: no-repeat" Text="  Guardar" Width="96px" Font-Italic="False" OnClientClick="procesar" TabIndex="15" />
                    <asp:Button ID="cmdcancelar" runat="server" BackColor="LemonChiffon" Font-Bold="False"
                        Font-Names="Courier New" Font-Size="9pt" Height="24px" Style="background-image: url(iconos/salir.gif);
                        background-repeat: no-repeat" Text="  Cancelar" Width="96px" Font-Italic="False" />
                    <asp:HiddenField ID="HiddenField1" runat="server" />
                </td>
            </tr>
        </table>
    
    </div>
    </form>
    <table id="tabmensaje" width="100%" height="500"  style="display:none">
            <tr height="100%" >
                <td bgcolor="lemonchiffon" style="width: 100%; height: 1000;" align="center"> Procesando, espere un momento por favor..
                    <br />
                        <img src ="cargando.gif" />             </td>
            </tr>
        </table>
    
</body>
</html>
