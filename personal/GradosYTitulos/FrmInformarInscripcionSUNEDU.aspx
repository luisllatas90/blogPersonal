<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmInformarInscripcionSUNEDU.aspx.vb"
    Inherits="GradosYTitulos_FrmInformarInscripcionSUNEDU" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Autorizar Trámite de Titulo a Estudiante</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta http-equiv="Pragma" content="no-cache" />
    <link href="../assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/css/material.css" rel="stylesheet" type="text/css" />
    <link rel='stylesheet' href='../assets/css/style.css?x=1' />

    <script src="../assets/js/jquery.js" type="text/javascript"></script>

    <script src="../assets/js/bootstrap.min.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">


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
    </style>
</head>
<body>
    <div class="wrapper">
        <div class="content">
            <form id="form1" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <div class="page_header">
                <div class="pull-left">
                    <i class="icon ti-check page_header_icon"></i><span class="main-text">Informar Inscripción
                        de Diploma en SUNEDU</span>
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
                <div class="row">
                    <label class="col-md-2 control-label ">
                        Sesion de Consejo</label>
                    <div class="col-md-3">
                        <asp:DropDownList runat="server" class="form-control" ID="cboSesion">
                            <asp:ListItem Value="">--Seleccione--</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <label class="col-md-2 control-label">
                        Tipo Denominación</label>
                    <div class="col-md-5">
                        <asp:DropDownList runat="server" class="form-control" ID="cboTipoDenominacion">
                            <asp:ListItem Value="">--Seleccione--</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                    <label class="col-md-2 control-label ">
                        DNI / Apellidos y Nombres</label>
                    <div class="col-md-3">
                        <asp:TextBox runat="server" ID="txtBusqueda" class="form-control" placeholder="DNI / Apellidos y Nombres"></asp:TextBox>
                    </div>
                    <div class="col-md-6">
                    </div>
                    <div class="col-md-1">
                        <asp:Button runat="server" ID="btnBuscar" Text="Buscar" CssClass="btn btn-primary"
                            OnClick="btnBuscar_Click" />
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="panel panel-piluku" id="PanelLista">
                    <div class="panel-body">
                        <div class="row">
                            <asp:UpdatePanel ID="Upd2" runat="server">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-md-12" id="divMensaje" runat="server">
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <%--<asp:AsyncPostBackTrigger ControlID="btnAutorizar" EventName="Click" />--%>
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                                    <ContentTemplate>
                                        <div class="piluku-preloader text-center hidden" id="divLoading" runat="server">
                                            <!--<div class="progress">
                <div class="indeterminate"></div>
                </div>-->
                                            <div class="loader">
                                                Loading...</div>
                                        </div>
                                        <div style="text-align: right; color: Black;">
                                            <b>
                                                <asp:Label runat="server" ID="lblContadorSeleccionado" Font-Bold="true" ForeColor="blue"></asp:Label>
                                                <asp:Label runat="server" ID="lblcontador" Font-Bold="true"></asp:Label>
                                            </b>
                                        </div>
                                        <asp:GridView runat="server" ID="gvAlumnos" DataKeyNames="descripcion_tdg,codigo_egr,email_alu" AutoGenerateColumns="False"
                                            CssClass=" table table-responsive">
                                            <Columns>
                                                <asp:BoundField HeaderText="TIPO DENOMINACIÓN" DataField="descripcion_tdg" ItemStyle-Width="20%">
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="DENOMINACIÓN" DataField="descripcion_dgt" ItemStyle-Width="32%">
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="EGRESADO" DataField="egresado" ItemStyle-Width="30%">
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="email" DataField="email_alu" ItemStyle-Width="9%">
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="ENVIADO" DataField="enviado" ItemStyle-Width="5%"></asp:BoundField>
                                                <asp:TemplateField ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkall" AutoPostBack="true" OnCheckedChanged="chckchanged" runat="server" /></HeaderTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkAutorizar" runat="server" EnableViewState="true" Checked="false"
                                                            AutoPostBack="true" OnCheckedChanged="lnkContar_Click" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle BackColor="#E33439" ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <EmptyDataTemplate>
                                                No se Encontraron Registros
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="row" >
                            <asp:Button runat="server" ID="btnEnviarCorreo" Text="Enviar Correo" OnClick="btnEnviarCorreo_Click" OnClientClick="return confirm('¿Está seguro que desea Enviar Confirmación a Egresados Seleccionados?.')"
                                CssClass="btn btn-success" />
                        </div>
                    </div>
                </div>
            </div>
            </form>
        </div>
    </div>
</body>
</html>
