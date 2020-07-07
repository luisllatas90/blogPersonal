<%@ Page Language="VB" AutoEventWireup="false" CodeFile="nuevapropuesta.aspx.vb" Inherits="nuevapropuesta" Debug="true" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<link href="../estilo.css"rel="stylesheet" type="text/css">
<script type="text/javascript" src="../funciones.js"> </script>

<script  type="text/javascript" >
function OcultarTabla()
    {
       if (document.form1.FileArchivo.value!="" && document.form1.TxtNombre.value!="" )
        {
            document.all.tblDatos.style.display="none"
            document.all.tblmensaje.style.display=""  
        }
    }
    
    function Cerrar(){
    if (confirm('¿Desea guardar la propuesta como borrador?')==true){; 
    
    }else{
    history.back();
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
    <title>Propuestas en Borrador</title>
    <link href="../estilo.css" rel="stylesheet" type="text/css" />
    <style type="text/css">

        .style11
        {
            width: 60%;
        }
        .style6
        {
            height: 15px;
        }
        .style1
        {
            width: 100%;
        }
        .style3
        {
            height: 17px;
        }
        .style4
        {
            width: 103px;
            height: 17px;
        }
        .style5
        {
            width: 30%;
            height: 17px;
        }
        .style2
        {
            width: 103px;
        }
        .style10
        {
            width: 18px;
        }
        .style8
        {
            height: 222px;
            width: 18px;
        }
        </style>
</head>

<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
    <div >
    
        <table class="contornotabla_azul" cellpadding="0" cellspacing="0" align="left">
            <tr>
                <td valign="top" bgcolor="#F0F0F0" class="style11">
    
        <table style="width:100%; background-color: #F0F0F0;" align="center" cellpadding=0 
                        cellspacing=0>
            <tr>
                <td class="bordeinf" valign="top">
                    <table style="width:100%;" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                    <asp:Button ID="cmdEnviar" runat="server" Text="   Enviar" 
                        CssClass="enviarpropuesta" Height="35px" Width="100px" />
                    <asp:Button ID="cmdGuardar" runat="server" Text="        Guardar" 
                        CssClass="guardar_prp" Height="35px" Width="100px" ValidationGroup="Guardar" />
                    <asp:Button ID="cmdPrioridad" runat="server" Text="        Prioridad" 
                        CssClass="prioridad" Height="35px" Width="100px" />
                    <asp:Button ID="cmdAdjuntar" runat="server" Text="     Adjuntar" 
                        CssClass="attach_" Height="35px" Width="100px" Visible="False" />
                            </td>
                            <td>
                                &nbsp;</td>
                                            <td align="right">
                    <asp:Button ID="cmdSalir" runat="server" Text="Salir" 
                        CssClass="salir_prp" Height="35px" Width="112px" />
                                            </td>
                                        </tr>
                                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width: 100%; height: 435px;">
                        <tr>
                            <td class="style6" colspan="6" style="font-weight: bold">
                                <asp:Label ID="lblIdPropuesta" runat="server" Visible="False"></asp:Label>
                                <asp:Label ID="lblIdUsuario" runat="server" Visible="False"></asp:Label>
                                <asp:Label ID="lblNuevoDap" runat="server" Visible="False"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style1" colspan="6" style="font-weight: bold">
                              DATOS GENERALES</td>
                        </tr>
                        <tr>
                            <td class="style1" style="font-weight: bold">
                                Propuesta:</td>
                            <td colspan="5" width="90%">
                                <asp:TextBox ID="txtPropuesta" runat="server" Width="440px" MaxLength="100"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                    ControlToValidate="txtPropuesta" 
                                    ErrorMessage="Ingrese un nombre para la propuesta" ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                <asp:DropDownList ID="ddlArea" runat="server" AutoPostBack="True" 
                                    Visible="False">
                                </asp:DropDownList>
                            &nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lblPrioridad" runat="server" Font-Bold="True" ForeColor="Red" 
                                    Text=" !Prioridad Alta" Visible="False"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style1" style="font-weight: bold">
                                Área:</td>
                            <td colspan="5">
                                <asp:DropDownList ID="ddlCentroCosto" runat="server" AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="style1" style="font-weight: bold">
                                Tipo Propuesta:</td>
                            <td colspan="5">
                                <asp:DropDownList ID="ddlTipoPropuesta" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="style1" style="font-weight: bold">
                                Facultad:</td>
                            <td colspan="5">
                                <asp:Label ID="lblFacultad" runat="server" Font-Bold="True" ForeColor="Blue"></asp:Label>
                            &nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lblFacultadID" runat="server" Font-Bold="True" ForeColor="Blue" 
                                    Visible="False"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style1" colspan="6" style="font-weight: bold">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style1" colspan="6" style="font-weight: bold">
                                PRESUPUESTO</td>
                        </tr>
                        <tr>
                            <td class="style1" style="font-weight: bold">
                                Moneda:&nbsp;</td>
                            <td colspan="5" valign="middle">
                                <asp:DropDownList ID="ddlMoneda" runat="server" AutoPostBack="True">
                                </asp:DropDownList>
&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="Label1" runat="server" Font-Bold="True" 
                                    Text="Tipo de Cambio:   "></asp:Label>
                                <asp:Label ID="lblTipoCambioSimbolo" runat="server"></asp:Label>
                                <asp:Label ID="lblTipoCambio" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style3">
                                Ingreso</td>
                            <td class="style3" width="10%">
                                <asp:TextBox ID="txtIngreso" runat="server" Width="70px" AutoPostBack="True">0</asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                    ControlToValidate="txtIngreso" ErrorMessage="Ingrese un Importe" 
                                    ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                </td>
                            <td class="style3" width="10%">
                                Egreso</td>
                            <td class="style3" width="10%">
                                <asp:TextBox ID="txtEgreso" runat="server" Width="70px" AutoPostBack="True">0</asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                    ControlToValidate="txtEgreso" ErrorMessage="Ingrese un Importe" 
                                    ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                </td>
                            <td class="style4" width="10%">
                                Utilidad</td>
                            <td class="style5" width="50%">
                                <asp:Label ID="lblUtilidad" runat="server" Font-Bold="True" ForeColor="Blue">0</asp:Label>
                                </td>
                        </tr>
                        <tr>
                            <td class="style1" style="font-weight: bold">
                                Ingreso S/.</td>
                            <td width="10%">
                                <asp:Label ID="lblIngresoMN" runat="server">0</asp:Label>
                            </td>
                            <td width="10%" style="font-weight: bold">
                                Egreso S/.</td>
                            <td width="10%">
                                <asp:Label ID="lblEgresoMN" runat="server">0</asp:Label>
                            </td>
                            <td class="style2" width="10%" style="font-weight: bold">
                                Utilidad S/.</td>
                            <td width="50%">
                                <asp:Label ID="lblUtilidadMN" runat="server" Font-Bold="True" ForeColor="Blue">0</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style1">
                                &nbsp;</td>
                            <td colspan="5">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style6" style="font-weight: bold" valign="top">
                                RESUMEN</td>
                            <td colspan="5" class="style6">
                                <asp:TextBox ID="txtResumen" runat="server" Height="60px" MaxLength="1000" 
                                    Rows="4" Width="430px" TextMode="MultiLine"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                                    ControlToValidate="txtResumen" ErrorMessage="Ingrese un Resumen" 
                                    ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="style6">
                                &nbsp;</td>
                            <td colspan="5" class="style6">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style6" style="font-weight: bold" valign="top">
                                IMPORTANCIA</td>
                            <td colspan="5" class="style6">
                                <asp:TextBox ID="txtImportancia" runat="server" Height="60px" MaxLength="500" 
                                    Rows="4" Width="430px" TextMode="MultiLine"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                                    ControlToValidate="txtImportancia" ErrorMessage="Ingrese la Importancia" 
                                    ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="style6">
                                &nbsp;</td>
                            <td colspan="5" class="style6">
                                &nbsp;</td>
                        </t&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style1">
                                &nbsp;</td>
                            <td colspan="5">
                                &nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" 
                        ShowMessageBox="True" ValidationGroup="Guardar" />
                </td>
            </tr>
        </table>
    
                </td>
                <td width="38%" align="center" bgcolor="#F0F0F0" valign="top" 
                        style="border-left-style: solid; border-left-width: 1px; border-left-color: #999999;">
                                    <table id="tblDatos" width="100%" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td colspan="3" valign="middle" style="vertical-align: middle" class="style7">
                                                &nbsp;<img src="../images/attach_2_small.gif" style="vertical-align: middle" />
                                                <asp:Label ID="Label2" runat="server" Text="Adjuntar Archivos" Font-Bold="True" 
                        Font-Names="Verdana" Font-Size="9pt" ForeColor="Black"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" align="center" class="style7">
                                                <hr style="height: 1px" />
                                                <asp:FileUpload ID="FileArchivo"  runat="server" BorderStyle="Solid" BorderWidth="1px"
                        Width="90%" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="FileArchivo"
                        ErrorMessage="Seleccione el archivo a subir" SetFocusOnError="True" 
                        ValidationGroup="agregar">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" 
                    style="font-weight: bold; font-size: 8pt; color: black; font-family: verdana" class="style7">
                                                &nbsp;Nombre&nbsp;
                                                <asp:TextBox ID="TxtNombre" runat="server" Width="60%"></asp:TextBox>
                                                &nbsp;<asp:RequiredFieldValidator
                        ID="RequiredFieldValidator2" runat="server" ControlToValidate="TxtNombre" ErrorMessage="Ingrese un nombre al archivo"
                        SetFocusOnError="True" ValidationGroup="agregar">*</asp:RequiredFieldValidator>
                                                <asp:Button ID="CmdAgregar" OnClientClick="OcultarTabla()" runat="server" 
                                    Text="Agregar" CssClass="attach_prp" Width="79px" ValidationGroup="agregar" />
                                                <hr style="height: 1px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td 
                    style="font-weight: bold; font-size: 8pt; color: black; font-family: verdana" class="style10">
                                                &nbsp;</td>
                                            <td colspan="2" 
                    style="font-weight: bold; font-size: 8pt; color: black; font-family: verdana">
                                                &nbsp;<img src="../images/attach_3.gif" /> Archivos Adjuntos</td>
                                        </tr>
                                        <tr>
                                            <td 
                    valign="top" class="style8">
                                                &nbsp;</td>
                                            <td style="font-weight: bold; font-size: 8pt; color: black; font-family: verdana; width: 80%; height: 222px; border-right: blue 1px solid; border-top: blue 1px solid; border-left: blue 1px solid; border-bottom: blue 1px solid; background-color: white; border-color: #3366FF;" 
                    valign="top">
                                                &nbsp;&nbsp;
                                                <asp:GridView ID="GridView1" runat="server" Width="100%" 
                        AutoGenerateColumns="False" ShowHeader="False" DataKeyNames="nombre_apr" GridLines="Horizontal">
                                                    <RowStyle Height="30px" BorderStyle="None" />
                                                    <Columns>
                                                        <asp:BoundField DataField="Codigo_apr" HeaderText="Codigo_apr" 
                                                            Visible="False" >
                                                            <HeaderStyle Width="0%" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="nombre_apr" HeaderText="Archivo" Visible="False" >
                                                            <HeaderStyle Width="0%" />
                                                            <ItemStyle Width="50%" />
                                                        </asp:BoundField>
                                                        <asp:ImageField DataImageUrlField="extension" DataImageUrlFormatString="../images/ext/{0}.gif"
                                HeaderText="imagen" ConvertEmptyStringToNull="False">
                                                            <HeaderStyle Width="10%" />
                                                            <ItemStyle HorizontalAlign="Center" Width="20px" />
                                                        </asp:ImageField>
                                                        <asp:BoundField DataField="descripcion_apr" HeaderText="descripcion" >
                                                            <HeaderStyle Width="70%" />
                                                        </asp:BoundField>
                                                        <asp:CommandField ShowDeleteButton="True">
                                                            <HeaderStyle Width="20%" />
                                                            <ItemStyle Width="30px" />
                                                        </asp:CommandField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                            <td colspan="1" rowspan="2" style="font-weight: bold; font-size: 8pt; color: black;
                    font-family: verdana" valign="top">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="font-weight: normal; font-size: 8pt; color: black;
                    font-family: verdana" valign="top" class="style10">
                                                &nbsp;</td>
                                            <td align="center" style="font-weight: normal; font-size: 8pt; width: 287px; color: black;
                    font-family: verdana" valign="top">
                                                &nbsp;<asp:Label ID="LblMensaje" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
            
            <asp:HiddenField  ID="txtelegido" runat="server" />
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" EnableViewState="False"
            ShowMessageBox="True" ValidationGroup="agregar" />
    <asp:HiddenField ID="txtTipo" runat="server" />
    <asp:HiddenField ID="txtEstado" runat="server" />
    <asp:HiddenField ID="txtMenu" runat="server" />
        <asp:HiddenField ID="txtnombrearchivo" runat="server" />
        
                            </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
