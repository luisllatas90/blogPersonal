<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmConfigTiempoAccion.aspx.vb" Inherits="GestionEgresado_SustentacionTesis_FrmConfigTiempoAccion" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Configuración</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta http-equiv="Pragma" content="no-cache" />
    <link href="../../assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../../assets/css/material.css" rel="stylesheet" type="text/css" />
    <link rel='stylesheet' href='../../assets/css/style.css?x=1' />

    <script src="../../assets/js/jquery.js" type="text/javascript"></script>

    <script src="../../assets/js/app.js" type="text/javascript"></script>

    <script type="text/javascript" src='../../assets/js/jquery-ui-1.10.3.custom.min.js'></script>

    <script src="../../assets/js/bootstrap.min.js" type="text/javascript"></script>

    <link href="../../assets/font-awesome-4.7.0/css/font-awesome.css" rel="stylesheet"  type="text/css" />
    <%-- ======================= Lista desplegable con busqueda =============================================--%>
    <link href="../../assets/bootstrap-select-1.13.1/css/bootstrap-select.css" rel="stylesheet"
        type="text/css" />

    <script type="text/javascript" src='../../assets/js/jquery.js'></script>

    <script src="../../assets/js/bootstrap.min.js" type="text/javascript"></script>

    <%-- ======================= Inicio Notificaciones =============================================--%>

    <script type="text/javascript" src="../../assets/js/noty/jquery.noty.js"></script>

    <script type="text/javascript" src='../../assets/js/noty/layouts/top.js'></script>

    <script type="text/javascript" src='../../assets/js/noty/layouts/default.js'></script>

    <script type="text/javascript" src="../../assets/js/noty/notifications-custom.js"></script>

    <%-- ======================= Lista desplegable con busqueda =============================================--%>

    <script src="../../assets/bootstrap-select-1.13.1/js/bootstrap-select.js" type="text/javascript"></script>
    
    <script type="text/javascript">

        function fnMensaje(typ, msje) {
            var n = noty({
                text: msje,
                type: typ,
                timeout: 5000,
                modal: false,
                dismissQueue: true,
                theme: 'defaultTheme'

            });
        }
    </script>
    
    <style type="text/css">            
        body
        {
            padding-right: 0 !important;
        }
        .modal-open
        {
            overflow: inherit;
        }
        .form table th
        {
            text-align: center;
        }
        .form-group
        {
            margin: 6px;
        }
        .form-control
        {
            color: Black;
        }
        .bootstrap-select .dropdown-toggle .filter-option
        {
            position: relative;
            padding-top: 0px;
            padding-bottom: 0px;
            padding-left: 0px;
        }
        .dropdown-menu open
        {
            min-width: 0px;
            max-width: 500px;
        }
        .table > thead > tr > th
        {
            color: White;
            font-size: 12px;
            font-weight: bold;
            text-align: center;
        }
        .table > tbody > tr > td
        {
            color: black;
            vertical-align: middle;
        }
        .table tbody tr th
        {
            color: White;
            font-size: 11px;
            font-weight: bold;
            text-align: center;
        }
        #datetimepicker1 a
        {
            color: #337ab7;
            font-weight: bold;
            vertical-align: middle;
        }
    </style>
    
</head>
<body>
    <div class="container-fluid">
        <form id="form1" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server">                
            </asp:ScriptManager>
            <div class="panel panel-default">
                <div class="panel-heading" style="background-color: #E33439; color: White; font-weight: bold;
                    font-size: 14px;">
                    <b>Configuración</b>                    
                </div>     
                <div class="panel-body">
                    <div runat="server" id="lblmensaje"> </div>
                    <div class="form-horizontal">
                        <div class="form-group">
                            <asp:Label ID="Label1" runat="server" CssClass="col-sm-1 col-md-1 control-label" style="text-align:left">Concepto:</asp:Label>
                            <div class="col-sm-5 col-md-5">
                                <div class="col-sm-12 col-md-12">
                                    <asp:TextBox runat="server" ID="txtDescripcion_cta" ReadOnly="false" CssClass="form-control" Text="">
                                    </asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="form-group">
                            <asp:Label ID="Label4" runat="server" CssClass="col-sm-6 col-md-6 control-label" style="text-align:left">Validación: <hr /></asp:Label>
                            <asp:Label ID="Label5" runat="server" CssClass="col-sm-6 col-md-6 control-label" style="text-align:left">Mensaje: <hr /></asp:Label>
                            <%--<asp:Label ID="Label5" runat="server" CssClass="col-sm-6 col-md-6 control-label" style="text-align:left"><hr /></asp:Label>--%>
                        </div> 
                                                   
                        <div class="form-group">
                            <asp:Label ID="Label2" runat="server" CssClass="col-sm-1 col-md-1 control-label">Dato:</asp:Label>
                            <div class="col-sm-2 col-md-2">
                                <div class="col-sm-12 col-md-12">
                                    <asp:TextBox runat="server" ID="txtTiempo_cta" ReadOnly="false" CssClass="form-control" Text="">
                                    </asp:TextBox>
                                </div>
                            </div>
                            <asp:Label ID="Label3" runat="server" CssClass="col-sm-1 col-md-1 control-label">Tipo:</asp:Label>
                            <div class="col-sm-2 col-md-2">
                                <div class="col-sm-12 col-md-12">
                                    <asp:TextBox runat="server" ID="txtTipoTiempo_cta" ReadOnly="true" CssClass="form-control" Text="">
                                    </asp:TextBox>
                                </div>
                            </div>
                            
                             <asp:Label ID="Label6" runat="server" CssClass="col-sm-1 col-md-1 control-label">Dato:</asp:Label>
                            <div class="col-sm-2 col-md-2">
                                <div class="col-sm-12 col-md-12">
                                    <asp:TextBox runat="server" ID="txtMensajeDato" ReadOnly="false" CssClass="form-control" Text="">
                                    </asp:TextBox>
                                </div>
                            </div>
                            <asp:Label ID="Label7" runat="server" CssClass="col-sm-1 col-md-1 control-label">Tipo:</asp:Label>
                            <div class="col-sm-2 col-md-2">
                                <div class="col-sm-12 col-md-12">
                                    <asp:TextBox runat="server" ID="txtMensajeTipo" ReadOnly="true" CssClass="form-control" Text="">
                                    </asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6"> <hr /> </div>
                            <div class="col-md-6"> <hr /> </div>
                        </div>
                   </div>
                   <br />
                   <div class="form-group">
                        <asp:Button runat="server" ID="btnActualizar" CssClass="btn btn-primary" Text="Actualizar" />
                   </div>
                   <br />
                   <div class="form-group">
                       <asp:HiddenField ID="hfCodigo_cta" runat="server" />
                                                        
                        <asp:GridView runat="server" ID="gvConfiguracion" CssClass="table table-condensed" DataKeyNames="codigo_cta"
                                    AutoGenerateColumns="false">
                                    <Columns>                                    
                                        <asp:BoundField DataField="descripcion_cta" HeaderText="CONCEPTO" HeaderStyle-Width="50%" />
                                        <asp:BoundField DataField="datoValidacion_cta" HeaderText="DATO" HeaderStyle-Width="10%" />
                                        <asp:BoundField DataField="unidadValidacion_cta" HeaderText="UNIDAD" HeaderStyle-Width="10%" />
                                        <asp:BoundField DataField="datoMensaje_cta" HeaderText="DATO" HeaderStyle-Width="10%" />
                                        <asp:BoundField DataField="unidadMensaje_cta" HeaderText="UNIDAD" HeaderStyle-Width="10%" />
                                        <asp:TemplateField HeaderText="" HeaderStyle-Width="5%" ShowHeader="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnEditar" runat="server" Text='<span class="fa fa-pencil-square-o"></span>'
                                                    CssClass="btn btn-info btn-sm btn-radius" ToolTip="Editar configuración" CommandName="Editar"
                                                    CommandArgument='<%#Convert.ToString(Container.DataItemIndex)%>'>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle Font-Size="11px" Font-Bold="true" BackColor="#D9534F" ForeColor="white" />
                                    <RowStyle Font-Size="12px" />
                                    <EmptyDataTemplate>
                                        <b>No se encontró información</b>
                                    </EmptyDataTemplate>
                        </asp:GridView>
                   </div>
                </div>
            </div>
        </form>
    </div>
</body>
</html>
