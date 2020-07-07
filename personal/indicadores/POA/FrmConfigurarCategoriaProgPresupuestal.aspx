<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmConfigurarCategoriaProgPresupuestal.aspx.vb"
    Inherits="FrmConfigurarCategoriaProgPresupuestal" Title="Untitled Page" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Registro de Grados y titulos acádemicos del Personal USAT</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <link rel='stylesheet' href='../../assets/css/bootstrap.min.css?x=1' />
    <%-- ======================= Fecha y Hora =============================================--%>
    <link href="../../assets/css/font-awesome-datetimepicker.min.css" rel="stylesheet"
        type="text/css" />
    <link href="../../assets/css/bootstrap-datetimepicker.css" rel="stylesheet" type="text/css" />
    <link href="../../assets/font-awesome-4.7.0/css/font-awesome.css" rel="stylesheet"
        type="text/css" />

    <script type="text/javascript" src='../../assets/js/jquery.js'></script>

    <script src="../../assets/js/bootstrap.min.js" type="text/javascript"></script>

    <%-- ======================= Inicio Notificaciones =============================================--%>

    <script type="text/javascript" src="../../assets/js/noty/jquery.noty.js"></script>

    <script type="text/javascript" src='../../assets/js/noty/layouts/top.js'></script>

    <script type="text/javascript" src='../../assets/js/noty/layouts/default.js'></script>

    <script type="text/javascript" src="../../assets/js/noty/notifications-custom.js"></script>

    <script type="text/javascript">

        function fnMensaje(typ, msje) {
            var n = noty({
                text: msje,
                type: typ,
                timeout: 3000,
                modal: false,
                dismissQueue: true,
                theme: 'defaultTheme'

            });
        }
    </script>

    <style type="text/css">
        table th
        {
            text-align: center;
        }
        .form-group
        {
            margin: 6px;
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
    </style>
</head>
<body>
    <div class="container-fluid">
        <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="panel panel-primary">
            <div class="panel-heading">
                <b>Configurar Categorias con Programa Presupuestal </b>
            </div>
            <div class="panel-body">
                <asp:UpdatePanel runat="server" ID="updListaConfiguracion" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="form-horizontal">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server" CssClass="col-sm-2 col-md-1 control-label">Categoria</asp:Label>
                                <div class="col-sm-3 col-md-4">
                                    <asp:DropDownList runat="server" ID="ddlCategoria" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                                <asp:Label ID="Label14" runat="server" CssClass="col-sm-2 col-md-2 control-label">Programa Presupuestal</asp:Label>
                                <div class="col-sm-3 col-md-4">
                                    <asp:DropDownList runat="server" ID="ddlProgramaPresupuestal" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-2 col-md-1">
                                    <asp:Button runat="server" ID="btnGuardarRegistro" CssClass="btn btn-sm btn-success"
                                        Text="Agregar" />
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="form-group">
                            <asp:GridView runat="server" ID="gvConfiguracion" CssClass="table table-condensed"
                                DataKeyNames="codigo_cpp" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:BoundField DataField="nombre_cap" HeaderText="CATEGORIA" HeaderStyle-Width="45%" />
                                    <asp:BoundField DataField="descripcion_ppr" HeaderText="PROGRAMA PRESUPUESTAL" HeaderStyle-Width="45%" />
                                    <asp:TemplateField HeaderText="" HeaderStyle-Width="3%" ItemStyle-VerticalAlign="Middle"
                                        ShowHeader="false">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnEliminar" runat="server" Text='<span class="fa fa-trash"></span>'
                                                CssClass="btn btn-danger btn-sm" ToolTip="Eliminar" CommandName="Eliminar" OnClientClick="return confirm('¿Esta seguro que desea eliminar el registro?')"
                                                CommandArgument='<%#Convert.ToString(Container.DataItemIndex)%>'>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle Font-Size="12px" Font-Bold="true" BackColor="#D9534F" ForeColor="white" />
                                <RowStyle Font-Size="12px" />
                                <EmptyDataTemplate>
                                    <b>No cuenta con configuraciones registradas</b>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnGuardarRegistro" EventName="click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
        </form>
    </div>
</body>
</html>
