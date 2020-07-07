<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmOfertasLaborales.aspx.vb" Inherits="Egresados_frmOfertasLaborales" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" /> 
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta name="google" value="notranslate">
    <title>Ofertas Laborales</title>
    <%--<meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=7' />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <meta http-equiv='X-UA-Compatible' content='IE=11' />--%>
    <meta http-equiv="x-ua-compatible" content="IE=10" />
    <meta name="GENERATOR" content="Microsoft FrontPage 12.0" />
    
    
    <link href="../scripts/css/bootstrap.css" rel="Stylesheet" type="text/css" />
    <script src="../scripts/js/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="js/popper.js" type="text/javascript"></script>
    <script src="../scripts/js/bootstrap.min.js" type="text/javascript"></script>
    <%--<script src="../assets/js/bootstrap.min.js" type="text/javascript"></script>--%>
    
    <%-- Estilo para fechas --%>
    <link href="../assets/css/bootstrap-datepicker3.css" rel="Stylesheet" type="text/css" />
    <script src="../assets/js/bootstrap-datepicker.js" type="text/javascript"></script>
    <script src="../assets/fontawesome-5.2/js/all.min.js" type="text/javascript"></script>   
    <link href="../assets/fontawesome-5.2/css/all.min.css" rel="stylesheet" type="text/css" /> 
    
    <%-- Estilo Datatables--%>     
    <link type="text/css" rel="stylesheet" href="https://cdn.datatables.net/1.10.9/css/dataTables.bootstrap.min.css" />
    <script type="text/javascript" src="https://cdn.datatables.net/1.10.9/js/jquery.dataTables.min.js"></script>     
    <script type="text/javascript" src="https://cdn.datatables.net/1.10.9/js/dataTables.bootstrap.min.js"></script>        
    
    <script type="text/javascript">
    
         // jquery
        $(document).ready(function() {

        //funcion para el datatable
        $(function() {
        $('[id*=gvwOfertasDt]').prepend($("<thead><tr><th></th></tr></thead>").append($(this).find("tr:first"))).DataTable({
                "responsive": true,
                "sPaginationType": "full_numbers",
                language: {
                    "decimal": "",
                    "emptyTable": "No hay información",
                    "info": "Mostrando _START_ a _END_ de _TOTAL_ Ofertas",
                    "infoEmpty": "Mostrando 0 to 0 of 0 Entradas",
                    "infoFiltered": "(Filtrado de _MAX_ total entradas)",
                    "infoPostFix": "",
                    "thousands": ",",
                    "lengthMenu": "Mostrar _MENU_ Ofertas",
                    "loadingRecords": "Cargando...",
                    "processing": "Procesando...",
                    "search": "Buscar:",
                    "zeroRecords": "Sin resultados encontrados",
                    "paginate": {
                        "first": "Primero",
                        "last": "Ultimo",
                        "next": "Siguiente",
                        "previous": "Anterior"
                    }
                }
            });
        });
        //Fin funcion para el datatable
        
                
         
         
         
            // para el activar del datepicker
             $('#txtFechIni').datepicker({
                 autoclose: true, orientation: "top"
             });
             $('#txtFechFin').datepicker({
                 autoclose: true, orientation: "top"
             });
             $('#btn_calendIni').click(function() {
                 $("#txtFechIni").datepicker('show');
             });
             $('#btn_calendFin').click(function() {
                 $("#txtFechFin").datepicker('show');
             });
             //modalOferta
                $('#btnIniPub').click(function() {
                    $("#txtFechIniPub").datepicker('show');
                });
                $('#btnFinPub').click(function() {
                    $("#txtFechFinPub").datepicker('show');
                });
             
                
            // fin de activar el datepicker

         });        //fin jQuery escucha

     function form1_onclick() {

     };
    // abre el modal de agregar ofertas
     function openModal(acc, des) {
         $('#modalOfertaLaboral').modal('show');
     }
     // abre el modal de agregar ofertas
     function openModalEmp(acc, des) {
         $('#modalSelecEmpresa').modal('show');
     }
    // Cierra el modal
     function closeModal(ind){
         if (ind) {
             $('#modalOfertaLaboral').modal('hide');
             $('#modalOfertaLaboral').remove();
         }
      }
     
     </script>
     
     
    
</head>
<body>
    <form id="form1" runat="server" onclick="return form1_onclick()">
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release">
    </asp:ScriptManager>    
        <div class="container-fluid">
            <div></div>
            <br />
            <div class="panel panel-default" id="pnlLista" runat="server">
                <div class="panel panel-heading">
                    <div class="row">
                        <div class="col-md-8">
                            <h4>OFERTAS LABORALES</h4>
                        </div>
                        <div class="col-md-4">
                           <asp:LinkButton ID="lbActivas" runat="server" Text='0 Activas'
                            CssClass="btn btn-primary btn-round btn-xs"></asp:LinkButton>                            
                            <asp:LinkButton ID="lbByRevisar" runat="server" Text='0 Por Revisar'
                            CssClass="btn btn-danger btn-round btn-xs" ></asp:LinkButton>   
                        </div>
                    </div>
                </div>
                <div class="panel panel-body">
                     <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                 <label class="col-md-3">Inicio:</label>
                                   <div class="input-group date">
                                     <asp:TextBox ID="txtFechIni" runat="server" CssClass="form-control input-sm"
                                                data-provide="datepicker"></asp:TextBox>
                                            <span class="input-group-addon bg" id="btn_calendIni"><i class="fa fa-calendar"></i></span>
                                   </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                 <label class="col-md-3">Fin:</label>
                                   <div class="input-group date">
                                     <asp:TextBox ID="txtFechFin" runat="server" CssClass="form-control input-sm"
                                                data-provide="datepicker"></asp:TextBox>
                                            <span class="input-group-addon bg" id="btn_calendFin"><i class="fa fa-calendar"></i></span>
                                   </div>
                                   
                            </div>
                        </div>
                        <div class="col-md-2">
                            <asp:LinkButton ID="btnBuscaL" runat="server" Text='<i class="fa fa-search"></i> Busca' CssClass="btn btn-success btn-block"> </asp:LinkButton>
                            <%--<asp:Button ID="btnBusca" runat="server" Text="Busca" CssClass="btn btn-success btn-block" />--%>
                            <%--<asp:TextBox ID="txtPrueba" runat="server"></asp:TextBox>--%>
                        </div>
                        <div class="col-md-2">                        
                            <!--<asp:Button ID="Button1" runat="server" Text="Oferta Laboral" CssClass="btn btn-primary btn-block" />-->                            
                            <asp:LinkButton ID="btnCrear" runat="server" Text='<i class="fa fa-plus"></i> Oferta Laboral'
                            CssClass="btn btn-primary btn-block"></asp:LinkButton>
                        </div>                                                
                     </div>
                     <%--<div class="row">
                        <div class="col-md-12">                            
                            <div class="table-responsive">
                                <asp:GridView ID="gvwOfertas" runat="server" AutoGenerateColumns="False" 
                                CssClass="table table-striped table-bordered table-hover" AllowPaging="True"
                                OnPageIndexChanging="gvwOfertas_PageIndexChanging">                                
                                <Columns>
                                    <asp:BoundField HeaderText="EMPRESA" DataField="nombrePro" />
                                    <asp:BoundField HeaderText="OFERTA" DataField="titulo_ofe" />
                                    <asp:BoundField HeaderText="CONTACTO" DataField="contacto_ofe" />
                                    <asp:BoundField HeaderText="TELEFONO" DataField="telefonocontacto_ofe" />
                                    <asp:BoundField HeaderText="FECHA REG" DataField="fechaReg_ofe" />
                                </Columns>
                                <EmptyDataTemplate>
                                    No se registró ninguna oferta laboral
                                </EmptyDataTemplate>
                                <HeaderStyle BackColor="#E33439" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center"
                                    Font-Size="12px" />
                                <EditRowStyle BackColor="#FFFFCC" Font-Size="Small" />
                                    <RowStyle Font-Size="13px" />
                                <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                            </asp:GridView>
                            </div>                            
                        </div>                        
                     </div>--%>
                     <div> <br /><br /></div>
                            <asp:GridView ID="gvwOfertasDt" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover" >
                                <RowStyle Font-Overline="False" Font-Size="12px" />
                            <Columns>
                                    <asp:BoundField HeaderText="EMPRESA" DataField="nombrePro" />
                                    <asp:BoundField HeaderText="OFERTA" DataField="titulo_ofe" />
                                    <asp:BoundField HeaderText="CONTACTO" DataField="contacto_ofe" />
                                    <asp:BoundField HeaderText="TELEFONO" DataField="telefonocontacto_ofe" />
                                    <asp:BoundField HeaderText="FECHA REG" DataField="fechaReg_ofe" />
                                </Columns>
                                <HeaderStyle BackColor="#E33439" Font-Size="12px" ForeColor="White" />
                                <EmptyDataTemplate>
                                    No se registró ninguna oferta laboral
                                </EmptyDataTemplate>
                         </asp:GridView>
                        
                     <div class="row">
                        <div class="col-md-12">
                        </div>                        
                     </div>
                 </div>
            </div>
        </div>
        <!-- Modal Oferta Laboral -->
        <div id="modalOfertaLaboral" class="modal fade" role="dialog"
         data-backdrop="static" data-keyboard="false" aria-hidden="true" runat="server">
            <div class="modal-dialog modal-lg" role="document">
                <div class="modal-content">
                    <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                        <span class="modal-title"> OFERTA LABORAL</span>
                    </div>
                    <div class="modal-body">
                         <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                     <label class="col-xs-3">Título:</label>
                                     <div class="col-xs-12">
                                        <asp:TextBox ID="txtTitulo" runat="server" CssClass="form-control"></asp:TextBox>
                                     </div>                                
                                </div>                               
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                     <label class="col-xs-3">Empresa:</label>
                                     <div class="col-xs-10">
                                        <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control"></asp:TextBox>
                                     </div>
                                     <div class="col-xs-1">
                                         <asp:LinkButton ID="lbModOlBuscaEmp" runat="server" CssClass="btn btn-sm btn-success" Text='<i class="fa fa-search"></i>'></asp:LinkButton>                                     
                                     </div>                                
                                     <div class="col-xs-1">
                                         <asp:LinkButton ID="LinkButton3" runat="server" CssClass="btn btn-sm btn-primary" Text='<i class="fa fa-plus"></i>'></asp:LinkButton>                                     
                                     </div>                                
                                </div>                               
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                     <label class="col-xs-3">Descripción:</label>
                                     <div class="col-xs-12">
                                        <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" Rows="3" 
                                             TextMode="MultiLine"></asp:TextBox>
                                     </div>                                
                                </div>                               
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                     <label class="col-xs-3">Requisitos:</label>
                                     <div class="col-xs-12">
                                        <asp:TextBox ID="txtRequisitos" runat="server" CssClass="form-control" Rows="3" 
                                             TextMode="MultiLine"></asp:TextBox>
                                     </div>                                
                                </div>                               
                            </div>
                        </div>
                        <br />
                        <div class="row">
                             <div class="col-md-6">
                                <div class="form-group">
                                    <label class="col-md-3">Departamento:</label>
                                    <div class="col-md-9">
                                        <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>                        
                            </div> 
                            <div class="col-md-6">
                                <div class="form-group">
                                     <label class="col-md-3">Lugar:</label>
                                     <div class="col-md-9">
                                        <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control"></asp:TextBox>
                                     </div>                                
                                </div>                               
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-6">
                                 <div class="form-group">
                                    <label class="col-md-3">Tipo:</label>
                                    <div class="col-md-9">
                                        <asp:DropDownList ID="DropDownList3" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>                                          
                            </div> 
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="col-md-3">Sector:</label>
                                    <div class="col-md-9">
                                        <asp:DropDownList ID="DropDownList4" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>         
                            </div>                            
                        </div>    
                        <br />
                        <div class="row">
                             <div class="col-md-6">
                                <div class="form-group">
                                     <label class="col-md-3">Contacto:</label>
                                     <div class="col-md-9">
                                        <asp:DropDownList ID="ddlContacto" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                     </div>                      
                                </div>                               
                            </div>                        
                            <div class="col-md-6">
                                <div class="form-group">
                                     <label class="col-md-3">Teléfono:</label>
                                     <div class="col-md-9">
                                        <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control"></asp:TextBox>
                                     </div>                                
                                </div>                               
                            </div>                        
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="col-md-3">Tipo Trabajo:</label>
                                    <div class="col-md-9">
                                        <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>          
                            </div>
                       </div> 
                       <br /> 
                    <div class="row">
                        <div class="col-md-8">
                             <div class="form-group">
                                 <label class="col-md-2">Correo:</label>
                                 <div class="col-md-10">
                                    <asp:TextBox ID="TextBox5" runat="server" CssClass="form-control"></asp:TextBox>                                    
                                 </div>
                                 
                            </div>          
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <div class="col-md-12">                                    
                                <asp:CheckBox ID="CheckBox1" runat="server" Text=" Mostrar Correo" /> 
                            </div>                                                
                            </div>                            
                        </div>
                    </div>
                    <br />   
                    <div class="row">
                        <div class="col-md-8">
                            <div class="form-group">
                                 <label class="col-md-2">Web:http://</label>
                                 <div class="col-md-10">
                                    <asp:TextBox ID="TextBox6" runat="server" CssClass="form-control"></asp:TextBox>                                    
                                 </div>                                 
                            </div>  
                        </div>
                        <div class="col-md-4">
                             <label class="col-md-6">Postular vía:</label>
                            <asp:RadioButtonList ID="RadioButtonList1" runat="server"   
                                 RepeatDirection="Horizontal">
                                <asp:ListItem> Web </asp:ListItem>
                                <asp:ListItem> Correo </asp:ListItem>
                            </asp:RadioButtonList>
                        </div>                        
                    </div>
                    <br />
                    <div class="row">
                          <div class="col-md-8">
                               <div class="form-group">
                                    <%--<label class="col-md-12">Publicación</label> --%>
                                    <label class="col-md-2">Publicación Inicio:</label>
                                    <div class="col-md-5">
                                        <div class="input-group date">
                                         <asp:TextBox ID="txtFechIniPub" runat="server" CssClass="form-control input-sm"
                                                    data-provide="datepicker"></asp:TextBox>
                                                <span class="input-group-addon bg" id="btnIniPub"><i class="fa fa-calendar"></i></span>
                                       </div>
                                    </div>  
                                   <label class="col-md-1">Fin:</label>
                                   <div class="col-md-4">
                                        <div class="input-group date">
                                        <asp:TextBox ID="txtFechFinPub" runat="server" CssClass="form-control input-sm"
                                                data-provide="datepicker"></asp:TextBox>
                                            <span class="input-group-addon bg" id="btnFinPub"><i class="fa fa-calendar"></i></span>
                                        </div>
                                   </div>                                       
                                </div>                     
                        </div>
             
                    </div>                   
                    </div>
                    <div class="modal-footer">
                        <asp:LinkButton ID="bntModOlGuarda" runat="server" CssClass="btn btn-success" Text="Guardar">
                            
                        </asp:LinkButton>
                        <asp:LinkButton ID="btnModOlCancela" runat="server" CssClass="btn btn-default" Text="Cancelar"
                             OnClientClick="closeModal(true);">
                        </asp:LinkButton>   
                    </div>
                </div>                
            </div> 
        </div>
        <!-- Fin Modal ol-->
        <!--Modal Empresa-->
        <div id="modalSelecEmpresa" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false" aria-hidden="true" runat="server">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header" style="background-color:#E33439; color: White; font-weight: bold;">
                        <span class="modal-title"> Seleccione Empresa</span>
                    </div>
                    <div class="modal-body">
                    </div>
               </div>
           </div>
        </div>
        <!--Fin Modal Empresa-->
    </form>
</body>
</html>
