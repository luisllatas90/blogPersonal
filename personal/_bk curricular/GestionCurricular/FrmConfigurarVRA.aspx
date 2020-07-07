<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmConfigurarVRA.aspx.vb"
    Inherits="GestionCurricular_FrmConfigurarVRA" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta name="google" value="notranslate">
    <title>Configuración General</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=7' />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <link href="../scripts/css/bootstrap.css" rel="Stylesheet" type="text/css" />
    <link href="../assets/css/bootstrap-datepicker3.css" rel="Stylesheet" type="text/css" />
    <link href="../assets/fontawesome-5.2/css/regular.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/bootstrap-colorpicker-2.5.2/css/bootstrap-colorpicker.min.css"
        rel="stylesheet" type="text/css" />
    <link href="../assets/smart-tab/styles/smart_tab.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../assets/js/jquery.js"></script>

    <script type="text/javascript" src="js/popper.js"></script>

    <script type="text/javascript" src="../assets/smart-tab/js/jquery.smartTab.min.js"></script>

    <script type="text/javascript" src="../assets/js/bootstrap.min.js"></script>

    <script type="text/javascript" src="../assets/js/bootstrap-datepicker.js"></script>

    <script type="text/javascript" src="../assets/fontawesome-5.2/js/all.min.js"></script>

    <script type="text/javascript" src="../assets/bootstrap-colorpicker-2.5.2/js/bootstrap-colorpicker.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            $('#tabs').smartTab({ selected: 0, saveState: true, autoProgress: false, stopOnFocus: true, transitionEffect: 'slide', transitionEasing: 'easeInOutExpo', keyNavigation: false });
   
            $('#<%=gvCorte.ClientID%> input[data-provide="datepicker"]').on('change', function(ev) {
                $(this).datepicker('hide');
            });

            $("#<%=gvNivelLogro.ClientID%> tr .colorpicker-component").each(function(index) {
                $(this).colorpicker({
                    colorSelectors: {
                        'black': '#000000',
                        'white': '#ffffff',
                        'red': '#FF0000',
                        'default': '#777777',
                        'primary': '#337ab7',
                        'success': '#5cb85c',
                        'info': '#5bc0de',
                        'warning': '#f0ad4e',
                        'danger': '#d9534f'
                    }
                });

                var $input = $(this).find('input');
                if ($input.prop('disabled')) {
                    $(this).colorpicker('disable');
                }
            });
        });

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
    </script>

    <style>
        .no-border
        {
            border: 0;
            box-shadow: none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" method="post" autocomplete="off">
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release">
    </asp:ScriptManager>
    <div class="container-fluid">
        <div class="messagealert" id="alert_container">
        </div>
        <br />
        <div class="panel panel-default" id="pnlLista" runat="server">
            <div class="panel panel-heading">
                <h4>
                    Configuración General</h4>
            </div>
            <div class="panel panel-body">
                <!-- Tabs -->
                <div id="tabs" runat="server">
                    <ul>
                        <li id="liCorte" runat="server"><a href="#tabs-1"><small><b>MOMENTOS (CORTES)</b></small>
                        </a></li>
                        <li id="liCondicion" runat="server"><a href="#tabs-2"><small><b>CONDICIÓN ESTUDIANTE</b></small>
                        </a></li>
                        <li id="liLogro" runat="server"><a href="#tabs-3"><small><b>NIVEL DE LOGRO</b></small>
                        </a></li>
                    </ul>
                    <asp:UpdatePanel ID="updCorte" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                        <ContentTemplate>
                            <div id="tabs-1">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label class="col-md-2">
                                                Semestre:</label>
                                            <div class="col-md-4">
                                                <asp:DropDownList ID="ddlSemestre" runat="server" CssClass="form-control input-sm"
                                                    AutoPostBack="true">
                                                </asp:DropDownList>
                                            </div>
                                            <label class="col-md-2">
                                                Tipo de Corte:</label>
                                            <div class="col-md-4">
                                                <asp:DropDownList ID="ddlTipoCorte" runat="server" CssClass="form-control input-sm"
                                                    AutoPostBack="true">
                                                    <asp:ListItem Value="P">PARA DOCENTES</asp:ListItem>
                                                    <asp:ListItem Value="D">PARA DIRECTORES</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:GridView ID="gvCorte" runat="server" AutoGenerateColumns="false" GridLines="None"
                                            OnRowEditing="gvCorte_RowEditing" ShowHeadersWhenNoRecords="True" DataKeyNames="codigo_cor, numeroSemana_cor"
                                            CssClass="table table-sm table-bordered table-hover" ShowFooter="True" ShowHeader="True">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Semana" HeaderStyle-Width="25%" ItemStyle-Width="25%"
                                                    FooterStyle-Width="25%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSemana" runat="server" Text='<%#Eval("nombre_sem")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:DropDownList ID="ddlSemana" runat="server" CssClass="form-control form-control-sm"
                                                            data-size="10" OnSelectedIndexChanged="ddlNewSemana_SelectedIndexChanged" AutoPostBack="true">
                                                        </asp:DropDownList>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:DropDownList ID="ddlNewSemana" runat="server" CssClass="form-control form-control-sm"
                                                            data-size="10" OnSelectedIndexChanged="ddlNewSemana_SelectedIndexChanged" AutoPostBack="true">
                                                        </asp:DropDownList>
                                                    </FooterTemplate>
                                                    <ItemStyle Wrap="True" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Fecha de Corte" HeaderStyle-Width="25%" ItemStyle-Width="25%"
                                                    FooterStyle-Width="25%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFecha" runat="server" Text='<%#Eval("fecha")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtFecha" runat="server" CssClass="form-control input-sm" Text='<%#Eval("fecha")%>'
                                                            data-provide="datepicker" placeholder="dd/mm/aaaa"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtNewFecha" runat="server" CssClass="form-control input-sm" data-provide="datepicker"></asp:TextBox>
                                                    </FooterTemplate>
                                                    <ItemStyle Wrap="True" />
                                                </asp:TemplateField>
                                                <asp:BoundField HtmlEncode="false" DataField="descripcion_cor" HeaderText="Número de Corte"
                                                    HeaderStyle-Width="38%" ItemStyle-Width="38%" FooterStyle-Width="38%" ReadOnly="true" />
                                                <asp:TemplateField HeaderText="Acción" HeaderStyle-Width="12%" ItemStyle-Width="12%"
                                                    FooterStyle-Width="12%">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LinkButton1" Text='<i class="fa fa-pen"></i>' ToolTip="Editar"
                                                            runat="server" CommandName="Edit" CssClass="btn btn-primary btn-sm" />
                                                        <span onclick="return confirm('¿Está seguro de eliminar esta Fecha de Corte?')">
                                                            <asp:LinkButton ID="LinkButton2" Text='<i class="fa fa-trash"></i>' ToolTip="Eliminar"
                                                                runat="server" OnClick="OnDeleteCorte" CssClass="btn btn-danger btn-sm" />
                                                        </span>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:LinkButton ID="LinkButton3" Text='<i class="fa fa-save"></i>' ToolTip="Actualizar"
                                                            runat="server" OnClick="OnUpdateCorte" CssClass="btn btn-success btn-sm" />
                                                        <asp:LinkButton ID="LinkButton4" Text='<i class="fa fa-times"></i>' ToolTip="Cancelar"
                                                            runat="server" OnClick="OnCancelCorte" CssClass="btn btn-info btn-sm" />
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="LinkButton5" Text='<i class="fa fa-plus"></i>' ToolTip="Agregar"
                                                            runat="server" CommandName="New" OnClick="OnNewCorte" CssClass="btn btn-success btn-sm" />
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                No se encontraron Cortes de Semana
                                            </EmptyDataTemplate>
                                            <HeaderStyle BackColor="#D9534F" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center"
                                                Font-Size="12px" />
                                            <RowStyle Font-Size="11px" />
                                            <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                        </asp:GridView>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="messagealert" id="divAlertModal" runat="server" visible="false">
                                            <div id="alert_div_modal" style="margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999;"
                                                class="alert alert-info">
                                                <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a> <span
                                                    id="lblMensaje" runat="server">Se recomienda que la última semana de corte también
                                                    sea seleccionada.</span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div id="tabs-2">
                        <div class="row">
                            <div class="col-lg-12">
                                <asp:GridView ID="gvCondicion" runat="server" AutoGenerateColumns="false" GridLines="None"
                                    OnRowEditing="gvCondicion_RowEditing" ShowHeadersWhenNoRecords="True" DataKeyNames="codigo_con, tipo, tipoCondicion_con, descripcion_con"
                                    CssClass="table table-sm table-bordered table-hover" ShowFooter="True" ShowHeader="True">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Tipo de Condición" HeaderStyle-Width="20%" ItemStyle-Width="20%"
                                            FooterStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTipoCon" runat="server" Text='<%#Eval("tipoCondicion_con")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlTipoCon" runat="server" CssClass="form-control form-control-sm"
                                                    data-size="10">
                                                    <asp:ListItem Value="1">CONDICIÓN INTERNA</asp:ListItem>
                                                    <asp:ListItem Value="2">CONDICIÓN EXTERNA</asp:ListItem>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:DropDownList ID="ddlNewTipoCon" runat="server" CssClass="form-control form-control-sm"
                                                    data-size="10">
                                                    <asp:ListItem Value="1">CONDICIÓN INTERNA</asp:ListItem>
                                                    <asp:ListItem Value="2">CONDICIÓN EXTERNA</asp:ListItem>
                                                </asp:DropDownList>
                                            </FooterTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Descripción" HeaderStyle-Width="68%" ItemStyle-Width="68%"
                                            FooterStyle-Width="68%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDescripcionCon" runat="server" Text='<%#Eval("descripcion_con")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtDescripcionCon" runat="server" CssClass="form-control input-sm"
                                                    Text='<%#Eval("descripcion_con")%>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtNewDescripcionCon" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                            </FooterTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Acción" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="12%"
                                            ItemStyle-Width="12%" FooterStyle-Width="12%">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton1" Text='<i class="fa fa-pen"></i>' ToolTip="Editar"
                                                    runat="server" CommandName="Edit" CssClass="btn btn-primary btn-sm" />
                                                <span onclick="return confirm('¿Está seguro de eliminar esta Condición del Estudiante?');">
                                                    <asp:LinkButton ID="LinkButton2" Text='<i class="fa fa-trash"></i>' ToolTip="Eliminar"
                                                        runat="server" OnClick="OnDeleteCondicion" CssClass="btn btn-danger btn-sm" />
                                                </span>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="LinkButton3" Text='<i class="fa fa-save"></i>' ToolTip="Actualizar"
                                                    runat="server" OnClick="OnUpdateCondicion" CssClass="btn btn-success btn-sm" />
                                                <asp:LinkButton ID="LinkButton4" Text='<i class="fa fa-times"></i>' ToolTip="Cancelar"
                                                    runat="server" OnClick="OnCancelCondicion" CssClass="btn btn-info btn-sm" />
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:LinkButton ID="LinkButton5" Text='<i class="fa fa-plus"></i>' ToolTip="Agregar"
                                                    runat="server" CommandName="New" OnClick="OnNewCondicion" CssClass="btn btn-success btn-sm" />
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        No se encontraron Condiciones del Estudiante
                                    </EmptyDataTemplate>
                                    <HeaderStyle BackColor="#D9534F" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center"
                                        Font-Size="12px" />
                                    <RowStyle Font-Size="11px" />
                                    <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <div id="tabs-3">
                        <div class="row">
                            <div class="col-md-12">
                                <asp:GridView ID="gvNivelLogro" runat="server" AutoGenerateColumns="false" GridLines="None"
                                    OnRowEditing="gvNivelLogro_RowEditing" ShowHeadersWhenNoRecords="True" DataKeyNames="codigo_niv, rangoDesde_niv, rangoHasta_niv"
                                    CssClass="table table-sm table-bordered table-hover" ShowFooter="True" ShowHeader="True">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Rango Desde" HeaderStyle-Width="12%" ItemStyle-Width="12%"
                                            FooterStyle-Width="12%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDesde" runat="server" Text='<%#Eval("rangoDesde_niv")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlDesde" runat="server" CssClass="form-control form-control-sm"
                                                    data-size="10">
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:DropDownList ID="ddlNewDesde" runat="server" CssClass="form-control form-control-sm"
                                                    data-size="10">
                                                </asp:DropDownList>
                                            </FooterTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rango Hasta" HeaderStyle-Width="12%" ItemStyle-Width="12%"
                                            FooterStyle-Width="12%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblHasta" runat="server" Text='<%#Eval("rangoHasta_niv")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlHasta" runat="server" CssClass="form-control form-control-sm"
                                                    data-size="10">
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:DropDownList ID="ddlNewHasta" runat="server" CssClass="form-control form-control-sm"
                                                    data-size="10">
                                                </asp:DropDownList>
                                            </FooterTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="¿Logro esperado?" HeaderStyle-Width="5%" ItemStyle-Width="5%"
                                            FooterStyle-Width="5%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                            FooterStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkLblLogro" runat="server" Checked='<%#Eval("clasificacion_niv")%>'
                                                    Enabled="false"></asp:CheckBox>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:CheckBox ID="chkLogro" runat="server" Checked='<%#Eval("clasificacion_niv")%>'>
                                                </asp:CheckBox>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:CheckBox ID="chkNewLogro" runat="server"></asp:CheckBox>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Nombre del Rango" HeaderStyle-Width="16%" ItemStyle-Width="16%"
                                            FooterStyle-Width="16%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNombre" runat="server" Text='<%#Eval("nombre_niv")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control input-sm" Text='<%#Eval("nombre_niv")%>'
                                                    Style="text-transform: uppercase"></asp:TextBox>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtNewNombre" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                            </FooterTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Descripción del Rango" HeaderStyle-Width="28%" ItemStyle-Width="28%"
                                            FooterStyle-Width="28%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDescripcion" runat="server" Text='<%#Eval("descripcion_niv")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control input-sm"
                                                    Text='<%#Eval("descripcion_niv")%>' Rows="3" TextMode="MultiLine"></asp:TextBox>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtNewDescripcion" runat="server" CssClass="form-control input-sm"
                                                    Rows="3" TextMode="MultiLine"></asp:TextBox>
                                            </FooterTemplate>
                                            <ItemStyle Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Color" HeaderStyle-Width="15%" ItemStyle-Width="15%"
                                            FooterStyle-Width="15%">
                                            <ItemTemplate>
                                                <div id="cp2" class="input-group colorpicker-component">
                                                    <input id="lblColor" runat="server" class="form-control input-sm" value='<%#Eval("color_niv")%>'
                                                        disabled="disabled" />
                                                    <span class="input-group-addon input-sm" disabled="disabled"><i></i></span>
                                                </div>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <div id="cp2" class="input-group colorpicker-component">
                                                    <input id="txtColor" runat="server" class="form-control input-sm" value='<%#Eval("color_niv")%>' />
                                                    <span class="input-group-addon input-sm"><i></i></span>
                                                </div>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <div id="cp2" class="input-group colorpicker-component">
                                                    <input id="txtNewColor" runat="server" class="form-control input-sm" value='#000000' />
                                                    <span class="input-group-addon input-sm"><i></i></span>
                                                </div>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Acción" HeaderStyle-Width="12%" ItemStyle-Width="12%"
                                            FooterStyle-Width="12%">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton1" Text='<i class="fa fa-pen"></i>' ToolTip="Editar"
                                                    runat="server" CommandName="Edit" CssClass="btn btn-primary btn-sm" />
                                                <span onclick="return confirm('¿Está seguro de eliminar este Nivel de Logro?')">
                                                    <asp:LinkButton ID="LinkButton2" Text='<i class="fa fa-trash"></i>' ToolTip="Eliminar"
                                                        runat="server" OnClick="OnDeleteNivel" CssClass="btn btn-danger btn-sm" />
                                                </span>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="LinkButton3" Text='<i class="fa fa-save"></i>' ToolTip="Actualizar"
                                                    runat="server" OnClick="OnUpdateNivel" CssClass="btn btn-success btn-sm" />
                                                <asp:LinkButton ID="LinkButton4" Text='<i class="fa fa-times"></i>' ToolTip="Cancelar"
                                                    runat="server" OnClick="OnCancelNivel" CssClass="btn btn-info btn-sm" />
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:LinkButton ID="LinkButton5" Text='<i class="fa fa-plus"></i>' ToolTip="Agregar"
                                                    runat="server" CommandName="New" OnClick="OnNewNivel" CssClass="btn btn-success btn-sm" />
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        No se encontraron Niveles de Logro
                                    </EmptyDataTemplate>
                                    <HeaderStyle BackColor="#D9534F" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center"
                                        Font-Size="12px" />
                                    <RowStyle Font-Size="11px" />
                                    <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
