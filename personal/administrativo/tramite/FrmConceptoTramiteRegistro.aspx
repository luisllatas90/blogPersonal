<%@ Page Language="VB" AutoEventWireup="false" validateRequest="false" CodeFile="FrmConceptoTramiteRegistro.aspx.vb"
    Inherits="FrmConceptoTramiteRegistro" %>
<%@ Register Assembly="BusyBoxDotNet" Namespace="BusyBoxDotNet" TagPrefix="busyboxdotnet" %>
<!DOCTYPE html>
<html lang="es">
<head id="Head1" runat="server">
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta name="google" value="notranslate">
    <title>Registrar Concepto T&aacute;mite</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=7' />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <link href="../../scripts/css/bootstrap.css" rel="Stylesheet" type="text/css" />
    <link rel='stylesheet' href='../../assets/css/material.css'/>
    <link href="../../assets/css/bootstrap-datepicker3.css" rel="Stylesheet" type="text/css" />
    <link href="../../assets/fontawesome-5.2/css/all.min.css" rel="stylesheet" type="text/css" />

    <script src="../../scripts/js/jquery-1.12.3.min.js" type="text/javascript"></script>

    

    <script src="../../scripts/js/bootstrap.min.js" type="text/javascript"></script>

    <script src="../../assets/js/bootstrap-datepicker.js" type="text/javascript"></script>

    <script src="../../assets/fontawesome-5.2/js/all.min.js" type="text/javascript"></script>

    <script type="text/javascript" src='../../assets/js/jquery.accordion.js'></script>
    <script type="text/javascript" src='../../assets/js/materialize.js'></script>    
 

  <style type="text/css">
        .no-border
        {
            border: 0;
            box-shadow: none;
        }
        
        
       .form-control2
        {             
        display: block;
        width: 100%;
       /* height:34px;*/
        height:24px;
        padding: 2px 4px; 
        font-size: 12px;
        line-height: 1.42857143;
        color: #555;
        background-color: #fff;
        background-image: none;
        border: 1px solid #ccc;
        border-radius: 4px;
        -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, .075);
        box-shadow: inset 0 1px 1px rgba(0, 0, 0, .075);
        -webkit-transition: border-color ease-in-out .15s, -webkit-box-shadow ease-in-out .15s;
        -o-transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s;
        transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s;
      }
    </style>

    <script type="text/javascript">
        $(document).ready(function() {
            $('#txtTramiteBsq').keypress(function(event) {
                if (event.which == 13) {
                    event.preventDefault();
                    document.getElementById("<%=btnConsultar.ClientID %>").click();
                }
            });

            $('#txtCodServicio').keypress(function(event) {
                if (event.which == 13) {
                    event.preventDefault();

                    if (document.getElementById("<%=btnBuscarServicio.ClientID %>"))
                        document.getElementById("<%=btnBuscarServicio.ClientID %>").click();
                    else if (document.getElementById("<%=btnBuscarServicioRef.ClientID %>"))
                        document.getElementById("<%=btnBuscarServicioRef.ClientID %>").click();

                    //document.getElementById("<%=btnBuscarServicio.ClientID %>").click();
                }
            });
            $('#txtServicioBsq').keypress(function(event) {
                if (event.which == 13) {
                    event.preventDefault();
                    if (document.getElementById("<%=btnBuscarServicio.ClientID %>"))
                        document.getElementById("<%=btnBuscarServicio.ClientID %>").click();
                    else if (document.getElementById("<%=btnBuscarServicioRef.ClientID %>"))
                        document.getElementById("<%=btnBuscarServicioRef.ClientID %>").click();


                    // document.getElementById("<%=btnBuscarServicio.ClientID %>").click();
                }
            });

            $('#txtCodCentroCosto').keypress(function(event) {
                if (event.which == 13) {
                    event.preventDefault();
                    if (document.getElementById("<%=btnBuscarCentroCosto.ClientID %>"))
                        document.getElementById("<%=btnBuscarCentroCosto.ClientID %>").click();
                    else if (document.getElementById("<%=btnBuscarCentroCostoRef.ClientID %>"))
                        document.getElementById("<%=btnBuscarCentroCostoRef.ClientID %>").click();
                }
            });
            $('#txtCentroCostoBsq').keypress(function(event) {
                if (event.which == 13) {
                    event.preventDefault();

                    if (document.getElementById("<%=btnBuscarCentroCosto.ClientID %>"))
                        document.getElementById("<%=btnBuscarCentroCosto.ClientID %>").click();
                    else if (document.getElementById("<%=btnBuscarCentroCostoRef.ClientID %>"))
                        document.getElementById("<%=btnBuscarCentroCostoRef.ClientID %>").click();
                }
            });




            $('#txtPrecio').on('input', function() {
                this.value = this.value.replace(/[^0-9]/g, '');
            });

            $('#txtCodCentroCosto').on('input', function() {
                this.value = this.value.replace(/[^0-9]/g, '');
            });

            $('#txtCodServicio').on('input', function() {
                this.value = this.value.replace(/[^0-9]/g, '');
            });
            $('#txtPrecioScoRef').on('input', function() {
                this.value = this.value.replace(/[^0-9]/g, '');
            });


        });


        function openModal(nombre) {
            $('#' + nombre).modal('show');
        }

        function closeModal(confirm) {
            if (confirm) {
                $('#modalComite').modal('hide');
                $("#modalComite").remove();

                $('body').removeClass('modal-open');
                $('.modal-backdrop').remove();
                $(".modal-dialog").remove();
                $(".modal").remove();
            }
        }




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

</head>
<body>
    <form id="form1" runat="server"  class="form form-horizontal">
    <asp:HiddenField ID="hf" runat="server" />
    <asp:HiddenField ID="hdCtr" Value="" runat="server" />    
    <asp:HiddenField ID="Hdftr" Value="" runat="server" />
    <asp:HiddenField ID="Hdmctr" Value="" runat="server" />
    <asp:HiddenField ID="hdUpd" Value="1" runat="server" />
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release">
    </asp:ScriptManager>
    <div class="container-fluid">
        <div class="messagealert" id="alert_container"></div>
         <asp:Panel CssClass="panel panel-primary" id="pnlLista"  runat="server" style="padding:0px;">
            
            <div class="panel panel-heading" >
                <h4>
                    Registrar Concepto Tr&aacute;mite</h4>
            </div>
            <div class="panel panel-body"  style="padding:3px;">            
            <div class="row">
                    <div class="col-md-9">
                     <div class="col-md-6">
                        <div class="form-group">
                            <label class="col-md-4" for="ddlTipoTramiteBsq">
                                Tipo Tr&aacute;mite</label>
                            <div class="col-md-8">
                            
                                <asp:DropDownList name="ddlTipoTramiteBsq" ID="ddlTipoTramiteBsq" runat="server" CssClass="form-control2" AutoPostBack=true>
                                    <asp:ListItem Value=""></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                     <div class="col-md-6">
                        <div class="form-group">
                            <label class="col-md-4" for="ddlTipoEstudioBsq">
                                Tipo Estudio</label>
                            <div class="col-md-8">
                                <asp:DropDownList name="ddlTipoEstudioBsq" ID="ddlTipoEstudioBsq" runat="server" CssClass="form-control2" AutoPostBack=true>
                                    <asp:ListItem Value=""></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    
                     <div class="col-md-6">
                        <div class="form-group">
                            <label class="col-md-4" for="ddlEgresadoBsq">
                                Egresado</label>
                            <div class="col-md-8">
                                <asp:DropDownList name="ddlEgresadoBsq" ID="ddlEgresadoBsq" runat="server" CssClass="form-control2" AutoPostBack=true>
                                    <asp:ListItem Value="">TODOS</asp:ListItem>                                    
                                    <asp:ListItem Value="1">SI</asp:ListItem>                                    
                                    <asp:ListItem Value="0">NO</asp:ListItem>                                    
                                </asp:DropDownList>
                                
                            </div>
                        </div>
                    </div>
                     <div class="col-md-6">
                        <div class="form-group">
                        <div class="col-md-12">
                            <asp:TextBox ID="txtTramiteBsq" name="txtTramiteBsq" runat="server" placeholder="Descripci&oacute;n Tramite" CssClass="form-control2 input-sm" Style="text-transform: uppercase;" autocomplete="off"  AutoPostBack="false" ></asp:TextBox>
                            </div>
                        </div>
                    </div>
                                          
                    </div>
                    <div class="col-md-3">
                    
                    <div class="col-md-12">
                    
                    <div class="btn-group" role="group" aria-label="Basic example">
                        
                        <asp:LinkButton ID="btnCrear" runat="server" Text='<i class="fa fa-plus"></i> Crear'
                            CssClass="btn btn-default btn-sm" OnClick="btnCrear_Click"></asp:LinkButton>
                      
                        <asp:LinkButton ID="btnConsultar" runat="server" Text='<i class="fa fa-search"></i> Buscar'
                        CssClass="btn btn-success btn-sm" OnClick="btnBuscar_Click"></asp:LinkButton>
                        
                    </div>
                    </div>
                    <div class="col-md-12">
                     <div class="form-group">
					    <div class="col-md-12">
						    <div class="input-group" style="text-align:justify">
							    <span class="input-group-addon addon-left" style="background-color:White">										
								    <asp:CheckBox ID="chkMostrarActivoCtr" name="chkMostrarActivoCtr" runat="server" Checked=true />
								    <label for="chkMostrarActivoCtr" class="margin-zero"><span></span></label>
							    </span>
							    <label class="form-control" for="chkMostrarActivoCtr">Mostrar solo activos</label>								
						    </div>
					    </div>
                     </div>
                    </div>
                    
                    </div>
            </div>            
            <div class="panel panel-body" style="padding:3px;">
        <div class="table-responsive">
            <asp:GridView ID="grwResultado" runat="server" AutoGenerateColumns="false" DataKeyNames="codigo_ctr, descripcion_ctr, ubicacion_ctr, descripcion_test, tieneSolicitudVirtual,esEgresado,nombre_tctr,tieneCarreraProfesionalAsociada,codigo_test,tieneMensajeInformativo,tieneReglas"
                CssClass="table table-sm table-bordered table-hover" GridLines="None" RowStyle-Font-Size=X-Small onselectedindexchanged="grwResultado_SelectedIndexChanged" OnRowDataBound="grwResultado_RowDataBound">
                <Columns>
                   <asp:BoundField DataField="codigo_ctr" HeaderText="ID" HeaderStyle-Width="5%"  />                   
                   <asp:BoundField DataField="descripcion_ctr" HeaderText="TR&Aacute;MITE" HeaderStyle-Width="37%"  />                   
                   <%--  <asp:TemplateField ItemStyle-HorizontalAlign="left" HeaderText="TR&Aacute;MITE"  ItemStyle-Width=37%  >
                    
                    <ItemTemplate>                                        
                        <asp:LinkButton ID="btnVerTramite"    runat="server"   CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" ForeColor=Black Font-Bold=true>
                        <%#Eval("descripcion_ctr")%>&nbsp;<i class="fa fa-file"></i>
                         <asp:Image ID="Image3" runat="server" ImageUrl="img/busca.gif" Width="10" Height="10"  />
                        </asp:LinkButton>
                    </ItemTemplate>                    
                    </asp:TemplateField>--%>
                    <asp:BoundField DataField="ubicacion_ctr" HeaderText="UBICACI&Oacute;N" HeaderStyle-Width="15%" />
                    <asp:BoundField DataField="nombre_tctr" HeaderText="TIPO TR&Aacute;MITE" HeaderStyle-Width="10%"
                        ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="descripcion_test" HeaderText="TIPO ESTUDIO" HeaderStyle-Width="10%"
                        ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="esEgresado" HeaderText="EGRESADO" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center"  />
                        <asp:BoundField DataField="mostrar" HeaderText="ACTIVO" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center"  />
                     <%-- <asp:BoundField HeaderText="EDITAR" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center" /> --%>
                     
                        <asp:TemplateField HeaderText="ACCIÓN" ShowHeader="False" HeaderStyle-Width="8%">
                        <ItemTemplate>
                        <div class="btn-group">
                        <asp:ImageButton ID="ImgBtnEditar" runat="server" CausesValidation="False"  CssClass="btn btn-warning btn-xs" CommandName="Editar" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"  ImageUrl="../../Images/menus/edit_s.gif" AlternateText="<i class='fa fa-search'></i> Editar" ToolTip="Editar Trámite" />
                        <asp:ImageButton ID="ImgBtnEliminar" runat="server" CausesValidation="False" CssClass="btn btn-danger btn-xs" CommandName="Eliminar"  CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" ImageUrl="../../Images/menus/noconforme_small.gif" alternateText="Eliminar" ToolTip="Eliminar Trámite" OnClientClick="return confirm('¿Desea Eliminar Registro?.')"  />
                        </div>
                        </ItemTemplate>
                    
                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                
                    </asp:TemplateField>                   
                       <asp:TemplateField HeaderText="OPCIÓN" ShowHeader="False" HeaderStyle-Width="5%">
                            <ItemTemplate>                                
                                <asp:DropDownList ID="ddl_data" runat="server"  OnSelectedIndexChanged="ddl_dat_SelectedIndexChanged"   AutoPostBack=true  >
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                       <%--  <asp:CommandField  ButtonType="Link"  ControlStyle-CssClass="btn btn-warning btn-sm" HeaderText="Editar" ShowHeader="True" ShowSelectButton="True" SelectText="<i aria-hidden='true' class='fa fa-edit'></i>" ></asp:CommandField> --%>
                </Columns>
                <EmptyDataTemplate>
                    No se registró ningún concepto tr&aacute;mite
                </EmptyDataTemplate>
                <HeaderStyle BackColor="#E33439" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center"
                    Font-Size="10px" />
                <EditRowStyle BackColor="#FFFFCC" />
                <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
            </asp:GridView>
        </div>
            </div>
            
            </div>
        
        </asp:Panel>        
        <asp:Panel CssClass="panel panel-default " id="pnlTramiteVirtual"  runat="server" Visible=false >
         <div class="panel panel-heading">
                        <div class="row">
                            <div class="col-md-1">   
                                    <asp:LinkButton ID="btnCancelarTramite" runat="server" Text='<i class="fa fa-arrow-circle-left"></i>'
                                CssClass="btn btn-danger"  Width="100%"></asp:LinkButton>
                            </div>
                            <div class="col-md-10" style="text-align:center">                            
                            <h4 id="tituloPaso" runat="server" style="font-weight:bold"></h4>
                            </div>
                            <div class="col-md-1">
                            <asp:HiddenField ID="hdPaso" runat="server" />                             
                                <asp:LinkButton ID="btnGuardarTramite" runat="server" Text='<i class="fa fa-arrow-circle-right"></i>'
                                CssClass="btn btn-success" Width="100%"></asp:LinkButton>
                            </div>
                         </div>
            </div>  
         <div class="panel panel-body" style="padding:5px">
            <asp:Panel CssClass="panel panel-primary" id="pnlRegistro"  runat="server" Visible=false>
            <div class="panel panel-body">  
            <div class="row">
            <div class="col-md-8">
            <div class="form-group">
            <label class="col-md-2" for="txtTramite">
                Tr&aacute;mite</label>
            <div class="col-md-10">
                <asp:TextBox ID="txtTramite" CssClass="form-control2"  runat="server" Style="text-transform: uppercase;" autocomplete="off"  AutoPostBack="false" TextMode=MultiLine></asp:TextBox>
            </div>
            </div>
            </div>
            <div class="col-md-4">
            <div class="form-group">
                <label class="col-md-3" for="txtUbicacion">
                Ubicaci&oacute;n</label>
                <div class="col-md-9">
                <asp:TextBox ID="txtUbicacion" CssClass="form-control2"  runat="server" Style="text-transform: uppercase;" autocomplete="off"  AutoPostBack="false" TextMode=MultiLine></asp:TextBox>
                </div>
            </div>
            </div>
            </div>
            <div class="row">
            <div class="col-md-4">
            <div class="form-group">
            <label class="col-md-4" for="ddlTipoTramite">
                Tipo</label>
            <div class="col-md-8">
                <asp:DropDownList name="ddlTipoTramite" ID="ddlTipoTramite" runat="server" CssClass="form-control2" AutoPostBack=true>
                    <asp:ListItem Value=""></asp:ListItem>
                </asp:DropDownList>
            </div>
            </div>
            </div>
            <div class="col-md-4">
            <div class="form-group">
            <label class="col-md-4" for="ddlTipoEstudio">
                Tipo Estudio</label>
            <div class="col-md-8">
                <asp:DropDownList name="ddlTipoEstudio" ID="ddlTipoEstudio" runat="server" CssClass="form-control2" AutoPostBack=true>
                    <asp:ListItem Value=""></asp:ListItem>
                </asp:DropDownList>
            </div>
            </div>
            </div>
            <div class="col-md-4">
            <div class="form-group">
            <label class="col-md-3" for="ddlEgresado">
                Egresado</label>
            <div class="col-md-9">
                <asp:DropDownList name="ddlEgresado" ID="ddlEgresado" runat="server" CssClass="form-control2">                                                                     
                    <asp:ListItem Value="0">NO</asp:ListItem> 
                    <asp:ListItem Value="1">SI</asp:ListItem>                                   
                </asp:DropDownList>
            </div>
            </div>
            </div>

            </div>
            <div class="row">
            <div class="col-md-4">
            <div class="form-group">
            <label class="col-md-4" for="ddlSolVirtual">
                Sol.Virtual(S/5)</label>
            <div class="col-md-8">
                <asp:DropDownList name="ddlSolVirtual" ID="ddlSolVirtual" runat="server" CssClass="form-control2" AutoPostBack=true>                                                                     
                    <asp:ListItem Value="1">SI</asp:ListItem>
                    <asp:ListItem Value="0">NO</asp:ListItem>
                </asp:DropDownList>
            </div>
            </div>
            </div>
            <div class="col-md-4">
            <div class="form-group">
            <label class="col-md-4" for="txtPrecio">
                Precio (S/)</label>
            <div class="col-md-8">
                <asp:TextBox ID="txtPrecio" CssClass="form-control2"  runat="server"  autocomplete="off"  AutoPostBack="false" placeholder="S/ 0.00" style="text-align:right" ></asp:TextBox>
            </div>
            </div>
            </div>

            <div class="col-md-4">
            <div class="form-group">
            <label class="col-md-3" for="ddlCantidad">
                Cant.Max</label>
            <div class="col-md-9">
                <asp:DropDownList name="ddlCantidad" ID="ddlCantidad" runat="server" CssClass="form-control2" AutoPostBack=true>                                                                     
                    <asp:ListItem Value="0">0</asp:ListItem> 
                    <asp:ListItem Value="1">1</asp:ListItem>
                    <asp:ListItem Value="2">2</asp:ListItem>
                    <asp:ListItem Value="3">3</asp:ListItem>
                    <asp:ListItem Value="4">4</asp:ListItem>
                    <asp:ListItem Value="5">5</asp:ListItem>
                    <asp:ListItem Value="6">6</asp:ListItem>
                    <asp:ListItem Value="7">7</asp:ListItem>
                    <asp:ListItem Value="8">8</asp:ListItem>
                    <asp:ListItem Value="9">9</asp:ListItem>
                    <asp:ListItem Value="10">10</asp:ListItem>
                    <asp:ListItem Value="11">11</asp:ListItem>
                    <asp:ListItem Value="12">12</asp:ListItem>
                    <asp:ListItem Value="13">13</asp:ListItem>
                    <asp:ListItem Value="14">14</asp:ListItem>
                    
                </asp:DropDownList>
            </div>
            </div>
            </div>
            </div>
            <div class="row">

            <div class="col-md-4">
            <div class="form-group">
                <div class="col-md-12">
                    <div class="btn-group-justified">
                    <asp:LinkButton ID="btnServicio" runat="server" Text='<i class="fa fa-search"></i> Servicio Concepto' CssClass="btn btn-success btn-xs" Width=""></asp:LinkButton>
                    <asp:LinkButton ID="btnQuitarServicio" runat="server" Text='<i class="fa fa-broom"></i> Eliminar' CssClass="btn btn-danger btn-xs" Width=""></asp:LinkButton>
                    </div>
                    <asp:HiddenField ID="txtServicioConceptoCod" runat="server" />
                    <asp:TextBox ID="txtServicioConcepto" CssClass="form-control2"  runat="server" Style="text-transform: uppercase; background-color: lightgray; height: 40px; font-size:x-small" autocomplete="off"  AutoPostBack="false" TextMode="MultiLine" Rows=2 Enabled="false"></asp:TextBox>

                    
                </div>
            </div>
            </div>
            <div class="col-md-4">
            <div class="form-group">           
                <div class="col-md-12">
                <div class="btn-group-justified">
                <asp:LinkButton ID="btnCostos" runat="server" Text='<i class="fa fa-search"></i> Centro Costos' CssClass="btn btn-success btn-xs" Width=""></asp:LinkButton>
                <asp:LinkButton ID="btnQuitarCentroCostos" runat="server" Text='<i class="fa fa-broom"></i> Eliminar' CssClass="btn btn-danger btn-xs" Width=""></asp:LinkButton>
                </div>
                <asp:HiddenField ID="txtCentroCostosCod" runat="server" />
                    <asp:TextBox ID="txtCentroCostos" CssClass="form-control2"  runat="server" Style="text-transform: uppercase; background-color: lightgray; height: 40px; font-size:x-small" autocomplete="off"  AutoPostBack="false" TextMode="MultiLine" Rows=2 Enabled="false"></asp:TextBox>
                    <%--<asp:Button ID="btnQuitarCentroCostos" CssClass="btn btn-danger btn-sm" runat="server" Text="Quitar" ></asp:Button>--%>
                    
                </div>
            </div>
            </div>
            <div class="col-md-4">
            <div class="form-group">
                <label class="col-md-3" for="ddlActividad">
                    Actividad </label>
                <div class="col-md-9">
                 <asp:DropDownList name="ddlActividad" ID="ddlActividad" runat="server" CssClass="form-control2" AutoPostBack=false>
                  </asp:DropDownList>
               </div>
            </div>
            </div>
            </div>
            <div class="row">
            <div class="col-md-4">
                <div class="form-group">
                    <div class="col-md-12">
                        <div class="btn-group-justified">
                         <asp:LinkButton ID="btnServicioRef" runat="server" Text='<i class="fa fa-search"></i> Serv Conc. Ref.' CssClass="btn btn-success btn-xs" Width=""></asp:LinkButton>
                         <asp:LinkButton ID="btnQuitarServicioRef" runat="server" Text='<i class="fa fa-broom"></i> Eliminar' CssClass="btn btn-danger btn-xs" Width=""></asp:LinkButton>
                         </div>
                        <asp:HiddenField ID="txtServicioConceptoCodRef" runat="server" />
                        <asp:TextBox ID="txtServicioConceptoRef" CssClass="form-control2"  runat="server" Style="text-transform: uppercase; background-color: lightgray; height: 40px; font-size:x-small" autocomplete="off"  AutoPostBack="false" TextMode="MultiLine" Rows=2 Enabled="false"></asp:TextBox>                        
                        
                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="form-group">                        
                    <div class="col-md-12">  
                    <div class="btn-group-justified">     
                    <asp:LinkButton ID="btnCostosRef" runat="server" Text='<i class="fa fa-search"></i> Serv Conc. Ref.' CssClass="btn btn-success btn-xs" Width=""></asp:LinkButton>                          
                    <asp:LinkButton ID="btnQuitarCentroCostosRef" runat="server" Text='<i class="fa fa-broom"></i> Eliminar' CssClass="btn btn-danger btn-xs" Width=""></asp:LinkButton>
                    </div>
                        <asp:HiddenField ID="txtCentroCostosCodRef" runat="server" />
                        <asp:TextBox ID="txtCentroCostosRef" CssClass="form-control2"  runat="server" Style="text-transform: uppercase; background-color: lightgray; height: 40px; font-size:x-small" autocomplete="off"  AutoPostBack="false" TextMode="MultiLine" Rows=2 Enabled="false"></asp:TextBox>                        
                        
                    </div>
                </div>
            </div>
            <div class="col-md-4">
            <div class="form-group">
                <label class="col-md-8" for="txtPrecioScoRef">
                    Precio Ref. (S/)</label>
                <div class="col-md-4">
                    <asp:TextBox ID="txtPrecioScoRef" CssClass="form-control2"  runat="server"  autocomplete="off" style="text-align:right"   AutoPostBack="false" placeholder="S/ 0.00" ></asp:TextBox>
                </div>
            </div>
            </div>           
            </div>
            <div class="row">
            <div class="col-md-4">
            
            
             <div class="form-group">
					    <div class="col-md-12">
						    <div class="input-group" style="text-align:justify">
							    <span class="input-group-addon addon-left" style="background-color:White">										
								    <asp:CheckBox ID="chkEstadoActivo" name="chkEstadoActivo" runat="server" />
								    <label for="chkEstadoActivo" class="margin-zero"><span></span></label>
							    </span>
							    <label class="form-control" for="chkEstadoActivo">Para alumnos activos</label>								
						    </div>
					    </div>
                    </div>
            </div>
            <div class="col-md-4">
             <div class="form-group">
					    <div class="col-md-12">
						    <div class="input-group" style="text-align:justify">
							    <span class="input-group-addon addon-left" style="background-color:White">										
								    <asp:CheckBox ID="chkTieneReglas" name="chkTieneReglas" runat="server" />
								    <label for="chkTieneReglas" class="margin-zero"><span></span></label>
							    </span>
							    <label class="form-control" for="chkTieneReglas">Tiene reglas</label>								
						    </div>
					    </div>
                    </div>
            </div>
            <div class="col-md-4">
                    <div class="form-group">
					    <div class="col-md-12">
						    <div class="input-group" style="text-align:justify">
							    <span class="input-group-addon addon-left" style="background-color:White">										
								    <asp:CheckBox ID="chkTieneFlujo" name="chkTieneFlujo" runat="server" />
								    <label for="chkTieneFlujo" class="margin-zero"><span></span></label>
							    </span>
							    <label class="form-control" for="chkTieneFlujo">Tiene Flujo Evaluaci&oacute;n</label>								
						    </div>
					    </div>
                    </div>
            </div>
            </div>
            <div class="row">
            <div class="col-md-4">
                    <div class="form-group">
					    <div class="col-md-12">
						    <div class="input-group" style="text-align:justify">
							    <span class="input-group-addon addon-left" style="background-color:White">										
								    <asp:CheckBox ID="chkTieneCarrerasAsociadas" name="chkTieneCarrerasAsociadas" runat="server" />
								    <label for="chkTieneCarrerasAsociadas" class="margin-zero"><span></span></label>
							    </span>
							    <label class="form-control" for="chkTieneCarrerasAsociadas">Tiene Carreras Asociadas</label>								
						    </div>
					    </div>
                    </div>
            </div>
            <div class="col-md-4">
             <div class="form-group">
					    <div class="col-md-12">
						    <div class="input-group" style="text-align:justify">
							    <span class="input-group-addon addon-left" style="background-color:White">										
								    <asp:CheckBox ID="chkTieneArchivo" name="chkTieneCarrerasAsociadas" runat="server" />
								    <label for="chkTieneArchivo" class="margin-zero"><span></span></label>
							    </span>
							    <label class="form-control" for="chkTieneArchivo">Obligar subir archivo adjunto</label>								
						    </div>
					    </div>
                    </div>
            </div>
            <div class="col-md-4">
            <div class="form-group">
					    <div class="col-md-12">
						    <div class="input-group" style="text-align:justify">
							    <span class="input-group-addon addon-left" style="background-color:White">										
								    <asp:CheckBox ID="chkTieneRequisitos" name="chkTieneRequisitos" runat="server" />
								    <label for="chkTieneRequisitos" class="margin-zero"><span></span></label>
							    </span>
							    <label class="form-control" for="chkTieneRequisitos">Tiene Requisitos</label>								
						    </div>
					    </div>
                    </div>
            </div>
            </div>
            <div class="row">
            <div class="col-md-4">
            <div class="form-group">
					    <div class="col-md-12">
						    <div class="input-group" style="text-align:justify">
							    <span class="input-group-addon addon-left" style="background-color:White">										
								    <asp:CheckBox ID="chkTieneNotaAbono" name="chkTieneNotaAbono" runat="server" />
								    <label for="chkTieneNotaAbono" class="margin-zero"><span></span></label>
							    </span>
							    <label class="form-control" for="chkTieneNotaAbono">Nota de Abono Automática</label>								
						    </div>
					    </div>
                    </div>
            </div>
            <div class="col-md-4">
             <div class="form-group">
					    <div class="col-md-12">
						    <div class="input-group" style="text-align:justify">
							    <span class="input-group-addon addon-left" style="background-color:White">										
								    <asp:CheckBox ID="chkTieneMensajeInformativo" name="chkTieneMensajeInformativo" runat="server" />
								    <label for="chkTieneMensajeInformativo" class="margin-zero"><span></span></label>
							    </span>
							    <label class="form-control" for="chkTieneMensajeInformativo">Tiene Mensaje Informativo</label>								
						    </div>
					    </div>
                    </div>
            </div>
            <div class="col-md-4" id="divGyT" runat="server" visible=false>
            <div class="form-group">
                <label class="col-md-4" for="ddlTieneDGyT">
                Denominación GyT</label>
                <div class="col-md-8">
                    <asp:DropDownList name="ddlTieneDGyT" ID="ddlTieneDGyT" runat="server" CssClass="form-control2" AutoPostBack=true>                                                                     
                        
                    </asp:DropDownList>
                </div>
            </div>
            </div>
            </div>
            <div class="row">
                    <div class="col-md-4">
                     <div class="form-group">
					    <div class="col-md-12">
						    <div class="input-group" style="text-align:justify">
							    <span class="input-group-addon addon-left" style="background-color:White">										
								    <asp:CheckBox ID="chkTieneEntrega" name="chkTieneEntrega" runat="server" />
								    <label for="chkTieneEntrega" class="margin-zero"><span></span></label>
							    </span>								
							    <label class="form-control" for="chkTieneEntrega">Tr&aacute;mite con Entrega</label>								
						    </div>
					    </div>
					    <div class="col-md-12">
					        <div class="input-group" style="text-align:justify">
					            <b>Check</b>
                                <ul style="font-size:x-small; text-align:justify">
                                <li>
                                <b><i class='fa fa-check-square'></i></b> El estudiante tiene que acercarse personalmente a la oficina asignada al trámite
                                </li>
                                <li>
                                <b><i class='fa fa-square-full'></i></b> El estudiante solo tiene que verificar por campus estudiante
                                </li>
                                </ul>
					        </div>
					    </div>
                    </div>
                         
                    </div>
            <div class="col-md-8">
            <fieldset >
            <legend style="background-color:khaki; font-size:medium; font-weight:bold; ">Activado Por:</legend>
                    <div class="row">
                            <div class="col-md-12"> 
                            <div class="form-group">    
                            <div class="col-md-12"> 
								<div class="input-group">
									<span class="input-group-addon addon-left" style="background-color:White">
										
										<asp:CheckBox ID="chkActivar" name="chkActivar" runat="server" AutoPostBack="true" />
										<label for="chkActivar" class="margin-zero"><span></span></label>
									</span>	
									<asp:TextBox ID="txtAprobadoPorPersonal" runat="server" CssClass="form-control" Enabled="false" BackColor="#E1F1FB" Font-Bold=true placeholder="Nombre del personal" ></asp:TextBox>
										
								</div>
							</div>
							</div>						
                        </div>
                   

                         <%--   <div class="col-md-8">
                                    <asp:TextBox ID="txtAprobadoPorPersonal" CssClass="form-control2"  runat="server"  
                                    Style="text-transform: uppercase; width:100%;background-color:#E1F1FB"   AutoPostBack="false" placeholder="Nombre del personal"
                                    ReadOnly=true></asp:TextBox>

                              
                            </div>--%>
                    </div>
            </fieldset>
            </div>
            </div>
            </div>
         </asp:Panel>
            <asp:Panel CssClass="panel panel-default" id="pnlFlujoTramite"  runat="server" >          
          <asp:Panel CssClass="panel panel-primary" id="pnlFlujoTramiteLista"  runat="server" >
            <asp:Panel CssClass="panel panel-default " id="pnlbotoneraFlujoLista"  runat="server" style="margin: 0px"  > 
               <div class="panel panel-heading">
                   <div class="row" >   
                        <div class="col-md-6">
                                        <asp:LinkButton ID="btnCrearFlujoTramite" runat="server" Text='<i class="fa fa-plus"></i>' CssClass="btn btn-default" Width="100%"></asp:LinkButton>
                        </div>                         
                        <div class="col-md-6" style="text-align:center">                            
                                        <asp:LinkButton ID="btnConsultarFlujoTramite" runat="server" Text='<i class="fa fa-search"></i>' CssClass="btn btn-info"  Width="100%"></asp:LinkButton>
                        </div>                           
                  </div>
                  </div>
             </asp:Panel>
            <div class="panel panel-body">          
             <div class="row">
            <div class="col-md-12">
                <div class="form-group">
					    <div class="col-md-12">
						    <div class="input-group" style="text-align:justify">
							    <span class="input-group-addon addon-left" style="background-color:White">										
								    <asp:CheckBox ID="chkMostrarFlujoTramiteActivo" name="chkMostrarFlujoTramiteActivo" runat="server" Checked=true />
								    <label for="chkMostrarFlujoTramiteActivo" class="margin-zero"><span></span></label>
							    </span>
							    <label class="form-control" for="chkMostrarFlujoTramiteActivo">Mostrar solo activos</label>								
						    </div>
					    </div>
                </div>
            </div>
            </div>
             <div class="row">          
          <asp:GridView ID="gvFlujoTramite" runat="server" AutoGenerateColumns="False" 
                    DataKeyNames="codigo_ftr,codigo_ctr,codigo_tfu"  CssClass="table table-sm table-bordered table-hover" GridLines="None" RowStyle-Font-Size=X-Small>
                    <Columns>
                        <asp:BoundField DataField="descripcion_Tfu" HeaderText="PERFIL DE EVALUACION" >
                            <HeaderStyle Font-Bold="True"  />
                            <ItemStyle Width="40%" HorizontalAlign="left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="orden_ftr" HeaderText="ORDEN" >
                            <HeaderStyle Font-Bold="True"  />
                            <ItemStyle Width="5%" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="estadodesc" HeaderText="ESTADO">
                            <ItemStyle Width="5%" HorizontalAlign="left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="verDetAcad_ftrdesc" HeaderText="VER DEUDAS">
                        <ItemStyle Width="10%" HorizontalAlign="left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="verDetAdm_ftrdesc" HeaderText="VER NOTAS">
                        <ItemStyle Width="10%" HorizontalAlign="left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="accionUrl_ftrdesc" HeaderText="PROCESA">
                        <ItemStyle Width="5%" HorizontalAlign="left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="proceso" HeaderText="PROCESO">
                        <ItemStyle Width="15%" HorizontalAlign="left" />
                        </asp:BoundField>
                         <asp:TemplateField HeaderText="ACCIÓN" ShowHeader="False" HeaderStyle-Width="10%">
                            <ItemTemplate>                        
                                <asp:ImageButton ID="BtnEditarFlujo" runat="server" CausesValidation="False"  CssClass="btn btn-warning btn-sm" CommandName="Editar" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"  ImageUrl="../../Images/menus/edit_s.gif" AlternateText="<i class='fa fa-search'></i> Editar" ToolTip="Editar Flujo Trámite" />
                                <asp:ImageButton ID="BtnEliminarFlujo" runat="server" CausesValidation="False"  CssClass="btn btn-danger btn-sm" CommandName="Eliminar" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"  ImageUrl="../../Images/menus/Eliminar_s.gif" AlternateText="<i class='fa fa-trash-alt'></i> Editar" ToolTip="Eliminar Flujo Trámite" Visible=false />                         
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                        </asp:TemplateField>
                       </Columns>
                        <EmptyDataTemplate>
                        No se ha registrado ningún flujo tr&aacute;mite
                        </EmptyDataTemplate>
                        <HeaderStyle BackColor="#E33439" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center"
                    Font-Size="10px" />                        
                        <AlternatingRowStyle BackColor="#F7F6F4"></AlternatingRowStyle>
                            <EditRowStyle BackColor="#FFFFCC" />
                            <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                </asp:GridView>
          </div>
           </div>
          </asp:Panel>
          <asp:Panel CssClass="panel panel-primary" id="pnlFlujoTramiteRegistro"  runat="server" Visible="false">   
            <asp:Panel CssClass="panel panel-default " id="pnlbotoneraFlujoRegistro"  runat="server" style="margin: 0px"   > 
             <div class="panel panel-heading">
              <div class="row">   
                    <div class="col-md-6" style="text-align:center">                            
                    <asp:LinkButton ID="btnCancelarFlujoTramite" runat="server" Text='<i class="fa fa-window-close"></i>' CssClass="btn btn-danger"  Width="100%"></asp:LinkButton>
                    </div>  
                    <div class="col-md-6">
                    <asp:LinkButton ID="btnGuardarFlujoTramite" runat="server" Text='<i class="fa fa-save"></i>' CssClass="btn btn-success" Width="100%"></asp:LinkButton>
                    </div>                         
                                          
              </div>
              </div>
            </asp:Panel>
          
            <div class="panel panel-body"> 
            
            <div class="row">
            <div class="col-md-4">
                <div class="form-group">
                        <label class="col-md-2" for="ddlFuncion_ft">Perfil </label>
                        <div class="col-md-10">
                                <div class="input-group btn-group">
                                     <span class="input-group-btn">										
										<asp:LinkButton ID="btnAgregarFuncionFT" runat="server" Text='<i class="fa fa-plus"></i>' CssClass="btn btn-default"  Width="100%"></asp:LinkButton>
									 </span>
									  <asp:DropDownList name="ddlFuncion_ft" ID="ddlFuncion_ft" runat="server" CssClass="form-control" style="font-size:10px"> 
									  </asp:DropDownList>  
									  <span class="input-group-btn">										
										<asp:LinkButton ID="btnActualizarFuncionFT" runat="server" Text='<i class="fa fa-sync"></i>' CssClass="btn btn-info"  Width="100%"></asp:LinkButton>
									 </span>                        
								</div>
					    </div>
                </div>
            </div>
            <div class="col-md-4">
                        <div class="form-group">                        
                         <label class="col-md-4" for="ddlOrden_ft">Orden</label>
                         <div class="col-md-8">
                            <asp:DropDownList name="ddlOrden_ft" ID="ddlOrden_ft" runat="server" CssClass="form-control">                                                                                                 
                                    <asp:ListItem Value="1">1</asp:ListItem>
                                    <asp:ListItem Value="2">2</asp:ListItem>
                                    <asp:ListItem Value="3">3</asp:ListItem>
                                    <asp:ListItem Value="4">4</asp:ListItem>
                                    <asp:ListItem Value="5">5</asp:ListItem>
                                    <asp:ListItem Value="6">6</asp:ListItem>
                                    <asp:ListItem Value="7">7</asp:ListItem>                            
                            </asp:DropDownList>
                        </div>
                </div>
            </div>
              <div class="col-md-4">
                <div class="form-group">    
                            <label class="col-md-4" for="chkProcesa_ft">Procesa</label>
							<div class="col-md-8">
								<div class="input-group">
									<span class="input-group-addon addon-left" style="background-color:White">
										
										<asp:CheckBox ID="chkProcesa_ft" name="chkProcesa_ft" runat="server" AutoPostBack="true" />
										<label for="chkProcesa_ft" class="margin-zero"><span></span></label>
									</span>	
									<asp:TextBox ID="txtproceso_ft" runat="server" CssClass="form-control" Enabled="false" BackColor="lightgray" placeholder="Proceso a ejecutar" ></asp:TextBox>
										
								</div>
							</div>						
                </div>
              </div>
            </div>
 
             <div class="row">
                <div class="col-md-4">
                    <div class="form-group">
					    <div class="col-md-12">
						    <div class="input-group" style="text-align:justify">
							    <span class="input-group-addon addon-left" style="background-color:White">										
								    <asp:CheckBox ID="chkVerDatosAdm_ft" name="chkVerDatosAdm_ft" runat="server" />
								    <label for="chkVerDatosAdm_ft" class="margin-zero"><span></span></label>
							    </span>
							    <label class="form-control" for="chkVerDatosAdm_ft">Ver deudas</label>								
						    </div>
					    </div>
                    </div>
                </div>
                 <div class="col-md-4">
                            <div class="form-group"> 
                               <div class="col-md-12">
								        <div class="input-group" style="text-align:justify">
									        <span class="input-group-addon addon-left" style="background-color:White">
									            <asp:CheckBox ID="chkVerDatosAcad_ft" name="chkVerDatosAcad_ft" runat="server" />										
										        <label for="chkVerDatosAcad_ft" class="margin-zero"><span></span></label>
									        </span>
							                <label for="chkVerDatosAcad_ft" class="form-control">Ver notas</label>
									    </div>
						        </div>
                            </div>
                  </div>
                 <div class="col-md-4">
                            <div class="form-group">  
                                <div class="col-md-12">
                                <div class="input-group" style="text-align:justify">
                                    <span class="input-group-addon addon-left" style="background-color:White">										
										<asp:CheckBox ID="chkEstado_ft" name="chkEstado_ft" runat="server" />
										<label for="chkEstado_ft" class="margin-zero"><span></span></label>
									</span>	
							       	<label for="chkEstado_ft" class="form-control">
									Activar									
									</label>
							    </div>
							    </div>						
                            </div>
                </div>                
            </div>
            <div class="row">
            <div class="col-md-4">
                    <div class="form-group">
					    <div class="col-md-12">
						    <div class="input-group" style="text-align:justify">
							    <span class="input-group-addon addon-left" style="background-color:White">										
								    <asp:CheckBox ID="chkEvaluaotroPer_ft" name="chkEvaluaotroPer_ft" runat="server" />
								    <label for="chkEvaluaotroPer_ft" class="margin-zero"><span></span></label>
							    </span>
							    <label class="form-control" for="chkEvaluaotroPer_ft">Evalua otro personal</label>								
						    </div>
					    </div>
                    </div>
                </div>
            </div>
            <fieldset>
            <legend style="background-color:khaki">&nbsp;<i class="fa fa-envelope-square"></i> Notificaciones Por Correo Electr&oacute;nico</legend>
            <div class="row">
              <div class="col-md-4">
                <div class="form-group">                               
							<div class="col-md-12">
								<div class="input-group" style="text-align:justify">
									<span class="input-group-addon addon-left" style="background-color:White">										
										<asp:CheckBox ID="chkEmailAprobacion_ft" name="chkEmailAprobacion_ft" runat="server" />
										<label for="chkEmailAprobacion_ft" class="margin-zero"><span></span></label>
									</span>	
									<label for="chkEmailAprobacion_ft" class="form-control">
									Aprobar evaluaci&oacute;n									
									</label>
									<span class="input-group-addon addon-right" class="form-control">										
									<i class="fa fa-envelope-square"></i>
									</span>									
								</div>
							</div>						
                </div>
              </div>
              
              <div class="col-md-4">
                <div class="form-group">                               
							<div class="col-md-12">
								<div class="input-group" style="text-align:justify">
									<span class="input-group-addon addon-left" style="background-color:White">										
										<asp:CheckBox ID="chkEmailRechazo_ft" name="chkEmailRechazo_ft" runat="server" />
										<label for="chkEmailRechazo_ft" class="margin-zero"><span></span></label>
									</span>
									<label for="chkEmailRechazo_ft" class="form-control">
									Rechazar la evaluaci&oacute;n									
									</label>
									<span class="input-group-addon addon-right" class="form-control">										
									<i class="fa fa-envelope-square"></i>
									</span>									
								</div>
							</div>						
                </div>
              </div>
              
               <div class="col-md-4">
                <div class="form-group">                               
							<div class="col-md-12">
								<div class="input-group" style="text-align:justify">
									<span class="input-group-addon addon-left" style="background-color:White">										
										<asp:CheckBox ID="chkEmailPlanEstudio_ft" name="chkEmailPlanEstudio_ft" runat="server" />
										<label for="chkEmailPlanEstudio_ft" class="margin-zero"><span></span></label>
									</span>
									<label for="chkEmailPlanEstudio_ft" class="form-control">
									Modificar plan de estudio.								
									</label>
									<span class="input-group-addon addon-right">										
									<i class="fa fa-envelope-square"></i>
									</span>									
								</div>
							</div>						
                </div>
              </div>
              
              </div>
              <div class="row">
              
               <div class="col-md-4">
                <div class="form-group">                               
							<div class="col-md-12">
								<div class="input-group" style="text-align:justify">
									<span class="input-group-addon addon-left" style="background-color:White">										
										<asp:CheckBox ID="chkEmailPrecioCredito_ft" name="chkEmailPrecioCredito_ft" runat="server" />
										<label for="chkEmailPrecioCredito_ft" class="margin-zero"><span></span></label>
									</span>
									<label for="chkEmailPrecioCredito_ft" class="form-control">
									Modificar precio cr&eacute;dito.								
									</label>
									<span class="input-group-addon addon-right">										
									<i class="fa fa-envelope-square"></i>
									</span>
								</div>
							</div>						
                </div>
              </div>
                 <div class="col-md-4">
                <div class="form-group">                               
							<div class="col-md-12">
								<div class="input-group" style="text-align:justify">
									<span class="input-group-addon addon-left" style="background-color:White">										
										<asp:CheckBox ID="chkEmailGyt_ft" name="chkEmailGyt_ft" runat="server" />
										<label for="chkEmailGyt_ft" class="margin-zero"><span></span></label>
									</span>
									<label for="chkEmailGyt_ft" class="form-control">
									Grados y Titulos.								
									</label>
									<span class="input-group-addon addon-right">										
									<i class="fa fa-envelope-square"></i>
									</span>									
								</div>
							</div>						
                </div>
              </div>
            </div>
            </fieldset>
            <fieldset>
            <legend style="background-color:khaki">&nbsp;<i class="fa fa-mobile-alt"></i> Notificaciones Por Mensaje de Texto</legend>
            <div class="row">
             <div class="col-md-4">
                <div class="form-group">                               
							<div class="col-md-12">
								<div class="input-group" style="text-align:justify">
									<span class="input-group-addon addon-left" style="background-color:White">										
										<asp:CheckBox ID="chkmsnaprobacion_ft" name="chkmsnaprobacion_ft" runat="server" />
										<label for="chkmsnaprobacion_ft" class="margin-zero"><span></span></label>
									</span>
									<label for="chkmsnaprobacion_ft" class="form-control">
									Aprobar evaluaci&oacute;n							
									</label>
									<span class="input-group-addon addon-right">										
									<i class="fa fa-mobile-alt"></i>
									</span>									
								</div>
							</div>						
                </div>
              </div>
              <div class="col-md-4">
                <div class="form-group">                               
							<div class="col-md-12">
								<div class="input-group" style="text-align:justify">
									<span class="input-group-addon addon-left" style="background-color:White">										
										<asp:CheckBox ID="chkmsnrechazar_ft" name="chkmsnrechazar_ft" runat="server" />
										<label for="chkmsnrechazar_ft" class="margin-zero"><span></span></label>
									</span>
									<label for="chkmsnrechazar_ft" class="form-control">
									Rechazar evaluaci&oacute;n							
									</label>
									<span class="input-group-addon addon-right">										
									<i class="fa fa-mobile-alt"></i>
									</span>									
								</div>
							</div>						
                </div>
              </div>
            </div>
            </fieldset>            
          </div>
          </asp:Panel>
          </asp:Panel>          
            <asp:Panel CssClass="panel panel-default" id="pnlCarreraAsociada"  runat="server" >      
            <div class="panel panel-body"> 
            <div class="row">
             <fieldset>
             <legend style="background-color:khaki; font-size:medium; font-weight:bold; ">&nbsp;<i class="fa fa-tasks"></i> Listado de Carreras Profesionales</legend>
             <div class="col-md-12">             
               <div class="row">                    
                         <div class="col-md-7">
                           <div class="form-group">
                            <label class="col-md-5" for="ddlTipoEstudioBsqmd">
                                Tipo Estudio</label>
                            <div class="col-md-7">
                                <asp:DropDownList name="ddlTipoEstudioBsqmd" ID="ddlTipoEstudioBsqmd" runat="server" CssClass="form-control2" AutoPostBack=true>
                                    <asp:ListItem Value=""></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            </div>
                         </div>                            
                         <div class="col-md-2">           
                            <asp:LinkButton ID="btnBuscarCarreraProfesional" runat="server" Text='<i class="fa fa-search"></i>'
                            CssClass="btn btn-primary btn-sm" ></asp:LinkButton>
                         </div>
                         <div class="col-md-3">   
                           <div class="form-group">
                            <div class="col-md-12">
                            <label class="col-md-8" for="ddlTipoEstudioBsqmd">
                                 Total Asignados </label>					           
					        <div class="col-md-4">
					            <span class="badge" id="spTotalCpfAsoc" runat="server">0</span>				           
				            </div>
                            </div>
                           </div>
                         </div>                             
                </div>
                <div class="row">
                    <div class="col-md-12">                         
                        
                          <asp:GridView ID="grwCarreraProfesional" runat="server" AutoGenerateColumns="false" DataKeyNames="codigo_cpf, nombre_Cpf,sel"
                            CssClass="table table-sm table-bordered table-hover" GridLines="None" RowStyle-Font-Size="X-Small">
                    <Columns>
                         <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center"  >     
                                                    
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <HeaderTemplate>  
                                                        <asp:CheckBox ID="chkall" AutoPostBack="true" OnCheckedChanged="chckchanged" runat="server" Text="" />
                                                        </HeaderTemplate>
                                                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                        <ItemTemplate  >
                                                            <asp:CheckBox ID="chkElegir" runat="server" AutoPostBack="true" />                                                            
                                                        </ItemTemplate>                                                        
                                                        <ItemStyle HorizontalAlign="Center"  />                                                    
                                                    </asp:TemplateField>
                        <asp:BoundField DataField="nombre_Cpf" HeaderText="CARRERA PROFESIONAL" HeaderStyle-Width="95%"   />
                    </Columns>
                    <EmptyDataTemplate>
                        No se ha encontrado carrera profesional
                    </EmptyDataTemplate>
                    <HeaderStyle BackColor="#E33439" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center"
                        Font-Size="10px" />
                    <EditRowStyle BackColor="#FFFFCC" />
                    <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                    </asp:GridView>
                    </div>  
                
                </div> 
             </div>             
             </fieldset>             
            
           </div>           
           </div>
          </asp:Panel> 
          
          <asp:Panel ID="pnlMensajeInformativo" runat="server" >
                <asp:Panel id="pnlMensajeInformativoLista" CssClass="panel panel-primary"   runat="server" >
                  <asp:Panel CssClass="panel panel-default" id="pnlMensajeInformativoListaBtn"  runat="server" style="margin: 0px"  > 
                   <div class="panel panel-heading">
                       <div class="row" >   
                            <div class="col-md-6">
                                            <asp:LinkButton ID="btnCrearMensajeInfo" runat="server" Text='<i class="fa fa-plus"></i>' CssClass="btn btn-default" Width="100%"></asp:LinkButton>
                            </div>                         
                            <div class="col-md-6" style="text-align:center">                            
                                            <asp:LinkButton ID="btnConsultarMensajeInfo" runat="server" Text='<i class="fa fa-search"></i>' CssClass="btn btn-info"  Width="100%"></asp:LinkButton>
                            </div>                           
                      </div>
                    </div>
                 </asp:Panel>
                 
                   <div class="panel panel-default">
                    <div class="panel panel-body">          
                     <div class="row">
                    <div class="col-md-12">
                    <div class="form-group">
					            <div class="col-md-12">
						            <div class="input-group" style="text-align:justify">
							            <span class="input-group-addon addon-left" style="background-color:White">										
								            <asp:CheckBox ID="chkMostrarMensajeInformativoActivo" name="chkMostrarMensajeInformativoActivo" runat="server" Checked=true />
								            <label for="chkMostrarMensajeInformativoActivo" class="margin-zero"><span></span></label>
							            </span>
							            <label class="form-control" for="chkMostrarMensajeInformativoActivo">Mostrar solo activos</label>								
						            </div>
					            </div>
                            </div>
                    </div>
                    </div>
                             <div class="row">          
                              <asp:GridView ID="grwMensajeInformativo" runat="server" AutoGenerateColumns="False" 
                                        DataKeyNames="codigo_mctr,codigo_ctr"  CssClass="table table-sm table-bordered table-hover" GridLines="None" RowStyle-Font-Size=X-Small>
                                        <Columns>
                                            <asp:BoundField DataField="descripcion" HeaderText="DESCRIPCION" >
                                                <HeaderStyle Font-Bold="True" />
                                                <ItemStyle Width="40%" HorizontalAlign="left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="orden" HeaderText="ORDEN" >
                                                <HeaderStyle Font-Bold="True"  />
                                                <ItemStyle Width="5%" HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="estadodesc" HeaderText="ESTADO">
                                                <ItemStyle Width="5%" HorizontalAlign="left" />
                                            </asp:BoundField>                        
                                             <asp:TemplateField HeaderText="ACCIÓN" ShowHeader="False" HeaderStyle-Width="10%">
                                                <ItemTemplate>                        
                                                    <asp:ImageButton ID="BtnEditarMsjInfo" runat="server" CausesValidation="False"  CssClass="btn btn-warning btn-sm" CommandName="Editar" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"  ImageUrl="../../Images/menus/edit_s.gif" AlternateText="<i class='fa fa-search'></i> Editar" ToolTip="Editar Mensaje Informativo" />
                                                    <asp:ImageButton ID="BtnEliminarMsjInfo" runat="server" CausesValidation="False"  CssClass="btn btn-danger btn-sm" CommandName="Eliminar" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"  ImageUrl="../../Images/menus/Eliminar_s.gif" AlternateText="<i class='fa fa-trash-alt'></i> Editar" ToolTip="Eliminar Mensaje Informativo" Visible=false />                         
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                            </asp:TemplateField>
                                           </Columns>
                                            <EmptyDataTemplate>
                                            No se ha registrado ningún mensaje informativo
                                            </EmptyDataTemplate>
                                            <HeaderStyle BackColor="#E33439" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center"
                                        Font-Size="10px" />                        
                                            <AlternatingRowStyle BackColor="#F7F6F4"></AlternatingRowStyle>
                                                <EditRowStyle BackColor="#FFFFCC" />
                                                <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                    </asp:GridView>
          </div>
                    </div>
                   </div>
                 
                </asp:Panel>
                <asp:Panel CssClass="panel panel-primary" id="pnlMensajeInformativoRegistro"  runat="server"  >
                 <asp:Panel CssClass="panel panel-default " id="pnlMensajeInformativoRegistroBtn"  runat="server" style="margin: 0px"   > 
                     <div class="panel panel-heading">
                      <div class="row">   
                            <div class="col-md-6" style="text-align:center">                            
                            <asp:LinkButton ID="btnCancelarMensajeInfo" runat="server" Text='<i class="fa fa-window-close"></i>' CssClass="btn btn-danger"  Width="100%"></asp:LinkButton>
                            </div>  
                            <div class="col-md-6">
                            <asp:LinkButton ID="btnGuardarMensajeInfo" runat="server" Text='<i class="fa fa-save"></i>' CssClass="btn btn-success" Width="100%"></asp:LinkButton>
                            </div>                         
                                                  
                      </div>
                      </div>
                    </asp:Panel>
                    <div class="panel panel-body"> 
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                        <label class="col-md-2" for="txtDescripcion_msjinfo">Descripcíón </label>
                                        <div class="col-md-10">
                                               <asp:TextBox ID="txtDescripcion_msjinfo" runat="server" CssClass="form-control" TextMode=MultiLine Rows=3></asp:TextBox>
					                    </div>
                                </div>
                            </div>
                            </div>
                            <div class="row">
                            
                            <div class="col-md-6">
                                        <div class="form-group">                        
                                         <label class="col-md-4" for="ddlOrden_msjinfo">Orden</label>
                                         <div class="col-md-8">
                                            <asp:DropDownList name="ddlOrden_msjinfo" ID="ddlOrden_msjinfo" runat="server" CssClass="form-control">                                                                                                 
                                                    <asp:ListItem Value="1">1</asp:ListItem>
                                                    <asp:ListItem Value="2">2</asp:ListItem>
                                                    <asp:ListItem Value="3">3</asp:ListItem>
                                                    <asp:ListItem Value="4">4</asp:ListItem>
                                                    <asp:ListItem Value="5">5</asp:ListItem>
                                                    <asp:ListItem Value="6">6</asp:ListItem>
                                                    <asp:ListItem Value="7">7</asp:ListItem>
                                                    <asp:ListItem Value="8">8</asp:ListItem>                            
                                                    <asp:ListItem Value="9">9</asp:ListItem> 
                                                    <asp:ListItem Value="10">10</asp:ListItem> 
                                                    <asp:ListItem Value="11">11</asp:ListItem> 
                                                    <asp:ListItem Value="12">12</asp:ListItem>
                                                    <asp:ListItem Value="13">13</asp:ListItem>
                                                    <asp:ListItem Value="14">14</asp:ListItem>
                                                    <asp:ListItem Value="15">15</asp:ListItem>
                                                    <asp:ListItem Value="16">16</asp:ListItem>
                                                    <asp:ListItem Value="17">17</asp:ListItem>
                                                    <asp:ListItem Value="18">18</asp:ListItem> 
                                            </asp:DropDownList>
                                        </div>
                                </div>
                                 </div> 
                                <div class="col-md-6">
                                                <div class="form-group">
					                                <div class="col-md-12">
						                                <div class="input-group" style="text-align:justify">
							                                <span class="input-group-addon addon-left" style="background-color:White">										
								                                <asp:CheckBox ID="chkActivo_msjinfo" name="chkActivo_msjinfo" runat="server" Checked=true />
								                                <label for="chkActivo_msjinfo" class="margin-zero"><span></span></label>
							                                </span>                            								
							                                <label class="form-control" for="chkActivo_msjinfo">Activo</label>								
						                                </div>
					                                </div>
                                                </div>
                               </div>
                                
                          
                        </div>
                    </div>
                </asp:Panel>
            </asp:Panel>
          
          <asp:Panel ID="pnlReglas" runat="server" >
              <asp:Panel id="pnlReglasLista" CssClass="panel panel-primary"   runat="server" >
                 <div class="panel panel-default">
                    <div class="panel panel-body">
                    
                     <div class="row">
             <fieldset>
             <legend style="background-color:khaki; font-size:medium; font-weight:bold; ">&nbsp;<i class="fa fa-tasks"></i> Listado de Reglas</legend>
             <div class="col-md-12">             
               <div class="row">                                                   
                         <div class="col-md-3">           
                            <asp:LinkButton ID="btnBuscarReglas" Visible=false runat="server" Text='<i class="fa fa-search"></i>'
                            CssClass="btn btn-primary btn-sm" ></asp:LinkButton>
                         </div>
                         <div class="col-md-3">
                         <label style="text-align:justify">Para que las reglas se apliquen debe activar el check Tiene Reglas del trámite</label>
                         </div> 
                         <div class="col-md-6">   
                           <div class="form-group">
                            <div class="col-md-12">
                                <label class="col-md-8" for="sptotalreglas" style="text-align:right">
                                 Total Asignados </label>					           
					        <div class="col-md-4">
					            <span class="badge" id="sptotalreglas" runat="server">0</span>				           
				            </div>
                            </div>
                           </div>
                         </div>                             
                </div>
               <div class="row">          
                                    <asp:GridView ID="grwReglas" runat="server" AutoGenerateColumns="False" 
                                        DataKeyNames="codigo_rctr,codigo_ctr,sel"  CssClass="table table-sm table-bordered table-hover" GridLines="None" RowStyle-Font-Size=X-Small>
                                        <Columns>
                                         <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                        <ItemTemplate  >
                                                            <asp:CheckBox ID="chkElegirRegla" runat="server" AutoPostBack="true" />                                                            
                                                        </ItemTemplate>                                                        
                                                        <ItemStyle HorizontalAlign="Center"  />                                                    
                                                    </asp:TemplateField>
                                            <asp:BoundField DataField="nombreRegla" HeaderText="NOMBRE">
                                                <HeaderStyle Font-Bold="True" />
                                                <ItemStyle Width="30%" HorizontalAlign="left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="descripcionRegla" HeaderText="DESCRIPCION" >
                                                <HeaderStyle Font-Bold="True"  />
                                                <ItemStyle Width="55%" HorizontalAlign="left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="operacion" HeaderText="OPERACIÓN">
                                                <ItemStyle Width="5%" HorizontalAlign="center" />
                                            </asp:BoundField>                        
                                          <asp:BoundField DataField="valor" HeaderText="VALOR">
                                                <ItemStyle Width="5%" HorizontalAlign="center" />
                                            </asp:BoundField>  
                                           </Columns>
                                            <EmptyDataTemplate>
                                            No se ha registrado ninguna regla
                                            </EmptyDataTemplate>
                                            <HeaderStyle BackColor="#E33439" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center"
                                        Font-Size="10px" />                        
                                            <AlternatingRowStyle BackColor="#F7F6F4"></AlternatingRowStyle>
                                                <EditRowStyle BackColor="#FFFFCC" />
                                                <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                 </asp:GridView>
                    </div>
                    </div>
                    </fieldset>
                    </div>
                    
                    </div>
                   </div>
               
              </asp:Panel>
          </asp:Panel>     
                
         </div>            
        </asp:Panel>         
    </div>
    <asp:Panel CssClass="panel panel-default" id="pnlBuscarServicio"  runat="server" Visible="false">
                    <div class="panel panel-heading">
                    <h5><b>Buscar Servicio Concepto</b></h5>
                    </div>
                    <div class="panel panel-body">  
                     <div class="row">
                     <div class="col-md-1">
                       <asp:LinkButton ID="btnCancelarServicioRef" runat="server" Text='<i class="fa fa-arrow-circle-left"></i>'
                            CssClass="btn btn-danger" OnClick="btnCancelarCentroCosto_Click"></asp:LinkButton>
                     </div>
                     
                         <div class="col-md-7">
                           <div class="form-group">
                                <label class="col-md-4" for="txtServicioBsq">
                                    Descripci&oacute;n</label>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtServicioBsq" CssClass="form-control2"  runat="server"  autocomplete="off" ></asp:TextBox>
                                </div>
                            </div>
                         </div> 
                            <div class="col-md-3">
                           <div class="form-group">
                                <label class="col-md-4" for="txtCodServicio">
                                    Cod</label>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtCodServicio" CssClass="form-control2"  runat="server"  autocomplete="off"  ></asp:TextBox>
                                </div>
                            </div>
                         </div> 
                             <div class="col-md-1" >                                 
                                    <asp:LinkButton ID="btnBuscarServicio" runat="server" Text='<i class="fa fa-search"></i>' 
                                    CssClass="btn btn-primary btn-sm" ></asp:LinkButton>
                                    <asp:LinkButton ID="btnBuscarServicioRef" runat="server" Text='<i class="fa fa-search"></i>' 
                                    CssClass="btn btn-primary btn-sm" ></asp:LinkButton>                           
                             </div>
                        </div>
                     <div class="row">
                         <div class="col-md-12">       
                        
                         <asp:GridView ID="grwServicio" runat="server" AutoGenerateColumns="false" DataKeyNames="codigo_Sco, descripcion_Sco,ref"
                            CssClass="table table-sm table-bordered table-hover" GridLines="None" RowStyle-Font-Size=X-Small>
                        <Columns>
                        <asp:BoundField DataField="descripcion_Sco" HeaderText="DESCRIPCION" HeaderStyle-Width="50%"   />                     
                        <asp:BoundField DataField="precio_Sco" HeaderText="PRECIO" HeaderStyle-Width="15%" />                    
                         <%-- <asp:BoundField HeaderText="EDITAR" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center" /> --%>                     
                        <asp:TemplateField HeaderText="SEL" ShowHeader="False" HeaderStyle-Width="5%">
                        <ItemTemplate>
                            <asp:ImageButton ID="ImgBtnSelServicio" runat="server" CausesValidation="False" CssClass="btn btn-success btn-sm"
                                CommandName="SelServicio" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" ImageUrl="../../Images/menus/download.gif"  alternateText="Seleccionar Servicio" OnClientClick="return confirm('¿Desea Asignar Servicio?')"  />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                    </asp:TemplateField>                   
                            
                           <%--  <asp:CommandField  ButtonType="Link"  ControlStyle-CssClass="btn btn-warning btn-sm" HeaderText="Editar" ShowHeader="True" ShowSelectButton="True" SelectText="<i aria-hidden='true' class='fa fa-edit'></i>" ></asp:CommandField> --%>
                    </Columns>
                    <EmptyDataTemplate>
                        No se ha encontrado servico concepto
                    </EmptyDataTemplate>
                    <HeaderStyle BackColor="#E33439" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center"
                        Font-Size="10px" />
                    <EditRowStyle BackColor="#FFFFCC" />
                    <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                </asp:GridView>
                         </div>                  
                    </div>
                    </div>            
             </asp:Panel>
    <asp:Panel CssClass="panel panel-default" id="pnlBuscarCentroCosto"  runat="server" Visible=false>
                    <div class="panel panel-heading">
                    <h5><b>Buscar Centro de Costos</b></h5>
                    </div>
                    <div class="panel panel-body">  
                     <div class="row">
                     <div class="col-md-1">
                       <asp:LinkButton ID="btnCancelarCentroCosto" runat="server" Text='<i class="fa fa-arrow-circle-left"></i>'
                            CssClass="btn btn-danger" OnClick="btnCancelarCentroCosto_Click"></asp:LinkButton>
                     </div>
                         <div class="col-md-7">
                           <div class="form-group">
                                <label class="col-md-4" for="txtCentroCostoBsq">
                                    Descripci&oacute;n</label>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtCentroCostoBsq" CssClass="form-control2"  runat="server"  autocomplete="off"  AutoPostBack="false" ></asp:TextBox>
                                </div>
                            </div>
                         </div>  
                         <div class="col-md-3">
                           <div class="form-group">
                                <label class="col-md-4" for="txtCodCentroCosto">
                                    Cod</label>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtCodCentroCosto" CssClass="form-control2"  runat="server"  autocomplete="off"  AutoPostBack="false" ></asp:TextBox>
                                </div>
                            </div>
                         </div> 
                             <div class="col-md-1">           
                                <asp:LinkButton ID="btnBuscarCentroCosto" runat="server" Text='<i class="fa fa-search"></i>'
                                CssClass="btn btn-primary" ></asp:LinkButton>
                                <asp:LinkButton ID="btnBuscarCentroCostoRef" runat="server" Text='<i class="fa fa-search"></i>'
                                CssClass="btn btn-primary" ></asp:LinkButton>
                          
                             </div>
                    </div>
                    <div class="row">
                         <div class="col-md-12">
                         
                        
                          <asp:GridView ID="grwCentroCosto" runat="server" AutoGenerateColumns="false" DataKeyNames="codigo_cco, descripcion_cco,ref"
                    CssClass="table table-sm table-bordered table-hover" GridLines="None" RowStyle-Font-Size="X-Small">
                    <Columns>
                        <asp:BoundField DataField="descripcion_cco" HeaderText="DESCRIPCION" HeaderStyle-Width="75%"   />                                                           
                         <%-- <asp:BoundField HeaderText="EDITAR" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center" /> --%>                     
                        <asp:TemplateField HeaderText="SEL" ShowHeader="False" HeaderStyle-Width="5%">
                        <ItemTemplate>
                            <asp:ImageButton ID="ImgBtnSelCentroCosto" runat="server" CausesValidation="False" CssClass="btn btn-success btn-sm"
                                CommandName="SelCentroCosto" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" ImageUrl="../../Images/menus/download.gif"  alternateText="Seleccionar Centro de Costos" OnClientClick="return confirm('¿Desea Asignar Centro de Costos?.')"  />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                    </asp:TemplateField>                   
                            
                           <%--  <asp:CommandField  ButtonType="Link"  ControlStyle-CssClass="btn btn-warning btn-sm" HeaderText="Editar" ShowHeader="True" ShowSelectButton="True" SelectText="<i aria-hidden='true' class='fa fa-edit'></i>" ></asp:CommandField> --%>
                    </Columns>
                    <EmptyDataTemplate>
                        No se ha encontrado centro de costos
                    </EmptyDataTemplate>
                    <HeaderStyle BackColor="#E33439" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center"
                        Font-Size="10px" />
                    <EditRowStyle BackColor="#FFFFCC" />
                    <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                </asp:GridView>
                         </div>                
                    </div>                
                    </div>
             </asp:Panel>
    
     <!-- Modal -->
    <div id="mdConfirmarActualizacion" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content" >
                <div class="modal-header">
                    <h4 class="modal-title"><i class="fa fa-edit"></i> Confirmaci&oacute;n de actualizaci&oacute;n de Datos     </h4>          
                <div class="modal-body">
                    <h4 class="modal-title">         
                            <p>
                            <h4>Se han modificado datos del registro del Trámite...<br /><b>¿Desea actualizar registro?</b></h4>
                            </p>       
                        </div>                    
                </div>
                <div class="modal-footer">
                    <div class="row">
                        <div class="col-md-6">
                            <asp:LinkButton ID="btnCancelaActualizar" runat="server" Text='<i class="fa fa-window-close"></i> Cancelar' CssClass="btn btn-danger" Width="100%" ></asp:LinkButton>
                        </div>
                        <div class="col-md-6">
                            <asp:LinkButton ID="btnAceptaActualizar" runat="server" Text='<i class="fa fa-edit"></i> Aceptar' CssClass="btn btn-info" Width="100%"></asp:LinkButton>
                        </div>
                    </div>
                </div>                
            </div>
        </div>
    </div>
    </form>
</body>
</html>
