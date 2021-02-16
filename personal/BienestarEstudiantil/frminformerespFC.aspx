<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frminformerespFC.aspx.vb"
    Inherits="frminformerespFC" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Registro de Informe</title>
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

    <link href="../assets/font-awesome-4.7.0/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <%-- ======================= Lista desplegable con busqueda =============================================--%>
    <link href="../../assets/bootstrap-select-1.13.1/css/bootstrap-select.css" rel="stylesheet"
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
        function solonumeros(e) {

            var key;

            if (window.event) // IE
            {
                key = e.keyCode;
            }
            else if (e.which) // Netscape/Firefox/Opera
            {
                key = e.which;
            }

            if (key < 48 || key > 57) {
                return false;
            }

            return true;
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
        <%--<asp:UpdatePanel runat="server" ID="updLoading" UpdateMode="Conditional">
            <contenttemplate>
                <div class="piluku-preloader text-center">
                    <div class="loader">
                        Loading...</div>
                </div>
            </contenttemplate>        
                <triggers>
                      <asp:AsyncPostBackTrigger ControlID="gvListaEscalas" EventName="RowCommand" />
                      <asp:AsyncPostBackTrigger ControlID="ddlEstado" EventName="SelectedIndexChanged" />
                      <asp:AsyncPostBackTrigger ControlID="btnResgresar" />
                      <asp:AsyncPostBackTrigger ControlID="btnActualizar" />
                      <asp:AsyncPostBackTrigger ControlID="btnBuscar" />
                 </triggers>           
        </asp:UpdatePanel>--%>
        <div class="panel panel-default">
            <div class="panel-heading" style="background-color: #E33439; color: White; font-weight: bold;
                font-size: 14px;">
                <b>REGISTRO DE INFORME PARA TRÁMITE DE RECONOCIMIENTO</b>
            </div>
            <div class="panel-body">
                <asp:UpdatePanel runat="server" ID="updActualiza" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <contenttemplate>
                    <div id="actualiza" runat="server" visible = "true">                                             
                        <div runat="server" id="lblmensaje"></div>
                        <div class="col-sm-12 col-md-12" style="">
                            <div class="form-group text-center">
                               <asp:Button runat="server" ID="btnResgresar" CssClass="btn btn-success" Text="Regresar" />
                            </div>  
                            <hr />                          
                        </div>                       
                        <div class="form-horizontal">
                            <asp:HiddenField ID="hfCodigo_ecs" runat="server" />             
                            <div class="form-group">                           
                                <asp:Label ID="Label1" runat="server" CssClass="col-sm-2 col-md-2 control-label">Estudiante:</asp:Label>
                                <div class="col-sm-4 col-md-4">
                                    <div class="col-sm-12 col-md-12">
                                    <asp:TextBox runat="server" ID="txtEstudiante" ReadOnly="false" CssClass="form-control"  Style="text-transform: uppercase" Text="Vanessa Raquel Torres Flores">
                                    </asp:TextBox>
                                    </div>
                                </div>
                                <asp:Label ID="Label2" runat="server" CssClass="col-sm-2 col-md-2 control-label">Cód.Univ.: </asp:Label>
                                <div class="col-sm-4 col-md-4">
                                    <div class="col-sm-5 col-md-5">
                                    <asp:TextBox runat="server" ID="txtGrupo" ReadOnly="false" CssClass="form-control"  Style="text-transform: uppercase" Text="191TD452369">
                                       </asp:TextBox>
                                    </div>
                                    <hr />                                      
                                </div>                                 
                                <asp:Label ID="Label3" runat="server" CssClass="col-sm-2 col-md-2 control-label">Escuela:</asp:Label>
                                <div class="col-sm-4 col-md-4">                                    
                                    <div class="col-sm-12 col-md-12">
                                    <asp:TextBox runat="server" ID="txtEscuela" ReadOnly="false" CssClass="form-control"  Style="text-transform: uppercase" Text="Administración de Empresas">
                                    </asp:TextBox>
                                    </div>
                                </div>
                                <asp:Label ID="Label10" runat="server" CssClass="col-sm-2 col-md-2 control-label">Adjunto: </asp:Label>
                                <div class="col-sm-4 col-md-4">
                                    <div class="col-sm-4 col-md-4">
                                         <asp:LinkButton ID="LinkButton1" runat="server" Text='<span class="fa fa-download"></span> Descargar'
                                                CssClass="btn btn-info btn-sm btn-radius" ToolTip="Descargar" OnClientClick="return false;">
                                            </asp:LinkButton>
                                    </div>
                                    <hr />                                    
                                </div> 
                                <asp:Label ID="Label7" runat="server" CssClass="col-sm-2 col-md-2 control-label">Detalle Trámite:</asp:Label>
                                <div class="col-sm-4 col-md-4">                                    
                                    <div class="col-sm-12 col-md-12">
                                    <asp:TextBox runat="server" ID="txtDetTramite" CssClass="form-control" Enabled="true"
                                            TextMode="MultiLine" Rows="3"  Style="text-transform: uppercase" Text="Solicito recococimiento para el taller de Voley por ser de la selección de voley de la universidad.">
                                    </asp:TextBox>
                                    </div>                                     
                                </div>
                                <asp:Label ID="Label4" runat="server" CssClass="col-sm-2 col-md-2 control-label">Cód. Trámite: </asp:Label>
                                <div class="col-sm-4 col-md-4">
                                    <div class="col-sm-4 col-md-4">
                                         <asp:TextBox runat="server" ID="txtCodTramite" ReadOnly="false" CssClass="form-control"  Style="text-transform: uppercase" Text="TR-2021-102">
                                    </asp:TextBox>
                                    </div>
                                      <hr />                                    
                                </div>                                 
                                <hr />
                                <hr />
                                <div class="col-sm-12 col-md-12">
                                  <div class="panel-heading" style="background-color: #E33439; color: White; font-weight: bold;
                                    font-size: 14px;"> 
                                    DATOS A REGISTRAR 
                                    </div>
                                     <hr /> 
                                 </div>   
                                 <hr />
                                 <asp:Label ID="Label8" runat="server" CssClass="col-sm-2 col-md-2 control-label"
                                        For="txtObservacion">Taller:</asp:Label>
                                    <div class="col-sm-4 col-md-4">                                    
                                    <div class="col-sm-12 col-md-12">
                                    <asp:DropDownList runat="server" ID="ddlTaller" CssClass="form-control" AutoPostBack="true">
                                        <asp:ListItem Value="-1">[--Seleccione--]</asp:ListItem>
                                        <asp:ListItem Value="0">Taller 1</asp:ListItem>
                                        <asp:ListItem Value="1">Taller 2</asp:ListItem>
                                        <asp:ListItem Value="2">Taller 3</asp:ListItem>                                                                                
                                    </asp:DropDownList> 
                                    </div>
                                </div>
                                    
                                <asp:Label ID="Label9" runat="server" CssClass="col-sm-2 col-md-2 control-label">Semestre: </asp:Label>
                                <div class="col-sm-4 col-md-4">
                                    <div class="col-sm-5 col-md-5">
                                    <asp:DropDownList runat="server" ID="ddlSemestre" CssClass="form-control" AutoPostBack="true">
                                        <asp:ListItem Value="-1">[--Seleccione--]</asp:ListItem>
                                        <asp:ListItem Value="0">2021-0</asp:ListItem>
                                        <asp:ListItem Value="1">2020-II</asp:ListItem>
                                        <asp:ListItem Value="2">2020-I</asp:ListItem>                                                                                
                                    </asp:DropDownList> 
                                           
                                    </div>
                                    <hr/> 
                                 </div>                                                                         
                                <asp:Label ID="Label6" runat="server" CssClass="col-sm-2 col-md-2 control-label"
                                        For="txtObservacion">Adjuntar Informe</asp:Label>
                                    <div class="col-sm-4 col-md-4">
                                        <asp:FileUpload runat="server" ID="archivo" CssClass="form-control" />
                                        <ul>
                                            <li>Archivos permitidos: <span style="color: Red">.doc,.docx,.pdf,.zip,.rar,.xls,.xlsx</span></li>
                                            <li>Tamaño Máximo: <span style="color: Red">20 Mb</span></li>
                                        </ul>
                                    </div>
                                <asp:Label ID="lblInforme" runat="server" CssClass="col-sm-2 col-md-2 control-label">Fecha de Ingreso: </asp:Label>
                                <div class="col-sm-4 col-md-4">
                                    <div class="col-sm-5 col-md-5" class="input-group date">
                                       <asp:TextBox runat="server" ID="txtFecha" ReadOnly="false" CssClass="form-control"  Style="text-transform: uppercase" Text="">
                                       </asp:TextBox>                                           
                                    </div>
                                    <hr/> 
                                 </div>                                                                                                                                                                                                                       
                                
                            </div>
                            <%--<div class="form-group">
                                <asp:Label ID="Label3" runat="server" CssClass="col-sm-2 col-md-2 control-label">Valor mínimo</asp:Label>
                                <div class="col-sm-1 col-md-1">
                                    <div class="col-sm-12 col-md-12">
                                        <asp:TextBox runat="server" ID="txtValorMinimo" ReadOnly="false" CssClass="form-control" Text="" onkeypress="javascript:return solonumeros(event)">
                                        </asp:TextBox>
                                    </div>
                                </div>
                                <asp:Label ID="Label4" runat="server" CssClass="col-sm-2 col-md-2 control-label">Valor máximo</asp:Label>
                                <div class="col-sm-1 col-md-1">
                                    <div class="col-sm-12 col-md-12">
                                        <asp:TextBox runat="server" ID="txtValorMáximo" ReadOnly="false" CssClass="form-control" Text="" onkeypress="javascript:return solonumeros(event)">
                                        </asp:TextBox>
                                    </div>
                                </div>
                            </div>    --%>                 
                        </div>     
                        <hr />               
                        <div class="form-group">                            
                            <asp:Button runat="server" ID="btnActualizar" CssClass="btn btn-primary" Text="Guardar" />
                        </div>
                     </div>
                     
                    </contenttemplate>
                    <triggers>
                            <asp:AsyncPostBackTrigger ControlID="gvListaEscalas" EventName="RowCommand" />
                            <asp:AsyncPostBackTrigger ControlID="ddlEstado" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="btnResgresar" />
                            <asp:AsyncPostBackTrigger ControlID="btnActualizar" />
                            <asp:AsyncPostBackTrigger ControlID="btnBuscar" />
                        </triggers>
                </asp:UpdatePanel>
                <asp:UpdatePanel runat="server" ID="updLista" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <contenttemplate>
                        <div id="lista" runat="server" visible = "true"> 
                        <div class="form-horizontal">
                            <div class="form-group">
                            <asp:Label ID="Label5" runat="server" CssClass="col-sm-1 col-md-1 control-label">Estado: </asp:Label>
                            <div class="col-sm-5 col-md-5">
                                <div class="col-sm-12 col-md-12">
                                    <asp:DropDownList runat="server" ID="ddlEstado" CssClass="form-control" AutoPostBack="true">
                                        <asp:ListItem Value="-1">[--TODOS--]</asp:ListItem>
                                        <asp:ListItem Value="0">PENDIENTES</asp:ListItem>
                                        <asp:ListItem Value="1">POR REVISAR</asp:ListItem>
                                        <asp:ListItem Value="2">CON INFORME</asp:ListItem>
                                        <asp:ListItem Value="3">OBSERVADOS POR COORD.</asp:ListItem>
                                        <%--<asp:ListItem Value="2">RECHAZADOS</asp:ListItem>                                                                                                           --%>
                                    </asp:DropDownList>
                                 </div>
                            </div>
                            <asp:Label ID="lblEscuela" runat="server" CssClass="col-sm-1 col-md-1 control-label">Escuela: </asp:Label>
                            <div class="col-sm-5 col-md-5">
                                <div class="col-sm-12 col-md-12">
                                    <asp:DropDownList runat="server" ID="cboEscuela" CssClass="form-control" AutoPostBack="true">
                                                                                                                                                 
                                    </asp:DropDownList>
                                 </div>
                            </div>
                        </div>                           
                                            
                        <br />                   

                        <div class="form-group">
                               <asp:Label ID="lblTramite" runat="server" CssClass="col-sm-1 col-md-1 control-label">N° Trámite</asp:Label>
                                <div class="col-sm-5 col-md-5">
                                    <div class="col-sm-5 col-md-5">
                                        <asp:TextBox runat="server" ID="txtTramite" ReadOnly="false" CssClass="form-control" Text="" onkeypress="javascript:return solonumeros(event)">
                                        </asp:TextBox>
                                    </div>
                                </div>
                        
                            <%--<asp:Button runat="server" ID="btnAgregar" CssClass="btn btn-primary" Text="Agregar" />--%>
                            <asp:LinkButton runat="server" ID="btnBuscar" CssClass="btn btn-sm btn-primary btn-radius"
                                            OnClientClick="fnLoading(true);" Text="<i class='fa fa-search'></i> Buscar">
                                        </asp:LinkButton>
                        </div> 
                         </div>
                        <br />                                     
                        <div class="form-group">                       
                            <asp:GridView runat="server" ID="gvListaEscalas" CssClass="table table-condensed" DataKeyNames="codigo_ecs, eliminado_ecs"
                                    AutoGenerateColumns="false">
                                    <Columns>                                                                            
                                        <asp:BoundField  HeaderText="N° TRÁM" HeaderStyle-Width="5%" />   
                                        <asp:BoundField  HeaderText="CÓD. UNIV" HeaderStyle-Width="10%" />                                     
                                        <asp:BoundField  HeaderText="ESTUDIANTE" HeaderStyle-Width="20%" />                                                                                                                        
                                        <asp:BoundField  HeaderText="DESCRIPCIÓN TRÁMITE" HeaderStyle-Width="20%" />                                                                                
                                        <asp:BoundField  HeaderText="FECHA TRÁMITE" HeaderStyle-Width="10%" /> 
                                        <asp:BoundField  HeaderText="FECHA ASIGNADO" HeaderStyle-Width="10%" />                                        
                                        <asp:BoundField  HeaderText="ESTADO" HeaderStyle-Width="10%" />                                                                                
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
                            <asp:AsyncPostBackTrigger ControlID="gvListaEscalas" EventName="RowCommand" />
                            <asp:AsyncPostBackTrigger ControlID="ddlEstado" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="btnResgresar" />
                            <asp:AsyncPostBackTrigger ControlID="btnActualizar" />
                            <asp:AsyncPostBackTrigger ControlID="btnBuscar" />
                        </triggers>
                </asp:UpdatePanel>
            </div>
        </div>
        </form>
    </div>
</body>
</html>
