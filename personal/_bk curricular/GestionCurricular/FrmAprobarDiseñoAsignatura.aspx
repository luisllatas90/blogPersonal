<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmAprobarDiseñoAsignatura.aspx.vb"
    Inherits="GestionCurricular_FrmAprobarDiseñoAsignatura" EnableEventValidation="false" %>

<%@ Register Assembly="BusyBoxDotNet" Namespace="BusyBoxDotNet" TagPrefix="busyboxdotnet" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta name="google" value="notranslate">
    <title>Aprobar diseño de Asignatura</title>
    <!-- custom scrollbar stylesheet -->
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=7' />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <link rel="stylesheet" type="text/css" href="../assets/css/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="../assets/fontawesome-5.2/css/all.min.css" />

    <script type="text/javascript" src="../assets/js/jquery.js"></script>

    <script type="text/javascript" src="js/popper.js"></script>

    <script type="text/javascript" src="../assets/js/bootstrap.min.js"></script>

    <script type="text/javascript" src='../assets/js/jquery-ui-1.10.3.custom.min.js'></script>

    <script type="text/javascript" src="../assets/fontawesome-5.2/js/all.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            $('#btnBuscar').click(function() {
                if ($('#ddlSemestre').val() == '' || $('#ddlSemestre').val() == '-1') {
                    alert("Seleccione el Semestre para listar las Asignaturas");
                    $('#ddlSemestre').focus();
                    return false;
                }

                if ($('#ddlPlanCurricular').val() == '' || $('#ddlPlanCurricular').val() == '-1') {
                    alert("Seleccione el Plan Curricular para listar las Asignaturas");
                    $('#ddlPlanCurricular').focus();
                    return false;
                }
            });
        });

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

        function openModal(modal) {
            switch (modal) {
                case "aprobar":
                    $('#myModal').modal('show');
                    break;
                default:
                    $('#myModalObs').modal('show');
            }
        }

        function closeModal() {
            switch (modal) {
                case "aprobar":
                    $('#myModal').modal('hide');
                    break;
                default:
                    $('#myModalObs').modal('hide');
            }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server" method="post" autocomplete="off">
    <busyboxdotnet:BusyBox ID="BusyBox1" runat="server" ShowBusyBox="OnLeavingPage" Image="Clock"
        Text="Su solicitud está siendo procesada..." Title="Por favor espere" />
    <!-- Listado Aprobar diseño de Asignatura -->
    <div class="container-fluid">
        <div class="messagealert" id="alert_container">
        </div>
        <br />
        <div class="panel panel-default" id="pnlLista" runat="server">
            <div class="panel panel-heading">
                <h4>
                    Aprobar diseño de Asignatura</h4>
            </div>
            <div class="panel panel-body">
                <div class="row">
                    <div class="col-md-6">
                        <div class="form-group">
                            <label class="col-md-4">
                                Semestre:</label>
                            <div class="col-md-8">
                                <asp:DropDownList ID="ddlSemestre" runat="server" CssClass="form-control input-sm"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <label class="col-md-4">
                                Plan Curricular:</label>
                            <div class="col-md-8">
                                <asp:DropDownList ID="ddlPlanCurricular" runat="server" CssClass="form-control input-sm"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-6">
                        <div class="form-group">
                            <label class="col-md-4">
                                Estado:</label>
                            <div class="col-md-8">
                                <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-control input-sm"
                                    AutoPostBack="true">
                                    <asp:ListItem Value="%" Selected="True">[--- TODOS ---]</asp:ListItem>
                                    <asp:ListItem Value="A">CON DISEÑO APROBADO</asp:ListItem>
                                    <asp:ListItem Value="E">SIN DISEÑO APROBADO</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-1">
                        <asp:Button ID="btnListar" runat="server" Text="Listar Asignaturas" CssClass="btn btn-info"
                            Visible="false" />
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <asp:GridView ID="gvResultados" runat="server" Width="99%" AutoGenerateColumns="false"
                            ShowHeader="true" DataKeyNames="codigo_Cur,nombre_Cur,codigo_dis,fecha_apr,codigo_Pes"
                            CssClass="table table-bordered">
                            <Columns>
                                <asp:BoundField DataField="codigo_Cur" HeaderText="CÓDIGO" Visible="false" />
                                <asp:BoundField DataField="nombre_Cur" HeaderText="NOMBRE DEL CURSO" HeaderStyle-Width="50%" />
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="DISEÑO ASIGNADO"
                                    HeaderStyle-Width="15%">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkDiseño" runat="server" Checked='<%# Eval("tiene_dis") %>' Enabled="false" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateField>
                                <asp:BoundField DataField="fecha_apr" HeaderText="FECHA DE APROBACIÓN" HeaderStyle-Width="20%" />
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Operaciones" HeaderStyle-Width="15%">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnAprobar" runat="server" CommandName="Aprobar" Text='<i class="fa fa-check"></i>'
                                            ToolTip="Aprobar diseño" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                                            CssClass="btn btn-success btn-sm" OnClick="btnAprobar_Click" OnClientClick="return confirm('¿Seguro de aprobar el diseño de la asignatura?');"
                                            Enabled='<%# Eval("tiene_dis").toString() %>'></asp:LinkButton>
                                        <asp:LinkButton ID="btnObservar" runat="server" CommandName="Observar" Text='<i class="fa fa-unlink"></i>'
                                            ToolTip="Observar diseño" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                                            CssClass="btn btn-danger btn-sm" OnClientClick="return confirm('¿Desea generar una observación del diseño de la asignatura?');">
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                No se encontraron diseño de asignaturas para aprobar
                            </EmptyDataTemplate>
                            <HeaderStyle BackColor="#D9534F" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center"
                                Font-Size="12px" />
                            <RowStyle Font-Size="11px" />
                            <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Modal Enviar Diseño de Asignatura -->
    <div id="myModal" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg">
            <div class="modal-content" id="modalFinalizaBody">
                <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                    <h4 class="modal-title">
                        Enviar Diseño de Asignatura</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="panel-group" id="accordion">
                                <div class="panel panel-default">
                                    <div class="panel-heading">
                                        <h4 class="panel-title">
                                            <a id="link1" data-toggle="collapse" data-parent="#accordion" href="#collapse1">Unidades
                                                de Asignatura</a>
                                        </h4>
                                    </div>
                                    <div id="collapse1" class="panel-collapse collapse in">
                                        <div class="panel-body">
                                            <asp:GridView ID="gvUnidad" runat="server" Width="99%" AutoGenerateColumns="false"
                                                ShowHeader="true" DataKeyNames="codigo_uni, codigo_res, descripcion_uni, codigo_dis, numero_uni"
                                                CssClass="table table-bordered">
                                                <Columns>
                                                    <asp:BoundField DataField="numero_uni" HeaderText="Unidad" HeaderStyle-Width="5%" />
                                                    <asp:BoundField DataField="descripcion_uni" HeaderText="Descripción de la unidad"
                                                        HeaderStyle-Width="35%" />
                                                    <asp:BoundField DataField="descripcion_res" HeaderText="Resultado de aprendizaje"
                                                        HeaderStyle-Width="60%" />
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    No se encontraron datos
                                                </EmptyDataTemplate>
                                                <HeaderStyle BackColor="#D9534F" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center"
                                                    Font-Size="12px" />
                                                <RowStyle Font-Size="11px" />
                                                <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                            </asp:GridView>
                                        </div>
                                        <div class="panel-footer">
                                            <%--<div class="row">
                                                <div class="col-md-11">
                                                    <label id="lblDA" runat="server">[OK]</label>
                                                </div>
                                                <div class="col-md-1">
                                                    <asp:LinkButton ID="btnDA" runat="server" CssClass="btn btn-default btn-sm"
                                                        OnClientClick="return confirm('¿Desea ir a Registro de Diseño de Asignatura?');">
                                                    <span><i class="fa fa-external-link-alt"></i></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>--%>
                                        </div>
                                    </div>
                                </div>
                                <div class="panel panel-default">
                                    <div class="panel-heading">
                                        <h4 class="panel-title">
                                            <a id="link2" data-toggle="collapse" data-parent="#accordion" href="#collapse2">Criterios
                                                de Evaluación</a>
                                        </h4>
                                    </div>
                                    <div id="collapse2" class="panel-collapse collapse">
                                        <div class="panel-body">
                                            <div class="table-responsive">
                                                <asp:GridView ID="gvCriterios" runat="server" Width="99%" AutoGenerateColumns="false"
                                                    ShowHeader="true" OnRowCreated="gvCriterios_OnRowCreated" OnRowDataBound="gvCriterios_RowDataBound"
                                                    DataKeyNames="codigo_dis,codigo_uni,codigo_res,descripcion_res,peso_res,codigo_ind,descripcion_ind,peso_ind,codigo_indEvi,codigo_evi,descripcion_evi,codigo_eviIns,codigo_ins,descripcion_ins,peso_ins,tipo_prom,tipo_prom2"
                                                    CssClass="table table-bordered table-hover">
                                                    <Columns>
                                                        <asp:BoundField DataField="descripcion_res" HeaderText="Descripción" />
                                                        <asp:BoundField DataField="peso_res" HeaderText="Peso" />
                                                        <asp:BoundField DataField="descripcion_ind" HeaderText="Descripción" />
                                                        <asp:BoundField DataField="peso_ind" HeaderText="Peso" />
                                                        <asp:BoundField DataField="descripcion_evi" HeaderText="Descripción" />
                                                        <asp:BoundField DataField="descripcion_ins" HeaderText="Descripción" />
                                                        <asp:BoundField DataField="peso_ins" HeaderText="Peso" />
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        No se encontraron Datos!
                                                    </EmptyDataTemplate>
                                                    <HeaderStyle BackColor="#D9534F" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center"
                                                        Font-Size="12px" Font-Bold="true" />
                                                    <RowStyle Font-Size="11px" />
                                                    <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                                </asp:GridView>
                                            </div>
                                        </div>
                                        <div class="panel-footer">
                                            <%--<div class="row">
                                                <div class="col-md-11">
                                                    <label id="lblRA" runat="server">[OK]</label>
                                                </div>
                                                <div class="col-md-1">
                                                    <asp:LinkButton ID="btnRA" runat="server" CssClass="btn btn-default btn-sm"
                                                        OnClientClick="return confirm('¿Desea ir a Registro de Criterios de Evaluación?');">
                                                    <span><i class="fa fa-external-link-alt"></i></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>--%>
                                        </div>
                                    </div>
                                </div>
                                <div class="panel panel-default">
                                    <div class="panel-heading">
                                        <h4 class="panel-title">
                                            <a id="link3" data-toggle="collapse" data-parent="#accordion" href="#collapse3">Contenidos
                                                de Asignatura</a>
                                        </h4>
                                    </div>
                                    <div id="collapse3" class="panel-collapse collapse">
                                        <div class="panel-body">
                                            <asp:GridView ID="gvContenido" runat="server" Width="99%" AutoGenerateColumns="false"
                                                ShowHeader="true" DataKeyNames="codigo_dis, codigo_uni, unidad, codigo_gru, total_ses"
                                                CssClass="table table-sm table-bordered table-hover">
                                                <Columns>
                                                    <asp:BoundField HtmlEncode="false" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center"
                                                        DataField="sesion" HeaderText="Sesiones" HeaderStyle-Width="3%" />
                                                    <asp:BoundField HtmlEncode="false" DataField="contenido" HeaderText="Contenidos"
                                                        HeaderStyle-Width="45%" />
                                                    <asp:BoundField HtmlEncode="false" DataField="actividad" HeaderText="Actividades"
                                                        HeaderStyle-Width="44%" />
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    No se encontraron contenido de asignaturas
                                                </EmptyDataTemplate>
                                                <HeaderStyle BackColor="#D9534F" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center"
                                                    Font-Size="12px" />
                                                <RowStyle Font-Size="11px" />
                                                <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                            </asp:GridView>
                                        </div>
                                        <div class="panel-footer">
                                            <%--<div class="row">
                                                <div class="col-md-11">
                                                    <label id="lblCA" runat="server">[OK]</label>
                                                </div>
                                                <div class="col-md-1">
                                                    <asp:LinkButton ID="btnCA" runat="server" CssClass="btn btn-default btn-sm"
                                                        OnClientClick="return confirm('¿Desea ir a Registro de Contenidos de Asignatura?');">
                                                    <span><i class="fa fa-external-link-alt"></i></span>
                                                    </asp:LinkButton>
                                                </div>
                                           </div>--%>
                                        </div>
                                    </div>
                                </div>
                                <div class="panel panel-default">
                                    <div class="panel-heading">
                                        <h4 class="panel-title">
                                            <a id="link4" data-toggle="collapse" data-parent="#accordion" href="#collapse4">Estrategias
                                                Didácticas</a>
                                        </h4>
                                    </div>
                                    <div id="collapse4" class="panel-collapse collapse">
                                        <div class="panel-body">
                                            <asp:GridView ID="gvEstrategia" runat="server" Width="99%" AutoGenerateColumns="false"
                                                ShowHeader="true" DataKeyNames="codigo_est, descripcion_dis" CssClass="table table-bordered  table-hover">
                                                <Columns>
                                                    <asp:BoundField DataField="codigo_est" HeaderText="Código" Visible="false" />
                                                    <asp:BoundField DataField="descripcion_dis" HeaderText="Nombre" />
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    No se encontraron estrategias didácticas
                                                </EmptyDataTemplate>
                                                <HeaderStyle BackColor="#D9534F" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center"
                                                    Font-Size="12px" />
                                                <RowStyle Font-Size="11px" />
                                                <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                            </asp:GridView>
                                        </div>
                                        <div class="panel-footer">
                                            <%--<div class="row">
                                                <div class="col-md-11">
                                                    <label id="lblED" runat="server">[OK]</label>
                                                </div>
                                                <div class="col-md-1">
                                                    <asp:LinkButton ID="btnED" runat="server" CssClass="btn btn-default btn-sm"
                                                        OnClientClick="return confirm('¿Desea ir a Registro de Estrategias Didácticas?');">
                                                    <span><i class="fa fa-external-link-alt"></i></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>--%>
                                        </div>
                                    </div>
                                </div>
                                <div class="panel panel-default">
                                    <div class="panel-heading">
                                        <h4 class="panel-title">
                                            <a id="link5" data-toggle="collapse" data-parent="#accordion" href="#collapse5">Referencias
                                                Bibliográficas</a>
                                        </h4>
                                    </div>
                                    <div id="collapse5" class="panel-collapse collapse">
                                        <div class="panel-body">
                                            <asp:GridView ID="gvReferencia" runat="server" Width="99%" AutoGenerateColumns="false"
                                                ShowHeader="true" DataKeyNames="codigo_ref, descripcion_ref, codigo_tip" CssClass="table table-bordered table-hover">
                                                <Columns>
                                                    <asp:BoundField DataField="nombre" HeaderText="Tipo Referencia" HeaderStyle-Width="20%" />
                                                    <asp:BoundField DataField="descripcion_ref" HeaderText="Referencia" />
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    No se encontraron Datos!
                                                </EmptyDataTemplate>
                                                <HeaderStyle BackColor="#D9534F" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center"
                                                    Font-Size="12px" />
                                                <RowStyle Font-Size="11px" />
                                                <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                            </asp:GridView>
                                        </div>
                                        <div class="panel-footer">
                                            <%--<div class="row">
                                                <div class="col-md-11">
                                                    <label id="lblRB" runat="server">[OK]</label>
                                                </div>
                                                <div class="col-md-1">
                                                    <asp:LinkButton ID="btnRB" runat="server" CssClass="btn btn-default btn-sm"
                                                        OnClientClick="return confirm('¿Desea ir a Registro de Referencias Bibliográficas?');">
                                                    <span><i class="fa fa-external-link-alt"></i></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>--%>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" id="btnsalir" class="btn btn-danger" data-dismiss="modal">
                        Cancelar</button>
                    <asp:LinkButton ID="btnGuardar" runat="server" Text='<i class="fa fa-check"></i> Aprobar'
                        class="btn btn-success" />
                </div>
            </div>
        </div>
    </div>
    <!-- Modal Registro de Observacion -->
    <div id="myModalObs" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content" id="Div2">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                    <h4 class="modal-title">
                        Registrar Observación</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <asp:TextBox ID="txtObservacion" runat="server" CssClass="form-control input-sm"
                                    TextMode="MultiLine" Rows="5"></asp:TextBox>
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
        <asp:HiddenField ID="hdcodigo_cup" runat="server" />
    </form>
</body>
</html>
