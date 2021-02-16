<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmEnlazarEvaluacionMoodle.aspx.vb"
    Inherits="GestionCurricular_frmEnlazarEvaluacionMoodle" EnableEventValidation="false" %>

<%@ Register Assembly="BusyBoxDotNet" Namespace="BusyBoxDotNet" TagPrefix="busyboxdotnet" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta name="google" value="notranslate" />
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

        $(document).ready(function() {
            $('#btnGrabar').click(function() {
                var cboMo = document.getElementById('<%=cboMoodle.ClientID%>');
                if (cboMo.selectedIndex < 1) {
                    alert('¡ Seleccione Tarea de Aula Virtual !');
                    cboMo.focus();
                    return false;
                }
            });
        });

        function openModal(accion) {
            $('#myModal').modal('show');
            document.getElementById('cboMoodle').selectedIndex = "0";
            document.getElementById('cboMoodle').focus();
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
                        <div class="row">
                            <div class="col-md-6" style="border-right: 1px solid #d6d2d2;">
                                <label>
                                    Alineación de Instrumentos de Evaluación con Tareas del Aula Virtual</label>
                            </div>
                            <div class="col-md-6">
                                <i class="fa fa-user-tie" style="color: Black"></i>
                                <label id="lblDocente" runat="server">
                                    [Nombre docente]</label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6" style="border-right: 1px solid #d6d2d2;">
                                <label id="lblCarrera" runat="server">
                                    Carrera:</label>
                            </div>
                            <div class="col-md-6">
                                <label id="lblCurso" runat="server">
                                    Asignatura:</label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel panel-body">
                <div class="row">
                    <div class="col-md-12">
                        <div class="table-responsive">
                            <asp:GridView ID="gvEvaluacion" runat="server" AutoGenerateColumns="false" ShowHeader="true"
                                DataKeyNames="codigo_eva, codigo_emd, codigo_mod, codigo_res, codigo_ind" OnRowDataBound="gvEvaluacion_OnRowDataBound"
                                CssClass="table table-bordered table-hover">
                                <Columns>
                                    <asp:BoundField DataField="descripcion_uni" HeaderText="Unidades" />
                                    <asp:BoundField DataField="descripcion_res" HeaderText="Resultados" />
                                    <asp:BoundField DataField="descripcion_ind" HeaderText="Indicadores" />
                                    <asp:BoundField DataField="descripcion_evi" HeaderText="Evidencias" />
                                    <asp:BoundField DataField="descripcion_ins" HeaderText="Intrumentos" />
                                    <%--<asp:BoundField DataField="descripcion_eva" HeaderText="Evaluaciones" />--%>
                                    <asp:BoundField HtmlEncode="false" DataField="fecha_gru" HeaderText="Fecha" HeaderStyle-Width="7%"
                                        ItemStyle-Width="7%" FooterStyle-Width="7%" />
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Alineado con Aula Virtual">
                                        <ItemTemplate>
                                            <%--<asp:RadioButtonList ID="rdModoCalifica" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rdModoCalifica_SelectedIndexChanged">
                                                <asp:ListItem Value="0" Selected="True"> Directo</asp:ListItem>
                                                <asp:ListItem Value="1"> Aula Virtual</asp:ListItem>
                                            </asp:RadioButtonList>--%>
                                            <asp:CheckBox ID="chkAlineado" runat="server" Enabled="false" Checked='<%# IIF(Eval("codigo_mod")=-1,False,True)%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="descripcion_eva" HeaderText="Tarea de Aula Virtual" />
                                    <%-- <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Aula Virtual" HeaderStyle-Width="20%">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="cboMoodle" runat="server" CssClass="form-control input-sm"
                                                AutoPostBack="true" OnSelectedIndexChanged="cboMoodle_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Acción">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnEnlazar" runat="server" OnClick="btnEnlazar_Click" CommandName="Enlazar"
                                                CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" Visible='<%# IIF(Eval("codigo_mod")=-1,True,False)%>'
                                                CssClass="btn btn-warning btn-sm" OnClientClick="return confirm('¿Desea alinear con aula virtual?');"
                                                ToolTip="Enlazar tarea">
                                                <span><i class="fa fa-link"></i></span>
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="btnDesenlazar" runat="server" CommandName="Desenlazar" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                                                Visible='<%# IIF(Eval("codigo_mod")<>-1, IIF(Eval("estadoNota_Cup") <> "R", True, False), False) %>'
                                                CssClass="btn btn-danger btn-sm" OnClientClick="return confirm('¿Está seguro de desalinear la tarea?');"
                                                ToolTip="Desenlazar tarea">
                                                <span><i class="fa fa-unlink"></i></span>
                                            </asp:LinkButton>
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
    <div id="myModal" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content" id="modalFinalizaBody">
                <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                    <h4 class="modal-title">
                        Alinear Instrumento de Evaluación con Aula Virtual</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label class="col-md-3">
                                    Tarea:
                                </label>
                                <div class="col-md-9">
                                    <asp:DropDownList ID="cboMoodle" runat="server" CssClass="form-control input-sm">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" id="btnsalir" class="btn btn-danger" data-dismiss="modal">
                        Cancelar</button>
                    <asp:Button ID="btnGrabar" runat="server" Text="Guardar" class="btn btn-success" />
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
