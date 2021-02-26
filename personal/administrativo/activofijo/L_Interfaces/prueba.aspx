<%@ Page Language="VB" AutoEventWireup="false" CodeFile="prueba.aspx.vb" Inherits="administrativo_activofijo_L_Interfaces_prueba" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
             <meta charset="UTF-8"/>
        <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
        <meta http-equiv="X-UA-Compatible" content="IE=edge" />
        <meta http-equiv="X-UA-Compatible" content="IE=7" />
        <meta http-equiv="X-UA-Compatible" content="IE=8" />
        <meta http-equiv="X-UA-Compatible" content="IE=10" />

        <!-- Estilos externos -->
        <link rel="stylesheet" href="../../../assets/bootstrap-4.1/css/bootstrap.min.css" />
        <link rel="stylesheet" href="../../../assets/smart-wizard/css/smart_wizard.min.css" />
        <link rel="stylesheet" href="../../../assets/bootstrap-select-1.13.1/css/bootstrap-select.min.css" />
        <link rel="stylesheet" href="../../../assets/fontawesome-5.2/css/all.min.css" />
        <link rel="stylesheet" href="../../../Alumni/css/datatables/jquery.dataTables.min.css?12" />
        <link rel="stylesheet" href="../../../Alumni/css/sweetalert/sweetalert2.min.css" />

        <!-- Estilos propios -->
        <link rel="stylesheet" href="../../../Alumni/css/estilos.css?13" />

        <!-- Scripts externos -->
        <script src="../../../assets/jquery/jquery-3.3.1.js"></script>
        <script src="../../../assets/bootstrap-4.1/js/bootstrap.bundle.js"></script>
        <script src="../../../assets/bootstrap-select-1.13.1/js/bootstrap-select.js"></script>
        <script src="../../../Alumni/js/popper.js"></script>
        <script src="../../../Alumni/js/sweetalert/es6-promise.auto.min.js"></script>
        <script src="../../../Alumni/js/sweetalert/sweetalert2.js"></script>
        <script src="../../../Alumni/js/datatables/jquery.dataTables.min.js?20"></script>

        <!-- Scripts propios -->
        <script src="../../../Alumni/js/funciones.js?1"></script>

       <script type="text/javascript">
         
           function prueba()
           {
               alert('soy una prueba');
               $('#myModal').on('shown.bs.modal');
           }

         
       </script>

</head>
<body>
    <form id="form1" runat="server">
           <asp:ScriptManager ID="scmInformacionContable" runat="server"></asp:ScriptManager>
         <asp:UpdatePanel ID="udpScripts" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
            </asp:UpdatePanel>
        
      <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#myModal">
        Abrir modal 
      </button> 
       
       
          <!-- The Modal -->
          <div class="modal fade" id="myModal">
            <div class="modal-dialog modal-dialog-centered">
              <div class="modal-content">
      
                <!-- Modal Header -->
                <div class="modal-header">
                  <h4 class="modal-title">Modal Heading</h4>
                   
                 
                </div>
        
                <!-- Modal body -->
                <div class="modal-body">
                  Modal body..
                    <asp:Button runat="server"  id="btn1" Text="Enviar a servidor" 
                       class="btn btn-primary" data-toggle="modal" data-target="#myModal" AutoPostBack="False" />
                </div>
        
                <!-- Modal footer -->
                <div class="modal-footer">
                  <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                </div>
        
              </div>
            </div>
          </div>



          <div class="container-fluid">
          <!--/Cabecera de Panel-->
                <!--Tabs-->
                <ul class="nav nav-tabs" role="tablist">
                    <li class="nav-item">
                        <a href="#listado" id="listado-tab" class="nav-link active" data-toggle="tab" role="tab"
                            aria-controls="listado" aria-selected="true">Listado</a>

                    </li>
                    <li class="nav-item">
                        <a href="#detalle" id="detalle-tab" class="nav-link disabled" data-toggle="tab" role="tab"
                            aria-controls="detalle" aria-selected="false">Detalle</a>

                    </li>
                     <li class="nav-item">
                        <a href="#detalle" id="despacho-tab" class="nav-link disabled" data-toggle="tab" role="tab"
                            aria-controls="despacho" aria-selected="false">Despacho</a>

                    </li>
                </ul>

                       

        <!--PESTAÑA 02: TAB. DE DETALLE DE PEDIDO-->
       <div class="tab-pane" id="detalle" role="tabpanel" aria-labelledby="detalle-tab">
                                          




          <div class="table-responsive">
               
            
                              <asp:UpdatePanel ID="udpDetalle" runat="server" UpdateMode="Conditional"
                                ChildrenAsTriggers="false">                
                                             <ContentTemplate>
                                <asp:GridView ID="grwListaa" runat="server" Width="100%" AutoGenerateColumns="false"
                                        ShowHeader="true" DataKeyNames="codigo_af" CssClass="display table table-sm" 
                                        GridLines="None">
                                        <Columns>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center"
                                                HeaderText="Selec.">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSelecPedido33" CssClass ="claseChk" runat ="server" AutoPostBack="false" />                                  
                                                 </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="desc_af" HeaderText="ACTIVO FIJO" />
                                            <asp:BoundField DataField="resp_bien" HeaderText="RESP. DEL BIEN" />
                                            <asp:BoundField DataField="ubicacion" HeaderText="UBICACIÓN" />
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center"
                                                HeaderText="FIC. CONTROL PAT.">
                                                <ItemTemplate>
                                                 
                                                    <asp:Button id="txtPrueba" runat ="server" AutoPostBack="false" ></asp:Button>
                                                   
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                 

                                         
                                        </Columns>
                                        <EmptyDataTemplate>
                                            No se encontraron Datos!
                                        </EmptyDataTemplate>
                                        <HeaderStyle BackColor="#D9534F" ForeColor="White" VerticalAlign="Middle"
                                            HorizontalAlign="Center" Font-Size="12px" />
                                        <RowStyle Font-Size="11px" />
                                        <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                    </asp:GridView>
                 </ContentTemplate>
                            </asp:UpdatePanel>
               
           
                      </div>


           </div>

              </div>
        
    </form>

        <script type="text/javascript">

        $(document).ready(function() {

          
            
            $("#grwListaa [id*='chkSelecPedido33']").change(function() {
                  var aux1 = $("#grwListaa [id*='chkSelecPedido33']");


                var chk = $(this).prop('checked');
                var hola = $('tbody tr td span.claseChk input:checkbox');
                if (chk == true) {
                    $('tbody tr td span.claseChk input:checkbox').prop("checked", false);
                        $(this).prop("checked", true);
                             


                }
                

            });

            });


            
            

        
    </script>

</body>
</html>
