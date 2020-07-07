<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TransferirPresupuesto.aspx.vb"
    Inherits="presupuesto_areas_TransferirPresupuesto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta http-equiv="Pragma" content="no-cache" />
    <link rel='stylesheet' href='../../assets/css/bootstrap.min.css?x=1' />
    <link rel='stylesheet' href='../../assets/css/material.css' />
    <link rel='stylesheet' href='../../assets/css/style.css?x=1' />

    <script type="text/javascript" src='../../assets/js/jquery.js'></script>

    <title>Transferir Presupuesto</title>

    <script type="text/javascript">
        function CerrarVentana(texto) {

            window.opener.location = window.opener.location;
            alert(texto)
            window.close();
            return false;
        }

        function Cancelar() {
            window.close();
            return false;
        }
    
    </script>

    <style type="text/css">
        .panel-piluku > .panel-heading
        {
            background-color: #E3F9FF;
        }
        .panel-title
        {
            font-weight: bold;
            color: #3366CC;
            font-size: 14px;
        }
        .row
        {
            margin-right: 0px;
            margin-left: 0px;
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
            margin: 4px;
        }
        /*
        .form-horizontal .control-label
        {
            padding-top: 5px;
        }
        .i-am-new
        {
            z-index: 100;
        }*/.page_header
        {
            background-color: #FAFCFF;
        }
        }</style>
</head>
<body>
    <div class="piluku-preloader text-center hidden">
        <div class="loader">
            Loading...</div>
    </div>
    <div class="wrapper">
        <div class="content">
            <%--<div class="page_header">
                <div class="pull-left">
                    <i class="icon ti-import page_header_icon"></i><span class="main-text">Transferir Presupuesto</span>
                      <p class="text">
                        Transferir Presupuesto a Programa/Proyecto.
                    </p>
                </div>
                <div class="right pull-right">
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
                </div>
            </div>--%>
            <div class="panel panel-piluku" id="PanelLista">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        Transferir Presupuesto
                        <%--                <span class="panel-options"><a class="panel-refresh"
                    href="#"> <i class="icon ti-reload" onclick="">
                    </i></a><a class="panel-minimize" href="#"><i class="icon ti-angle-up"></i></a>
                </span>--%>
                    </h3>
                </div>
                <div class="panel-body">
                    <form id="frmTransferir" runat="server">
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <div class="row">
                        <div class="form-group">
                            <label class="col-md-2 control-label ">
                                Plan Operativo anual (POA)</label>
                            <div class="col col-md-9 col-sm-9">
                                <asp:HiddenField runat="server" ID="hdcodDpr" />
                                <asp:Label runat="server" ID="lblPoa" Font-Bold="true" ForeColor="blue"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group">
                            <label class="col-md-2 control-label ">
                                Programa/Proyecto</label>
                            <div class="col col-md-9">
                                <asp:Label runat="server" ID="lblPrograma" Font-Bold="true" ForeColor="Red"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group">
                            <label class="col-md-2 control-label ">
                                Presupuesto Disponible (S/)</label>
                            <div class="col col-md-5">
                                <asp:Label runat="server" ID="lblpresupuesto" Font-Bold="true" ForeColor="green"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <%--<asp:UpdatePanel ID="UpdatePanel" runat="server">
                        <ContentTemplate>--%>
                    <div class="row">
                        <div class="form-group">
                            <label class="col-md-2 control-label ">
                                Programa/Proyecto Hijo</label>
                            <div class="col col-md-6">
                                <asp:DropDownList runat="server" ID="ddlPrograma" CssClass="form-control" AutoPostBack="true">
                                    <asp:ListItem Value="">--Seleccione--</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <%--</ContentTemplate>
                                            <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddltipoestudio" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>--%>
                    <asp:UpdatePanel ID="UpdatePanel" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <div class="form-group">
                                    <label class="col-md-2 control-label ">
                                        Actividad</label>
                                    <div class="col col-md-6">
                                        <asp:DropDownList runat="server" ID="ddlActividad" CssClass="form-control">
                                            <asp:ListItem Value="">--Seleccione--</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlPrograma" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <div class="row">
                        <div class="form-group">
                            <label class="col-md-2 control-label ">
                                Item</label>
                            <div class="col col-md-6">
                                <asp:DropDownList runat="server" ID="ddlItem" CssClass="form-control">
                                    <asp:ListItem Value="">--Seleccione--</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group">
                            <label class="col-md-2 col-sm-2 control-label ">
                                Monto (S/)</label>
                            <div class="col-md-2 col-sm-2">
                                <asp:TextBox runat="server" ID="txtmonto" CssClass="form-control"></asp:TextBox>
                            </div>
                            <%--             </div>
                    </div>
                    <div class="row">
                        <div class="form-group">--%>
                            <label class="col-md-2 col-sm-2 control-label ">
                                Mes</label>
                            <div class="col-md-3 col-sm-3">
                                <asp:DropDownList runat="server" ID="ddlMes" CssClass="form-control">
                                    <asp:ListItem Value="">--Seleccione--</asp:ListItem>
                                    <asp:ListItem Value="1">ENERO</asp:ListItem>
                                    <asp:ListItem Value="2">FEBRERO</asp:ListItem>
                                    <asp:ListItem Value="3">MARZO</asp:ListItem>
                                    <asp:ListItem Value="4">ABRIL</asp:ListItem>
                                    <asp:ListItem Value="5">MAYO</asp:ListItem>
                                    <asp:ListItem Value="6">JUNIO</asp:ListItem>
                                    <asp:ListItem Value="7">JULIO</asp:ListItem>
                                    <asp:ListItem Value="8">AGOSTO</asp:ListItem>
                                    <asp:ListItem Value="9">SETIEMBRE</asp:ListItem>
                                    <asp:ListItem Value="10">OCTUBRE</asp:ListItem>
                                    <asp:ListItem Value="11">NOVIEMBRE</asp:ListItem>
                                    <asp:ListItem Value="12">DICIEMBRE</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <div id="Mensaje" runat="server">
                                </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlPrograma" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <div class="row">
                    <div class="form-group">
                        <div class="col col-md-12">
                            <center>
                                <%--<asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                        <ProgressTemplate>
                                            Procesando
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>--%>
                                <asp:Button runat="server" ID="btnGuardar" CssClass="btn btn-success" Text="Guardar" />
                                <asp:Button runat="server" ID="btnCancelar" CssClass="btn btn-danger" Text="Cancelar" />
                            </center>
                        </div>
                    </div>
                </div>
                </form>
            </div>
        </div>
    </div>
    </div>
</body>
</html>
