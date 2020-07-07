<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmCriteriosEvaluacion.aspx.vb"
    Inherits="GestionCurricular_frmCriteriosEvaluacion" EnableEventValidation="false" %>

<%@ Register Assembly="BusyBoxDotNet" Namespace="BusyBoxDotNet" TagPrefix="busyboxdotnet" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta name="google" value="notranslate">
    <title>Criterios de Evaluación</title>
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
            $('#btnListar').click(function() {
                return fc_validarCombos();
            });
            $('#btnGrabarRes').click(function() {
                var nombre = $('#txtResultado').val();
                if (nombre == '') {
                    alert("¡ Ingrese Resultado de Aprendizaje !");
                    $('#txtResultado').focus();
                    return false;
                }
                var porcen = $('#txtPorcenRes').val();
                if (porcen == '') {
                    alert("¡ Ingrese Porcentaje !");
                    $('#txtPorcenRes').focus();
                    return false;
                }
                var num = parseInt(porcen);
                if (num < 1 || num > 100) {
                    alert("¡ Ingrese Porcentaje como minimo 1 y maximo 100 !");
                    $('#txtPorcenRes').focus();
                    return false;
                }
                var sum = '<%= Session("gc_peso_res") %>';
                var sum_peso = parseFloat(sum);
                if ((sum_peso + num) > 100) {
                    alert("¡ Ingrese Porcentaje. La suma del ponderado supera el 100% !");
                    $('#txtPorcenRes').focus();
                    return false;
                }
            });
            $('#btnGrabarInd').click(function() {
                var nombre = $('#txtIndicador').val();
                if (nombre == '') {
                    alert("¡ Ingrese Indicador de Aprendizaje !");
                    $('#txtIndicador').focus();
                    return false;
                }
                var txtNum = document.getElementById('<%=txtPorcentaje.ClientID%>');
                if (!(txtNum.disabled)) {
                    var porcen = $('#txtPorcentaje').val();
                    if (porcen == '') {
                        alert("¡ Ingrese Porcentaje !");
                        $('#txtPorcentaje').focus();
                        return false;
                    }
                    var num = parseInt(porcen);
                    if (num < 1 || num > 100) {
                        alert("¡ Ingrese Porcentaje como minimo 1 y maximo 100 !");
                        $('#txtPorcentaje').focus();
                        return false;
                    }
                    var sum = '<%= Session("gc_peso_ind") %>';
                    var sum_peso = parseFloat(sum);
                    if ((sum_peso + num) > 100) {
                        alert("¡ Ingrese Porcentaje. La suma del ponderado supera el 100% !");
                        $('#txtPorcentaje').focus();
                        return false;
                    }
                }
            });
            $('#btnGrabarEvi').click(function() {
                var nombre = $('#txtEvidencia').val();
                if (nombre == '') {
                    alert("¡ Ingrese Evidencia de Aprendizaje !");
                    $('#txtEvidencia').focus();
                    return false;
                }
            });
            $('#btnGrabarIns').click(function() {
                var nombre = $('#txtInstrumento').val();
                if (nombre == '') {
                    alert("¡ Ingrese Instrumento de Aprendizaje !");
                    $('#txtInstrumento').focus();
                    return false;
                }
                var txtNum2 = document.getElementById('<%=txtPorcenIns.ClientID%>');
                if (!(txtNum2.disabled)) {
                    var porcen = $('#txtPorcenIns').val();
                    if (porcen == '') {
                        alert("¡ Ingrese Porcentaje !");
                        $('#txtPorcenIns').focus();
                        return false;
                    }
                    var num = parseInt(porcen);
                    if (num < 1 || num > 100) {
                        alert("¡ Ingrese Porcentaje como minimo 1 y maximo 100 !");
                        $('#txtPorcenIns').focus();
                        return false;
                    }
                    var sum = '<%= Session("gc_peso_ins") %>';
                    var sum_peso = parseFloat(sum);
                    if ((sum_peso + num) > 100) {
                        alert("¡ Ingrese Porcentaje. La suma del ponderado supera el 100% !");
                        $('#txtPorcenIns').focus();
                        return false;
                    }
                }
            });
        });

        function openModal(modal, accion) {
            switch (modal) {
                case "Resultado":
                    $('#myModalRes').modal('show');
                    document.getElementById('txtResultado').select();
                    break;
                case "Indicador":
                    if (accion == 'Agregar') {
                        $('#txtIndicador').val(' ');
                        $('#txtPorcentaje').val('');
                    }
                    $('#myModal').modal('show');
                    document.getElementById('txtIndicador').select();
                    break;
                case "Evidencia":
                    if (accion == 'Agregar') {
                        $('#txtEvidencia').val(' ');
                    }
                    $('#myModalEvi').modal('show');
                    document.getElementById('txtEvidencia').select();
                    break;
                case "Instrumento":
                    if (accion == 'Agregar') {
                        $('#txtInstrumento').val(' ');
                        $('#txtPorcenIns').val('');
                    }
                    $('#myModalIns').modal('show');
                    document.getElementById('txtInstrumento').select();
                    break;
                default:
                    $('#myModalImp').modal('show');
            }
        }

        function closeModal() {
            switch (modal) {
                case "Resultado":
                    $('#myModalRes').modal('hide');
                    break;
                case "Indicador":
                    $('#myModal').modal('hide');
                    break;
                case "Evidencia":
                    $('#myModalEvi').modal('hide');
                    break;
                case "Instrumento":
                    $('#myModalIns').modal('hide');
                    break;
                default:
                    $('#myModalImp').modal('hide');
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

        function isNumeric(n) {
            return !isNaN(parseFloat(n)) && isFinite(n);
        }

        function fc_validarCombos() {
            var cboCA = document.getElementById('<%=cboSemestre.ClientID%>');
            if (cboCA.selectedIndex < 1) {
                //alert('¡ Seleccione Semestre !');
                ShowMessage('¡ Seleccione Semestre !', 'Warning');
                cboCA.focus();
                return false;
            }
            var cboCP = document.getElementById('<%=cboCarrProf.ClientID%>');
            if (cboCP.selectedIndex < 1) {
                //alert('¡ Seleccione Carrera Profesional !');
                ShowMessage('¡ Seleccione Carrera Profesional !', 'Warning');
                cboCP.focus();
                return false;
            }
            var cboPE = document.getElementById('<%=cboPlanEstudio.ClientID%>');
            if (cboPE.selectedIndex < 1) {
                //alert('¡ Seleccione Plan Estudio !');
                ShowMessage('¡ Seleccione Plan Estudio !', 'Warning');
                cboPE.focus();
                return false;
            }
            var cboDA = document.getElementById('<%=cboDisApr.ClientID%>');
            if (cboDA.selectedIndex < 1) {
                //alert('¡ Seleccione Diseño Asignatura !');
                ShowMessage('¡ Seleccione Diseño Asignatura !', 'Warning');
                cboDA.focus();
                return false;
            }
        }

        function soloNumeros(e) {
            var key = window.Event ? e.which : e.keyCode
            return (key >= 48 && key <= 57)
        }
    </script>

</head>
<body>
    <form id="form1" runat="server" method="post" autocomplete="off">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <busyboxdotnet:BusyBox ID="BusyBox1" runat="server" ShowBusyBox="OnLeavingPage" Image="Clock"
        Text="Su solicitud está siendo procesada..." Title="Por favor espere" />
    <!-- Listado Resultados deAprendizaje -->
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
                    <div class="col-md-10">
                        <h4>
                            <label id="lblCurso" runat="server">
                                Configurar Criterios de Evaluación</label></h4>
                    </div>
                    <div class="col-md-1">
                        <asp:LinkButton ID="btnSeguir" runat="server" CssClass="btn btn-default">
                            <span><i class="fa fa-arrow-right"></i></span> Seguir
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
            <div class="panel panel-body">
                <div class="row">
                    <div class="col-md-5">
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
                    <div class="col-md-7">
                        <div class="form-group">
                            <label class="col-md-4">
                                Carrera Profesional:</label>
                            <div class="col-md-8">
                                <asp:DropDownList ID="cboCarrProf" runat="server" CssClass="form-control input-sm"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <%-- <div class="col-md-2">
                        <asp:Button ID="btnListar" runat="server" Text="Listar" CssClass="btn btn-info"/>
                        <asp:Button ID="btnImportar" runat="server" Text="Importar" CssClass="btn btn-success"/>
                    </div>--%>
                </div>
                <div class="row">
                    <div class="col-md-5">
                        <div class="form-group">
                            <label class="col-md-4">
                                Plan Estudio:</label>
                            <div class="col-md-8">
                                <asp:DropDownList ID="cboPlanEstudio" runat="server" CssClass="form-control input-sm"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-7">
                        <div class="form-group">
                            <label id="lblAsignatura" runat="server" class="col-md-4">
                                Asignatura:</label>
                            <div class="col-md-8">
                                <asp:DropDownList ID="cboDisApr" runat="server" CssClass="form-control input-sm"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel panel-body">
                <div class="row">
                    <div class="col-md-12">
                        <div class="table-responsive">
                            <!-- OnRowDataBound="gvResultados_RowDataBound" -->
                            <asp:GridView ID="gvResultados" runat="server" Width="99%" AutoGenerateColumns="false"
                                ShowHeader="true" OnRowCreated="gvResultados_OnRowCreated" DataKeyNames="codigo_dis,codigo_uni,codigo_res,descripcion_res,peso_res,codigo_ind,descripcion_ind,peso_ind,codigo_indEvi,codigo_evi,descripcion_evi,codigo_eviIns,codigo_ins,descripcion_ins,peso_ins,tipo_prom,tipo_prom2"
                                OnRowDataBound="gvResultados_RowDataBound" CssClass="table table-bordered table-hover">
                                <Columns>
                                    <asp:BoundField DataField="descripcion_uni" HeaderText="N°" />
                                    <asp:BoundField DataField="descripcion_res" HeaderText="Descripción" />
                                    <asp:BoundField DataField="peso_res" HeaderText="Peso" HeaderStyle-Width="4%" />
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Editar" HeaderStyle-Width="4%">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnEditResultado" runat="server" OnClick="btnEditResultado_Click"
                                                CommandName="EditResultado" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                                                CssClass="btn btn-primary btn-sm" OnClientClick="return confirm('¿Desea editar resultados?');">
                                                <span><i class="fa fa-pen"></i></span>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Agregar" HeaderStyle-Width="4%">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnAddIndicador" runat="server" OnClick="btnAddIndicador_Click"
                                                CommandName="AddIndicador" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                                                CssClass="btn btn-success btn-sm" ToolTip="Agregar Indicador de Aprendizaje"
                                                OnClientClick="return confirm('¿Desea agregar indicadores?');">
                                                <span><i class="fa fa-plus"></i></span>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="descripcion_ind" HeaderText="Descripción" />
                                    <asp:BoundField DataField="peso_ind" HeaderText="Peso" HeaderStyle-Width="4%" />
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Acción" HeaderStyle-Width="8%">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnEditIndicador" runat="server" OnClick="btnEditIndicador_Click"
                                                CommandName="EditIndicador" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                                                CssClass="btn btn-primary btn-sm" Visible='<%# IIf(Eval("codigo_ind")>-1,True,False) %>'
                                                OnClientClick="return confirm('¿Desea editar indicadores?');">
                                                <span><i class="fa fa-pen"></i></span>
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="btnDeleteIndicador" runat="server" CommandName="DeleteIndicador"
                                                CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" CssClass="btn btn-danger btn-sm"
                                                Visible='<%# IIf(Eval("codigo_ind")>-1,True,False) %>' OnClientClick="return confirm('¿Desea eliminar indicador?');">
                                                <span><i class="fa fa-trash"></i></span>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Agregar" HeaderStyle-Width="4%">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnAddEvidencia" runat="server" OnClick="btnAddEvidencia_Click"
                                                CommandName="AddEvidencia" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                                                CssClass="btn btn-success btn-sm" ToolTip="Agregar Evidencia de Evaluación" Visible='<%# IIf(Eval("codigo_ind")>-1,True,False) %>'
                                                OnClientClick="return confirm('¿Desea agregar evidencia?');">
                                                <span><i class="fa fa-plus"></i></span>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="descripcion_evi" HeaderText="Descripción" />
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Acción" HeaderStyle-Width="8%">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnEditEvidencia" runat="server" OnClick="btnEditEvidencia_Click"
                                                CommandName="EditEvidencia" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                                                CssClass="btn btn-primary btn-sm" Visible='<%# IIf(Eval("codigo_evi")>-1,True,False) %>'
                                                OnClientClick="return confirm('¿Desea editar evidencia?');">
                                                <span><i class="fa fa-pen"></i></span>
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="btnDeleteEvidencia" runat="server" CommandName="DeleteEvidencia"
                                                CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" CssClass="btn btn-danger btn-sm"
                                                Visible='<%# IIf(Eval("codigo_evi")>-1,True,False) %>' OnClientClick="return confirm('¿Desea eliminar evidencia?');">
                                                <span><i class="fa fa-trash"></i></span>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Agregar" HeaderStyle-Width="4%">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnAddInstrumento" runat="server" OnClick="btnAddInstrumento_Click"
                                                CommandName="AddInstrumento" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                                                CssClass="btn btn-success btn-sm" ToolTip="Agregar Instrumento de Evaluación"
                                                Visible='<%# IIf(Eval("codigo_evi")>-1,True,False) %>' OnClientClick="return confirm('¿Desea agregar instrumento?');">
                                                <span><i class="fa fa-plus"></i></span>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="descripcion_ins" HeaderText="Descripción" />
                                    <asp:BoundField DataField="peso_ins" HeaderText="Peso" HeaderStyle-Width="4%" />
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Acción" HeaderStyle-Width="8%">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnEditInstrumento" runat="server" OnClick="btnEditInstrumento_Click"
                                                CommandName="EditInstrumento" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                                                CssClass="btn btn-primary btn-sm" Visible='<%# IIf(Eval("codigo_ins")>-1,True,False) %>'
                                                OnClientClick="return confirm('¿Desea editar instrumento?');">
                                                <span><i class="fa fa-pen"></i></span>
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="btnDeleteInstrumento" runat="server" CommandName="DeleteInstrumento"
                                                CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" CssClass="btn btn-danger btn-sm"
                                                Visible='<%# IIf(Eval("codigo_ins")>-1,True,False) %>' OnClientClick="return confirm('¿Desea eliminar instrumento?');">
                                                <span><i class="fa fa-trash"></i></span>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
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
        </div>
    </div>
    <!-- Modal Registro Indicadores de Aprendizaje -->
    <div id="myModal" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content" id="modalFinalizaBody">
                <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                    <h4 class="modal-title">
                        Registro de Indicadores de Aprendizaje</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label class="col-md-4">
                                    Descripción:
                                </label>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtIndicador" runat="server" CssClass="form-control input-sm" TextMode="MultiLine"
                                        Rows="5"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label class="col-md-4">
                                    Porcentaje:
                                </label>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtPorcentaje" runat="server" CssClass="form-control input-sm" placeholder="0.00"
                                        onkeypress="return soloNumeros(event)"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <label class="col-md-4">
                                Método Promedio para Instrumento:
                            </label>
                            <div class="col-md-4">
                                <asp:DropDownList ID="cboTipoProm2" runat="server" CssClass="form-control input-sm">
                                    <asp:ListItem Value="1">Simple</asp:ListItem>
                                    <asp:ListItem Value="2">Ponderado</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" id="btnsalir" class="btn btn-danger" data-dismiss="modal">
                        Cancelar</button>
                    <asp:Button ID="btnGrabarInd" runat="server" Text="Guardar" class="btn btn-success" />
                </div>
            </div>
        </div>
    </div>
    <!-- Modal Registro Evidencia de Aprendizaje -->
    <div id="myModalEvi" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content" id="Div2">
                <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                    <h4 class="modal-title">
                        Registro de Evidencia de Aprendizaje</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label class="col-md-4">
                                    Descripción:
                                </label>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtEvidencia" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" id="btnsalir" class="btn btn-danger" data-dismiss="modal">
                        Cancelar</button>
                    <asp:Button ID="btnGrabarEvi" runat="server" Text="Guardar" class="btn btn-success" />
                </div>
            </div>
        </div>
    </div>
    <!-- Modal Registro Instrumento de Aprendizaje -->
    <div id="myModalIns" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content" id="Div3">
                <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                    <h4 class="modal-title">
                        Registro de Instrumento de Aprendizaje</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label class="col-md-4">
                                    Descripción:
                                </label>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtInstrumento" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label class="col-md-4">
                                    Porcentaje:
                                </label>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtPorcenIns" runat="server" CssClass="form-control input-sm" placeholder="0.00"
                                        onkeypress="return soloNumeros(event)"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" id="btnsalir" class="btn btn-danger" data-dismiss="modal">
                        Cancelar</button>
                    <asp:Button ID="btnGrabarIns" runat="server" Text="Guardar" class="btn btn-success" />
                </div>
            </div>
        </div>
    </div>
    <!-- Modal Registro Resultado de Aprendizaje -->
    <div id="myModalRes" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content" id="Div4">
                <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                    <h4 class="modal-title">
                        Registro de Resultado de Aprendizaje</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label class="col-md-4">
                                    Descripción:
                                </label>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtResultado" runat="server" CssClass="form-control input-sm" TextMode="MultiLine"
                                        Rows="8"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label class="col-md-4">
                                    Porcentaje:
                                </label>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtPorcenRes" runat="server" CssClass="form-control input-sm" placeholder="00"
                                        onKeyPress="return soloNumeros(event)"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <label class="col-md-4">
                                Método Promedio para Indicador:
                            </label>
                            <div class="col-md-4">
                                <asp:DropDownList ID="cboTipoProm" runat="server" CssClass="form-control input-sm">
                                    <asp:ListItem Value="1">Simple</asp:ListItem>
                                    <asp:ListItem Value="2">Ponderado</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" id="btnsalir" class="btn btn-danger" data-dismiss="modal">
                        Cancelar</button>
                    <asp:Button ID="btnGrabarRes" runat="server" Text="Guardar" class="btn btn-success" />
                </div>
            </div>
        </div>
    </div>
    <!-- Modal Registro Importacion  -->
    <div id="myModalImp" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content" id="Div5">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                    <h4 class="modal-title">
                        Importar Configuración</h4>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="panModal" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:GridView ID="gvImportar" runat="server" Width="99%" AutoGenerateColumns="false"
                                        ShowHeader="true" AllowPaging="True" PageSize="10" DataKeyNames="codigo_pes, codigo_Cur"
                                        CssClass="table table-bordered">
                                        <Columns>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Selec">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSelect" runat="server" Checked='<%# Eval("selec") %>' AutoPostBack="true"
                                                        OnCheckedChanged="chkSelect_CheckedChanged" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="nombre" HeaderText="Plan de Estudio" />
                                            <asp:BoundField DataField="nombre_Cur" HeaderText="Curso" />
                                            <asp:BoundField DataField="ciclo_Cur" HeaderText="Ciclo" />
                                            <asp:BoundField DataField="creditos_Cur" HeaderText="Crédito" />
                                        </Columns>
                                        <EmptyDataTemplate>
                                            No se encontraron Datos!
                                        </EmptyDataTemplate>
                                        <HeaderStyle BackColor="#D9534F" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center"
                                            Font-Size="11px" />
                                        <RowStyle Font-Size="10px" />
                                        <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                        <FooterStyle Font-Bold="True" ForeColor="White" />
                                        <PagerStyle ForeColor="#003399" HorizontalAlign="Center" CssClass="pagination-ys" />
                                    </asp:GridView>
                                    <asp:HiddenField ID="hdselec" runat="server" />
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                    <button type="button" id="btnsalir" class="btn btn-danger" data-dismiss="modal">
                        Cancelar</button>
                    <asp:Button ID="btnGrabarImp" runat="server" Text="Guardar" class="btn btn-success" />
                </div>
            </div>
        </div>
    </div>
    <%--<asp:HiddenField ID="hdpeso_res" runat="server" />
    <asp:HiddenField ID="hdpeso_ind" runat="server" />--%>
    </form>
</body>
</html>
