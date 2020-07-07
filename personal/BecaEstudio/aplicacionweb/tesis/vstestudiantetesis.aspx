<%@ Page Language="VB" AutoEventWireup="false" CodeFile="vstestudiantetesis.aspx.vb" Inherits="personal_academico_tesis_vstestudiantetesis" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Estudiantes /Tesis</title>
    <link href="../../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="JavaScript" src="../../../private/funciones.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <p class="usatTitulo">Estudiantes que aún no han aprobado Seminario de Tesis</p>
    <table width="100%" cellpadding="3" cellspacing="0">
            <tr>
                <td width="20%" valign="top">
                    Carrera Profesional</td>
                <td width="80%" valign="top" colspan="2">
                <asp:DropDownList ID="dpEscuela" runat="server" AutoPostBack="True" 
            Font-Size="7pt">
        </asp:DropDownList>
                    <asp:DropDownList ID="dpPlanEstudio" runat="server" AutoPostBack="True" 
                        Visible="False" Font-Size="7pt">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td width="20%" valign="top">
                    <asp:Label ID="lblCurso" runat="server" Text="Asignatura"></asp:Label>
                </td>
                <td width="80%" valign="top" colspan="2">
                    <asp:DropDownList ID="dpCurso" runat="server">
                    </asp:DropDownList>
                &nbsp; 
                    <asp:Button ID="cmdBuscar" runat="server" CssClass="buscar2" 
                        Text="     Buscar" Width="60px" />
                    &nbsp;<asp:Button ID="cmdExportar" runat="server" CssClass="excel2" 
                    Text="  Exportar" Visible="False" />
                </td>
            </tr>
            <tr>
                <td width="20%" valign="top">
                    &nbsp;</td>
                <td width="80%" valign="top" colspan="2">
                    Los estudiantes han aprobado &gt;=
                    <asp:TextBox ID="txtCrdAprobados" runat="server" CssClass="cajas" MaxLength="3" 
                        Width="35px">120</asp:TextBox>
&nbsp;créditos totales.<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="txtCrdAprobados" 
                        ErrorMessage="Debe especificar un número máximo de créditos aprobados">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td width="20%">
                    &nbsp;</td>
                <td width="40%">
                    &nbsp;</td>
                <td align="right" width="40%">
                    <asp:Label ID="lblTotal" runat="server" Font-Bold="True" Font-Names="Tahoma" 
                        Font-Size="8pt" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            </table>
        <asp:GridView ID="grdEstudiantes" runat="server" AutoGenerateColumns="False" 
            CellPadding="4" ForeColor="#333333">
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <RowStyle BackColor="#EFF3FB" />
            <Columns>
                <asp:BoundField DataField="codigouniver_alu" HeaderText="Código">
                    <ItemStyle Width="15%" />
                </asp:BoundField>
                <asp:BoundField DataField="alumno" HeaderText="Apellidos y Nombres">
                    <ItemStyle Width="60%" />
                </asp:BoundField>
                <asp:BoundField DataField="cicloIng_alu" HeaderText="Ciclo de Ingreso">
                    <ItemStyle Width="10%" />
                </asp:BoundField>
                <asp:BoundField DataField="ccreditosaprobados_alu" HeaderText="Crd. Aprob.">
                    <ItemStyle Width="10%" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Estado actual">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" 
                            Text='<%# iif(eval("estadoactual_alu")=0,"Inactivo","Activo") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="10%" />
                </asp:TemplateField>
            </Columns>
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
            ShowMessageBox="True" />
    </form>
    <table id="tblmensaje" border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse;display:none;height:100%;width:100%;"  class="contornotabla">
	    <tr>
	    <td style="background-color: #FEFFE1" align="center" class="usatTitulo" >
	    Procesando<br />Por favor espere un momento...<br />
	    <img src="../../../images/cargando.gif" width="209" height="20" alt="Cargando...">
	    </td>
	    </tr>
    </table>
    </body>
</html>
