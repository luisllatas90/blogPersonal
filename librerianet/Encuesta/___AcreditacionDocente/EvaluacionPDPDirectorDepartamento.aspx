<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EvaluacionPDPDirectorDepartamento.aspx.vb" Inherits="Encuesta_AcreditacionDocente_EvaluacionPDPDirectorDepartamento" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" /> 
    <link href="../../private/estiloweb.css" rel="stylesheet" type="text/css" /> 
    <script src="../../private/funciones.js" type ="text/javascript" language ="javascript"></script>
   
    </head>
<body>
    <form id="form1" runat="server">
    <table style="width:100%;">
        <tr>
            <td align="justify" colspan="2">
                <b>&nbsp;PLAN DE DESARROLO PERSONAL EN EL PRESENTE AÑO - </b>Semestre 2009 - II</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td align="right">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                Departamento académico:
                <asp:DropDownList ID="cboDepartamento" runat="server" AutoPostBack="True">
                </asp:DropDownList>
            </td>
            <td align="right">
                <asp:Label ID="lblFecha" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <hr />
            </td>
        </tr>
        <tr>
            <td align="justify">
                &nbsp;</td>
            <td align="right">
                <asp:Button ID="cmdGuardarArriba" runat="server" Text="   Guardar" 
                    ValidationGroup="Guardar" CssClass="guardar" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:GridView ID="gvdocentes" runat="server" AutoGenerateColumns="False" 
                    DataKeyNames="codigo_per" HorizontalAlign="Center" Width="100%">
                    <Columns>
                        <asp:BoundField HeaderText="Nº" />
                        <asp:BoundField HeaderText="PDP &lt;br&gt;CV" HtmlEncode="False">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Docente" DataField="Personal" />
                        <asp:BoundField HeaderText="E-mail" DataField="login_per" />
                        <asp:BoundField HeaderText="Presentó PDP en &lt;br&gt;el plazo establecido" 
                            HtmlEncode="False" DataField="presento_epd" >
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Las propuestas del PDP &lt;br&gt;son coherentes con:" 
                            HtmlEncode="False" DataField="coherentecon_epd" >
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="El cumplimiento de &lt;br&gt;su PDP en el 2008 fue:" 
                            HtmlEncode="False" DataField="cumplimiento_epd" >
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                    </Columns>
                    <EmptyDataTemplate>
                        No hay docentes asignados al departamento académico seleccionado
                    </EmptyDataTemplate>
                    <HeaderStyle CssClass="TituloTabla" Height="20px" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="right">
                <asp:Button ID="cmdGuardarAbajo" runat="server" Text="   Guardar" 
                    ValidationGroup="Guardar" CssClass="guardar" />
            </td>
        </tr>
    </table>
    <asp:CustomValidator ID="CustomValidator1" runat="server" 
        ClientValidationFunction="VerificarDatosAGuardar" 
        ErrorMessage="Usted debe marcar el check de los docentes que ha respondido las preguntas">*</asp:CustomValidator>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
    </form>
</body>
</html>
