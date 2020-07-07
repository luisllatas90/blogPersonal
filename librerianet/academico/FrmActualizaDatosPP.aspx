<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmActualizaDatosPP.aspx.vb" Inherits="academico_FrmActualizaDatosPP" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script src="../../aprise/apprise-1.5.full.js" type="text/javascript"></script>
    <link href="../../aprise/apprise.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.4.4.min.js"></script>
    <!----------- Para DropDowList con CheckBoxs !--------------------->
	<link href="../../personal/css/MyStyles.css" rel="stylesheet" type="text/css" />
	<!----------------------------------------------------------------->
    <style type="text/css">
        .style2
        {
            color: #000000;
            font-size: smaller;
        }
        .style4
        {
            width: 162px;
        }
        .style5
        {
            width: 123px;
        }
        .style6
        {
            width: 123px;
            height: 27px;
        }
        .style7
        {
            height: 27px;
        }
        </style>        
        <script src="../../private/calendario.js"></script>
        
        <script type="text/javascript">
            function AbrirVentanaObservaciones() {                
                var codigouniv = $("#txtCodUniversitario_Buscar").val();                
                window.open("frmBitacoraObservaciones.aspx?codigouniv=" + codigouniv, "", "toolbar=yes, location=no, directories=no, status=no, menubar=no, resizable=yes, width=800, height=300, top=50");
            }

            /********************* Para DropDowList con CheckBoxs ******************************/
            var timoutID;

            function ShowMList() {
                var divRef = document.getElementById("divCheckBoxList");
                divRef.style.display = "block";
                var divRefC = document.getElementById("divCheckBoxListClose");
                divRefC.style.display = "block";
            }

            function ShowMList2() {
                var divRef2 = document.getElementById("divCheckBoxList2");
                divRef2.style.display = "block";
                var divRefC2 = document.getElementById("divCheckBoxListClose2");
                divRefC2.style.display = "block";
            }

            function HideMList() {
                document.getElementById("divCheckBoxList").style.display = "none";
                document.getElementById("divCheckBoxListClose").style.display = "none";
            }

            function HideMList2() {
                document.getElementById("divCheckBoxList2").style.display = "none";
                document.getElementById("divCheckBoxListClose2").style.display = "none";
            }

            function FindSelectedItems(sender, textBoxID) {
                var cblstTable = document.getElementById(sender.id);
                var checkBoxPrefix = sender.id + "_";
                var noOfOptions = cblstTable.rows.length;
                var selectedText = "";
                for (i = 0; i < noOfOptions; ++i) {
                    if (document.getElementById(checkBoxPrefix + i).checked) {
                        if (selectedText == "")
                            selectedText = document.getElementById(checkBoxPrefix + i).parentNode.innerText;
                        else
                            selectedText = selectedText + "," + document.getElementById(checkBoxPrefix + i).parentNode.innerText;
                    }
                }
                document.getElementById(textBoxID.id).value = selectedText;
            }

            function MarcarItems(sender, textBoxID, item) {
                var cblstTable = document.getElementById(sender);
                var checkBoxPrefix = sender + "_";
                var noOfOptions = cblstTable.rows.length;
                var selectedText = document.getElementById(textBoxID).value;

                //Recorrer lista
                //Marcar el item que coincida
                //Armar el texto
                //Asignar el texto al txt

                for (i = 0; i < noOfOptions; ++i) {
                    if (document.getElementById(checkBoxPrefix + i).parentNode.innerText == item) {
                        document.getElementById(checkBoxPrefix + i).checked = true;

                        if (selectedText == "")
                            selectedText = document.getElementById(checkBoxPrefix + i).parentNode.innerText;
                        else
                            selectedText = selectedText + "," + document.getElementById(checkBoxPrefix + i).parentNode.innerText;

                    }
                }
                document.getElementById(textBoxID).value = selectedText;

            }

            function LimpiarItems(sender, textBoxID) {
                var cblstTable = document.getElementById(sender);
                var checkBoxPrefix = sender + "_";
                var noOfOptions = cblstTable.rows.length;

                for (i = 0; i < noOfOptions; ++i) {
                    document.getElementById(checkBoxPrefix + i).checked = false;
                    document.getElementById(textBoxID).value = "";
                }
            }
            /************************************************************************************/
        </script>        
        
</head>
<body>
    <form id="form1" runat="server">
    <p class="usatTitulo"> ACTUALIZACION DE DATOS: <span class="style2">Datos de Alumno</span></p>
    <table style="width:83%" bgcolor="White" cellpadding="3" cellspacing="0">
        <tr>
            <td colspan="3" style="width: 100%" class="usatCeldaTitulo">Estudiante</td>
        </tr>
        <tr>
            <td class="style5">Codigo Universitario:</td>
            <td class="style4">
                <asp:TextBox ID="txtCodUniversitario_Buscar" runat="server" Width="232px"></asp:TextBox>
            </td>
            <td>
    <asp:Button ID="cmdBuscar" runat="server" CssClass="buscar2" Text="     Buscar" 
                    Width="108px" Height="21px" />
            </td>
        </tr>               
        <tr>
            <td class="style5"></td>
            <td colspan="2"></td>
        </tr>
        <tr>            
            <td colspan="3" style="width: 100%" class="usatCeldaTitulo">
                Datos personales</td>
        </tr>
        <tr>
            <td class="style6">Apellido Paterno:</td>
            <td colspan="2" class="style7">
                <asp:TextBox ID="txtApellidoPat" runat="server" Width="230px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style6">Apellido Materno:</td>
            <td colspan="2">
                <asp:TextBox ID="txtApellidoMat" runat="server" Width="230px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style6">Nombres:</td>
            <td colspan="2">
                <asp:TextBox ID="txtNombres" runat="server" Width="230px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style6">Sexo</td>
            <td colspan="2">
                <asp:DropDownList ID="cboSexo" runat="server" Height="19px" 
                    Width="168px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="style6">Fecha Nacimiento</td>
            <td colspan="2">
                <asp:TextBox ID="txtFecha" runat="server" Width="168px"></asp:TextBox>
                <input class="cunia" onclick="MostrarCalendario('txtFecha')" type="button" />
                &nbsp;&nbsp;&nbsp;
                </td>
        </tr>
        <tr>
            <td class="style6">Tipo Documento</td>
            <td colspan="2">
                <asp:DropDownList ID="cboTipoDocumento" runat="server" Width="168px" Height="19px">
                </asp:DropDownList>
&nbsp;&nbsp;&nbsp; Nro. Documento  
                <asp:TextBox ID="txtNroDocumento" runat="server" 
                    Width="127px" MaxLength="8"></asp:TextBox>                
            </td>
        </tr>
        <tr>
            <td class="style6">Ciclo de Ingreso</td>
            <td colspan="2">
                <asp:DropDownList ID="cboCicloIngreso" runat="server" Height="19px" 
                    Width="168px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="style6">Email</td>
            <td colspan="2">
                <asp:TextBox ID="txtEmail" runat="server" Width="250px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style6">Modalidad de Ingreso</td>
            <td colspan="2">
                <asp:DropDownList ID="cboModalidadIng" runat="server" Height="19px" 
                    Width="250px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="style6">Condicion </td>
            <td colspan="2">
                <asp:DropDownList ID="cboCondicion" runat="server" Height="19px" Width="250px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="style6">Carrera Profesional</td>
            <td colspan="2">
                <asp:DropDownList ID="cboCarreraProf" runat="server" Height="19px" 
                    Width="250px" AutoPostBack="True">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="style6">Plan de Estudio</td>
            <td colspan="2">
                <asp:DropDownList ID="cboPlanEstudio" runat="server" Height="19px" 
                    Width="250px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="style6">Colegio Aplicación</td>
            <td colspan="2">
                <asp:CheckBox ID="chkAplicacion" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="style6">Categoría</td>
            <td colspan="2">
                <!----------------------------- Para DropDowList con CheckBoxs !--------------------------------->
                            <div id="divCustomCheckBoxList2" runat="server" onmouseover="clearTimeout(timoutID);" onmouseout="timoutID = setTimeout('HideMList2()', 750);"  style="float:left; width:300px">
                                <table>
                                    <tr>
                                        <td align="right" class="DropDownLook2">
                                            <input id="txtSelectedMLValues2" type="text" readonly="readonly" onclick="ShowMList2()" style="width:229px;" runat="server" />
                                        </td>
                                        <td align="left" class="DropDownLook2">
                                            <img id="imgShowHide2" runat="server" src="../../personal/Iconos/drop.gif" onclick="ShowMList2()" align="left" />                
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" class="DropDownLook2">
                                            <div>
            	                                <div runat="server" id="divCheckBoxListClose2" class="DivClose2">			                        
		                                            <label runat="server" onclick="HideMList2();" class="LabelClose2" id="lblClose2" style="font-size:18px; font-weight:bold; color:red"> x</label>
		                                        </div>
                                                <div runat="server" id="divCheckBoxList2" class="DivCheckBoxList2">
		                                            <asp:CheckBoxList ID="lstMultipleValues2" runat="server" Width="250px" CssClass="CheckBoxList2"></asp:CheckBoxList>						        			           			        
		                                        </div>
		                                    </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <!------------------------------------------------------------------------->   
            </td>
        </tr>
        <tr>
            <td class="style6">Beneficio</td>
            <td colspan="2">                
                            <div id="divCustomCheckBoxList" runat="server" onmouseover="clearTimeout(timoutID);" onmouseout="timoutID = setTimeout('HideMList()', 750);" style="float:left">
                                <table>
                                    <tr>
                                        <td align="right" class="DropDownLook">
                                            <input id="txtSelectedMLValues" type="text" readonly="readonly" onclick="ShowMList()" style="width:229px;" runat="server" />
                                        </td>
                                        <td align="left" class="DropDownLook">
                                            <img id="imgShowHide" runat="server" src="../../personal/Iconos/drop.gif" onclick="ShowMList()" align="left" />                
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" class="DropDownLook">
                                            <div>
            	                                <div runat="server" id="divCheckBoxListClose" class="DivClose">			                        
		                                            <label runat="server" onclick="HideMList();" class="LabelClose" id="lblClose" style="font-size:18px; font-weight:bold; color:red"> x</label>
		                                        </div>
                                                <div runat="server" id="divCheckBoxList" class="DivCheckBoxList">
		                                            <asp:CheckBoxList ID="lstMultipleValues" runat="server" Width="250px" CssClass="CheckBoxList"></asp:CheckBoxList>						        			           			        
		                                        </div>
		                                    </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
            </td>
        </tr>
        <tr>
            <td class="style6">Observación</td>
            <td>
                <asp:TextBox ID="txtObs" runat="server" TextMode="MultiLine" Width="250px"></asp:TextBox>
                
            </td>
            <td>
                <input id="btnBitacoraObservaciones" type="button" value="Ver histórico de observaciones" onclick="AbrirVentanaObservaciones();"
                 runat="server" visible=false />
            </td>
        </tr>
    </table>
     <p align="center" style="height: 55px; width: 729px"><asp:Button ID="cmdGuardar" 
             runat="server" CssClass="guardar_prp" 
        Text="          Guardar" Height="55px" />
&nbsp;<asp:Button ID="cmdCancelar" runat="server" CssClass="noconforme1" 
        Text="       Cancelar" ValidationGroup="cancelar" 
            UseSubmitBehavior="False" 
             Height="55px" />
        &nbsp;<asp:HiddenField ID="hfCodAlu" runat="server" />
    </p>
    <asp:HiddenField ID="HdEstadoActual" runat="server" />
    </form>
</body>
</html>
