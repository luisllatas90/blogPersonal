<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmRegistroSumillasAsignatura_Exportar.aspx.vb"
    Inherits="GestionCurricular_frmRegistroSumillasAsignatura_Exportar" EnableEventValidation="false" %>

<%@ Register Assembly="BusyBoxDotNet" Namespace="BusyBoxDotNet" TagPrefix="busyboxdotnet" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta name="google" value="notranslate">
    <title>Registrar Sumillas por Asignatura</title>
    <!-- custom scrollbar stylesheet -->
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=7' />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <link href="../scripts/css/bootstrap.css" rel="Stylesheet" type="text/css" />
    <link href="../assets/fontawesome-5.2/css/regular.min.css" rel="stylesheet" type="text/css" />
    <link href="css/paginacion.css" rel="stylesheet" type="text/css" />

    <script src="../scripts/js/jquery-1.12.3.min.js" type="text/javascript"></script>

    <script src="js/popper.js" type="text/javascript"></script>

    <script src="../scripts/js/bootstrap.min.js" type="text/javascript"></script>

    <script src="../assets/fontawesome-5.2/js/all.min.js" type="text/javascript"></script>

    <script type="text/javascript">

        function openModal(accion) {
            $('#myModal').modal('show');
        }

        function closeModal() {
            $('#myModal').modal('hide');
        }

        function ShowMessage(message, messagetype) {
            var cssclss;
            switch (messagetype) {
                case 'Success':
                    cssclss = 'alert-success'
                    break;
                case 'Error':
                    cssclss = 'alert-danger'
                    break;
                case 'Warning':
                    cssclss = 'alert-warning'
                    break;
                default:
                    cssclss = 'alert-info'
            }
            $('#alert_container').append('<div id="alert_div" style="margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999;" class="alert ' + cssclss + '"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><span>' + message + '</span></div>');
        }

    </script>

</head>
<body>
    <form id="form1" runat="server" method="post" autocomplete="off">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <!-- Listado de Sumillas por Asignatura -->
    <div class="container-fluid">
        <div class="panel panel-default" id="pnlLista" runat="server">
            <div class="panel panel-heading">
                <div class="row">
                </div>
            </div>
            <div class="panel panel-body">
                <%--<asp:UpdatePanel ID="panGrid" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <contenttemplate>--%>
                <div class="col-md-12">
                    <div class="row">
                        <div class="col-md-12">
                            <asp:GridView ID="gvAsignatura" runat="server" Width="99%" AutoGenerateColumns="false"
                                ShowHeader="true"
                                DataKeyNames="codigo_sum, codigo_pes, codigo_Cur, descripcion_sum,competencia_sum,transversal,cod_aux"
                                CssClass="table table-bordered table-hover">
                                <Columns>
                                    <asp:BoundField DataField="cicloRom" HeaderText="Ciclo" />
                                    <asp:BoundField DataField="nombre_Cur" HeaderText="Asignatura" />
                                    <%--<asp:BoundField DataField="creditos_Cur" HeaderText="Créditos" />--%>
                                    <asp:BoundField DataField="descripcion_sum" HeaderText="Sumilla" HeaderStyle-Width="35%" />
                                    <asp:BoundField DataField="competencia_sum" HeaderText="Competencia" HeaderStyle-Width="35%" />
                                </Columns>
                                <EmptyDataTemplate>
                                    No se encontraron Datos!
                                </EmptyDataTemplate>
                                <HeaderStyle BackColor="#D9534F" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center"
                                    Font-Size="12px" />
                                <RowStyle Font-Size="11px" />
                                <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                <FooterStyle Font-Bold="True" ForeColor="White" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <%--</contenttemplate>
                </asp:UpdatePanel>--%>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
