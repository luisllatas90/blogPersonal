<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmActualizarBeca.aspx.vb"
    Inherits="FrmActualizarBeca" Title="Untitled Page" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Altas/Bajas Alumno Pronabec</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <link href="../assets/bootstrap-4.4.1-dist/css/bootstrap.min.css" rel="stylesheet"
        type="text/css" />
        
    <link href="../assets/font-awesome-4.7.0/css/font-awesome.css" rel="stylesheet" type="text/css" />

    <script src="../assets/js/jquery.js" type="text/javascript"></script>

    <script src="../assets/bootstrap-4.4.1-dist/js/popper.min.js" type="text/javascript"></script>

    <script src="../assets/bootstrap-4.4.1-dist/js/bootstrap.min.js" type="text/javascript"></script>

    <%-- ======================= Inicio Notificaciones =============================================--%>

    <script type="text/javascript" src="../assets/js/noty/jquery.noty.js"></script>

    <script type="text/javascript" src='../assets/js/noty/layouts/top.js'></script>

    <script type="text/javascript" src='../assets/js/noty/layouts/default.js'></script>

    <script type="text/javascript" src="../assets/js/noty/notifications-custom.js"></script>

    <%-- ======================= Lista desplegable con busqueda =============================================--%>
    <link href="../assets/bootstrap-select-1.13.1/css/bootstrap-select.css" rel="stylesheet"
        type="text/css" />

    <script src="../assets/bootstrap-select-1.13.1/js/bootstrap-select.js" type="text/javascript"></script>

    <script type="text/javascript">

        function initCombo(id) {
        var options = {
        noneSelectedText: '-- Seleccione --',
        };

            $('#' + id).selectpicker(options);
        }
        
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
        /*table th
        {
            text-align: center;
        }
        .form-group
        {
            margin: 6px;
        }*/.table td, .table th
        {
            padding: 4px;
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
        <div class="card">
            <div class="card-header">
                <b>Altas/Bajas Alumno Pronabec</b>
            </div>
        </div>
        <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="card">
            <%--<div class="card-header">
                Featured
            </div>--%>
            <div class="card-body">
                <div class="form-group row">
                    <label for="ddlCarrera" class="col-sm-4 col-md-2 col-form-label-sm">
                        Carrera Profesional</label>
                    <div class="col-sm-8 col-md-8">
                        <asp:DropDownList runat="server" ID="ddlCarrera" CssClass="form-control form-control-sm selectpicker"
                            AutoPostBack="true" data-live-search="true">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
        </div>
        <div class="card">
            <div class="card-header">
                <b>Alumnos con Beneficio</b>
            </div>
            <div class="card-body">
                <asp:UpdatePanel runat="server" ID="updAgregar" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="form-group row">
                            <label for="txtCodigo" class="col-sm-3 col-md-2 col-form-label-sm">
                                Código Universitario</label>
                            <div class="col-sm-3 col-md-2">
                                <asp:TextBox runat="server" ID="txtCodigo" CssClass="form-control form-control-sm"></asp:TextBox>
                            </div>
                            <label for="ddlCicloAcademico" class="col-sm-3 col-md-2 col-form-label-sm">
                                Desde el Ciclo Académico
                            </label>
                            <div class="col-sm-3 col-md-2">
                                <asp:DropDownList runat="server" ID="ddlCicloAcademico" CssClass="form-control form-control-sm selectpicker"
                                    data-live-search="true">
                                </asp:DropDownList>
                            </div>
                            <div class="col-sm-3 col-md-2">
                                <asp:Button runat="server" ID="btnAgregar" CssClass="btn btn-sm btn-primary" Text="Agregar Beneficio" />
                            </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnAgregar" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <asp:UpdatePanel ID="updListado" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:GridView runat="server" ID="gvAlumnos" CssClass="table table-condensed" DataKeyNames="codigo_alu,codigouniver_alu"
                        AutoGenerateColumns="false">
                        <Columns>
                            <asp:TemplateField HeaderText="N°" HeaderStyle-Width="3%">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex + 1%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="codigouniver_alu" HeaderText="Código Universitario" HeaderStyle-Width="10%"
                                HeaderStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="nroDocIdent_Alu" HeaderText="DNI" HeaderStyle-Width="8%"
                                HeaderStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Alumno" HeaderText="Alumno" HeaderStyle-Width="40%" HeaderStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="FechaAlta" HeaderText="Fecha Alta" HeaderStyle-Width="10%"
                                HeaderStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="descripcion_Cac" HeaderText="Ciclo Académico" HeaderStyle-Width="10%"
                                HeaderStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="UsuarioAlta" HeaderText="Usuario Alta" HeaderStyle-Width="10%" />
                            <%--      <asp:BoundField DataField="FechaBaja" HeaderText="Fecha Baja" HeaderStyle-Width="10%" />
                        <asp:BoundField DataField="UsuarioBaja" HeaderText="Usuario Baja" HeaderStyle-Width="10%" />--%>
                            <asp:TemplateField HeaderText="" HeaderStyle-Width="3%" ItemStyle-VerticalAlign="Middle"
                                ShowHeader="false">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnEliminar" runat="server" Text='<span class="fa fa-trash"></span>'
                                        CssClass="btn btn-danger btn-sm" ToolTip="Dar de baja el beneficio" CommandName="Eliminar"
                                        OnClientClick="return confirm('¿Esta seguro que desea dar de baja el beneficio del alumno?')"
                                        CommandArgument='<%#Convert.ToString(Container.DataItemIndex)%>'>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle Font-Size="12px" Font-Bold="true" BackColor="#D9534F" ForeColor="white"
                            HorizontalAlign="Center" />
                        <RowStyle Font-Size="12px" />
                        <EmptyDataRowStyle Font-Size="11px" Font-Bold="false" />
                        <EmptyDataTemplate>
                            <b>No se encontraron Alumnos con Beneficio</b>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlCarrera" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="btnAgregar" />
                    <asp:AsyncPostBackTrigger ControlID="gvAlumnos" EventName="RowCommand" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
    </form> </div>
</body>
</html>
