<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmVerSeparacionDetalle.aspx.vb" Inherits="academico_estudiante_FrmVerSeparacionDetalle" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Detalle de Separaciones</title>
    <link href="../../scripts/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="../../scripts/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="../../scripts/js/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script>
        function ShowMessage(message, messagetype) {
            var cssclass;
            switch (messagetype) {
                case 'Success':
                    cssclass = 'alert-success'
                    break;
                case 'Error':
                    cssclass = 'alert-danger'
                    break;
                case '1':
                    cssclass = 'alert-danger'
                    break;
                case 'Warning':
                    cssclass = 'alert-warning'
                    break;
                default:
                    cssclass = 'alert-info'
            }

            if (cssclass != 'alert-danger') {
                $('#alert_container').append('<div id="alert_div" style="margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999;" class="alert alert-success"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><span>' + message + '</span></div>');
            } else {
                $('#alert_container').append('<div id="alert_div" style="margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999;" class="alert alert-danger"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><span>' + message + '</span></div>');
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="container-fluid">
        <div class="messagealert" id="alert_container"></div>
        <br />
        <asp:GridView ID="gvLista" runat="server" Width="99%" CssClass="table table-bordered bs-table"
            AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="descripcion_cse" HeaderText="TIPO SEPARACION" />
                <asp:BoundField DataField="descripcion_tse" HeaderText="SEPARACION" />
                <asp:BoundField DataField="motivo_sep" HeaderText="MOTIVO" />
                <asp:BoundField DataField="fechaIni_sep" HeaderText="F. INICIO" />
                <asp:BoundField DataField="fechaFin_Sep" HeaderText="F. FINAL" />
                <asp:BoundField DataField="vigencia_sep" HeaderText="VIGENCIA" />
                <asp:BoundField DataField="nroResRevocada" HeaderText="REVOCADO" />
            </Columns>
            <EmptyDataTemplate>
            </EmptyDataTemplate>
            <HeaderStyle BackColor="#D9534F" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center"
                Font-Size="12px" />
            <RowStyle Font-Size="11px" />
            <EditRowStyle BackColor="#ffffcc" />
            <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
        </asp:GridView>
    </div>
    </form>
</body>
</html>
