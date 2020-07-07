<%@ Page Language="VB" AutoEventWireup="false" CodeFile="adjuntar.aspx.vb" Inherits="propuestas_adjuntar" Debug="true" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<link href="../../private/estilo.css" rel="stylesheet" type="text/css">
<script src="../../private/funciones.js"> </script>

<script>
function OcultarTabla()
    {
       if (document.form1.FileArchivo.value!="" && document.form1.TxtNombre.value!="" )
        {
            document.all.tblDatos.style.display="none"
            document.all.tblmensaje.style.display=""  
        }
    }
</script>

<script type="text/javascript" language="javascript" >

function ValidarEnvio()	{	
var Cadena=document.form1.FileArchivo.value
var SubCadena=Cadena
var n=0
	if (document.form1.FileArchivo.value!="")	{
		for (var i=0; i<15; i++){			 
			 n = SubCadena.indexOf("\\"); 
			// alert(n)
			 if(n==-1){
				break;
			}
			SubCadena=SubCadena.substr(n+1,SubCadena.length-7) 
		}
	}	
			document.form1.TxtNombre.value=SubCadena	
}

function ponervalortext(nombre,numero)
	{
	SeleccionarFila1()
	form1.txtelegido.value=numero
	form1.txtnombrearchivo.value=nombre
	}

function SeleccionarFila1()
    {
	oRow = window.event.srcElement.parentElement;
	if (oRow.tagName == "TR")
	    {
		AnteriorFila.Typ = "Sel";
		AnteriorFila.className = AnteriorFila.Typ + "Off";
		AnteriorFila = oRow;
	    }
	oRow.Typ ="Selected";
	oRow.className = oRow.Typ;
    }		

</script>
    <title>Adjuntar Archivos</title>
</head>
<body style="margin:0px,0px,0px,0px; background-color:#F0F0F0">
    <form id="form1" runat="server">
    <div>
        <table style="width: 391px" id="tblDatos">
            <tr>
                <td colspan="4" valign="middle" style="vertical-align: middle">
                    &nbsp;<img src="images/attach_2_small.gif" style="vertical-align: middle" />
                    <asp:Label ID="Label1" runat="server" Text="Adjuntar Archivos" Font-Bold="True" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="4">
                    <hr style="height: 1px" />
                    <asp:FileUpload ID="FileArchivo"  runat="server" BorderStyle="Solid" BorderWidth="1px"
                        Width="371px" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="FileArchivo"
                        ErrorMessage="Seleccione el archivo a subir" SetFocusOnError="True" ValidationGroup="agregar">*</asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td colspan="4" style="font-weight: bold; font-size: 8pt; color: black; font-family: verdana">
                    &nbsp;Nombre&nbsp;
                    <asp:TextBox ID="TxtNombre" runat="server" Width="309px"></asp:TextBox>&nbsp;<asp:RequiredFieldValidator
                        ID="RequiredFieldValidator2" runat="server" ControlToValidate="TxtNombre" ErrorMessage="Ingrese un nombre al archivo"
                        SetFocusOnError="True" ValidationGroup="agregar">*</asp:RequiredFieldValidator>
                    <hr style="height: 1px" />
                </td>
            </tr>
            <tr>
                <td colspan="4" style="font-weight: bold; font-size: 8pt; color: black; font-family: verdana">
                    &nbsp;<img src="images/attach_3.gif" />
                    Archivos Adjuntos</td>
            </tr>
            <tr>
                <td style="font-weight: bold; font-size: 8pt; color: black; font-family: verdana; width: 287px; height: 222px; border-right: blue 1px solid; border-top: blue 1px solid; border-left: blue 1px solid; border-bottom: blue 1px solid; background-color: white;" valign="top">
                    <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False" ShowHeader="False" GridLines="None">
                        <RowStyle Height="30px" />
                        <Columns>
                            <asp:BoundField DataField="Codigo_apr" HeaderText="Codigo_apr" Visible="False" />
                            <asp:BoundField DataField="nombre_apr" HeaderText="Archivo" Visible="False" />
                            <asp:ImageField DataImageUrlField="nombre_apr" DataImageUrlFormatString="images/img{0}.jpg"
                                HeaderText="imagen">
                                <ItemStyle HorizontalAlign="Center" Width="20px" />
                            </asp:ImageField>
                            <asp:BoundField DataField="descripcion_apr" HeaderText="descripcion" />
                        </Columns>
                    </asp:GridView>
                    &nbsp;&nbsp;
                </td>
                <td colspan="1" rowspan="2" style="font-weight: bold; font-size: 8pt; color: black;
                    font-family: verdana" valign="top">
                    <table>
                        <tr>
                            <td style="width: 100px">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100px">
                                &nbsp;<asp:Button ID="CmdAgregar" OnClientClick="OcultarTabla()" runat="server" Text="Agregar" CssClass="attach_prp" Width="79px" ValidationGroup="agregar" /></td>
                        </tr>
                        <tr>
                            <td style="width: 100px">&nbsp;
                                </td>
                        </tr>
                        <tr>
                            <td style="width: 100px">
                                &nbsp;<asp:Button ID="CmdQuitar" runat="server" Text="Quitar" CssClass="remove_prp" Width="80px" /></td>
                        </tr>
                        <tr>
                            <td style="width: 100px">&nbsp;
                                </td>
                        </tr>
                        <tr>
                            <td style="width: 100px">&nbsp;
                                </td>
                        </tr>
                        <tr>
                            <td style="width: 100px">&nbsp;
                                </td>
                        </tr>
                        <tr>
                            <td style="width: 100px">&nbsp;
                                </td>
                        </tr>
                        <tr>
                            <td style="width: 100px">&nbsp;
                                </td>
                        </tr>
                        <tr>
                            <td style="width: 100px">&nbsp;
                                </td>
                        </tr>
                        <tr>
                            <td style="width: 100px">&nbsp;
                                </td>
                        </tr>
                        <tr>
                            <td style="width: 100px">
                                &nbsp;<asp:Button ID="CmdSalir" runat="server" CssClass="cerrar_prp" Text="Salir"
                                    Width="74px" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" style="font-weight: normal; font-size: 8pt; width: 287px; color: black;
                    font-family: verdana" valign="top">
                    &nbsp;<asp:Label ID="LblMensaje" runat="server"></asp:Label></td>
            </tr>
        </table>
    
    </div>
            
            <asp:HiddenField  ID="txtelegido" runat="server" />
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" EnableViewState="False"
            ShowMessageBox="True" ValidationGroup="agregar" />
    <asp:HiddenField ID="txtTipo" runat="server" />
    <asp:HiddenField ID="txtEstado" runat="server" />
    <asp:HiddenField ID="txtMenu" runat="server" />
        <asp:HiddenField ID="txtnombrearchivo" runat="server" />
        
    </form>
    <table id="tblmensaje" border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse;display:none" bordercolor="#111111" width="100%" height="100%" class="contornotabla">
	    <tr>
	    <td width="100%" align="center" class="usatEtiqOblig" bgcolor="#FEFFFF">
	    Procesando<br>
	    Por favor espere un momento...<br>
	    <img border="0" src="../../images/cargando.gif" width="209" height="20">
	    </td>
	    </tr>
    </table>
</body>
</html>
