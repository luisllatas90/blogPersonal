<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AsignarActivos.aspx.vb" Inherits="administrativo_activofijo_m_Interfaces_AsignarActivos" %>

<!DOCTYPE html>
<html lang="es">
    <head>
        <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
    <meta http-equiv="X-UA-Compatible" content="IE=10" />    
    <title>Registro de Notificaciones</title>

    <!-- Estilos externos -->
    <link rel="stylesheet" href="../../../assets/bootstrap-4.1/css/bootstrap.min.css">
    <link rel="stylesheet" href="../../../assets/smart-wizard/css/smart_wizard.min.css">
    <link rel="stylesheet" href="../../../assets/bootstrap-select-1.13.1/css/bootstrap-select.min.css">    
    <link rel="stylesheet" href="../../../assets/fontawesome-5.2/css/all.min.css">            
    <link rel="stylesheet" href="../../../Alumni/css/datatables/jquery.dataTables.min.css">      
    <link rel="stylesheet" href="../../../Alumni/css/sweetalert/sweetalert2.min.css"> 
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/summernote/0.8.12/summernote-bs4.css">

    <!-- Estilos propios -->        
    <link rel="stylesheet" href="../../../Alumni/css/estilos.css?13">
    <link rel="stylesheet" href="../../../Alumni/css/m_estilos.css">

    <!-- Scripts externos -->
    <script src="../../../assets/jquery/jquery-3.3.1.js"></script>
    <script src="../../../assets/bootstrap-4.1/js/bootstrap.bundle.js"></script>
    <script src="../../../assets/bootstrap-select-1.13.1/js/bootstrap-select.js"></script>            
    <script src="../../../Alumni/js/popper.js"></script>    
    <script src="../../../Alumni/js/sweetalert/es6-promise.auto.min.js"></script>
    <script src="../../../Alumni/js/sweetalert/sweetalert2.js"></script>
    <script src="../../../Alumni/js/datatables/jquery.dataTables.min.js?1"></script> 
    <script src="https://cdnjs.cloudflare.com/ajax/libs/summernote/0.8.12/summernote-bs4.js"></script>
    
    <!-- Scripts propios -->
    <script src="../../../Alumni/js/funciones.js?1"></script>

    </head>
    
    <body>
    
    <!--<div class="loader"></div>-->
    
    <form id="AsignarActivos" runat="server">
    
        <div class="container-fluid">

            <div class="card div-title">                        
                <div class="row title">ASIGNAR ACTIVOS</div>
            </div>
            
            <ul class="nav nav-tabs" role="tablist">
                <li class="nav-item">
                    <a href="#listado" id="listado-tab" class="nav-link active" data-toggle="tab" role="tab"
                        aria-controls="listado" aria-selected="true">Listado</a>
                </li>
                <li class="nav-item">
                    <a href="#edicion" id="edicion-tab" class="nav-link disabled" data-toggle="tab" role="tab"
                        aria-controls="edicion" aria-selected="false">Edición</a>
                </li>
                
                <li class="nav-item">
                    <a href="#responsable" id="responsable-tab" class="nav-link disabled" data-toggle="tab" role="tab"
                        aria-controls="responsable" aria-selected="false">Asignar Responsable</a>
                </li>
            </ul>
            
            <div class="tab-content" id="contentTabs">
                <div class="tab-pane show active" id="listado" role="tabpanel" aria-labelledby="listado-tab">
            
                    <div class="panel-cabecera">
                        <ContentTemplate>
                            <div class="card">
                                <div class="card-header">Lista de Activos fijos</div>
                                <div class="card-body">
                                <br />
                                    <div class="row">
                                        <div class="col-sm-12 text-right">
                                            <asp:LinkButton ID="btnListar" runat="server" CssClass="btn btn-accion btn-verde">
                                                <i class="fa fa-user-check"></i>
                                                <span class="text">Asignar Responsable</span>
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="btnNuevo" runat="server" CssClass="btn btn-accion btn-celeste"                                                    
                                                OnClientClick="return alertConfirm(this, event, '¿Desea registrar una nueva notificación?', 'warning');">
                                                <i class="fa fa-save"></i>
                                                <span class="text">Registrar Activo Fijo</span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                    
                                    <div class="row">
                                    
                                        <div class="col-sm-2">
                                            <asp:DropDownList ID="cmbTipoFiltro" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm combo_filtro" data-live-search="true" AutoComplete="off">
                                                <asp:ListItem Value="">[-- SELECCIONE --]</asp:ListItem>
                                                <asp:ListItem Value="PEDIDO">PEDIDO</asp:ListItem>
                                                <asp:ListItem Value="ARTÍCULO">ARTÍCULO</asp:ListItem>                                                   
                                            </asp:DropDownList>
                                        </div>
                                        
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtBuscar" runat="server" MaxLength="200" CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                        </div>
                                        
                                        <div class="col-sm-4">
                                            <asp:RadioButton ID="RadioButton1" GroupName="RadioGroup1" CssClass="form-control-radio form-control-sm" runat="server"></asp:RadioButton> <label for="cmbProfile" class="col-form-label form-control-sm label-radio">Sin Etiquetar</label>  
                                            <asp:RadioButton ID="RadioButton2" GroupName="RadioGroup1" CssClass="form-control-radio form-control-sm" runat="server"></asp:RadioButton> <label for="cmbProfile" class="col-form-label form-control-sm label-radio">Sin Asignar</label> 
                                            <asp:RadioButton ID="RadioButton3" GroupName="RadioGroup1" CssClass="form-control-radio form-control-sm" runat="server"></asp:RadioButton> <label for="cmbProfile" class="col-form-label form-control-sm label-radio">Registrado</label>                                    
                                        </div>
                                        
                                        <div class="col-sm-2 text-right">
                                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-accion btn-azul">
                                                <i class="fa fa-search"></i>
                                                <span class="text">Consultar</span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                  <br />  
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="table-responsive">
                                            
                                                <table class="table table-sm table-striped">
                                                
                                                    <thead>
                                                        <th>AS. RESP.</th>
                                                        <th>ITEM</th>
                                                        <th>ARTÍCULO</th>
                                                        <th>U. MEDIDA</th>
                                                        <th>ETIQUETA</th>
                                                        <th>VALOR</th>
                                                        <th>ESTADO</th>
                                                        <th>ACCIONES</th>
                                                    </thead>
                                                    
                                                    <tbody>
                                                        <tr>
                                                            <td><input class="form-check-input" type="checkbox" value="" id="defaultCheck1"></td>
                                                            <td>12345</td>
                                                            <td>COMPUTADORA LAPTOP</td>
                                                            <td>UNIDAD</td>
                                                            <td>AF-9876</td>
                                                            <td>1594.99</td>
                                                            <td>SIN ASIGNAR</td>
                                                            <td>
                                                                <asp:LinkButton ID="btnEditar" runat="server" 
                                                                    CommandName="Editar" 
                                                                    CssClass="btn btn-warning btn-sm" 
                                                                    ToolTip="Editar notificación"
                                                                    OnClientClick="return alertConfirm(this, event, '¿Desea editar el registro seleccionado?', 'warning');">
                                                                    <span><i class="fa fa-pen"></i></span>
                                                                </asp:LinkButton>
                                                                <asp:LinkButton ID="btnEliminar" runat="server" 
                                                                    CommandName="Eliminar" 
                                                                    CssClass="btn btn-success btn-sm" 
                                                                    ToolTip="Eliminar notificación"
                                                                    OnClientClick="return alertConfirm(this, event, '¿Desea eliminar el registro seleccionado?', 'error');">
                                                                    <span><i class="fa fa-print"></i></span>
                                                                </asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                        
                                                        <tr>
                                                            <td><input class="form-check-input" type="checkbox" value="" id="Checkbox1"></td>
                                                            <td>12346</td>
                                                            <td>COMPUTADORA LAPTOP</td>
                                                            <td>UNIDAD</td>
                                                            <td></td>
                                                            <td>1594.99</td>
                                                            <td>SIN ETIQUETAR</td>
                                                            <td>
                                                                <asp:LinkButton ID="LinkButton2" runat="server" 
                                                                    CommandName="Editar" 
                                                                    CssClass="btn btn-warning btn-sm" 
                                                                    ToolTip="Editar notificación"
                                                                    OnClientClick="return alertConfirm(this, event, '¿Desea editar el registro seleccionado?', 'warning');">
                                                                    <span><i class="fa fa-pen"></i></span>
                                                                </asp:LinkButton>
                                                                <asp:LinkButton ID="LinkButton3" runat="server" 
                                                                    CommandName="Eliminar" 
                                                                    CssClass="btn btn-success btn-sm" 
                                                                    ToolTip="Eliminar notificación"
                                                                    OnClientClick="return alertConfirm(this, event, '¿Desea eliminar el registro seleccionado?', 'error');">
                                                                    <span><i class="fa fa-print"></i></span>
                                                                </asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                
                                                </table>
                                                
                                                <!--<ContentTemplate>
                                                    <asp:GridView ID="grwLista" runat="server" AutoGenerateColumns="false" ShowHeader="true" DataKeyNames="codigo_not"
                                            CssClass="display table table-sm" GridLines="None">
                                                        <Columns>
                                                            <asp:BoundField DataField="as_resp" HeaderText="AS. RESP." ItemStyle-Width="5%" ItemStyle-Wrap="false" />
                                                            <asp:BoundField DataField="item" HeaderText="ITEM" ItemStyle-Width="5%" ItemStyle-Wrap="false" />
                                                            <asp:BoundField DataField="articulo" HeaderText="ARTÍCULO" ItemStyle-Width="30%" ItemStyle-Wrap="false" />
                                                            <asp:BoundField DataField="u_medida" HeaderText="U. MEDIDA" ItemStyle-Width="5%" ItemStyle-Wrap="false" />
                                                            <asp:BoundField DataField="etiqueta" HeaderText="ETIQUETA" ItemStyle-Width="10%" ItemStyle-Wrap="false" />
                                                            <asp:BoundField DataField="valor" HeaderText="VALOR" ItemStyle-Width="10%" ItemStyle-Wrap="false" />
                                                            <asp:BoundField DataField="estado" HeaderText="ESTADO" ItemStyle-Width="15%" ItemStyle-Wrap="false" />
                                                            <asp:TemplateField HeaderText="ACCIONES" ItemStyle-Width="20%" ItemStyle-Wrap="false">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="btnEditar" runat="server" 
                                                                        CommandName="Editar" 
                                                                        CssClass="btn btn-primary btn-sm" 
                                                                        ToolTip="Editar notificación"
                                                                        OnClientClick="return alertConfirm(this, event, '¿Desea editar el registro seleccionado?', 'warning');">
                                                                        <span><i class="fa fa-pen"></i></span>
                                                                    </asp:LinkButton>
                                                                    <asp:LinkButton ID="btnEliminar" runat="server" 
                                                                        CommandName="Eliminar" 
                                                                        CssClass="btn btn-danger btn-sm" 
                                                                        ToolTip="Eliminar notificación"
                                                                        OnClientClick="return alertConfirm(this, event, '¿Desea eliminar el registro seleccionado?', 'error');">
                                                                        <span><i class="fa fa-trash"></i></span>
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                            
                                                    </asp:GridView>
                                                
                                                </ContentTemplate>-->
                                            </div>
                                        </div>
                                    </div>
                                
                                </div>
                            </div>
                          
                            
                        </ContentTemplate>
                
              
                </div> <!-- fin panel cabecera-->
               </div> <!-- fin listado-tab-->
               
              <div class="tab-pane" id="edicion" role="tabpanel" aria-labelledby="edicion-tab">
                  <div class="panel-cabecera">
                  
                        <ContentTemplate>
                            <div class="card">
                                <div class="card-header">Edición de Activo</div>
                                <div class="card-body">
                                    <div class="row">
                                        dfsdfsdfd
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                  </div> <!-- fin panel-cabecera-->
              </div> <!--fin registro-tab-->
           </div> <!-- fin tab-content-->

        </div> 
    </form>   
        
    </body>
</html>