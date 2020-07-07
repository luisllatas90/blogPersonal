<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmpermisoacciones.aspx.vb" Inherits="frmpermisoacciones" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Administrar cursos y departamentos académicos</title>
    
    <script type="text/javascript" src="../../private/funciones.js"></script>
    <script type="text/javascript" language="JavaScript" src="../../private/PopCalendar.js"></script>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css">
</head>
<body>
    <form id="form1" runat="server" visible="True">
      <%Response.Write(ClsFunciones.CargaCalendario)%> 
    <p class="usattitulopagina">
        <asp:Label ID="lblTitulo" runat="server" CssClass="usatTituloPagina" 
            Text="Permisos para acciones en un Tabla desde aplicación"></asp:Label>
      </p>
    <table width="100%" class="contornotabla">
        <tr>
            <td style="width:20%" >
                <asp:Label ID="lblmodulo" runat="server" Text="Módulo del sistema" 
                    Visible="False"></asp:Label>
            </td>
            <td style="width:60%">
                <asp:DropDownList ID="dpModulo" runat="server" AutoPostBack="True" 
                    Visible="False">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width:20%">Usuarios con acceso</td>
            <td style="width:60%">
                <asp:DropDownList ID="dpPersonal" runat="server" AutoPostBack="True">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width:20%">Menú de opciones</td>
            <td style="width:60%">
                <asp:DropDownList ID="dpMenu" runat="server" AutoPostBack="True">
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <br>
        <asp:Panel ID="Panel1" runat="server" Visible="False">
          <table border="0px" class="contornotabla" cellspacing="0" cellpadding="3" width="100%">
              <tr>
                  <td colspan="4" class="rojo"><b><u>Configuración del Menú seleccionado</u></b></td>
              </tr>
              <tr>
                  <td>
                      Fecha Inicio</td>
                  <td>
                      <asp:TextBox ID="txtFechaInicio" runat="server" BackColor="#CCCCCC" 
                          Font-Size="10px" ForeColor="Navy" MaxLength="12" style="text-align: right" 
                          Width="80px"></asp:TextBox>
                      <asp:Button ID="cmdInicio" runat="server" CausesValidation="False" 
                          onclientclick="PopCalendar.selectWeekendHoliday(1,1);PopCalendar.show(document.form1.txtFechaInicio,'dd/mm/yyyy');return(false)" 
                          Text="..." UseSubmitBehavior="False" />
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                          ControlToValidate="txtFechaInicio" 
                          ErrorMessage="Debe especificar la fecha de inicio">*</asp:RequiredFieldValidator>
                  </td>
                  <td>
                      Fecha Fin</td>
                  <td>
                      <asp:TextBox ID="txtFechaFin" runat="server" BackColor="#CCCCCC" 
                          Font-Size="10px" ForeColor="Navy" MaxLength="12" style="text-align: right" 
                          Width="80px"></asp:TextBox>
                      <asp:Button ID="cmdFin" runat="server" CausesValidation="False" 
                          onclientclick="PopCalendar.selectWeekendHoliday(1,1);PopCalendar.show(document.form1.txtFechaFin,'dd/mm/yyyy');return(false)" 
                          Text="..." UseSubmitBehavior="False" />
                      &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                          ControlToValidate="txtFechaInicio" 
                          ErrorMessage="Debe especificar la fecha de término">*</asp:RequiredFieldValidator>
                      <asp:CompareValidator ID="CompareValidator1" runat="server" 
                          ControlToCompare="txtFechaFin" ControlToValidate="txtFechaInicio" 
                          ErrorMessage="Fecha de Termino menor o igual a fecha de inicio." 
                          Operator="LessThan" Type="Date">*</asp:CompareValidator>
                  </td>
              </tr>
              <tr>
                  <td>
                      Acciones<br />
                      para el menú</td>
                  <td>
                      <asp:CheckBoxList ID="chkAcciones" runat="server">
                          <asp:ListItem Value="consult_acr">Consultar</asp:ListItem>
                          <asp:ListItem Value="agregar_acr">Agregar</asp:ListItem>
                          <asp:ListItem Value="modificar_acr">Modificar</asp:ListItem>
                          <asp:ListItem Value="eliminar_acr">Eliminar</asp:ListItem>
                      </asp:CheckBoxList>
                  </td>
                  <td>
                      &nbsp;</td>
                  <td>
                      &nbsp;</td>
              </tr>
              <tr>
                  <td align="center" colspan="4">
                      <asp:Button ID="cmdGuardar" runat="server" Text="Guardar" />
                  </td>
              </tr>
              </table>
              
            <asp:ValidationSummary ID="ValidationSummary1" runat="server"
            ShowMessageBox="True" ShowSummary="False" />
        </asp:Panel>
    </form>
</body>
</html>
