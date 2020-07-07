<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmListarConcurso.aspx.vb"
    Inherits="GestionInvestigacion_FrmListarConcurso" %>

<%@ Register Assembly="BusyBoxDotNet" Namespace="BusyBoxDotNet" TagPrefix="busyboxdotnet" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Listado de Concursos/Convocatorias</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta http-equiv="Pragma" content="no-cache" />
    <link href="../assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/css/material.css" rel="stylesheet" type="text/css" />
    <link rel='stylesheet' href='../assets/css/style.css?x=1' />

    <script src="../assets/js/jquery.js" type="text/javascript"></script>

    <script src="../assets/js/app.js" type="text/javascript"></script>

    <script src="../assets/js/bootstrap.min.js" type="text/javascript"></script>

    <script src="../assets/js/bootstrap-datepicker.js" type="text/javascript"></script>

    <script type="text/javascript">
        function fnDownload(id_ar) {
            window.open("DescargarArchivo.aspx?Id=" + id_ar);
        }
    </script>

    <style type="text/css">
        .content
        {
            margin-left: 0px;
        }
        .page_header
        {
            background-color: #FAFCFF;
        }
        .form-control
        {
            border-radius: 0px;
            box-shadow: none; /*border-color: #718FAB;*/
            height: 30px;
            font-weight: 300;
            color: black;
            border: 1px solid #ccc;
        }
        .row
        {
            margin-right: 0px;
            margin-left: 0px;
            padding: 4px;
            vertical-align: middle;
        }
        .table > tfoot > tr > th, .table > tbody > tr > td, .table > tfoot > tr > td
        {
            color: rgb(51, 51, 51);
            border-color: rgb(51, 51, 51);
        }
        .table > tbody > tr > th, .table > thead > tr > th, .table > thead > tr > td
        {
            color: White;
            text-align: center;
            vertical-align: middle; /*font-weight: bold;*/
        }
        .checkbox label
        {
            padding-left: 1px;
        }
        input[type="checkbox"] + label
        {
            color: Black;
        }
        :-ms-input-placeholder.form-control
        {
            line-height: 0px;
        }
        #UpdateProgress1
        {
            width: 400px;
            background-color: #FFC080;
            bottom: 0%;
            left: 0px;
            position: absolute;
        }
        #mdRegistro
        {
            overflow-x: hidden;
            overflow-y: auto;
        }
    </style>
</head>
<body>
    <div class="wrapper">
        <div class="content">
            <form id="form1" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <div class="page_header">
                <div class="row">
                    <div class="pull-left">
                        <i class="icon ti-bookmark-alt page_header_icon"></i><span class="main-text">Concursos</span>
                    </div>
                </div>
            </div>
            <div class="panel panel-piluku">
                <%--<div class="panel-heading">
                    <h3 class="panel-title">
                        Listado de asesorías
                    </h3>
                </div>--%>
                <div class="panel-body">
                    <div class="row">
                        <label class=" col-sm-2 col-md-2 control-label">
                            Título del Concurso
                        </label>
                        <div class="col-sm-4 col-md-4">
                            <asp:TextBox runat="server" ID="txtbusqueda" CssClass="form-control"></asp:TextBox>
                        </div>
                        <label class="col-sm-1 col-md-1 control-label">
                            Estado
                        </label>
                        <div class="col-sm-3 col-md-3">
                            <asp:DropDownList runat="server" class="form-control" ID="ddlEstado">
                                <asp:ListItem Value="">--Seleccione--</asp:ListItem>
                                <asp:ListItem Value="1" Selected="True">EN PROCESO</asp:ListItem>
                                <asp:ListItem Value="2">CULMINADO</asp:ListItem>
                                <asp:ListItem Value="T">TODOS</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-sm-1 col-md-1">
                            <asp:Button runat="server" ID="btnConsultar" CssClass="btn btn-primary" Text="Consultar" />
                        </div>
                        <div class="col-sm-1 col-md-1">
                            <asp:Button runat="server" ID="btnAgregar" CssClass="btn btn-success" Text="Agregar" />
                        </div>
                    </div>
                    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-sm-12 col-md-12">
                                    <div runat="server" id="lblMensaje">
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <asp:GridView runat="server" ID="gvConcursos" DataKeyNames="codigo_con" AutoGenerateColumns="False"
                                    CssClass="table table-responsive">
                                    <Columns>
                                        <asp:TemplateField HeaderText="N°" HeaderStyle-Width="3%">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex + 1%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Título" DataField="titulo_con" HeaderStyle-Width="46%" />
                                        <asp:BoundField HeaderText="Fecha Inicio" DataField="fechaini_con" HeaderStyle-Width="7%" />
                                        <asp:BoundField HeaderText="Fecha Fin" DataField="fechafin_con" HeaderStyle-Width="7%" />
                                        <asp:BoundField HeaderText="Ámbito" DataField="ambito_con" HeaderStyle-Width="8%" />
                                        <asp:BoundField HeaderText="Tipo" DataField="tipo_con" HeaderStyle-Width="10%" />
                                        <asp:BoundField HeaderText="Estado" DataField="estado_con" HeaderStyle-Width="9%" />
                                        <asp:TemplateField HeaderText="Opciones" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center"
                                            ItemStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <div class="row">
                                                    <asp:LinkButton runat="server" ID="lbEditar" CommandName="EditarConcurso" CommandArgument="<%#CType(Container,GridViewRow).RowIndex%>"
                                                        CssClass="btn btn-info"> <i class="ion-edit"></i></asp:LinkButton>
                                                    <asp:LinkButton runat="server" ID="lbEliminar" CommandName="EliminarConcurso" CommandArgument="<%#CType(Container,GridViewRow).RowIndex%>"
                                                        CssClass="btn btn-danger"> <i class="ion-close"></i></asp:LinkButton>
                                                    <%--     <asp:TextBox runat="server" ID="txtPorcentaje" Width="40px" Height="28px" placeholder="%%"
                                                        Text='<%# Eval("Porcentaje") %>'></asp:TextBox>&nbsp;&nbsp;|&nbsp;&nbsp;
                                                    <asp:TextBox runat="server" ID="txtNota" Width="40px" Height="28px" placeholder="Nota"
                                                        Text='<%# Eval("Nota") %>'></asp:TextBox>
                                                </div>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle BackColor="#E33439" ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle"
                                        Font-Size="12px" />
                                    <RowStyle Font-Size="11px" />
                                    <EmptyDataTemplate>
                                        No se Encontraron Registros
                                    </EmptyDataTemplate>
                                </asp:GridView>
                                <asp:UpdateProgress runat="server" ID="UpdateProgress1" AssociatedUpdatePanelID="UpdatePanel1">
                                    <ProgressTemplate>
                                        <h3>
                                            Actualización en Progreso</h3>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </div>
                            <div class="row">
                                <div class="modal fade" id="mdConfirmacion" runat="server" role="dialog" aria-labelledby="myModalLabel"
                                    style="z-index: 0;" aria-hidden="true" data-backdrop="static" data-keyboard="false">
                                    <div runat="server" id="BloqueoModal" class='modal-backdrop fade in' style='height: 100%;'>
                                    </div>
                                    <div class="modal-dialog modal-lg" id="Div2">
                                        <div class="modal-content">
                                            <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                                                <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right;">
                                                    <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                                                </button>
                                                <h5 class="modal-title" id="H1">
                                                    ¿Desea eliminar el concurso?</h5>
                                            </div>
                                            <div class="modal-footer">
                                                <center>
                                                    <asp:Button runat="server" ID="btnEliminar" Text="SI" CssClass="btn btn-primary" />
                                                    <asp:Button runat="server" ID="btnCancelarEliminar" Text="NO" CssClass="btn btn-danger" />
                                                </center>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="modal fade" id="mdRegistro" runat="server" role="dialog" aria-labelledby="myModalLabel"
                                    style="z-index: 0;" aria-hidden="false" data-backdrop="static" data-keyboard="false">
                                    <div runat="server" id="Div1" class='modal-backdrop fade in' style='height: 100%;'>
                                    </div>
                                    <div class="modal-dialog modal-lg" id="modalReg">
                                        <div class="modal-content">
                                            <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                                                <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right;">
                                                    <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                                                </button>
                                                <h4 class="modal-title" id="myModalLabel3">
                                                    Concurso</h4>
                                            </div>
                                            <div class="modal-body">
                                                <form id="frmRegistro" name="frmRegistro" enctype="multipart/form-data" class="form-horizontal"
                                                method="post" onsubmit="return false;" action="#">
                                                <div class="row">
                                                    <label class="col-sm-3 control-label" for="txttitulo">
                                                        Título:</label>
                                                    <div class="col-sm-8">
                                                        <asp:HiddenField runat="server" ID="cod_con" Value="0" Visible="false" />
                                                        <asp:TextBox runat="server" ID="txtTitulo" CssClass="form-control" MaxLength="500"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <label class="col-sm-3 control-label">
                                                        Breve Descripción:</label>
                                                    <div class="col-md-8">
                                                        <asp:TextBox runat="server" ID="txtDescripcion" MaxLength="800" CssClass="form-control"
                                                            Columns="50" Rows="3" TextMode="MultiLine"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <label class="col-sm-3 control-label">
                                                        Ámbito:</label>
                                                    <div class="col-md-3">
                                                        <asp:DropDownList runat="server" ID="ddlAmbito" CssClass="form-control">
                                                            <asp:ListItem Value="">-- Seleccione --</asp:ListItem>
                                                            <asp:ListItem Value="0">INTERNO</asp:ListItem>
                                                            <asp:ListItem Value="1">EXTERNO</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <label class="col-sm-3 control-label">
                                                        Fecha Inicio Postulación:</label>
                                                    <div class="col-sm-4" id="date-popup-group">
                                                        <div class="input-group date" id="FechaInicio">
                                                            <input name="txtfecini" runat="server" class="form-control" id="txtfecini" style="text-align: right;"
                                                                type="text" placeholder="__/__/____" data-provide="datepicker" autocomplete="off" />
                                                            <span class="input-group-addon sm"><i class="ion ion-ios-calendar-outline" id="FecInicial">
                                                            </i></span>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <label class="col-sm-3 control-label">
                                                        Fecha Fin Postulación:</label>
                                                    <div class="col-sm-4" id="date-popup-group">
                                                        <div class="input-group date" id="FechaFin">
                                                            <input name="txtfecfin" runat="server" class="form-control" id="txtfecfin" style="text-align: right;"
                                                                type="text" placeholder="__/__/____" data-provide="datepicker" autocomplete="off" />
                                                            <span class="input-group-addon sm"><i class="ion ion-ios-calendar-outline" id="FecFinal">
                                                            </i></span>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <label class="col-sm-3 control-label">
                                                        Fecha Fin Evaluación:</label>
                                                    <div class="col-sm-4" id="date-popup-group">
                                                        <div class="input-group date" id="FechaFinEvaluacion">
                                                            <input name="txtfecfineva" runat="server" class="form-control" id="txtfecfineva"
                                                                style="text-align: right;" type="text" placeholder="__/__/____" data-provide="datepicker"
                                                                autocomplete="off" />
                                                            <span class="input-group-addon sm"><i class="ion ion-ios-calendar-outline" id="I1"></i>
                                                            </span>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <label class="col-sm-3 control-label">
                                                        Fecha de Publicación de Resultados:</label>
                                                    <div class="col-sm-4" id="date-popup-group">
                                                        <div class="input-group date" id="FechaResultados">
                                                            <input name="txtfecres" runat="server" class="form-control" id="txtfecres" style="text-align: right;"
                                                                type="text" placeholder="__/__/____" data-provide="datepicker" autocomplete="off" />
                                                            <span class="input-group-addon sm"><i class="ion ion-ios-calendar-outline" id="I2"></i>
                                                            </span>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <label class="col-sm-3 control-label">
                                                        Tipo:</label>
                                                    <div class="col-md-3">
                                                        <asp:DropDownList runat="server" ID="ddlTipo" CssClass="form-control">
                                                            <asp:ListItem Value="">-- Seleccione --</asp:ListItem>
                                                            <asp:ListItem Value="0">INDIVIDUAL</asp:ListItem>
                                                            <asp:ListItem Value="2">GRUPAL</asp:ListItem>
                                                            <asp:ListItem Value="3">INDIVIDUAL/GRUPAL</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <label class="col-sm-3 control-label">
                                                        Dirigido a:</label>
                                                    <div class="col-md-3">
                                                        <asp:DropDownList runat="server" ID="ddlDirigidoA" CssClass="form-control">
                                                            <asp:ListItem Value="">-- Seleccione --</asp:ListItem>
                                                            <asp:ListItem Value="1">ESTUDIANTES</asp:ListItem>
                                                            <asp:ListItem Value="2">DOCENTES</asp:ListItem>
                                                            <asp:ListItem Value="3">AMBOS</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <label class="col-sm-3 control-label">
                                                        De Innovación:</label>
                                                    <div class="col-md-3">
                                                        <asp:CheckBox runat="server" ID="chkInnovacion" Visible="true" />
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <label class="col-sm-3 control-label">
                                                        Bases(PDF)</label>
                                                    <div class="col-sm-8">
                                                        <asp:FileUpload runat="server" ID="archivo" />
                                                        <%--<input type="file" id="fileBases" name="fileBases" />--%>
                                                        <div id="archivo_base" runat="server">
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12 col-md-12">
                                                        <div runat="server" id="lblMensajeRegistro">
                                                        </div>
                                                    </div>
                                                </div>
                                                </form>
                                            </div>
                                            <div class="modal-footer">
                                                <center>
                                                    <asp:Button runat="server" ID="btnGuardar" CssClass="btn btn-primary" Text="Guardar" />
                                                    <asp:Button runat="server" ID="btnCancelar" CssClass="btn btn-danger" Text="Cancelar" />
                                                </center>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <%--<asp:AsyncPostBackTrigger ControlID="ddlEstado" EventName="SelectedIndexChanged" />--%>
                            <asp:AsyncPostBackTrigger ControlID="btnConsultar" />
                            <asp:AsyncPostBackTrigger ControlID="btnAgregar" />
                            <asp:AsyncPostBackTrigger ControlID="gvConcursos" EventName="RowCommand" />
                            <asp:PostBackTrigger ControlID="btnGuardar" />
                            <%--           <asp:AsyncPostBackTrigger ControlID="cboAnio" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="btnGuardar" />--%>
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
            </form>
        </div>
    </div>
</body>
</html>
