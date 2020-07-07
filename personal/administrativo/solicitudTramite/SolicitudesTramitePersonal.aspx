<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SolicitudesTramitePersonal.aspx.vb"
    Inherits="_SolicitudesTramitePersonal" %>

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

    <%--<script type="text/javascript" language="JavaScript" src="../../private/jq/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" language="javascript" src="../../private/jq/jquery.mascara.js"></script>--%>

    <script type="text/javascript" language="javascript">
        $(document).ready(function() {
	        jQuery(function($) {
	            $("#TxtFechaNac").mask("99/99/9999"); //.mask("(999)-999999");
	            //   $("#txttelefono").mask("(999)-9999999");
	            //   $("#txtcelular").mask("(999)-9999999");  
	        });

	    })
        function MarcarEvaluadores(obj)
        {
           //asignar todos los controles en array
            var arrChk = document.getElementsByTagName('input');
            for (var i = 0 ; i < arrChk.length ; i++)
            {
                var chk = arrChk[i];
                //verificar si es Check
                if (chk.type == "checkbox")
                {
                    chk.checked = obj.checked;
                    if (chk.id!=obj.id){  
                    
                        PintarFilaMarcada(chk.parentNode.parentNode,obj.checked)
                         }
                }
            }
        }
               
        function PintarFilaMarcada(obj,estado)
        {
            if (estado==true){
                obj.style.backgroundColor="#FFE7B3"
            }
            else{
                obj.style.backgroundColor="white"
            }
        }       
         if(top.location==self.location)
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
        .table-responsive th
        {
            text-align: center;
            padding: 6px 5px 6 px;
        }
        .table tr td CheckBox
        {
            padding-left: 3px;
            padding-right: 3px;
        }
    </style>
    <link rel="stylesheet" href="../js/jquery-ui.css" />

    <script src="../js/jquery-1.9.1.js" type="text/javascript"></script>

    <script type="text/javascript" src="../js/jquery-ui.js?x=1"></script>

    <script src="../../javascript/DateTime/Scripts/jquery.dynDateTime.min.js" type="text/javascript"></script>

    <script src="../../javascript/DateTime/Scripts/calendar-en.min.js" type="text/javascript"></script>

    <link href="../../javascript/DateTime/Styles/calendar-blue.css" rel="stylesheet"
        type="text/css" />

    <script src="../js/jquery.ui.datepicker-es.js?xq=3" type="text/javascript"></script>

    <script type="text/javascript">
        $(function() {
            $.datepicker.setDefaults($.datepicker.regional["es"]);
            $("#txtFechaInicio").datepicker({
                firstDay: 1
            });
            $("#txtFechaFin").datepicker({
                firstDay: 1
            });
        });
               
    </script>

</head>
<body>
    <div class="wrapper">
        <div class="content">
            <form id="Form1" runat="server" name="frm">
            <div class="page_header">
                <div class="pull-left">
                    <i class="icon ion-ios-list-outline  page_header_icon"></i><span class="main-text">Lista
                        de Solicitudes de
                        <asp:Label ID="lblTipo_Solic" runat="server"></asp:Label>
                        - Evaluación Área de Personal</span>
                    <p class="text">
                        Solicitudes de
                        <asp:Label ID="lblTipo_Solic1" runat="server"></asp:Label>
                        de los Colaboradores
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
                                        Font-Bold="true"></asp:Label>
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
                                        <asp:ListItem Value="AD" Selected="True">Pendientes</asp:ListItem>
                                        <asp:ListItem Value="AP">Aprobados Personal</asp:ListItem>
                                        <asp:ListItem Value="RE">Rechazados</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <label class="col-md-2 control-label">
                                    Tipo de Solicitud :</label>
                                <div class="col-md-2">
                                    <asp:DropDownList ID="ddlTipoSolicitud" runat="server" AutoPostBack="True" CssClass="form-control">
                                        <asp:ListItem Value="L" Selected="True">Licencias</asp:ListItem>
                                        <asp:ListItem Value="P">Permisos por Horas</asp:ListItem>
                                        <asp:ListItem Value="V">Vacaciones</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row ">
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
                                    F.Inicio :</label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtFechaInicio" runat="server" AutoPostBack="true" CssClass="form-control"
                                        BackColor="#C9DDF5"></asp:TextBox>
                                </div>
                                <label class="col-md-1 control-label">
                                    F.Fin :</label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtFechaFin" runat="server" AutoPostBack="true" CssClass="form-control"
                                        BackColor="#C9DDF5"></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    <asp:Label ID="lblAviso" class="aviso" Style="color: #CC0000; font-style: oblique"
                                        runat="server" Text="*Seleccione el rango de fechas de envío de las Solicitudes de Trámite. Fechas en que enviaron los colaboradores"></asp:Label>
                                </div>
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
                                <asp:GridView ID="gvCarga" runat="server" AutoGenerateColumns="False" DataKeyNames="codigo_ST,Tipo_Solicitud, Estado, codigo_EST, codigo_Per"
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
                                        <asp:BoundField HeaderText="Número" DataField="codigo_ST">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Tipo_Solicitud" HeaderText="Tipo Solicitud" />
                                        <asp:BoundField DataField="Colaborador" HeaderText="Colaborador" />
                                        <asp:BoundField DataField="Prioridad" HeaderText="Prioridad" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Fecha_Ini" HeaderText="Fecha Inicio Solicitada" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Fecha_Fin" HeaderText="Fecha Final Solicitada" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Adjunto" HeaderText="Adjuntos" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="FechaIni_Auto" HeaderText="Fecha Inicio Autorizada" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="FechaFin_Auto" HeaderText="Fecha Final Autorizada" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Director" HeaderText="Responsable de Área" />
                                        <asp:BoundField DataField="Fecha_Envio" HeaderText="Fecha Envío" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Fecha_Respuesta" HeaderText="Respuesta Responsable" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Aut_Personal" HeaderText="Autoriza por Responsable" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Personal" HeaderText="Evaluación área Personal"
                                            ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="estado" HeaderText="Estado" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="codigo_EST" HeaderText="codigo_EST" Visible="false" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="codigo_Per" HeaderText="codigo_Per" Visible="false" ItemStyle-HorizontalAlign="Center">
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
                            <div class="form-group">
                                <div class="col-md-8">
                                    <asp:Label ID="lblMensaje" runat="server" class="control-label" Text="Label" Style="color: #E33439"
                                        Font-Bold="true"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <center>
                                    <asp:Button ID="btnAnadirObservacion" runat="server" Text="Añadir Observación" CssClass="btn btn-success" />
                                    <asp:Button ID="btnAprobar" runat="server" Text="Evaluar" CssClass="btn btn-success" />
                                    <asp:Button ID="btnRechazar" runat="server" Text="Rechazo Directo" CssClass="btn btn-success" />
                                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar Aprobación" CssClass="btn btn-success" />
                                </center>
                            </div>
                        </div>
                    </div>
                    <div id="divConfirmaRechazar" runat="server">
                        <div class="row">
                        <label style="font-weight: bold">
                            ¿ESTÁ SEGURO QUE DESEA RECHAZAR LA SOLICITUD DE TRÁMITE?</label>
                        <asp:Button ID="btnConfirmaRechazarSI" runat="server" Text="SI" CssClass="btn btn-success" />
                        <asp:Button ID="btnConfirmaRechazarNO" runat="server" Text="NO" CssClass="btn btn-success" />
                        </div> 
                        <div class="row">
                            <div class="form-group">
                                <label class="col-md-2 control-label">
                                    Observación:</label>
                                <div class="col-md-10">
                                    <asp:TextBox ID="txtObservacionPer" runat="server" AutoPostBack="True" MaxLength="1000"
                                        CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="divConfirmaCancelar" runat="server">
                        <label style="font-weight: bold">
                            ¿ESTÁ SEGURO QUE DESEA CANCELAR LA SOLICITUD DE TRÁMITE YA APROBADA? NO PODRÁ REVERTIR
                            LA ACCIÓN.</label>
                        <asp:Button ID="btnConfirmaCancelarSI" runat="server" Text="SI" CssClass="btn btn-success" />
                        <asp:Button ID="btnConfirmaCancelarNO" runat="server" Text="NO" CssClass="btn btn-success" />
                    </div>
                </div>
            </div>
            </form>
        </div>
    </div>
</body>
</html>
