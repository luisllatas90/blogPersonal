<%@ Page Language="VB" AutoEventWireup="false" CodeFile="agregaevento.aspx.vb" Inherits="agregaexperiencia" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >

<head runat="server">

<script type ="text/javascript">
function duracion(source,arguments)
{
if (document.form1.LstEventos.value == '0' && otros.style.visibility=='visible')
    if (document.form1.TxtHoras.value=="")
        arguments.IsValid = false;
    else
        arguments.IsValid = true;
}

function abrir()
{
var id;
var tipo;
id = document.form1.LstEventos.value;
if (document.form1.RbAcademico.checked == true )
    tipo = 1;
else
    tipo = 2;
showModalDialog("detalleevento.aspx?id="+id + "&tipo=" + tipo,window,"dialogWidth:545px;dialogHeight:170px;status:no;help:no;center:yes;scroll:no")
}

function validacheck(source,arguments)
{
var i;
var fin;
var bandera;
bandera=0;
fin = parseInt(document.form1.HddLista.value)- 1;
for(i=0;i<=fin;i++) {
    if (eval("document.form1.ChkParticipa_" + i + ".checked")== true)
        bandera=1; }       
if (bandera==0)
    arguments.IsValid = false;
else
    arguments.IsValid = true; 
}

function validalista(source,arguments)
{
    if (document.form1.LstEventos.selectedIndex < 0)
        arguments.IsValid = false;
     else
        arguments.IsValid = true;
}

function validaevento(source,arguments)
{
if( document.form1.TxtOtro.value == "" && otros.style.visibility=='visible')
    arguments.IsValid = false;
 else
    arguments.IsValid = true;
}

function validaorganiza(source,arguments)
{
if (document.form1.TxtOrganizado.value == "" && otros.style.visibility=='visible')
    arguments.IsValid = false;
else
    arguments.IsValid = true;
}

function validafecha1(source, arguments)
{
    if (otros.style.visibility=='visible')
        if (document.form1.DDLIniDia.value=='0' || document.form1.DDLIniMes.value=='0' || document.form1.DDLIniAño.value=='0')
            arguments.IsValid = false;
        else
            arguments.IsValid = true;
 }      
 
 function validafecha2(source, arguments)
 {
    if (otros.style.visibility=='visible')
        if (document.form1.DDLFinDIa.value=='0' || document.form1.DDLFinMes.value=='0' || document.form1.DDLFinAño.value=='0')
            arguments.IsValid = false;
        else
            arguments.IsValid = true;
 }
        

function vernuevo()
    {
    if (document.form1.LstEventos.selectedIndex>0)
       otros.style.visibility = 'hidden';
    else
       otros.style.visibility = 'visible';
    }
function validarnumero()
{
	if (event.keyCode < 45 || event.keyCode > 57)
		{event.returnValue = false}
}

</script>
<link rel="STYLESHEET" href="private/estilo.css"/>
<link rel="stylesheet" type="text/css" href="../../../private/estilo.css" />
    <title>Hoja de Vida :: Agregar Eventos</title>
      
</head>
<body onload="javascript:otros.style.visibility='hidden'">
<div id="divmensaje"></div>
<script type="text/javascript" src="div.js"></script>
    <form id="form1" runat="server" style=" visibility:visible  ">
    <div>
        <table cellpadding="0" cellspacing="0" style="border-right: darkgray 1px solid; border-top: darkgray 1px solid;
            border-left: darkgray 1px solid; width: 466px; border-bottom: darkgray 1px solid; height: 403px;">
            <tr>
                <td colspan="3" rowspan="3" style="width: 481px">
                    <table style="width: 451px">
                        <tr>
                            <td align="center" colspan="3" style="font-weight: bold; font-size: 11pt; color: white;
                                font-family: Verdana; height: 20px; background-color: #c2a877">
                                Registro de Asistencia a Eventos</td>
                        </tr>
                        <tr>
                            <td colspan="3" style="height: 397px">
                                <table id="tabla" style="width: 450px">
                                    <tr>
                                        <td style="font-size: 10pt; color: olive; font-family: Verdana; height: 18px;" colspan="2">
                                            Buscar Eventos por: &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            &nbsp;<asp:RadioButton ID="RbAcademico" runat="server" Checked="True" GroupName="tipoeven"
                                                Text="Academico" AutoPostBack="True" />
                                            <asp:RadioButton ID="RbSocial" runat="server" GroupName="tipoeven"
                                                    Text="Social" Width="60px" AutoPostBack="True" />
                                            de tipo:
                                            <asp:DropDownList ID="DDLTIpo" runat="server" Font-Names="Arial" Font-Size="X-Small"
                                                ForeColor="Navy" Width="94px">
                                            </asp:DropDownList>
                                            <asp:Image
                                                    ID="Img" runat="server" ImageUrl="~/images/menus/prioridad_.gif" Style="cursor: hand" />
                                            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="DDLTIpo"
                                                ErrorMessage="Seleccione tipo de evento" Operator="NotEqual" ValidationGroup="guardar"
                                                ValueToCompare="0" Width="1px">*</asp:CompareValidator>
                                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="DDLTIpo"
                                                ErrorMessage="Seleccione tipo de evento" Operator="NotEqual" ValidationGroup="buscar"
                                                ValueToCompare="0" Width="1px">*</asp:CompareValidator>&nbsp;<asp:DropDownList ID="DDLClaseEven" runat="server" Font-Names="Arial"
                                                Font-Size="X-Small" ForeColor="Navy" Width="106px">
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: 10pt; color: olive; font-family: Verdana" colspan="2">
                                           
                                            <asp:TextBox ID="TxtBuscar" runat="server" BorderColor="Black" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Names="Arial" Font-Size="X-Small" ForeColor="Navy" Width="437px"></asp:TextBox><asp:RequiredFieldValidator
                                                    ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtBuscar" ErrorMessage="Ingrese Texto de busqueda"
                                                    ValidationGroup="buscar">*</asp:RequiredFieldValidator>
                                            </td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: 10pt; color: olive; font-family: Verdana" valign="top" colspan="2" align="right">
                                            &nbsp;<asp:Button ID="CmdBuscar" runat="server" CssClass="buscar" Text="Buscar"
                                    Width="78px" ValidationGroup="buscar" /></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" valign="top">
                                            <hr />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="font-size: 10pt; color: olive; font-family: Verdana" valign="top">
                                            &nbsp;Eventos Registrados:
                                            <asp:CustomValidator ID="CustomValidator5" runat="server" ClientValidationFunction="validalista"
                                                ErrorMessage="Seleccione un elemento de la lista" ValidationGroup="guardar">*</asp:CustomValidator></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="font-size: 10pt; color: olive; font-family: Verdana" valign="top">
                                            <asp:ListBox ID="LstEventos" runat="server" Font-Names="Arial" Font-Size="X-Small"
                                                ForeColor="Navy" Height="90px" Width="446px" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid"></asp:ListBox></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            Ingrese el (los) tipo(s) de participacion que tuvo en el evento.<asp:Button ID="Button1" runat="server" CssClass="buscar" Text="     Detalle Ev."
                                    Width="82px" OnClientClick="javascript: abrir(); return false;" /></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            
                                            <asp:CheckBoxList ID="ChkParticipa" runat="server" BorderStyle="None" BorderWidth="1px" ForeColor="Navy" RepeatColumns="3" Width="451px">
                                            </asp:CheckBoxList>
                                            <asp:CustomValidator ID="CustomValidator6" runat="server" ErrorMessage="Debe marcar por lo menos 1 tipo de participacion"
                                                ValidationGroup="guardar" ClientValidationFunction="validacheck">*</asp:CustomValidator></td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: 10pt; width: 14px; color: olive; font-family: Verdana" valign="top">
                                        </td>
                                        <td valign="top">
            <table id="otros" style="width: 430px; height: 102px;" cellpadding="0" cellspacing="0">
                <tr>
                    <td colspan="2" style="height: 21px">
                        Ingresar datos si el evento no se encuentra en lista.</td>
                </tr>
                <tr>
                    <td style="width: 109px; height: 21px;">
                        Nombre</td>
                    <td>
                                            <asp:TextBox ID="TxtOtro" runat="server" BorderColor="Black" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Names="Arial" Font-Size="X-Small" ForeColor="Navy" Width="302px"></asp:TextBox>
                        <asp:CustomValidator ID="CustomValidator3" runat="server" ClientValidationFunction="validaevento"
                            ErrorMessage="Ingrese Nombre de Evento" ValidationGroup="guardar">*</asp:CustomValidator></td>
                </tr>
                <tr>
                    <td style="width: 109px">
                        Organizado por</td>
                    <td>
                                            <asp:TextBox ID="TxtOrganizado" runat="server" BorderColor="Black" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Names="Arial" Font-Size="X-Small" ForeColor="Navy" Width="302px"></asp:TextBox>
                        <asp:CustomValidator ID="CustomValidator4" runat="server" ClientValidationFunction="validaorganiza"
                            ErrorMessage="Nombre de organizacion requerido" ValidationGroup="guardar">*</asp:CustomValidator></td>
                </tr>
                <tr>
                    <td style="width: 109px">
                        Fecha Inicio</td>
                    <td>
                        <asp:DropDownList ID="DDLIniDia" runat="server" Font-Names="Arial" Font-Size="X-Small" ForeColor="Navy">
                            <asp:ListItem Value="0">Dia</asp:ListItem>
                            <asp:ListItem>1</asp:ListItem>
                            <asp:ListItem>2</asp:ListItem>
                            <asp:ListItem>3</asp:ListItem>
                            <asp:ListItem>4</asp:ListItem>
                            <asp:ListItem>5</asp:ListItem>
                            <asp:ListItem>6</asp:ListItem>
                            <asp:ListItem>7</asp:ListItem>
                            <asp:ListItem>8</asp:ListItem>
                            <asp:ListItem>9</asp:ListItem>
                            <asp:ListItem>10</asp:ListItem>
                            <asp:ListItem>11</asp:ListItem>
                            <asp:ListItem>12</asp:ListItem>
                            <asp:ListItem>13</asp:ListItem>
                            <asp:ListItem>14</asp:ListItem>
                            <asp:ListItem>15</asp:ListItem>
                            <asp:ListItem>16</asp:ListItem>
                            <asp:ListItem>17</asp:ListItem>
                            <asp:ListItem>18</asp:ListItem>
                            <asp:ListItem>19</asp:ListItem>
                            <asp:ListItem>20</asp:ListItem>
                            <asp:ListItem>21</asp:ListItem>
                            <asp:ListItem>22</asp:ListItem>
                            <asp:ListItem>23</asp:ListItem>
                            <asp:ListItem>24</asp:ListItem>
                            <asp:ListItem>25</asp:ListItem>
                            <asp:ListItem>26</asp:ListItem>
                            <asp:ListItem>27</asp:ListItem>
                            <asp:ListItem>28</asp:ListItem>
                            <asp:ListItem>29</asp:ListItem>
                            <asp:ListItem>30</asp:ListItem>
                        </asp:DropDownList>&nbsp;<asp:DropDownList ID="DDLIniMes" runat="server" Font-Names="Arial" Font-Size="X-Small" ForeColor="Navy" Width="72px">
                            <asp:ListItem Value="0">Mes</asp:ListItem>
                            <asp:ListItem Value="1">Enero</asp:ListItem>
                            <asp:ListItem Value="2">Febrero</asp:ListItem>
                            <asp:ListItem Value="3">Marzo</asp:ListItem>
                            <asp:ListItem Value="4">Abril</asp:ListItem>
                            <asp:ListItem Value="5">Mayo</asp:ListItem>
                            <asp:ListItem Value="6">Junio</asp:ListItem>
                            <asp:ListItem Value="7">Julio</asp:ListItem>
                            <asp:ListItem Value="8">Agosto</asp:ListItem>
                            <asp:ListItem Value="9">Setiembre</asp:ListItem>
                            <asp:ListItem Value="10">Octubre</asp:ListItem>
                            <asp:ListItem Value="11">Noviembre</asp:ListItem>
                            <asp:ListItem Value="12">Diciembre</asp:ListItem>
                                            </asp:DropDownList>&nbsp;<asp:DropDownList ID="DDLIniAño" runat="server" Font-Names="Arial" Font-Size="X-Small" ForeColor="Navy">
                                            </asp:DropDownList>
                        <asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="validafecha1"
                            ErrorMessage="Fecha de Inicio Incorrecta" ValidationGroup="guardar">*</asp:CustomValidator></td>
                </tr>
                <tr>
                    <td style="width: 109px; height: 6px;">
                        Fecha Termino</td>
                    <td style="height: 6px"><asp:DropDownList ID="DDLFinDIa" runat="server" Font-Names="Arial" Font-Size="X-Small" ForeColor="Navy">
                        <asp:ListItem Value="0">Dia</asp:ListItem>
                        <asp:ListItem>1</asp:ListItem>
                        <asp:ListItem>2</asp:ListItem>
                        <asp:ListItem>3</asp:ListItem>
                        <asp:ListItem>4</asp:ListItem>
                        <asp:ListItem>5</asp:ListItem>
                        <asp:ListItem>6</asp:ListItem>
                        <asp:ListItem>7</asp:ListItem>
                        <asp:ListItem>8</asp:ListItem>
                        <asp:ListItem>9</asp:ListItem>
                        <asp:ListItem>10</asp:ListItem>
                        <asp:ListItem>11</asp:ListItem>
                        <asp:ListItem>12</asp:ListItem>
                        <asp:ListItem>13</asp:ListItem>
                        <asp:ListItem>14</asp:ListItem>
                        <asp:ListItem>15</asp:ListItem>
                        <asp:ListItem>16</asp:ListItem>
                        <asp:ListItem>17</asp:ListItem>
                        <asp:ListItem>18</asp:ListItem>
                        <asp:ListItem>19</asp:ListItem>
                        <asp:ListItem>20</asp:ListItem>
                        <asp:ListItem>21</asp:ListItem>
                        <asp:ListItem>22</asp:ListItem>
                        <asp:ListItem>23</asp:ListItem>
                        <asp:ListItem>24</asp:ListItem>
                        <asp:ListItem>25</asp:ListItem>
                        <asp:ListItem>26</asp:ListItem>
                        <asp:ListItem>27</asp:ListItem>
                        <asp:ListItem>28</asp:ListItem>
                        <asp:ListItem>29</asp:ListItem>
                        <asp:ListItem>30</asp:ListItem>
                    </asp:DropDownList>&nbsp;<asp:DropDownList ID="DDLFinMes" runat="server" Font-Names="Arial" Font-Size="X-Small" ForeColor="Navy" Width="73px">
                        <asp:ListItem Value="0">Mes</asp:ListItem>
                        <asp:ListItem Value="1">Enero</asp:ListItem>
                        <asp:ListItem Value="2">Febrero</asp:ListItem>
                        <asp:ListItem Value="3">Marzo</asp:ListItem>
                        <asp:ListItem Value="4">Abril</asp:ListItem>
                        <asp:ListItem Value="5">Mayo</asp:ListItem>
                        <asp:ListItem Value="6">Junio</asp:ListItem>
                        <asp:ListItem Value="7">Julio</asp:ListItem>
                        <asp:ListItem Value="8">Agosto</asp:ListItem>
                        <asp:ListItem Value="9">Setiembre</asp:ListItem>
                        <asp:ListItem Value="10">Octubre</asp:ListItem>
                        <asp:ListItem Value="11">Noviembre</asp:ListItem>
                        <asp:ListItem Value="12">Diciembre</asp:ListItem>
                    </asp:DropDownList>&nbsp;<asp:DropDownList ID="DDLFinAño" runat="server" Font-Names="Arial" Font-Size="X-Small" ForeColor="Navy">
                        </asp:DropDownList>
                        <asp:CustomValidator ID="CustomValidator2" runat="server" ClientValidationFunction="validafecha2"
                            ErrorMessage="Fecha de Termino Incorrecta" ValidationGroup="guardar">*</asp:CustomValidator></td>
                </tr>
                <tr>
                    <td style="width: 109px; height: 6px">
                        Duración</td>
                    <td style="height: 6px">
                        <asp:TextBox ID="TxtHoras" runat="server" BorderColor="Black" BorderStyle="Solid"
                            BorderWidth="1px" Font-Names="Arial" Font-Size="X-Small" ForeColor="Navy" Style="text-align: right"
                            Width="36px"></asp:TextBox>&nbsp;<asp:DropDownList ID="DDLDuracion" runat="server"
                                Font-Size="X-Small" ForeColor="Navy" Width="54px">
                                <asp:ListItem Value="1">Hora(s)</asp:ListItem>
                                <asp:ListItem Value="2">Dia(s)</asp:ListItem>
                                <asp:ListItem Value="3">Mes(es)</asp:ListItem>
                                <asp:ListItem Value="4">A&#241;o(s)</asp:ListItem>
                            </asp:DropDownList>&nbsp;
                        <asp:CustomValidator ID="CustomValidator7" runat="server" ClientValidationFunction="duracion"
                            ErrorMessage="Ingrese Duracion" ValidationGroup="guardar">*</asp:CustomValidator></td>
                </tr>
            </table>
                                        </td>
                                    </tr>
                                </table>
                                &nbsp;
                                <asp:Label ID="LblError" runat="server" ForeColor="#C00000"></asp:Label></td>
                        </tr>
                        <tr>
                            <td align="right" colspan="3" style="height: 21px" valign="top">
                                &nbsp;<asp:Button ID="CmdGuardar" runat="server" CssClass="guardar" Text="Guardar"
                                    Width="85px" ValidationGroup="guardar" />
                                <asp:Button ID="CmdCancelar" runat="server" CssClass="salir" OnClientClick="javascript:window.close();return false;"
                                    Text="Cancelar" Width="86px" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
            </tr>
            <tr>
            </tr>
        </table>
        &nbsp;
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
            ShowMessageBox="True" ShowSummary="False" ValidationGroup="buscar" />
        <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
            ShowMessageBox="True" ShowSummary="False" ValidationGroup="guardar" />
        <asp:HiddenField ID="HddLista" runat="server" />
    
    </div>
    </form>
</body>
</html>
