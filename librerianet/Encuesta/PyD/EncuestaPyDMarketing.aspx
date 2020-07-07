<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EncuestaPyDMarketing.aspx.vb" Inherits="Encuesta_PyD_EncuestaPyDMarketing" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" /> 
    <script language="javascript" type="text/javascript" src="../../../private/funciones.js"></script>
    <script type="text/javascript" language="javascript">
       
    function ValidaOtraVirtud(source, arguments)
    {
      if (eval("document.form1.rblVirtudes_3.checked")== true)
         if (document.form1.txtOtros.value.length == 0)
             arguments.IsValid = false; 
         else
             arguments.IsValid = true; 
      else
         arguments.IsValid = true; 
    }
    function ValidaMedioPublicitario(source, arguments)
    {
      var i;
      var fin;
      var bandera;
      bandera=0;
      fin = 6;
      for (i=0;i<=fin;i++) {
         if (eval("document.form1.cblMedio_" + i + ".checked")== true)
               bandera=1;   
      }
      if (bandera==0)
         arguments.IsValid = false;
      else
         arguments.IsValid = true;
    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table style="width:100%;" cellspacing="0">
            <tr>
                <td align="center" bgcolor="#0066CC" height="20" style="color: #FFFFFF">
                        <b>INSTRUMENTO DE RECOLECCIÓN DE DATOS – CUESTIONARIO
                        </b>
                </td>
            </tr>
            <tr>
                <td bgcolor="#ECF5FF">
                        &nbsp;</td>
            </tr>
            <tr>
                <td bgcolor="#ECF5FF">
                    Participantes: Estudiantes del I CICLO de la Facultad de Ciencias Empresariales. 
                    Objetivo: Determinar la influencia delos medios publicitarios utilizados por la 
                    USAT en la captación de estudiantes. Instrucciones: La información proporcionada 
                    será anónima. Les agradeceríamos respondan a las preguntas con veracidad. Marcar 
                    con una X las respuestas que correspondan a cada pregunta. </td>
            </tr>
            <tr>
                <td bgcolor="#ECF5FF">
                    </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    1. Sexo:<asp:RequiredFieldValidator ID="RequiredFieldValidator8" 
                        runat="server" ControlToValidate="rblSexo" ErrorMessage="Seleccione sexo" 
                        ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                    <asp:RadioButtonList ID="rblSexo" runat="server" 
                        RepeatDirection="Horizontal">
                        <asp:ListItem Value="F">Femenino</asp:ListItem>
                        <asp:ListItem Value="M">Masculino</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    2. Edad:<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="txtEdad" ErrorMessage="Indique su edad" 
                        ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                    <asp:RangeValidator ID="RangeValidator1" runat="server" 
                        ControlToValidate="txtEdad" ErrorMessage="Ingrese un valor válido en la edad" 
                        MaximumValue="99" MinimumValue="15" 
                        ToolTip="Ingrese un valor valido en la edad" ValidationGroup="Guardar">*</asp:RangeValidator>
&nbsp;<asp:TextBox ID="txtEdad" runat="server" Width="56px" MaxLength="2"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    3. Escuela Profesional<asp:RequiredFieldValidator 
                        ID="RequiredFieldValidator7" runat="server" ControlToValidate="rblEscuela" 
                        ErrorMessage="Seleccione escuela profesional" ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
&nbsp;<asp:RadioButtonList ID="rblEscuela" runat="server" RepeatColumns="2" 
                        RepeatDirection="Horizontal" CellPadding="1" CellSpacing="1" Width="635px" 
                        Enabled="False" EnableTheming="True">
                        <asp:ListItem Value="1">Administración de empresas</asp:ListItem>
                        <asp:ListItem Value="34">Administración Hotelera y de Servicios</asp:ListItem>
                        <asp:ListItem Value="4">Contabilidad</asp:ListItem>
                        <asp:ListItem Value="21">Economía</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    4. ¿Porqué modalidad ingresó a la USAT?<asp:RequiredFieldValidator 
                        ID="RequiredFieldValidator6" runat="server" ControlToValidate="rblModalidad" 
                        ErrorMessage="Seleccione modalidad de ingreso" ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                    <asp:RadioButtonList 
                        ID="rblModalidad" runat="server" CellPadding="1" CellSpacing="1" 
                        RepeatColumns="2" RepeatDirection="Horizontal" Width="475px">
                        <asp:ListItem Value="4">Escuela Pre USAT</asp:ListItem>
                        <asp:ListItem Value="8">Test Dahc</asp:ListItem>
                        <asp:ListItem Value="1">Examen de Admisión</asp:ListItem>
                        <asp:ListItem Value="30">Ingreso Directo</asp:ListItem>
                        <asp:ListItem Value="31">Otra modalidad</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    5. ¿Porqué medio publicitario se enteró de la Universidad?<asp:CustomValidator 
                        ID="CustomValidator1" runat="server" 
                        ClientValidationFunction="ValidaMedioPublicitario" 
                        ErrorMessage="Seleccione medio publicitario" ValidationGroup="Guardar">*</asp:CustomValidator>
                    <asp:CheckBoxList 
                        ID="cblMedio" runat="server" CellPadding="1" CellSpacing="1" 
                        RepeatColumns="2" RepeatDirection="Horizontal" Width="428px">
                        <asp:ListItem>Internet</asp:ListItem>
                        <asp:ListItem>Periódicos</asp:ListItem>
                        <asp:ListItem>Volantes propagandísticos</asp:ListItem>
                        <asp:ListItem>Televisión</asp:ListItem>
                        <asp:ListItem>Radio</asp:ListItem>
                        <asp:ListItem>Gigantografías</asp:ListItem>
                        <asp:ListItem>Otros</asp:ListItem>
                    </asp:CheckBoxList>
                </td>
            </tr>
            <tr>
                <td>
                    6. ¿Cómo considera el mensaje de los avisos publicitarios que realiza la USAT?<asp:RequiredFieldValidator 
                        ID="RequiredFieldValidator5" runat="server" ControlToValidate="rblAvisos" 
                        ErrorMessage="Seleccione como considera los avisos publicitarios" 
                        ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                    <asp:RadioButtonList 
                        ID="rblAvisos" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="1">Claro y preciso</asp:ListItem>
                        <asp:ListItem Value="2">Poco entendible</asp:ListItem>
                        <asp:ListItem Value="3">No entendible</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    7. ¿Crees que la Universidad debería expandir su publicidad a otros lugares?<asp:RequiredFieldValidator 
                        ID="RequiredFieldValidator4" runat="server" ControlToValidate="rblPublicidad" 
                        ErrorMessage="Seleccione donde debe expandir la publicidad" 
                        ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                    <asp:RadioButtonList 
                        ID="rblPublicidad" runat="server" RepeatDirection="Horizontal" 
                        Width="265px">
                        <asp:ListItem Value="1">Si</asp:ListItem>
                        <asp:ListItem Value="2">No</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    8. ¿A que lugares?
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ControlToValidate="txtLugares" 
                        ErrorMessage="Indique el lugar donde se debe expandir la publicidad" 
                        ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                    <asp:TextBox ID="txtLugares" runat="server" Width="80%" MaxLength="200"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    9. ¿Las virtudes académicas de la USAT corresponden a lo ofrecido por su 
                    publicidad?<asp:RequiredFieldValidator ID="RequiredFieldValidator3" 
                        runat="server" ControlToValidate="rblVirtudes" 
                        ErrorMessage="Seleccione las virtudes académicas" ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                    <asp:RadioButtonList ID="rblVirtudes" runat="server" 
                        RepeatColumns="1">
                        <asp:ListItem Value="1">Infraestructura</asp:ListItem>
                        <asp:ListItem Value="2">Docentes</asp:ListItem>
                        <asp:ListItem Value="3">Limpieza</asp:ListItem>
                        <asp:ListItem Value="4">Otros</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:CustomValidator ID="CustomValidator2" runat="server" 
                        ClientValidationFunction="ValidaOtraVirtud" 
                        ErrorMessage="Indique otra virtud académica" ValidationGroup="Guardar">*</asp:CustomValidator>
                    <asp:TextBox ID="txtOtros" runat="server" MaxLength="100"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="cmdGuardar" runat="server" Text="Guardar" 
                        ValidationGroup="Guardar" Width="70px" />
                    &nbsp;<asp:Button ID="cmdCerrar" runat="server" onclientclick="window.close()" 
                        Text="Cerrar" Width="70px" />
                </td>
            </tr>
            <tr>
                <td bgcolor="#0066CC">
                    &nbsp;</td>
            </tr>
        </table>
    
    </div>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                        ShowMessageBox="True" ShowSummary="False" ValidationGroup="Guardar" />
    </form>
</body>
</html>
