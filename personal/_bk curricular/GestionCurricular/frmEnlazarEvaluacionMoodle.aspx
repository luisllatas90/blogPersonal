<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmEnlazarEvaluacionMoodle.aspx.vb"
    Inherits="GestionCurricular_frmEnlazarEvaluacionMoodle" EnableEventValidation="false" %>

<%@ Register Assembly="BusyBoxDotNet" Namespace="BusyBoxDotNet" TagPrefix="busyboxdotnet" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta name="google" value="notranslate">
    <title>Enlazar evaluación con el Aula Virtual</title>
    <!-- custom scrollbar stylesheet -->
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=7' />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <link href="../scripts/css/bootstrap.css" rel="Stylesheet" type="text/css" />
    <link href="../assets/fontawesome-5.2/css/regular.min.css" rel="stylesheet" type="text/css" />

    <script src="../scripts/js/jquery-1.12.3.min.js" type="text/javascript"></script>

    <script src="js/popper.js" type="text/javascript"></script>

    <script src="../scripts/js/bootstrap.min.js" type="text/javascript"></script>

    <script src="../assets/fontawesome-5.2/js/all.min.js" type="text/javascript"></script>

    <script type="text/javascript">
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
    <busyboxdotnet:BusyBox ID="BusyBox1" runat="server" ShowBusyBox="OnLeavingPage" Image="Clock"
        Text="Su solicitud está siendo procesada..." Title="Por favor espere" />
    <div class="container-fluid">
        <div class="messagealert" id="alert_container">
        </div>
        <br />
        <div class="panel panel-default" id="pnlLista" runat="server">
            <div class="panel panel-heading">
                <div class="row">
                    <div class="col-md-1">
                        <asp:LinkButton ID="btnBack" runat="server" CssClass="btn btn-default">
                            <span><i class="fa fa-arrow-left"></i></span> Volver
                        </asp:LinkButton>
                    </div>
                    <div class="col-md-11">
                        <h4>
                            <label id="lblCurso" runat="server">
                                Asignatura:</label></h4>
                    </div>
                </div>
            </div>
            <div class="panel panel-body">
                <div class="row">
                    <div class="col-md-12">
                        <div class="table-responsive">
                            <asp:GridView ID="gvEvaluacion" runat="server" AutoGenerateColumns="false" ShowHeader="true"
                                DataKeyNames="codigo_eva, codigo_emd, codigo_mod" OnRowDataBound="gvEvaluacion_OnRowDataBound"
                                CssClass="table table-bordered table-hover">
                                <Columns>
                                    <asp:BoundField DataField="descripcion_uni" HeaderText="Unidades" />
                                    <asp:BoundField DataField="descripcion_res" HeaderText="Resultados" />
                                    <asp:BoundField DataField="descripcion_ind" HeaderText="Indicadores" />
                                    <asp:BoundField DataField="descripcion_evi" HeaderText="Evidencias" />
                                    <asp:BoundField DataField="descripcion_ins" HeaderText="Intrumentos" />
                                    <asp:BoundField DataField="descripcion_eva" HeaderText="Evaluaciones" />
                                    <asp:BoundField DataField="fecha_gru" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" />
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Modo de Calificación">
                                        <ItemTemplate>
                                            <asp:RadioButtonList ID="rdModoCalifica" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rdModoCalifica_SelectedIndexChanged">
                                                <asp:ListItem Value="0" Selected="True"> Directo</asp:ListItem>
                                                <asp:ListItem Value="1"> Aula Virtual</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Aula Virtual" HeaderStyle-Width="20%">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="cboMoodle" runat="server" CssClass="form-control input-sm"
                                                AutoPostBack="true" OnSelectedIndexChanged="cboMoodle_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    No se encontraron Datos
                                </EmptyDataTemplate>
                                <HeaderStyle BackColor="#D9534F" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center"
                                    Font-Size="12px" Font-Bold="true" />
                                <RowStyle Font-Size="11px" />
                                <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                <FooterStyle Font-Bold="True" ForeColor="White" />
                                <%--<PagerStyle ForeColor="#003399" HorizontalAlign="Center" />--%>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
