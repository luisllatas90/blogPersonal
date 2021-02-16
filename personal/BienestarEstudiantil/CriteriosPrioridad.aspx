<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CriteriosPrioridad.aspx.vb"
    Inherits="CriteriosPrioridad" %>
    
<%@ Register Assembly="BusyBoxDotNet" Namespace="BusyBoxDotNet" TagPrefix="busyboxdotnet" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta http-equiv="Pragma" content="no-cache" />
    <title>Configuración de Criterios Prioridad</title>
    <link href="../assets/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link rel='stylesheet' href='../assets/css/style.css?x=1' />

    <script type="text/javascript" src='../assets/js/jquery.js'></script>

    <script src="../assets/js/bootstrap.min.js" type="text/javascript"></script>

    <script type="text/javascript" language="JavaScript" src="../../private/funciones.js"></script>

    <%-- ======================= Notificaciones =============================================--%>

    <script type="text/javascript" src="../assets/js/noty/jquery.noty.js"></script>

    <script type="text/javascript" src='../assets/js/noty/layouts/top.js'></script>

    <script type="text/javascript" src='../assets/js/noty/layouts/default.js'></script>

    <script type="text/javascript" src="../assets/js/noty/notifications-custom.js"></script>

    <%-- ======================= Sweet Alert =============================================--%>

    <script src="../assets/js/sweetalert2.all.min.js" type="text/javascript"></script>

    <script src="../assets/js/promise.min.js" type="text/javascript"></script>

    <script type="text/javascript">

        $(document).ready(function() {
            fnLoading(false);
        });
        function fnLoading(sw) {
            console.log(sw);
            if (sw) {
                $('.piluku-preloader').removeClass('hidden');
            } else {
                $('.piluku-preloader').addClass('hidden');
            }
            //console.log(sw);
        }
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
        function fnConfirmacion(ctrl, texto, adicional) {
            var defaultAction = $(ctrl).prop("href");
            Swal.fire({
                title: texto,
                text: adicional,
                icon: 'question',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Si',
                cancelButtonText: 'No'
            }).then(function(result) {
                if (result.value == true) {
                    fnLoading(true);
                    eval(defaultAction);
                }
            })
        }
        function Validar(ctrl, texto, adicional) {
            if ($("#txtDescripcion").val() == "") {
                fnMensaje('error', 'Debe ingresar la descripción del agregado')
                return false
            } else if ($("#txtDetalle").val() == "") {
                fnMensaje('error', 'Debe ingresar el detalle de la clasificación')
                return false
            } else {
                fnConfirmacion(ctrl, texto, adicional);
            }
        }
    </script>

    <style type="text/css">
        body
        {
            padding-right: 0 !important;
        }
        .form-group
        {
            margin: 6px;
        }
        .form-control
        {
            color: Black;
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
        .style2
        {
            height: 20px;
            width: 275px;
        }
        .style3
        {
            height: 20px;
            width: 420px;
        }
        .style4
        {
            width: 420px;
        }
        .style5
        {
            width: 24%;
        }
        .style6
        {
            height: 20px;
            width: 24%;
        }
        .style7
        {
            width: 275px;
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
                font-size: 16px;">
                CONFIGURACIÓN DE CRITERIOS DE PRIORIDAD
            </div>
            <div class="panel-body">
                <asp:UpdatePanel runat="server" ID="updLista" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <contenttemplate>
                        <div id="divLista" runat="server" visible = "true"> 
                        <div class="form-horizontal">
                            <div class="form-group">
                            <asp:Label ID="Label5" runat="server" CssClass="col-sm-2 col-md-2 control-label">Semestre Académico: </asp:Label>
                            <div class="col-sm-4 col-md-4">
                                <div class="col-sm-4 col-md-4">                                                                        
                                    <asp:DropDownList ID="cboCicloAcademico" runat="server" AutoPostBack="True">
                                    </asp:DropDownList>
                                 </div>
                            </div>
                            <asp:Label ID="lblEscuela" runat="server" CssClass="col-sm-2 col-md-2 control-label">Modalidad de Estudio: </asp:Label>
                            <div class="col-sm-4 col-md-4">
                                <div class="col-sm-12 col-md-12">                                    
                                    <asp:DropDownList ID="cboTipoEstudio" runat="server" AutoPostBack="True">
                                    </asp:DropDownList>
                                 </div>
                            </div>
                        </div>                                                                       
                        <br />                   
                        <div class="form-group">
                               <asp:Label ID="lblTramite" runat="server" CssClass="col-sm-2 col-md-2 control-label">Plan Incluye AFC:</asp:Label>
                                <div class="col-sm-5 col-md-5">
                                    <div class="col-sm-5 col-md-5">
                                         
                                    <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True">
                                    <asp:ListItem Value="S">Sí</asp:ListItem>
                                    <asp:ListItem Value="N">No</asp:ListItem>
                                    </asp:DropDownList>
                                    <%--&nbsp;<asp:Button ID="Button1" runat="server" Text="Mostrar" />--%>
                                    </div>
                                </div>
                        
                            <%--<asp:Button runat="server" ID="btnAgregar" CssClass="btn btn-primary" Text="Agregar" />--%>
                            <asp:LinkButton runat="server" ID="cmdBuscar" CssClass="btn btn-sm btn-primary btn-radius"
                                            OnClientClick="fnLoading(true);" Text="<i class='fa fa-search'></i> Buscar">
                                        </asp:LinkButton>
                        </div> 
                         
                        <br />                                     
                        <div class="form-group">                       
                            <asp:GridView runat="server" ID="gvLista" CssClass="table table-condensed" DataKeyNames=""
                                    AutoGenerateColumns="false">
                                    <Columns>                                                                            
                                        <asp:BoundField  HeaderText="PLAN INCLUYE AFC" HeaderStyle-Width="5%" />   
                                        <asp:BoundField  HeaderText="N°CR.ACREDITA" HeaderStyle-Width="10%" />                                     
                                        <asp:BoundField  HeaderText="RSU" HeaderStyle-Width="20%" />                                                                                                                        
                                        <asp:BoundField  HeaderText="NO RSU" HeaderStyle-Width="20%" />                                                                                
                                        <asp:BoundField  HeaderText="1° VEZ" HeaderStyle-Width="10%" /> 
                                        <asp:BoundField  HeaderText="1ER DÍA" HeaderStyle-Width="10%" />                                        
                                        <asp:BoundField  HeaderText="2DO DÍA" HeaderStyle-Width="10%" />                                                                                
                                        <asp:BoundField  HeaderText="3ER DÍA" HeaderStyle-Width="10%" />                                                                                
                                        <asp:TemplateField HeaderText="" HeaderStyle-Width="5%" ShowHeader="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnEditar" runat="server" Text='<span class="fa fa-pencil-square-o"></span>'
                                                    CssClass="btn btn-info btn-sm btn-radius" ToolTip="Responder" CommandName="Editar"
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
                        </contenttemplate>
                    <triggers>
                            <asp:AsyncPostBackTrigger ControlID="gvLista" EventName="RowCommand" />                                                       
                            <asp:AsyncPostBackTrigger ControlID="cmdBuscar" />
                        </triggers>
                </asp:UpdatePanel>
   </div>
        </div>
        </form>
    </div>
</body>
</html>  
   
    
   
  
