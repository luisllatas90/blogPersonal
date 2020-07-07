<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmConfigurarCargoTipoFuncion.aspx.vb"
    Inherits="FrmConfigurarCargoTipoFuncion" Title="Untitled Page" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Configurar Cargo y Tipo de Función</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <link href="../assets/bootstrap-4.4.1-dist/css/bootstrap.min.css" rel="stylesheet"
        type="text/css" />
    <link href="../assets/font-awesome-4.7.0/css/font-awesome.css" rel="stylesheet" type="text/css" />

    <script src="../assets/bootstrap-4.4.1-dist/js/jquery-3.4.1.slim.min.js" type="text/javascript"></script>

    <script src="../assets/bootstrap-4.4.1-dist/js/popper.min.js" type="text/javascript"></script>

    <script src="../assets/bootstrap-4.4.1-dist/js/bootstrap.min.js" type="text/javascript"></script>

    <%-- ======================= Lista desplegable con busqueda =============================================--%>
    <link href="../assets/bootstrap-select-1.13.1/css/bootstrap-select.css" rel="stylesheet"
        type="text/css" />

    <script src="../assets/bootstrap-select-1.13.1/js/bootstrap-select.js" type="text/javascript"></script>

    <script type="text/javascript">
        /*$(document).ready(function() {
        Calendario();
        })
        function MostrarModal(div) {
        $("#" + div).modal('show');
        }
       
        function Calendario() {
        $('#datetimepicker1').datetimepicker({
        locale: 'es',
        format: 'L'
                
        });           
        }*/
        function initCombo(id) {
        var options = {
        noneSelectedText: '-- Seleccione --',
        };

            $('#' + id).selectpicker(options);
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
                <b>Configurar Cargo</b>
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
                    <label for="ddlCargo" class="col-sm-1 col-form-label-sm">
                        Cargo</label>
                    <div class="col-sm-6">
                        <asp:DropDownList runat="server" ID="ddlCargo" CssClass="form-control form-control-sm selectpicker"
                            AutoPostBack="true" data-live-search="true">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="form-group row">
                    <label for="ddlCargo" class="col-sm-1 col-form-label-sm">
                        Requiere</label>
                    <div class="col-sm-3">
                        <div class="form-check">
                            <asp:CheckBox runat="server" ID="chkDepartamento" CssClass="form-check-input" />
                            <label class="form-check-label" for="chkDepartamento">
                                Departamento Académico</label>
                        </div>
                    </div>
                    <div class="col-sm-2">
                        <div class="form-check">
                            <asp:CheckBox runat="server" ID="chkFacultad" CssClass="form-check-input" />
                            <label class="form-check-label" for="chkFacultad">
                                Facultad</label>
                        </div>
                    </div>
                    <div class="col-sm-2">
                        <div class="form-check">
                            <asp:CheckBox runat="server" ID="chkEscuela" CssClass="form-check-input" />
                            <label class="form-check-label" for="chkEscuela">
                                Escuela</label>
                        </div>
                    </div>
                    <div class="col-sm-2">
                        <div class="form-check">
                            <asp:CheckBox runat="server" ID="chkPOA" CssClass="form-check-input" />
                            <label class="form-check-label" for="chkPOA">
                                POA</label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="card">
            <div class="card-header">
                <b>Aplicación y Funciones</b>
            </div>
            <div class="card-body">
                <div class="form-group row">
                    <label for="ddlAplicacion" class="col-sm-1 col-form-label-sm">
                        Aplicación</label>
                    <div class="col-sm-4">
                        <asp:DropDownList runat="server" ID="ddlAplicacion" CssClass="form-control form-control-sm selectpicker"
                            data-live-search="true">
                        </asp:DropDownList>
                    </div>
                    <label for="ddlTipoFuncion" class="col-sm-1 col-form-label-sm">
                        Función</label>
                    <div class="col-sm-5">
                        <asp:DropDownList runat="server" ID="ddlTipoFuncion" CssClass="form-control form-control-sm selectpicker"
                            data-live-search="true">
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-1">
                        <asp:Button runat="server" ID="btnAgregar" CssClass="btn btn-sm btn-primary" Text="Agregar" />
                    </div>
                </div>
                <asp:GridView runat="server" ID="gvConfiguracion" CssClass="table table-condensed"
                    DataKeyNames="codigo_caf" AutoGenerateColumns="false">
                    <Columns>
                        <asp:TemplateField HeaderText="N°" HeaderStyle-Width="5%">
                            <ItemTemplate>
                                <%#Container.DataItemIndex + 1%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="descripcion_apl" HeaderText="Módulo/Aplicación" HeaderStyle-Width="40%" />
                        <asp:BoundField DataField="descripcion_tfu" HeaderText="Tipo de Función" HeaderStyle-Width="40%" />
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
                    <EmptyDataRowStyle Font-Size="11px" Font-Bold="false" />
                    <EmptyDataTemplate>
                        <b>No se encontraron configuraciones asignadas al cargo</b>
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
        </div>
        <div class="card">
            <div class="card-header">
                <b>Colaboradores</b>
            </div>
            <div class="card-body">
                <asp:GridView runat="server" ID="gvPersonal" CssClass="table table-condensed" DataKeyNames="codigo_per"
                    AutoGenerateColumns="false">
                    <Columns>
                        <asp:TemplateField HeaderText="N°" HeaderStyle-Width="5%">
                            <ItemTemplate>
                                <%#Container.DataItemIndex + 1%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Personal" HeaderText="Personal" HeaderStyle-Width="95%" />
                    </Columns>
                    <HeaderStyle Font-Size="12px" Font-Bold="true" BackColor="#D9534F" ForeColor="white" />
                    <RowStyle Font-Size="12px" />
                    <EmptyDataRowStyle Font-Size="11px" Font-Bold="false" />
                    <EmptyDataTemplate>
                        <b>No se encontraron colaboradores asignados al cargo</b>
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
        </div>
        </form>
    </div>
</body>
</html>
