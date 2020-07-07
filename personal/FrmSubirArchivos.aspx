<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmSubirArchivos.aspx.vb"
    Inherits="FrmSubirArchivos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta http-equiv="Pragma" content="no-cache" />
    <link href="assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/css/material.css" rel="stylesheet" type="text/css" />
    <link rel='stylesheet' href='assets/css/style.css?x=1' />

    <script src="assets/js/jquery.js" type="text/javascript"></script>

    <script type="text/javascript">
        function DescargarArchivo(IdArchivo, tk) {
            window.open("DescargarArchivo.aspx?Id=" + IdArchivo + "&tk=" + tk, 'ta', "");
        }


    </script>

    <style type="text/css">
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
    </style>
</head>
<body>
    <div class="wrapper">
        <div class="content">
            <div class="page_header">
                <div class="pull-left">
                    <i class="icon ti-import page_header_icon"></i><span class="main-text">Subir Archivos</span>
                    <p class="text">
                        Subir Multiples Archivos con Shared Files.
                    </p>
                </div>
                <div class="right pull-right">
                    <%-- <ul class="right_bar">
                        <li class="list-unstyled"><i class="icon ion-checkmark text-primary"></i>&nbsp;Headings</li>
                        <li class="list-unstyled"><i class="icon ion-checkmark text-primary"></i>&nbsp;Inline
                            Text Elements</li>
                        <li class="list-unstyled"><i class="icon ion-checkmark text-primary"></i>&nbsp;alignment
                            Classes</li>
                        <li class="list-unstyled"><i class="icon ion-checkmark text-primary"></i>&nbsp;List
                            Types &amp; Groups</li>
                        <li class="list-unstyled"><i class="icon ion-checkmark text-primary"></i>&nbsp;and more...</li>
                    </ul>--%>
                </div>
            </div>
            <div class="panel panel-piluku" id="PanelLista">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        Subir Archivos
                        <%--                <span class="panel-options"><a class="panel-refresh"
                    href="#"> <i class="icon ti-reload" onclick="">
                    </i></a><a class="panel-minimize" href="#"><i class="icon ti-angle-up"></i></a>
                </span>--%>
                    </h3>
                </div>
                <div class="panel-body">
                    <form id="form1" runat="server">
                    <div class="row">
                        <label class="col-md-2 control-label">
                            Nombre</label>
                        &nbsp;<div class="col-md-2">
                            <asp:TextBox ID="txtnombre" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group">
                            <label id="lbl1" class="col-md-2 control-label">
                                Adjuntar</label>
                            <div class="col-md-4">
                                <asp:FileUpload ID="files" runat="server" CssClass="form-control" multiple="multiple" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-8" id="divMensaje" runat="server">
                        </div>
                    </div>
                    <div class="row">
                        <center>
                            <asp:Button ID="btnSubir" runat="server" Text="Subir Archivos" CssClass="btn btn-success"
                                OnClick="SubirButton_Click" />
                        </center>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:GridView runat="server" ID="gvArchivos" AutoGenerateColumns="False" CssClass="table-responsive"
                                DataKeyNames="ID,token">
                                <Columns>
                                    <asp:BoundField HeaderText="codigo_ast" DataField="codigo_ast"></asp:BoundField>
                                    <asp:BoundField DataField="Nombre" HeaderText="Nombre Archivo"></asp:BoundField>
                                    <asp:BoundField HeaderText="Fecha" DataField="Fecha"></asp:BoundField>
                                    <asp:BoundField HeaderText="Ruta" DataField="Ruta"></asp:BoundField>
                                </Columns>
                                <HeaderStyle BackColor="#E33439" ForeColor="White" />
                                <EmptyDataTemplate>
                                    No se Encontraron Registros
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </div>
                    </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
