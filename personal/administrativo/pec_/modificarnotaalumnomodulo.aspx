<%@ Page Language="VB" AutoEventWireup="false" CodeFile="modificarnotaalumnomodulo.aspx.vb" Inherits="modificarnotaalumnomodulo" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Modificar Nota del Participante</title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <p class="usatTitulo">Modificar Notas del Participante</p>

<table cellpadding="4" cellspacing="0" width="100%" class="contornotabla">
	<tr>
		<td >Código</td>
		<td style="width: 75%" >:
                                                <asp:Label ID="lblCodigo" runat="server" Font-Bold="True" 
                                                    ForeColor="#006600"></asp:Label>
                                            </td>
	</tr>
	<tr>
		<td >Participante</td>
		<td style="width: 75%" >:
                                                <asp:Label ID="lblParticipante" 
                runat="server" Font-Bold="True" 
                                                    ForeColor="#006600"></asp:Label>
                                            </td>
	</tr>
	<tr>
		<td >Nota Anterior</td>
		<td style="width: 75%" >:
                                                <asp:Label ID="lblNotaAnterior" 
                runat="server" Font-Bold="True" 
                                                    ForeColor="#006600"></asp:Label>
                                            &nbsp; /
                                                <asp:Label ID="lblCondicionAnterior" 
                runat="server" Font-Bold="True" 
                                                    ForeColor="#006600"></asp:Label>
                                            </td>
	</tr>
	<tr>
		<td >Nota Nuevo</td>
		<td style="width: 75%" >:
            <asp:TextBox ID="txtNota" runat="server" MaxLength="2" Width="27px"></asp:TextBox>
            <asp:RangeValidator ID="RangeValidator1" runat="server" 
                                                                    ControlToValidate="txtNota" 
                                                                    ErrorMessage="Debe ingresar notas en 0 y 20 para Aprobar o Desaprobar; ó -1 para notas pendientes" 
                                                                    MaximumValue="20" 
                MinimumValue="-1" Type="Double">*</asp:RangeValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                                                    ControlToValidate="txtNota" 
                                                                    
                                                                    
                ErrorMessage="Debe ingresar un valor en la nota. Si no desea evaluarlo aún, asigne: -1">*</asp:RequiredFieldValidator>
        </td>
	</tr>
	<tr>
		<td >Motivo de Cambio de Nota</td>
		<td style="width: 75%" >:
		    <asp:TextBox ID="txtMotivo" runat="server" Width="90%"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                                                    ControlToValidate="txtMotivo" 
                                                                    
                                                                    
                ErrorMessage="Debe ingresar el motivo de cambio de nota">*</asp:RequiredFieldValidator>
        </td>
	</tr>
	<tr>
		<td >
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                ShowMessageBox="True" ShowSummary="False" />
        </td>
		<td style="width: 75%" align="right" >
                                                <asp:Button ID="cmdGuardar" runat="server" CssClass="guardar2" Text="Guardar" />
                                                &nbsp;<input id="cmdCerrar" class="eliminar2" title="Cerrar" type="button" 
                                                    value="Cerrar" onclick="top.window.close()" /></td>
	</tr>
	<tr>
		<td >&nbsp;</td>
		<td style="width: 75%" >&nbsp;</td>
	</tr>
	
		
</table>
    </form>

</body>
</html>
