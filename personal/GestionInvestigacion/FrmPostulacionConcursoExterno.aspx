<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmPostulacionConcursoExterno.aspx.vb"
    Inherits="GestionInvestigacion_FrmPostulacionConcursoExterno" %>

<%--<%@ Register Assembly="BusyBoxDotNet" Namespace="BusyBoxDotNet" TagPrefix="busyboxdotnet" %>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Postular Convocatoria Externa</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta http-equiv="Pragma" content="no-cache" />
    <link href="../assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/css/material.css" rel="stylesheet" type="text/css" />
    <link rel='stylesheet' href='../assets/css/style.css?x=1' />

    <script src="../assets/js/jquery.js" type="text/javascript"></script>

    <script type="text/javascript">
        function fnAbrirLoading() {
            console.log("abri");
            $('.piluku-preloader').removeClass('hidden');
            //console.log(sw);
        }
    </script>

    <style type="text/css">
        .row
        {
            margin-right: 0px;
            margin-left: 0px;
            padding: 4px;
        }
        .content .main-content
        {
            padding-right: 15px;
        }
        .content
        {
            margin-left: 0px;
        }
        .form-control
        {
            border-radius: 0px;
            box-shadow: none;
            border-color: #d9d9d9;
            height: 28px;
            font-weight: 300; /* line-height: 40px; */
            color: black;
        }
        .form-group
        {
            margin: 6px;
        }
        .form-horizontal .control-label
        {
            padding-top: 5px;
        }
        /*
        .i-am-new
        {
            z-index: 100;
        }*/.page_header
        {
            background-color: #FAFCFF;
            padding: 15px;
            padding-left: 35px;
        }
        .table > tfoot > tr > th, .table > tbody > tr > td, .table > tfoot > tr > td
        {
            color: Black;
            border-color: Black;
        }
        .table > tbody > tr > th, .table > thead > tr > th, .table > thead > tr > td
        {
            color: White;
            text-align: center;
            vertical-align: middle;
            font-weight: bold;
        }
        .selector-for-some-widget
        {
            box-sizing: content-box;
        }
    </style>
</head>
<body>
    <div class="wrapper">
        <div class="content">
            <form id="form1" runat="server">
           <%-- <busyboxdotnet:BusyBox ID="BusyBox1" runat="server" ShowBusyBox="OnLeavingPage" Image="Clock"
                Text="Su solicitud esta siendo procesada..." Title="Por favor espere" />--%>
            <div class="page_header">
                <div class="pull-left">
                    <i class="icon ti-bookmark-alt page_header_icon"></i><span class="main-text">Postular
                        Convocatoria Externa</span>
                    <%--<p class="text">
                        Autorizar Trámite de Título
                    </p>--%>
                </div>
                <%--<div class="right pull-right">
                    <ul class="right_bar">
                        <li class="list-unstyled"><i class="icon ion-checkmark text-primary"></i>&nbsp;Headings</li>
                        <li class="list-unstyled"><i class="icon ion-checkmark text-primary"></i>&nbsp;Inline
                            Text Elements</li>
                        <li class="list-unstyled"><i class="icon ion-checkmark text-primary"></i>&nbsp;alignment
                            Classes</li>
                        <li class="list-unstyled"><i class="icon ion-checkmark text-primary"></i>&nbsp;List
                            Types &amp; Groups</li>
                        <li class="list-unstyled"><i class="icon ion-checkmark text-primary"></i>&nbsp;and more...</li>
                    </ul>
                </div>--%>
            </div>
            <div class="panel-piluku">
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-6">
                            <label class="col-md-4 control-label ">
                                Titulo de Convocatoria</label>
                            <div class="col-md-8">
                                <input type="hidden" id="hdcod" name="hdcod" value="0" />
                                <input type="text" class="form-control" id="txtBusqueda" name="txtBusqueda" />
                            </div>
                        </div>
                        <div class="col-md-4">
                            <label class="col-md-4 control-label ">
                                Estado
                            </label>
                            <div class="col-md-8">
                                <asp:DropDownList runat="server" ID="cboEstado" CssClass="form-control">
                                    <asp:ListItem Value="">-- Seleccione --</asp:ListItem>
                                    <asp:ListItem Value="1">EN PROCESO</asp:ListItem>
                                    <asp:ListItem Value="2">CULMINADO</asp:ListItem>
                                    <asp:ListItem Value="T">TODOS</asp:ListItem>
                                </asp:DropDownList>
                                <%-- <select name="cboEstado" class="form-control" id="cboEstado">
                                    <option value="">-- Seleccione -- </option>
                                    <option value="1" selected="selected">EN PROCESO</option>
                                    <option value="2">CULMINADO</option>
                                    <option value="T">TODOS</option>
                                </select>--%>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <asp:Button runat="server" ID="btnConsultar" name="btnConsultar" CssClass="btn btn-primary"
                                Text="Consultar" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel-piluku">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        Convocatorias</h3>
                </div>
                <div class="panel-body">
                    <asp:ScriptManager ID="Scriptmanager1" runat="server">
                    </asp:ScriptManager>
                    <asp:UpdateProgress ID="UpdateProgress1" runat="Server" AssociatedUpdatePanelID="UpdatePanel1" >
                        <ProgressTemplate>
                            <asp:Label ID="lblwait" runat="server" Text="Please wait.."></asp:Label>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:GridView runat="server" ID="gvConcursos" DataKeyNames="codigo_con" AutoGenerateColumns="false"
                                        Width="100%" CssClass="table table-responsive">
                                        <Columns>
                                            <%--<asp:BoundField HeaderText="N°" />--%>
                                            <asp:BoundField DataField="titulo_con" HeaderText="Titulo" ItemStyle-Width="50%" />
                                            <asp:BoundField DataField="fechaini_con" HeaderText="Fecha Inicio" ItemStyle-Width="10%" />
                                            <asp:BoundField DataField="fechafin_con" HeaderText="Fecha Fin" ItemStyle-Width="10%" />
                                            <asp:BoundField DataField="tipo_con" HeaderText="Tipo" ItemStyle-Width="15%" />
                                            <asp:TemplateField ItemStyle-Width="15%">
                                                <ItemTemplate>
                                                    <asp:Button runat="server" ID="btnVer" CssClass="btn btn-info " Text="Ver" />
                                                    <asp:Button runat="server" ID="btnPostular" CssClass="btn btn-success" Text="Postular" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle BackColor="#E33439" ForeColor="White" />
                                        <EmptyDataTemplate>
                                            No se Encontraron Registros
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnConsultar" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
            </form>
        </div>
    </div>
</body>
</html>
