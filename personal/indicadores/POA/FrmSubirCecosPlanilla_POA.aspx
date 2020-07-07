<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmSubirCecosPlanilla_POA.aspx.vb"
    Inherits="indicadores_POA_PROTOTIPOS_FrmSubirCecosPlanilla_POA" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7, IE=EmulateIE9, IE=EDGE" />
    <link href="css/estilo_poa.css" rel="stylesheet" type="text/css" media="screen" />
</head>
<body>
    <%--<form id="form1">
    <div class="titulo_poa">
        <label id="lblTitulo">
            Generar Programas de Planilla</label>
    </div>
    <div class="contorno_poa">
        <table width="100%">
            <tr>
                <td>
                    Ejercicio Presupuestal
                </td>
                <td>
                    <select id="cboEjercicio" name="cboEjercicio" style="width: 205px; height: 18px;">
                    </select>
                </td>
            </tr>
            <tr>
                <td>
                    Cargar Excel
                </td>
                <td>
                    <input type="file" id="ArchivoUpload" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                </td>
            </tr>
            <tr>
                <td align="right" colspan="2">
                    <input type="button" class="btnGuardar" id="btnSubir" name="btnSubir" value="Subir"
                        style="width: 75px" onclick="my_function()" />
                    <input type="button" class="btnCancelar" id="btnCancelar" name="btnCancelar" value="Cancelar"
                        style="width: 90px" />
                </td>
            </tr>
        </table>
        <table id='my_file_output'>
        </table>
    </div>
    </form>--%>
    <form id="form1" runat="server">
    <div class="titulo_poa">
        <label id="lblTitulo">
            Generar Programas de Planilla</label>
    </div>
    <div class="contorno_poa">
        <table width="100%">
            <tr>
                <td colspan="2">
                    (*) Descargar Plantilla para Cargar de Centros de Costos de Programas de Planilla
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnPlantilla" runat="server" Text="    Descargar Plantilla" OnClick="btnPlantilla_Click" CssClass="btnNuevo" />
                </td>
            </tr>
            <tr>
                <td>
                    Ejercicio Presupuestal
                </td>
                <td>
                    <select id="cboEjercicio" name="cboEjercicio" style="width: 205px; height: 18px;">
                    </select>
                </td>
            </tr>
            <tr>
                <td>
                    Cargar Archivo (.xls)
                </td>
                <td>
                    <asp:FileUpload ID="FileUpload1" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                </td>
            </tr>
            <tr>
                <td align="right" colspan="2">
                    <asp:Button ID="btnUpload" runat="server" Text="    Cargar" OnClick="btnUpload_Click" CssClass="btnNuevo" />
                    <input type="button" class="btnCancelar" id="btnCancelar" name="btnCancelar" value="Cancelar"
                        style="width: 90px" />
                </td>
            </tr>
        </table>
        <br />
        <asp:GridView ID="GridView1" runat="server" Width="100%">
        </asp:GridView>
    </div>
    </form>
</body>
</html>
