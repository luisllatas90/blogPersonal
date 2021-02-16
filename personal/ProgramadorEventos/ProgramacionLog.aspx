<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ProgramacionLog.aspx.vb" Inherits="ProgramacionLog" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="ie=edge">
    <title>.:: Log de comunicaciones ::.</title>

    <%-- Estilos externos --%>
    <link href="libs/bootstrap-4.1/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="libs/fontawesome-5.2/css/all.min.css" rel="stylesheet" type="text/css" />
    <%-- Estilos propios --%>
    <link href="css/programacionLog.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form runat="server" id="frmProgramacionLog">
        <asp:GridView ID="grwProgramacionLog" runat="server" AutoGenerateColumns="false" DataKeyNames="codigo_his"
            CssClass="table table-sm table-bordered table-hover" GridLines="None">
            <Columns>
                <asp:BoundField HeaderText="NRO" />
                <asp:BoundField DataField="fechaEnvio_his" HeaderText="FECHA DE ENVÍO" DataFormatString="{0:dd/MM/yyyy hh:mm:ss}" ItemStyle-HorizontalAlign="center" />
                <asp:BoundField DataField="destinatario_his" HeaderText="DESTINATARIO" ItemStyle-HorizontalAlign="center" />
                <asp:BoundField DataField="idRespuesta_his" HeaderText="ID RESPUESTA" ItemStyle-HorizontalAlign="center" />
                <asp:BoundField DataField="respuesta_his" HeaderText="RESPUESTA" />
                <asp:BoundField DataField="fechaRespuesta_his" HeaderText="FECHA DE RESPUESTA" DataFormatString="{0:dd/MM/yyyy hh:mm:ss}" ItemStyle-HorizontalAlign="center" />
                <asp:BoundField DataField="tipo_his" HeaderText="TIPO" ItemStyle-HorizontalAlign="center" />
                <asp:BoundField DataField="estado_his" HeaderText="ESTADO" ItemStyle-HorizontalAlign="center" />
                <asp:BoundField DataField="codigo_his" HeaderText="CÓDIGO" ItemStyle-HorizontalAlign="center" />
            </Columns>
            <EmptyDataTemplate>
                No se registró ningún registro
            </EmptyDataTemplate>
            <HeaderStyle BackColor="#E33439" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center"
                Font-Size="10px" />
            <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
        </asp:GridView>
    </form>
    <%-- Scripts externos --%>
    <script src="libs/jquery/jquery-3.3.1.js" type="text/javascript"></script>
    <script src="libs/popper-1.14.4/js/popper.js" type="text/javascript"></script>
    <script src="libs/bootstrap-4.1/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="libs/fontawesome-5.2/js/all.min.js" type="text/javascript"></script>
    <script src="libs/iframeresizer/iframeResizer.contentWindow.min.js" type="text/javascript"></script>
</body>
</html>