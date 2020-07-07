<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmConfigurarAsignatura.aspx.vb"
    Inherits="GestionCurricular_frmConfigurarAsignatura" EnableEventValidation="false" %>

<%@ Register Assembly="BusyBoxDotNet" Namespace="BusyBoxDotNet" TagPrefix="busyboxdotnet" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta name="google" value="notranslate">
    <title>Configuración de Asignatura</title>
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
        $(document).ready(function() {
            $('#' + "<%=fuArchivo.ClientID%>").attr('accept', 'application/pdf');

            $('#btnGuardar').click(function() {
                var txtDA = document.getElementById('lblDA').innerHTML;
                if (txtDA != "[OK]") {
                    alert(txtDA);
                    document.getElementById('link1').click();
                    return false;
                }
                var txtRA = document.getElementById('lblRA').innerHTML;
                if (txtRA != "[OK]") {
                    alert(txtRA);
                    document.getElementById('link2').click();
                    return false;
                }
                var txtCA = document.getElementById('lblCA').innerHTML;
                if (txtCA != "[OK]") {
                    alert(txtCA);
                    document.getElementById('link3').click();
                    return false;
                }
                var txtED = document.getElementById('lblED').innerHTML;
                if (txtED != "[OK]") {
                    alert(txtED);
                    document.getElementById('link4').click();
                    return false;
                }
                var txtRB = document.getElementById('lblRB').innerHTML;
                if (txtRB != "[OK]") {
                    alert(txtRB);
                    document.getElementById('link5').click();
                    return false;
                }
            });
            $('#btnGuardar2').click(function() {
                var cbo = document.getElementById('<%=cboDiseño.ClientID%>');
                if (cbo.selectedIndex < 1) {
                    alert('¡ Seleccione Semestre !');
                    cbo.focus();
                    return false;
                }
            });
        });

        function openModal(modal_ord) {
            if (modal_ord == '1') {
                $('#myModal').modal('show');
            }
            else {
                if (modal_ord == '2') {
                    $('#myModal2').modal('show');
                }
                else {
                    $('#myModal3').modal('show');
                }
            }
        }

        function closeModal(modal_ord) {
            if (modal_ord == '1') {
                $('#myModal').modal('hide');
            }
            else {
                if (modal_ord == '2') {
                    $('#myModal2').modal('hide');
                }
                else {
                    $('#myModal3').modal('hide');
                }
            }
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

        function txtBuscar_onKeyPress(objTextBox, objBtnID) {
            if (window.event.keyCode == 13) {
                document.getElementById(objBtnID).focus();
                document.getElementById(objBtnID).click();
            }
        }

        function FileSelected(fu) {
            var fn = fu.value;
            $("#spnFile").empty();

            if (fn !== "") {
                var idx = fn.lastIndexOf("\\") + 1;
                fn = fn.substr(idx, fn.lenght);

                $("#hf").val("1");
                $("#spnFile").text(fn);
            } else {
                $("#hf").val("0");
                $("#spnFile").text("No se eligió anexo de la asignatura");
            }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server" method="post" autocomplete="off">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <busyboxdotnet:BusyBox ID="BusyBox1" runat="server" ShowBusyBox="OnLeavingPage" Image="Clock"
        Text="Su solicitud está siendo procesada..." Title="Por favor espere" />
    <div class="container-fluid">
        <div class="messagealert" id="alert_container">
        </div>
        <br />
        <div class="panel panel-default" id="pnlLista" runat="server">
            <div class="panel panel-heading">
                <h4>
                    Configurar Asignatura</h4>
            </div>
            <div class="panel panel-body">
                <div class="row">
                    <div class="col-md-4">
                        <div class="form-group">
                            <label class="col-md-4">
                                Semestre:</label>
                            <div class="col-md-8">
                                <asp:DropDownList ID="cboSemestre" runat="server" CssClass="form-control input-sm"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-5">
                        <div class="form-group">
                            <label class="col-md-4">
                                Carrera Profesional:</label>
                            <div class="col-md-8">
                                <asp:DropDownList ID="cboCarrProf" runat="server" CssClass="form-control input-sm"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <%--<asp:LinkButton ID="btnDiseño" runat="server" CssClass="btn btn-primary btn-sm" >
                            <span><i class="fa fa-pen"></i></span>  Ir Diseño
                        </asp:LinkButton>--%>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label class="col-md-3">
                                Estado:</label>
                            <div class="col-md-9">
                                <asp:DropDownList ID="cboEstado" runat="server" CssClass="form-control input-sm"
                                    AutoPostBack="true">
                                    <asp:ListItem Value="-1"> TODOS </asp:ListItem>
                                    <asp:ListItem Value="0" Selected="True"> PENDIENTE </asp:ListItem>
                                    <asp:ListItem Value="1"> ENVIADO </asp:ListItem>
                                    <asp:ListItem Value="2"> APROBADO </asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel panel-body">
                <div class="row">
                    <div class="col-md-6">
                        <div class="form-group">
                            <div class="col-md-10">
                                <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control input-sm" placeholder="Ingrese Nombre de la Asignatura"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                <asp:LinkButton ID="btnBuscar" runat="server" CssClass="btn btn-info">
                                    <span><i class="fa fa-search"></i></span>
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <div class="table-responsive">
                            <asp:GridView ID="gvAsignatura" runat="server" AutoGenerateColumns="false" ShowHeader="true"
                                AllowPaging="True" PageSize="10" DataKeyNames="codigo_cur, codigo_pes, nombre_Cur, codigo_dis, estado_dis"
                                OnRowCreated="gvAsignatura_OnRowCreated" CssClass="table table-bordered table-hover">
                                <Columns>
                                    <asp:BoundField DataField="cicloRom" HeaderText="Ciclo" />
                                    <asp:BoundField DataField="nombre_Cur" HeaderText="Nombre" />
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Avance">
                                        <ItemTemplate>
                                            <div class="progress">
                                                <div class="progress-bar progress-bar-info progress-bar-striped active" role="progressbar"
                                                    aria-valuenow="<%# Eval("avance") %>" aria-valuemin="0" aria-valuemax="100" style='<%# "width:" & Eval("avance") & "%" %>'>
                                                    <%#Eval("avance")%>
                                                    % Complete (success)
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="estado" HeaderText="Estado" />
                                    <asp:BoundField DataField="unidad" HeaderText="Unidades" />
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Acción">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnAgregarDA" runat="server" CommandName="FrmResultadoAprendizaje"
                                                CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" CssClass='<%# IIF(Eval("avance")=0,"btn btn-success btn-sm","btn btn-primary btn-sm") %>'
                                                Visible='<%# IIf(Eval("estado_dis")="P",True,False) %>' OnClientClick="return confirm('¿Desea ir a Registro de Unidades de Asignatura?');">
                                            <span><i class='<%# IIF(Eval("avance")=0,"fa fa-plus","fa fa-pen") %>'></i></span>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="evaluacion" HeaderText="Evaluación" />
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Acción">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnAgregarCE" runat="server" CommandName="frmCriteriosEvaluacion"
                                                CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" CssClass='<%# IIF(Eval("evaluacion")=0,"btn btn-success btn-sm","btn btn-primary btn-sm") %>'
                                                Visible='<%# IIf(Eval("unidad")>0 AND Eval("estado_dis")="P",True,False) %>'
                                                OnClientClick="return confirm('¿Desea ir a Registro de Criterios de Evaluación de Asignatura?');">
                                            <span><i class='<%# IIF(Eval("evaluacion")=0,"fa fa-plus","fa fa-pen") %>'></i></span>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="estrategia" HeaderText="Estrategias" />
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Acción">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnAgregarED" runat="server" CommandName="frmEstrategiasDidactica"
                                                CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" CssClass='<%# IIF(Eval("estrategia")=0,"btn btn-success btn-sm","btn btn-primary btn-sm") %>'
                                                Visible='<%# IIf(Eval("unidad")>0 AND Eval("estado_dis")="P",True,False) %>'
                                                OnClientClick="return confirm('¿Desea ir a Registro de Estrategias Didácticas de Asignatura?');">
                                            <span><i class='<%# IIF(Eval("estrategia")=0,"fa fa-plus","fa fa-pen") %>'></i></span>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="referencia" HeaderText="Referencias" />
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Acción">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnAgregarRB" runat="server" CommandName="frmReferenciaBibliografica"
                                                CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" CssClass='<%# IIF(Eval("referencia")=0,"btn btn-success btn-sm","btn btn-primary btn-sm") %>'
                                                Visible='<%# IIf(Eval("unidad")>0 AND Eval("estado_dis")="P",True,False) %>'
                                                OnClientClick="return confirm('¿Desea ir a Registro de Referencias Bibliográficas de Asignatura?');">
                                            <span><i class='<%# IIF(Eval("referencia")=0,"fa fa-plus","fa fa-pen") %>'></i></span>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="contenido" HeaderText="Contenidos" />
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Acción">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnAgregarGC" runat="server" CommandName="FrmContenidosAsignatura"
                                                CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" CssClass='<%# IIF(Eval("contenido")=0,"btn btn-success btn-sm","btn btn-primary btn-sm") %>'
                                                Visible='<%# IIf(Eval("unidad")>0 AND Eval("estado_dis")="P",True,False) %>'
                                                OnClientClick="return confirm('¿Desea ir a Registro de Contenido de Asignatura?');">
                                            <span><i class='<%# IIF(Eval("contenido")=0,"fa fa-plus","fa fa-pen") %>'></i></span>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Anexos">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnSubir" runat="server" CommandName="Subir" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                                                CssClass="btn btn-info btn-sm" Visible='<%# IIF(Eval("codigo_dis")<>-1 and (Eval("estado_dis")="P" or Eval("estado_dis")="O"), True, False) %>'
                                                OnClientClick="return confirm('¿Desea subir anexos a la Asignatura?');">
                                                <span><i class="fa fa-upload"></i></span>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Enviar">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnEnviar" runat="server" CommandName="Enviar" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                                                CssClass="btn btn-warning btn-sm" Visible='<%# IIf(Eval("avance")=100,IIF(Eval("estado_dis")="P",True,False),False) %>'
                                                OnClientClick="return confirm('¿Desea ir a enviar el Diseño de Asignatura?');">
                                                <span><i class="fa fa-share"></i></span>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Clonar">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnClonar" runat="server" CommandName="Clonar" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                                                CssClass="btn btn-success btn-sm" Visible='<%# IIf(Eval("avance")=0,True,False) %>'
                                                OnClientClick="return confirm('¿Desea ir a clonar el Diseño de Asignatura?');">
                                                <span><i class="fa fa-object-group"></i></span>
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
                                <PagerStyle ForeColor="#003399" HorizontalAlign="Center" CssClass="pagination-ys" />
                            </asp:GridView>
                        </div>
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
                                            <div class="row">
                                                <div class="col-md-11">
                                                    <label id="lblDA" runat="server">
                                                        [OK]</label>
                                                </div>
                                                <div class="col-md-1">
                                                    <asp:LinkButton ID="btnDA" runat="server" CssClass="btn btn-default btn-sm" OnClientClick="return confirm('¿Desea ir a Registro de Diseño de Asignatura?');">
                                                        <span><i class="fa fa-external-link-alt"></i></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
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
                                                <asp:GridView ID="gvResultados" runat="server" Width="99%" AutoGenerateColumns="false"
                                                    ShowHeader="true" OnRowCreated="gvResultados_OnRowCreated" OnRowDataBound="gvResultados_RowDataBound"
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
                                            <div class="row">
                                                <div class="col-md-11">
                                                    <label id="lblRA" runat="server">
                                                        [OK]</label>
                                                </div>
                                                <div class="col-md-1">
                                                    <asp:LinkButton ID="btnRA" runat="server" CssClass="btn btn-default btn-sm" OnClientClick="return confirm('¿Desea ir a Registro de Criterios de Evaluación?');">
                                                    <span><i class="fa fa-external-link-alt"></i></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
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
                                                    <asp:BoundField HtmlEncode="false" DataField="unidad" HeaderText="Unidad" HeaderStyle-Width="20%" />
                                                    <asp:BoundField HtmlEncode="false" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center"
                                                        DataField="sesion" HeaderText="Sesiones" HeaderStyle-Width="3%" />
                                                    <asp:BoundField HtmlEncode="false" DataField="contenido" HeaderText="Contenidos"
                                                        HeaderStyle-Width="35%" />
                                                    <asp:BoundField HtmlEncode="false" DataField="actividad" HeaderText="Actividades"
                                                        HeaderStyle-Width="34%" />
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
                                            <div class="row">
                                                <div class="col-md-11">
                                                    <label id="lblCA" runat="server">
                                                        [OK]</label>
                                                </div>
                                                <div class="col-md-1">
                                                    <asp:LinkButton ID="btnCA" runat="server" CssClass="btn btn-default btn-sm" OnClientClick="return confirm('¿Desea ir a Registro de Contenidos de Asignatura?');">
                                                    <span><i class="fa fa-external-link-alt"></i></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
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
                                            <div class="row">
                                                <div class="col-md-11">
                                                    <label id="lblED" runat="server">
                                                        [OK]</label>
                                                </div>
                                                <div class="col-md-1">
                                                    <asp:LinkButton ID="btnED" runat="server" CssClass="btn btn-default btn-sm" OnClientClick="return confirm('¿Desea ir a Registro de Estrategias Didácticas?');">
                                                    <span><i class="fa fa-external-link-alt"></i></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
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
                                                ShowHeader="true" DataKeyNames="codigo_ref, descripcion_ref, codigo_tip, nombre"
                                                CssClass="table table-bordered table-hover">
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
                                            <div class="row">
                                                <div class="col-md-11">
                                                    <label id="lblRB" runat="server">
                                                        [OK]</label>
                                                </div>
                                                <div class="col-md-1">
                                                    <asp:LinkButton ID="btnRB" runat="server" CssClass="btn btn-default btn-sm" OnClientClick="return confirm('¿Desea ir a Registro de Referencias Bibliográficas?');">
                                                    <span><i class="fa fa-external-link-alt"></i></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
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
                    <asp:LinkButton ID="btnGuardar" runat="server" Text="Enviar" class="btn btn-success" />
                </div>
            </div>
        </div>
    </div>
    <!-- Modal Clonar Diseño de Asignatura -->
    <div id="myModal2" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg">
            <div class="modal-content" id="modalFinalizaBody2">
                <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                    <h4 class="modal-title">
                        Clonar Diseño de Asignatura</h4>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="panModal" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label class="col-md-3">
                                            Semestre Académico:</label>
                                        <div class="col-md-6">
                                            <asp:DropDownList ID="cboDiseño" runat="server" CssClass="form-control input-sm"
                                                AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="panel-group" id="accordion2">
                                        <div class="panel panel-default">
                                            <div class="panel-heading">
                                                <h4 class="panel-title">
                                                    <a id="link11" data-toggle="collapse" data-parent="#accordion2" href="#c1">Unidades
                                                        de Asignatura</a>
                                                </h4>
                                            </div>
                                            <div id="c1" class="panel-collapse collapse in">
                                                <div class="panel-body">
                                                    <asp:GridView ID="gvUA" runat="server" Width="99%" AutoGenerateColumns="false" ShowHeader="true"
                                                        DataKeyNames="codigo_uni, codigo_res, descripcion_uni, codigo_dis, numero_uni"
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
                                            </div>
                                        </div>
                                        <div class="panel panel-default">
                                            <div class="panel-heading">
                                                <h4 class="panel-title">
                                                    <a id="link12" data-toggle="collapse" data-parent="#accordion2" href="#c2">Criterios
                                                        de Evaluación</a>
                                                </h4>
                                            </div>
                                            <div id="c2" class="panel-collapse collapse">
                                                <div class="panel-body">
                                                    <div class="table-responsive">
                                                        <asp:GridView ID="gvCE" runat="server" Width="99%" AutoGenerateColumns="false" ShowHeader="true"
                                                            OnRowCreated="gvResultados_OnRowCreated" OnRowDataBound="gvResultados_RowDataBound"
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
                                            </div>
                                        </div>
                                        <div class="panel panel-default">
                                            <div class="panel-heading">
                                                <h4 class="panel-title">
                                                    <a id="link13" data-toggle="collapse" data-parent="#accordion2" href="#c3">Contenidos
                                                        de Asignatura</a>
                                                </h4>
                                            </div>
                                            <div id="c3" class="panel-collapse collapse">
                                                <div class="panel-body">
                                                    <asp:GridView ID="gvCA" runat="server" Width="99%" AutoGenerateColumns="false" ShowHeader="true"
                                                        DataKeyNames="codigo_dis, codigo_uni, unidad, codigo_gru, total_ses" CssClass="table table-sm table-bordered table-hover">
                                                        <Columns>
                                                            <asp:BoundField HtmlEncode="false" DataField="unidad" HeaderText="Unidad" HeaderStyle-Width="20%" />
                                                            <asp:BoundField HtmlEncode="false" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center"
                                                                DataField="sesion" HeaderText="Sesiones" HeaderStyle-Width="3%" />
                                                            <asp:BoundField HtmlEncode="false" DataField="contenido" HeaderText="Contenidos"
                                                                HeaderStyle-Width="35%" />
                                                            <asp:BoundField HtmlEncode="false" DataField="actividad" HeaderText="Actividades"
                                                                HeaderStyle-Width="34%" />
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
                                            </div>
                                        </div>
                                        <div class="panel panel-default">
                                            <div class="panel-heading">
                                                <h4 class="panel-title">
                                                    <a id="link14" data-toggle="collapse" data-parent="#accordion2" href="#c4">Estrategias
                                                        Didácticas</a>
                                                </h4>
                                            </div>
                                            <div id="c4" class="panel-collapse collapse">
                                                <div class="panel-body">
                                                    <asp:GridView ID="gvED" runat="server" Width="99%" AutoGenerateColumns="false" ShowHeader="true"
                                                        DataKeyNames="codigo_est, descripcion_dis" CssClass="table table-bordered  table-hover">
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
                                            </div>
                                        </div>
                                        <div class="panel panel-default">
                                            <div class="panel-heading">
                                                <h4 class="panel-title">
                                                    <a id="link15" data-toggle="collapse" data-parent="#accordion2" href="#c5">Referencias
                                                        Bibliográficas</a>
                                                </h4>
                                            </div>
                                            <div id="c5" class="panel-collapse collapse">
                                                <div class="panel-body">
                                                    <asp:GridView ID="gvRB" runat="server" Width="99%" AutoGenerateColumns="false" ShowHeader="true"
                                                        DataKeyNames="codigo_ref, descripcion_ref, codigo_tip, nombre" CssClass="table table-bordered table-hover">
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
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                    <button type="button" id="btnsalir2" class="btn btn-danger" data-dismiss="modal">
                        Cancelar</button>
                    <asp:LinkButton ID="btnGuardar2" runat="server" Text="Guardar" class="btn btn-success" />
                </div>
            </div>
        </div>
    </div>
    <!-- Modal subir anexo -->
    <div id="myModal3" class="modal fade" role="dialog" role="dialog" aria-labelledby="myModalLabel"
        aria-hidden="true" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg">
            <div class="modal-content" id="Div3">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                    <h4 class="modal-title">
                        Subir Anexos</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-xs-12 col-sm-12">
                            <div class="form-group row">
                                <asp:UpdatePanel ID="updFileUpload" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                    <ContentTemplate>
                                        <label class="col-sm-3" for="fuArchivo">
                                            Adjuntar Anexo:</label>
                                        <div class="col-sm-9">
                                            <label id="lblFuArchivo" runat="server" style="font-style: normal; font-size: small;
                                                font-weight: normal">
                                                <input id="btnFuArchivo" type="button" value="Seleccionar archivo" runat="server" />
                                                <span id="spnFile" runat="server">No se eligió anexo de la asignatura</span>
                                            </label>
                                            <asp:FileUpload ID="fuArchivo" runat="server" CssClass="form-control input-sm" AllowMultiple="true"
                                                Accept=".pdf" Style="display: none;" onChange="FileSelected(this);" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic"
                                                ErrorMessage="* Seleccione un archivo" ControlToValidate="fuArchivo" ValidationGroup="subirArchivo">
                                            </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ForeColor="Red"
                                                runat="server" ValidationGroup="subirArchivo" ControlToValidate="fuArchivo" ValidationExpression="^([a-zA-Z].*|[1-9].*)\.(((p|P)(d|D)(f|F)))$">Solo archivos *.pdf</asp:RegularExpressionValidator>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" id="btnSalirAnexo" class="btn btn-danger" data-dismiss="modal">
                        Cancelar</button>
                    <asp:Button ID="btnGuardarAnexo" runat="server" Text="Guardar" CssClass="btn btn-success"
                        required ValidationGroup="subirArchivo" />
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
