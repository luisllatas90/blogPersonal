<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SolicitudesPermisos.aspx.vb"
    Inherits="_SolicitudesPermisos" %>

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

    <%--<script src="../../../javascript/DateTime/Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>--%>
    <%--<link rel='stylesheet' href='../../assets/css/material.css' /> 
    <script type="text/javascript" language="javascript" src="../../../../private/funciones.js"></script>  
    <script type="text/javascript" language="javascript" src="../../../../private/tooltip.js"></script>
    <script type="text/javascript" src='../../assets/js/jquery.dataTables.min.js'></script>
    <script type="text/javascript" src='../../assets/js/funciones.js'></script>
    <script type="text/javascript" src='../../assets/js/jquery.nicescroll.min.js'></script>
    <script type="text/javascript" src='../../assets/js/wow.min.js'></script>
    <script type="text/javascript" src='../../assets/js/jquery.loadmask.min.js'></script>
    <link rel='stylesheet' href='../../assets/css/jquery.dataTables.min.css' />--%>

    <script type="text/javascript" language="javascript">
        $(document).ready(function() {
	        jQuery(function($) {
	            $("#TxtFechaNac").mask("99/99/9999"); //.mask("(999)-999999");
	            //   $("#txttelefono").mask("(999)-9999999");
	            //   $("#txtcelular").mask("(999)-9999999");  
	        });

	    })
          
        function PintarFilaMarcada(obj,estado)
        {
            if (estado==true){
                obj.style.backgroundColor="#FFE7B3"
            }
            else{
                obj.style.backgroundColor="white"
            }
        }
        function Cambia_Check(obj,estado)
        {
            if (estado==1){
                obj.style.ImageUrl="../../images/CZ.gif"
            }
            if (estado==2){
                obj.style.ImageUrl="../../images/CR.gif"
            }
            if (estado==3){
                obj.style.ImageUrl="../../images/CV.gif"
            }
        }
         if(top.location==self.location)
            //location.href='../../tiempofinalizado.asp'} //El ../ depende de la ruta de la página   
    </script>

    <script type="text/javascript" language="javascript">
        $(document).ready(function() {
            jQuery(function($) {
                $("#txtFecha").mask("99/99/9999"); //.mask("(999)-999999");

            });
        })
    </script>

    <%--<script src="../../javascript/DateTime/Scripts/jquery.dynDateTime.min.js" type="text/javascript"></script>

    <script src="../../javascript/DateTime/Scripts/calendar-en.min.js" type="text/javascript"></script>

    <link href="../../javascript/DateTime/Styles/calendar-blue.css" rel="stylesheet"
        type="text/css" />

    <script src="../js/jquery.ui.datepicker-es.js" type="text/javascript"></script>--%>
    <style type="text/css">
        .row
        {
            margin-right: 2px;
            margin-left: 2px;
        }
        .content .main-content
        {
            padding-right: 15px;
        }
        .content
        {
            margin-left: 2px;
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
            padding: 6px 3px;
            font-size: small;
        }
        .table tr td CheckBox
        {
            padding-left: 3;
            padding-right: 3;
        }
    </style>
    <%--<link rel="stylesheet" href="../js/jquery-ui.css" />

    <script src="../js/jquery-1.9.1.js" type="text/javascript"></script>

    <script type="text/javascript" src="../js/jquery-ui.js"></script>

    <script src="../../javascript/DateTime/Scripts/jquery.dynDateTime.min.js" type="text/javascript"></script>

    <script src="../../javascript/DateTime/Scripts/calendar-en.min.js" type="text/javascript"></script>

    <link href="../../javascript/DateTime/Styles/calendar-blue.css" rel="stylesheet"
        type="text/css" />

    <script src="../js/jquery.ui.datepicker-es.js" type="text/javascript"></script>--%>

    <script type="text/javascript" language="javascript">
        var nav1 = window.Event ? true : false;
        var nav2 = window.Event ? true : false;
        function solonumerosentero(evt) {
            // Backspace = 8, Enter = 13, '0' = 48, '9' = 57, '.' = 46
            var key = nav1 ? evt.which : evt.keyCode;
            return (key <= 13 || (key >= 48 && key <= 57));
        }
        function solonumerodecimal(evt) {
            // Backspace = 8, Enter = 13, '0' = 48, '9' = 57, '.' = 46
            var key = nav2 ? evt.which : evt.keyCode;
            return (key <= 13 || (key >= 48 && key <= 57) || key == 46);
        }
    </script>

    <script type="text/javascript">
        $(function() {
            $.datepicker.setDefaults($.datepicker.regional["es"]);
            $("#txtFecha").datepicker({
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
                        de Permisos por Horas de los Colaboradores - Vigilancia </span>
                    <p class="text">
                        Permisos por Horas de los Colaboradores</p>
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
                                <label class="col-md-2 control-label">
                                    Fecha Actual :</label>
                                <asp:Label ID="lblDia" Style="color: #CC0000; vertical-align: sub" runat="server"
                                    Text="Fecha" class="col-md-1 control-label"></asp:Label>
                                <asp:Label ID="lblLista" runat="server" Text="lblLista" class="col-md-1 control-label"
                                    Visible="false"></asp:Label>
                                <asp:Label ID="lblCod_ST" runat="server" Text="lblTrab" class="col-md-1 control-label"
                                    Visible="false"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <label class="col-md-2 control-label">
                                    Filtrar Estado :</label>
                                <div class="col-md-2">
                                    <asp:DropDownList ID="ddlEstado" runat="server" AutoPostBack="True" CssClass="form-control">
                                        <asp:ListItem Value="TO" Selected="True">TODOS</asp:ListItem>
                                        <asp:ListItem Value="PE">Pendientes</asp:ListItem>
                                        <asp:ListItem Value="RP">Retorno Pendiente</asp:ListItem>
                                        <asp:ListItem Value="CO">Completados</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    <asp:ImageButton runat="server" AlternateText="Buscar" ID="btnActualizar" ToolTip="*Actualizar lista.."
                                        ImageUrl="../../images/refrescar_02.png" OnClick="Unnamed3_Click" Height="27px"
                                        Width="113px" />
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <label class="col-md-2 control-label">
                                    Apellido / Nombre :</label>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtPersonal" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-1">
                                    <asp:ImageButton runat="server" AlternateText="Buscar" ID="btnBuscar" ToolTip="*Buscar Trabajador"
                                        ImageUrl="../../images/busca.gif" OnClick="Unnamed1_Click" />
                                </div>
                                <div class="col-md-1">
                                    <asp:ImageButton runat="server" AlternateText="Limpiar" ID="btnLimpiar" ToolTip="*Limpiar Controles"
                                        ImageUrl="../../images/delete-icon.jpg" OnClick="Unnamed2_Click" />
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <asp:GridView ID="gvCarga" runat="server" AutoGenerateColumns="False" DataKeyNames="codigo_ST, Colaborador"
                                    CssClass="table-responsive" Font-Size="9" ForeColor="#333333">
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <Columns>
                                        <asp:BoundField DataField="codigo_ST" HeaderText="Núm.">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="dni" HeaderText="DNI" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Colaborador" HeaderText="Colaborador" />
                                        <asp:BoundField DataField="Fecha" HeaderText="Fecha" />
                                        <asp:BoundField DataField="Hora_Ini" HeaderText="Hora de Inicio" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Hora_IniAu" HeaderText="Salida Real" ItemStyle-Font-Bold="true"
                                            ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Underline="true">
                                            <ItemStyle HorizontalAlign="Center" Font-Bold="True" Font-Underline="True"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:ButtonField ButtonType="Image" ItemStyle-HorizontalAlign="Center" Visible="true"
                                            ImageUrl="../../images/check.gif">
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:ButtonField>
                                        <%--<asp:TemplateField HeaderText="-SA-">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="registra_salida"
                                                    ImageUrl="../../images/check - azul.gif" OnClientClick="return confirm('¿Desea Eliminar Registro?.')"/>
                                            </ItemTemplate>
                                            <ControlStyle Font-Underline="True" ForeColor="Blue" />
                                        </asp:TemplateField>--%>
                                        <asp:ButtonField ButtonType="Image" CommandName="registra_salida" HeaderText="-SA-"
                                            ImageUrl="../../images/check - azul.gif" />
                                        <asp:BoundField DataField="Hora_Fin" HeaderText="Hora de Fin" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Hora_FinAu" HeaderText="Retorno Real" ItemStyle-Font-Bold="true"
                                            ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Underline="true">
                                            <ItemStyle HorizontalAlign="Center" Font-Bold="True" Font-Underline="True"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:ButtonField ButtonType="Image" ItemStyle-HorizontalAlign="Center" Visible="true"
                                            ImageUrl="../../images/check.gif">
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:ButtonField>
                                        <asp:ButtonField ButtonType="Image" CommandName="registra_retorno" HeaderText="-RE-"
                                            ImageUrl="../../images/check - azul.gif" />
                                        <asp:TemplateField HeaderText="Estado" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Image ID="ImgEstado" runat="server" CausesValidation="TRUE" Height="28px" Width="28px">
                                                </asp:Image>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="codigo_ST" HeaderText="codigo_ST" Visible="false" ItemStyle-HorizontalAlign="Center">
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
                                        Font-Bold ="true"></asp:Label>

                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="divConfirmaRegistroSalida" runat="server">
                        <div class="row">
                            <div class="form-group">
                                 <label style="font-weight: bold" class="col-md-6 control-label">
                                    ¿Está seguro que desea Registrar la Salida del Permiso del colaborador :
                                </label>
                                <asp:Label ID="lblTrabajador" runat="server" Text="lblTrabajador" class="col-md-5 control-label"
                                    Style="margin-left" Font-Bold="True"></asp:Label>
                                <label style="font-weight: bold" class="col-md-1 control-label">
                                    ?
                                </label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <asp:Button ID="btnConfirmaSalidaSI" runat="server" Text="SI" CssClass="btn btn-success" />
                                <asp:Button ID="btnConfirmaSalidaNO" runat="server" Text="NO" CssClass="btn btn-success" />
                            </div>
                        </div>
                    </div>
                    <div id="divConfirmaRegistroRetorno" runat="server">
                        <div class="row">
                            <div class="form-group">
                                <label style="font-weight: bold" class="col-md-6 control-label">
                                    ¿Está seguro que desea Registrar el Retorno del Permiso del colaborador :
                                </label>
                                <asp:Label ID="lblTrabajador1" runat="server" Text="lblTrabajador" class="col-md-5 control-label"
                                    Style="margin-left" Font-Bold="True"></asp:Label>
                                <label style="font-weight: bold" class="col-md-1 control-label">
                                    ?
                                </label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <asp:Button ID="btnConfirmaRetornoSI" runat="server" Text="SI" CssClass="btn btn-success" />
                                <asp:Button ID="btnConfirmaRetornoNO" runat="server" Text="NO" CssClass="btn btn-success" />
                            </div>
                        </div>
                    </div>
            </div> </div>
            </form>
        </div>
    </div>
</body>
</html>
