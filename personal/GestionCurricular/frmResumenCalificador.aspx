<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmResumenCalificador.aspx.vb"
    Inherits="GestionCurricular_frmResumenCalificador" EnableEventValidation="false" %>

<%@ Register Assembly="BusyBoxDotNet" Namespace="BusyBoxDotNet" TagPrefix="busyboxdotnet" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="google" value="notranslate" />
    <title>Evaluación y monitoreo por asignatura</title>
    <!-- custom scrollbar stylesheet -->
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=7' />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <link href="../scripts/css/bootstrap.css" rel="Stylesheet" type="text/css" />
    <link href="../assets/fontawesome-5.2/css/regular.min.css" rel="stylesheet" type="text/css" />
    <link href="css/paginacion.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../scripts/js/jquery-1.12.3.min.js"></script>

    <script type="text/javascript" src="js/popper.js"></script>

    <script type="text/javascript" src="../scripts/js/bootstrap.min.js"></script>

    <script type="text/javascript" src="../assets/fontawesome-5.2/js/all.min.js"></script>

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

        function openModal(acc) {
            if (acc == '') {
                $('#myModal').modal('show');
            } else {
                $('#myModalContenido').modal('show');
            }
        }

        function closeModal() {
            $('#myModal').modal('hide');
        }

        function txtBuscar_onKeyPress(obj, e) {
            var key;
            if (window.event)
                key = window.event.keyCode; //IE, Chrome
            else
                key = e.which; //firefox

            if (key == 13) {
                var btn = document.getElementById(obj);
                if (btn != null) {
                    //$('#btnBuscar').focus();
                    //$('#btnBuscar').click();
                    btn.click();
                    event.keyCode = 0
                }
            }
        }
    </script>
    
    <style type="text/css">
        .panel {
        	margin-bottom: 0px;
        }
        .panel-heading {
            padding: 5px 7.5px;
        }
        .panel-body {
            padding: 7.5px;
        }
        .badge-success{
        	background-color: #5cb85c;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server" method="post" autocomplete="off">
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release">
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
                    Evaluación y monitoreo por asignatura</h4>
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
                                Carrera Prof:</label>
                            <div class="col-md-8">
                                <asp:DropDownList ID="cboCarrProf" runat="server" CssClass="form-control input-sm"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label class="col-md-4">
                                Estado:</label>
                            <div class="col-md-8">
                                <asp:DropDownList ID="cboEstado" runat="server" CssClass="form-control input-sm"
                                    AutoPostBack="true">
                                    <asp:ListItem Value="" Selected="True">TODOS</asp:ListItem>
                                    <asp:ListItem Value="P">SIN PUBLICAR</asp:ListItem>
                                    <asp:ListItem Value="E">PUBLICADOS</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel panel-body">
                <div class="row">
                    <div class="col-md-7">
                        <asp:Panel ID="panBuscar" runat="server" DefaultButton="btnBuscar" Width="100%">
                            <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control input-sm" placeholder="Ingrese Nombre de la Asignatura"></asp:TextBox>
                        </asp:Panel>
                    </div>
                    <div class="col-md-1">
                        <asp:LinkButton ID="btnBuscar" runat="server" CssClass="btn btn-info">
                            <span><i class="fa fa-search"></i></span>
                        </asp:LinkButton>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <div class="table-responsive">
                            <asp:GridView ID="gvAsignatura" runat="server" AutoGenerateColumns="false" ShowHeader="true"
                                AllowPaging="True" PageSize="20" DataKeyNames="codigo_cup, codigo_cur, codigo_pes, nombre_Cur, codigo_dis, grupoHor_Cup, idcurso_mdl, modular_pcu, cicloRom, estado_sil"
                                OnRowCreated="gvAsignatura_OnRowCreated" OnRowDataBound="gvAsignatura_OnRowDataBound"
                                CssClass="table table-bordered table-hover">
                                <Columns>
                                    <asp:BoundField DataField="cicloRom" HeaderText="Ciclo" />
                                    <asp:BoundField DataField="abreviatura_Cpf" HeaderText="Carr. Prof." />
                                    <asp:BoundField DataField="nombre_Cur" HeaderText="Asignatura" />
                                    <%--<asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Asignatura">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnAulaVirtual" runat="server" CommandName="AulaVirtual" Text='<%# Eval("nombre_Cur") %>'
                                                CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" OnClientClick="return confirm('¿Desea ir al aula virtual?');">
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:BoundField DataField="grupoHor_Cup" HeaderText="Grupo" />
                                    <%--<asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Moodle">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnMoodle" runat="server" CommandName="Moodle" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                                                CssClass="btn btn-warning btn-sm" OnClientClick="return confirm('¿Desea ingresar información?');">
                                            <span><i class="fa fa-link"></i></span>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Promedio">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnGenerar" runat="server" CommandName="Generar" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                                                CssClass="btn btn-primary btn-sm" OnClientClick="return confirm('¿Desea generar el promedio?');">
                                            <span><i class="fa fa-arrow-alt-circle-right"></i></span>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                </Columns>
                                <EmptyDataTemplate>
                                    No se encontraron Datos
                                </EmptyDataTemplate>
                                <HeaderStyle BackColor="#D9534F" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center"
                                    Font-Size="12px" />
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
    <!-- Modal Detalle -->
    <div id="myModal" class="modal fade" role="dialog" role="dialog" aria-labelledby="myModalLabel"
        aria-hidden="true" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg">
            <div class="modal-content" id="modalFinalizaBody">
                <div class="modal-header" style="background-color: #D9534F; color: White; font-weight: bold;
                    font-size: 16px;">
                    <span class="modal-title">Observaciones del curso:
                        <label id="lblTitulo" runat="server">
                        </label>
                    </span>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="updMensaje" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                        <ContentTemplate>
                            <div class="messagealert" id="divAlertModal" runat="server" visible="false">
                                <div id="alert_div_modal" style="margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999;"
                                    class="alert alert-info">
                                    <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a> <span
                                        id="lblMensaje" runat="server"></span>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <br />
                    <asp:UpdatePanel ID="udpDesarrollo" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                        <ContentTemplate>
                            <fieldset class="border p-1">
                                <legend class="w-auto" style="font-size: 16px; font-weight: 600">Modificación al programa
                                    durante el desarrollo</legend>
                                <div class="row">
                                    <div class="col-xs-12">
                                        <asp:GridView ID="gvDesarrollo" runat="server" AutoGenerateColumns="false" GridLines="None"
                                            OnRowEditing="gvDesarrollo_RowEditing" ShowHeadersWhenNoRecords="True" DataKeyNames="codigo_uad"
                                            CssClass="table table-sm table-bordered table-hover" ShowFooter="False" ShowHeader="True">
                                            <Columns>
                                                <asp:BoundField DataField="numero_uni" HeaderText="Unidad" HeaderStyle-Width="5%"
                                                    ItemStyle-Width="5%" FooterStyle-Width="5%" ReadOnly="true" />
                                                <asp:TemplateField HeaderText="Comentarios" HeaderStyle-Width="83%" ItemStyle-Width="83%"
                                                    FooterStyle-Width="83%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblComentario" runat="server" Text='<%#Eval("comentario_uad")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtComentario" runat="server" CssClass="form-control input-sm" Text='<%#Eval("comentario_uad")%>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtNewComentario" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                                    </FooterTemplate>
                                                    <ItemStyle Wrap="True" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Acción" HeaderStyle-Width="12%" ItemStyle-Width="12%"
                                                    FooterStyle-Width="12%">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LinkButton1" Text='<i class="fa fa-pen"></i>' ToolTip="Editar"
                                                            runat="server" CommandName="Edit" CssClass="btn btn-primary btn-sm" />
                                                        <span onclick="return confirm('¿Está seguro de quitar este comentario?')">
                                                            <asp:LinkButton ID="LinkButton2" Text='<i class="fa fa-backspace"></i>' ToolTip="Quitar comentario"
                                                                runat="server" OnClick="OnDeleteDesarrollo" CssClass="btn btn-danger btn-sm" />
                                                        </span>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:LinkButton ID="LinkButton3" Text='<i class="fa fa-save"></i>' ToolTip="Actualizar"
                                                            runat="server" OnClick="OnUpdateDesarrollo" CssClass="btn btn-success btn-sm" />
                                                        <asp:LinkButton ID="LinkButton4" Text='<i class="fa fa-times"></i>' ToolTip="Cancelar"
                                                            runat="server" OnClick="OnCancelDesarrollo" CssClass="btn btn-info btn-sm" />
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                No se encontró detalle
                                            </EmptyDataTemplate>
                                            <HeaderStyle BackColor="#D9534F" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center"
                                                Font-Size="12px" />
                                            <RowStyle Font-Size="12px" />
                                            <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </fieldset>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:UpdatePanel ID="udpDificultad" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                        <ContentTemplate>
                            <fieldset class="border p-1">
                                <legend class="w-auto" style="font-size: 16px; font-weight: 600">Dificultades presentadas</legend>
                                <div class="row">
                                    <div class="col-xs-12">
                                        <asp:GridView ID="gvDificultad" runat="server" AutoGenerateColumns="false" GridLines="None"
                                            OnRowEditing="gvDificultad_RowEditing" ShowHeadersWhenNoRecords="True" DataKeyNames="codigo_udf, codigo_uae, descripcion_uae"
                                            CssClass="table table-sm table-bordered table-hover" ShowFooter="True" ShowHeader="True">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Elemento del Proceso" HeaderStyle-Width="20%" ItemStyle-Width="20%"
                                                    FooterStyle-Width="20%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDescripcion" runat="server" Text='<%#Eval("descripcion_uae")%>'>
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:DropDownList ID="ddlElemento" runat="server" CssClass="form-control form-control-sm">
                                                        </asp:DropDownList>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:DropDownList ID="ddlNewElemento" runat="server" CssClass="form-control form-control-sm">
                                                        </asp:DropDownList>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Comentarios" HeaderStyle-Width="68%" ItemStyle-Width="68%"
                                                    FooterStyle-Width="68%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblComentario" runat="server" Text='<%#Eval("comentario_udf")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtComentario" runat="server" CssClass="form-control input-sm" Text='<%#Eval("comentario_udf")%>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtNewComentario" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                                    </FooterTemplate>
                                                    <ItemStyle Wrap="True" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Acción" HeaderStyle-Width="12%" ItemStyle-Width="12%"
                                                    FooterStyle-Width="12%">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LinkButton1" Text='<i class="fa fa-pen"></i>' ToolTip="Editar"
                                                            runat="server" CommandName="Edit" CssClass="btn btn-primary btn-sm" />
                                                        <span onclick="return confirm('¿Está seguro de eliminar esta dificultad presentada?')">
                                                            <asp:LinkButton ID="LinkButton2" Text='<i class="fa fa-trash"></i>' ToolTip="Eliminar"
                                                                runat="server" OnClick="OnDeleteDificultad" CssClass="btn btn-danger btn-sm" />
                                                        </span>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:LinkButton ID="LinkButton3" Text='<i class="fa fa-save"></i>' ToolTip="Actualizar"
                                                            runat="server" OnClick="OnUpdateDificultad" CssClass="btn btn-success btn-sm" />
                                                        <asp:LinkButton ID="LinkButton4" Text='<i class="fa fa-times"></i>' ToolTip="Cancelar"
                                                            runat="server" OnClick="OnCancelDificultad" CssClass="btn btn-info btn-sm" />
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="LinkButton5" Text='<i class="fa fa-plus"></i>' ToolTip="Agregar"
                                                            runat="server" CommandName="New" OnClick="OnNewDificultad" CssClass="btn btn-success btn-sm" />
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                No se encontró detalle
                                            </EmptyDataTemplate>
                                            <HeaderStyle BackColor="#D9534F" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center"
                                                Font-Size="12px" />
                                            <RowStyle Font-Size="12px" />
                                            <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </fieldset>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <fieldset class="border p-1">
                        <legend class="w-auto" style="font-size: 16px; font-weight: 600">Sugerencias de mejora</legend>
                        <div class="row">
                            <div class="col-xs-12">
                                <asp:TextBox ID="txtSugerencia" runat="server" CssClass="form-control input-sm" Rows="2"
                                    TextMode="MultiLine">
                                </asp:TextBox>
                            </div>
                        </div>
                    </fieldset>
                </div>
                <div class="modal-footer">
                    <button type="button" id="btnCancelar" class="btn btn-danger" data-dismiss="modal">
                        Cancelar</button>
                    <asp:Button ID="btnAceptar" runat="server" Text="Guardar" CssClass="btn btn-success" />
                </div>
            </div>
        </div>
    </div>
    <!-- Modal Detalle -->
    
    <!-- Modal Contenido -->
    <div id="myModalContenido" class="modal fade" role="dialog" role="dialog" aria-labelledby="myModalLabel"
        aria-hidden="true" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg">
            <div class="modal-content" id="Div1">
                <div class="modal-header" style="background-color: #D9534F; color: White; font-weight: bold;
                    font-size: 16px;">
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                    <span class="modal-title">Confirmar contenidos desarrollados del curso:
                        <label id="lblTituloContenido" runat="server">
                        </label>
                    </span>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="udpContenido" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                        <ContentTemplate>
                            <div class="row" id="divSubGrupo" runat="server">
                                <div class="col-xs-4">
                                    <div class="form-group">
                                        <label class="col-xs-5" for="ddlSubGrupo" style="padding: 0px">
                                            Sub Grupo:</label>
                                        <div class="col-xs-4" style="padding: 0px">
                                            <asp:DropDownList ID="ddlSubGrupo" runat="server" CssClass="form-control input-sm"
                                                AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-12">
                                    <asp:GridView ID="gvContenido" runat="server" AutoGenerateColumns="false" GridLines="None"
                                        ShowHeadersWhenNoRecords="True" DataKeyNames="codigo_con, codigo_cdes, numero_uni, descripcion_uni"
                                        CssClass="table table-sm table-bordered table-hover" ShowFooter="False" ShowHeader="True">
                                        <Columns>
                                            <%--<asp:BoundField DataField="numero_uni" HeaderText="Unidad" HeaderStyle-Width="5%"
                                                ItemStyle-Width="5%" FooterStyle-Width="5%" ReadOnly="true" />--%>
                                            <asp:BoundField DataField="sesion" HeaderText="Sesión" HeaderStyle-Width="10%" ItemStyle-Width="10%"
                                                FooterStyle-Width="10%" ReadOnly="true" HtmlEncode="false" />
                                            <asp:BoundField DataField="contenido" HeaderText="Contenido" HeaderStyle-Width="67%"
                                                ItemStyle-Width="67%" FooterStyle-Width="67%" ReadOnly="true" HtmlEncode="false" />
                                            <asp:BoundField DataField="fecha_realizado" HeaderText="Fecha realizado" HeaderStyle-Width="13%"
                                                ItemStyle-Width="13%" FooterStyle-Width="13%" ReadOnly="true" />
                                            <asp:TemplateField HeaderText="¿Realizado?" HeaderStyle-Width="10%" ItemStyle-Width="10%"
                                                FooterStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkRealizado" runat="server" Text='<%# IIF(Eval("realizado_cdes"), " Sí", " No")%>'
                                                        Checked='<%#Eval("realizado_cdes")%>' OnCheckedChanged="OnCheck" AutoPostBack="true" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            No se encontró ningún contenido
                                        </EmptyDataTemplate>
                                        <HeaderStyle BackColor="#D9534F" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center"
                                            Font-Size="12px" />
                                        <RowStyle Font-Size="12px" />
                                        <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                    <button type="button" id="btnSalir" class="btn btn-danger" data-dismiss="modal">
                        Cancelar</button>
                </div>
            </div>
        </div>
    </div>
    <!-- Modal Contenido -->
    </form>
</body>
</html>
