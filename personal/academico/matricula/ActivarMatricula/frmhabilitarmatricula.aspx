<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmhabilitarmatricula.aspx.vb" Inherits="personal_academico_tesis_habilitar_frmhabilitarmatricula" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Habilitar Matrícula</title>
    <script type="text/javascript" language="javascript">
        /*
        if(top.location==self.location)
        {location.href='../../../../tiempofinalizado.asp'} //El ../ depende de la ruta de la página
        */
    </script>
    <link href="../../../../private/estilo.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <p class="usatTitulo">Cronograma de actividades por semestre académico</p>
      
    <p>
        Semestre académico:
        <asp:DropDownList ID="dpCodigo_cac" runat="server" AutoPostBack="True" 
            DataTextField="descripcion_cac" DataValueField="codigo_cac">
        </asp:DropDownList>
    </p>
    <asp:GridView ID="grwCronograma" runat="server" CellPadding="3" 
        AutoGenerateColumns="False">
        <Columns>
            <asp:BoundField DataField="codigo_cro" HeaderText="ID" />
            <asp:BoundField DataField="descripcion_act" HeaderText="Actividad" />
            <asp:BoundField DataField="fechaini_cro" HeaderText="Inicio" />
            <asp:BoundField DataField="fechafin_cro" HeaderText="Fin" />
            <asp:BoundField DataField="observacion_cro" HeaderText="Observaciones" />
        </Columns>
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    </asp:GridView>
    
    <p class="usatTitulo">Apertuar nueva matrícula</p>
    <p>
        <asp:Label ID="lblmensaje" runat="server" 
            Font-Bold="True" Font-Size="10pt" ForeColor="Red"></asp:Label>
    </p>
    
    Ingrese clave: <asp:TextBox ID="txtClave" runat="server" TextMode="Password"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RqClave" runat="server" 
        ControlToValidate="txtClave" Display="Dynamic" ErrorMessage="Clave correcta" 
        SetFocusOnError="True">Clave incorrecta</asp:RequiredFieldValidator>
&nbsp;<asp:Button ID="cmdActivar" runat="server" Text="Ingresar..." />
    
    &nbsp;<br />
    <br />
    <table style="width:100%; border-collapse:collapse" cellpadding="3" cellspacing="0" border="1">
        <tr class="etabla">
            <td>
                Descripción</td>
            <td>
                Acción</td>
        </tr>
        <tr>
            <td>
                Permite habilitar una nueva matrícula. El codigo_cac se ha ingresado 
                manualmente.<br />
                <asp:Label ID="lbltipo0" runat="server" Font-Bold="True" Font-Size="10pt" 
                    ForeColor="Red"></asp:Label>
            </td>
            <td>
                <asp:Button ID="cmdAperturar" runat="server" Enabled="False" 
                    Text="Aperturar nueva matrícula 2010-II" ValidationGroup="cancelar" />
            </td>
        </tr>
        <tr>
            <td>
                Actualizar estado de cursos programados<br />
                <asp:Label ID="lbltipo5" runat="server" Font-Bold="True" Font-Size="10pt" 
                    ForeColor="Red"></asp:Label>
            </td>
            <td>
                <asp:Button ID="cmdAperturar4" runat="server" Enabled="False" 
                    Text="Actualizar cursos programados" ValidationGroup="cancelar" />
            </td>
        </tr>
        <tr>
            <td>
                Cambiar estadodeuda_alu a 1, siempre y cuando el alumno tenga deudas pendientes 
                en ciclos anteriores. Estos estudiantes no se podrán matricular hasta que 
                cancelen.<br />
                <asp:Label ID="lbltipo1" runat="server" Font-Bold="True" Font-Size="10pt" 
                    ForeColor="Red"></asp:Label>
            </td>
            <td>
                <asp:Button ID="cmdAperturar0" runat="server" Enabled="False" 
                    Text="Actualizar Deudas pendientes" ValidationGroup="cancelar" />
            </td>
        </tr>
        <tr>
            <td>
                Actualiza los créditos acumulados hasta el momento para cada <b>estudiante</b>. 
                Esto es importante para que se muestren correctamente los cursos que debe llevar 
                (requisito creditaje)<br />
                <asp:Label ID="lbltipo2" runat="server" Font-Bold="True" Font-Size="10pt" 
                    ForeColor="Red"></asp:Label>
            </td>
            <td>
                <asp:Button ID="cmdAperturar1" runat="server" Enabled="False" 
                    Text="Actualizar créditos acumulados" ValidationGroup="cancelar" />
            </td>
        </tr>
        <tr>
            <td>
                Actualiza los créditos acumulados por ciclo de estudios de cada plan y escuela. 
                Esto permite mostrar correctamente los cursos complementarios según el ciclo que 
                le toca.<br />
                <asp:Label ID="lbltipo3" runat="server" Font-Bold="True" Font-Size="10pt" 
                    ForeColor="Red"></asp:Label>
            </td>
            <td>
                <asp:Button ID="cmdAperturar2" runat="server" Enabled="False" 
                    Text="Actualizar créditos del Plan" ValidationGroup="cancelar" />
            </td>
        </tr>
        <tr>
            <td>
                Corrige en la tabla <b>CargaAcademica siempre y cuando no se haya cambiando las 
                horas,</b> los cambios de horas de teoría y asesoría que se hayan realizado en 
                el plan de estudios.
                <br />
                <asp:Label ID="lbltipo4" runat="server" Font-Bold="True" Font-Size="10pt" 
                    ForeColor="Red"></asp:Label>
            </td>
            <td>
                <asp:Button ID="cmdAperturar3" runat="server" Enabled="False" 
                    Text="Actualizar Horas del Plan" ValidationGroup="cancelar" />
            </td>
        </tr>
    </table>
</form>
</body>
</html>
