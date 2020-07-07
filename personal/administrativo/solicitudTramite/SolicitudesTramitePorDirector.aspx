<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SolicitudesTramitePorDirector.aspx.vb"
    Inherits="_SolicitudesTramitePorDirector" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta http-equiv="Pragma" content="no-cache" />
    <link href="../../assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link rel='stylesheet' href='../../assets/css/material.css' />
    <link rel='stylesheet' href='../../assets/css/style.css?x=1' />

    <script src="../../assets/js/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
        $(document).ready(function() {
            jQuery(function($) {
                $("#TxtFechaNac").mask("99/99/9999"); //.mask("(999)-999999");
                //   $("#txttelefono").mask("(999)-9999999");
                //   $("#txtcelular").mask("(999)-9999999");  
            });

        })
        function MarcarEvaluadores(obj) {
            //asignar todos los controles en array
            var arrChk = document.getElementsByTagName('input');
            for (var i = 0; i < arrChk.length; i++) {
                var chk = arrChk[i];
                //verificar si es Check
                if (chk.type == "checkbox") {
                    chk.checked = obj.checked;
                    if (chk.id != obj.id) {
                        PintarFilaMarcada(chk.parentNode.parentNode, obj.checked)
                    }
                }
            }
        }
        function PintarFilaMarcada(obj, estado) {
            if (estado == true) {
                obj.style.backgroundColor = "#FFE7B3"
            }
            else {
                obj.style.backgroundColor = "white"
            }
        }

        //if(top.location==self.location)
        //location.href='../../tiempofinalizado.asp'} //El ../ depende de la ruta de la página   
    </script>

    <style type="text/css">
        .row
        {
            margin-right: 1px;
            margin-left: 1px;
        }
        .content .main-content
        {
            padding-right: 15px;
        }
        .content
        {
            margin-left: 1px;
        }
        .form-control
        {
            border-radius: 1px;
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
        .page_header
        {
            background-color: #FAFCFF;
        }
        .table-responsive th
        {
            text-align: center;
            padding: 5;
        }
        .table tr td CheckBox
        {
            padding-left: 3;
            padding-right: 3;
        }
    </style>
</head>
<body>
    <div class="wrapper">
        <div class="content">
            <form id="Form1" runat="server" name="frm">
            <div class="page_header">
                <div class="pull-left">
                    <i class="icon ion-ios-list-outline  page_header_icon"></i><span class="main-text">Solicitudes
                        de Trámite de Colaboradores por Centro de Costos </span>
                    <p class="text">
                        Solicitudes de Trámite de los Colaboradores
                    </p>
                </div>
                <div class="right pull-right">
                </div>
            </div>
            <div class="panel panel-piluku" id="PanelLista">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <%--<span class="panel-options"><a class="panel-refresh"
                    href="#"> <i class="icon ti-reload" onclick="">
                    </i></a><a class="panel-minimize" href="#"><i class="icon ti-angle-up"></i></a>
                    </span>--%>
                    </h3>
                </div>
                <div class="panel-body">
                    <div id="divListado" runat="server">
                        <div class="row">
                            <div class="form-group">
                                <center>
                                    <asp:Label ID="lblMensaje0" runat="server" class="col-md-12 control-label" Style="color: #FF6833"
                                        Font-Bold ="true"></asp:Label>
                                </center>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <label class="col-md-1 control-label">
                                    Año :</label>
                                <div class="col-md-2">
                                    <asp:DropDownList ID="ddlAño" runat="server" AutoPostBack="True" CssClass="form-control">
                                        <%--<asp:ListItem Value="2019" Selected="True">2019</asp:ListItem>
                                        <asp:ListItem Value="2018">2018</asp:ListItem>
                                        <asp:ListItem Value="2017">2017</asp:ListItem>--%>
                                    </asp:DropDownList>
                                </div>
                                <label class="col-md-1 control-label">
                                    Estado :</label>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlEstado" runat="server" AutoPostBack="True" CssClass="form-control">
                                        <asp:ListItem Value="TO">TODOS</asp:ListItem>
                                        <asp:ListItem Value="EN" Selected="True">Pendientes</asp:ListItem>
                                        <asp:ListItem Value="AD">Enviado a Personal</asp:ListItem>
                                        <asp:ListItem Value="RE">Rechazados</asp:ListItem>
                                        <asp:ListItem Value="AP">Aprobados Personal</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <label class="col-md-1 control-label">
                                    Prioridad :</label>
                                <div class="col-md-2">
                                    <asp:DropDownList ID="ddlPrioridad" runat="server" AutoPostBack="True" CssClass="form-control">
                                        <asp:ListItem Value="T" Selected="True">TODOS</asp:ListItem>
                                        <asp:ListItem Value="U">Urgente</asp:ListItem>
                                        <asp:ListItem Value="N">Normal</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <label class="col-md-1 control-label">
                                    Mes :</label>
                                <div class="col-md-2">
                                    <asp:DropDownList ID="ddlMes" runat="server" AutoPostBack="True" CssClass="form-control">
                                        <asp:ListItem Value="0" Selected="True">TODOS</asp:ListItem>
                                        <asp:ListItem Value="1">Enero</asp:ListItem>
                                        <asp:ListItem Value="2">Febrero</asp:ListItem>
                                        <asp:ListItem Value="3">Marzo</asp:ListItem>
                                        <asp:ListItem Value="4">Abril</asp:ListItem>
                                        <asp:ListItem Value="5">Mayo</asp:ListItem>
                                        <asp:ListItem Value="6">Junio</asp:ListItem>
                                        <asp:ListItem Value="7">Julio</asp:ListItem>
                                        <asp:ListItem Value="8">Agosto</asp:ListItem>
                                        <asp:ListItem Value="9">Setiembre</asp:ListItem>
                                        <asp:ListItem Value="10">Octubre</asp:ListItem>
                                        <asp:ListItem Value="11">Noviembre</asp:ListItem>
                                        <asp:ListItem Value="12">Diciembre</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <label class="col-md-1 control-label">
                                    Trámite :</label>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlTipoSolicitud" runat="server" AutoPostBack="True" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                                <label class="col-md-1 control-label">
                                    Trabajador :</label>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtTrabajador" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-1">
                                    <asp:ImageButton runat="server" AlternateText="Buscar" ID="btnBuscar" ToolTip="*Buscar texto.."
                                        ImageUrl="../../images/busca.gif" OnClick="Unnamed1_Click" />
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <label class="col-md-1 control-label">
                                    Director :</label>
                                <div class="col-md-10">
                                    <asp:DropDownList ID="ddlResponsableArea" runat="server" AutoPostBack="True" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                                <%--<label class="col-md-2 control-label">
                                    Centro de Costos:</label>
                                <asp:Label ID="lblCentroCostos" class="col-md-4 control-label" runat="server" Text="---"></asp:Label>--%>
                            </div>
                        </div>
                        <div class="row">
                            <asp:Button ID="btnExporta" runat="server" Text="Exportar" CssClass="btn btn-success" />
                        </div>
                        <div class="row">
                            <p>
                            </p>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <asp:GridView ID="gvCarga" runat="server" AutoGenerateColumns="False" DataKeyNames="codigo_ST, Estado, codigo_EST"
                                    CssClass="table-responsive" Font-Size="9" ForeColor="#333333">
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <Columns>
                                        <asp:TemplateField>
                                            <%--<HeaderTemplate>
                                                <asp:CheckBox ID="chkHeader" runat="server" onclick="MarcarEvaluadores(this)" />
                                            </HeaderTemplate>--%>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBox1" runat="server" ItemStyle-HorizontalAlign="left" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Núm." DataField="codigo_ST">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Tipo_Solicitud" HeaderText="Tipo Solicitud" />
                                        <asp:BoundField DataField="Prioridad" HeaderText="Prioridad" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Colaborador" HeaderText="Colaborador" />
                                        <asp:BoundField DataField="Director" HeaderText="Director de Área"/>
                                        <asp:BoundField DataField="Fecha_Ini" HeaderText="Fecha Inicio Solicitada" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Fecha_Fin" HeaderText="Fecha Final Solicitada" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Adjunto" HeaderText="Adjuntos" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="FechaIni_Auto" HeaderText="Fecha Inicio Autorizada" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="FechaFin_Auto" HeaderText="Fecha Final Autorizada" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Fecha_Envio" HeaderText="Fecha Envío" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Fecha_Respuesta" HeaderText="Respuesta Responsable" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Aut_Personal" HeaderText="Autoriza por Responsable" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Personal" HeaderText="Evaluación área Personal" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Estado" HeaderText="Estado" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="codigo_EST" HeaderText="codigo_EST" Visible="false" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:BoundField>
                                    </Columns>
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <HeaderStyle BackColor="#E33439" ForeColor="White" HorizontalAlign="Center" />
                                    <EditRowStyle BackColor="#999999" />
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <div class="col-md-8">
                                   
                                        <asp:Label ID="lblMensaje" runat="server" class="control-label" Text="Label" Style="color: #E33439"
                                        Font-Bold ="true"></asp:Label>
                                    
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <center>
                                    <asp:Button ID="btnEvaluar" runat="server" Text="Evaluar" CssClass="btn btn-success" />
                                    <asp:Button ID="btnRechazar" runat="server" Text="Rechazo Directo" CssClass="btn btn-success" />
                                </center>
                            </div>
                        </div>
                    </div>
                    <div id="divConfirmaRechazar" runat="server">
                        <label style="font-weight: bold">
                            ¿ESTÁ SEGURO QUE DESEA RECHAZAR LA SOLICITUD DE TRÁMITE?</label>
                        <asp:Button ID="btnConfirmaRechazarSI" runat="server" Text="SI" CssClass="btn btn-success" />
                        <asp:Button ID="btnConfirmaRechazarNO" runat="server" Text="NO" CssClass="btn btn-success" />
                    </div>
                </div>
            </div>
            </form>
        </div>
    </div>
</body>
</html>
