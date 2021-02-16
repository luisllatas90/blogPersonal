<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmCronogramaSesiones.aspx.vb" Inherits="GestionDocumentaria_frmCronogramaSesiones" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Configuración</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta http-equiv="Pragma" content="no-cache" />
    
    <link href="../assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    
    <link href="../assets/css/material.css" rel="stylesheet" type="text/css" />
    <link rel='stylesheet' href='../assets/css/style.css?x=1' />

    <script src="../assets/js/jquery.js" type="text/javascript"></script>

    <script src="../assets/js/app.js" type="text/javascript"></script>

    <script type="text/javascript" src='../assets/js/jquery-ui-1.10.3.custom.min.js'></script>

    <script src="../assets/js/bootstrap.min.js" type="text/javascript"></script>

    <link href="../assets/font-awesome-4.7.0/css/font-awesome.css" rel="stylesheet"  type="text/css" />
    <%-- ======================= Lista desplegable con busqueda =============================================--%>
    <link href="../assets/bootstrap-select-1.13.1/css/bootstrap-select.css" rel="stylesheet"
        type="text/css" />

    <script type="text/javascript" src='../assets/js/jquery.js'></script>

    <script src="../assets/js/bootstrap.min.js" type="text/javascript"></script>

    <%-- ======================= Inicio Notificaciones =============================================--%>

    <script type="text/javascript" src="../assets/js/noty/jquery.noty.js"></script>

    <script type="text/javascript" src='../assets/js/noty/layouts/top.js'></script>

    <script type="text/javascript" src='../assets/js/noty/layouts/default.js'></script>

    <script type="text/javascript" src="../assets/js/noty/notifications-custom.js"></script>

    <%-- ======================= Lista desplegable con busqueda =============================================--%>

    <script src="../assets/bootstrap-select-1.13.1/js/bootstrap-select.js" type="text/javascript"></script>
 
    <script type="text/javascript">
        
    </script>
    <style type="text/css">
        body {padding-right: 0 !important;}  
        
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
                    <b>SESIONES DE CONSEJO DE FACULTAD</b>                    
                </div>  
                <div class="panel-body">
                    
                    <asp:UpdatePanel runat="server" ID="updConformidad" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <div runat="server" id="Lista">
                            <div class="form-horizontal">                    
                                <div class="form-group">
                                    <asp:Label ID="Label1" runat="server" CssClass="col-sm-1 col-md-1 control-label">Facultad:</asp:Label>
                                    <div class="col-sm-5 col-md-5">
                                        <div class="col-sm-12 col-md-12">
                                            <asp:TextBox runat="server" ID="txtFacultad" ReadOnly="true" CssClass="form-control" Text="CIENCIAS EMPRESARIALES">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <hr />                                                                
                            </div>
                            <br />                            
                            <div class="form-group">
                                <asp:Button runat="server" ID="btnAgregar" CssClass="btn btn-success" Text="Agregar" />                                
                            </div>
                            <div class="form-group">
                                    <asp:GridView runat="server" ID="gvListaSesiones" CssClass="table table-condensed" DataKeyNames=""
                                    AutoGenerateColumns="false">
                                    <Columns>                                    
                                        
                                        <asp:BoundField DataField="Facultad" HeaderText="FACULTAD" HeaderStyle-Width="35%" />
                                        <asp:BoundField DataField="Fecha" HeaderText="FECHA" HeaderStyle-Width="20%" />
                                        <asp:BoundField DataField="Tipo" HeaderText="TIPO" HeaderStyle-Width="15%" />
                                        <asp:BoundField DataField="id" HeaderText="N° SESION" HeaderStyle-Width="5" /> 
                                         <asp:BoundField DataField="Motivo" HeaderText="MOTIVO" HeaderStyle-Width="15%" />                                       
                                        <asp:TemplateField HeaderText="" HeaderStyle-Width="10%" ShowHeader="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnEditar" runat="server" Text='<span class="fa fa-pencil-square-o"></span>'
                                                    CssClass="btn btn-info btn-sm btn-radius" ToolTip="Editar Sesion" CommandName="Editar"
                                                    CommandArgument='<%#Convert.ToString(Container.DataItemIndex)%>'>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="LinkButton1" runat="server" Text='<span class="fa fa fa-trash"></span>'
                                                    CssClass="btn btn-danger btn-sm btn-radius" ToolTip="Desactivar Sesion" CommandName="Eliminar"
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
                    </ContentTemplate>                   
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnAgregar" EventName ="Click" />
                        <asp:AsyncPostBackTrigger ControlID="gvListaSesiones" EventName="RowCommand" />
                        <%--<asp:AsyncPostBackTrigger ControlID="ddlConsejo" EventName="selectedindexchanged" />--%>
                        <asp:AsyncPostBackTrigger ControlID="btnAtras" />
                    </Triggers>
                    </asp:UpdatePanel>
                    
                    <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
                    <ContentTemplate>                       
                        <div class="form-group form-horizontal" id="DivAsesorias" runat="server" visible="false">
                            <div class="row">
                                <div class="form-group text-center">
                                    <asp:Button runat="server" ID="btnAtras" CssClass="btn btn-sm btn-danger" Text="Atrás" />
                                </div>
                            </div>
                            <br />
                            <hr />
                            <div class="form-group">
                                    <asp:Label ID="lblcalendario" runat="server" CssClass="col-md-2 col-sm-2 control-label">Fecha y Hora de Sesión: </asp:Label>
                                    <div class="col-sm-3 col-md-3" runat="server" id="Divfecha">
                                        <div class="input-group date" id="datetimepicker1">
                                            <asp:TextBox runat="server" ID="txtFecha" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                            <span class="input-group-addon"><span class="ion ion-calendar"></span></span>
                                        </div>
                                    </div>
                                             
                                    <asp:Label ID="Label2" runat="server" CssClass="col-sm-1 col-md-1 control-label">Tipo: </asp:Label>
                                    <div class="col-sm-5 col-md-5">
                                        <div class="col-sm-12 col-md-12">
                                        <asp:DropDownList runat="server" ID="ddlEstado" CssClass="form-control" AutoPostBack="true">
                                            <asp:ListItem Value="O">ORDINARIA</asp:ListItem>
                                            <asp:ListItem Value="E">EXTRAORDINARIA</asp:ListItem>                                        
                                        </asp:DropDownList>
                                        </div>
                                  </div>                       
                            </div>
                             <div class="form-group">
                                    <asp:Label ID="Label4" runat="server" CssClass="col-sm-2 col-md-2 control-label">Motivo de la sesión: </asp:Label>
                                    <div class="col-sm-3 col-md-3">
                                     <%--   <div class="col-sm-12 col-md-12">--%>
                                        <asp:DropDownList runat="server" ID="DropDownList1" CssClass="form-control" AutoPostBack="true">
                                            <asp:ListItem Value="O">APROBAR GRADOS</asp:ListItem>
                                            <asp:ListItem Value="E">APROBAR TITULOS</asp:ListItem>                                        
                                        </asp:DropDownList>
                                       <%-- </div>--%>
                                  </div>                       
                            </div>
                            
                            <br />
                            <hr />
                            <asp:Button runat="server" ID="btnActualizar" CssClass="btn btn-primary" Text="Guardar" />                               
                        </div>
                       
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnAgregar" EventName ="Click" />
                        <asp:AsyncPostBackTrigger ControlID="gvListaSesiones" EventName="RowCommand" />
                       <%-- <asp:AsyncPostBackTrigger ControlID="ddlConsejo" EventName="selectedindexchanged" />--%>
                        <asp:AsyncPostBackTrigger ControlID="btnAtras" />
                    </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
            
            
            
            
        </form>
    </div>
</body>
</html>
